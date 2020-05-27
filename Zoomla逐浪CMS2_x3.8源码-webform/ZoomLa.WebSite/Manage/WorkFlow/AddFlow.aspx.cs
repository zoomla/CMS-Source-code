using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Manage_WorkFlow_AddFlow : System.Web.UI.Page
{
    protected B_UserBaseField ubBll = new B_UserBaseField();
    protected B_User buser = new B_User();
    protected B_Group groupBll = new B_Group();
    protected M_MisProcedure proMod = new M_MisProcedure();
    protected B_MisProcedure proBll = new B_MisProcedure();
    protected B_MisProLevel stepBll = new B_MisProLevel();
    protected M_MisProLevel stepMod = new M_MisProLevel();
    public int ProID { get { return DataConvert.CLng(Request.QueryString["ProID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string ids = Request.Form["IDList"];
            string str = UpdateOrder(ids);
            Response.Write(str);
            Response.End();
        }
        if (ProID < 1){ function.WriteErrMsg("请先选定流程!!!"); }
        if (!IsPostBack)
        {
            DataBind();
            ClearSession();
        }
        Call.HideBread(this.Master);
    }
    private void DataBind(string key = "")
    {
        //stepCodeT.Text = GenStepCode(proID).ToString();
        proMod = proBll.SelReturnModel(ProID);
        ProceName_L.Text = proMod.ProcedureName;
        DataTable dt = new DataTable();
        dt = stepBll.SelByProID(ProID);
        if (!string.IsNullOrEmpty(key))
        {
            dt.DefaultView.RowFilter = "StepName like '%" + key + "%'";
        }
        if (dt.Rows.Count > 1) orderBtn.Visible = true;
        EGV.DataSource = dt;
        EGV.DataBind();
        ImgData_Hid.Value=JsonHelper.JsonSerialDataTable(dt);
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
                int id = DataConvert.CLng(e.CommandArgument.ToString());
                stepBll.Del(id);
                stepBll.UpdateStep(DataConvert.CLng(ProID));
                break;
            case "upmove":
                function.WriteErrMsg(EGV.Rows[DataConvert.CLng(e.CommandArgument)].Cells[0].Text);
                break;
        }
        DataBind();
    }
    protected string UpdateOrder(string Ids)
    {
        string[] idsArr = Ids.Split(',');
        for (int i = 0; i < idsArr.Length; i++)
        {
            stepMod = stepBll.SelReturnModel(DataConvert.CLng(idsArr[i]));
            stepMod.stepNum = i + 1;
            stepBll.UpdateByID(stepMod);
        }
        return "1";
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes["class"] = "tdbg";
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["ondblclick"] = String.Format("javascript:location.href='AddFlowStep.aspx?proID={0}&StepID={1}'", ProID, this.EGV.DataKeys[e.Row.RowIndex].Value.ToString());
            e.Row.Attributes["onmouseover"] = "this.className='tdbgmouseover'";
            e.Row.Attributes["onmouseout"] = "this.className='tdbg'";
            e.Row.Attributes["style"] = "cursor:pointer;";
            e.Row.Attributes["title"] = "双击修改";
        }
    }
    public string GetUserInfo(object u,object g)
    {
        string ids = u.ToString();
        if (!string.IsNullOrEmpty(g.ToString()))
            ids += groupBll.GetUserIDByGroupIDS(g.ToString().Trim(','));
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
            default :
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
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        DataBind(searchText.Text);
    }
}