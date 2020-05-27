using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Common;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL.Project;
using ZoomLa.Model.Project;

namespace ZoomLaCMS.Manage.AddOn.Project
{
    public partial class Projects : System.Web.UI.Page
    {
        B_Admin badmin = new B_Admin();
        B_Pro_Project ppBll = new B_Pro_Project();
        B_Pro_Type ptBll = new B_Pro_Type();
        public string Skey { get { return Skey_T.Text.Trim(); } set { Skey_T.Text = value; } }
        public string SDate { get { return STime_T.Text.Trim(); } set { STime_T.Text = value; } }
        public string EDate { get { return ETime_T.Text.Trim(); } set { ETime_T.Text = value; } }
        public int Type { get { return Request.QueryString["type"] == null ? 0 : DataConverter.CLng(Request.QueryString["type"]); } }
        public int Status { get { return Request.QueryString["status"] == null ? -100 : DataConverter.CLng(Request.QueryString["status"]); } }
        public int MyStatus { get { return DataConverter.CLng(Request.QueryString["mystatus"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Skey = Request.QueryString["key"];
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li class='active'>项目管理<a href='AddProjects.aspx'>[新增项目]</a></li>" + Call.GetHelp(42));
            }
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        private void MyBind()
        {
            DataTable dt = null;
            switch (SkeyType_DP.SelectedValue)
            {
                case "1":
                    dt = ppBll.Search(name: Skey, sdate: SDate, edate: EDate, type: Type, status: Status);
                    break;
                case "2":
                    dt = ppBll.Search(manageer: Skey, sdate: SDate, edate: EDate, type: Type, status: Status);
                    break;
                case "3":
                    dt = ppBll.Search(tecDirector: Skey, sdate: SDate, edate: EDate, type: Type, status: Status);
                    break;
            }


            EGV.DataSource = dt;
            EGV.DataBind();
        }
        //按要求搜索
        protected void BntSearch_Click(object sender, EventArgs e)
        {
            MyBind();

        }
        protected int GetManageGroup(string Leader)
        {
            if (badmin.GetAdminLogin().IsSuperAdmin(badmin.GetAdminLogin().RoleList))
            {
                return 1;
            }
            else
            {
                if (Leader == badmin.GetAdminLogin().AdminName)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        protected string GetLink(string adminname)
        {
            string keyword = Request.QueryString["TxtADName"];
            string DDList0 = Request.QueryString["DDList0"];

            return "Projects.aspx?txtPage=" + Request.QueryString["txtPage"] + "&DDList0=" + DDList0 + "&keyword=" + Server.UrlEncode(keyword) + "&pid=" + ViewState["typeid"] + "&adminname=" + Server.UrlEncode(adminname) + "&orderby=" + Request.QueryString["orderby"] + "&timess=-1&timeval=-1&tname=" + ViewState["tname"] + "&OpjectID=" + ViewState["OpjectID"];
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Audit":
                    M_Pro_Project proMod = ppBll.SelReturnModel(id);
                    if (proMod != null)
                    {
                        proMod.ZStatus = 1;
                        ppBll.UpdateByID(proMod);
                    }
                    break;
                case "canclepass":
                    function.WriteErrMsg("取消!");
                    break;
                case "manage":
                    Response.Redirect("AddUpdateProject.aspx?ID=" + e.CommandArgument);
                    break;
                case "del":
                    ppBll.Del(id);
                    break;
                case "Comments":
                    Response.Redirect("ProcessesComments.aspx?ProjectID=" + e.CommandArgument);
                    break;
                case "Run":
                    //pinfo = bpt.GetSelect(id);
                    //if (pinfo.AuditStatus != 2)
                    //{
                    //    function.Script(this, "alert('对不起！该项目还未审核通过！请审核后再进行操作！');history.back();");
                    //}
                    //else
                    //{
                    //    if (pinfo.ProStatus != 1)
                    //    {
                    //        pinfo.ProStatus = 1;
                    //        pinfo.BeginTime = DateTime.Now;
                    //        bpt.GetUpdate(pinfo);
                    //        Response.Redirect(Request.UrlReferrer.ToString());
                    //    }
                    //    else
                    //    {
                    //        pinfo.ProStatus = 0;
                    //        pinfo.BeginTime = pinfo.ApplicationTime;
                    //        bpt.GetUpdate(pinfo);
                    //        Response.Redirect(Request.UrlReferrer.ToString());
                    //    }
                    //}
                    break;
                default:
                    break;
            }
            MyBind();
        }
        //显示审核
        protected string GetAuditEdit(string audit)
        {
            if (audit == "1")
                return "通过";
            else
                return "取消";
        }
        //绑定类型
        protected string GetProType(string typeid)
        {
            int id = DataConverter.CLng(typeid);
            return ptBll.SelReturnModel(id).TName;
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected string GetLong(string id)
        {
            int i = DataConverter.CLng(id);
            //DataTable t = bps.SelectByProID(i);
            int line = 0;
            //foreach (DataRow r in t.Rows)
            //{
            //    if (r[5].ToString() == "1")
            //    {
            //        line += DataConverter.CLng(r[4].ToString());
            //    }
            //}
            if (line > 100) { line = 100; }
            string li = line.ToString();
            li += "%";
            return li;
        }
        //绑定审核
        protected string GetAudit(string Audit)
        {
            if (Audit == "0")
            {
                return "<font color=red>×</font>";
            }
            else
            {
                return "<font color=green>√</font>";
            }
        }
        //绑定是否完成
        protected string GetProStatus(string prostatus)
        {
            if (prostatus == "2")
                return "启动";
            else if (prostatus == "3")
                return "完成";
            else if (prostatus == "4")
            {
                return "存档";
            }
            else
                return "未启动";
        }
        protected bool GetEnabled(string auditstatus)
        {
            if (auditstatus == "1")
                return false;
            else
                return true;
        }
        protected bool GetBool(string prostatus)
        {
            if (prostatus == "2")
                return true;
            else
                return false;
        }
        protected string GetManageer(string id)
        {
            return B_Admin.GetAdminByID(DataConverter.CLng(id)).AdminName;
        }
        private void CVSOut()
        {
            string str = "";
            str += "项目名称 \\t 项目类型 \\t 项目价格 \\t 启动时间 \\t 项目经理 \\t 审核 \\t 项目 \\t 当前进度 \\t 申请时间 \\n";


            DataTable dt = (DataTable)ViewState["datatable"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str += dt.Rows[i]["Name"].ToString() + " \\t ";
                str += GetProType(dt.Rows[i]["TypeID"].ToString()) + " \\t ";
                str += GetManageGroup(dt.Rows[i]["Leader"].ToString() == "1" ? "￥" + dt.Rows[i]["Price"].ToString() : "******") + " \\t ";
                str += dt.Rows[i]["ProStatus"].ToString() == "0" ? "---- \\t " : dt.Rows[i]["BeginTime"].ToString() + " \\t ";
                str += GetManageer(dt.Rows[i]["Leader"].ToString()) + " \\t ";
                string audti = dt.Rows[i]["AuditStatus"].ToString();
                if (audti == "1")
                {
                    str += "× \\t ";
                }
                else
                {
                    str += "√ \\t ";
                }
                str += GetProStatus(dt.Rows[i]["ProStatus"].ToString()) + " \\t ";
                str += GetLong(dt.Rows[i]["id"].ToString()) + " \\t ";
                str += dt.Rows[i]["ApplicationTime"].ToString() == "9999-12-31 23:59:59" ? "-" : dt.Rows[i]["ApplicationTime"].ToString();
                str += "\\n";
            }
            string filesname = "";
            filesname = DateTime.Now.ToShortDateString() + "_project";
            Response.Write("<script>var winname = window.open('', '_blank', 'top=10000');winname.document.open('text/html', 'replace');winname.document.write('" + str.ToString() + "');winname.document.execCommand('saveas','','" + filesname + ".csv');winname.close();</script>");
            str = "";
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        protected void btnexcel_Click(object sender, EventArgs e)
        {
            CVSOut();
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                //bpt.DelByIds(ids);
                function.WriteSuccessMsg("删除成功！", "Projects.aspx");
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ondblclick", "javascript:location.href='ProjectsDetail.aspx?ProjectID=" + EGV.DataKeys[e.Row.RowIndex].Value + "'");//双击事件
            }
        }
    }
}