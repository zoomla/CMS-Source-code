using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

public partial class Design_SPage_PreView : System.Web.UI.Page
{
    B_SPage_Page pageBll = new B_SPage_Page();
    public M_SPage_Page pageMod = null;
    public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        pageMod = pageBll.SelReturnModel(Mid);
        if (pageMod == null) { function.WriteErrMsg("页面不存在"); }
    }
}