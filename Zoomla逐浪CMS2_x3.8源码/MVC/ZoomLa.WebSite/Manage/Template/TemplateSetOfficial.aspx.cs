using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Template
{
    public partial class TemplateSetOfficial : CustomerPageAction
    {
        protected B_Admin badmin = new B_Admin();
        public string serverdomain = SiteConfig.SiteOption.ProjectServer;// 
        DataSet tableset = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }

        private void MyBind()
        {
            //tableset.ReadXml(serverdomain + "/api/gettemplate.aspx?menu=getprojectinfo");
            RPT.DataSource = TempDT.ToTable();
            RPT.DataBind();
        }
        public DataView TempDT
        {
            get
            {
                if (Cache["TempDT"] == null)
                    Cache["TempDT"] = GetServerTemp();
                return (Cache["TempDT"] as DataTable).DefaultView;
            }
        }
        //调用服务端的接口，获取信息
        private DataTable GetServerTemp()
        {
            DataSet tableset = new DataSet();
            tableset.ReadXml(serverdomain + "/api/gettemplate.aspx?menu=getprojectinfo");
            return tableset.Tables[0];
        }
        public string GetDownUrl()
        {
            return "DownTemplate.aspx?proname=" + HttpUtility.UrlEncode(Eval("Project", "")) + "&dir=" + Eval("TempDirName", "");
        }
    }
}