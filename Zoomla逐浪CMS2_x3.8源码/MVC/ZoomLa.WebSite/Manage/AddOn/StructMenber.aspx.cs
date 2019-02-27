using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Web;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;

namespace ZoomLaCMS.Manage.AddOn
{
    public partial class StructMenber : System.Web.UI.Page
    {
        M_AdminInfo manager = new M_AdminInfo();
        B_Structure strBll = new B_Structure();
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        M_UserInfo userMod = new M_UserInfo();
        public int StructID
        {
            get
            {
                return Convert.ToInt32(ViewState["StructID"]);
            }
            set { ViewState["StructID"] = value; }
        }
        //所有成员的id用于过滤选择
        public string UserIDS
        {
            get
            {
                return ViewState["UserIDS"] as string;
            }
            private set { ViewState["UserIDS"] = value; }
        }
        public string ManagerIDS { get { return (ViewState["ManagerIDS"] as string); } set { ViewState["ManagerIDS"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "AdminManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                StructID = DataConverter.CLng(Request.QueryString["id"]);
                curStr_L.Text = StructID == 0 ? "根结构" : strBll.SelReturnModel(StructID).Name;
                MyBind();
                Call.HideBread(Master);
                //Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='StructMenber.aspx'>组织结构</a></li><li class='active'>成员列表<a href='javascript:showdiv(\"div_share\",1);'>[选择成员]</a><a href='AddStruct.aspx?pid=" + StructID + "'>[添加组织结构]</a></li>");
            }
        }
        // 修改,变量说明：flag判断记录是否选中，chkCount选种记录数量
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "Remove":
                    strBll.AddUsers(e.CommandArgument.ToString(), StructID, "remove");
                    MyBind();
                    break;
            }
            MyBind();
        }
        // 批量删除
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                strBll.AddUsers(Request.Form["idchk"], StructID, "remove");
            }
            MyBind();
        }
        public void MyBind()
        {
            if (StructID < 1) { return; }
            M_Structure strMod = strBll.SelReturnModel(StructID);
            DataTable dt = new DataTable();
            dt = buser.SelectUserByIds(strMod.UserIDS);
            UserIDS = strMod.UserIDS;
            ManagerIDS = strMod.ManagerIDS;
            EGV.DataSource = dt;
            EGV.DataKeyNames = new string[] { "UserID" };
            EGV.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected string GetStruct(string structid)
        {
            DataTable dt = strBll.Sel(DataConverter.CLng(structid));
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Name"].ToString();
            }
            else return "";
        }
        protected string GetRole(string structid)
        {
            string[] str = structid.Split(new Char[] { ',' });
            string strs = "";
            B_Role brole = new B_Role();
            DataTable dt = new DataTable();
            for (int i = 0; i < str.Length; i++)
            {
                dt = brole.Sel(DataConverter.CLng(str[i]));
                if (dt.Rows.Count > 0)
                {
                    strs += dt.Rows[0]["RoleName"] + " ";
                }
            }
            return strs;
        }
        //选择用户后，添加进入该组,剔除掉重复的
        protected void Sure_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HiddenUser.Value))
            {
                //AddUsers(HiddenUser.Value, StructID);
                strBll.AddUsers(HiddenUser.Value, StructID);
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                function.Script(this, "alert('未选定需要添加的用户!!');");
            }
        }
        //设为部门管理员
        protected void SetM_Btn_Click(object sender, EventArgs e)
        {
            string uids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(uids))
            {
                strBll.AddManager(uids, StructID);
                Response.Redirect(Request.RawUrl);
            }
            else { function.Script(this, "alert('未选择用户');"); }
        }
        public string GetIsManager()
        {
            string uid = Eval("UserID", "{0}");
            return StrHelper.IsContain(ManagerIDS, uid) ? "<span class='rd_green'>管理员</san>" : "<span>部门成员</span>";
        }
        protected void UnSetM_Btn_Click(object sender, EventArgs e)
        {
            string uids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(uids))
            {
                strBll.AddManager(uids, StructID, "remove");
                Response.Redirect(Request.RawUrl);
            }
            else { function.Script(this, "alert('未选择用户');"); }
        }
    }
}