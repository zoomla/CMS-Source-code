namespace ZoomLaCMS.Common.Dialog
{
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
    using ZoomLa.SQLDAL;
    using System.Xml;
    public partial class SelStructure : System.Web.UI.Page
    {
        B_Structure struBll = new B_Structure();
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        public string GroupID
        {
            get
            {
                return GroupID_H.Value as string;
            }
            set { GroupID_H.Value = value; }
        }
        //当前关键词
        public string KeyWord
        {
            get
            {
                return ViewState["KeyWord"] as string;
            }
            set { ViewState["KeyWord"] = value; }
        }
        //需要过滤的UserID,格式:1,2,3
        public string FilterID
        {
            get
            {
                return ViewState["FilterID"] as string;
            }
            private set
            {
                ViewState["FilterID"] = value;
            }
        }
        public string ManagerIDS { get { return (ViewState["ManagerIDS"] as string); } set { ViewState["ManagerIDS"] = value; } }
        /*
         * 父页面需要实现三个JS方法,详见示例页
         * 关闭与清空chk方法，必须放在父页面调用,该页面只允许实现,选择会员，并返回Json数据.
         * 清空调用本页ClearChk();
         */
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
                DataTable dt = struBll.Sel();
                if (dt.Rows.Count < 1) { function.WriteErrMsg("尚未指定任何部门"); return; }
                FilterID = Request.QueryString["fid"];
                switch (Request.QueryString["type"])
                {
                    case "null":
                        Normal_Div.Visible = true;
                        modelHtml.Text = "<ul id='GroupSel'>" + GetTable(dt, disType.Null) + "</ul>";
                        sureBtn.Visible = false;
                        break;
                    case "AllInfo":
                        AllInfo_Div.Visible = true;
                        AllInfo_Litral.Text = "<ul id='GroupSel'>" + GetTable(dt, disType.AllInfo) + "</ul>";
                        GroupID = dt.Rows[0]["ID"].ToString();
                        curLabel.Text = dt.Rows[0]["Name"].ToString();
                        MyBind();
                        break;
                    default:
                        Normal_Div.Visible = true;
                        modelHtml.Text = "<ul id='GroupSel'>" + GetTable(dt, disType.CheckBox) + "</ul>";
                        break;
                }
            }
        }
        public string GetTable(DataTable dt, disType type, int pid = 0)
        {
            string result = "";
            DataRow[] dr = dt.Select("ParentID=" + pid);
            for (int i = 0; i < dr.Length; i++)
            {
                dt.DefaultView.RowFilter = "ParentID=" + dr[i][0];
                switch (type)
                {
                    case disType.CheckBox:
                        result += "<li><input type='checkbox' onclick='checkAll(this)' myName='" + dr[i]["Name"] + "' name='selgroup' value='" + dr[i][0] + "' />{0}<a href='javascript:;' onclick='hiddenul(this)'>" + dr[i]["Name"] + "</a>";
                        break;
                    case disType.Radio:
                        result += "<li><input type='radio' onclick='checkAll(this)' myName='" + dr[i]["Name"] + "' name='selgroup' value='" + dr[i][0] + "' />{0}<a href='javascript:;' onclick='hiddenul(this)'>" + dr[i]["Name"] + "</a>";
                        break;
                    case disType.AllInfo:
                        result += "<li>{0}<a href='javascript:;' onclick='hiddenul(this);FilterTr(" + dr[i]["ID"] + ",\"" + dr[i]["Name"] + "\")'>" + dr[i]["Name"] + "</a>";
                        break;
                    case disType.Null:
                        result += "<li>{0}<a href='javascript:;' onclick='disFrame(" + dr[i][0] + ");'>" + dr[i]["Name"] + "</a>";
                        break;
                }
                if (dt.DefaultView.ToTable().Rows.Count > 0)//是否还有子组，如无，则使用其他图片
                    result = string.Format(result, "<img src='/Images/TreeLineImages/groups.gif' border='0' />");
                else
                    result = string.Format(result, "<img src='/Images/TreeLineImages/group.gif' border='0' />");

                result += "<ul style='padding-left:15px;'>" + GetTable(dt, type, Convert.ToInt32(dr[i][0])) + "</ul>";
                result += "</li>";
            }
            return result.Replace("<ul></ul>", "");
        }
        public enum disType { CheckBox, Radio, Null, AllInfo };
        private void MyBind()
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(KeyWord))
            {
                dt = buser.SearchByInfo(KeyWord);
                curLabel.Text = "关键词搜索";
            }
            else if (!string.IsNullOrEmpty(GroupID))
            {
                M_Structure strmod = struBll.SelReturnModel(DataConvert.CLng(GroupID));
                ManagerIDS = strmod.ManagerIDS;
                dt = buser.SelectUserByIds(strmod.UserIDS);
            }
            else
            {
                dt = buser.SelAll();
                curLabel.Text = "全部会员";
            }
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
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
        //按部门筛选
        protected void ReBind_Btn_Click(object sender, EventArgs e)
        {
            KeyWord = "";
            MyBind();
        }
        protected void keyBtn_Click(object sender, EventArgs e)
        {
            GroupID = "";
            KeyWord = keyText.Text.Trim();
            MyBind();
        }
        protected void showAll_Btn_Click(object sender, EventArgs e)
        {
            GroupID = "";
            KeyWord = "";
            MyBind();
        }
        public string IsChecked(string str)
        {
            if (UserInfo_H.Value.Contains(str))
                return "checked=\"checked\"";
            else
                return "";
        }
    }
}