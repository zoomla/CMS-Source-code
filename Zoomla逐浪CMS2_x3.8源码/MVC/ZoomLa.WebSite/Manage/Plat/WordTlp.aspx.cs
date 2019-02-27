using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;
namespace ZoomLaCMS.Manage.Plat
{
    public partial class WordTlp : System.Web.UI.Page
    {
        B_Plat_Comp compBll = new B_Plat_Comp();
        B_User_Plat upBll = new B_User_Plat();
        B_User buser = new B_User();
        private int Mid { get { return DataConvert.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_Plat_Comp compMod = compBll.SelReturnModel(Mid);
                M_UserInfo mu = buser.SelReturnModel(compMod.CreateUser);
                CompName_L.Text = compMod.CompName;
                CDate_L.Text = compMod.CreateTime.ToString("yyyy-MM-dd");
                CUserName_L.Text = B_User.GetUserName(mu.HoneyName, mu.UserName);
                CUserEmail_L.Text = mu.Email;
                DataTable dt = upBll.SelByCompany(compMod.ID);
                dt.DefaultView.RowFilter = "UserID<>'" + mu.UserID + "'";
                RPT.DataSource = dt;
                RPT.DataBind();
            }
        }
        protected void DownDoc_Btn_Click(object sender, EventArgs e)
        {
            M_Plat_Comp compMod = compBll.SelReturnModel(Mid);
            string html = Request.Form["html_hid"];
            html = "<html><head></head><body>" + html + "</body></html>";
            string vpath = "/UploadFiles/Plat/CompReport/" + compMod.CompName + ".doc";
            vpath = OfficeHelper.W_HtmlToWord(html, vpath);
            SafeSC.DownFile(vpath);
        }
    }
}