namespace ZoomLa.WebSite
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
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.BLL;

    public partial class correct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            string title = base.Request.QueryString["t"];
            string url = base.Request.QueryString["u"];
            this.lblTitle.Text = title;
            this.lblUrl.Text = url;
        }
    }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                string vCode = this.Session["ValidateCode"].ToString();
                if (string.IsNullOrEmpty(vCode))
                {
                    function.WriteErrMsg("<li>验证码无效，请刷新验证码重新登录</li>");
                }
                if (string.Compare(this.TxtCode.Text.Trim(), vCode, true) != 0)
                {
                    function.WriteErrMsg("<li>验证码不正确</li>");
                }   
                M_Correct info = new M_Correct();
                info.CorrectID = 0;
                info.CorrectTitle = this.lblTitle.Text;
                info.CorrectUrl = this.lblUrl.Text;
                info.CorrectType = DataConverter.CLng(this.RBLType.SelectedValue);
                info.CorrectDetail = this.TxtDetail.Text;
                info.CorrectPer = this.TxtPer.Text;
                info.CorrectEmail = this.TxtEmail.Text;
                B_Correct bll = new B_Correct();
                bll.Add(info);
                function.WriteSuccessMsg("纠错报告成功记录，谢谢您的参与！");
            }
        }
    }
}