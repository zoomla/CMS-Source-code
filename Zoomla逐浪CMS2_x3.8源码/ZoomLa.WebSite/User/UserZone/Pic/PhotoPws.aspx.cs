using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using BDUBLL;
using BDUModel;

public partial class User_UserZone_Pic_PhotoPws : System.Web.UI.Page
{
    public Guid categid;
    PicCateg_BLL categ = new PicCateg_BLL();
    PicCateg piccateg = new PicCateg();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {        
        categid = new Guid(Request.QueryString["CategID"]);
        piccateg = categ.GetPicCateg(categid);           
            
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (PicCategPws.Text == piccateg.PicCategPws)
        {
            Response.Write("<script>window.close();window.open('PicTureList.aspx?CategID=" + Request.QueryString["CategID"] + "&PicPws=" + piccateg.PicCategPws + "','_blank');</script>");
        }
        else
        {
            Response.Write("<script>alert('密码错误！');</script>");
        }
    }
}
