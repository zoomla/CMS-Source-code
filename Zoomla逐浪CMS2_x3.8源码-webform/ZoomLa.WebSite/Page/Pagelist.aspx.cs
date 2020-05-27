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
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.BLL.Page;
using ZoomLa.Model.Page;


public partial class Pagelist :FrontPage
{
    protected B_CreateHtml bll = new B_CreateHtml();
    protected B_PageReg b_PageReg = new B_PageReg();
    //Pagelist.Cpage”隐藏了继承的成员“FrontPage.Cpage”
    public new int Cpage { get { int page = Page.RouteData.Values["CPage"] == null ? 1 : DataConverter.CLng(Page.RouteData.Values["CPage"].ToString()); page = page < 1 ? 1 : page; return page; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(base.Request.QueryString["NodeID"]))
        {
            int ItemID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
            int Pageid = DataConverter.CLng(base.Request.QueryString["Pageid"]);

            B_Templata bnode = new B_Templata();
            B_ModelField mll = new B_ModelField();
            M_PageReg pageinfo = b_PageReg.SelReturnModel(Pageid);
            M_Templata tempinfo = bnode.Getbyid(ItemID);
            B_Content cll = new B_Content();
            string tablename = pageinfo.TableName;
            if (pageinfo.IsNull)
            {
                ErrToClient("[产生错误的可能原因：您访问的黄页信息不存在！]");
            }
            if (tablename.IndexOf("ZL_Reg_") == -1)
            {
                ErrToClient("[产生错误的可能原因：黄页信息不存在！]");
            }
            int Styleid = DataConverter.CLng(tempinfo.UserGroup);
            int userid = tempinfo.UserID;

            DataTable nodeinfo = bnode.Getinputinfo("TemplateID", ItemID.ToString());
            if (nodeinfo.Rows.Count == 0) { ErrToClient("[产生错误的可能原因：您访问的栏目信息不存在！]"); }
            if (DataConverter.CLng(nodeinfo.Rows[0]["IsTrue"]) != 1) { ErrToClient("[产生错误的可能原因：您访问的信息不可用！]"); }

            int nodetype = DataConverter.CLng(nodeinfo.Rows[0]["TemplateType"]);
            string opentype = nodeinfo.Rows[0]["OpenType"].ToString();
            if (nodetype == 3)
            {
                Response.Redirect(nodeinfo.Rows[0]["linkurl"].ToString());
            }
            //---获取路径
            B_PageStyle styleBll = new B_PageStyle();
            M_PageStyle styleMod = new M_PageStyle();
            if (pageinfo.NodeStyle == 0) { ErrToClient("[产生错误的可能原因：未为该黄页栏目指定样式!]"); }
            //-----获取该黄页所绑定的样式,将栏目模板与样式模板路径组合,UserGroup即为其所绑定的样式ID
            styleMod = styleBll.SelReturnModel(Convert.ToInt32(pageinfo.NodeStyle));
            string TemplateDir = Server.MapPath(styleMod.StylePath + tempinfo.TemplateUrl);
            string ContentHtml = FileSystemObject.ReadFile(TemplateDir);
            ContentHtml = this.bll.CreateHtml(ContentHtml, Cpage, ItemID, Pageid);

            string identifiers = nodeinfo.Rows[0]["identifiers"].ToString();
            if (!string.IsNullOrEmpty(identifiers))
            {
                ContentHtml = ContentHtml.Replace(identifiers, ItemID.ToString());
            }
            Response.Write(ContentHtml);
        }
        else
        {
            ErrToClient("[产生错误的可能原因：没有指定栏目ID！访问规则：Pagelist.aspx?NodeID=节点ID&Pageid=黄页ID]");
        }
    }

    /// <summary>
    /// 获得缓存
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetCatch(string key)
    {
        return Cache.Get(key).ToString();
    }

    /// <summary>
    /// 设置文件缓存
    /// </summary>
    /// <param name="key"></param>
    /// <param name="dirvalue"></param>
    private void SetCatch(string key, string dirvalue)
    {
        if (Cache.Get(key) == null || Cache.Get(key).ToString() == "")
        {
            Cache.Insert(key, FileSystemObject.ReadFile(dirvalue), new System.Web.Caching.CacheDependency(dirvalue));
        }
    }
    //private string GetTempPath(M_PageReg regMod,tempMod)
    //{
    //    B_PageStyle styleBll = new B_PageStyle();
    //    M_PageStyle styleMod = new M_PageStyle();
    //    if (string.IsNullOrEmpty(regMod.NodeStyle)) function.WriteErrMsg("未为该黄页栏目指定样式!");
    //    string tempPath = "";
    //    //-----获取该黄页所绑定的样式,将栏目模板与样式模板路径组合,UserGroup即为其所绑定的样式ID
    //    styleMod = styleBll.SelReturnModel(Convert.ToInt32(regMod.NodeStyle));
    //    tempPath = Server.MapPath(styleMod.StylePath + tempMod.TemplateUrl);
    //    return tempPath;
    //}
}