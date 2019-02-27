using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Design;

public partial class Design_Diag_mb_music : System.Web.UI.Page
{
    B_Design_RES resBll = new B_Design_RES();
    private int CPage { get { return PageCommon.GetCPage(); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind(string skey ="")
    {
        int psize = 20, pcount = 0;
        DataTable dt = resBll.Search(skey, "", "music");
        RPT.DataSource = PageCommon.GetPageDT(psize, CPage, dt, out pcount);
        RPT.DataBind();
        Page_Lit.Text = PageCommon.CreatePageHtml(pcount, CPage);
    }
    protected void Search_B_Click(object sender, EventArgs e)
    {
        string skey = Skey_T.Text.Trim(' ');
        MyBind(skey);
    }
}