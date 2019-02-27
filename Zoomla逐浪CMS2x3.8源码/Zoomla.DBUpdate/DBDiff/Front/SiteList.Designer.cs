namespace DBDiff.Front
{
    partial class SiteList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EGV = new System.Windows.Forms.DataGridView();
            this.站点名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.网站状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.程序池 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMS版本 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.操作 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.EGV)).BeginInit();
            this.SuspendLayout();
            // 
            // EGV
            // 
            this.EGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.站点名称,
            this.网站状态,
            this.程序池,
            this.CMS版本,
            this.操作});
            this.EGV.Location = new System.Drawing.Point(12, 13);
            this.EGV.Name = "EGV";
            this.EGV.RowTemplate.Height = 23;
            this.EGV.Size = new System.Drawing.Size(547, 296);
            this.EGV.TabIndex = 0;
            this.EGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EGV_CellContentClick);
            // 
            // 站点名称
            // 
            this.站点名称.DataPropertyName = "SiteName";
            this.站点名称.HeaderText = "站点名称";
            this.站点名称.Name = "站点名称";
            this.站点名称.ReadOnly = true;
            // 
            // 网站状态
            // 
            this.网站状态.DataPropertyName = "SiteState";
            this.网站状态.HeaderText = "网站状态";
            this.网站状态.Name = "网站状态";
            this.网站状态.ReadOnly = true;
            // 
            // 程序池
            // 
            this.程序池.DataPropertyName = "AppPoolName";
            this.程序池.HeaderText = "程序池";
            this.程序池.Name = "程序池";
            this.程序池.ReadOnly = true;
            // 
            // CMS版本
            // 
            this.CMS版本.DataPropertyName = "zoomlaVersion";
            this.CMS版本.HeaderText = "CMS版本";
            this.CMS版本.Name = "CMS版本";
            this.CMS版本.ReadOnly = true;
            // 
            // 操作
            // 
            this.操作.DataPropertyName = "SiteName";
            this.操作.HeaderText = "修复配置";
            this.操作.Name = "操作";
            this.操作.ReadOnly = true;
            this.操作.Text = "修复配置";
            this.操作.UseColumnTextForButtonValue = true;
            // 
            // SiteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 321);
            this.Controls.Add(this.EGV);
            this.Name = "SiteList";
            this.Text = "站点列表";
            ((System.ComponentModel.ISupportInitialize)(this.EGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn 站点名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 网站状态;
        private System.Windows.Forms.DataGridViewTextBoxColumn 程序池;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMS版本;
        private System.Windows.Forms.DataGridViewButtonColumn 操作;
        private System.Windows.Forms.DataGridView EGV;
    }
}