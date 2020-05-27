using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

/*
 * 流程列表
 */

namespace ZoomLaCMS.Manage.WorkFlow
{
    public partial class Default : System.Web.UI.Page
    {

        B_MisType typeBll = new B_MisType();
        M_MisType typeMod = new M_MisType();
        B_MisProcedure prodBll = new B_MisProcedure();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Pname"]))
                    DataBind(Request.QueryString["Pname"]);
                else
                    DataBind();
            }
            Call.HideBread(this.Master);
        }
        private void DataBind(string key = "")
        {
            DataTable dt = new DataTable();
            dt = prodBll.Sel();
            if (!string.IsNullOrEmpty(key))
            {
                dt.DefaultView.RowFilter = "ProcedureName like '%" + key + "%'";
            }
            EGV.DataSource = dt;
            EGV.DataBind();
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
                    prodBll.Del(DataConverter.CLng(e.CommandArgument.ToString()));
                    break;
            }
            DataBind();
        }
        protected void searchBtn_Click(object sender, EventArgs e)
        {
            DataBind(searchText.Text.Trim());
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes["class"] = "tdbg";
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = String.Format("javascript:location.href='AddFlow.aspx?proID={0}'", this.EGV.DataKeys[e.Row.RowIndex].Value.ToString());
                e.Row.Attributes["onmouseover"] = "this.className='tdbgmouseover'";
                e.Row.Attributes["onmouseout"] = "this.className='tdbg'";
                e.Row.Attributes["style"] = "cursor:pointer;";
                e.Row.Attributes["title"] = "双击修改";
            }
        }
        public string GetClassID(string classID)
        {
            DataTable dt = MisTypeDT;
            dt.DefaultView.RowFilter = "ID = " + classID;
            return dt.DefaultView.ToTable().Rows.Count > 0 ? dt.DefaultView.ToTable().Rows[0]["TypeName"].ToString() : "";
        }
        public DataTable MisTypeDT
        {
            get
            {
                if (ViewState["MisTypeDT"] == null)
                    ViewState["MisTypeDT"] = typeBll.Sels();
                return ViewState["MisTypeDT"] as DataTable;
            }
        }
        public string GetAllowAttach(string allowAttach)
        {
            switch (allowAttach)
            {
                case "0":
                    return "不允许";
                case "1":
                    return "允许";
                default:
                    return "";
            }
        }
    }
}