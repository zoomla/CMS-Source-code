using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Design;
using ZoomLa.SQLDAL;
using ZoomLa.BLL.Helper;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZoomLaCMS.Design.User
{
    public partial class PubList : System.Web.UI.Page
    {
        public string H5ID { get { return Request.QueryString["H5ID"] ?? ""; } }
        public string FName { get { return HttpUtility.UrlDecode(Request.QueryString["FName"] ?? ""); } }
        B_Design_Pub pubBll = new B_Design_Pub();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            DataTable dt = pubBll.Sel(H5ID, -100, FName, "");
            EGV.DataSource = dt;
            EGV.DataBind();
            if (dt.Rows.Count < 1) { }
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    int id = Convert.ToInt32(e.CommandArgument);
                    pubBll.Del(id);
                    break;
            }
            MyBind();
        }
        public string GetIP() { return IPScaner.IPLocation(Eval("IP", "")); }
        public string GetUser()
        {
            string uid = Eval("UserID", "");
            if (string.IsNullOrEmpty(uid)) { return "游客"; }
            else { return "<a href='javascript:;' onclick='showuinfo(" + uid + ");'>" + Eval("UserName") + "</a>"; }
        }
        public string GetContent()
        {
            string result = "";
            string content = Eval("FormContent", "");
            if (string.IsNullOrEmpty(content) || content.Equals("[]")) { return result; }
            else
            {
                JArray jarr = JsonConvert.DeserializeObject<JArray>(content);
                foreach (var item in jarr)
                {
                    result += item["name"] + ":" + item["value"] + "|";
                }
                return result;
            }
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                pubBll.DelByIDS(ids);
            }
            MyBind();
        }
        protected void Search_B_Click(object sender, EventArgs e)
        {
            MyBind();
        }
    }
}