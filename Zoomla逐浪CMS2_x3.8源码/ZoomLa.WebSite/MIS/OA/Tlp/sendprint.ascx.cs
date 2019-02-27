using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.MIS;
using ZoomLa.SQLDAL;

public partial class MIS_OA_Tlp_sendprint : B_OAFormUI
{
    private int AppID { get { return DataConvert.CLng(Request.QueryString["AppID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public string GetHQInfo(int stepnum)
    {
        if (AppID < 1) { return ""; }
        B_Mis_AppProg progBll = new B_Mis_AppProg();
        return progBll.SelHQInfo(AppID, stepnum);
    }
}