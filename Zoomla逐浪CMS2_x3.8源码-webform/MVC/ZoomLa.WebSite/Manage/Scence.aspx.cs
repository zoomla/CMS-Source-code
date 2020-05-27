namespace ZoomLaCMS.Manage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;
    using ZoomLa.Common;
    using ZoomLa.Components;

    public partial class Scence : System.Web.UI.Page
    {
        XmlDocument xmlDoc = new XmlDocument();
        //来源,为空则更新,admin则返回
        public string Source { get { return (Request.QueryString["source"] ?? "").ToLower(); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                if (!string.IsNullOrEmpty(Request.Form["action"]))
                {
                    UpdateDesk();
                    //if (!System.Configuration.ConfigurationManager.AppSettings["ShowedAD"].ToLower().Equals("true"))
                    //{ UpdateConfig(); }
                }
                Response.Write(1); Response.Flush(); Response.End();

            }
            if (!IsPostBack)
            {
                CurModel_Hid.Value = Request.QueryString["curmodel"];
                Call.HideBread(Master);
            }
        }

        public void UpdateDesk()
        {
            string action = Request.Form["action"];
            string SavaInner = "";
            switch (action)
            {
                case "Desk":
                    SavaInner = Request.Form["value"];
                    break;
            }
            SiteConfig.SiteOption.Desk = SavaInner;
            SiteConfig.Update();
            //UpdateDeskCache();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "scence_clear", "ClearDefault();", true);
        }

        public void PushToDic(string id, Dictionary<string, string> dic, params string[] desk)
        {
            string r = "";
            foreach (string s in desk)
            {
                r += s + ",";
            }
            r = r.TrimEnd(',');
            dic.Add(id, r);
        }
    }
}