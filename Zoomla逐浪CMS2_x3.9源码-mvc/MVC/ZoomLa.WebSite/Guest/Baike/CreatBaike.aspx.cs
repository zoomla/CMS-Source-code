using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;

namespace ZoomLaCMS.Guest.Baike
{
    public partial class CreatBaike : System.Web.UI.Page
    {
        protected B_Baike b_Baike = new B_Baike();
        protected M_Baike m_Baike = new M_Baike();
        protected B_User b_User = new B_User();//基本用户BLl
        protected M_UserInfo m_UserInfo = new M_UserInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            if (!IsPostBack)
            {
                m_UserInfo = b_User.GetLogin();
                if (!string.IsNullOrEmpty(GuestConfig.GuestOption.BKOption.CreateBKGroup))
                {//用户组编辑权限
                    string groups = "," + GuestConfig.GuestOption.BKOption.CreateBKGroup + ",";
                    if (!groups.Contains("," + m_UserInfo.GroupID.ToString() + ","))
                        function.WriteErrMsg("您没有创建词条的权限!");
                }
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            if (b_User.CheckLogin())
            {
                string baike = Server.HtmlEncode(creatbai.Text);
                if (baike != "")
                {
                    DataTable dt = b_Baike.SelBy(baike, 1);
                    if (dt.Rows.Count > 0)
                    {
                        Response.Redirect("Details.aspx?action=new&soure=user&tittle=" + baike);
                    }
                    else
                    {
                        Response.Redirect("BKEditor.aspx?tittle=" + baike);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(string), "script", "<script>alert('请输入词条！');</script>");
                }
            }
            else
            {
                Response.Redirect("/User/Login.aspx?ReturnUrl=/Guest/Baike/BKEditor.aspx");
            }
        }
        protected string getstyle()
        {
            if (b_User.CheckLogin())
            {
                return "display:inherit";
            }
            else return "display:none";
        }
        protected string getstyles()
        {
            if (b_User.CheckLogin())
            {
                return "display:none";
            }
            else return "display:inherit";
        }
    }
}