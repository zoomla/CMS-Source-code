namespace ZoomLa.WebSite.Manage.AddOn
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using ZoomLa.BLL;

    public partial class UploadDir : System.Web.UI.Page
    {
        string AppPath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            if (!badmin.ChkPermissions("FileManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            string uploadDir = SiteConfig.SiteOption.UploadDir;
            this.AppPath = base.Request.PhysicalApplicationPath;
            TreeNode tmpNd;
            tmpNd = new TreeNode();
            tmpNd.Value = uploadDir;
            tmpNd.Text = uploadDir;
            tmpNd.NavigateUrl = "UploadFile.aspx?Dir=" + base.Server.UrlEncode(uploadDir);
            tmpNd.Target = "main_right";
            tmpNd.ImageUrl = "";
            tmpNd.ToolTip = "根目录";
            tvNav.Nodes.Add(tmpNd);
            InitTreeNode(tmpNd.ChildNodes, uploadDir);
            tvNav.ExpandAll();
        }
        private void InitTreeNode(TreeNodeCollection Nds, string UpDir)
        {
            string AbsPath = this.AppPath + UpDir + "/";
            AbsPath = AbsPath.Replace("/", @"\");
            AbsPath = AbsPath.Replace(@"\\", @"\");
            TreeNode tmpNd;
            string search = "UploadFile.aspx?Dir=";
            DataTable dt = FileSystemObject.GetDirectoryInfos(AbsPath, FsoMethod.Folder);
            foreach (DataRow dr in dt.Rows)
            {
                tmpNd = new TreeNode();
                string objPath=UpDir+"/"+dr["Name"].ToString();
                tmpNd.Value = dr["Name"].ToString();
                tmpNd.Text = dr["Name"].ToString();
                tmpNd.NavigateUrl = search + base.Server.UrlEncode(objPath);
                tmpNd.Target = "main_right";
                Nds.Add(tmpNd);
                string NextDir=UpDir+"/"+tmpNd.Value;
                string aPath = this.AppPath + NextDir + "/";
                aPath = aPath.Replace("/", @"\");
                aPath = aPath.Replace(@"\\", @"\");
                if (FileSystemObject.GetDirectoryInfos(aPath, FsoMethod.Folder).Rows.Count > 0)
                {
                    InitTreeNode(tmpNd.ChildNodes, NextDir);
                }
            }
        }
    }
}