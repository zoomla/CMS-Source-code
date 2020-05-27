using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Components;

public partial class User_UserZone_School_SchoolFellow : System.Web.UI.Page
{
    B_User ubll = new B_User();
    B_ClassRoom bcr = new B_ClassRoom();
    B_School bs = new B_School();
    B_Student st = new B_Student();
    protected void Page_Load(object sender, EventArgs e)
    {
        ubll.CheckIsLogin();
        if (!IsPostBack)
        { 

        }

    }
    protected void MyBind(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text.Trim())) return;
            DataTable dt = st.SelByValue(txtName.Text.Trim());
            this.EGV.DataSource = dt;
            this.EGV.DataBind();
    }
 
    protected string GetUserName(string id)
    {
        return ubll.GetSelect(Convert.ToInt32(id)).TrueName;
    }

    protected string GetSchool(string id)
    {
        return bs.GetSelect(int.Parse(id)).SchoolName;
    }

    protected string GetRoom(string id)
    {
        return bcr.GetSelect(int.Parse(id)).RoomName;
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind(sender,e);
    }
}
