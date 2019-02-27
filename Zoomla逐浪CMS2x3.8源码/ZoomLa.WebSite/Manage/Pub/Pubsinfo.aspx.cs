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
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL.Content;
using ZoomLa.Model.Content;

public partial class Manage_I_Pub_Pubsinfo : CustomerPageAction
{
    private B_User buser = new B_User();
    private B_ModelField bfield = new B_ModelField();
    private B_Model bmodel = new B_Model();
    private int m_type;
    private B_Pub pub = new B_Pub();
    public int PubID { get { return DataConverter.CLng(Request.QueryString["Pubid"]); } }
    public string PubName { get { return ViewState["PubName"] as string; } set { ViewState["PubName"] = value; } }
    public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        if (!IsPostBack)
        {
            M_Pub pubinfo = pub.GetSelect(PubID);
            //string prowinfo = B_Role.GetPowerInfoByIDs(badmin.GetAdminLogin().RoleList);
            //if (!badmin.GetAdminLogin().RoleList.Contains(",1,") && !prowinfo.Contains("," + pubinfo.PubTableName + ","))
            //{
            //    function.WriteErrMsg("无权限管理该互动信息!!");
            //}
            string type = (Request.QueryString["type"] == null) ? "0" : Request.QueryString["type"].ToString();
            //----------------------
            PubName = pubinfo.PubName;
            string ModelID = (pubinfo.PubModelID == 0) ? "0" : pubinfo.PubModelID.ToString();
            M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(ModelID));
            this.HiddenNode.Value = "";
            if (NodeID > 0)
            {
                this.HiddenNode.Value = "&nodeid=" + NodeID;
            }
            this.HdnModelID.Value = ModelID.ToString();
            this.HiddenType.Value = type;
            this.ViewState["ModelID"] = ModelID.ToString();
            this.ViewState["cType"] = "1";
            DataBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Content/ContentManage.aspx'>内容管理</a></li><li><a href='"+Request.RawUrl+"'>互动模块管理</a></li><li class='active'>[" + PubName + "]互动信息</li>");
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.Egv.PageIndex = e.NewPageIndex;
        DataBind();
    }

    public string shenhe(string shen)
    {
        if (shen == "1")
            return "<span style='color:red'>已审核</span>";
        else
            return "未审核";
    }

    protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            Page.Response.Redirect("PubView.aspx?ID=" + e.CommandArgument.ToString() + "&Pubid=" + PubID + this.HiddenNode.Value);
        }
        if (e.CommandName == "Audit")
        {
            string Id = e.CommandArgument.ToString();
            int modeid = DataConverter.CLng(this.ViewState["ModelID"].ToString());
            M_ModelInfo aa = bmodel.GetModelById(modeid);
        }
        if (e.CommandName == "Del")
        {
            string Id = e.CommandArgument.ToString();
            buser.DelModelInfo(bmodel.GetModelById(DataConverter.CLng(this.ViewState["ModelID"].ToString())).TableName, DataConverter.CLng(Id));
        }
        if (e.CommandName == "Edit")
            Page.Response.Redirect("EditPub.aspx?ID=" + e.CommandArgument.ToString() + "&small=pubs&Pubid=" + PubID+ this.HiddenNode.Value);
        DataBind();
    }

    protected void Egv_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = e.Row.Cells[1].Text;
        }
    }
      
    protected void Button1_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(ids))
        {
            foreach (string id in ids.Split(','))
            {
                buser.DelModelInfo(bmodel.GetModelById(DataConverter.CLng(this.ViewState["ModelID"].ToString())).TableName, Convert.ToInt32(id));
            }
            DataBind();
        }
        else
        {
            function.Script(this, "alert('未选择信息!');");
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(ids))
        {
            foreach (string id in ids.Split(','))
            {
                buser.DelModelInfo2(bmodel.GetModelById(DataConverter.CLng(this.ViewState["ModelID"].ToString())).TableName, Convert.ToInt32(id), 12);
            }
            DataBind();
        }
        else
        {
            function.Script(this, "alert('未选择信息!');");
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(ids))
        {
            foreach (string id in ids.Split(','))
            {
                buser.DelModelInfo2(bmodel.GetModelById(DataConverter.CLng(this.ViewState["ModelID"].ToString())).TableName, Convert.ToInt32(id), 13);
            }
            DataBind();
        }
        else
        {
            function.Script(this, "alert('未选择信息!');");
        }
    }

    public string GetCount(string id)
    {
        M_Pub pubinfo = pub.GetSelect(PubID);
        string ModelID = (pubinfo.PubModelID == 0) ? "0" : pubinfo.PubModelID.ToString();
        M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(ModelID));
        DataTable UserData = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(id), 13);
        if (UserData != null)
        {
            return UserData.Rows.Count.ToString() + "条";
        }
        else
        {
            return "0条";
        }
    }
    private DataTable GetDT()
    {
        this.ViewState["cType"] = Request.QueryString["type"];
        this.m_type = DataConverter.CLng(this.ViewState["cType"].ToString());
        DataTable UserData;
        M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(this.ViewState["ModelID"].ToString()));
        //如果绑定错误模型,则不查询
        if (model.ModelType!=7) { return new DataTable(); }
        switch (m_type)
        {
            case 1:
                UserData = buser.GetUserModeInfo(model.TableName, 9, 111);
                break;
            case 2:
                UserData = buser.GetUserModeInfo(model.TableName, 9, 15);
                break;
            case 3:
                UserData = buser.GetUserModeInfo(model.TableName, 9, 14);
                break;
            default:
                UserData = buser.GetUserModeInfo(model.TableName, 9, 111);
                break;
        }
        return UserData;
    }
    private void DataBind(string key="")
    {
        Egv.DataSource = GetDT();
        Egv.DataBind();
    }
    public string GetPubTitle(object o) 
    {
        if (string.IsNullOrEmpty(o.ToString()))
        {
            o = ViewState["PubName"] + "(无标题)";
        }
        return o.ToString();
    }
    private string[] GetChecked()
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            string[] chkArr = Request.Form["idchk"].Split(',');
            return chkArr;
        }
        else
            return null;
    }
    protected void DownExcel_Btn_Click(object sender, EventArgs e)
    {
        M_Pub_Excel excelMod = new M_Pub_Excel();
        B_Pub_Excel excelBll = new B_Pub_Excel();
        M_Pub pubinfo = pub.GetSelect(PubID);
        excelMod = excelBll.SelByTbName(pubinfo.PubTableName);
        if (excelMod == null) { function.WriteErrMsg("尚未为表：" + pubinfo.PubTableName + "指定导出规则,请先<a href='PubExcel.aspx'>点此设定导出规则</a>"); }
        OfficeHelper ofHelper = new OfficeHelper();
        SafeSC.DownStr(ofHelper.GetExcelByDT(GetDT(), excelMod.Fields, excelMod.CNames), "互动信息.xls");
    }
}