using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Plat_Left_star : System.Web.UI.Page
{
    //取最近的十条
    protected void Page_Load(object sender, EventArgs e)
    {
        MyBind();
    }
    private void MyBind()
    {
        M_User_Plat upMod = B_User_Plat.GetLogin();
        string where = " ColledIDS LIKE '%," + upMod.UserID + ",%' ";
        DataTable dt = DBCenter.SelWithField("ZL_Plat_Blog", "TOP 12 ID,MsgContent,CUName,CUser,CDate", where, "ID DESC");
        if (dt.Rows.Count > 0)
        {
            RPT.DataSource = dt;
            RPT.DataBind();
        }
        else { empty_div.Visible = true; }
    }
    public string GetContent()
    {
        string msg = StringHelper.StripHtml(Eval("MsgContent", ""));
        return StringHelper.SubStr(msg, 28);
    }
}