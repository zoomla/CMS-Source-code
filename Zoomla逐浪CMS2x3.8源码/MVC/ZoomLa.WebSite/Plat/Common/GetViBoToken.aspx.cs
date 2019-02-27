using Newtonsoft.Json.Linq;
using System;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.Third;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.Model.Third;
using ZoomLa.PdoApi.SinaWeiBo;

namespace ZoomLaCMS.Plat.Common
{
    public partial class GetViBoToken : System.Web.UI.Page
    {
        B_User_Token tokenbll = new B_User_Token();
        B_User buser = new B_User();
        B_Third_Info thirdBll = new B_Third_Info();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int uid = Convert.ToInt32(Request.QueryString["state"]);
                M_UserInfo mu = buser.GetLogin();//注意域名不要子域名跳过来
                M_User_Token tokenMod = tokenbll.SelModelByUid(mu.UserID);
                if (tokenMod == null) { tokenMod = new M_User_Token(); }
                switch (Request.QueryString["s"])
                {
                    case "qq"://使用JS SDK
                        break;
                    default://Sina
                        string code = Request.QueryString["code"];
                        if (!string.IsNullOrEmpty(code))
                        {
                            M_Third_Info appmod = thirdBll.SelModelByName("Sina");
                            SinaHelper sinaBll = new SinaHelper(null);
                            tokenMod.uid = mu.UserID;
                            tokenMod.SinaToken = sinaBll.GetTokenByCode(code);
                            tokenbll.InsertORUpdate(tokenMod);
                        }
                        Response.Redirect("/Plat/UpCenter.aspx?Set=1");
                        break;
                }
            }
        }
        protected void QQBind_Btn_Click(object sender, EventArgs e)
        {

            M_UserInfo mu = buser.GetLogin();
            M_User_Token tokenMod = tokenbll.SelModelByUid(mu.UserID);
            if (tokenMod == null) { tokenMod = new M_User_Token(); }
            tokenMod.uid = mu.UserID;
            tokenMod.QQOpenID = OpenID_Hid.Value;
            tokenMod.QQToken = Token_Hid.Value;
            JObject obj = new QQHelper(tokenMod.QQToken, tokenMod.QQOpenID).GetUserInfo();
            if (obj == null || obj["data"] == null) function.WriteErrMsg("绑定失败" + obj);
            if (obj["data"]["ret"] != null && obj["data"]["ret"].ToString().Equals("6")) { function.WriteErrMsg("用户未开通腾迅微博"); }
            tokenMod.QQUName = obj["data"]["nick"].ToString();
            tokenbll.InsertORUpdate(tokenMod);
            Response.Redirect("/Plat/UpCenter.aspx?Set=1");
        }
    }
}