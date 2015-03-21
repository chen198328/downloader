using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ivony.Html.Parser;
using Ivony.Html;
using System.Net;
using System.IO;
using System.Threading;
namespace JCRDownload.Code
{
    public class JCR
    {
        public static List<string> Errors = new List<string>();
        /// <summary>
        /// 学科代码提取学科名和学科代码
        /// </summary>
        /// <param name="html"></param>
        /// <returns>如果返回null，表示有错误</returns>
        public static Dictionary<string, string> ExtractCategoryCode(string html)
        {
            Dictionary<string, string> categorycode = new Dictionary<string, string>();
            IHtmlDocument Document = new JumonyParser().Parse(html);
            if (html.Contains("query_data") && html.Contains("option"))
            {
                IHtmlElement element = Document.FindSingle("#query_data");
                if (element != null)
                {
                    var nodes = element.Find("option");


                    foreach (var node in nodes)
                    {
                        if (!string.IsNullOrWhiteSpace(node.InnerText()))
                        {
                            categorycode[node.InnerText()] = node.Attribute("value").Value();
                        }
                    }
                }
                return categorycode;
            }
            else
            {
                return categorycode;
            }
        }

        /// <summary>
        /// 根据学科代码获取期刊列表数据
        /// </summary>
        /// <param name="code">学科代码</param>
        /// <param name="jcrsid"></param>
        /// <param name="category">学科名</param>
        /// <param name="interval">分页请求之间的间隙,单位毫秒</param>
        /// <![CDATA[猜测在post页面并返回页面的时候，系统使用serer.transfer类似的技术，实际页面已经是列表页了，但是在URL上并没有体现]]>
        /// <returns></returns>
        public static List<Journal> GetJournalsFromCategory(string category, string code, string jcrsid, int interval = 2000)
        {
            CookieContainer cookiecontainer = new CookieContainer();
            cookiecontainer.Add(new Cookie("jcrsid", jcrsid, "/", "admin-apps.webofknowledge.com"));
            // cookiecontainer.Add(new Cookie("AKSB", "s=1425784316155&r=http%3A//admin-apps.webofknowledge.com/JCR/JCR", "/", "admin-apps.webofknowledge.com"));
            List<Journal> journallist = new List<Journal>();
            string url = "http://admin-apps.webofknowledge.com/JCR/JCR?RQ=LIST_SUMMARY_JOURNAL&cursor=1";
            string postdata = string.Format("query_data={0}+&RQ=LIST_SUMMARY_JOURNAL&journal_sort_by=title&category_sort_by=cat_title&query_type=category&query_new=true&Submit.x=54&Submit.y=15", code);
            string html = GetHtml(url, ref cookiecontainer, postdata);
            int itemcount = JCR.GetItemCount(html);
            if (!string.IsNullOrEmpty(html))
            {
                List<Journal> journals = GetJournals(html, category);
                if (journals == null || journals.Count == 0)
                {
                    return journallist;
                }
                else
                {
                    journallist.AddRange(journals);
                    if (journals.Count >= 20)
                    {//有可能有第二页内容
                        for (int index = 21; index < int.MaxValue; index += 20)
                        {
                            url = string.Format("http://admin-apps.webofknowledge.com/JCR/JCR?RQ=LIST_SUMMARY_JOURNAL&cursor={0}", index);
                            Thread.Sleep(interval);
                            html = GetHtml(url, cookiecontainer);
                            if (!string.IsNullOrEmpty(html))
                            {
                                journals = GetJournals(html, category);
                                if (journals == null || journals.Count == 0)
                                {
                                    //空列表，表示期刊列表已经被获取完成了
                                    break;
                                }
                                else
                                {
                                    journallist.AddRange(journals);
                                    if (journals.Count < 20)
                                    {
                                        //不满20，表示这是最后一页
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

            }


            if (journallist.Count != itemcount)
            {
                Errors.Add(string.Format("{0}应该下载{0}条记录，实际记录为{1}", category, itemcount, journallist.Count));
            }
            else
            {
                //每个页面挨个下载
                journallist.ForEach(p =>
                {
                    Thread.Sleep(interval);
                    try
                    {
                        p = GetJournal(p, cookiecontainer);
                    }
                    catch (Exception ex)
                    {
                        Errors.Add(string.Format("{0}({1}):{2}", p.JCRAbbreviatedTitle, p.DetailURL, ex.Message.ToString()));
                    }
                });
            }
            return journallist;
        }
        /// <summary>
        /// 获取列表总数
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static int GetItemCount(string html)
        {
            IHtmlDocument Document = new JumonyParser().Parse(html);
            var tables = Document.Find("table");
            int index = 0;
            string value = string.Empty;
            foreach (var table in tables)
            {
                index++;
                if (index == 6)
                {
                    var tds = table.Find("td");
                    index = 0;
                    foreach (var td in tds)
                    {
                        index++;
                        if (index == 1)
                        {
                            value = td.InnerText();
                            break;
                        }
                    }
                    break;
                }
            }
            ////Journals([\s|\S]*?)\)
            //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"of &nbsp;[0-9]*\)");
            //string value = regex.Match(html).Value;
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            else
            {
                value = value.Split(new string[] { "of", ")" }, StringSplitOptions.None)[1];
                int items = 0;
                int.TryParse(value.Trim(), out items);
                return items;
            }
        }
        private static string GetHtml(string url, ref CookieContainer cookiecontainer, string postdata)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.CookieContainer = cookiecontainer;
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Host = "admin-apps.webofknowledge.com";
                request.Referer = "http://admin-apps.webofknowledge.com/JCR/JCR";
                request.Method = "POST";
                request.AllowAutoRedirect = true;

                byte[] post = System.Text.Encoding.UTF8.GetBytes(postdata);
                Stream stream = request.GetRequestStream();
                stream.Write(post, 0, post.Length);
                stream.Flush();
                stream.Close();

                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string html = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    CookieCollection cookies = response.Cookies;
                    return html;
                }
                else { return null; }
            }
            catch (WebException ex)
            {
                Errors.Add(string.Format("{0}:{1}", url, ex.Message.ToString()));
                return null;
                //此处应该有日志记录
            }
            catch (IOException ex)
            {
                Errors.Add(string.Format("{0}:{1}", url, ex.Message.ToString()));
                return null;
                //此处应该有日志记录
            }
        }
        private static string GetHtml(string url, CookieContainer cookiecontainer)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8,text/vnd.wap.wml;q=0.6";
                request.UserAgent = "	Mozilla/5.0 (Windows NT 6.1; rv:36.0) Gecko/20100101 Firefox/36.0";
                request.Host = "admin-apps.webofknowledge.com";
                request.Referer = "http://admin-apps.webofknowledge.com/JCR/JCR";
                request.CookieContainer = cookiecontainer;
                // request.Connection = "keep-alive";
                request.Method = "GET";
                request.AllowAutoRedirect = true;
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string html = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    return html;
                }
                else { return null; }
            }
            catch (WebException ex)
            {
                Errors.Add(string.Format("{0}:{1}", url, ex.Message.ToString()));
                return null;
                //此处应该有日志记录
            }
            catch (IOException ex)
            {
                Errors.Add(string.Format("{0}:{1}", url, ex.Message.ToString()));
                return null;
                //此处应该有日志记录
            }
        }
        private static Journal GetJournal(IHtmlElement element, string category = null)
        {
            if (element != null)
            {
                var tds = element.Find("td");
                if (tds != null)
                {
                    Journal journal = new Journal();
                    journal.Category = category;
                    int index = 0;

                    foreach (var td in tds)
                    {
                        index++;
                        string value = td.InnerText();
                        switch (index)
                        {
                            #region  列数据读取
                            case 3:
                                //简称和url
                                var a = td.FindFirst("a");
                                journal.JCRAbbreviatedTitle = a.InnerText();
                                journal.DetailURL = a.Attribute("href").Value();
                                break;
                            case 4:
                                journal.ISSN = value;
                                break;
                            case 5:
                                if (string.IsNullOrEmpty(value))
                                {
                                    journal.TotalCites = -1;
                                }
                                else
                                {
                                    int num = 0;
                                    int.TryParse(value, out num);
                                    journal.TotalCites = num;
                                }

                                break;
                            case 6:
                                if (string.IsNullOrEmpty(value))
                                {
                                    journal.ImpactFactor = -1;
                                }
                                else
                                {
                                    double factor = 0;
                                    double.TryParse(value, out factor);
                                    journal.ImpactFactor = factor;
                                }
                                break;
                            case 7:
                                if (string.IsNullOrEmpty(value))
                                {
                                    journal.ImpactFactorFor5years = -1;
                                }
                                else
                                {
                                    double factor5 = 0;
                                    double.TryParse(value, out factor5);
                                    journal.ImpactFactorFor5years = factor5;
                                }
                                break;
                            case 8:
                                if (string.IsNullOrEmpty(value))
                                {
                                    journal.ImmediacyIndex = -1;
                                }
                                else
                                {
                                    double immindex = 0;
                                    double.TryParse(value, out immindex);
                                    journal.ImmediacyIndex = immindex;
                                }
                                break;
                            case 9:
                                if (string.IsNullOrEmpty(value))
                                {
                                    journal.Aritcles = -1;
                                }
                                else
                                {
                                    int articles = 0;
                                    int.TryParse(value, out articles);
                                    journal.Aritcles = articles;
                                }
                                break;
                            case 10:
                                if (string.IsNullOrEmpty(value))
                                {
                                    journal.CitedHalfLife = -1;
                                }
                                else if (value.Contains(">"))
                                {
                                    journal.CitedHalfLife = 10.1;
                                }
                                else
                                {
                                    double chl = 0;
                                    double.TryParse(value, out chl);
                                    journal.CitedHalfLife = chl;
                                    break;
                                }
                                break;
                            case 11:
                                if (string.IsNullOrEmpty(value))
                                {
                                    journal.EigenfactorScore = -1;
                                }
                                else
                                {
                                    double efscore = 0;
                                    double.TryParse(value, out efscore);
                                    journal.EigenfactorScore = efscore;
                                }
                                break;
                            case 12:
                                if (string.IsNullOrEmpty(value))
                                {
                                    journal.ArticleInfluenceScore = -1;
                                }
                                else
                                {
                                    double ais = 0;
                                    double.TryParse(value, out ais);
                                    journal.ArticleInfluenceScore = ais;
                                }
                                break;
                            default: break;
                            #endregion
                        }
                    }
                    return journal;
                }
            }
            return null;
        }
        private static List<Journal> GetJournals(string html, string category = null)
        {
            var document = new JumonyParser().Parse(html);
            var tables = document.Find("table");
            List<Journal> journallist = new List<Journal>();
            foreach (var table in tables)
            {
                if (table != null)
                {
                    if (table.Attribute("bordercolor").Value() != "#CCCCCC")
                    {
                        continue;
                    }
                    else
                    {
                        var trs = table.Find("tr");
                        int index = -2;
                        foreach (var tr in trs)
                        {

                            index++;
                            if (index <= 0) continue;
                            Journal journal = GetJournal(tr, category);
                            if (journal != null)
                            {
                                journallist.Add(journal);
                            }
                        }
                    }
                }
            }
            return journallist;
        }

        private static Journal GetJournal(Journal journal, CookieContainer cookiecontainer)
        {
            if (string.IsNullOrEmpty(journal.DetailURL))
                throw new ArgumentNullException("url为null");
            string html = GetHtml(journal.DetailURL, cookiecontainer);
            return GetJournal(journal, html);
        }
        public static Journal GetJournal(Journal journal, string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                throw new ArgumentNullException("html 为 nulll");
            }
            var document = new JumonyParser().Parse(html);
            var table = GetTable(document, 7);
            var tr = GetRowColumn(table, 1, "tr");
            if (tr != null)
            {
                var td = GetRowColumn(tr, 2, "td");
                journal.Title = td.InnerText().Trim();
            }
            tr = GetRowColumn(table, 2, "tr");
            if (tr != null)
            {
                var td = GetRowColumn(tr, 2, "td");
                journal.ISOAbbreviatedTitle = td.InnerText().Trim();
            }
            tr = GetRowColumn(table, 5, "tr");
            if (tr != null)
            {
                var td = GetRowColumn(tr, 2, "td");
                journal.Issues = td.InnerText().Trim();
            }
            tr = GetRowColumn(table, 6, "tr");
            if (tr != null)
            {
                var td = GetRowColumn(tr, 2, "td");
                journal.Language = td.InnerText().Trim();
            }
            tr = GetRowColumn(table, 7, "tr");
            if (tr != null)
            {
                var td = GetRowColumn(tr, 2, "td");
                journal.Country = td.InnerText().Trim();
            }
            tr = GetRowColumn(table, 8, "tr");
            if (tr != null)
            {
                var td = GetRowColumn(tr, 2, "td");
                journal.Publisher = td.InnerText().Trim();
            }
            tr = GetRowColumn(table, 9, "tr");
            if (tr != null)
            {
                var td = GetRowColumn(tr, 2, "td");
                journal.Address = td.InnerText().Trim();
            }
            return journal;
        }
        /// <summary>
        /// 获取指定表
        /// </summary>
        /// <param name="document"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private static IHtmlElement GetTable(IHtmlDocument document, int position)
        {
            var tables = document.Find("table");
            int index = 0;
            foreach (var table in tables)
            {
                index++;
                if (index == position)
                    return table;
            }
            return null;
        }
        /// <summary>
        /// 获取指定行
        /// </summary>
        /// <param name="element"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private static IHtmlElement GetRowColumn(IHtmlElement element, int position, string trortd)
        {
            var trs = element.Find(trortd);
            int index = 0;
            foreach (var tr in trs)
            {
                index++;
                if (index == position)
                {
                    return tr;
                }
            }
            return null;
        }
    }
}
