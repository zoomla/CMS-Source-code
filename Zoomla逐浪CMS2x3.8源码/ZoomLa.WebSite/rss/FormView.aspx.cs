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

public partial class test_FormView : System.Web.UI.Page
{
    B_Pub pubBll = new B_Pub();
    M_Pub pubMod = new M_Pub();
    public int Mid { get { return Convert.ToInt32(Request.QueryString["Pid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pubMod = pubBll.SelReturnModel(Mid);
            formtitle.InnerText = pubMod.PubName;
            formintro.InnerText = pubMod.PubTemplate;
            PubContent.Value = pubMod.Pubinfo;
            Response.Redirect( "/PubAction.aspx?Pubid=" + pubMod.Pubid);
            function.Script(this, "InitForm()");
        }
    }
    protected void SaveDataBtn_Click(object sender, EventArgs e)
    {
        pubMod = pubBll.SelReturnModel(Mid);

    }
}