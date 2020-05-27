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
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text;
using ZoomLa.BLL.Page;
using ZoomLa.Model.Page;
using System.Data.SqlClient;

namespace ZoomLaCMS.Manage.Page
{
    public partial class PageTemplate : CustomerPageAction
    {
        protected B_Admin badmin = new B_Admin();
        protected B_Content cll = new B_Content();
        protected B_Templata tll = new B_Templata();
        protected B_ModelField mll = new B_ModelField();
        protected B_PageStyle sll = new B_PageStyle();
        protected B_PageReg regBll = new B_PageReg();
        protected int dp = -2, tempStyleid;
        public int UserID
        {
            get
            {
                return Convert.ToInt32(ViewState["UserID"]);
            }
            set { ViewState["UserID"] = value; }
        }
        //样式ID
        public int StyleID { get { return DataConverter.CLng(Request.QueryString["StyleID"]); } }
        //PageRegID
        private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        private string Menu { get { return (Request.QueryString["menu"] ?? "").ToLower(); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (StyleID < 1) { function.WriteErrMsg("Style参数错误,用户尚未绑定样式"); }
            if (!IsPostBack)
            {
                DPBind();
                UserID = DataConverter.CLng(Request.QueryString["UserID"]);
                if (Menu.Equals("del"))
                {
                    int did = DataConverter.CLng(Request.QueryString["did"]);
                    if (did > 0)
                    {
                        delnodes(did);
                    }
                    Response.Redirect("PageTemplate.aspx?StyleID=" + StyleID);
                }
                //----------------------------------
                if (StyleID > 0)
                {
                    if (!B_ARoleAuth.Check(ZLEnum.Auth.page, "AddPageStyle"))
                    {
                        function.WriteErrMsg("没有权限进行此项操作");
                    }
                    String styleName = sll.Getpagestrylebyid(StyleID).PageNodeName;
                    StyleName_L.Text = styleName;
                    Label1.Text = "<a href=\"PageStyle.aspx\">黄页样式管理</a> &gt;&gt; <a href=\"PageTemplate.aspx?styleid=" + this.StyleID + "\">样式栏目设置</a> &gt;&gt; " + styleName;
                    addtree.Text = "<a href=\"AddPageTemplate.aspx?styleid=" + StyleID + "\">添加黄页栏目</a>";
                }
                if (Mid > 0)
                {
                    M_PageReg regMod = regBll.SelReturnModel(Mid);
                    string tablename = regMod.TableName;
                    DataTable temptable = mll.SelectTableName(tablename, "UserName =@uname", new SqlParameter[] { new SqlParameter("uname", regMod.UserName) });
                    this.tempStyleid = DataConverter.CLng(temptable.Rows[0]["Styleid"]);
                    UserID = regMod.UserID;
                    Label1.Text = "黄页管理 &gt;&gt; <a href=\"PageManage.aspx\">黄页用户列表</a> &gt;&gt; <a href=\"PageTemplate.aspx?id=" + Mid + "\">" + regMod.UserName + "</a> &gt;&gt; ";
                    addtree.Text = "<a href=\"AddPageTemplate.aspx?id=" + Mid + "\">添加自定义用户黄页栏目</a>";
                }
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='PageManage.aspx'>企业黄页</a><li><a href='PageStyle.aspx'>黄页样式</a></li></li><li class='active'>黄页栏目<a href='AddPageTemplate.aspx?styleid=" + StyleID + "'>[添加栏目]</a></li>");
            }
        }
        public void MyBind()
        {
            DataTable templist = tll.SelByStyleAndPid(StyleID);
            templist.DefaultView.RowFilter = "ParentID=0";
            templist.DefaultView.Sort = "OrderID ASC";
            templist = templist.DefaultView.ToTable();
            Temptable.DataSource = templist;
            Temptable.DataBind();
            //--------------------------
            if (this.StyleID > 0)//系统样式,不显示模板选择
            {
                updatetemplate.Visible = false;
            }
            else if (Mid > 0)//用户栏目
            {
                updatetemplate.Visible = true;
                M_PageReg regMod = regBll.SelReturnModel(Mid);
                templateUrl.Value = regMod.Template;
            }
            else
            {
                updatetemplate.Visible = false;
            }
        }
        private void DPBind()
        {
            TempList_RPT.DataSource = FileSystemObject.GetDTForTemplate();
            TempList_RPT.DataBind();
        }
        public string getdelbotton(string templataid)
        {
            if (StyleID > 0)
            {
                return "　<a href=\"pageTemplate.aspx?menu=del&did=" + templataid + "&styleid=" + StyleID + "\" OnClick=\"return confirm('不可恢复性删除数据,你确定将该数据删除吗？');\">删除</a> | <a href=\"javascript:;\" onclick=\"ShowOrder(" + templataid + ")\">节点排序</a>";
            }
            else
            {
                return "　<a href=\"pageTemplate.aspx?menu=del&did=" + templataid + "&id=" + Mid + "\" OnClick=\"return confirm('不可恢复性删除数据,你确定将该数据删除吗？');\">删除</a> | <a href=\"javascript:;\" onclick=\"ShowOrder(" + templataid + ")\">节点排序</a>";
            }
        }

        public string getmodefy(string templateID)
        {
            return "|<a href=\"AddPageTemplate.aspx?sid=" + templateID + "&userid=" + UserID + "&StyleID=" + StyleID + "\">修改</a>";
        }

        public string gettempname(string templateID)
        {
            int templateIDs = DataConverter.CLng(templateID);
            M_Templata tts = tll.Getbyid(templateIDs);
            string dddd = "";
            if (tts.UserID > 0)
            {
                if (tts.TemplateType != 3)
                {
                    dddd = "<a href=\"AddPageTemplate.aspx?menu=edit&StyleID=" + templateIDs + "&userid=" + Mid + "\">" + tts.TemplateName + "</a>　";
                }
                else
                {
                    dddd = "<a href=\"AddPageTemplate.aspx?menu=edit&StyleID=" + templateIDs + "&userid=" + Mid + "\">" + tts.TemplateName + "</a>　";
                }
            }
            else
            {
                if (tts.TemplateType != 3)
                {
                    dddd = tts.TemplateName;
                }
                else
                {
                    dddd = tts.TemplateName;
                }
            }
            return dddd;
        }

        public string getaddnode(string templateID)
        {
            int StyleID = DataConverter.CLng(Request.QueryString["styleid"]);
            if (StyleID > 0)
            {
                return "<a href=\"AddPageTemplate.aspx?ParentID=" + templateID + "&StyleID=" + this.StyleID + "\">添加节点</a>";
            }
            else
            {
                return "<a href=\"AddPageTemplate.aspx?ParentID=" + templateID + "&id=" + Mid + "\">添加节点</a>";
            }
        }

        public string getistrue(string istrue)
        {
            return (istrue == "1") ? "<font color=green>启用</font>" : "<font color=red>停用</font>";
        }

        public string gettemptype(string type)
        {
            string restr = string.Empty;
            switch (type)
            {
                case "1":
                    restr = "文本型栏目";
                    break;
                case "2":
                    restr = "栏目型栏目";
                    break;
                case "3":
                    restr = "Url转发型栏目";
                    break;
                case "4":
                    restr = "功能型栏目";
                    break;
                default:
                    restr = "未知栏目";
                    break;
            }
            return restr;
        }
        public string getparentid(string templateID)
        {
            string restr = string.Empty;
            string imgsrcs = "";
            string temptype;
            DataTable ptable = tll.Sel();
            ptable.DefaultView.RowFilter = "ParentID=" + templateID;
            ptable.DefaultView.Sort = "OrderID ASC";
            ptable = ptable.DefaultView.ToTable();
            int pid = DataConverter.CLng(templateID);
            dp = dp + 2;
            string dddd = "";
            string ccs = new string('　', dp + 1);
            for (int i = 0; i < ptable.Rows.Count; i++)
            {
                temptype = ptable.Rows[i]["templateType"].ToString();
                imgsrcs = getnodesrc(ptable.Rows[i]["TemplateID"].ToString());
                restr = restr + "<tr class=\"tdbg list" + ptable.Rows[i]["ParentID"] + "\" name='list" + ptable.Rows[i]["ParentID"] + "' id=\"list" + ptable.Rows[i]["TemplateID"] + "\" state=\"0\" onclick=\"showlist(this," + ptable.Rows[i]["TemplateID"] + ")\">";
                restr = restr + "<td>" + ptable.Rows[i]["templateID"] + "</td>";
                restr = restr + "<td>" + ccs + "<img src=\"/Images/TreeLineImages/t.gif\"  /><img style='width:20px; height:20px;' src=\"" + imgsrcs + "\" onerror=\"this.src='/Images/nopic.gif'\"  />";

                restr = restr + gettempname(ptable.Rows[i]["templateID"].ToString());
                restr = restr + "</td><td>" + getistrue(ptable.Rows[i]["isTrue"].ToString()) + "</td>";
                restr = restr + "<td>" + gettemptype(ptable.Rows[i]["templateType"].ToString()) + "</td>";
                dddd = (temptype != "3") ? "<td>" + getaddnode(ptable.Rows[i]["templateID"].ToString()) + getmodefy(ptable.Rows[i]["templateID"].ToString()) + getdelbotton(ptable.Rows[i]["templateID"].ToString()) + "</td>" : "<td height=\"24\" >" + getmodefy(ptable.Rows[i]["templateID"].ToString()) + getdelbotton(ptable.Rows[i]["templateID"].ToString()) + " | <a href=\"javascript:;\" onclick=\"ShowOrder(" + ptable.Rows[i]["templateID"] + ")\">节点排序</a></td>";
                restr = restr + dddd;
                restr = restr + "</tr>" + getparentid(ptable.Rows[i]["templateID"].ToString()) + "";
            }
            dp = dp - 2;
            return restr;
        }
        //public string getuserparentid(string templateID)
        //{
        //    string restr = string.Empty;
        //    string imgsrcs = "";
        //    string temptype;
        //    DataTable ptable = null;
        //    int pid = DataConverter.CLng(templateID);
        //    if (StyleID > 0)
        //    {
        //        ptable = tll.ReadSysParentID(pid);
        //    }
        //    else if (Mid > 0)
        //    {
        //        ptable = tll.ReadParentID(pid);
        //    }
        //    dp = dp + 2;
        //    string dddd = "";
        //    string ccs = new string('　', dp);
        //    for (int i = 0; i < ptable.Rows.Count; i++)
        //    {
        //        temptype = ptable.Rows[i]["templateType"].ToString();
        //        imgsrcs = getnodesrc(temptype);
        //        restr = restr + "<tr class=\"tdbg list" + ptable.Rows[i]["ParentID"] + "\" name='list" + ptable.Rows[i]["ParentID"] + "' id=\"list" + ptable.Rows[i]["templateID"] + "\" onmouseover=\"this.className='tdbgmouseoverlist" + ptable.Rows[i]["ParentID"] + "'\" onmouseout=\"this.className='tdbg list" + ptable.Rows[i]["ParentID"] + "'\">";
        //        restr = restr + "<td height=\"24\" align=\"center\">" + ptable.Rows[i]["templateID"] + "</td>";
        //        restr = restr + "<td height=\"24\">" + ccs + "<img src=\"/Images/TreeLineImages/t.gif\" align=\"absmiddle\" /><img src=\"" + imgsrcs + "\" align=\"absmiddle\" />" + ptable.Rows[i]["templateName"] + "</td>";
        //        restr = restr + "<td height=\"24\" align=\"center\">" + getistrue(ptable.Rows[i]["isTrue"].ToString()) + "</td>";
        //        restr = restr + "<td height=\"24\" align=\"center\">" + gettemptype(ptable.Rows[i]["templateType"].ToString()) + "</td>";
        //        int userids = DataConverter.CLng(ptable.Rows[i]["Userid"]);
        //        if (userids > 0)
        //        {
        //            if (temptype != "3")
        //            {
        //                dddd = "<td height=\"24\" align=\"center\">" + getaddnode(ptable.Rows[i]["templateID"].ToString()) + "　" + getmodefy(ptable.Rows[i]["templateID"].ToString()) + "修改</a>　" + getdelbotton(ptable.Rows[i]["templateID"].ToString()) + "删除</a></td>";
        //            }
        //            else
        //            {
        //                dddd = "<td height=\"24\" align=\"center\">" + getmodefy(ptable.Rows[i]["templateID"].ToString()) + "修改</a>　" + getdelbotton(ptable.Rows[i]["templateID"].ToString()) + "删除</a></td>";
        //            }
        //        }
        //        else
        //        {
        //            if (temptype != "3")
        //            {
        //                dddd = "<td height=\"24\" align=\"center\">" + getaddnode(ptable.Rows[i]["templateID"].ToString()) + "</td>";
        //            }
        //            else
        //            {
        //                dddd = "<td height=\"24\" align=\"center\"></td>";
        //            }
        //        }
        //        restr = restr + dddd;
        //        restr = restr + "</tr>" + getuserparentid(ptable.Rows[i]["templateID"].ToString()) + "";
        //    }
        //    dp = dp - 2;
        //    return restr;
        //}
        public string getnodesrc(string temptype)
        {
            DataTable dt = tll.Sel();
            dt.DefaultView.RowFilter = "ParentID=" + temptype;
            dt = dt.DefaultView.ToTable();
            string imgsrcs = "";
            switch (dt.Rows.Count)
            {
                case 0:
                    imgsrcs = "fa fa-file";
                    break;
                default:
                    imgsrcs = "fa fa-folder-open";
                    break;
            }
            return imgsrcs;
        }
        public void delnodes(int did)
        {
            DataTable pdata = tll.ReadParentID(did);
            int tempid;
            for (int i = 0; i < pdata.Rows.Count; i++)
            {
                tempid = DataConverter.CLng(pdata.Rows[i]["TemplateID"]);
                delnodes(tempid);
            }
            tll.Delete(did);
        }
        //更新
        protected void Button1_Click(object sender, EventArgs e)
        {
            string tempurl = templateUrl.Value;
            if (regBll.UpTemplata(Mid, tempurl))
            {
                function.WriteSuccessMsg("更新成功");
            }
            else
            {
                function.WriteErrMsg("更新失败");
            }
        }
        public string GetFileInfo()
        {
            if (Eval("type").Equals("1"))
                return "<a href='javascript:;' onclick='toggleChild(this," + Eval("id") + ")' ><span class='fa fa-folder-open'></span> " + Eval("name") + "</a>";
            else
                return "<a href='javascript:;' data-pid='" + Eval("pid") + "' onclick=\"setVal(this,'" + Eval("rname") + "')\"><img src='/Images/TreeLineImages/t.gif' /> <span class='fa fa-file'></span> " + Eval("name") + "</a>";
        }
    }
}