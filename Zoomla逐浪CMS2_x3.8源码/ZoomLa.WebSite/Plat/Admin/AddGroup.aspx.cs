using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

public partial class Plat_Group_AddGroup : System.Web.UI.Page
{
    B_Plat_Group gpBll = new B_Plat_Group();
    B_User buser = new B_User();
    private int Pid { get { return DataConvert.CLng(ViewState["Pid"]); } set { ViewState["Pid"] = value; } }
    private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Pid = DataConvert.CLng(Request.QueryString["Pid"]);
            MyBind();
        }
    }
    private void MyBind() 
    {
        if (Mid > 0) 
        {
            M_Plat_Group gpMod = gpBll.SelReturnModel(Mid);
            GroupName_T.Text = gpMod.GroupName;
            GroupDesc_T.Text = gpMod.GroupDesc;
            manage_hid.Value = buser.SelByIDS(gpMod.ManageIDS);
            member_hid.Value = buser.SelByIDS(gpMod.MemberIDS);
            Pid = gpMod.Pid;
        }
        M_Plat_Group pmod = gpBll.SelReturnModel(Pid);
        if (pmod != null) { PGroup_L.Text = pmod.GroupName; }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Plat_Group gpMod = Mid > 0 ? gpBll.SelReturnModel(Mid) : new M_Plat_Group();
        M_User_Plat upMod = B_User_Plat.GetLogin();
        gpMod.GroupName = GroupName_T.Text;
        gpMod.GroupDesc = GroupDesc_T.Text;
        gpMod.ManageIDS = manage_hid.Value;
        gpMod.MemberIDS = member_hid.Value;
        if (gpMod.ID > 0) { gpBll.UpdateByID(gpMod); }
        else
        {
            string nodeTree = "";
            gpMod.Pid = Pid;
            gpMod.CreateUser = upMod.UserID;
            gpMod.FirstID = gpBll.SelFirstID(Pid, ref nodeTree);
            gpMod.Depth = gpBll.GetDepth(Pid);
            gpBll.Insert(gpMod);
        }
        function.WriteSuccessMsg("信息保存成功","GroupAdmin.aspx");
    }
}