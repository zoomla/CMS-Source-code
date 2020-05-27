namespace DBDiff.Front
{
    partial class BasicForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BasicForm));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Config_Btn = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.dbText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.UserID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DataSource = new System.Windows.Forms.TextBox();
            this.SqlVersion = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dbList = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.updateBar = new System.Windows.Forms.ProgressBar();
            this.msginfo = new System.Windows.Forms.TextBox();
            this.exitBtn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menu = new System.Windows.Forms.ToolStripMenuItem();
            this.官方网站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据工具Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.技术论坛Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.程序下载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.主机服务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.商业购买ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动开发ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beginUpdateBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 72);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.label1.Size = new System.Drawing.Size(233, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Zoomla!逐浪CMS All To X3.8一键升级程序";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Config_Btn);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.dbText);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Password);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.UserID);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.DataSource);
            this.groupBox1.Controls.Add(this.SqlVersion);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(4, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(491, 209);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库信息";
            // 
            // Config_Btn
            // 
            this.Config_Btn.Location = new System.Drawing.Point(87, 13);
            this.Config_Btn.Name = "Config_Btn";
            this.Config_Btn.Size = new System.Drawing.Size(88, 23);
            this.Config_Btn.TabIndex = 22;
            this.Config_Btn.Text = "选择配置文件";
            this.Config_Btn.UseVisualStyleBackColor = true;
            this.Config_Btn.Click += new System.EventHandler(this.Config_Btn_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(46, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 21;
            this.label12.Text = "操作:";
            // 
            // dbText
            // 
            this.dbText.Location = new System.Drawing.Point(87, 101);
            this.dbText.Name = "dbText";
            this.dbText.Size = new System.Drawing.Size(152, 21);
            this.dbText.TabIndex = 2;
            this.dbText.Text = "ZoomlaCMS";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(11, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(467, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "请检查参数否正确或数据库服务器身份验证模式是否为SQL Server和Windows混合模式！";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(245, 156);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(173, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "有权限访问该数据库的有效密码";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Gray;
            this.label11.Location = new System.Drawing.Point(247, 101);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "请确认是否该数据是否存在";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Gray;
            this.label10.Location = new System.Drawing.Point(247, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 12);
            this.label10.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(245, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(185, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "有权限访问该数据库的有效用户名\r\n";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(87, 162);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '●';
            this.Password.Size = new System.Drawing.Size(152, 21);
            this.Password.TabIndex = 4;
            this.Password.Text = "ZoomlaCMS";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "用户口令:";
            // 
            // UserID
            // 
            this.UserID.Location = new System.Drawing.Point(87, 132);
            this.UserID.Name = "UserID";
            this.UserID.Size = new System.Drawing.Size(152, 21);
            this.UserID.TabIndex = 3;
            this.UserID.Text = "ZoomlaCMS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "用户名称:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "数据库名称:";
            // 
            // DataSource
            // 
            this.DataSource.Location = new System.Drawing.Point(87, 69);
            this.DataSource.Name = "DataSource";
            this.DataSource.Size = new System.Drawing.Size(152, 21);
            this.DataSource.TabIndex = 1;
            this.DataSource.Text = "(local)";
            // 
            // SqlVersion
            // 
            this.SqlVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SqlVersion.FormattingEnabled = true;
            this.SqlVersion.Items.AddRange(new object[] {
            "Sql Server 2008及更高版本",
            "Sql Server 2000",
            "Oracle数据库"});
            this.SqlVersion.Location = new System.Drawing.Point(87, 39);
            this.SqlVersion.Name = "SqlVersion";
            this.SqlVersion.Size = new System.Drawing.Size(195, 20);
            this.SqlVersion.TabIndex = 10;
            this.SqlVersion.TabStop = false;
            this.SqlVersion.SelectedIndexChanged += new System.EventHandler(this.SqlVersion_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "数据源IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "数据库版本:";
            // 
            // dbList
            // 
            this.dbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dbList.FormattingEnabled = true;
            this.dbList.Location = new System.Drawing.Point(403, 8);
            this.dbList.Name = "dbList";
            this.dbList.Size = new System.Drawing.Size(75, 20);
            this.dbList.TabIndex = 17;
            this.dbList.Visible = false;
            this.dbList.Click += new System.EventHandler(this.dbList_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.updateBar);
            this.groupBox2.Controls.Add(this.dbList);
            this.groupBox2.Controls.Add(this.msginfo);
            this.groupBox2.Location = new System.Drawing.Point(4, 315);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(491, 92);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "消息窗口";
            // 
            // updateBar
            // 
            this.updateBar.Location = new System.Drawing.Point(0, 63);
            this.updateBar.Name = "updateBar";
            this.updateBar.Size = new System.Drawing.Size(485, 23);
            this.updateBar.TabIndex = 1;
            // 
            // msginfo
            // 
            this.msginfo.BackColor = System.Drawing.SystemColors.Control;
            this.msginfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.msginfo.ForeColor = System.Drawing.Color.DimGray;
            this.msginfo.Location = new System.Drawing.Point(8, 17);
            this.msginfo.Multiline = true;
            this.msginfo.Name = "msginfo";
            this.msginfo.ShortcutsEnabled = false;
            this.msginfo.Size = new System.Drawing.Size(468, 40);
            this.msginfo.TabIndex = 100;
            this.msginfo.TabStop = false;
            this.msginfo.TextChanged += new System.EventHandler(this.msginfo_TextChanged);
            // 
            // exitBtn
            // 
            this.exitBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitBtn.Location = new System.Drawing.Point(239, 420);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(86, 23);
            this.exitBtn.TabIndex = 0;
            this.exitBtn.Text = "退出程序(&Q)";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 469);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(499, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 100;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.DimGray;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(359, 450);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(140, 19);
            this.linkLabel1.TabIndex = 100;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "访问ZoomLa!逐浪CMS官网";
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Black;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu});
            this.menuStrip1.Location = new System.Drawing.Point(398, 67);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(184, 25);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.SystemColors.HotTrack;
            this.menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.官方网站ToolStripMenuItem,
            this.数据工具Menu,
            this.技术论坛Menu,
            this.程序下载ToolStripMenuItem,
            this.主机服务ToolStripMenuItem,
            this.商业购买ToolStripMenuItem,
            this.移动开发ToolStripMenuItem});
            this.menu.ForeColor = System.Drawing.Color.White;
            this.menu.Name = "menu";
            this.menu.RightToLeftAutoMirrorImage = true;
            this.menu.Size = new System.Drawing.Size(84, 21);
            this.menu.Text = "便捷通道(&R)";
            this.menu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu.DropDownClosed += new System.EventHandler(this.menu_DropDownClosed);
            this.menu.DropDownOpened += new System.EventHandler(this.menu_DropDownOpened);
            // 
            // 官方网站ToolStripMenuItem
            // 
            this.官方网站ToolStripMenuItem.Name = "官方网站ToolStripMenuItem";
            this.官方网站ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.官方网站ToolStripMenuItem.Text = "官方网站";
            this.官方网站ToolStripMenuItem.Click += new System.EventHandler(this.官方网站ToolStripMenuItem_Click);
            // 
            // 数据工具Menu
            // 
            this.数据工具Menu.Name = "数据工具Menu";
            this.数据工具Menu.Size = new System.Drawing.Size(152, 22);
            this.数据工具Menu.Text = "数据工具";
            this.数据工具Menu.Click += new System.EventHandler(this.数据工具Menu_Click);
            // 
            // 技术论坛Menu
            // 
            this.技术论坛Menu.Name = "技术论坛Menu";
            this.技术论坛Menu.Size = new System.Drawing.Size(152, 22);
            this.技术论坛Menu.Text = "技术论坛";
            this.技术论坛Menu.Click += new System.EventHandler(this.技术论坛Menu_Click);
            // 
            // 程序下载ToolStripMenuItem
            // 
            this.程序下载ToolStripMenuItem.Name = "程序下载ToolStripMenuItem";
            this.程序下载ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.程序下载ToolStripMenuItem.Text = "程序下载";
            this.程序下载ToolStripMenuItem.Click += new System.EventHandler(this.程序下载ToolStripMenuItem_Click);
            // 
            // 主机服务ToolStripMenuItem
            // 
            this.主机服务ToolStripMenuItem.Name = "主机服务ToolStripMenuItem";
            this.主机服务ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.主机服务ToolStripMenuItem.Text = "主机服务";
            this.主机服务ToolStripMenuItem.Click += new System.EventHandler(this.主机服务ToolStripMenuItem_Click);
            // 
            // 商业购买ToolStripMenuItem
            // 
            this.商业购买ToolStripMenuItem.Name = "商业购买ToolStripMenuItem";
            this.商业购买ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.商业购买ToolStripMenuItem.Text = "商业购买";
            this.商业购买ToolStripMenuItem.Click += new System.EventHandler(this.商业购买ToolStripMenuItem_Click);
            // 
            // 移动开发ToolStripMenuItem
            // 
            this.移动开发ToolStripMenuItem.Name = "移动开发ToolStripMenuItem";
            this.移动开发ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.移动开发ToolStripMenuItem.Text = "移动开发";
            this.移动开发ToolStripMenuItem.Click += new System.EventHandler(this.移动开发ToolStripMenuItem_Click);
            // 
            // beginUpdateBtn
            // 
            this.beginUpdateBtn.Location = new System.Drawing.Point(121, 420);
            this.beginUpdateBtn.Name = "beginUpdateBtn";
            this.beginUpdateBtn.Size = new System.Drawing.Size(93, 23);
            this.beginUpdateBtn.TabIndex = 0;
            this.beginUpdateBtn.Text = "智能升级(&I)";
            this.beginUpdateBtn.UseVisualStyleBackColor = true;
            this.beginUpdateBtn.Click += new System.EventHandler(this.BeginUpdateBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::DBDiff.Properties.Resources.logo1;
            this.pictureBox1.Location = new System.Drawing.Point(0, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(499, 68);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pictureBox2.Location = new System.Drawing.Point(-19, 67);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(525, 25);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // BasicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(499, 491);
            this.Controls.Add(this.beginUpdateBtn);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "BasicForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zoomla!逐浪CMS All To X3.8 一键升级程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DataSource;
        private System.Windows.Forms.ComboBox SqlVersion;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox UserID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox msginfo;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu;
        private System.Windows.Forms.ToolStripMenuItem 官方网站ToolStripMenuItem;
        private System.Windows.Forms.Button beginUpdateBtn;
        private System.Windows.Forms.ComboBox dbList;
        private System.Windows.Forms.ProgressBar updateBar;
        private System.Windows.Forms.TextBox dbText;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button Config_Btn;
        private System.Windows.Forms.ToolStripMenuItem 数据工具Menu;
        private System.Windows.Forms.ToolStripMenuItem 技术论坛Menu;
        private System.Windows.Forms.ToolStripMenuItem 程序下载ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 主机服务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 商业购买ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动开发ToolStripMenuItem;
    }
}