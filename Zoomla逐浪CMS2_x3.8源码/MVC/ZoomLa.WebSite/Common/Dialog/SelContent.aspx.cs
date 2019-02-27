namespace ZoomLaCMS.Common.Dialog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using System.Data;
    using ZoomLa.SQLDAL.SQL;
    public partial class SelContent : System.Web.UI.Page
    {
        B_Content contentBll = new B_Content();
        public int NodeID { get { return DataConverter.CLng(Request.QueryString["nid"]); } }
        public string Key { get { return Request.QueryString["key"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            RPT.SPage = SelPage;
            if (!IsPostBack)
            {
                Search_T.Text = Key;
                RPT.DataBind();
            }
        }
        public DataTable SelPage(int pageSize, int pageIndex)
        {
            PageSetting config = new PageSetting();
            string status = ((int)ZLEnum.ConStatus.Audited).ToString();
            config = contentBll.SelPage(pageIndex, pageSize, NodeID, status: status, keyWord: Key);
            RPT.ItemCount = config.itemCount;
            return config.dt;
        }
        protected void SelContent_B_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["GeneralID"]))
            {
                int gid = Convert.ToInt32(Request.Form["GeneralID"]);
                M_CommonData model = contentBll.SelReturnModel(gid);
                HtmlContent_Hid.Value = contentBll.GetHtmlContent(gid);
                function.Script(this, "SetContent('" + model.Title + "','" + model.GeneralID + "');");
            }
        }
        protected void Search_L_Click(object sender, EventArgs e)
        {
            Response.Redirect("SelContent.aspx?key=" + Search_T.Text);
        }
    }
}