namespace ZoomLaCMS.Plugins.ECharts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    public partial class ZLEcharts : System.Web.UI.Page
    {
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        public string CodeID { get { return string.IsNullOrEmpty(Request["CodeID"]) ? "code" : Request["CodeID"]; } }
        //附加行为,如生成base64
        public string Action { get { return Request.QueryString["action"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Mid > 0)
                {
                    M_Content_Chart chartMod = new ZoomLa.BLL.Content.B_Content_Chart().SelReturnModel(Mid);
                    code.Text = chartMod.option;
                }
                else
                {
                    function.Script(this, "GetCode();");
                }
            }
        }
    }
}