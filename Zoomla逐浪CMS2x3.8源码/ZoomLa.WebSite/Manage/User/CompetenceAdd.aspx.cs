using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class Manage_User_CompetenceAdd : System.Web.UI.Page
{
    B_Admin badmin = new B_Admin();
    B_UserPurview bup = new B_UserPurview();
    M_UserPurview mup = new M_UserPurview();
    B_Permission perbll = new B_Permission();
    B_Structure structBll = new B_Structure();
    M_Structure structMod = new M_Structure();
    public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string codes = Request.Form["Codes"];
            string nodeid = Request.Form["NodeID"];
            string str="";
            int id = DataConverter.CLng(Request.Form["ID"]);
            if (codes!="")
                str = Add(codes, id, nodeid);
            else
                str = Del(id);
            Response.Write(str);
            Response.End();
        }
        if (!IsPostBack)
        {
            M_Permission structMod = perbll.SelReturnModel(Mid);
            OATopNodeID_T.Text = bup.GetNodeIDs(Mid, "OATop");
            OADelNodeID_T.Text = bup.GetNodeIDs(Mid, "OADel");
            OAEditNodeID_T.Text = bup.GetNodeIDs(Mid, "OAEdit");
            RoleName.Text = structMod.RoleName;
            RoleInfo.Text = structMod.Info;
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li><a href='../Addon/StructList.aspx'>组织结构</a></li><li class='active'><a href='"+Request.RawUrl+"'>权限设置</a></li>");
        }
    }
    protected string Add(string codes, int id, string nodeid)
    {
        M_Permission perMod = perbll.SelReturnModel(id);
        perMod.Auth_OA = codes;
        perbll.GetUpdate(perMod);
        return "1";
    }
    protected string Del(int id)
    {
        bup.DelByRoleID(id);
        return "1";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bup.InsertByCodes("ContentManage", Mid);
    }
    public string GetCodes()
    {
        M_Permission perMod = perbll.SelReturnModel(Mid);
        return perMod.Auth_OA;
    }
}