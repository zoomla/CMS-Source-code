using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Site;
namespace ZoomLaCMS.Design.Diag.Domain
{
    public partial class AddDomain : System.Web.UI.Page
    {
        B_IDC_DomainList domBll = new B_IDC_DomainList();
        string[] banDomain = "demo,www,admin,code,update".Split(',');
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                function.WriteErrMsg("默认页面关闭");
            }
        }
        public string GetDomName() { return ".demo.com"; }
        protected void checkBtn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = new B_User().GetLogin();
            string domain = domNameT.Text.Replace(" ", "").ToLower();
            if (banDomain.Contains(domain)) { function.WriteErrMsg("该域名禁止申请"); }
            M_IDC_DomainList domMod = domBll.SelModelByDomain(domain);
            if (domMod != null) { function.WriteErrMsg("该域名已存在,请更新一个名称"); }
            domMod = new M_IDC_DomainList();
            domMod.DomName = domain + GetDomName();
            domMod.UserID = mu.UserID.ToString();
            domMod.SType = 2;
            domBll.Insert(domMod);
            RouteHelper.RouteDT = domBll.Sel();
            Response.Redirect("MyDomain.aspx");
        }
    }
}