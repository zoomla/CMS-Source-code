using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Plat
{
    public partial class Main : System.Web.UI.MasterPage
    {
        B_Blog_Msg msgBll = new B_Blog_Msg();
        B_Guest_Bar barBll = new B_Guest_Bar();
        B_User buser = new B_User();
        B_User_Plat upBll = new B_User_Plat();
        M_User_Plat upMod = null;
        protected void Page_Init(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            upMod = B_User_Plat.GetLogin();
            if (upMod == null) { function.WriteErrMsg("你并非能力中心用户,请联系网络管理员,为你发送注册邀请!!"); }
            else if (upMod.CompID < 1) { function.WriteErrMsg("你尚未归属公司,请联系网络管理员,将你加入公司!!"); }
            else if (upMod.Status == -1) { function.WriteErrMsg("你的帐户尚未审核,无法登录能力中心,请联系网络管理员!!!"); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_Plat_Comp compMod = new B_Plat_Comp().SelReturnModel(upMod.CompID);
                Main_UName_T.Text = upMod.TrueName.Length > 6 ? upMod.TrueName.Substring(0, 5) : upMod.TrueName;
                Main_UserFace_Img.Src = upMod.UserFace;
                M_CompName_L.Text = compMod.CompShort;
                logo_img.Src = compMod.CompLogo;
                GetNotify(upMod);
                //At_Lit.Text = upMod.AtCount > 0 ? "<span class='haveat'>" + upMod.AtCount + "</span>" : "<span class='noat'>0</span>";
                //int emailCount= new B_Message().GetUnReadMail(upMod.UserID).Rows.Count;
                //Email_Lit.Text = emailCount > 0 ? "<span class='haveat'>" + emailCount + "</span>" : "<span class='noat'>0</span>";//后期也加入计数中
                //Main_Skey_T.Text = Request.QueryString["Skey"];
                //Comp_Img.Src = string.IsNullOrEmpty(compMod.CompLogo) ? "/Plat/images/vlogo.png" : compMod.CompLogo;
                //Comp_Href.HRef = UrlDeal(compMod.CompHref);
                //Comp_Href.InnerText = compMod.CompHref;
                //Main_UserFace_Img.Src =upMod.UserName;
                //Main_UserFace_Img.Attributes.Add("data-uid", upMod.UserID.ToString());
                //Main_UserWord_Div.InnerText = upMod.UserName;
            }
        }
        //protected void Search_Btn_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("/Plat/Blog/?Skey=" + Main_Skey_T.Text.Trim());
        //}
        private void GetNotify(M_User_Plat upMod)
        {
            //用户登录,检测有消息则提示
            B_Common_Notify comBll = new B_Common_Notify();
            DataTable dt = comBll.Blog_SelMy(upMod.UserID);
            foreach (DataRow dr in dt.Rows)//移除rows中的值,并添加已读列表
            {
                B_User_Notify.Add(dr["Title"].ToString(), dr["Content"].ToString(), upMod.UserID.ToString());
                string rece = StrHelper.RemoveToIDS(dr["ReceUsers"].ToString(), upMod.UserID.ToString());
                string readed = StrHelper.AddToIDS(dr["ReadedUsers"].ToString(), upMod.UserID.ToString());
                List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("rece", rece), new SqlParameter("readed", readed) };
                DBCenter.UpdateSQL("ZL_Common_Notify", "ReadedUsers=@readed,ReceUsers=@rece", "ID=" + dr["ID"], sp);
            }
        }
    }
}