using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.IO;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Sentiment;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Safe;
using ZoomLa.SQLDAL;

public partial class test_Default : System.Web.UI.Page
{
    HtmlHelper htmlHelper = new HtmlHelper();
    B_Admin badmin = new B_Admin();
    B_Content conBll = new B_Content();
    B_Model modelBll = new B_Model();
    B_ModelField fieldBll = new B_ModelField();
    B_Node nodeBll = new B_Node();
    B_Role roleBll = new B_Role();
    B_Con_GetArticle acBll = new B_Con_GetArticle();
    public string Url { get { return Request.QueryString["Url"]; } }
    public int DType { get { return DataConvert.CLng(Request.QueryString["DType"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.content, "ContentMange"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='ContentManage.aspx'>内容管理</a></li><li class='active'>内容转载</li>");
        }
    }
    //节点权限检测,True通过
    public bool NodeAuth(M_AdminInfo adminMod, int NodeID)
    {
        if (adminMod.IsSuperAdmin()) return true;
        DataTable dt = roleBll.SelectNodeRoleName(adminMod.NodeRole);
        return dt.Select("NodeID=" + NodeID).Length > 0;
    }
    public void MyBind()
    {
        NodeDPBind();
        if (!string.IsNullOrEmpty(Url))//普通页面,新浪等
        {
            string html = htmlHelper.GetHtmlFromSite(Url);
            Content_T.Text = acBll.GetArticleFromWeb(html, Url);
            Title_T.Text = Request["Title"] == null ? htmlHelper.GetTitle(html) : Request["Title"];
            SourceUrl_T.Text = Request["Source"] == null ? GetSource(Url) : Request["Source"];
            Author_T.Text = Request["Author"] == null ? badmin.GetAdminLogin().AdminName : Request["Author"];
            Synopsis_T.Text = DateTime.Now.ToString();
        }
        else if (!string.IsNullOrEmpty(Request.Form["cms_json_hid"]))//页面下载,视频下载等
        {
            JObject json = (JObject)JsonConvert.DeserializeObject(Request.Form["cms_json_hid"]);
            switch (json["action"].ToString())
            {
                case "downpage":
                    if (json["url"] == null || string.IsNullOrEmpty(json["url"].ToString())) { return; }
                    string title = json["title"].ToString();
                    title = string.IsNullOrEmpty(title) ? "无标题.mht" : SafeC.RemovePathChar(title + ".mht");
                    string vpath = "/Temp/DownPage/" + title;
                    if (!Directory.Exists(Server.MapPath("/Temp/DownPage/")))
                    { Directory.CreateDirectory(Server.MapPath("/Temp/DownPage/")); }
                    vpath = htmlHelper.DownToMHT(json["url"].ToString(), vpath);
                    SafeSC.DownFile(vpath);
                    SafeSC.DelFile(vpath);
                    Response.End();
                    break;
                case "video":
                    Title_T.Text = json["title"].ToString();
                    SourceUrl_T.Text = GetSource(json["url"].ToString());
                    Content_T.Text = json["content"].ToString();
                    break;
            }
        }
    }
    //节点权限
    public void NodeDPBind()
    {
        M_AdminInfo adminMod = new M_AdminInfo();
        DataTable nodedt = nodeBll.Sel();
        if (!adminMod.IsSuperAdmin())
        {
            DataTable dt = roleBll.SelectNodeRoleName(adminMod.NodeRole);
            string ids = "";
            foreach (DataRow dr in dt.Rows)
            {
                ids += dr["NodeID"] + ",";
            }
            ids = ids.TrimEnd(',');
            if (!string.IsNullOrEmpty(ids))
            {
                nodedt.DefaultView.RowFilter = "NodeID in(" + ids + ")";
                nodedt = nodedt.DefaultView.ToTable();
            }
        }
        BindItem(nodedt);
    }
    public void BindItem(DataTable dt, int pid = 0, int layer = 0)
    {
        DataRow[] drs = dt.Select("ParentID=" + pid);
        string pre = layer > 0 ? "{0}<img src='/Images/TreeLineImages/t.gif' />" : "";
        string nbsp = "";
        for (int i = 0; i < layer; i++)
        {
            nbsp += "&nbsp;&nbsp;&nbsp;";
        }
        pre = string.Format(pre, nbsp);
        foreach (DataRow dr in drs)
        {
            NodeList_DP.InnerHtml += string.Format("<li role=\"{1}\" onclick=\"selectCate(this)\"><a role=\"menuitem\" tabindex=\"1\" href=\"javascript:;\">{0}</a></li>", pre + dr["NodeName"].ToString(), dr["NodeID"].ToString());
            BindItem(dt, Convert.ToInt32(dr["NodeID"]), (layer + 1));
        }
    }
    public string GetSource(string url)
    {
        string source = "网络转载";
        url = url.ToLower();
        if (url.Contains(".sina."))
        { source = "新浪"; }
        else if (url.Contains(".qq."))
        { source = "腾迅"; }
        else if (url.Contains(".youku."))
        { source = "优酷"; }
        return source;
    }
    public void FillContent(DataTable dt, string ftype, string name, string value)
    {
        DataRow dr = dt.NewRow();
        dr["FieldName"] = name;
        dr["FieldType"] = ftype;
        dr["FieldValue"] = value;
        dt.Rows.Add(dr);
    }
    public DataTable CreateDT()
    {
        DataTable table = new DataTable();
        table.Columns.Add(new DataColumn("FieldName", typeof(string)));
        table.Columns.Add(new DataColumn("FieldType", typeof(string)));
        table.Columns.Add(new DataColumn("FieldValue", typeof(string)));
        table.Columns.Add(new DataColumn("FieldAlias", typeof(string)));
        return table;
    }
    protected void Add_Btn_Click(object sender, EventArgs e)
    {
        int nodeid = DataConvert.CLng(selected_Hid.Value);
        if (nodeid < 1) { function.WriteErrMsg("请先选择节点", "javascript:history.go(-1);"); }
        if (!NodeAuth(badmin.GetAdminLogin(), nodeid)) { function.WriteErrMsg("你无权在该节点添加文章", "javascript:history.go(-1);"); }
        M_AdminInfo adminMod = badmin.GetAdminLogin();
        //对应模型必须有相应字段(即默认字段,不能删)
        //后台配置好视频,文章,等对应的模型,只有这些模型才能下拉选择
        //只筛选出绑定了文章模型的节点
        //只读取所绑定的第一个节点,如果绑定了多个节点,即使符合也不显示
        M_Node nodeMod = nodeBll.SelReturnModel(nodeid);
        int modeid = DataConvert.CLng(nodeMod.ContentModel.Split(',')[0]);
        M_ModelInfo modelMod = modelBll.GetModelById(modeid);
        DataTable dt = CreateDT();
        FillContent(dt, "TextType", "author", Author_T.Text);
        //FillContent(dt, "OptionType", "Source", "");
        FillContent(dt, "MultipleTextType", "synopsis", Synopsis_T.Text);
        FillContent(dt, "MultipleHtmlType", "content", Content_T.Text);
        FillContent(dt, "TextType", "Edit", adminMod.AdminName);
        //FillContent(dt, "pic", "PicType", "");
        M_CommonData model = new M_CommonData();
        model.Title = Title_T.Text;
        model.TableName = "ZL_C_Article";// modelMod.TableName;
        model.NodeID = nodeid;
        model.ModelID = 2;
        model.Status = 99;
        model.Inputer = adminMod.AdminName;
        int id = conBll.AddContent(dt, model);
        Response.Redirect("/Admin/Content/ContentShow.aspx?gid=" + id + "&nodename=" + Server.UrlEncode(nodeMod.NodeName) + "&type=add");
        //function.WriteSuccessMsg("添加成功!", "/Item/" + id + ".aspx", 0);
    }
}