using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

public partial class User_Info_UserDay : System.Web.UI.Page
{
    protected B_User ull =new B_User();
    protected B_UserDay dll = new B_UserDay();
    protected M_UserInfo userinfo = new M_UserInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        userinfo = ull.GetLogin();
        if (!IsPostBack)
        {
            if (Request.QueryString["menu"] != null)
            {
                if (Request.QueryString["menu"] == "delete")
                {
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    dll.DeleteByGroupID(id);
                }
                else if (Request.QueryString["menu"] == "edit")
                {
                    this.BtnSubmit.Text = "修改";
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    M_UserDay dinfo = dll.GetSelect(id);
                    int v_id = dinfo.id;
                    this.D_title.Text = dinfo.D_name;
                    this.D_Content.Text = dinfo.D_Content;
                    this.D_date.Text = dinfo.D_date.ToShortDateString();
                    hidenid.Value = v_id.ToString();
                }
            }
        }

        DataTable dtable = dll.Select_All(this.userinfo.UserID);
        this.Repeater1.DataSource = dtable;
        this.Repeater1.DataBind();


        for (int i = 0; i < Repeater1.Items.Count; i++)
        {
            Label lbl = (Label)Repeater1.Items[i].FindControl("idla");
            lbl.Text = (i + 1).ToString();
        }

    }



    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        M_UserDay dinfo = new M_UserDay();
        if (Request.Form["hidenid"] != null)
        {
            int id = DataConverter.CLng(Request.Form["hidenid"]);
            dinfo = dll.GetSelect(id);
        }
        dinfo.D_UserID = this.userinfo.UserID;
        dinfo.D_name = this.D_title.Text;
        dinfo.D_date = DataConverter.CDate(this.D_date.Text);
        dinfo.D_Content = this.D_Content.Text;
        if (Request.Form["hidenid"] != null)
        {
            dll.GetUpdate(dinfo);
            Response.Write("<script>alert('修改成功!');location.href='UserDay.aspx';</script>");
       
        }
        else
        {
            dll.GetInsert(dinfo);
            Response.Write("<script>alert('添加成功!');location.href='UserDay.aspx';</script>");
        }
    }
    protected void BtnCancle_Click(object sender, EventArgs e)
    {
        Response.Write("<script>location.href='UserDay.aspx';</script>");
    }
}