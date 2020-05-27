using System;
using System.Data;
using System.IO;
using ZoomLa.BLL;
using ZoomLa.BLL.CreateJS;
using ZoomLa.BLL.Design;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;
/*
 * 每个用户暂只能拥有一个站点
 * 根据模板ID和用户信息,生成站点
 */
public partial class Design_NewSite : System.Web.UI.Page
{
    B_Design_Tlp tlpBll = new B_Design_Tlp();
    B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
    B_Design_Helper desHelper = new B_Design_Helper();
    B_Design_Node desNodeBll = new B_Design_Node();
    B_IDC_DomainList domainBll = new B_IDC_DomainList();
    B_Node nodeBll = new B_Node();
    B_CodeModel conBll = new B_CodeModel("ZL_CommonModel");
    B_CodeModel artBll = new B_CodeModel("ZL_C_Article");
    B_User buser = new B_User();
    DataTableHelper dtHelper = new DataTableHelper();
    private int TlpID { get { return DataConvert.CLng(Request.QueryString["TlpID"]); } }
    private string RUrl {get { return Request.QueryString["rurl"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged(Request.RawUrl);
        M_UserInfo mu = buser.GetLogin(false);
        M_Design_Tlp tlpMod = null;
        if (TlpID == -1) { tlpMod = tlpBll.SelModelByDef(0); }
        else { tlpBll.SelReturnModel(TlpID); }
        if (tlpMod == null) { function.WriteErrMsg("模板不存在"); }
        //为其注册二级域名
        string domain = domainBll.GetNewDomain(mu.UserID.ToString()); 
        M_IDC_DomainList domainMod = new M_IDC_DomainList();
        domainMod.SType = 2;
        domainMod.MyStatus = 1;
        domainMod.UserID = mu.UserID.ToString();
        domainMod.DomName = domain;
        domainMod.ID = domainBll.Insert(domainMod);
        //else if (domainMod.UserID == mu.UserID.ToString()) { }//以前注册过,不需要做特殊处理
        //else if (domainMod.UserID != mu.UserID.ToString()) { function.WriteErrMsg("该域名已被占用"); }
        //生成站点信息(用户只能拥有一个站点)
        M_Design_SiteInfo sfMod = sfBll.SelModelByUid(mu.UserID);
        if (sfMod != null)
        {
            mu.SiteID = sfMod.ID;
            if (mu.SiteID < 1) { DBCenter.UpdateSQL("ZL_User", "SiteID=" + mu.SiteID, "UserID=" + mu.UserID); }
            //function.WriteSuccessMsg("你已经创建站点了,将自动转至用户中心", "/design/user/default.aspx");
        }
        else { sfMod = new M_Design_SiteInfo(); }
        //M_Design_SiteInfo sfMod = new M_Design_SiteInfo();
        sfMod.SiteFlag = function.GetRandomString(8).ToLower();
        sfMod.UserID = mu.UserID.ToString();
        sfMod.UserName = mu.UserName;
        sfMod.ZStatus = 1;
        sfMod.SiteName = mu.UserName + "的站点";
        sfMod.DomainID = domainMod.ID;
        sfMod.ID = sfBll.Insert(sfMod);
        //拷贝站点文件
        string tlpdir = Server.MapPath("/Design/Tlp/" + tlpMod.TlpName + "/");
        string userdir = Server.MapPath("/Site/" + sfMod.ID + "/");
        if (Directory.Exists(userdir)) { Directory.CreateDirectory(userdir); }
        if (Directory.Exists(tlpdir))
        { FileSystemObject.CopyDirectory(tlpdir, userdir); }
        //创建用户节点,先检测是否通过其他途径创建了根节点
        M_Node nodeMod = desNodeBll.GetUserRootNode(mu);
        if (nodeMod.IsNull)
        {
            nodeMod = desNodeBll.CreateUserRootNode(mu);
        }
        nodeMod.NodeBySite = sfMod.ID;
        nodeBll.UpdateByID(nodeMod);
        //导入节点和内容数据(从公用数据区)
        string commonDir = Server.MapPath("/design/tlp/common/init/");
        //DataTable nodeDT = XmlToDT(function.VToP(commonDir + "Node.xml"));
        DataTable nodeDT = DBCenter.Sel("ZL_Node", "ParentID=797");//1站点,非根节点信息
        DataTable conDT = XmlToDT(function.VToP(commonDir + "CommonModel.xml"));
        DataTable artDT = XmlToDT(function.VToP(commonDir + "Article.xml"));
        for (int i = 0; i < nodeDT.Rows.Count; i++)
        {
            DataRow dr = nodeDT.Rows[i];
            dr["NodeBySite"] = sfMod.ID;
            dr["ParentID"] = nodeMod.NodeID;
            dr["CUser"] = sfMod.UserID;
        }
        //修改逻辑,支持来源拷贝,将站点数据拷贝后-->修改-->写入,即nodedt不再来自XML
        desHelper.ImportContentFromDT(nodeDT, conDT, artDT);
        //根据所选模板,拷贝内容
        string guid = tlpBll.CopyTlp(tlpMod.ID, sfMod);
        if (string.IsNullOrEmpty(guid)) { function.WriteErrMsg("站点创建失败"); }
        RouteHelper.RouteDT = domainBll.Sel();
        mu.SiteID = sfMod.ID;
        DBCenter.UpdateSQL("ZL_User", "SiteID=" + mu.SiteID, "UserID=" + mu.UserID);
        if (string.IsNullOrEmpty(RUrl)) { Response.Redirect("/Design/Default.aspx?ID=" + guid); }
        else { Response.Redirect(RUrl); }
    }
    public DataTable XmlToDT(string ppath)
    {
        DataSet ds = new DataSet();
        ds.ReadXml(ppath);
        return ds.Tables[0];
    }
}