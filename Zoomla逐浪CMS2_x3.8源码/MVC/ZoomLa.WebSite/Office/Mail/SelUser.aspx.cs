using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

/*
 * 用户信息公共页面,管理员或会员均有权使用该页面
 * AllInfo只允许输出简单非隐私的用户信息
 */

namespace ZoomLaCMS.MIS.OA.Mail
{
    public partial class SelUser : System.Web.UI.Page
    {
        private B_Group bll = new B_Group();
        private B_User buser = new B_User();
        private B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (badmin.CheckLogin() || buser.CheckLogin())
            {

            }
            else
            {
                function.WriteErrMsg("无权访问该页面"); return;
            }

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["Type"]))
                {
                    CheckDiv.Visible = true;
                }
                if (Request.QueryString["Type"] == "Radio")
                {
                    RadioDiv.Visible = true;
                }
                else if (Request.QueryString["Type"] == "AllInfo")
                {
                    AllInfo_Div.Visible = true;
                }
                BindData();
            }
        }
        private void BindData(string key = "")
        {
            DataTable dt = new DataTable();
            key = "'%" + key + "%'";
            switch (Request.QueryString["Type"])
            {
                case "Radio":
                    dt = bll.Select_All();
                    if (!string.IsNullOrEmpty(key))
                    {
                        dt.DefaultView.RowFilter = "GroupName Like " + key;
                        dt = dt.DefaultView.ToTable();
                    }
                    Repeater3.DataSource = dt;
                    Repeater3.DataBind();
                    break;
                case "AllInfo"://未完成
                    dt = bll.Select_All();
                    if (!string.IsNullOrEmpty(key))
                    {
                        dt.DefaultView.RowFilter = "GroupName Like " + key;
                        dt = dt.DefaultView.ToTable();
                    }
                    AllInfo_Rep.DataSource = dt;
                    AllInfo_Rep.DataBind();
                    break;
                default:
                    dt = bll.Select_All();
                    if (!string.IsNullOrEmpty(key))
                    {
                        dt.DefaultView.RowFilter = "GroupName Like " + key;
                        dt = dt.DefaultView.ToTable();
                    }
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                    break;
            }
            UserDT = null;
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rep = e.Item.FindControl("Repeater2") as Repeater;
                DataRowView rowv = (DataRowView)e.Item.DataItem;
                DataTable dt = UserDT;
                int GroupID = DataConverter.CLng(rowv["GroupID"]);
                if (!string.IsNullOrEmpty(Request.QueryString["UID"]))
                {
                    dt.DefaultView.RowFilter = "GroupID=" + GroupID + " and userid not in(" + Request.QueryString["UID"] + ")";
                    dt = dt.DefaultView.ToTable();
                }
                else
                {
                    dt.DefaultView.RowFilter = "GroupID=" + GroupID;
                    dt = dt.DefaultView.ToTable();
                }
                if (!string.IsNullOrEmpty(searchT.Text))
                {
                    dt.DefaultView.RowFilter = "username like '%" + searchT.Text + "%' or userid ='" + DataConverter.CLng(searchT.Text) + "'";
                    dt = dt.DefaultView.ToTable();
                }
                rep.DataSource = dt;
                rep.DataBind();
            }
        }
        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            BindData();
        }
        public DataTable UserDT
        {
            get
            {
                if (Session["UserDT"] == null)
                {
                    Session["UserDT"] = buser.SelAll();
                }
                return Session["UserDT"] as DataTable;
            }
            set
            {
                Session["UserDT"] = value;
            }
        }
    }
}