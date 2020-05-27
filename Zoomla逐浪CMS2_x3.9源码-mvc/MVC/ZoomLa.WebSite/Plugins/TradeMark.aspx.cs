namespace ZoomLaCMS.Plugins
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    public partial class TradeMark : System.Web.UI.Page
    {
        public string Key { get { return Request.QueryString["key"] ?? ""; } }
        public int CPage { get { return PageCommon.GetCPage(); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Key))
            {
                list_div.Visible = true;
                skey_t.Value = HttpUtility.UrlDecode(Key);
                string result = APIHelper.GetWebResult("http://api.tmkoo.com/search.php?keyword=" + Key + "&apiKey=4399320012393234&apiPassword=331nd3342d&pageSize=30&pageNo=" + CPage);
                JObject obj = JsonConvert.DeserializeObject<JObject>(result);
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(obj["results"].ToString());
                RPT.DataSource = dt;
                RPT.DataBind();
            }
        }
    }
}