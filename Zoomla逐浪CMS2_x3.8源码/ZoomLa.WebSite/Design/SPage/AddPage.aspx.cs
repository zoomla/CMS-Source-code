using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

public partial class Design_SPage_AddPage : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_SPage_Page pageBll = new B_SPage_Page();
    private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind() 
    {
        if (Mid > 0)
        {
            M_SPage_Page pageMod = pageBll.SelReturnModel(Mid);
            PageName_T.Text = pageMod.PageName;
            PageDesc_T.Text = pageMod.PageDesc;
            PageRes_T.Text = pageMod.PageRes;
        } 
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_SPage_Page pageMod = new M_SPage_Page();
        if (Mid > 0)
        {
            pageMod = pageBll.SelReturnModel(Mid);
        }
        pageMod.PageName = PageName_T.Text;
        pageMod.PageDesc = PageDesc_T.Text;
        pageMod.PageRes = PageRes_T.Text;
        if (Mid > 0)
        {
            pageBll.UpdateByID(pageMod);
            function.Script(this,"parent.closeDiag();");
        }
        else
        {
            pageMod.UserID = mu.UserID;
            pageMod.ID = pageBll.Insert(pageMod);
            function.Script(this,"parent.location='/design/spage/default.aspx?id="+pageMod.ID+"';");
        }
    }
}