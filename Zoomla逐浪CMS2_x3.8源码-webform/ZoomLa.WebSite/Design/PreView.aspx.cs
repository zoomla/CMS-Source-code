using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

public partial class Design_PreView : System.Web.UI.Page
{
    B_Design_Tlp tlpBll = new B_Design_Tlp();
    B_Design_TlpClass tcBll = new B_Design_TlpClass();
    B_Design_Page pageBll = new B_Design_Page();
    B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
    B_CreateHtml createBll = new B_CreateHtml();
    B_User buser = new B_User();
    //-------前端使用
    public M_Design_Page pageMod = new M_Design_Page();
    public string extendData = "[]";
    public string comp_global = "[]";
    public string sitecfg = "{}";
    public int TlpID { get { return DataConverter.CLng(Request.QueryString["TlpID"]); } }
    public string Mid { get { return Request.QueryString["ID"] ?? ""; } }
    public string Domain
    {
        get
        {
            string _domain = B_Route.GetParam("domain", Page);
            if (string.IsNullOrEmpty(_domain)) { _domain = "/index"; }
            return _domain;
        }
    }
    public string Path { get { return B_Route.GetParam("Path", Page); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        //模板预览
        if (TlpID > 0)
        {
            DataTable tlpdt = DBCenter.Sel("ZL_Design_Page", "TlpID=" + TlpID + " AND SiteID=0 AND (Path='' OR Path='/' OR Path='/index' OR Path='index')");
            if (tlpdt.Rows.Count > 0) { Response.Redirect("/Design/PreView.aspx?ID=" + tlpdt.Rows[0]["Guid"]); return; }
            else { function.WriteErrMsg("模板未指定首页,无法预览"); }
        }
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Mid))
            {
                pageMod = pageBll.SelModelByGuid(Mid);
            }
            else if (!string.IsNullOrEmpty(Path))
            {
                pageMod = pageBll.SelModelByPath(Domain, Path);
            }
            if (pageMod == null || pageMod.ID < 1) { function.WriteErrMsg("页面不存在"); }
            pageMod = pageBll.MergeGlobal(pageMod,"");
            if (pageMod.comp_global.Count > 0)
            {
                comp_global = JsonConvert.SerializeObject(pageMod.comp_global);
            }
            //------站点
            M_Design_SiteInfo sfMod = sfBll.SelReturnModel(pageMod.SiteID);
            if (pageMod.IsTemplate)//浏览模板
            {
                tlpprompt_div.Visible = true;
                M_UserInfo mu = buser.GetLogin();
                M_Design_Tlp tlpMod = tlpBll.SelReturnModel(pageMod.TlpID);
                M_Design_TlpClass tcMod=tcBll.SelReturnModel(tlpMod.ClassID);
                Title_L.Text = tlpMod.TlpName + "_" + pageMod.Title;
                tlpinfo_sp.InnerHtml = "模板名称：" + tlpMod.TlpName + " 归属分类：" + tcMod.Name;
                if (mu.IsNull) { nologin_wrap.Visible = true; }
                else { logged_wrap.Visible = true; }
            }
            else if (sfMod != null)//浏览用户站点
            {
                sitecfg = sfBll.ToSiteCfg(sfMod);
                Title_L.Text = sfMod.SiteName + "_" + pageMod.Title;
            }
            else
            {
                Title_L.Text = pageMod.Title;
            }
            //------解析标签
            if (!string.IsNullOrEmpty(pageMod.labelArr))
            {
                DataTable labelDT = new DataTable();
                labelDT.Columns.Add(new DataColumn("guid", typeof(string)));
                labelDT.Columns.Add(new DataColumn("label", typeof(string)));
                labelDT.Columns.Add(new DataColumn("htmlTlp", typeof(string)));
                string[] labelArr = pageMod.labelArr.Trim('|').Split('|');
                foreach (string label in labelArr)
                {
                    DataRow dr = labelDT.NewRow();
                    dr["guid"] = label.Split(':')[0];
                    dr["label"] = label.Split(':')[1];
                    string html = createBll.CreateHtml(StringHelper.Base64StringDecode(dr["label"].ToString()));
                    dr["htmlTlp"] = StringHelper.Base64StringEncode(html);
                    labelDT.Rows.Add(dr);
                }
                extendData = JsonConvert.SerializeObject(labelDT);
            }
            //------解析页面属性
            Resource_L.Text = pageMod.Resource;
            Meta_L.Text = pageMod.Meta;
        }
    }
    protected void Apply_Btn_Click(object sender, EventArgs e)
    {
        M_Design_Page pageMod = pageBll.SelModelByGuid(Mid);
        M_UserInfo mu = buser.GetLogin();
        if (mu.IsNull) { function.WriteErrMsg("用户未登录"); }
        if (mu.SiteID < 1)
        {
            //直接创建站点
            Response.Redirect("/design/newsite.aspx?TlpID=" + pageMod.TlpID);
        }
        else//已有站点则应用模板 
        {
            string guid = tlpBll.CopyTlp(pageMod.TlpID, sfBll.SelReturnModel(mu.SiteID));
            Response.Redirect("/design/?ID=" + guid);
        }
    }
}