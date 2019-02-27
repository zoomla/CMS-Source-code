using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.CreateJS;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

public partial class Manage_Design_Addon_Restore : System.Web.UI.Page
{
    B_Design_Helper desHelper = new B_Design_Helper();
    B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.IsSuperManage();
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Config/DatalistProfile.aspx'>扩展功能</a></li> <li><a href='" + CustomerPageAction.customPath2 + "Template/LabelManage.aspx'>标签管理</a></li><li><a href='" + Request.RawUrl + "'>加载动力版</a></li>");
        }
    }
    protected void Restore_Btn_Click(object sender, EventArgs e)
    {
        string path = TlpFile_T.Text.Trim();
        string tdir = function.VToP("/Site/NoName/");
        if (!File.Exists(path)) { function.WriteErrMsg("站点文件不存在"); }
        if (!Path.GetExtension(path).ToLower().Equals(".zip")) { function.WriteErrMsg("文件格式不正确"); }
        if (Directory.Exists(tdir)) { Directory.Delete(tdir, true); }
        //----------------------
        ZipClass zip = new ZipClass();
        zip.UnZipFiles(path, tdir);
        //还原信息(尽量少或不清除)
        DataSet siteDS = new DataSet();
        siteDS.ReadXml(tdir + "Init/Site.xml");
        //-------------站点信息(仍建立资源文件夹,但不用于建site信息了)
        DataTable siteDT = siteDS.Tables["ZL_Design_SiteInfo"];
        M_Design_SiteInfo sfMod = new M_Design_SiteInfo().GetModelFromReader(siteDT.Rows[0]);
        SiteConfig.SiteInfo.SiteName = sfMod.SiteName;
        //-------------导入页面
        B_CodeModel pageBll = new B_CodeModel("ZL_Design_Page");
        SqlHelper.ExecuteSql("TRUNCATE TABLE ZL_Design_Page");
        DataSet pageDS = new DataSet();
        pageDS.ReadXml(tdir + "Init/Page.xml");
        DataTable pageDT = pageDS.Tables[0];
        for (int i = 0; i < pageDT.Rows.Count; i++)
        {
            pageDT.Rows[i]["SiteID"] = 0;
            pageDT.Rows[i]["CDate"] = DateTime.Now;
            pageDT.Rows[i]["UPDate"] = DateTime.Now;
            pageBll.Insert(pageDT.Rows[i]);
        }
        //-------------导入节点|内容(新建一个节点)
        SqlHelper.ExecuteSql("TRUNCATE TABLE ZL_Node");
        SqlHelper.ExecuteSql("TRUNCATE TABLE ZL_CommonModel");//可注释
        SqlHelper.ExecuteSql("TRUNCATE TABLE ZL_C_Article");
        DataTable nodeDT = siteDS.Tables["ZL_Node"];//与站点绑定,其他内容与节点绑定
        DataTable conDT = siteDS.Tables["ZL_CommonModel"];
        DataTable artDT = siteDS.Tables["ZL_C_Article"];
        for (int i = 0; i < nodeDT.Rows.Count; i++)
        {
            DataRow dr = nodeDT.Rows[i];
            dr["NodeBySite"] = 0;
            dr["ParentID"] = 0;
            dr["CUser"] = 0;
        }
        desHelper.ImportContentFromDT(nodeDT, conDT, artDT);
        //移动文件
        string siteDir = function.VToP(sfMod.SiteDir);
        if (!Directory.Exists(tdir)) { function.WriteErrMsg("来源目录不存在"); }
        if (Directory.Exists(siteDir)) { Directory.Delete(siteDir, true); }
        Directory.Move(tdir, siteDir);
        function.WriteSuccessMsg("恢复站点成功,即将跳转首页", "/");
    }
    protected void DelAll_Btn_Click(object sender, EventArgs e)
    {
        //SqlHelper.ExecuteSql("TRUNCATE TABLE ZL_design_siteinfo");
        //SqlHelper.ExecuteSql("DELETE FROM ZL_DESIGN_PAGE WHERE ZTYPE=0");
        //SqlHelper.ExecuteSql("TRUNCATE TABLE ZL_COMMONMODEL");
        //SqlHelper.ExecuteSql("TRUNCATE TABLE ZL_C_Article");
        //SqlHelper.ExecuteSql("TRUNCATE TABLE ZL_NODE");
        //FileSystemObject.Delete(function.VToP("/Site/"), FsoMethod.Folder, false);
        //function.WriteSuccessMsg("清理成功");
    }
}