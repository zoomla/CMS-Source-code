using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.AddOn
{
    public partial class StructList : System.Web.UI.Page
    {
        B_Structure bll = new B_Structure();
        DataTable dt = new DataTable();
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string action = Request.Form["action"];
                string result = "";
                switch (action)
                {
                    case "getchild"://获取字节点
                        DataTable dt = bll.SelByPid(DataConvert.CLng(Request.Form["pid"]));
                        dt.Columns.Add("UserCount");
                        dt.Columns.Add("UserName");
                        dt.Columns.Add("StatusStr");
                        dt.Columns.Add("OpensStr");
                        foreach (DataRow item in dt.Rows)
                        {
                            item["UserName"] = getName(item["UserID"].ToString());
                            item["OpensStr"] = getOpen(item["Opens"].ToString());
                            item["StatusStr"] = getStatus(item["Status"].ToString());
                            item["UserCount"] = GetCount(item["ID"]);
                        }
                        result = JsonHelper.JsonSerialDataTable(dt);
                        break;
                    case "del":
                        bll.Del(DataConvert.CLng(Request.Form["id"]));
                        result = "1";
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
            }
            if (!IsPostBack)
            {
                PID = DataConvert.CLng(Request.QueryString["pid"]);
                if (PID == 0)
                {
                    curStr_L.Text = "根结构";
                }
                else
                {
                    curStr_L.Text = bll.SelReturnModel(PID).Name;
                }
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='StructList.aspx'>组织结构</a></li><li class='active'>" + curStr_L.Text + "<a href='AddStruct.aspx?type=0' style='margin-left:5px;'>[添加部门]</a></li>");
        }
        public string GetName()
        {
            string name = Eval("Name").ToString();
            return name.Length > 12 ? name.Substring(0, 12) + ".." : name;
        }
        public int PID
        {
            get { return DataConvert.CLng(ViewState["PID"]); }
            set { ViewState["PID"] = value; }
        }
        public new void DataBind()
        {
            DataTable dt = new DataTable();
            //if (!string.IsNullOrEmpty(SearchKey))
            //{
            //    dt.DefaultView.RowFilter = "Title Like '%" + key + "%'";
            //    dt = dt.DefaultView.ToTable();
            //}
            dt = bll.SelByPid(PID);
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
            int id = DataConverter.CLng(e.CommandArgument);

            if (e.CommandName == "Del")
            {
                bll.Del(id);
                function.WriteSuccessMsg("删除成功！", "StructManage.aspx?type=" + Request["type"]);
            }
            if (e.CommandName == "Edit")
            {
                Response.Redirect("AddStruct.aspx?ID=" + id + "&type=" + Request["type"]);
            }
            if (e.CommandName == "Add")
            {
                Response.Redirect("AddStruct.aspx?pid=" + id + "&type=" + Request["type"]);
            }
            if (e.CommandName == "SubList")
            {
                Response.Redirect("StructList.aspx?pid=" + id + "&type=" + Request["type"]);
            }
            if (e.CommandName == "View")
            {
                Response.Redirect("StructManage.aspx?pid=" + id + "&type=" + Request["type"]);
            }
        }
        protected string getGroup(string gid)
        {
            if (gid == "0")
                return "管理员";
            else
                return "会员";

        }
        public string GetIcon()
        {
            int count = DataConvert.CLng(Eval("childCount"));
            return count > 0 ? "<span class='icon fa fa-plus-square'></span>" : "";
        }
        protected string getName(string gid)
        {
            string UserName = "";
            if (Request["type"] == "1")
            {
                B_User buser = new B_User();
                UserName = buser.GetSelect(DataConverter.CLng(gid)).UserName;
                return UserName;
            }
            else {

                UserName = B_Admin.GetAdminByID(DataConverter.CLng(gid)).AdminName;
                return UserName;
            }
        }
        protected string getOpen(string gid)
        {
            if (gid == "0")
                return "×";
            else
                return "√";

        }
        protected string getStatus(string gid)
        {
            if (gid.Equals("0"))
                return "<span style='color:red;'>禁用</span>";
            else return "<span style='color:green;'>启用</span>";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["idChk"]))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('未选定成员!!');", true);
            }
            else
            {
                string[] deeds = Request.Form["idChk"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int s = 0; s < deeds.Length; s++)
                {
                    int dsd = DataConverter.CLng(deeds[s]);
                    bll.Del(dsd);
                }
                function.WriteSuccessMsg("批量删除成功！", "StructList.aspx?type=" + Request["type"]);
            }
        }
        private DataTable StrDT
        {
            get
            {
                if (ViewState["StrDT"] == null)
                {
                    ViewState["StrDT"] = bll.Sel();
                }
                return ViewState["StrDT"] as DataTable;
            }
            set
            {
                StrDT = value;
            }
        }
        public string GetIcon(object gid, object parentID)
        {
            string result = "";
            int depth = GetDepth(parentID.ToString());//获取当前深度
            int pid = DataConvert.CLng(parentID.ToString());
            DataView dv = StrDT.DefaultView;
            for (int i = 0; i < depth; i++)
            {
                result += "<img src='/Images/TreeLineImages/tree_line4.gif' border='0' width='19' height='20' />";
            }
            result += "<img src='/Images/TreeLineImages/t.gif' border='0' />";
            dv.RowFilter = "ParentID=" + gid;
            if (dv.Count > 0)
            {
                result += "<img src='/Images/TreeLineImages/groups.gif' border='0' />";
            }
            else
            {
                result += "<img src='/Images/TreeLineImages/group.gif' border='0' />";
            }
            StrDT.DefaultView.RowFilter = "";
            return result;
        }
        public int GetDepth(string ParentID)
        {
            int pid = DataConvert.CLng(ParentID);
            int depth = 0;
            for (int i = 0; pid > 0; i++)
            {
                DataRow[] dr = StrDT.Select("ID=" + pid);
                if (dr != null && dr.Length > 0)
                {
                    pid = DataConvert.CLng(dr[0]["ParentID"]);
                    depth++;
                }
            }
            return depth;
        }
        //该结构下成员数
        public string GetCount(object id)
        {
            int sid = Convert.ToInt32(id);
            return bll.GetCount(sid).ToString();
        }
        public string IsHasChild(object sid)
        {
            string result = "href='StructList.aspx?pid=" + sid + "'";
            DataRow[] dr = StrDT.Select("ParentID=" + sid);
            if (dr == null || dr.Length < 1)
            {
                result = "href='javascript:;' style='color:gray;'";
            }
            return result;
        }

    }
}