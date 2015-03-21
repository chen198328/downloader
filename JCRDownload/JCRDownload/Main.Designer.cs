namespace JCRDownload
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnExtractCategory = new System.Windows.Forms.Button();
            this.cbxCategorys = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.txtCategoryInterval = new System.Windows.Forms.TextBox();
            this.lable1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPageInterval = new System.Windows.Forms.TextBox();
            this.lblDownloadedCategoryCount = new System.Windows.Forms.Label();
            this.lblDownloadedItemCount = new System.Windows.Forms.Label();
            this.btnViewData = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(764, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "http://www.webofknowledge.com/JCR";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(790, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "进入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnExtractCategory
            // 
            this.btnExtractCategory.Location = new System.Drawing.Point(23, 17);
            this.btnExtractCategory.Name = "btnExtractCategory";
            this.btnExtractCategory.Size = new System.Drawing.Size(107, 23);
            this.btnExtractCategory.TabIndex = 4;
            this.btnExtractCategory.Text = "提取学科";
            this.btnExtractCategory.UseVisualStyleBackColor = true;
            this.btnExtractCategory.Click += new System.EventHandler(this.btnExtractCategory_Click);
            // 
            // cbxCategorys
            // 
            this.cbxCategorys.FormattingEnabled = true;
            this.cbxCategorys.Location = new System.Drawing.Point(147, 17);
            this.cbxCategorys.Name = "cbxCategorys";
            this.cbxCategorys.Size = new System.Drawing.Size(386, 20);
            this.cbxCategorys.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(23, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "提取数据";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(13, 40);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(926, 361);
            this.webBrowser1.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 408);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(923, 120);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnViewData);
            this.tabPage1.Controls.Add(this.lblDownloadedItemCount);
            this.tabPage1.Controls.Add(this.lblDownloadedCategoryCount);
            this.tabPage1.Controls.Add(this.btnExtractCategory);
            this.tabPage1.Controls.Add(this.cbxCategorys);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(915, 94);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "操作";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.richTextBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(915, 94);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "日志";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.txtPageInterval);
            this.tabPage3.Controls.Add(this.lable1);
            this.tabPage3.Controls.Add(this.txtCategoryInterval);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(915, 94);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "配置";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(909, 88);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // txtCategoryInterval
            // 
            this.txtCategoryInterval.Location = new System.Drawing.Point(163, 15);
            this.txtCategoryInterval.Name = "txtCategoryInterval";
            this.txtCategoryInterval.Size = new System.Drawing.Size(120, 21);
            this.txtCategoryInterval.TabIndex = 0;
            this.txtCategoryInterval.Text = "2000";
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Location = new System.Drawing.Point(27, 20);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(125, 12);
            this.lable1.TabIndex = 1;
            this.lable1.Text = "学科间时间间隔(毫秒)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "分页间时间间隔(毫秒)";
            // 
            // txtPageInterval
            // 
            this.txtPageInterval.Location = new System.Drawing.Point(163, 46);
            this.txtPageInterval.Name = "txtPageInterval";
            this.txtPageInterval.Size = new System.Drawing.Size(120, 21);
            this.txtPageInterval.TabIndex = 2;
            this.txtPageInterval.Text = "2000";
            // 
            // lblDownloadedCategoryCount
            // 
            this.lblDownloadedCategoryCount.AutoSize = true;
            this.lblDownloadedCategoryCount.Location = new System.Drawing.Point(149, 54);
            this.lblDownloadedCategoryCount.Name = "lblDownloadedCategoryCount";
            this.lblDownloadedCategoryCount.Size = new System.Drawing.Size(77, 12);
            this.lblDownloadedCategoryCount.TabIndex = 7;
            this.lblDownloadedCategoryCount.Text = "下载的学科数";
            // 
            // lblDownloadedItemCount
            // 
            this.lblDownloadedItemCount.AutoSize = true;
            this.lblDownloadedItemCount.Location = new System.Drawing.Point(322, 57);
            this.lblDownloadedItemCount.Name = "lblDownloadedItemCount";
            this.lblDownloadedItemCount.Size = new System.Drawing.Size(77, 12);
            this.lblDownloadedItemCount.TabIndex = 8;
            this.lblDownloadedItemCount.Text = "下载的记录数";
            // 
            // btnViewData
            // 
            this.btnViewData.Location = new System.Drawing.Point(539, 49);
            this.btnViewData.Name = "btnViewData";
            this.btnViewData.Size = new System.Drawing.Size(107, 23);
            this.btnViewData.TabIndex = 9;
            this.btnViewData.Text = "查看下载数据";
            this.btnViewData.UseVisualStyleBackColor = true;
            this.btnViewData.Click += new System.EventHandler(this.btnViewData_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 530);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnExtractCategory;
        private System.Windows.Forms.ComboBox cbxCategorys;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPageInterval;
        private System.Windows.Forms.Label lable1;
        private System.Windows.Forms.TextBox txtCategoryInterval;
        private System.Windows.Forms.Button btnViewData;
        private System.Windows.Forms.Label lblDownloadedItemCount;
        private System.Windows.Forms.Label lblDownloadedCategoryCount;
    }
}

