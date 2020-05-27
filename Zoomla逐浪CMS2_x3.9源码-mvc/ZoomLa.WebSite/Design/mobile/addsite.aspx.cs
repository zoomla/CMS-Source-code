using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Design.mobile
{
    public partial class addsite : System.Web.UI.Page
    {
        B_Design_MBSite mbBll = new B_Design_MBSite();
        B_User buser = new B_User();
        private int TlpID { get { return DataConvert.CLng(Request["TlpID"]); } }
        private int Mid { get { return DataConvert.CLng(Request["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            Tlp_DP.DataSource = mbBll.GetTlpDT();
            Tlp_DP.DataBind();
            if (Mid > 0)
            {
                M_Design_MBSite mbMod = mbBll.SelReturnModel(Mid);
                if (mbMod.UserID != mu.UserID) { function.WriteErrMsg("你无权管理该微站"); }
                SiteName_T.Text = mbMod.SiteName;
                SiteImg_Hid.Value = mbMod.SiteImg;
                if (!string.IsNullOrEmpty(mbMod.SiteImg)) { siteimg.Src = mbMod.SiteImg; }
                Head_L.Text = Title_L.Text = "修改微站";
            }
            else//新建
            {
                Tlp_DP.SelectedValue = TlpID.ToString();
                int mbcount = mbBll.GetSiteCount(mu.UserID);
                if (mbcount >= SiteConfig.SiteOption.DN_MBSiteCount)
                {
                    function.Script(this, "NoMoreSite('你已经拥有了" + mbcount + "个微站,不能再继续创建');");
                    Save_Btn.Visible = false;
                }
                Head_L.Text = Title_L.Text = "新建微站";
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            M_Design_MBSite mbMod = new M_Design_MBSite();
            if (Mid > 0) { mbMod = mbBll.SelReturnModel(Mid); }
            mbMod.SiteName = SiteName_T.Text;
            mbMod.SiteImg = SiteImg_Hid.Value;
            mbMod.TlpID = DataConvert.CLng(Tlp_DP.SelectedValue);
            if (Mid > 0)
            {
                mbBll.UpdateByID(mbMod);
            }
            else
            {
                string err = "";
                mbMod.UserID = mu.UserID;
                mbMod = mbBll.CreateSite(mu, mbMod, out err);
                if (!string.IsNullOrEmpty(err)) { function.WriteErrMsg("创建微站失败,原因:" + err); }
            }
            Response.Redirect("default.aspx?id=" + mbMod.ID);
        }
    }
}