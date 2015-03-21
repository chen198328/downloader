using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ivony.Html.Parser;
using Ivony.Html;
using JCRDownload.Code;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
namespace JCRDownload
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text.Trim();
            webBrowser1.Navigate(url);
        }
        Dictionary<string, string> categorycode = new Dictionary<string, string>();
        List<Journal> journallist = new List<Journal>();
        private void btnExtractCategory_Click(object sender, EventArgs e)
        {
            string html = webBrowser1.DocumentText;
            Dictionary<string, string> _categorycode = JCR.ExtractCategoryCode(html);
            if (_categorycode.Count == 0)
            {
                MessageBox.Show("没有提取到学科代码，请注意所在页面是否是学科代码页");
            }
            else
            {
                categorycode = _categorycode;
                cbxCategorys.Items.Clear();
                cbxCategorys.Items.Add("全部");
                foreach (var category in _categorycode)
                {
                    cbxCategorys.Items.Add(category.Key);
                }
                cbxCategorys.SelectedIndex = 0;
                string message = "总共提取" + _categorycode.Count + "个学科";
                MessageBox.Show(message);
                AddLog(message);
            }

        }
        public void AddLog(string message)
        {
            Action<string> addlog = (ms) =>
            {
                richTextBox1.AppendText(string.Format("{0}:{1}\r\n", DateTime.Now, message));
                richTextBox1.ScrollToCaret();
            };
            this.BeginInvoke(addlog, message);
        }

        public void UpdateLabel(Label label, string text)
        {
            Action<Label, string> updatelabel = (lbl, value) =>
            {

                lbl.Text = value;
            };
            this.BeginInvoke(updatelabel, label, text);
        }
        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            textBox1.Text = webBrowser1.Url.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cookies = webBrowser1.Document.Cookie;
            if (cookies == null || !cookies.Contains("jcrsid"))
            {
                string message = "无法获取jcrsid，无法抓取数据";
                AddLog(message);
                MessageBox.Show(message);
                return;
            }
            string jcrsid = cookies.Split(';')[0].Split('=')[1];
            AddLog("获取jcrsid:" + jcrsid);


            int categoryinterval = GetInt(txtCategoryInterval.Text.Trim());
            int pageinterval = GetInt(txtPageInterval.Text.Trim());
            string selectedcategory = cbxCategorys.SelectedItem.ToString();
            Task task = new Task(() =>
            {
                JCR.Errors.Clear();
                journallist.Clear();
                if (selectedcategory == "全部")
                {
                    AddLog("开始下载列表数据：" + categorycode.Count + "学科");
                    int index = 1;
                    foreach (var category in categorycode)
                    {
                        List<Journal> journals = JCR.GetJournalsFromCategory(category.Key, category.Value, jcrsid, pageinterval);
                        journallist.AddRange(journals);
                        AddLog(index + "个学科:" + category.Key + " 记录数：" + journals.Count);
                        UpdateLabel(lblDownloadedCategoryCount, "下载学科数：" + index);
                        UpdateLabel(lblDownloadedItemCount, "下载记录数：" + journallist.Count);
                        index++;
                        Thread.Sleep(categoryinterval);
                    }
                }
                else
                {
                    AddLog("开始下载" + selectedcategory + "学科");
                    List<Journal> journals = JCR.GetJournalsFromCategory(selectedcategory, categorycode[selectedcategory], jcrsid, pageinterval);
                    journallist.AddRange(journals);
                    AddLog("记录数：" + journals.Count);
                    UpdateLabel(lblDownloadedCategoryCount, "下载学科数：1");
                    UpdateLabel(lblDownloadedItemCount, "下载记录数：" + journallist.Count);
                }
            });
            task.Start();
            task.ContinueWith(t =>
            {
                MessageBox.Show("下载已完成");
                AddLog("下载已完成");
                if (JCR.Errors.Count > 0)
                {
                    AddLog("下载数据时出现以下问题请注意");
                    foreach (var error in JCR.Errors)
                    {
                        AddLog(error);
                    }
                }
            });

        }
        private int GetInt(string number)
        {
            int num = 0;
            int.TryParse(number, out num);
            return num;
        }

        private void btnViewData_Click(object sender, EventArgs e)
        {
            ViewData viewdata = new ViewData();
            viewdata.Journals = journallist;
            viewdata.Show();
        }

    }
}
