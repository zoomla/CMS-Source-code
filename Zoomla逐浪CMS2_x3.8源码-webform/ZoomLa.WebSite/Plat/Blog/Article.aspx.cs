using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

public partial class Plat_Blog_Article : System.Web.UI.Page
{
    B_Blog_Msg msgBll = new B_Blog_Msg();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_User_Plat upMod = B_User_Plat.GetLogin();
            int id = DataConvert.CLng(Request.QueryString["id"]);
            M_Blog_Msg msgMod = msgBll.SelReturnModel(id);
            Response.Write(msgMod.MsgContent); Response.Flush(); Response.End();
        }
    }
}