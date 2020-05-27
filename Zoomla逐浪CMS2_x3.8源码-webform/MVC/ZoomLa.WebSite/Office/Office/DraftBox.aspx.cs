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

/*
 * 公文草稿列表页
 */
namespace ZoomLaCMS.MIS.OA.Office
{
    public partial class DraftBox : System.Web.UI.Page
    {
        B_Mis_Model modelBll = new B_Mis_Model();
        B_OA_Document oaBll = new B_OA_Document();
        B_Mis_AppProg proBll = new B_Mis_AppProg();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            EGV.txtFunc = txtPageFunc;
            if (!IsPostBack)
            {
                DataBind();
            }
        }
        private void DataBind(string key = "")
        {
            DataTable dt = new DataTable();
            dt = oaBll.SelByUserID(buser.GetLogin().UserID);
            dt.DefaultView.RowFilter = "Status=-80";
            dt = dt.DefaultView.ToTable();
            if (!string.IsNullOrEmpty(key.Trim()))
            {
                dt.DefaultView.RowFilter = "Title Like '%" + key + "%'";
                dt = dt.DefaultView.ToTable();
            }
            EGV.DataSource = dt;
            EGV.DataBind();
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
                    int id = DataConverter.CLng(e.CommandArgument.ToString());
                    oaBll.UpdateStatus(id);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功');", true);
                    break;
                case "edit1":
                    Response.Redirect("../Drafting.aspx?Edit=1&AppID=" + e.CommandArgument.ToString());
                    break;
            }
            DataBind();
        }
        protected void searchBtn_Click(object sender, EventArgs e)
        {
            DataBind(searchText.Text.Trim());
        }
        public string GetType(string type)
        {
            DataTable dt = MiaModelDT;
            dt.DefaultView.RowFilter = "ID=" + type;
            dt = dt.DefaultView.ToTable();
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["ModelName"].ToString();
            else
                return "未定义";
        }
        public string GetSecret(string Secret)
        {
            switch (Secret)
            {
                case "0":
                    return "机密";
                case "1":
                    return "绝密";
                case "2":
                    return "秘密";
                case "3":
                    return "一般";
                default:
                    return "";
            }
        }
        public string GetImport(string importance)
        {
            switch (importance)
            {
                case "0":
                    return "较重要";
                case "1":
                    return "很重要";
                case "2":
                    return "重要";
                case "3":
                    return "非常重要";
                case "4":
                    return "一般";
                default:
                    return "";
            }
        }
        public string GetUrgency(string Urgency)
        {
            switch (Urgency)
            {
                case "0":
                    return "较紧急";
                case "1":
                    return "紧急";
                case "2":
                    return "非常紧急";
                case "3":
                    return "很紧急";
                case "4":
                    return "一般";
                default:
                    return "";
            }
        }
        public string GetStatus(string Status)
        {
            switch (Status)
            {
                case "-80":
                    return "<span style='color:Gray;'>草稿箱</span>";
                default:
                    return "";
            }
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rv = (DataRowView)e.Row.DataItem;
                if (oaBll.IsApproving(Convert.ToInt32(rv["ID"])))
                {
                    e.Row.FindControl("del").Visible = false;
                    e.Row.FindControl("edit").Visible = false;
                }
            }
        }
        public DataTable MiaModelDT
        {
            get
            {
                if (Session["MiaModelDT"] == null)
                    Session["MiaModelDT"] = modelBll.Sel();
                return Session["MiaModelDT"] as DataTable;
            }
            set
            {
                Session["MiaModelDT"] = value;
            }
        }
    }
}