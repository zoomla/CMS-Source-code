using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Model.Design;

namespace ZoomLaCMS.Design.Editor
{
    public partial class mobile : System.Web.UI.Page
    {
        B_Design_Page pageBll = new B_Design_Page();
        B_CreateHtml createBll = new B_CreateHtml();
        public string Mid { get { return Request.QueryString["id"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Mid))
            {
                M_Design_Page pageMod = pageBll.SelModelByGuid(Mid);
                //Meta_L.Text = pageMod.Meta;
                //Resource_L.Text = pageMod.Resource;
                //Title_L.Text = pageMod.Title;
                //Meta_L.Text = createBll.CreateHtml(pageMod.Meta);
                //Resource_L.Text = createBll.CreateHtml(pageMod.Resource);
                //Title_L.Text = createBll.CreateHtml(pageMod.Title);
            }
        }
    }
}