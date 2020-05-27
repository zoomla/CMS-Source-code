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

namespace ZoomLaCMS.Manage.User
{
    public partial class DelJobsinfos : CustomerPageAction
    {
        private B_User buser = new B_User();
        private B_Model bmodel = new B_Model();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            if (!this.Page.IsPostBack)
            {
                int ModelID = string.IsNullOrEmpty(Request.QueryString["ModelID"]) ? 0 : DataConverter.CLng(Request.QueryString["ModelID"]);
                int ID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? 0 : DataConverter.CLng(Request.QueryString["ID"]);
                int Modeltype = string.IsNullOrEmpty(Request.QueryString["Modeltype"]) ? 1 : DataConverter.CLng(Request.QueryString["Modeltype"]);
                string ModelName = string.IsNullOrEmpty(Request.QueryString["ModelName"]) ? "zhappin" : Request.QueryString["ModelName"];



                switch (Modeltype)
                {
                    case 1:
                        buser.DelModelInfo2(bmodel.GetModelById(ModelID).TableName, ID, Modeltype);
                        Response.Redirect("Jobsinfos.aspx?modeid=" + ModelName);

                        break;
                    case 2:
                        buser.DelModelInfo(bmodel.GetModelById(ModelID).TableName, ID);
                        Response.Redirect("Jobsinfos.aspx?modeid=" + ModelName);
                        break;
                    case 3:
                        buser.DelModelInfo2(bmodel.GetModelById(ModelID).TableName, ID, Modeltype);
                        Response.Redirect("JobsRecycler.aspx?modeid=" + ModelName);
                        break;
                    case 4:
                        buser.DelModelInfo(bmodel.GetModelById(ModelID).TableName, ID);
                        Response.Redirect("JobsRecycler.aspx?modeid=" + ModelName);
                        break;
                }

            }
        }
    }
}