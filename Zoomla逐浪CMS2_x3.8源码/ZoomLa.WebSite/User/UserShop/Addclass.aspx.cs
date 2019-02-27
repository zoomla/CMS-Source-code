using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

public partial class User_UserShop_Addclass : System.Web.UI.Page
{
    //protected B_UserShopClass cll = new B_UserShopClass();
    protected B_User ull = new B_User();
    protected B_ModelField mfbll = new B_ModelField();
    public string str = "添加分类";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Request.QueryString["menu"] != null)
            {
                if (Request.QueryString["menu"] == "edit")
                {
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    //M_UserShopClass shopinfo = cll.GetSelect(id);
                    //this.Classname.Text = shopinfo.Classname;
                    //this.Classinfo.Text = shopinfo.Classinfo;
                    //this.Orderid.Text = shopinfo.Orderid.ToString();
                    //hiden.Value = id.ToString();
                    this.Button1.Text = "修改 ";
                    str = "修改分类";
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
    //    M_UserShopClass cinfo = new M_UserShopClass();
    //    if (Request.Form["hiden"] != null)
    //    {
    //        cinfo = cll.GetSelect(DataConverter.CLng(Request.Form["hiden"]));
    //    }
    //    cinfo.Classname = this.Classname.Text;
    //    cinfo.Classinfo = this.Classinfo.Text;
    //    cinfo.Orderid = DataConverter.CLng(this.Orderid.Text);
    //    cinfo.Shopid = 0;
    //    cinfo.Username = ull.GetLogin().UserName;
    //    cinfo.Classid = 0;
    //    if (Request.Form["hiden"] == null)
    //    {
    //        cll.GetInsert(cinfo);
    //        Response.Write("<script>alert('添加成功！');location.href='Classmanage.aspx';</script>");
    //    }
    //    else
    //    {
    //        cinfo.id = DataConverter.CLng(Request.Form["hiden"]);
    //        cll.GetUpdate(cinfo);
    //        Response.Write("<script>alert('修改成功！');location.href='Classmanage.aspx';</script>");
    //    }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Classmanage.aspx");
    }
}