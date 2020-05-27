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

/*
 * 必须通过邀请链接才能进入该页
 */
namespace ZoomLaCMS.Plat
{
    public partial class Fill : System.Web.UI.Page
    {
        M_User_Plat upMod = new M_User_Plat();
        B_User_Plat upBll = new B_User_Plat();
        B_User buser = new B_User();
        M_Plat_Comp compMod = new M_Plat_Comp();
        B_Plat_Comp compBll = new B_Plat_Comp();

        protected void Page_Load(object sender, EventArgs e)
        {
            function.WriteErrMsg("页面功能关闭");
            if (!IsPostBack)
            {
                B_User.CheckIsLogged(Request.RawUrl);
                string info = Request.QueryString["Info"];
                if (string.IsNullOrEmpty(info) || DataConverter.CLng(EncryptHelper.AESDecrypt(info)) < 1)
                    function.WriteErrMsg("该页面必须通过邀请才能访问,请联系管理员!!!");
                switch (B_User_Plat.IsPlatUser())
                {
                    //case 0://非Plat用户
                    //    Response.Redirect("/Plat/Fill.aspx");
                    //    break;
                    case 1://未审核
                        function.WriteErrMsg("你的帐户尚未审核,无法登录能力中心,请联系网络管理员!!!");
                        break;
                    case 99://正常登录
                        Session["Main_IsPlatUser"] = true;
                        break;
                }
            }
        }
        protected void Sure_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            ////------------UserBase
            //bool isExist = true;
            //M_Uinfo muinfo = buser.GetUserBaseByuserid(mu.UserID);
            //if (muinfo == null)
            //{
            //    isExist = false;
            //    muinfo = new M_Uinfo();
            //    muinfo.UserId = mu.UserID;
            //}
            //muinfo.TrueName = TrueName_T.Text;
            //muinfo.Mobile = Mobile_T.Text;
            //muinfo.CompanyName = CompName_T.Text;
            //if (isExist)
            //    buser.UpdateBase(muinfo);
            //else
            //    buser.AddBase(muinfo);
            //------------M_Plat_User
            int compid = Convert.ToInt32(EncryptHelper.AESDecrypt(Request.QueryString["Info"]));
            upMod.UserID = mu.UserID;
            upMod.CompID = compid;
            upMod.Mobile = Mobile_T.Text;
            upMod.TrueName = TrueName_T.Text;
            upMod.Post = Post_T.Text;
            upMod.Status = 0;
            upMod.CreateTime = DateTime.Now;
            upBll.Insert(upMod);
            Response.Redirect("/Plat/Blog/");
        }
    }
}