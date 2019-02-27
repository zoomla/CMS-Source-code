using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Model.Page;
using ZoomLa.BLL.Page;

public partial class User_Pages_ClassManage : System.Web.UI.Page
{
    protected B_User ull = new B_User();
    protected B_PageReg pll = new B_PageReg();
    protected B_Templata tll = new B_Templata();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "delete")
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            if (tll.Getbyid(id).UserID > 0)
            {
                tll.Delete(id);
            }
        }
        M_UserInfo uinfo = ull.GetLogin();
        DataTable Uptab = pll.Sel("UserID=" + uinfo.UserID, "");
        if (Uptab.Rows.Count > 0)
        {
            if (Uptab.Rows[0]["Status"].ToString() != "99")
            {
                function.WriteErrMsg("您的黄页还未通过审核！");
            }
        }
        else
        {
            function.WriteErrMsg("您还未注册黄页！");
        }
        DataTable templist = tll.ReadUserall(0, DataConverter.CLng(uinfo.UserID), DataConverter.CLng(Uptab.Rows[0]["NodeStyle"]));
        this.Repeater1.DataSource = templist;
        this.Repeater1.DataBind();
    }

    protected void BtnCancle_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            tll.DelByIDS(Request.Form["idchk"]);
        }
        MyBind();
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            tll.ChangeStatus(Request.Form["idchk"],0);
        }
        MyBind();
    }
}
