using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
//认证公司
public partial class Plat_Common_ApplyForCert : System.Web.UI.Page
{
    B_Plat_Comp compBll = new B_Plat_Comp();
    B_Common_UserApply ualyBll = new B_Common_UserApply();
    M_Common_UserApply ualyMod = null;
    M_User_Plat upMod = null;
    M_Plat_Comp compMod = null;
    //检测是否申请过
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind() 
    {
        //如果企业已认证过,或其不是企业的创始人,则无权修改
        upMod = B_User_Plat.GetLogin();
        compMod = compBll.SelReturnModel(upMod.CompID);
        err_div.Visible = true;
        if (compMod.Status != 0) { err_div.InnerHtml = "该企业已经认证过了"; }
        else if (compMod.CreateUser != upMod.UserID) { err_div.InnerHtml = "你没有提交认证的权限"; }
        else if (ualyBll.CompCert_Sel((int)ZLEnum.ConStatus.UnAudit, "", upMod.UserID).Rows.Count > 0)
        {
            //检测是否已提交过申请
            err_div.InnerHtml = "你已经提交过申请了,请等待管理员处理";
        }
        else 
        {
            err_div.Visible = false;
            ok_div.Visible = true;
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        string telephone = Telephone_T.Text.Replace(" ","");
        string mobile = Mobile_T.Text.Replace(" ","");
        if (string.IsNullOrEmpty(telephone) && string.IsNullOrEmpty(mobile))
        {
            function.Script(this,"alert('电话和手机不能同时为空！');");
            return;
        }
        upMod = B_User_Plat.GetLogin();
        JObject json = new JObject();
        json.Add("mail", Mails_T.Text);
        json.Add("mobile", mobile);
        json.Add("telephone", telephone);
        json.Add("compid", upMod.CompID);
        json.Add("compshort", CompShort_T.Text);
        ualyMod = new M_Common_UserApply();
        ualyMod.ZType = "plat_compcert";
        ualyMod.UserID = upMod.UserID;
        ualyMod.UserName = upMod.UserName;
        ualyMod.Remind = CompName_T.Text;
        ualyMod.UserRemind = JsonConvert.SerializeObject(json);
        ualyBll.Insert(ualyMod);
        function.WriteSuccessMsg("申请已提交,请等待管理员审核", "/Plat/");
    }
}