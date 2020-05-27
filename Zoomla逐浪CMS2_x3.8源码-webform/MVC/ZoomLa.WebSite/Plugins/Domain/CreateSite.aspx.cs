namespace ZoomLaCMS.Plugins.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Components;
    public partial class CreateSite : System.Web.UI.Page
    {
        protected IISWebSite site = new IISWebSite();
        protected B_User buser = new B_User();

        protected string serverdomain = SiteConfig.SiteOption.ProjectServer;
        PagedDataSource pds = new PagedDataSource();
        //Project	  TempDirName	Author	 ProType	OrderNum	ProjectImg	isinstall
        //蓝色121外教	Speak	   逐浪官方	  0	           0	 	               true
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);

            if (!IsPostBack)
            {
                if (Session["domNameL"] == null)
                    function.WriteErrMsg("请按步骤来，点击<a href='/SiteDefault.aspx' style='color:blue'>返回第一步!!</a>");
                domNameL.Text = Session["domNameL"].ToString();
                DataBind();
            }
        }
        private int GetPageIndex(PagedDataSource pds, string v)
        {
            if (string.IsNullOrEmpty(v) || DataConverter.CLng(v) == 0) return 1;
            int cPage = DataConverter.CLng(v);
            if (cPage > pds.PageCount) cPage = pds.PageCount;
            return cPage;
        }
        //产生前台分页,cPage从1开始
        public string CreateFrontPage(int pageCount, int cPage)
        {
            #region 前台最终Html
            //<li class="disabled"><a href="?page=1">&laquo;</a></li>
            //<li class="active"><a href="?page=1">1 <span class="sr-only">(current)</span></a></li>
            //<li><a href="?page=2">2 <span class="sr-only"></span></a></li>
            //<li><a href="?page=last">&raquo;</a></li>
            //</ul>
            #endregion

            string pageHtml = "<ul class='pagination'>";
            pageHtml += "<li " + (cPage > 1 ? "" : "class='disabled'") + "><a href='?page=1'>&laquo;</a></li>";
            for (int i = 1; i <= pageCount; i++)
            {
                pageHtml += "<li " + (cPage != i ? "" : "class='active'") + "><a href='?page=" + i + "'>" + i + " <span class='sr-only'>(current)</span></a></li>";
            }
            pageHtml += "<li><a href='?page=" + pageCount + "'>&raquo;</a></li></ul>";
            return pageHtml;
        }
        private void DataBind(string key = "")
        {
            //PageSize固定,只需要获取当前页即可
            pds.DataSource = TempDT;
            pds.PageSize = 6;
            pds.AllowPaging = true;
            int cPage = GetPageIndex(pds, Request.QueryString["page"]);
            pds.CurrentPageIndex = (cPage - 1);
            pageHtmlLi.Text = CreateFrontPage(pds.PageCount, cPage);
            tempRepeater.DataSource = pds;
            tempRepeater.DataBind();
        }
        //模板表,模板数据较少更改，存入Cache缓存中
        public DataView TempDT
        {
            get
            {
                if (Cache["TempDT"] == null)
                    Cache["TempDT"] = GetServerTemp();
                return (Cache["TempDT"] as DataTable).DefaultView;
            }
        }
        //调用服务端的接口，获取信息
        private DataTable GetServerTemp()
        {
            DataSet tableset = new DataSet();
            tableset.ReadXml(serverdomain + "/api/gettemplate.aspx?menu=getprojectinfo");
            return tableset.Tables[0];
        }
        public string GetThumbnail(string tempDirName)// onclick="setData('<%#Eval("Project")+":"+Eval("TempDirName") %>');"
        {
            //string result = "<a rel='lightbox' class='thumbnail' title='点击大图预览'  href='" + serverdomain + "/Template/" + tempDirName + "/Bview.jpg'> <img alt='点击大图预览'  onerror='this.onerror=null;this.src='/Images/nopic.gif' style='width:124px;height:111px;' src='" + serverdomain + "/Template/" + Server.UrlEncode(tempDirName) + "/view.jpg'></a>";

            string result = "<a class='thumbnail lightbox' title='点击大图预览'  href='" + serverdomain + "/Template/" + tempDirName + "/view.jpg' target='_PreView'> <img alt='点击大图预览' style='width:625px;height:350px;' src='" + serverdomain + "/Template/" + Server.UrlEncode(tempDirName) + "/view.jpg'></a>";
            return result;
        }
        protected void sureBtn_Click(object sender, EventArgs e)
        {
            IdentityAnalogue ia = new IdentityAnalogue();
            if (ia.CheckEnableSA(false))
            {
                site.SiteName = domNameL.Text.Split('.')[0] + "_" + buser.GetLogin().UserName;
                site.Port = "998";//将来改为80
                site.DomainName = domNameL.Text;// "win01"
                site.PhysicalPath = StationGroup.SitePath + site.SiteName;
                site.AppPool = site.SiteName;
                site.dbName = domNameL.Text.Split('.')[0];//使用用户的二级域名作为数据库名,例mysite
                site.TempName = Request.Form["selectedTempData"].Split(':')[0];
                site.TempDir = Request.Form["selectedTempData"].Split(':')[1];
                site.TempUrl = serverdomain + "/Template/" + site.TempDir + ".zip";
                //iisHelper.CreateSite(site);//移往最后一步创建
            }
            Session["siteInfo"] = site;
            Response.Redirect("SiteProgress.aspx");
        }
        #region Disuse
        //public string GetUrl() 
        //{
        //   string url= Request.Url.Host;
        //   if (url.IndexOf("www.") == 0) { url=url.Remove(0, 4); }
        //    url= buser.GetLogin().UserName +"."+ url;
        //   return url;
        //}
        #endregion
    }
}