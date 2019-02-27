using Newtonsoft.Json;
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

public partial class Manage_WorkFlow_AddFlowStep : System.Web.UI.Page
{
    protected B_UserBaseField ubBll = new B_UserBaseField();
    protected B_User buser = new B_User();
    protected B_Structure strBll = new B_Structure();
    protected M_MisProcedure proMod = new M_MisProcedure();
    protected B_MisProcedure proBll = new B_MisProcedure();
    protected B_MisProLevel stepBll = new B_MisProLevel();
    //流程ID
    public int proID { get { return DataConvert.CLng(Request.QueryString["ProID"]); } }
    public int Mid { get { return DataConvert.CLng(Request.QueryString["StepID"]); } }
    public string refergt;
    public string refergd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (proID<1)
            function.WriteErrMsg("请先选定流程!!!");
        if (!IsPostBack)
        {
            if (Mid>0)
            {
                saveBtn.Text = "修改";
                M_MisProLevel stepMod = stepBll.SelReturnModel(Mid);
                stepCodeT.Text = stepMod.stepNum.ToString();
                stepNameT.Text = stepMod.stepName;
                ReferUser_Hid.Value =  stepMod.ReferUser;
                referUserDatas_Hid.Value= JsonConvert.SerializeObject(buser.SelectUserByIds(stepMod.ReferUser));
                referGroup_Hid.Value = stepMod.ReferGroup;
                CCUser_Hid.Value = stepMod.CCUser;
                ccUserDatas_Hid.Value= JsonConvert.SerializeObject(buser.SelectUserByIds(stepMod.CCUser));
                ccGroup_Hid.Value = stepMod.CCGroup;
                emailAlertD.Value = stepMod.EmailAlert;
                emailGroupD.Value = stepMod.EmailGroup;
                smsAlertD.Value = stepMod.SmsAlert;
                smsGroupD.Value = stepMod.SmsGroup;
                ReferUser_T.Text = buser.GetUserNameByIDS(stepMod.ReferUser);
                CCUser_T.Text = buser.GetUserNameByIDS(stepMod.CCUser);
               // referGroup_T.Text = strBll.SelStrNameByIDS(stepMod.ReferGroup);
                //ccGroup_T.Text = strBll.SelStrNameByIDS(stepMod.CCGroup);
                emailAlertT.Text = buser.GetUserNameByIDS(stepMod.EmailAlert);
                emailGroupT.Text = strBll.SelStrNameByIDS(stepMod.EmailGroup);
                smsAlertT.Text = buser.GetUserNameByIDS(stepMod.SmsAlert);
                smsGroupT.Text = strBll.SelStrNameByIDS(stepMod.SmsGroup);
                hqOptionDP.SelectedValue=stepMod.HQoption.ToString();
                qzzjDP.SelectedValue = stepMod.Qzzjoption.ToString();
                htDP.SelectedValue = stepMod.HToption.ToString();
                remindT.Text = stepMod.Remind;
                function.Script(this,"SetRadVal('next_rad','"+stepMod.DocAuth+"')");
                //PublicAttachOptionDP.SelectedValue = stepMod.PublicAttachOption.ToString();
                //CanEditField_T.Text = stepMod.CanEditField;
                //function.Script(this, "SetChkVal('docauth_chk','"+stepMod.DocAuth+"');");
            }
            else
                stepCodeT.Text = GenStepCode(proID).ToString();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='AddFlow.aspx?proID=" + proID + "'>流程设计</a></li><li class='active'>添加步骤</a></li>");
    }
    /// <summary>
    /// 产生序号
    /// </summary>
    /// <param name="pid">目标流程的ID</param>
    /// <returns></returns>
    public int GenStepCode(int proid)
    {
        int stepCode = 1;
        DataTable dt = new DataTable();
        dt = stepBll.SelByProID(proid);
        if (dt.Rows.Count > 0)
        {
            stepCode = dt.Rows.Count + 1;
        }
        return stepCode;
    }
    protected void saveBtn_Click(object sender, EventArgs e)
    {
        M_MisProLevel stepModel = new M_MisProLevel();
        if (Mid > 0)
        {
            stepModel = stepBll.SelReturnModel(Mid);
        }
        stepModel.ProID = DataConvert.CLng(proID);
        stepModel.stepNum = DataConvert.CLng(stepCodeT.Text.Trim());
        stepModel.stepName = stepNameT.Text.Trim();
        stepModel.SendMan = "";//不用这个了。
        stepModel.ReferUser = ReferUser_Hid.Value.TrimEnd(',');
        stepModel.ReferGroup = referGroup_Hid.Value.TrimEnd(',');
        stepModel.CCUser = CCUser_Hid.Value.TrimEnd(',');
        stepModel.CCGroup = ccGroup_Hid.Value.TrimEnd(',');
        stepModel.HQoption = DataConvert.CLng(hqOptionDP.SelectedValue);
        stepModel.Qzzjoption = DataConvert.CLng(qzzjDP.SelectedValue);
        stepModel.HToption = DataConvert.CLng(htDP.SelectedValue);
        stepModel.EmailAlert = emailAlertD.Value.TrimEnd(',');
        stepModel.EmailGroup = emailGroupD.Value.TrimEnd(',');
        stepModel.SmsAlert = smsAlertD.Value.TrimEnd(',');
        stepModel.SmsGroup = smsGroupD.Value.TrimEnd(',');
        //stepModel.PublicAttachOption = DataConvert.CLng(PublicAttachOptionDP.SelectedValue);//使用模型字段,不需要此功能限制
        stepModel.Status = 1;
        stepModel.CreateTime = DateTime.Now;
        stepModel.Remind = remindT.Text.Trim();
        stepModel.CanEditField = "*";//CanEditField_T.Text;
        stepModel.DocAuth = Request.Form["next_rad"];
        //stepModel.DocAuth = Request.Form["docauth_chk"];
        if (!string.IsNullOrEmpty(Request.QueryString["stepID"]))
        {
            stepModel.ID = DataConvert.CLng(Request.QueryString["stepID"]);
            stepBll.UpdateByID(stepModel);
        }
        else
            stepBll.insert(stepModel);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "location=location;", true);
        Response.Redirect("AddFlow.aspx?proID=" + proID);
    }
}