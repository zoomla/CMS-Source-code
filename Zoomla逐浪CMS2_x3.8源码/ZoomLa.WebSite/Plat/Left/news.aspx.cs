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
using ZoomLa.SQLDAL;

public partial class Plat_Left_news : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MyBind();
    }
    private void MyBind()
    {
        M_User_Plat upMod = B_User_Plat.GetLogin();
        string where = "Pid=0 AND Status>0 AND ATUser LIKE '%," + upMod.UserID + ",%'";
        DataTable dt = DBCenter.SelWithField("ZL_Plat_Blog", "TOP 9 ID,MsgContent,CUName,CUser,CDate", where, "ID DESC");
        if (dt.Rows.Count < 1) { Response.Clear(); Response.Write("<div class='r_gray'>没有与你相关的信息</div>"); }
        else
        {
            RPT.DataSource = dt;
            RPT.DataBind();
        }
    }
    public string GetContent()
    {
        string msg = StringHelper.StripHtml(Eval("MsgContent", ""));
        return StringHelper.SubStr(msg, 28);
    }
}