using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
/// <summary>
/// 非能力中心用户申请加入公司
/// </summary>
public partial class Plat_Common_ApplyJoinComp : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Plat_Comp compBll = new B_Plat_Comp();
    B_Common_UserApply ualyBll = new B_Common_UserApply();
    M_Common_UserApply ualyMod = new M_Common_UserApply();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged(Request.RawUrl);
        if (!IsPostBack)
        {
            //已申请加入,必须等待对方网络管理员审核
            //已申请,再进入该页面,进入请等待审核提示(可取消申请)
            MyBind();
        }
    }
    public void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        if (ualyBll.JoinComp_Exist(mu.UserID))
        {
            comp_tab.Visible = true;
            //申请信息
            DataTable dt = ualyBll.Search("plat_joincomp", "", "", (int)ZLEnum.ConStatus.UnAudit, mu.UserID);
            ualyMod = ualyBll.SelReturnModel(DataConverter.CLng(dt.Rows[0]["ID"]));
            //申请加入的公司信息
            int compid = DataConverter.CLng(ualyMod.Remind);
            compId_Hid.Value = compid.ToString();
            ualyId_Hid.Value = ualyMod.ID.ToString();
            M_Plat_Comp compMod = compBll.SelReturnModel(compid);
            BindComp(compMod);
            Apply_B.Visible = false;
            apply_div.Visible = false;
            cancel_div.Visible = true;
            Mess_L.Text = "您已提交申请,请等候公司管理员审核";
        }
        else
        {
           
            M_User_Plat upMod = B_User_Plat.GetLogin();
            if (upMod != null)
            {
                M_Plat_Comp compMod = compBll.SelReturnModel(upMod.CompID);
                if (compMod.Status == 1)
                {
                    Mess_L.Text = "您已加入了[ " + compMod.CompName+" ]";
                    BindComp(compMod);
                    comp_tab.Visible = true;
                    Apply_B.Visible = false;
                    apply_div.Visible = false;
                    cancel_div.Visible = true;
                    Cancel_B.Visible = false;
                }
            }
        }
    }
    protected void Skey_Btn_Click(object sender, EventArgs e)
    {
        M_Plat_Comp compMod = compBll.SelModelByName(Skey_T.Text);
        if (compMod == null)
        {
            empty_div.Visible = true;
            comp_tab.Visible = false;
            Empty_L.Text = "[" + Skey_T.Text + "]公司不存在,请核对公司名称后再搜索";
        }
        else
        {
            empty_div.Visible = false;
            comp_tab.Visible = true;
            BindComp(compMod);
        }
    }
    public void BindComp(M_Plat_Comp compMod)
    {
        compId_Hid.Value = compMod.ID.ToString();
        CompName_T.Text = compMod.CompName + "(" + compMod.CompShort + ")";
        CreateTime_T.Text = compMod.CreateTime.ToString("yyyy-MM-dd");
        Desc_T.Text = string.IsNullOrEmpty(compMod.CompDesc) ? "(暂无)" : compMod.CompDesc;
        Logo_Img.ImageUrl = compMod.CompLogo;
    }
    //取消申请
    protected void Cancel_B_Click(object sender, EventArgs e)
    {
        int id = DataConverter.CLng(ualyId_Hid.Value);
        ualyBll.ChangeByIDS(id.ToString(), (int)ZLEnum.ConStatus.Recycle);
        function.WriteSuccessMsg("操作成功!");
    }
    //申请加入
    protected void Apply_B_Click(object sender, EventArgs e)
    {
        int compid = DataConverter.CLng(compId_Hid.Value);
        M_UserInfo mu = buser.GetLogin();
        //记录用户信息并写入
        ualyMod.UserID = mu.UserID;
        ualyMod.ZType = "plat_joincomp";
        ualyMod.UserName = mu.UserName + "(" + mu.TrueName + ")";
        ualyMod.Remind = compid.ToString();
        ualyMod.IP = IPScaner.GetUserIP();
        ualyBll.Insert(ualyMod);
        function.WriteSuccessMsg("申请成功,请等待管理员审核");
    }
}