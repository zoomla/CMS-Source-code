using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

/*
 * 公文的具体处理页,对权限作出各种限制
 * 以前的定位好的签章，不允许其更改
 * 此页应为纯AJAX,点击按钮后,应跳转重刷该页面
 */ 
public partial class MIS_ZLOA_Office_ReadOffice : System.Web.UI.Page
{
    protected B_Content conBll = new B_Content();
    protected B_Group groupBll = new B_Group();
    protected B_Node bnode = new B_Node();
    protected B_Model bmode = new B_Model();
    protected B_ModelField bfield = new B_ModelField();
    protected B_MisProLevel stepBll = new B_MisProLevel();
    protected M_Mis_AppProg progMod = new M_Mis_AppProg();//执行进度
    protected B_Mis_AppProg progBll = new B_Mis_AppProg();
    protected B_OA_FreePro freeBll = new B_OA_FreePro();
    protected B_OA_Document oaBll = new B_OA_Document();
    protected B_User buser = new B_User();
    protected B_Sensitivity sll = new B_Sensitivity();
    protected Call commCall = new Call();
    protected OACommon oaCom = new OACommon();
    //签章
    protected B_OA_Sign signBll = new B_OA_Sign();
    //公用
    public int appID, userID;//公文ID,用户ID
    public M_OA_Document oaMod = new M_OA_Document();
    /*
     *第一步,则不显示回退 
     *最后一步,不允许转交
     *一切以CurStepNum为准,必须维护好这个数值
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        //需要加上验证权限,发起人,经办人,审核人可查看进程,只要任意一步骤有该权限
        B_User.CheckIsLogged();
        userID = buser.GetLogin().UserID;
        if (function.isAjax())//处理AJAX
        {
            string action = Request.Form["action"];
            string value = Request.Form["value"];
            string result="0";
            switch (action)
            {
                case "PostDate":
                    int id = Convert.ToInt32(value.Split(':')[0]);
                    DateTime date = Convert.ToDateTime(value.Split(':')[1]);
                    try
                    {
                        progBll.UpdateDate(userID, id, date);
                        result = "1";
                    }
                    catch { }
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
        }
        EGV.txtFunc = txtPageFunc;
        appID = DataConverter.CLng(Request.QueryString["AppID"]);
        DataTable dt = buser.SelByUserID(buser.GetLogin().UserID);
        oaMod = oaBll.SelReturnModel(appID);
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetSelect(oaMod.UserID);
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
                        if (oaMod.IsFreePro)
                            FreeDPDataBind();
                        else
                            DPDataBind();
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
            if (CurrentStep.Auth(M_MisProLevel.AuthEnum.Refer, dt))
            {
                //拥有经办权限
                agreeBtn.Visible = true;
                rejectBtn.Visible = true;
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
            else if (oaMod.UserID == userID)
            {
                //发起人查看文件
                DisAllAuth();
            }
            else
            {
                function.WriteErrMsg("你当前无权限批复该公文!!");
            }
            //附件删除权限,拥有权限和附件的时候才显示
            if (CurrentStep.PublicAttachOption > 0&&!string.IsNullOrEmpty(oaMod.PublicAttach))
            {
                //delAttachBtn.Visible = true;
            }
            //显示附件
            if (!string.IsNullOrEmpty(oaMod.PublicAttach))
            {
                string[] af = oaMod.PublicAttach.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
                 string h = "";
                for (int i = 0; i < af.Length; i++)
                {
                    h+="<span class='disupFile'>";
                    h += GroupPic.GetShowExtension(GroupPic.GetExtName(af[i]));
                    string filename = Path.GetExtension(af[i]).ToLower().Replace(".", "");
                    if (filename == "docx" || filename == "doc" || filename == "xls" || filename == "xlsx")
                    {
                        h += af[i].Split('/')[(af[i].Split('/').Length - 1)] + "</a><a class='filea' href='/PreView.aspx?vpath=" + Server.UrlEncode(af[i]) + "' target='_blank'>(预览)</a><a href='" + af[i] + "'>(下载)</span>";
                    }
                    else
                        h += "<a href='" + af[i] + "' title='点击下载'>" + af[i].Split('/')[(af[i].Split('/').Length - 1)] + "</a></span>";
                }
                publicAttachTD.InnerHtml = h;
            }
            //会签
            if (CurrentStep.HQoption>0)
            {
                string ids=progBll.SelHQUserID(appID, CurrentStep.stepNum);
                hqTr.Visible = true;
                hqL.Text = buser.GetUserNameByIDS(ids);
                //如果用户已会签，则不显示拒绝与同意
                if (ids.Split(',').Select(p => p).Contains(buser.GetLogin().UserID.ToString())) 
                {
                    opBar.Visible = false;
                }
                //显示未会签人
               string[] allUser=CurrentStep.ReferUser.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
               string ids2 = RemoveRepeat(allUser,ids.Split(','));//未会签用户
               if (string.IsNullOrEmpty(ids2.Replace(",", "")))
                   unHql.Text = "(会签完成!!!)";
               else {
                   unHql.Text = "(尚未会签:" + buser.GetUserNameByIDS(ids2) + ")";
               }
            }
            if (oaMod.Status == -1 || oaMod.Status == 99 || oaMod.Status == 98)
            {
               //如果当前流程已完成,或已被拒绝
                //opBar.Visible = false;
                //delAttachBtn.Visible = false;
            }
            //该栏与上一栏只能显示一个,逻辑上问题,当前是第三步,但第三步未处理完
            if (oaMod.IsFreePro && oaMod.Status == 98&&freeBll.IsLastStep(oaMod,CurrentStep))//是自由流程,已同意,并且是最后一步时，显示该栏
            {
                Free_OP_Tr.Visible = true;
            }
            titleL.Text = oaMod.Title;
            sendManL.Text = mu.UserName;
            stepNameL.Text = CurrentStep.stepName;
            createTimeL.Text = oaMod.CreateTime.ToString("yyyy年MM月dd日 HH:mm");
            txt_Content.Text = oaCom.ReplaceHolder(oaMod);
            DataBind();
        }
    }
    private void DataBind(string key = "")
    {
        DataTable dt = new DataTable();
        dt = progBll.SelByAppID(appID.ToString());
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    private void DPDataBind() 
    {
        DataTable dt=new DataTable();
        dt = stepBll.SelByStep(oaMod.ProID,oaMod.CurStepNum);
        rollBackDP.DataSource = dt;
        rollBackDP.DataValueField = "StepNum";
        rollBackDP.DataTextField = "StepName";
        rollBackDP.DataBind();
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
        dt = stepBll.SelByStep(oaMod.ProID, oaMod.CurStepNum,">");
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
           signRadio.Items[i].Attributes["picUrl"]=dt.Rows[i]["VPath"].ToString();
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
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "InitPos('" + result + "');", true);
    }
    //处理页码
    public void txtPageFunc(string size)
    {
        int pageSize;
        if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
        {
            pageSize = EGV.PageSize;
        }
        else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
        {
            pageSize = EGV.PageSize;
        }
        EGV.PageSize = pageSize;
        EGV.PageIndex = 0;//改变后回到首页
        size = pageSize.ToString();
        DataBind();
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
        int s = 99;
        if (oaMod.IsFreePro)//增加个98状态,用于指定自由流程是否完成
        {
            s = 98;
        }
        UpdateModel(s);
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
        if (signRadio.Items.Count > 0 && signRadio.SelectedIndex > 0)//0是不使用签章
        {
            progMod.Sign = signRadio.SelectedValue + ":" + curPosD.Value;
            progMod.SignID = signRadio.SelectedValue;
        }
        //用于email与sms通知功能
        string content = "";

         //是否为最后一步
        bool isLastStep=false;
        if (oaMod.ProID == 0) isLastStep = freeBll.IsLastStep(oaMod, CurrentStep);
        else isLastStep=stepBll.IsLastStep(CurrentStep);

        //增加通知功能,
        if (isLastStep&& status == 99||status==98)
        {
            if (CurrentStep.HQoption > 0)//会签判断
            {
                if (progBll.IsHQComplete(appID, userID, CurrentStep))
                {
                    oaMod.Status = status;//流程完成,全部同意
                    oaMod.CurStepNum = CurrentStep.stepNum;
                    content = oaMod.Title+"会签完成,已进入下一步骤";
                }
            }
            else
            {
                oaMod.Status = status;//流程完成,全部同意
                oaMod.CurStepNum = CurrentStep.stepNum;
                content = oaMod.Title + "已完成"+CurrentStep.stepName+",进入下一步骤";
            }
        }
        else if (status == 2) //回退
        {
            oaMod.Status = 2;

            int rbStep = DataConvert.CLng(rollBackDP.SelectedValue);
            if (rbStep > 0)
            {
                rbStep = rbStep - 1;
                //自由流程回退删除步骤
                if (oaMod.IsFreePro)
                    freeBll.DelByStep(oaMod.ID, Convert.ToInt32(rollBackDP.SelectedValue));
            }
            else  //回退至上一步(-1)
            {
                rbStep = CurrentStep.stepNum - 2;
                //自由流程回退删除步骤
                if (oaMod.IsFreePro)
                    freeBll.DelByStep(oaMod.ID, CurrentStep.stepNum - 1);
            }
            oaMod.CurStepNum = rbStep;
            progMod.Remind2 = "回退至" + rbStep;
            progMod.HQUserID = "";
            content = oaMod.Title + "已被经办人回退至" + rbStep;
            //回退的时候，清除目标会签与签章信息
            progBll.ClearHQ(oaMod.ID,rbStep);
            progBll.ClearSign(oaMod.ID,rbStep);
        }
        else if (status ==-1)
        {
            oaMod.Status = status;  //拒绝
            oaMod.CurStepNum = CurrentStep.stepNum;
            content = oaMod.Title + "已被经办人"+buser.GetLogin().UserName+"拒绝.";
        }
        else//同意，但未至最后一步
        {
            if (CurrentStep.HQoption > 0)//需要会签
            {
                if (progBll.IsHQComplete(appID, userID, CurrentStep))
                {
                    oaMod.CurStepNum = CurrentStep.stepNum;
                    content = oaMod.Title + "已完成会签审批,成功通过";
                    //if (oaMod.IsFreePro) oaMod.Status = status;//如果是自由流程就仍更新状态
                }
            }
            else
            {
                oaMod.CurStepNum = CurrentStep.stepNum;
                content = oaMod.Title + "已完成审批,成功通过";
            }
        }

        if (!string.IsNullOrEmpty(CurrentStep.EmailAlert))//Email与sms通知
        {
            //发送邮件包括会员组里的会员(需扩展支持会员组)
            string ids = "";
            if (!string.IsNullOrEmpty(CurrentStep.EmailGroup))
                ids += groupBll.GetUserIDByGroupIDS(CurrentStep.EmailGroup);
            ids += CurrentStep.SmsAlert;
            ids = ids.Trim(',');
            emailToUser(content, ids);
        }
        if (!string.IsNullOrEmpty(CurrentStep.SmsAlert))
        {
            smsTouser(content,CurrentStep.SmsAlert);
        }
        progBll.Insert(progMod);
        oaBll.UpdateByID(oaMod);
        //分发功能
        if (isLastStep && oaMod.Status == 99&&!oaMod.IsFreePro)
        {
            B_MisProcedure prodBll = new B_MisProcedure();
            M_MisProcedure prodMod = prodBll.SelReturnModel(oaMod.ProID);
            if (prodMod.NodeID>-1)//指定了节点，才分发
            {
                commCall.AddContentToNode(oaMod, prodMod.NodeID, OAConfig.ModelID);
            }
        }

   
    }
    // 返回当前需要进行的流程模型,如果无流程,或流已完结,返回最后一步模型
    public M_MisProLevel GetNextLevel()
    {
        //CurStepNum:指定当前进行到的步骤
        M_MisProLevel model = new M_MisProLevel();
        //获取下一级
        oaMod = oaBll.SelReturnModel(appID);//公文模型
        if (oaMod.IsFreePro)//自由流程
        {
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
                if (!freeBll.IsLastStep(oaMod, model))//如果不是最后一步,则进一位
                {
                    model = freeBll.SelByProIDAndStepNum(oaMod.ID, (oaMod.CurStepNum + 1));
                }
            }
        }
        else//固定流程(后台定义)
        {
            if (oaMod.Status == -1 || oaMod.Status == 99)//如果已经处理完成，则返回最后一步模型
            {
                model = stepBll.SelByProIDAndStepNum(oaMod.ProID, oaMod.CurStepNum);
                return model;
            }
            DataTable progDT = progBll.SelByAppID(appID.ToString());//已进行到的流程
            DataTable proLevelDT = stepBll.SelByProID(oaMod.ProID);//全部流程
            if (oaMod.CurStepNum == 0)//步骤为0时，加载第一步,默认为0    ,progDT.Rows.Count < 1||为空或(除去此判断,我们维护好CurrentStepNum)
            {
                if (proLevelDT == null || proLevelDT.Rows.Count < 1)
                {
                    function.WriteErrMsg("未设定流程,请联系管理员!!!");
                }
                model = model.GetModelFromDR(proLevelDT.Rows[0]);//用第一个填充,其值是经过Level排序的
            }
            else
            {
                model = stepBll.SelByProIDAndStepNum(oaMod.ProID, oaMod.CurStepNum);//获取上一步骤的模型
                if (!stepBll.IsLastStep(model))//如果不是最后一步,则进一位
                {
                    model = stepBll.SelByProIDAndStepNum(oaMod.ProID, (oaMod.CurStepNum + 1));
                }
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
        return progMod.GetResult(DataConvert.CLng(o.ToString())) ;
    }

    //--------------邮件等通知
    protected M_Message mailMod = new M_Message();
    protected B_Message mailBll = new B_Message();
    //步骤改变时，发送站内邮件给目标用户
    public void emailToUser(string content,string ids) 
    {
        mailMod.Title = "系统提醒";
        mailMod.MsgType = 3;
        mailMod.Content = content;//XXXX流程已进入下一步，XXXX
        mailMod.CCUser = ids;
        mailMod.Sender = "1";
        mailMod.PostDate = DateTime.Now;
        mailBll.GetInsert(mailMod);
    }
    public void smsTouser(string content,string ids) 
    {

    }

    //CC与本人查阅使用,禁掉所有权限
    public void DisAllAuth() 
    {
        opBar.Visible = false;//操作栏
        signTr.Visible = false;//签章栏
        remindTr.Visible = false;//审核意见栏
    }
    //获取ZL_User中的数据,包含组名等

    //完结自由流程，只有会签完毕才会显示
    protected void Free_Sure_Btn_Click(object sender, EventArgs e)
    {
        //判断是否真的执行完成，避免用户添加了步骤后，又点完结流程

        if (freeBll.IsLastStep(oaMod, CurrentStep))
        {
            oaMod.Status = 99;
            oaBll.UpdateByID(oaMod);
            Response.Redirect(Request.RawUrl);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(),"disAlert","alert('流程未完成,是否已指定下一步骤!!!');location=location;",true);
        }

    }
    /// <summary>
    /// 去除a数组与b数组中值相同的元素,用于OA会签,显示未会签人
    /// </summary>
    protected string RemoveRepeat(string[] a,string[] b) 
    {
        string result = "";
        for (int i = 0; i < b.Length; i++)
        {
            for (int j = 0; j < a.Length; j++)
            {
                if (a[j] == b[i] && a[j] != "")
                    a[j] = "";
            }
        }
        foreach(string s in a)
        {
            if(!string.IsNullOrEmpty(s))
            result += s + ",";
        }
        return result.TrimEnd(',');
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
            result = "<input type='text' id='SignDate_T_" + Eval("ID") + "' value='" + date + "' onfocus='WdatePicker({dateFmt:\"yyyy/MM/dd\"});' /><input type='button' value='保存' onclick='PostDate(" + Eval("ID") + ");' />";
        }
        else
        {
            result = date;
        }
        return result;
    }
}