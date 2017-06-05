namespace MySQLWatcher
{
    partial class FrmMain
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnStart = new System.Windows.Forms.Button();
            this.tbPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbHostIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDbName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutUsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            this.tbOperUserName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbOperPwd = new System.Windows.Forms.TextBox();
            this.lbNote = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(577, 42);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(152, 48);
            this.btnStart.TabIndex = 20;
            this.btnStart.Text = "开始巡检";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbPwd
            // 
            this.tbPwd.Location = new System.Drawing.Point(425, 42);
            this.tbPwd.Margin = new System.Windows.Forms.Padding(2);
            this.tbPwd.Name = "tbPwd";
            this.tbPwd.Size = new System.Drawing.Size(136, 21);
            this.tbPwd.TabIndex = 19;
            this.tbPwd.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(363, 46);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "密码：";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(149, 42);
            this.tbUserName.Margin = new System.Windows.Forms.Padding(2);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(136, 21);
            this.tbUserName.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "用户名：";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(636, 15);
            this.tbPort.Margin = new System.Windows.Forms.Padding(2);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(93, 21);
            this.tbPort.TabIndex = 15;
            this.tbPort.Text = "3306";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(574, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "端口：";
            // 
            // tbHostIP
            // 
            this.tbHostIP.Location = new System.Drawing.Point(149, 15);
            this.tbHostIP.Margin = new System.Windows.Forms.Padding(2);
            this.tbHostIP.Name = "tbHostIP";
            this.tbHostIP.Size = new System.Drawing.Size(173, 21);
            this.tbHostIP.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "数据库主机地址：";
            // 
            // tbDbName
            // 
            this.tbDbName.Location = new System.Drawing.Point(425, 15);
            this.tbDbName.Margin = new System.Windows.Forms.Padding(2);
            this.tbDbName.Name = "tbDbName";
            this.tbDbName.Size = new System.Drawing.Size(136, 21);
            this.tbDbName.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(339, 19);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 22;
            this.label5.Text = "数据库名：";
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(11, 155);
            this.webBrowser.Margin = new System.Windows.Forms.Padding(2);
            this.webBrowser.MinimumSize = new System.Drawing.Size(16, 17);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(1291, 786);
            this.webBrowser.TabIndex = 24;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbNote);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.tbOperUserName);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.tbOperPwd);
            this.panel1.Controls.Add(this.tbHostIP);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbPort);
            this.panel1.Controls.Add(this.tbDbName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbUserName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbPwd);
            this.panel1.Location = new System.Drawing.Point(0, 29);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1314, 122);
            this.panel1.TabIndex = 41;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.ConfigToolStripMenuItem,
            this.aboutUsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1314, 32);
            this.menuStrip1.TabIndex = 42;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportSaveToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(58, 28);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // reportSaveToolStripMenuItem
            // 
            this.reportSaveToolStripMenuItem.Enabled = false;
            this.reportSaveToolStripMenuItem.Name = "reportSaveToolStripMenuItem";
            this.reportSaveToolStripMenuItem.Size = new System.Drawing.Size(159, 30);
            this.reportSaveToolStripMenuItem.Text = "另存为...";
            this.reportSaveToolStripMenuItem.Click += new System.EventHandler(this.reportSaveToolStripMenuItem_Click);
            // 
            // ConfigToolStripMenuItem
            // 
            this.ConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customConfigToolStripMenuItem,
            this.toolStripMenuItem1});
            this.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            this.ConfigToolStripMenuItem.Size = new System.Drawing.Size(58, 28);
            this.ConfigToolStripMenuItem.Text = "配置";
            // 
            // customConfigToolStripMenuItem
            // 
            this.customConfigToolStripMenuItem.Name = "customConfigToolStripMenuItem";
            this.customConfigToolStripMenuItem.Size = new System.Drawing.Size(219, 30);
            this.customConfigToolStripMenuItem.Text = "自定义参数配置";
            this.customConfigToolStripMenuItem.Click += new System.EventHandler(this.customConfigToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(216, 6);
            // 
            // aboutUsToolStripMenuItem
            // 
            this.aboutUsToolStripMenuItem.Name = "aboutUsToolStripMenuItem";
            this.aboutUsToolStripMenuItem.Size = new System.Drawing.Size(58, 28);
            this.aboutUsToolStripMenuItem.Text = "关于";
            this.aboutUsToolStripMenuItem.Click += new System.EventHandler(this.aboutUsToolStripMenuItem_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 72);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 15);
            this.label6.TabIndex = 24;
            this.label6.Text = "操作系统用户名：";
            // 
            // tbOperUserName
            // 
            this.tbOperUserName.Location = new System.Drawing.Point(149, 69);
            this.tbOperUserName.Margin = new System.Windows.Forms.Padding(2);
            this.tbOperUserName.Name = "tbOperUserName";
            this.tbOperUserName.Size = new System.Drawing.Size(136, 21);
            this.tbOperUserName.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(315, 72);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 15);
            this.label7.TabIndex = 26;
            this.label7.Text = "操作系统密码：";
            // 
            // tbOperPwd
            // 
            this.tbOperPwd.Location = new System.Drawing.Point(425, 69);
            this.tbOperPwd.Margin = new System.Windows.Forms.Padding(2);
            this.tbOperPwd.Name = "tbOperPwd";
            this.tbOperPwd.Size = new System.Drawing.Size(136, 21);
            this.tbOperPwd.TabIndex = 27;
            this.tbOperPwd.UseSystemPasswordChar = true;
            // 
            // lbNote
            // 
            this.lbNote.AutoSize = true;
            this.lbNote.ForeColor = System.Drawing.Color.Red;
            this.lbNote.Location = new System.Drawing.Point(146, 102);
            this.lbNote.Name = "lbNote";
            this.lbNote.Size = new System.Drawing.Size(349, 15);
            this.lbNote.TabIndex = 28;
            this.lbNote.Text = "获取日志相关参数（仅Linux系统）需输入操作系统账号及密码！";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 950);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.webBrowser);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MySQL巡检助手";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbHostIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDbName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutUsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportSaveToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbOperUserName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbOperPwd;
        private System.Windows.Forms.Label lbNote;
    }
}

