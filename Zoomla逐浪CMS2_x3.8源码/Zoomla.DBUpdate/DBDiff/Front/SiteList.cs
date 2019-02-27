using DBDiff.BLL;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DBDiff.Front
{
    public partial class SiteList : Form
    {
        ServerManager iis = new ServerManager();
        IISHelper iisHelp = new IISHelper();
        public SiteList()
        {
            InitializeComponent();
            EGV.AutoGenerateColumns = false;
            try { MyBind(); }
            catch { MessageBox.Show("读取数据失败,请以[管理员身份运行]升级工具"); EGV.Visible = false; return; }
        }
        private void MyBind() 
        {
            EGV.DataSource = iisHelp.GetWebSiteList(true);
        }
        private void EGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ( e.ColumnIndex == 4)
            {
                string siteName = EGV.Rows[e.RowIndex].Cells[0].Value.ToString();
                Site siteMod = iis.Sites[siteName];
                ApplicationPool poolMod = iis.ApplicationPools[siteMod.Applications[0].ApplicationPoolName];
                poolMod.ProcessModel.IdentityType = ProcessModelIdentityType.LocalSystem;
                poolMod.ManagedPipelineMode = ManagedPipelineMode.Integrated;
                poolMod.ManagedRuntimeVersion = "v4.0";
                iis.CommitChanges();
                MyBind();
                MessageBox.Show("站点[" + siteName + "]修复完成");
            }
        }
    }
}
