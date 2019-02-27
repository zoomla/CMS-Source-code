namespace ZoomLa.WebSite.Manage.Template
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
    using ZoomLa.BLL;
    using ZoomLa.Common;

    public partial class LabelExport : System.Web.UI.Page
    {
        private B_Label bll = new B_Label();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("LabelExport"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataSet ds = this.bll.GetAllLabel();
            string filename = base.Request.PhysicalApplicationPath + @"\" + "App_Data" + @"\" + "LabelExport.xml";
            if (!FileSystemObject.IsExist(filename, FsoMethod.File))
                FileSystemObject.Create(filename, FsoMethod.File);
            this.Label1.Text = "开始导出";
            ds.WriteXml(filename);
            this.Label1.Text = "成功!";
        }
}
}