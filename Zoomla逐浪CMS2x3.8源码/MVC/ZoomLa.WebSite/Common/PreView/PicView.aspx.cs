namespace ZoomLaCMS.Common.PreView
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Helper;
    using ZoomLa.BLL.Plat;
    using ZoomLa.Model.Message;
    using ZoomLa.Model.Plat;
    using ZoomLa.SQLDAL;
    /*
     * 用于预览贴子||能力中心图片,后期处理安全
     */
    public partial class PicView : System.Web.UI.Page
    {
        M_Guest_Bar barMod = new M_Guest_Bar();
        B_Guest_Bar barBll = new B_Guest_Bar();
        public string Source
        {
            get { return string.IsNullOrEmpty(Request.QueryString["s"]) ? "bar" : Request.QueryString["s"]; }
        }
        public int Sid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Sid < 1) return;
                MyBind();
            }
        }
        public DataTable GetDTFromBar()
        {
            barMod = barBll.SelReturnModel(Sid);
            if (barMod == null) return null;
            RegexHelper regHelper = new RegexHelper();
            string msg = StrHelper.DecompressString(barMod.MsgContent);
            barMod.MsgContent = msg.Replace("</img", " </img");
            MatchCollection matches = regHelper.GetValuesBySE(barMod.MsgContent, "<img", "/>");
            DataTable dt = DTFormat();
            for (int i = 0; i < matches.Count; i++)
            {
                Match m = matches[i];
                if (m.Value.Contains("/Ueditor")) { continue; }
                DataRow dr = dt.NewRow();
                dr["Index"] = i;
                dr["Src"] = regHelper.GetHtmlAttr(m.Value, "src");
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public DataTable GetDTFromPlat()
        {
            B_Blog_Msg msgBll = new B_Blog_Msg();
            M_Blog_Msg msgMod = msgBll.SelReturnModel(Sid);
            DataTable dt = DTFormat();
            int index = 0;
            foreach (string file in msgMod.Attach.Split('|'))
            {
                if (SafeSC.IsImage(file))
                {
                    DataRow dr = dt.NewRow();
                    dr["Index"] = index; index++;
                    dr["Src"] = file;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        public void MyBind()
        {
            DataTable dt = new DataTable();
            switch (Source)
            {
                case "bar":
                    dt = GetDTFromBar();
                    break;
                case "plat":
                    dt = GetDTFromPlat();
                    break;
            }
            RPT.DataSource = dt;
            RPT.DataBind();
            if (dt != null && dt.Rows.Count > 0)
            {
                main_img.Src = dt.Rows[0]["Src"] as string;
                piccount_span.InnerText = dt.Rows.Count.ToString();
            }
        }
        public DataTable DTFormat()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Index", typeof(int)));
            dt.Columns.Add(new DataColumn("Title", typeof(string)));
            dt.Columns.Add(new DataColumn("Src", typeof(string)));
            return dt;
        }
    }
}