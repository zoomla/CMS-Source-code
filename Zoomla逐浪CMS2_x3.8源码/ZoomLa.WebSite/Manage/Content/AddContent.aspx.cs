using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
public partial class V_Content_AddContent : CustomerPageAction
{
    M_AdminInfo adminMod = new M_AdminInfo();
    M_Node nodeMod = new M_Node();
    B_Admin adminBll = new B_Admin();
    B_Content contentBll = new B_Content();
    B_Content_WordChain wordBll = new B_Content_WordChain();
    B_DocModel docBll = new B_DocModel();
    B_Model modelBll = new B_Model();
    B_ModelField fieldBll = new B_ModelField();
    B_Node nodeBll = new B_Node();
    B_NodeRole nodeRBll = new B_NodeRole();
    B_Role roleBll = new B_Role();
    B_Spec spbll = new B_Spec();
    B_Content_ScheTask taskBll = new B_Content_ScheTask();
    B_KeyWord keyBll = new B_KeyWord();
    public int NodeID { get { return DataConvert.CLng(Request.QueryString["NodeID"]); } }
    public int ModelID { get { return DataConvert.CLng(Request.QueryString["ModelID"]); } }
    public string NodeDir { get { return ViewState["NodeDir"] as string; } set { ViewState["NodeDir"] = value; } }
    public string NodeName { get { return ViewState["NodeName"] as string; } set { ViewState["NodeName"] = value; } }
    protected bool createnew = true;
    public string keys = "";
    //----工作流
    B_Process proBll = new B_Process();
    M_Process proMod = new M_Process();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string action = Request["action"] ?? "";
            string value = Request["value"] ?? "";
            string result = "";
            switch (action)
            {
                case "duptitle":
                    DataTable dt = contentBll.GetByDupTitle(value);
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
        }
        if (ModelID < 1 || NodeID < 1) { function.WriteErrMsg("参数错误"); }
        DataTable fieldDT = fieldBll.SelByModelID(ModelID, true);
        bt_txt.Text = B_Content.GetFieldAlias("Title", fieldDT) + "：";
        gjz_txt.Text = B_Content.GetFieldAlias("Tagkey", fieldDT) + "：";
        Label4.Text = B_Content.GetFieldAlias("Subtitle", fieldDT) + "：";
        tj_txt.Text = B_Content.GetFieldAlias("EliteLevel", fieldDT);
        zht_txt.Text = B_Content.GetFieldAlias("Status", fieldDT);
        gx_time.Text = B_Content.GetFieldAlias("UpDateTime", fieldDT);
        hits_txt.Text = B_Content.GetFieldAlias("Hits", fieldDT);
        if (!IsPostBack)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
            if (!string.IsNullOrEmpty(Request.QueryString["Source"])) { function.Script(this, "SetContent();"); }
            //-----工作流,检测该节点是否绑定工作流，如无绑定，则直接通过,未绑定,则以第一步为准
            contentsk.Visible = B_ARoleAuth.Check(ZLEnum.Auth.content, "ContentMange");
            DataTable ddlDT = proBll.SelByNodeID2(NodeID);
            ddlFlow.DataSource = ddlDT;
            ddlFlow.DataTextField = "PName";
            ddlFlow.DataValueField = "PPassCode";
            ddlFlow.DataBind();
            M_AdminInfo adminMod = B_Admin.GetLogin();
            ddlFlow.SelectedValue = adminMod.DefaultStart.ToString();
            //if (ddlDT.TableName.Equals("Default"))
            //{
            //    ddlFlow.SelectedValue = "99";
            //}
            //else
            //{
            //    ddlFlow.Items[0].Selected = true;
            //}
            //-----
            //专题
            if (spbll.GetFirstID() > 0) { SpecInfo_Li.Text = "<button type='button' class='btn btn-info btn-sm' onclick='ShowSpDiag()'>" + Resources.L.添加至专题 + "</button>"; }
            else { SpecInfo_Li.Text = "<div style='margin:5px;' class='btn btn-default disabled'><span class='fa fa-info-circle'></span> " + Resources.L.尚未定义专题 + "</div>"; }
            //智能关键词
            keys = keyBll.SelToJson();
            //------
            nodeMod = nodeBll.GetNodeXML(NodeID);
            NodeDir = nodeMod.NodeDir;
            if (nodeMod.ListPageHtmlEx < 3)
            {
                CreateHTML.Visible = true;
            }
            else
            {
                CreateHTML.Visible = false;
            }
            nodeMod = nodeBll.GetNodeXML(NodeID);
            if (nodeMod.Contribute != 1)
            {
                function.Script(this, "ShowSys();");
            }
            M_ModelInfo model = modelBll.GetModelById(ModelID);
            Label1.Text = "添加" + model.ItemName;
            EBtnSubmit.Text = "添加" + model.ItemName;
            Title_L.Text = "添加" + model.ItemName;
            ModelHtml.Text = fieldBll.InputallHtml(ModelID, NodeID, new ModelConfig()
            {
                Source = ModelConfig.SType.Admin
            });
            txtAddTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtdate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            BindTempList();
            string bread = "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='ContentManage.aspx'>" + Resources.L.内容管理 + "</a></li><li><a href='ContentManage.aspx?NodeID=" + nodeMod.NodeID + "'>" + nodeMod.NodeName + "</a></li>";
            bread += "<li class='active'>[向本栏目添加" + model.ItemName + "]</li>";
            bread += "<div class='pull-right hidden-xs'> <span> <a href='" + customPath2 + "Content/SchedTask.aspx' title='" + Resources.L.查看计划任务 + "'><span class='fa fa-clock-o' style='color:#28b462;'></span></a>" + GetOpenView() + "<span onclick=\"ShowDiag('EditNode.aspx?NodeID=" + NodeID + "','" + Resources.L.配置本节点 + "');\" class='fa fa-cog' title='" + Resources.L.配置本节点 + "' style='cursor:pointer;margin-left:5px;'></span></span></div>";
            Call.SetBreadCrumb(Master, bread);
        }
    }
    //节点权限操作
    void CheckNode(M_Node nodemod)
    {
        //string Purview = nodemod.Purview;
        //Purview = "<Purview>" + Purview + "</Purview>";
        //XmlDocument doc = new XmlDocument();
        //doc.LoadXml(Purview);
        //string forumstr= doc.SelectSingleNode("//forum").InnerText;
        //if (forumstr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0].Equals("1"))
        //    IsComm_Radio.SelectedValue = "1";
        //else
        //    IsComm_Radio.SelectedValue = "0";
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)//添加文章
    {
        M_AdminInfo adminMod = B_Admin.GetLogin();
        IList<string> content = new List<string>();
        if (SiteConfig.SiteOption.FileRj == 1 && contentBll.SelHasTitle(txtTitle.Text.Trim())){ function.WriteErrMsg(Resources.L.该内容标题已存在+"!", "javascript:history.go(-1);"); }
        DataTable dt = fieldBll.SelByModelID(ModelID, false);
        Call commonCall = new Call();
        DataTable table = commonCall.GetDTFromPage(dt, Page, ViewState, content);
        M_CommonData CData = new M_CommonData();
        CData.NodeID = NodeID;
        CData.ModelID = ModelID;
        CData.TableName = modelBll.GetModelById(ModelID).TableName;
        CData.Title = txtTitle.Text.Trim();
        CData.Inputer = string.IsNullOrEmpty(txtInputer.Text) ? adminMod.AdminName : txtInputer.Text;
        CData.EliteLevel = ChkAudit.Checked ? 1 : 0;
        CData.InfoID = "";
        CData.Hits = string.IsNullOrEmpty(txtNum.Text) ? 0 : DataConverter.CLng(txtNum.Text);
        CData.UpDateType = 2;
        CData.UpDateTime = DataConverter.CDate(txtdate.Text);
        CData.TagKey = Request.Form["tabinput"];
        string status = ddlFlow.SelectedValue.Trim();
        if (!string.IsNullOrEmpty(status))
        {
            CData.Status = Convert.ToInt32(status);
        }
        // CData.Titlecolor = Titlecolor.Text;
        CData.Template = TxtTemplate_hid.Value;
        CData.CreateTime = DataConverter.CDate(txtAddTime.Text);
        CData.SpecialID = "," + string.Join(",", Spec_Hid.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) + ",";
        string parentTree = "";
        CData.FirstNodeID = nodeBll.SelFirstNodeID(NodeID, ref parentTree);
        CData.ParentTree = parentTree;
        CData.TitleStyle = ThreadStyle.Value;
        CData.TopImg = ThumImg_Hid.Value;//首页图片
        CData.PdfLink = Makepdf.Checked ? "pdf_" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ".pdf" : "";
        CData.Subtitle = Subtitle.Text;
        CData.PYtitle = PYtitle.Text;
        CData.RelatedIDS = RelatedIDS_Hid.Value;
        CData.IsComm = Convert.ToInt32(IsComm_Radio.SelectedValue);
        int newID = contentBll.AddContent(table, CData);//插入信息给两个表，主表和从表:CData-主表的模型，table-从表
        //推送
        if (!string.IsNullOrEmpty(pushcon_hid.Value))
        {
            string[] nodeArr = pushcon_hid.Value.Trim(',').Split(',');
            for (int i = 0; i < nodeArr.Length; i++)
            {
                CData.NodeID = Convert.ToInt32(nodeArr[i]);
                contentBll.AddContent(table, CData);
            }
        }
        #region 生成PDF
        //if (Makepdf.Checked)
        //{
        //    M_CommonData m_CommonData = contentBll.SelReturnModel(newID);
        //    string strSql = "select source from " + CData.TableName + " where id=" + m_CommonData.ItemID;
        //    string source = "";
        //    SqlDataReader dr = SqlHelper.ExecuteReader(System.Data.CommandType.Text, strSql);
        //    if (dr.Read())
        //    {
        //        source = dr["source"].ToString();
        //    }
        //    dr.Close();
        //}
        #endregion
        #region  关键词
        {
            keys = StrHelper.RemoveRepeat(CData.TagKey.Split(','), IgnoreKey_Hid.Value.Split(','));
            if (!string.IsNullOrEmpty(keys))
            {
                keyBll.AddKeyWord(keys, 1);
            }
        }
        #endregion
        ZLLog.ToDB(ZLEnum.Log.content, new M_Log()
        {
            UName = adminMod.AdminName,
            Source = Request.RawUrl,
            Action = "添加内容",
            Message = "标题:" + CData.Title + ",Gid:" + newID,
            Level = "add"
        });
        //添加计划任务(审核时间),如果自动审核时间小于当前时间则忽略
        //if (!string.IsNullOrEmpty(CheckDate_T.Text) && Convert.ToDateTime(CheckDate_T.Text) > DateTime.Now)
        //{
        //    AddSched(newID, CheckDate_T.Text, M_Content_ScheTask.TaskTypeEnum.AuditArt);
        //    contentBll.UpdateStatus(newID, (int)ZLEnum.ConStatus.UnAudit);
        //}
        //if (!string.IsNullOrEmpty(TimeDate_T.Text))
        //{
        //    AddSched(newID, TimeDate_T.Text, M_Content_ScheTask.TaskTypeEnum.UnAuditArt);
        //}
        //积分
        if (SiteConfig.UserConfig.InfoRule > 0)
        {
            B_User buser = new B_User();
            M_UserInfo muser = buser.GetUserByName(adminMod.AdminName);
            if (!muser.IsNull)
            {
                buser.ChangeVirtualMoney(muser.UserID, new M_UserExpHis()
                {
                    UserID = muser.UserID,
                    detail = "添加内容:" + txtTitle.Text + "增加积分",
                    score = SiteConfig.UserConfig.InfoRule,
                    ScoreType = (int)M_UserExpHis.SType.Point
                });
            }
        }
        M_Node noinfo = nodeBll.GetNodeXML(NodeID);
        CreateHtmlDel createHtml = CreateHtmlFunc;
        createHtml.BeginInvoke(HttpContext.Current.Request, quickmake.Checked, CData, table, null, null);
        Response.Redirect("ContentShow.aspx?gid=" + newID  + "&type=add");
    }
    public void AddSched(int gid, string datetext, M_Content_ScheTask.TaskTypeEnum type)
    {
        M_Content_ScheTask taskmod = new M_Content_ScheTask();
        taskmod.TaskType = (int)type;
        taskmod.TaskContent = gid.ToString();
        taskmod.ExecuteTime = datetext;
        taskmod.LastTime = DateTime.Parse(datetext).ToString();
        taskBll.Add(taskmod);
    }
    //草稿
    protected void DraftBtn_Click(object sender, EventArgs e)
    {
        M_AdminInfo adminMod = B_Admin.GetLogin();
        DataTable dt = fieldBll.SelByModelID(ModelID);
        Call commonCall = new Call();
        DataTable table = commonCall.GetDTFromPage(dt, Page, ViewState);
        M_CommonData CData = new M_CommonData();
        CData.NodeID = NodeID;
        if (!string.IsNullOrEmpty(ModelID.ToString()))
        {
            CData.ModelID = ModelID;
            CData.TableName = modelBll.GetModelById(ModelID).TableName;
        }
        CData.Title = txtTitle.Text.Trim();
        CData.Status = (int)ZLEnum.ConStatus.Draft;
        CData.Inputer = adminMod.AdminName;
        CData.Inputer = string.IsNullOrEmpty(txtInputer.Text) ? adminMod.AdminName : txtInputer.Text;
        CData.EliteLevel = ChkAudit.Checked ? 1 : 0;
        CData.InfoID = "";
        CData.PdfLink = "";
        CData.Hits = string.IsNullOrEmpty(txtNum.Text) ? 0 : DataConverter.CLng(txtNum.Text);
        CData.CreateTime = DataConverter.CDate(txtAddTime.Text);
        CData.UpDateType = 2;
        CData.UpDateTime = DataConverter.CDate(txtdate.Text);
        CData.TagKey = Request.Form["tabinput"];
        CData.Template = TxtTemplate_hid.Value;
        CData.Subtitle = Subtitle.Text;
        CData.PYtitle = PYtitle.Text;
        CData.TitleStyle = Request.Form["ThreadStyle"];
        string parentTree = "";
        CData.FirstNodeID = nodeBll.SelFirstNodeID(NodeID, ref parentTree);
        CData.ParentTree = parentTree;
        int newID = contentBll.AddContent(table, CData);
        Response.Redirect("ContentShow.aspx?gid=" + newID  + "&type=add");
    }
    public bool checknode(string bb, int nodeid)
    {
        string[] bbarray = bb.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < bbarray.Length; i++)
        {
            if (NodeID == DataConverter.CLng(bbarray[i]))
                return true;
        }
        return false;
    }
    //添加的同时,异步生成静态页
    public delegate void CreateHtmlDel(HttpRequest r, bool c, M_CommonData cdate, DataTable table);
    public void CreateHtmlFunc(HttpRequest r, bool c, M_CommonData cdate, DataTable table)
    {
        M_Node noinfo = nodeBll.GetNodeXML(NodeID);
        if (noinfo.ListPageHtmlEx < 3 && quickmake.Checked == true)
        {
            B_Create CreateBll = new B_Create(r);
            if (c)
            {
                CreateBll.createann(cdate.GeneralID.ToString());//发布内容页
                CreateBll.CreateColumnByID(cdate.NodeID.ToString());//发布栏目页
            }
            CreateBll.CreatePageIndex(); //发布首页
        }
        cdate = contentBll.SelReturnModel(cdate.GeneralID);
        string[] strArr = fieldBll.GetIsChain(ModelID, 1).Split(',');//需要替换的字段
        for (int i = 0; i < strArr.Length; i++)
        {
            DataRow[] dr = table.Select("FieldName = '" + strArr[i] + "' ");
            if(dr!=null&&dr.Length>0)
                dr[0]["FieldValue"] = wordBll.RePlaceKeyWord(dr[0]["FieldValue"].ToString());
        }
        contentBll.UpdateContent(table, cdate);
    }
    //显示前台浏览按钮
    public string GetOpenView()
    {
        string outstr = "", strurl = string.Empty;
        strurl = "Class_" + NodeID + "/Default.aspx";
        outstr = " <a href='/" + strurl + "'  target='_blank' title='"+Resources.L.前台浏览 + "'><span class='fa fa-eye'></span></a>";
        return outstr;
    }
    public void BindTempList()
    {
        string pathdir = (AppDomain.CurrentDomain.BaseDirectory + ZoomLa.Components.SiteConfig.SiteOption.TemplateDir.Replace("/", @"\")).Replace(@"\\", @"\");
        if (!File.Exists(pathdir)) { return; }
        DataTable tables = FileSystemObject.GetDirectoryAllInfos(pathdir + "/" + Resources.L.内容页 + "/", FsoMethod.File);
        tables.Columns.Add("TemplatePic");//添加模板图片url
        tables.Columns.Add("TemplateID");
        tables.Columns.Add("TemplateUrl");
        for (int i = 0; i < tables.Rows.Count; i++)
        {
            tables.Rows[i]["rname"] = tables.Rows[i]["rname"].ToString().Replace(pathdir, "").Replace(@"\", "/").Substring(1);
            string filename = "内容页_" + Path.GetFileNameWithoutExtension(tables.Rows[i]["name"].ToString()) + ".jpg";
            tables.Rows[i]["TemplatePic"] = ZoomLa.Components.SiteConfig.SiteOption.TemplateDir + "/thumbnail/" + filename;
            tables.Rows[i]["TemplateID"] = tables.Rows[i]["rname"];
            tables.Rows[i]["TemplateUrl"] = tables.Rows[i]["rname"];
            if (!File.Exists(function.VToP(pathdir + "/thumbnail/" + filename)))
            {
                tables.Rows.Remove(tables.Rows[i]);
                i--;
            }
        }
        tables.DefaultView.RowFilter = "type=1 OR name LIKE '%.html'";
        TlpView_Tlp.DataSource = tables;
        TlpView_Tlp.DataBind();
    }
}