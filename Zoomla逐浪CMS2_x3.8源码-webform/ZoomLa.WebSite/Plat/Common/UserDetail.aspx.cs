using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Plat_Blog_InfoDetail : System.Web.UI.Page
{
    B_User_Plat upBll = new B_User_Plat();
    M_User_Plat upMod = new M_User_Plat();
    public int UserID
    {
        get { return DataConverter.CLng(Request.QueryString["ID"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (UserID < 1) function.WriteErrMsg("用户ID传入错误");
            else { MyBind(UserID); }
        }
    }
    public void MyBind(int id)
    {
        upMod = upBll.SelReturnModel(id);
        pre_img.ImageUrl = upMod.UserFace;
        username_T.Value = upMod.UserName;
        trueName_L.Text = upMod.TrueName;
        position_L.Text = upMod.Post;
        mobile_L.Text = upMod.Mobile;
        group_L.Text = upMod.GroupName;
    }
}