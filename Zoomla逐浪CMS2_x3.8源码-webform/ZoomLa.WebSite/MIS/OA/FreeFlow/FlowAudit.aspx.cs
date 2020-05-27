using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.MIS;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

/*
 * 专用于处理自由流程
 */ 
public partial class MIS_OA_Flow_FlowAudit : System.Web.UI.Page
{
    protected Call commCall = new Call();
    protected OACommon oaCom = new OACommon();
    protected StrHelper strhelp = new StrHelper();
    protected B_Content conBll = new B_Content();
    protected B_ModelField fieldBll = new B_ModelField();
    protected B_MisProLevel stepBll = new B_MisProLevel();
    protected B_Mis_AppProg progBll = new B_Mis_AppProg();
    protected B_MisProcedure proceBll = new B_MisProcedure();
    protected B_OA_FreePro freeBll = new B_OA_FreePro();
    protected B_OA_Document oaBll = new B_OA_Document();
    protected B_OA_ShowField showBll = new B_OA_ShowField();
    protected B_OA_Sign signBll = new B_OA_Sign();
    protected B_User buser = new B_User();
    protected B_Permission perBll = new B_Permission();
    protected M_Mis_AppProg progMod = new M_Mis_AppProg();//执行进度
    int userID; //公文ID,用户ID
    //OA_Document ID;
    public int appID
    {
        get
        {
            return Convert.ToInt32(Request.QueryString["AppID"]);
        }
    }
    public int ModelID { get { return DataConvert.CLng(ViewState["ModelID"]); } set { ViewState["ModelID"] = value; } }
    private string ascx { get { return ViewState["ascx"] as string; } set { ViewState["ascx"] = value; } }
    private B_OAFormUI OAFormUI
    {
        get
        {
            var control = OAForm_Div.FindControl("ascx_" + ascx);
            if (control == null)//加载默认
            {
                control = OAForm_Div.FindControl("ascx_def");
                control.Visible = true;
                return (B_OAFormUI)control;
            }
            else { control.Visible = true; return (B_OAFormUI)control; }
        }
    }
    public M_OA_Document oaMod = new M_OA_Document();
    protected void Page_Load(object sender, EventArgs e)
    {
        //需要加上验证权限,发起人,经办人,审核人可查看进程,只要任意一步骤有该权限
        B_User.CheckIsLogged();
        userID = buser.GetLogin().UserID;
        if (function.isAjax())//处理AJAX
        {
            #region AJAX
            string action = Request.Form["action"];
            string value = Request.Form["value"];
            string result = "0";
            switch (action)
            {
                case "PostDate":
                    int id = Convert.ToInt32(value.Split(':')[0]);
                    DateTime date = Convert.ToDateTime(value.Split(':')[1]);
                    progBll.UpdateDate(userID, id, date);
                    result = "1";
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
            #endregion
        }
        DataTable dt = buser.SelByUserID(userID);//用于验证是否有主办或经办权限,已改为与角色绑定,可以不需要使用dt
        oaMod = oaBll.SelReturnModel(appID);
        if (oaMod.Status == (int)ZLEnum.ConStatus.Filed) { Response.Redirect("FlowView.aspx?action=filed&appid="+appID); }
        if (!IsPostBack)
        {
            #region 权限检测
            M_UserInfo sendmu = buser.GetSelect(oaMod.UserID);
            M_UserInfo mu = buser.GetLogin();
            ascx = proceBll.SelReturnModel(oaMod.ProID).FlowTlp;
            //回退权限
            if (CurrentStep.HToption > 0)
            {
                if (oaMod.CurStepNum == 0)//如果为起始步,则不管是否有权限都不显示
                {

                }
                else
                {
                    rollBackSpan.Visible = true;
                    rollBackBtn.Visible = true;
                    if (CurrentStep.HToption == 1)
                    {
                        rollBackDP.Items.Insert(0, new ListItem("回退至上一步", "-1"));
                    }
                    else if (CurrentStep.HToption == 2)
                    {
                        FreeDPDataBind();
                        rollBackDP.Items.Insert(0, new ListItem("回退至上一步", "-1"));
                    }
                }
            }
            //转交权限
            if (CurrentStep.Qzzjoption > 0)
            {
                if (stepBll.IsLastStep(CurrentStep))//如果是最后一步,则也不显示
                {

                }
                else
                {
                    zjSpan.Visible = true;
                    ZJDataBind();
                    zjDP.Items.Insert(0, new ListItem("下一步", "-1"));
                }
            }
            #region 签章
            DataTable signDT = signBll.SelByUserID(buser.GetLogin().UserID);
            if (oaMod.IsComplete)
            {
                signTr.Visible = false;
            }
            else if (signDT != null && signDT.Rows.Count > 0)
            {
                SignRadioBind(signDT);
            }
            else
            {
                signTrRemind.Visible = true;
            }
            SignImgBind();
            #endregion
            //-------------------经办,审阅等权限
            #region 是否是经办,抄送或发起人
            if (CurrentStep.Auth(M_MisProLevel.AuthEnum.Refer, dt))
            {
                //拥有经办权限
                agreeBtn.Visible = true;
                rejectBtn.Visible = true;
                //检测用户有无修改编号或表单的权限,如果有的话,则显示修改表单栏
                SaveForm_Btn.Visible = true;
                //formop_tr.Visible = true;
            }
            else if (CurrentStep.Auth(M_MisProLevel.AuthEnum.CCUser, dt))
            {
                //拥有抄送权限
                DisAllAuth();
                signTr.Visible = true;
                ccOPBar.Visible = true;
                if (progBll.CheckApproval(buser.GetLogin().UserID, CurrentStep.stepNum, oaMod.ID))
                {
                    signTr.Visible = false;
                    ccUser_Btn.Visible = false;
                    ccUser_Lab.Visible = true;
                }
            }
            else if (oaMod.UserID == userID)//发起人查看文件
            {
                DisAllAuth();
            }
            else
            {
                function.WriteErrMsg("你当前无权限批复该公文!!");
            }
            #endregion
            //附件删除权限,拥有权限和附件的时候才显示
            if (CurrentStep.PublicAttachOption > 0 && !string.IsNullOrEmpty(oaMod.PublicAttach))
            {
                //delAttachBtn.Visible = true;
            }
            //显示附件
            if (!string.IsNullOrEmpty(oaMod.PublicAttach))
            {
                function.Script(this, "ZL_Webup.AddReadOnlyLi('" + oaMod.PublicAttach + "');");
            }
            //会签
            if (CurrentStep.HQoption > 0)
            {
                string ids = progBll.SelHQUserID(appID, CurrentStep.stepNum);
                hqTr.Visible = true;
                hqL.Text = buser.GetUserNameByIDS(ids);
                //如果用户已会签，则不显示拒绝与同意
                if (ids.Split(',').Select(p => p).Contains(buser.GetLogin().UserID.ToString()))
                {
                    opBar.Visible = false;
                }
                //显示未会签人
                string[] allUser = CurrentStep.ReferUser.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string ids2 = StrHelper.RemoveRepeat(allUser, ids.Split(','));//未会签用户
                if (string.IsNullOrEmpty(ids2.Replace(",", "")))
                    unHql.Text = "(办理完成!!!)";
                else
                {
                    unHql.Text = "(尚未办理:" + buser.GetUserNameByIDS(ids2) + ")";
                }
            }
            if (oaMod.Status == -1 || oaMod.Status == 99 || oaMod.Status == 98)
            {
                //如果当前流程已完成,或已被拒绝
                //opBar.Visible = false;
                //delAttachBtn.Visible = false;
            }
            //该栏与上一栏只能显示一个
            if (oaMod.Status == 98 && freeBll.IsLastFreeStep(CurrentStep))//是自由流程,已同意,并且是最后一步时，显示该栏
            {
                if (oaMod.ProType == (int)M_MisProcedure.ProTypes.Free)
                {
                    Free_OP_Tr.Visible = HasNextAuth(CurrentStep,oaMod,mu);
                }
                else if (oaMod.ProType == (int)M_MisProcedure.ProTypes.AdminFree)
                {
                    AdminFree_OP_Tr.Visible = HasNextAuth(CurrentStep, oaMod, mu);
                }
            }
            #region 数据绑定
            //ProceName_L.Text = oaMod.Title;
            ////titleL.Text = oaMod.Title;
            if (!string.IsNullOrEmpty(oaMod.PrivateAttach))
            {
                function.Script(this, "ShowWord();");
            }
            SendMan_L.Text = sendmu.HoneyName;
            stepNameL.Text = CurrentStep.stepName;
            OAFormUI.SendDate_ASCX = oaMod.SendDate.ToString();
            createTimeL.Text = oaMod.SendDate.ToString("yyyy年MM月dd日 HH:mm");
            //txt_Content.Text = oaCom.ReplaceHolder(oaMod);
            ModelID = Convert.ToInt32(proceBll.SelReturnModel(oaMod.ProID).FormInfo);
            DataTable dtContent = conBll.GetContent(Convert.ToInt32(oaMod.Content));
            OAFormUI.InitControl(ViewState, ModelID);
            if (dtContent != null && dtContent.Rows.Count > 0)
            {
                OAFormUI.dataRow = dtContent.Rows[0];
                OAFormUI.MyBind();
            }
            OAFormUI.Title_ASCX = oaMod.Title;
            OAFormUI.NO_ASCX = oaMod.No;
            DataTable authDT = perBll.SelAuthByRoles(mu.UserRole);
            OAFormUI.No_ASCX_T.Enabled = perBll.CheckAuth(authDT, "oa_pro_no");
            DataBind();
            #endregion
            #endregion
        }
    }
    //是否有选择下一步经办人的权限
    private bool HasNextAuth(M_MisProLevel curStep, M_OA_Document oaMod, M_UserInfo mu)
    {
        switch (CurrentStep.DocAuth)
        {
            case "refer":
                return (curStep.ReferUser.Contains("," + mu.UserID + ","));
            case "sender":
                return (oaMod.UserID == mu.UserID);
            case "all":
            default:
                return ((oaMod.UserID == mu.UserID) || (curStep.ReferUser.Contains("," + mu.UserID + ",")));
        }
    }
    private void DataBind(string key = "")
    {
        DataTable dt = new DataTable();
        dt = progBll.SelByAppID(appID.ToString());
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    private void FreeDPDataBind()
    {
        DataTable dt = new DataTable();
        dt = freeBll.SelByStep(oaMod.ID, oaMod.CurStepNum);
        rollBackDP.DataSource = dt;
        rollBackDP.DataValueField = "StepNum";
        rollBackDP.DataTextField = "StepName";
        rollBackDP.DataBind();
    }
    private void ZJDataBind()
    {
        DataTable dt = new DataTable();
        dt = stepBll.SelByStep(oaMod.ProID, oaMod.CurStepNum, ">");
        zjDP.DataSource = dt;
        zjDP.DataValueField = "StepNum";
        zjDP.DataTextField = "StepName";
        zjDP.DataBind();
    }
    private void SignRadioBind(DataTable dt)
    {
        signRadio.DataSource = dt;
        signRadio.DataValueField = "ID";
        signRadio.DataTextField = "SignName";
        signRadio.DataBind();
        for (int i = 0; i < signRadio.Items.Count; i++)
        {
            signRadio.Items[i].Attributes["picUrl"] = dt.Rows[i]["VPath"].ToString();
        }
        signRadio.Items.Insert(0, new ListItem("不使用签章", "-1"));
        signRadio.SelectedValue = "-1";
    }
    //在文档上显示签名图
    private void SignImgBind()
    {

        DataTable imgDT = progBll.SelHasSign(appID);
        string result = "";
        foreach (DataRow dr in imgDT.Rows)
        {
            result += dr["Sign"].ToString() + ":" + dr["VPath"].ToString() + ",";
        }
        //如果有个人签章
        int signid = 0;
        if (!string.IsNullOrEmpty(oaMod.SignID) && oaMod.SignID.Split(':').Length > 0 && Int32.TryParse(oaMod.SignID.Split(':')[0], out signid))
        {
            M_OA_Sign signMod = signBll.SelReturnModel(signid);
            result += oaMod.SignID + ":" + signMod.VPath;
        }
        else result = result.TrimEnd(',');
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "InitPos('" + result + "');", true);
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del2":
                //删除记录，同时删除目标数据库
                break;
        }
    }
    //-----------------------
    protected void agreeBtn_Click(object sender, EventArgs e)
    {
        UpdateModel(98);
        Response.Redirect(Request.RawUrl);//"../AffairsList.aspx?View=2"
    }
    protected void rejectBtn_Click(object sender, EventArgs e)
    {
        UpdateModel(-1);
        Response.Redirect(Request.RawUrl);//"../AffairsList.aspx?View=2"
    }
    protected void rollBackBtn_Click(object sender, EventArgs e)
    {
        UpdateModel(2);
        Response.Redirect(Request.RawUrl);//"../AffairsList.aspx?View=2"
    }
    //转交
    protected void zjBtn_Click(object sender, EventArgs e)
    {
        //只修改步骤,不做其他任何修改
        oaMod = oaBll.SelReturnModel(appID);
        if (zjDP.SelectedValue == "-1")//较交给下一步
        {
            oaMod.CurStepNum = oaMod.CurStepNum == 0 ? 2 : (oaMod.CurStepNum + 1);//默认步数为零
        }
        else
        {
            oaMod.CurStepNum = Convert.ToInt32(zjDP.SelectedValue);
        }
        oaBll.UpdateByID(oaMod);
        Response.Redirect("../AffairsList.aspx?View=2");
    }
    //与写入新的进度信息,如果是最后一步或是拒绝,则更新OA模型
    private void UpdateModel(int status)
    {
        //会签需要加判断,备注需要另外处理
        oaMod = oaBll.SelReturnModel(appID);
        M_MisProcedure proceMod = proceBll.SelReturnModel(oaMod.ProID);
        //throw new Exception(progBll.IsHQComplete(appID, userID, CurrentStep).ToString());
        progMod.AppID = oaMod.ID;
        progMod.ProID = oaMod.ProID;
        progMod.ProLevel = CurrentStep.stepNum;
        progMod.ProLevelName = CurrentStep.stepName;
        progMod.ApproveID = userID;
        progMod.Result = status;
        progMod.Remind = remindT.Text;
        progMod.CreateTime = DateTime.Now;
        progMod.HQUserID = userID.ToString();
        //if (signRadio.Items.Count > 0 && signRadio.SelectedIndex > 0)//0是不使用签章
        //{
        //    progMod.Sign = signRadio.SelectedValue + ":" + curPosD.Value;
        //    progMod.SignID = signRadio.SelectedValue;
        //}
        //用于email与sms通知功能
        string content = "";
        //是否为最后一步
        bool isLastStep = freeBll.IsLastStep(oaMod,CurrentStep);
        //增加通知功能,
        if (isLastStep && status == 98)
        {
            if (oaMod.ProType == (int)M_MisProcedure.ProTypes.AdminFree)
            {
                //公文流程且是最后一步,
                status = proceMod.AllowAttach == 1 ? (int)ZLEnum.ConStatus.Filed : (int)ZLEnum.ConStatus.Audited;
            }
            if (CurrentStep.HQoption > 0)//会签判断
            {
                if (progBll.IsHQComplete(appID, userID, CurrentStep))
                {
                    oaMod.Status = status;//流程完成,全部同意
                    oaMod.CurStepNum = CurrentStep.stepNum;
                    content = oaMod.Title + "会签完成,已进入下一步骤";
                }
            }
            else
            {
                oaMod.Status = status;//流程完成,全部同意
                oaMod.CurStepNum = CurrentStep.stepNum;
                content = oaMod.Title + "已完成" + CurrentStep.stepName + ",进入下一步骤";
            }
        }
        else if (status == 2) //回退
        {
            #region 回退
            oaMod.Status = 2;

            int rbStep = DataConvert.CLng(rollBackDP.SelectedValue);
            if (rbStep > 0)
            {
                rbStep = rbStep - 1;
                //自由流程回退删除步骤
                freeBll.DelByStep(oaMod.ID, Convert.ToInt32(rollBackDP.SelectedValue));
            }
            else  //回退至上一步(-1)
            {
                rbStep = CurrentStep.stepNum - 2;
                //自由流程回退删除步骤
               freeBll.DelByStep(oaMod.ID, CurrentStep.stepNum - 1);
            }
            oaMod.CurStepNum = rbStep;
            progMod.Remind2 = "回退至" + rbStep;
            progMod.HQUserID = "";
            content = oaMod.Title + "已被经办人回退至" + rbStep;
            //回退的时候，清除目标会签与签章信息
            progBll.ClearHQ(oaMod.ID, rbStep);
            progBll.ClearSign(oaMod.ID, rbStep);
            #endregion
        }
        else if (status == -1)
        {
            oaMod.Status = status;  //拒绝
            oaMod.CurStepNum = CurrentStep.stepNum;
            content = oaMod.Title + "已被经办人" + buser.GetLogin().HoneyName + "拒绝.";
        }
        else//同意，但未至最后一步
        {
            if (CurrentStep.HQoption > 0)//需要会签
            {
                if (progBll.IsHQComplete(appID, userID, CurrentStep))
                {
                    oaMod.Status = status;
                    oaMod.CurStepNum = CurrentStep.stepNum;
                    content = oaMod.Title + "已完成会签审批,成功通过";
                }
            }
            else
            {
                oaMod.Status = status;
                oaMod.CurStepNum = CurrentStep.stepNum;
                content = oaMod.Title + "已完成审批,成功通过";
            }
        }

        if (!string.IsNullOrEmpty(CurrentStep.EmailAlert))//Email与sms通知
        {
            //发送邮件包括会员组里的会员(需扩展支持会员组)
            string ids = "";
            //if (!string.IsNullOrEmpty(CurrentStep.EmailGroup))
            //    ids += groupBll.GetUserIDByGroupIDS(CurrentStep.EmailGroup);
            ids += CurrentStep.SmsAlert;
            ids = ids.Trim(',');
            emailToUser(content, ids);
        }
        //if (!string.IsNullOrEmpty(CurrentStep.SmsAlert))
        //{
        //    smsTouser(content, CurrentStep.SmsAlert);
        //}
        progBll.Insert(progMod);
        oaBll.UpdateByID(oaMod);
    }
    // 返回当前需要进行的流程模型,如果无流程,或流已完结,返回最后一步模型
    //仅用于处理自由流程(另一页面改为只处理自由流程)
    public M_MisProLevel GetNextLevel()
    {
        //CurStepNum:指定当前进行到的步骤
        M_MisProLevel model = new M_MisProLevel();
        //获取下一级
        oaMod = oaBll.SelReturnModel(appID);//公文模型
        if (oaMod.Status == -1 || oaMod.Status == 99)//如果已经处理完成，则返回最后一步模型
        {
            model = freeBll.SelByProIDAndStepNum(oaMod.ID, oaMod.CurStepNum);
            return model;
        }
        DataTable progDT = progBll.SelByAppID(appID.ToString());//已进行到的流程
        DataTable freeDT = freeBll.SelDTByDocID(oaMod.ID);//全部自由流程
        if (oaMod.CurStepNum == 0)//步骤为0时，加载第一步
        {
            if (freeDT == null || freeDT.Rows.Count < 1)
            {
                function.WriteErrMsg("该自由流程未指定投递人!!!");
            }
            model = model.GetModelFromDR(freeDT.Rows[0]);//用第一个填充,其值是经过Level排序的
        }
        else
        {
            model = freeBll.SelByProIDAndStepNum(oaMod.ID, oaMod.CurStepNum);//获取上一步骤的模型
            if (!freeBll.IsLastFreeStep(model))//如果不是最后一步,则进一位
            {
                model = freeBll.SelByProIDAndStepNum(oaMod.ID, (oaMod.CurStepNum + 1));
            }
        }
        return model;
    }
    //获取当前正在执行政步骤
    public M_MisProLevel CurrentStep
    {
        get
        {
            if (ViewState["CurrentStep"] == null)
                ViewState["CurrentStep"] = GetNextLevel();
            return ViewState["CurrentStep"] as M_MisProLevel;
        }
        set { ViewState["CurrentStep"] = value; }
    }
    public string GetResult(object o)
    {
        return progMod.GetResult(DataConvert.CLng(o.ToString()));
    }

    //--------------邮件等通知
    protected M_Message mailMod = new M_Message();
    protected B_Message mailBll = new B_Message();
    //步骤改变时，发送站内邮件给目标用户
    public void emailToUser(string content, string ids)
    {
        mailMod.Title = "系统提醒";
        mailMod.MsgType = 3;
        mailMod.Content = content;//XXXX流程已进入下一步，XXXX
        mailMod.CCUser = ids;
        mailMod.Sender = "1";
        mailMod.PostDate = DateTime.Now;
        mailBll.GetInsert(mailMod);
    } 

    //CC与本人查阅使用,禁掉所有权限
    public void DisAllAuth()
    {
        opBar.Visible = false;//操作栏
        signTr.Visible = false;//签章栏
        //remindTr.Visible = false;//审核意见栏
    }
    //获取ZL_User中的数据,包含组名等

    //完结自由流程，只有会签完毕才会显示
    protected void Free_Sure_Btn_Click(object sender, EventArgs e)
    {
        //判断是否真的执行完成，避免用户添加了步骤后，又点完结流程

        if (freeBll.IsLastStep(oaMod,CurrentStep))
        {
            M_MisProcedure proceMod = proceBll.SelReturnModel(oaMod.ProID);
            oaMod.Status = proceMod.AllowAttach == 1 ? (int)ZLEnum.ConStatus.Filed : (int)ZLEnum.ConStatus.Audited;
            oaBll.UpdateByID(oaMod);
            Response.Redirect(Request.RawUrl);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "disAlert", "alert('流程未完成,是否已指定下一步骤!!!');location=location;", true);
        }

    }
    //协办人，即CCUser发表意见与批复
    protected void ccUser_Btn_Click(object sender, EventArgs e)
    {
        M_Mis_AppProg mod = new M_Mis_AppProg();
        mod.AppID = oaMod.ID;
        mod.ProID = oaMod.ProID;
        mod.ProLevel = CurrentStep.stepNum;
        mod.ProLevelName = CurrentStep.stepName;
        mod.ApproveID = buser.GetLogin().UserID;
        mod.Result = 0;
        mod.Remind = remindT.Text;
        mod.CreateTime = DateTime.Now;
        mod.HQUserID = "";
        if (signRadio.Items.Count > 0 && signRadio.SelectedIndex > 0)//0是不使用签章
        {
            mod.Sign = signRadio.SelectedValue + ":" + curPosD.Value;
            mod.SignID = signRadio.SelectedValue;
        }
        progBll.Insert(mod);
        Response.Redirect(Request.RawUrl);
    }
    //回复人可以修改回复日期
    public string HasEditDate()
    {
        string result = "";
        string date = Convert.ToDateTime(Eval("CreateTime")).ToString("yyyy/MM/dd");
        if (userID == DataConvert.CLng(Eval("ApproveID")))//当前用户为回复人,拥有修改权限
        {
            result = "<input type='text' id='SignDate_T_" + Eval("ID") + "' value='" + date + "' onfocus='WdatePicker({dateFmt:\"yyyy/MM/dd\"});' /><input type='button' value='保存' class='btn btn-xs btn-primary' onclick='PostDate(" + Eval("ID") + ");' />";
        }
        else
        {
            result = date;
        }
        return result;
    }
    //修改表单
    protected void SaveForm_Btn_Click(object sender, EventArgs e)
    {
        //后台验证,前台不验证
        OAFormUI.vstate = ViewState;
        M_UserInfo mu = buser.GetLogin();
        DataTable authDT = perBll.SelAuthByRoles(mu.UserRole);
        M_OA_Document oaMod = oaBll.SelReturnModel(appID);
        if (perBll.CheckAuth(authDT, "oa_pro_no"))
        {
            oaMod.No = OAFormUI.NO_ASCX;
        }
        oaMod.Title = OAFormUI.Title_ASCX;
        oaMod.SendDate =DataConvert.CDate(OAFormUI.SendDate_ASCX);
        DataTable table = OAFormUI.CreateTable(OAFormUI.GetFields(OAFormUI.ModelID).Split(','));
        M_CommonData conMod = conBll.SelReturnModel(Convert.ToInt32(oaMod.Content));
        conBll.UpdateContent(table, conMod);
        oaBll.UpdateByID(oaMod);
        //if (!string.IsNullOrEmpty(CurrentStep.CanEditField))//修改表单
        //{
        //    M_ModelInfo modelMod = bmode.GetModelById(ModelID);
        //    M_CommonData CData = conBll.SelReturnModel(Convert.ToInt32(oaMod.Content));
        //    DataTable dt = fieldBll.GetModelFieldListall(ModelID);
        //    DataTable table = commCall.GetDTFromPage(dt, Page, ViewState, null, true, CurrentStep.CanEditField);//读取步骤中设定的可修改字段
        //    conBll.UpdateContent(table, CData);
        //}
        function.WriteSuccessMsg("修改成功");
    }
}