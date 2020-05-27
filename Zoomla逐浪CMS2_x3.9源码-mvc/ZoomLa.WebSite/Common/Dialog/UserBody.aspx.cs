namespace ZoomLaCMS.Common.Dialog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.BLL;
    using System.Data;
    using ZoomLa.SQLDAL.SQL;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using Newtonsoft.Json.Linq;
    public partial class UserBody : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_User_Plat platBll = new B_User_Plat();
        B_Structure struBll = new B_Structure();

        public string PreValue { get { return function.Decode(Request.QueryString["skey"] ?? ""); } }
        public int PSize { get { return DataConverter.CLng(Request.QueryString["psize"]); } }
        public int PIndex { get { return DataConverter.CLng(Request.QueryString["pindex"]); } }
        public int GroupID { get { return DataConverter.CLng(Request.QueryString["groupid"]); } }
        public string Char { get { return Request.QueryString["char"] ?? ""; } }
        public string Source { get { return Request.QueryString["source"] ?? "user"; } }
        public string Config { get { return Request.QueryString["config"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            switch (Source)
            {
                case "plat"://能力中心
                    LoadPlatUser();
                    break;
                case "oa"://oa组织结构
                    LoadStructList();
                    break;
                default://普通用户查询
                    LoadUserList();
                    break;
            }
        }
        public void LoadStructList()
        {
            DataTable dt = new DataTable();
            if (GroupID > 0)
            {
                M_Structure strMod = struBll.SelReturnModel(GroupID);
                dt = buser.SelectUserByIds(strMod.UserIDS);
                User_RPT.DataSource = dt;
                User_RPT.DataBind();
                return;
            }
            LoadUserList();

        }
        //加载用户
        public void LoadUserList()
        {
            //int outcount = 0;
            string action = "name";
            string value = PreValue;
            if (GroupID > 0)
            {
                action = "groupid";
                value = GroupID.ToString();
            }
            //DataTable dt = buser.SelPage(PSize, PIndex, out outcount, action, value , "AscID", Char, 0);
            DataTable dt = Sel(PSize, PIndex, action, value);
            User_RPT.DataSource = dt;
            User_RPT.DataBind();
        }
        //加载能力中心用户
        public void LoadPlatUser()
        {
            //暂时禁用分页
            if (PIndex > 1) { return; }
            M_User_Plat platmu = B_User_Plat.GetLogin();
            DataTable dt = new DataTable();
            if (GroupID > 0)//按部门查询
            {
                dt = platBll.SelByGroup(platmu.CompID, GroupID);
            }
            else
            {
                dt = platBll.SelByCompany(platmu.CompID, PreValue);
            }
            UserPlat_RPT.DataSource = dt;
            UserPlat_RPT.DataBind();
        }
        public string GetHeadIcon()
        {
            return string.IsNullOrEmpty(Eval("salt").ToString()) ? "/images/userface/noface.png" : Eval("salt").ToString();
        }
        public string GetName()
        {
            string honeyName = Eval("HoneyName").ToString();
            string userName = Eval("UserName").ToString();
            string comp = Eval("CompName", "");
            string result = userName;
            if (!string.IsNullOrEmpty(honeyName)) { result += "(" + honeyName + ")"; }
            if (!string.IsNullOrEmpty(comp)) { result = "<span style='color:#0094ff;'>[" + comp + "]</span>" + result; }
            return result;
        }
        private DataTable Sel(int psize, int cpage, string action, string value)
        {
            List<SqlParameter> spList = new List<SqlParameter>();
            string where = "A.Status=0 ";
            JObject config = GetConfig();
            if (config != null)
            {
                //不包含已经升级为能力中心的用户
                if (DataConvert.CLng(config["noplat"]) == 1) { where += " AND B.CompID IS NULL"; }
            }
            string stable = "(SELECT A.*,B.GroupName FROM ZL_User A LEFT JOIN ZL_Group B ON A.GroupID=B.GroupID)";
            string mtable = "(SELECT A.UserID,A.CompID,B.CompName FROM ZL_User_Plat A LEFT JOIN ZL_Plat_Comp B ON A.CompID=B.ID)";
            if (!string.IsNullOrEmpty(value))
            {
                switch (action)//筛选条件
                {
                    case "groupid":
                        value = DataConvert.CLng(value).ToString();
                        where += " AND A.GroupID=@value";
                        break;
                    case "name":
                        where += " AND (A.UserName LIKE @uname OR A.HoneyName LIKE @uname)";
                        break;
                }
            }
            if (where.Contains("@uname")) { spList.Add(new SqlParameter("uname", "%" + value + "%")); }
            if (where.Contains("@value")) { spList.Add(new SqlParameter("value", value)); }
            PageSetting setting = new PageSetting()
            {
                pk = "A.UserID",
                fields = "A.*,B.CompID,B.CompName",
                t1 = stable,
                t2 = mtable,
                psize = psize,
                cpage = cpage,
                on = "A.UserID=B.UserID",
                where = where,
                order = "A.UserID DESC",
                sp = spList.ToArray(),
            };
            return DBCenter.SelPage(setting);
        }
        private JObject GetConfig()
        {
            if (string.IsNullOrEmpty(Config) || Config.Equals("undefined")) { return null; }
            JObject jobj = new JObject();
            foreach (string conf in Config.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                string name = conf.Split('|')[0];
                string value = conf.Split('|')[1];
                jobj.Add(name, value);
            }
            return jobj;
        }
    }
}