using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
public partial class manage_User_UserDayManage : CustomerPageAction
{
    protected B_UserDay dll = new B_UserDay();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            dll.DeleteByGroupID(id);

            if (ViewState["txtPage"] == null)
            {
                if (Request.QueryString["pagenum"] != null)
                {
                    string pagenum = Request.QueryString["pagenum"];
                    
                }
                else
                {
                    
                }
            }
            else
            {
                
            }

            RepNodeBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>"+Resources.L.工作台 + "</a></li><li><a href='UserManage.aspx'>" + Resources.L.用户管理 + "</a></li><li><a href='UserManage.aspx'>" + Resources.L.会员管理 + "</a></li><li>" + Resources.L.用户节日管理 + "</li>");
    }

    protected string Getusername(string userid)
    {
        B_User ull = new B_User();
        return  ull.GetUserByUserID(DataConverter.CLng(userid)).UserName;
    }

    public void RepNodeBind()
    {
        this.txtdate.Text = Request.QueryString["txtdate"];
        if (Request.QueryString["txtdate"] != null && Request.QueryString["txtdate"]!="")
        {
            DateTime txtdate = DataConverter.CDate(Request.QueryString["txtdate"]);
            DataTable dt = new DataTable();
            dt = dll.Select_All(txtdate);
            Page_list(dt);
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            dt = dll.Select_All();
            Page_list(dt);
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        



    }

    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        RepNodeBind();
    }

    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        if (Cll != null)
        {
            this.RPT.DataSource = Cll;
            this.RPT.DataBind();
        }
    }
    #endregion
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Response.Redirect("UserDayManage.aspx?Currentpage=" + DropDownList1.Text.ToString() + "&txtdate=" + Request.QueryString["txtdate"] + "&pagenum=" + ViewState["txtPage"].ToString() + "");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserDayManage.aspx?txtdate=" +Server.UrlEncode(this.txtdate.Text));
    }
}