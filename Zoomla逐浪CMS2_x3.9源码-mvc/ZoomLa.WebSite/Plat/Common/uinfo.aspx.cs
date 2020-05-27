using System;
using System.Collections.Generic;
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

namespace ZoomLaCMS.Plat.Common
{
    public partial class uinfo : System.Web.UI.Page
    {
        B_User_Plat upBll = new B_User_Plat();
        B_Plat_Comp compBll = new B_Plat_Comp();
        public int UserID { get { return DataConvert.CLng(Request.QueryString["uid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserID < 1) { function.WriteErrMsg("未指定用户ID"); }
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            M_User_Plat upMod = upBll.SelReturnModel(UserID);
            M_Plat_Comp compMod = compBll.SelReturnModel(upMod.CompID);
            if (upMod.CompID != B_User_Plat.GetLogin().CompID) { function.WriteErrMsg("用户不存在"); }
            HoneyName_L.InnerHtml = B_User.GetUserName(upMod.UserName, upMod.TrueName);
            Post_L.InnerHtml = GetInfo(upMod.Post);
            CompName_l.InnerHtml = upMod.CompName;
            if (!string.IsNullOrEmpty(upMod.UserFace)) { uface_img.Src = upMod.UserFace; }
            //---------
            Mobile_L.Text = GetInfo(upMod.Mobile);
            Mail_L.Text = GetInfo("");
            WorkNum_L.Text = GetInfo("");
            Work_L.Text = GetInfo("");
            Home_L.Text = GetInfo("");
            BirthDay_L.Text = GetInfo("");
            uid_hid.Value = upMod.UserID.ToString();
        }
        private string GetInfo(string info)
        {
            return string.IsNullOrEmpty(info) ? "--" : info;
        }
    }
}