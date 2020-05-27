using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model.Edit;

namespace ZoomLaCMS.Edit.Doc
{
    public partial class UpdateDoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string html = Request.Form["html"];
            DocCache.verMod.html = html;
            DocCache.verMod.sessionID = Session.SessionID;
            DocCache.verMod.version = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            Response.Write(DocCache.verMod.version); Response.Flush(); Response.End();
        }
    }
}