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
 * 公文列表页，待办事项,显示抄送人,或经办人是自己的
 */
namespace ZoomLaCMS.MIS.OA.Office
{
    public partial class Default : System.Web.UI.Page
    {
        B_Mis_Model modelBll = new B_Mis_Model();
        B_OA_Document oaBll = new B_OA_Document();
        B_Mis_AppProg proBll = new B_Mis_AppProg();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
        }
        private void DataBind(string keys = "")
        {
            DataTable dt = new DataTable();
            dt = oaBll.SelByUserID(buser.GetLogin().UserID);
            string key = SearchKey;
            if (!string.IsNullOrEmpty(key))
            {
                dt.DefaultView.RowFilter = "Title Like '%" + key + "%'";
                dt = dt.DefaultView.ToTable();
            }
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        private string SearchKey
        {
            get
            {
                return ViewState["SearchKey"] as string;
            }
            set
            {
                ViewState["SearchKey"] = value;
            }

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
                case "view":
                    Response.Redirect("ReadOffice.aspx?AppID=" + e.CommandArgument.ToString());
                    break;
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
            SearchKey = searchText.Text.Trim();
            DataBind();
        }
        public string GetType(string type)
        {
            string result = "公文";
            if (type.Equals("1"))
            {
                result = "事务";
            }
            return result;
            //DataTable dt = MiaModelDT;
            //dt.DefaultView.RowFilter = "ID=" + type;
            //dt = dt.DefaultView.ToTable();
            //if (dt.Rows.Count > 0)
            //    return dt.Rows[0]["ModelName"].ToString();
            //else
            //    return "未定义";
        }
        public string GetSecret(string Secret)
        {
            //return OAConfig.StrToDic(OAConfig.Secret)[Secret];
            return "";
        }
        public string GetImport(string importance)
        {
            //return OAConfig.StrToDic(OAConfig.Importance)[importance];
            return "";
        }
        public string GetUrgency(string Urgency)
        {
            //return OAConfig.StrToDic(OAConfig.Urgency)[Urgency];
            return "";
        }
        public string GetStatus(string Status)
        {
            string flag = "";
            switch (Status)
            {
                case "-1":
                    flag = "<span style='color:red;'>不同意</span>";
                    break;
                case "2":
                    flag = "<span style='color:green;'>进行中</span>";
                    break;
                case "98":
                    flag = "<span style='color:blue;'>进行中</span>";
                    break;
                case "99":
                    flag = "<span style='color:blue;'>已完成</span>";
                    break;
                case "0":
                    flag = "<span style='color:green;'>进行中</span>";
                    break;
                default:
                    flag = "<span style='color:blue;'>进行中</span>";
                    break;
            }
            return flag;
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
                if (rv["Status"].ToString() == "-80")
                {
                    e.Row.FindControl("view").Visible = false;
                }
                if (rv["Status"].ToString() == "-99")
                {
                    e.Row.FindControl("del").Visible = false;
                    e.Row.FindControl("view").Visible = false;
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