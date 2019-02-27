namespace manage.Page
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
    using ZoomLa.Common;
    using ZoomLa.Model;
    using System.Text;
    using ZoomLa.Model.Page;
    using ZoomLa.BLL.Page;
    using System.Data.SqlClient;

    public partial class AddPageTemplate : CustomerPageAction
    {
        protected B_Group gll = new B_Group();
        protected B_Templata tll = new B_Templata();
        protected B_PageStyle sll = new B_PageStyle();
        protected B_Content cll = new B_Content();
        protected B_User ull = new B_User();
        protected B_Model bllmodel = new B_Model();
        protected B_ModelField mll = new B_ModelField();
        protected B_Admin badmin = new B_Admin();
        private M_Templata Tempinfo = null;
        private B_PageReg regBll = new B_PageReg();

        protected string returnurl = string.Empty;
        public int sid, id, styleids;
        public int PageparentID;
        public int ds = 2;
        protected string callBackReference;
        protected string result;

        public int UserID 
        {
            get { return Convert.ToInt32(Request.QueryString["UserID"]); }
        }
        public int StyleID
        {
            get
            {
                if (string.IsNullOrEmpty(StyleID_Hid.Value))
                { StyleID_Hid.Value = Request.QueryString["StyleID"]; }
                return DataConverter.CLng(StyleID_Hid.Value);
            }
            set { StyleID_Hid.Value = value.ToString(); }
        }
        public int Pid { get { return DataConverter.CLng(Request.QueryString["ParentID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            sid = DataConverter.CLng(base.Request.QueryString["sid"]);
            id = DataConverter.CLng(base.Request.QueryString["id"]);
            styleids = DataConverter.CLng(base.Request.QueryString["styleids"]);
            PageparentID = DataConverter.CLng(base.Request.QueryString["ParentID"]);
            if (!IsPostBack)
            {
                Call.HideBread(Master);
                BindDP();
                #region 判断添加、修改
                //else if (styleids > 0)
                //{
                //    loadmenulist(UserID);
                //    int groupid = DataConverter.CLng(tll.Getbyid(styleids).UserGroup);
                //    string nodename = sll.Getpagestrylebyid(groupid).PageNodeName.ToString();
                //    Label1_Hid.Value = "<a href=\"PageTemplate.aspx?styleid=" + groupid + "\">[黄页栏目管理]</a><a href=\"PageTemplate.aspx?styleid=" + groupid + "\">样式栏目设置</a>" + nodename;
                //    Label2_Hid.Value = "修改黄页栏目";
                //    Label3.Text = "修改黄页栏目";
                //    Button1.Text = "修改栏目";
                //    returnurl = "PageTemplate.aspx?styleid=" + groupid + "";
                //    Tempinfo = tll.Getbyid(styleids);
                //    PageStyle_DP.SelectedValue = Tempinfo.UserGroup;
                //    StyleID_Hid.Value = Tempinfo.UserGroup;
                //}
                if (sid > 0)//修改
                {
                    string nodename = ull.SeachByID(UserID).UserName;
                    loadmenulist(UserID);
                    int PageStyleid = 0;
                    DataTable regdt = mll.SelectTableName("ZL_Pagereg", "TableName like 'ZL_Reg_%' and UserName='" + nodename + "'");
                    if (regdt != null && regdt.Rows.Count > 0)
                    {
                        string PageTableName = regdt.Rows[0]["TableName"].ToString();
                        DataTable pagetable = mll.SelectTableName(PageTableName, "UserName = '" + nodename + "'");
                        PageStyleid = DataConverter.CLng(pagetable.Rows[0]["Styleid"]);
                        UserGroup.Value = PageStyleid.ToString();
                        Username.Value = nodename;
                    }
                    Label1_Hid.Value = "黄页管理 <a href=\"PageManage.aspx\">[黄页用户列表]</a> <a href=\"PageTemplate.aspx?id=" + UserID + "\">" + nodename + "</a> ";
                    Label2_Hid.Value = "修改自定义用户黄页栏目";
                    Label3.Text = "修改栏目";
                    Button1.Text = "修改栏目";
                    returnurl = "PageTemplate.aspx?id=" + UserID + "";
                    Tempinfo = tll.Getbyid(sid);
                    PageStyle_DP.SelectedValue = Tempinfo.UserGroup;
                }
                else if (StyleID > 0)//从样式列表处跳转
                {
                    M_PageStyle styleinfo = sll.Getpagestrylebyid(StyleID);
                    string nodename = styleinfo.PageNodeName;
                    returnurl = "PageTemplate.aspx?styleid=" + StyleID + "";
                    UserGroup.Value = StyleID.ToString();
                    loadmenulist(UserID);
                    if (PageparentID > 0)
                    {
                        string snodename = tll.Getbyid(PageparentID).TemplateName;
                        Label1_Hid.Value = "<a href=\"PageTemplate.aspx?styleid=" + StyleID + "\">[黄页栏目管理]</a><a href=\"PageTemplate.aspx?styleid=" + StyleID + "\">样式栏目设置</a> " + nodename;
                        Label2_Hid.Value = "添加黄页子栏目";
                        Label3.Text = "添加黄页子栏目 [" + snodename + "]";
                    }
                    else
                    {
                        Label1_Hid.Value = "<a href=\"PageTemplate.aspx?styleid=" + StyleID + "\">[黄页栏目管理]</a>  <a href=\"PageTemplate.aspx?styleid=" + StyleID + "\">样式栏目设置</a>" + nodename;
                        Label2_Hid.Value = "添加黄页栏目";
                        Label3.Text = "添加黄页样式栏目";
                    }
                    PageStyle_DP.SelectedValue = StyleID.ToString();
                }
                else if (id > 0)
                {
                    M_PageReg m_PageReg = regBll.SelReturnModel(DataConverter.CLng(Request["id"]));
                    string nodename = m_PageReg.UserName;
                    DataTable cmdtable = mll.SelectTableName("ZL_Pagereg", "TableName like 'ZL_Reg_%' and UserName='" + nodename + "'");
                    string PageTableName = cmdtable.Rows[0]["TableName"].ToString();
                    DataTable pagetable = mll.SelectTableName(PageTableName, "UserName = '" + nodename + "'");
                    int PageStyleid = DataConverter.CLng(pagetable.Rows[0]["Styleid"]);
                    UserGroup.Value = PageStyleid.ToString();
                    Username.Value = nodename;
                    string tempoptionlist = "<select name=\"ParentID\" id=\"ParentID\">";
                    tempoptionlist = tempoptionlist + "<option value=\"0\">请选择栏目</option>";
                    loadmenulist(UserID);

                    Label1_Hid.Value = "黄页管理  <a href=\"PageTemplate.aspx?id=" + id + "\">[黄页用户列表]</a> <a href=\"PageTemplate.aspx?id=" + id + "\">" + nodename + "</a> ";
                    Label2_Hid.Value = "添加自定义用户黄页栏目";
                    Label3.Text = "添加自定义用户黄页栏目";
                    returnurl = "PageTemplate.aspx?id=" + id + "&userid="+UserID;
                    Userids.Value = ull.GetUserByName(nodename).UserID.ToString();
                }
                #endregion
                #region 初始化界面默认值
                if (sid > 0 || styleids > 0)
                {
                    if (Tempinfo != null)//修改栏目
                    {
                        if (!IsPostBack)
                        {
                            templateName.Text = Tempinfo.TemplateName;//栏目名称
                            templateUrl_hid.Value = Tempinfo.TemplateUrl.ToString();//栏目模板地址
                            templateType.Text = Tempinfo.TemplateType.ToString();//栏目类型
                            OpenType.Text = Tempinfo.OpenType.ToString();//打开方式
                            isTrue.Text = Tempinfo.IsTrue.ToString();//启用状态
                            identifiers.Text = Tempinfo.Identifiers.ToString();//标识符
                            NodeFileEx.Text = Tempinfo.NodeFileEx.ToString();//栏目扩展名
                            ContentFileEx.Text = Tempinfo.ContentFileEx;//内容页扩展名
                            addtime.Text = Tempinfo.Addtime.ToString();//添加时间
                            Nodeimgurl.Text = Tempinfo.Nodeimgurl.ToString();//栏目图片地址
                            Nodeimgtext.Text = Tempinfo.Nodeimgtext.ToString();//栏目提示
                            Pagecontent.Text = Tempinfo.Pagecontent.ToString();//说明 用于在单页页详细介绍单页信息，支持HTML
                            PageMetakeyword.Text = Tempinfo.PageMetakeyword.ToString();//META关键词
                            PageMetakeyinfo.Text = Tempinfo.PageMetakeyinfo.ToString();//META网页描述
                            linkurl.Text = Tempinfo.Linkurl.ToString();//外部链接地址
                            linkimg.Text = Tempinfo.Linkimg.ToString();//外部链接图片地址
                            linktxt.Text = Tempinfo.Linktxt.ToString();//外部链接提示
                            OrderID.Text = Tempinfo.OrderID.ToString();//排序
                            TemplateID.Value = Tempinfo.TemplateID.ToString();//栏目ID
                            UserGroup.Value = Tempinfo.UserGroup;//所属样式ID
                            PageStyle_DP.SelectedValue = Tempinfo.UserGroup;
                            Userids.Value = Tempinfo.UserID.ToString();//所属用户ID
                            lblmodelstr.Value = Tempinfo.Modelinfo.ToString();
                            Username.Value = Tempinfo.Username.ToString();
                        }
                    }
                }
                else //公用添加栏目
                {
                    TemplateID.Value = "0";
                    addtime.Text = DateTime.Now.ToString();
                    OpenType.Text = "_blank";
                    NodeFileEx.Text = "html";
                    ContentFileEx.Text = "html";
                    OrderID.Text = "0";
                }

                #endregion
                DataTable dt = mll.SelectTableName("ZL_Model", "TableName like 'ZL_Page_%'");
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                #region 判断栏目类型
                switch (templateType.Text)
                {
                    case "0": //默认为栏目
                        typenode.Visible = true;
                        tempurl.Visible = true;
                        typetxt.Visible = false;
                        urltype.Visible = false;
                        nodetype.Visible = true;
                        SelectModel.Visible = true;
                        templateType.SelectedValue = "2";
                        break;
                    case "1": //单页
                        typenode.Visible = false;
                        tempurl.Visible = true;
                        typetxt.Visible = true;
                        urltype.Visible = false;
                        nodetype.Visible = true;
                        SelectModel.Visible = false;
                        templateType.SelectedValue = "1";
                        break;
                    case "2": //栏目
                        tempurl.Visible = true;
                        typenode.Visible = true;
                        typetxt.Visible = true;
                        urltype.Visible = false;
                        nodetype.Visible = true;
                        SelectModel.Visible = true;
                        templateType.SelectedValue = "2";
                        break;
                    case "3": //url连接
                        typenode.Visible = false;
                        tempurl.Visible = false;
                        typetxt.Visible = false;
                        urltype.Visible = true;
                        nodetype.Visible = false;
                        SelectModel.Visible = false;
                        templateType.SelectedValue = "3";
                        break;
                    case "4": //功能型栏目
                        typenode.Visible = false;
                        tempurl.Visible = true;
                        typetxt.Visible = false;
                        urltype.Visible = false;
                        nodetype.Visible = false;
                        SelectModel.Visible = true;
                        templateType.SelectedValue = "4";
                        break;
                    default: //默认为栏目
                        tempurl.Visible = true;
                        typenode.Visible = true;
                        typetxt.Visible = true;
                        urltype.Visible = false;
                        nodetype.Visible = true;
                        SelectModel.Visible = true;
                        templateType.SelectedValue = "2";
                        break;
                }
                #endregion
            }
        }
        private void BindDP()
        {
            PageStyle_DP.DataSource = sll.Sel();
            PageStyle_DP.DataBind();
            PageStyle_DP.Items.Insert(0, new ListItem("未指定", "0"));
        }
        protected void loadmenulist(int usid)
        {
            int groupid = 0;
            if (StyleID > 0)
            {
                groupid = StyleID;
            }
            else if (styleids > 0)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.page, "AddPageStyle"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                groupid = DataConverter.CLng(tll.Getbyid(styleids).UserGroup);
            }
            else if (id > 0)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.page, "PageAudit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                M_AdminInfo info = badmin.GetAdminLogin();
                DataTable m_PageRega = regBll.Sel(DataConverter.CLng( Request["id"]));
                string nodename = m_PageRega.Rows[0]["UserName"].ToString();
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("nodename", nodename) };
                int UsertabID = ull.GetUserByName(nodename).UserID;
                DataTable regdt = mll.SelectTableName("ZL_Pagereg", "TableName like 'ZL_Reg_%' and UserName=@nodename",sp);
                if (regdt != null && regdt.Rows.Count > 0)
                {
                    string PageTableName = regdt.Rows[0]["TableName"].ToString();
                    DataTable pagetable = mll.SelectTableName(PageTableName, "UserName = @nodename", sp);
                    groupid = DataConverter.CLng(pagetable.Rows[0]["Styleid"]);
                }
            }
            else if (sid > 0)
            {
                string nodename = ull.SeachByID(usid).UserName;
                if (nodename != null)
                {
                    SqlParameter[] sp2 = new SqlParameter[] { new SqlParameter("nodename", nodename) };
                    DataTable regdt = mll.SelectTableName("ZL_Pagereg", "TableName like 'ZL_Reg_%' and UserName=@nodename", sp2);
                    if (regdt != null && regdt.Rows.Count > 0)
                    {
                        string PageTableName = regdt.Rows[0]["TableName"].ToString();
                        DataTable pagetable = mll.SelectTableName(PageTableName, "UserName = @nodename", sp2);
                        groupid = DataConverter.CLng(pagetable.Rows[0]["Styleid"]);
                    }
                }
            }

            DataTable styletable = null;
            styletable = sll.GetPagestylelist();
            string TempParentID = "<select name=\"ParentID\" class='form-control' id=\"ParentID\">";
            TempParentID = TempParentID + "<option value=\"0\" selected=\"selected\">请选择栏目</option>";
            for (int s = 0; s < styletable.Rows.Count; s++)
            {
                string ddstr = "";
                if (groupid.ToString() == styletable.Rows[s]["PageNodeid"].ToString())
                {
                    ddstr = "selected=\"selected\"";
                }
                TempParentID = TempParentID + "<option " + ddstr + " value=\"s-" + styletable.Rows[s]["PageNodeid"].ToString() + "\">" + styletable.Rows[s]["PageNodeName"].ToString() + "</option>";
                TempParentID = TempParentID + optionlist(styletable.Rows[s]["PageNodeid"].ToString(), StyleID, usid);
            }
            ParentID.Text = TempParentID;
        }
        public string GetModelIcon()
        {
            string icon = Eval("ItemIcon").ToString();
            return StringHelper.GetItemIcon(icon);
        }
        protected string optionlist(string PageNodeid, int styleid, int usid)
        {
            string cclist = "";
            int cs = 1;
            int ppid = DataConverter.CLng(PageNodeid);
            DataTable templist = null;
            templist = tll.Getinputinfo("UserGroup", PageNodeid + "and ParentID=0 and userid=0 or (ParentID=0 and Userid=" + usid + " and UserGroup=" + PageNodeid + ") ");
            for (int i = 0; i < templist.Rows.Count; i++)
            {
                string spancode = new string('　', cs);
                string selecttxt = "";
                if (PageparentID == DataConverter.CLng(templist.Rows[i]["TemplateID"]))
                {
                    selecttxt = "selected=\"selected\"";
                }
                if (sid > 0)
                {
                    if (tll.Getbyid(sid).ParentID == DataConverter.CLng(templist.Rows[i]["TemplateID"]))
                    {
                        selecttxt = "selected=\"selected\"";
                    }
                }
                if (styleids > 0)
                {
                    if (tll.Getbyid(styleids).ParentID == DataConverter.CLng(templist.Rows[i]["TemplateID"]))
                    {
                        selecttxt = "selected=\"selected\"";
                    }
                }
                cclist = cclist + "<option " + selecttxt + " value=\"" + templist.Rows[i]["TemplateID"].ToString() + "\">" + spancode + "├" + templist.Rows[i]["TemplateName"].ToString() + "</option>";
                cclist = cclist + Parentlists(templist.Rows[i]["TemplateID"].ToString(), styleid, usid, ppid);
            }
            cs = cs + 1;
            return cclist;
        }
        protected string Parentlists(string ParentID, int styleid, int usid, int PageNodeid)
        {
            string cclist = "";
            DataTable templist = null;
            int ppid = DataConverter.CLng(ParentID);
            ds = ds + 1;
            if (usid > 0)
            {
                templist = tll.ReadUserall(ppid, usid, PageNodeid);
            }
            else
            {
                templist = tll.ReadSysParentID(ppid);
            }
            for (int i = 0; i < templist.Rows.Count; i++)
            {
                string spancode = new string('　', ds);
                string selecttxt = "";
                if (PageparentID == DataConverter.CLng(templist.Rows[i]["TemplateID"]))
                {
                    selecttxt = "selected=\"selected\"";
                }
                //if (sid == DataConverter.CLng(templist.Rows[i]["TemplateID"]))
                //{
                //    selecttxt = "selected=\"selected\"";
                //}
                if (sid > 0)
                {
                    if (tll.Getbyid(sid).ParentID == DataConverter.CLng(templist.Rows[i]["TemplateID"]))
                    {
                        selecttxt = "selected=\"selected\"";
                    }
                }
                if (styleids > 0)
                {
                    if (tll.Getbyid(styleids).ParentID == DataConverter.CLng(templist.Rows[i]["TemplateID"]))
                    {
                        selecttxt = "selected=\"selected\"";
                    }
                }
                cclist = cclist + "<option " + selecttxt + " value=\"" + templist.Rows[i]["TemplateID"].ToString() + "\">" + spancode + "├" + templist.Rows[i]["TemplateName"].ToString() + "</option>";
                cclist = cclist + Parentlists(templist.Rows[i]["TemplateID"].ToString(), styleid, usid, PageNodeid);
            }
            ds = ds - 1;
            return cclist;
        }
        //提交
        protected void Button1_Click(object sender, EventArgs e)
        {
            int TempID = DataConverter.CLng(TemplateID.Value);
            M_Templata Cdata = new M_Templata();
            Cdata.TemplateID = DataConverter.CLng(TemplateID.Value);
            Cdata.TemplateName = templateName.Text;
            Cdata.TemplateUrl = templateUrl_hid.Value;
            Cdata.TemplateType = DataConverter.CLng(templateType.Text);
            Cdata.OpenType = OpenType.Text;
            Cdata.UserGroup = PageStyle_DP.SelectedValue;//绑定样式
            Cdata.Addtime = DataConverter.CDate(addtime.Text);
            Cdata.IsTrue = DataConverter.CLng(isTrue.SelectedValue);
            Cdata.UserID = DataConverter.CLng(Userids.Value);
            Cdata.OrderID = DataConverter.CLng(OrderID.Text);
            Cdata.ParentID = PageparentID;
            Cdata.Identifiers = identifiers.Text;
            Cdata.NodeFileEx = NodeFileEx.Text;
            Cdata.ContentFileEx = ContentFileEx.Text;
            Cdata.Nodeimgurl = Nodeimgurl.Text;
            Cdata.Nodeimgtext = Nodeimgtext.Text;
            Cdata.Pagecontent = Pagecontent.Text;
            Cdata.PageMetakeyword = PageMetakeyword.Text;
            Cdata.PageMetakeyinfo = PageMetakeyinfo.Text;
            Cdata.Linkurl = linkurl.Text;
            Cdata.Linkimg = linkimg.Text;
            Cdata.Linktxt = linktxt.Text;
            Cdata.Username = Username.Value;
            #region 模型,后期需置入BLL
            string ChkModel = Request.Form["ChkModel"];
            string mdinfo = string.Empty;
            if (!string.IsNullOrEmpty(ChkModel))
            {
                if (ChkModel.IndexOf(",") > -1)
                {
                    string[] modestr = ChkModel.Split(new char[] { ',' });
                    for (int i = 0; i < modestr.Length; i++)
                    {
                        mdinfo = mdinfo + modestr[i] + "," + Request.Form["TxtModelTemplate_" + modestr[i]] + "|";
                    }

                }
                else
                {
                    if (DataConverter.CLng(ChkModel) > 0)
                    {
                        mdinfo = ChkModel + "," + Request.Form["TxtModelTemplate_" + ChkModel];
                    }
                }
                Cdata.Modelinfo = mdinfo;
            }
            if (TempID > 0)
            {
                tll.Update(Cdata);
            }
            else
            {
                Cdata.OrderID = tll.GetNextOrderID(Pid);
                tll.Add(Cdata);
            }
            #endregion
            function.WriteSuccessMsg("操作成功", "PageTemplate.aspx?UserID=" + UserID + "&StyleID=" + StyleID);
        }
        public string GetChk(string mid)
        {
            string result = "";
            string[] arr = lblmodelstr.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            if (IsInModel(mid, arr))
            {
                result = "<input type=\"checkbox\" id=\"ChkModel\" name=\"ChkModel\" value=\"" + mid + "\" checked />";
            }
            else
            {
                result = "<input type=\"checkbox\" id=\"ChkModel\" name=\"ChkModel\" value=\"" + mid + "\" />";
            }
            return result;
        } 
        public bool IsInModel(string modelid, string[] array)
        {
            bool flag = false;
            string[] tempno = null;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].IndexOf(",") > 0)
                {
                    tempno = array[i].Split(new char[] { ',' });
                    if (modelid == tempno[0])
                    {
                        flag = true;
                        break;
                    }
                }


            }
            return flag;
        }
        public string GetTemplate(string mid)
        {
            string result = "";
            string[] arr = lblmodelstr.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            string[] tempmo = null;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].IndexOf(",") > 0)
                {
                    tempmo = arr[i].Split(new char[] { ',' });
                    if (mid == tempmo[0])
                    {
                        result = tempmo[1];
                    }
                }
            }
            return result;
        }
    }
}