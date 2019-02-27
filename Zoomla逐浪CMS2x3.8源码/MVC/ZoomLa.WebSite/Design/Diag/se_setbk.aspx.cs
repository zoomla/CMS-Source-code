namespace ZoomLaCMS.Design.Diag
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Design;
    using ZoomLa.Common;
    using ZoomLa.Model.Design;
    public partial class se_setbk : System.Web.UI.Page
    {
        public M_Design_RES resMod = new M_Design_RES();
        B_Design_RES resBll = new B_Design_RES();
        B_User buser = new B_User();
        private int CPage { get { return PageCommon.GetCPage(); } }
        public string ZType { get { return Request.QueryString["type"] ?? "sysimg"; } }
        public string Style { get { return HttpUtility.UrlDecode(Request.QueryString["style"] ?? "全部"); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            //---读取系统资源
            int psize = 18, pcount = 0;
            DataTable dt = new DataTable();
            switch (ZType)
            {
                case "sysimg":
                    {
                        dt = resBll.Search("", "bk_h5", "img", "", "", Style);
                        RPT.DataSource = PageCommon.GetPageDT(psize, CPage, dt, out pcount);
                        RPT.DataBind();
                        Page_Lit.Text = PageCommon.CreatePageHtml(pcount, CPage);
                        empty_div.Visible = dt.Rows.Count < 1;
                        //-------------------
                        string[] styleArr = ("全部," + resMod.StyleArr).Split(',');
                        foreach (string name in styleArr)
                        {
                            string css = name.Equals(Style) ? "active" : "";
                            style_ul.InnerHtml += "<li class=\"style_li " + css + "\" onclick=\"getto('" + HttpUtility.UrlEncode(name) + "');\">" + name + "</li>";
                        }
                    }
                    break;
                case "myimg":
                    {
                        //---读取用户上传的资源
                        myimg_div.Visible = true;
                        sysimg_div.Visible = false;
                        var mu = buser.GetLogin();
                        string path = function.VToP("/UploadFiles/User/" + mu.UserName + mu.UserID);
                        if (FileSystemObject.IsExist(path, FsoMethod.Folder)) { dt = FileSystemObject.SearchImg(path); }
                        Myimg_RPT.DataSource = PageCommon.GetPageDT(psize, CPage, dt, out pcount);
                        Myimg_RPT.DataBind();
                        ImgPage_Lit.Text = PageCommon.CreatePageHtml(pcount, CPage);
                        emptyimg.Visible = dt.Rows.Count < 1;
                    }
                    break;
            }
        }
    }
}