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
using ZoomLa.Common;

public partial class Manage_I_Plat_PlatInfoDeail : System.Web.UI.Page
{
    B_Blog_Msg msgBll = new B_Blog_Msg();
    public new int ID
    {
        get { return DataConverter.CLng(Request.QueryString["ID"]); }
    }
    public int Pid
    {
        get {return DataConverter.CLng(Request.QueryString["pid"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PlatInfoManage.aspx'>信息管理</a></li><li class='active'>信息详情</li>");
            MyBind();
        }
    }
    void MyBind()
    {
        ID_L.Text = ID.ToString();
        M_Blog_Msg msgMod = msgBll.SelReturnModel(ID);
        CUser_L.Text = "<a href='../User/UserInfo.aspx?ID=" + msgMod.CUser + "'>" + msgMod.CUName + "</a>";
        MsgContent_L.Text = msgMod.MsgContent;
        MsgContent_T.Text = msgMod.MsgContent;
        CDate_L.Text = msgMod.CDate.ToString();
        CDate_T.Text = msgMod.CDate.ToString();
        EGV.DataSource = msgBll.Sel(ID);
        EGV.DataBind();
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Blog_Msg msgMod = msgBll.SelReturnModel(ID);
        msgMod.MsgContent = MsgContent_T.Text;
        msgMod.CDate = DateTime.Parse(CDate_T.Text);
        msgBll.UpdateByID(msgMod);
        MyBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Dels_Click(object sender, EventArgs e)
    {
        string ids = string.IsNullOrEmpty(Request.Form["idchk"]) ? "" : Request.Form["idchk"];
        msgBll.DelByIds(ids);
        MyBind();
    }

    public string getText()
    {
        string str = StringHelper.StripHtml(Eval("MsgContent").ToString(), 500).Replace(" ", "");
        return str.Length > 30 ? str.Substring(0, 29) + "..." : str;
    }
}