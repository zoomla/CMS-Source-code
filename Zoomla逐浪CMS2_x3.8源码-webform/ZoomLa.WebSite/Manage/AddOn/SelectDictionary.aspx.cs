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
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using System.Text;

    public partial class SelectDictionary : System.Web.UI.Page
    {
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!buser.CheckLogin() && !badmin.CheckLogin()) { function.WriteErrMsg("该页面必须登录后才可访问"); }
            B_Admin.CheckIsLogged();
            if (!IsPostBack)
            {
                HdnContrID.Value = Request.QueryString["ControlId"];
                myBind();
                Response.Expires = -1;
            }
        }
        private void myBind()
        {
            this.EGV.DataSource = B_DataDicCategory.SearchDicCateList(this.HdnNameKey.Value);
            this.EGV.DataKeyNames = new string[] { "DicCateID" };
            this.EGV.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.EGV.PageIndex = e.NewPageIndex;
            myBind();
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
         
                if (e.CommandName == "Select")
                {
                    string Id = e.CommandArgument.ToString();
                    string returnValue=B_DataDictionary.GetDicString(DataConverter.CLng(Id));
                    returnValue = returnValue.Replace(",", "\r\n");
                    this.TxtSelectDic.Text = returnValue;
                }
         
        }
           
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCategoryName.Text.Trim()))
            {
                this.HdnNameKey.Value = this.txtCategoryName.Text.Trim();
                myBind();
            }
        }
    }
}