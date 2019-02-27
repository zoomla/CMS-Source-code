using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_UserZone_Kiss_kiss : System.Web.UI.Page
{
    private B_Sns_Kiss kll = new B_Sns_Kiss();
    private B_User ull = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        ull.CheckIsLogin();

        M_UserInfo info = ull.GetLogin(); 

        if (Request.QueryString["menu"] != null)
        {
            string menu = Request.QueryString["menu"].ToString();
            if (menu == "delete")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                kll.DeleteByGroupID(id);
            }
        }

        string UserFace = ull.GetUserBaseByuserid(info.UserID).UserFace;

        if (UserFace == "" || UserFace=="~/Images/userface/noface.png"|| UserFace == "~/Images/head.jpg")
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;
            this.RPT.Visible = false;
        }
        else
        {
            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
            this.RPT.Visible = true;
        }

        this.ShowUserFace.ImageUrl =UserFace;
        DataTable ktable = kll.Select_All(info.UserID);
        Page_list(ktable, "SendTime desc");


    }

    protected string getuserpic(string SendID)
    {
        int userid = DataConverter.CLng(SendID);
        return ull.GetUserBaseByuserid(userid).UserFace;

    }

    protected string getusername(string SendID)
    {

        int userid = DataConverter.CLng(SendID);
        return ull.GetUserByUserID(userid).UserName;
    }
    

    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll, string order)
    {
        RPT.DataSource = Cll;
        RPT.DataBind();
    }
    #endregion
    protected void Button1_Click(object sender, EventArgs e)
    {
        M_UserInfo userinfo = ull.GetLogin();
        M_Uinfo infos = ull.GetUserBaseByuserid(userinfo.UserID);
        infos.UserFace = Request.Form["UserFaceddd"].ToString();
        ull.UpdateBase(infos);
        Response.Write("<script>alert('更新成功!');location.href='kiss.aspx';</script>");
    }
}
