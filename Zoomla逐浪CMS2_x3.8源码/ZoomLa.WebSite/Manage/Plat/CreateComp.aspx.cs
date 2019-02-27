using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

public partial class Manage_Plat_CreateComp : CustomerPageAction
{
    B_Plat_Comp compBll = new B_Plat_Comp();
    B_Plat_Group gpBll = new B_Plat_Group();
    B_Plat_UserRole urBll = new B_Plat_UserRole();
    B_User_Plat upBll = new B_User_Plat();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            M_APIResult retMod = new M_APIResult();
            retMod.retcode = M_APIResult.Failed;
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(Request["list"]);
            foreach (DataRow dr in dt.Rows)
            {
                string uname = dr["uname"].ToString();
                if (string.IsNullOrEmpty(uname)) { continue; }
                if (buser.IsExist("ume", uname)) { retMod.retmsg = "用户名[" + uname + "]已存在,请修改名称"; RepToClient(retMod); }
            }
            retMod.retcode = M_APIResult.Success;
            RepToClient(retMod);
        }
        if (!IsPostBack)
        {
            MyBind();
            PreFix_Hid.Value = function.GetRandomString(6, 3).ToLower();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='CompList.aspx'>能力中心</a></li><li><a href='PlatInfoManage.aspx'>信息管理</a></li><li><a href='" + Request.RawUrl + "'>创建企业</a></li>");
        }
    }
    private void RepToClient(M_APIResult retMod) { Response.Clear(); Response.Write(retMod.ToString()); Response.Flush(); Response.End(); }
    private void MyBind() { }
    //选择好用户后,为其创建相关信息
    protected void Submit_Btn_Click(object sender, EventArgs e)
    {
        int uid = Convert.ToInt32(User_Hid.Value.Split(',')[0]);
        M_UserInfo mu = buser.SelReturnModel(uid);
        if (mu.IsNull) { function.WriteErrMsg("用户[" + User_T.Text + "]不存在"); }
        if (upBll.SelReturnModel(uid) != null) { function.WriteErrMsg("创建失败:用户" + User_T.Text + "已经有了企业,不能重复创建"); }
        M_Plat_Comp compMod = new M_Plat_Comp();
        M_Plat_UserRole urMod = new M_Plat_UserRole();
        DataTable userDT = JsonConvert.DeserializeObject<DataTable>(UserInfo_Hid.Value);//为空下是否会报错
        if (SFileUp.HasFile) { SFileUp.SaveFile(); }
        //------------------公司
        compMod.CompName = CompName_T.Text;
        compMod.Status = 1;
        compMod.CreateUser = mu.UserID;
        compMod.CompLogo = SFileUp.FileUrl;
        compMod.UPPath = compBll.CreateUPPath(compMod);
        compMod.ID = compBll.Insert(compMod);
        //------------------角色
        urMod.IsSuper = 1;
        urMod.RoleAuth = "";
        urMod.CompID = compMod.ID;
        urMod.RoleName = "网络管理员";
        urMod.RoleDesc = "公司网络管理员,拥有全部权限,该角色只允许存在一个";
        urMod.UserID = mu.UserID;
        urMod.ID = urBll.Insert(urMod);
        //------------------网络管理员
        {
            M_User_Plat upMod = upBll.NewUser(mu, compMod);
            upMod.Plat_Role = "," + urMod.ID + ",";
            upBll.Insert(upMod);
            M_Plat_Group gpMod = gpBll.NewGroup("办公部门", upMod.CompID, upMod.UserID);
            gpBll.Insert(gpMod);
        }
        //------------------其他权限管理员
        upBll.NewByUserDT(compMod, userDT);
        function.WriteSuccessMsg(mu.UserName + "的企业[" + compMod.CompName + "]创建成功", "CompList.aspx");
    }
}