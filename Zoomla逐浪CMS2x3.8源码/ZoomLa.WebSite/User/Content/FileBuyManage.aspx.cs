using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model.Content;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.Common;


public partial class User_Content_FileBuyManage : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Content_FileBuy buyBll = new B_Content_FileBuy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        EGV.DataSource = buyBll.SelByUid(buser.GetLogin().UserID);
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }

}