using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;
using ZoomLa.Components;
using ZoomLa.Model;

/*
 * 后期需要加上权限限制,如只有创建人可修改
 * 自由流程步骤创建页,只允许创建下一步骤
 */ 
public partial class MIS_OA_Office_FreeProNext : System.Web.UI.Page
{
    protected B_User buser = new B_User();
    protected B_UserBaseField ubBll = new B_UserBaseField();
    protected B_Group groupBll = new B_Group();
    protected B_OA_FreePro freeBll = new B_OA_FreePro();
    protected M_MisProLevel freeMod = new M_MisProLevel();
    public M_OA_Document moa = new M_OA_Document();
    protected B_OA_Document boa = new B_OA_Document();
    OACommon oaCom = new OACommon();
    public int AppID
    {
        get
        {
            return Convert.ToInt32(Request.QueryString["AppID"]);
        }
    }
    //需要修改的步骤主键ID
    public int StepID
    {
        get
        {
            return DataConverter.CLng(Request.QueryString["StepID"]);
        }
    }
    public int CurStepNum
    {
        get
        {
            if (ViewState["CurStepNum"] == null)
            {
                moa = boa.SelReturnModel(AppID);
                ViewState["CurStepNum"] = moa.CurStepNum;
            }
            return Convert.ToInt32(ViewState["CurStepNum"]);
        }
        set
        {
            ViewState["CurStepNum"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged();
        if (!IsPostBack)
        {
            SafeSC.Submit_Begin(this.ViewState);
            if (string.IsNullOrEmpty(Request.QueryString["AppID"]))
            {
                function.WriteErrMsg("未指定公文ID");
            }
            else
            {
                //只有上一级经办人有权限继续指定流程
                //M_MisProLevel fMod = freeBll.GetLastStep(AppID);
                //if (!fMod.ReferUser.Contains("," + buser.GetLogin().UserID + ",")) function.WriteErrMsg("只有上一级经办人才有权限修改该流程");
                if (StepID > 0)//修改步骤
                {
                    return_Btn.Visible = true;
                    freeMod = freeBll.SelReturnModel(StepID);
                    moa = boa.SelReturnModel(AppID);
                    //-----Auth
                    if (!moa.IsFreePro) { function.WriteErrMsg("非自由流程，不允许自指定步骤"); }
                    if (freeMod.BackOption != AppID) function.WriteErrMsg("公文与步骤数据不匹配,取消访问!!!");
                    if (moa.CurStepNum >= freeMod.stepNum) function.WriteErrMsg("该步骤已执行,不允许修改!!!");
                    //-----
                    ReferUser_T.Text = buser.GetUserNameByIDS(freeMod.ReferUser);
                    ReferUser_Hid.Value = freeMod.ReferUser;
                    CCUser_T.Text = buser.GetUserNameByIDS(freeMod.CCUser);
                    CCUser_Hid.Value = freeMod.CCUser;
                }
                else
                {
                    //-----Auth2(只允许创建下一步)
                    if (freeBll.GetStep(AppID) - CurStepNum > 1)
                    {
                        Free_Div.Visible = false;
                        remind2.Visible = true;
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "disFoo", "ShowFoo();", true);
                DataBind();//绑定已有步骤
            }
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    private void DataBind(string key = "")
    {
        DataTable dt = new DataTable();
        dt = freeBll.SelDTByDocID(AppID);
        if (!string.IsNullOrEmpty(key))
        {
            dt.DefaultView.RowFilter = "StepName like '%" + key + "%'";
        }

        EGV.DataSource = dt;
        EGV.DataBind();
    }
    public string GetUserInfo(object u, object g)
    {
        string ids = u.ToString();
        if (!string.IsNullOrEmpty(g.ToString()))
            ids += groupBll.GetUserIDByGroupIDS(g.ToString());
        string[] uidArr = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        string result = "";
        switch (OAConfig.UNameConfig)
        {
            case "1":
                result = buser.GetUserNameByIDS(ids);
                break;
            case "2":
                foreach (string uid in uidArr)
                {
                    result = GetTrueName(uid, UserBaseDT) + "(" + GetHoneyName(uid, UserBaseDT) + "),";
                }
                break;
            case "3":
                foreach (string uid in uidArr)
                {
                    result = GetTrueName(uid, UserBaseDT) + ",";
                }
                break;
            case "4":
                foreach (string uid in uidArr)
                {
                    result = GetTrueName(uid, UserBaseDT) + "(" + uid + "),";
                }
                break;
        }
        return "<span title='" + result + "'>" + (result.Length > 13 ? result.Substring(0, 12) + "..." : result) + "</span>";
    }
    //HoneyName,TrueName
    public DataTable UserBaseDT
    {
        get
        {
            if (Session["UserBaseDT"] == null)
                Session["UserBaseDT"] = ubBll.SelAll();
            return Session["UserBaseDT"] as DataTable;
        }
    }
    //UserName,ID
    public DataTable UserNameDT
    {
        get
        {
            if (Session["UserNameDT"] == null)
                Session["UserNameDT"] = buser.Sel();
            return Session["UserNameDT"] as DataTable;
        }

    }
    public string GetTrueName(string uid, DataTable dt)
    {
        dt.DefaultView.RowFilter = "UserID = " + uid;
        return dt.DefaultView.ToTable().Rows.Count > 0 ? dt.DefaultView.ToTable().Rows[0]["TrueName"].ToString() : "";
    }
    public string GetHoneyName(string uid, DataTable dt)
    {
        dt.DefaultView.RowFilter = "UserID = " + uid;
        return dt.DefaultView.ToTable().Rows.Count > 0 ? dt.DefaultView.ToTable().Rows[0]["HoneyName"].ToString() : "";
    }
    public string GetUserName(string uid, DataTable dt)
    {
        dt.DefaultView.RowFilter = "UserID = " + uid;
        return dt.DefaultView.ToTable().Rows.Count > 0 ? dt.DefaultView.ToTable().Rows[0]["UserName"].ToString() : "";
    }
    public void ClearSession()
    {
        Session["UserBaseDT"] = null;
        Session["UserNameDT"] = null;
    }
    public string GetHQoption(string hQoption)
    {
        switch (hQoption)
        {
            case "0":
                return "任意一人即可";
            case "1":
                return "必须全部审核";
            default:
                return hQoption;
        }
    }
    public string GetQzzjoption(string qzzjoption)
    {
        switch (qzzjoption)
        {
            case "0":
                return "不允许";
            case "1":
                return "允许";
            default:
                return "";
        }
    }
    public string GetHToption(string hGetHToption)
    {
        switch (hGetHToption)
        {
            case "0":
                return "不允许";
            case "1":
                return "允许回退上一步骤";
            case "2":
                return "允许回退之前步骤";
            default:
                return "";
        }
    }
    public string GetEditLink(object sid,object snum) //步骤ID,步骤数
    {
        string result = "<a href='FreeProNext.aspx?AppID={0}&StepID={1}' title='修改步骤' style='color:blue;'>修改</a>";
        if (Convert.ToInt32(snum) <= CurStepNum)
            result = "<span style='color:green'>已执行</span>";
        else
            result = string.Format(result,AppID,sid);
        return result;
    }
    protected void Free_Sure_Btn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ReferUser_Hid.Value)) { function.WriteErrMsg("主办人不能为空"); }
        if (!SafeSC.Submit_Check(this.ViewState))
        {
            B_OA_Document oaBll=new B_OA_Document();
            M_OA_Document oaMod = oaBll.SelReturnModel(AppID);
            M_MisProcedure proceMod=new B_MisProcedure().SelReturnModel(oaMod.ProID);
            oaCom.CreateStep(oaMod, proceMod, new OAStepParam() { 
                ReferUser = ReferUser_Hid.Value,
                CCUser = CCUser_Hid.Value, 
                StepNum = freeBll.GetStep(AppID),
                StepID=this.StepID
            });
            DataBind();
            Free_Div.Visible = false;
            remind2.Visible = true;
            switch (Request["s"] ?? "")
            {
                case "old":
                    function.Script(this, "PaerntUrl(\"/Mis/OA/AffairsList.aspx?view=1\");");
                    break;
                default:
                    function.Script(this, "PaerntUrl(\"/Mis/OA/Flow/ApplyList.aspx?view=1\");");
                    break;
            }
          
        }
        else 
        {
            Response.Redirect(Request.RawUrl);
        }
    }
    protected void return_Btn_Click(object sender, EventArgs e)
    {
        Response.Redirect("FreeProNext.aspx?AppID="+AppID);
    }
}