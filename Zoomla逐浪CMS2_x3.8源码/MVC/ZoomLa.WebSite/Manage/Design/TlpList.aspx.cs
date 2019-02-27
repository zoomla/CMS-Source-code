using Newtonsoft.Json;
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
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Design
{
    public partial class TlpList : CustomerPageAction
    {
        B_Design_Tlp tlpBll = new B_Design_Tlp();
        B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
        B_Design_TlpClass classBll = new B_Design_TlpClass();
        B_User buser = new B_User();
        public int ZType { get { return DataConvert.CLng(Request.QueryString["type"]); } }
        protected int ClassID { get { return DataConvert.CLng(Request.QueryString["ClassID"]); } }
        public string Skey
        {
            get
            {
                if (ViewState["skey"] == null || string.IsNullOrEmpty(ViewState["skey"].ToString()))
                {
                    ViewState["skey"] = Request.QueryString["skey"] ?? "";
                }
                return ViewState["skey"].ToString();
            }
            set
            {
                ViewState["skey"] = value;
            }
        }
        public int Status { get { return string.IsNullOrEmpty(Request.QueryString["status"]) ? -100 : DataConvert.CLng(Request.QueryString["status"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Status == (int)ZLEnum.ConStatus.Recycle)
                {
                    reclink.Visible = false;
                    recli.Visible = true;
                }
                MyBind();
            }
        }
        private void MyBind()
        {
            DataTable dt = tlpBll.SelWith(ClassID, ZType, 0, Skey, Status);
            RPT.DataSource = dt;
            RPT.DataBind();
            empty_div.Visible = dt == null || dt.Rows.Count == 0;
            //获取模板类型
            DataTable classTd = classBll.SelByTlpType(ZType);
            classTd.Columns.Add("label");
            DataRow allrow = classTd.NewRow();
            allrow["ID"] = 0;
            allrow["Name"] = "全部模板";
            classTd.Rows.InsertAt(allrow, 0);
            TlpClass_Hid.Value = JsonConvert.SerializeObject(classTd);
        }
        public string GetPrice()
        {
            double price = DataConvert.CDouble(Eval("Price"));
            if (price == 0) { return "免费"; }
            else { return price.ToString("f2"); }
        }
        public string GetClassName()
        {
            if (DataConvert.CLng(Eval("ClassID")) == 0) { return "未分类"; }
            else { return "<a href='TlpList.aspx?type=" + ZType + "&ClassID=" + Eval("ClassID") + "' title='点击筛选' style='color:green;'>" + Eval("ClassName") + "</a>"; }
        }
        public string GetStatus()
        {
            switch (Convert.ToInt32(Eval("ZStatus")))
            {
                case -1:
                    return "(<span style='color:red;'>停</span>)";
                case 1:
                    return "(<span style='color:green;'>荐</span>)";
                case 0:
                default:
                    return "";
            }
        }
        public string GetIsDef()
        {
            return DataConvert.CLng(Eval("IsDef")) == 1 ? "<i class='fa fa-flag' style='color:red;'></i>" : "<i class='fa fa-flag'></i>";
        }
        public string GetEditLink()
        {
            switch (ZType)
            {
                case 1:
                    return "AddScenceTlp.aspx?ID=" + Eval("ID");
                case 0:
                default:
                    return "AddTlp.aspx?ID=" + Eval("ID");
            }
        }
        public string GetViewLink()
        {
            switch (ZType)
            {
                case 1:
                    return "/design/h5/preview.aspx?TlpID=" + Eval("ID");
                case 0:
                default:
                    return "/design/preview.aspx?TlpID=" + Eval("ID");
            }
        }
        protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int tlpID = Convert.ToInt32(e.CommandArgument);
            M_Design_Tlp tlpMod = tlpBll.SelReturnModel(tlpID);
            switch (e.CommandName)
            {
                case "del2":
                    tlpMod.ZStatus = (int)ZLEnum.ConStatus.Recycle;
                    tlpBll.UpdateByID(tlpMod);
                    break;
                case "setdef":
                    tlpBll.SetDef(tlpID, ZType);
                    break;
                case "rec"://恢复
                    tlpMod.ZStatus = 0;
                    tlpBll.UpdateByID(tlpMod);
                    break;
                case "del"://彻底删除
                    tlpBll.Del(tlpMod.ID);
                    break;
            }
            MyBind();
        }

        protected void Search_B_Click(object sender, EventArgs e)
        {
            Skey = Skey_T.Text;
            if (!string.IsNullOrEmpty(Skey_T.Text)) { sel_box.Attributes.Add("style", "display:inline;"); template.Attributes.Add("style", "margin-top:44px;"); }
            else { sel_box.Attributes.Add("style", "display:none;"); }
            MyBind();
        }
        public string GetThumbImg()
        {
            string path = Eval("PreviewImg").ToString();
            if (!string.IsNullOrEmpty(path))
            {
                return "<a href = '" + path + "' class='lightbox' title='点击查看屏幕截图'><i class='fa fa-search-plus'></i></a>";
            }
            else return "";
        }
    }
}