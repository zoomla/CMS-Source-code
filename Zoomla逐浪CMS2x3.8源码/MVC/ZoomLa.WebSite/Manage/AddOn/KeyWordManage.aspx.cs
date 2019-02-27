using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
namespace ZoomLaCMS.Manage.AddOn
{
    public partial class KeyWordManage : System.Web.UI.Page
    {
        public string ShowOrder { get { return DataConverter.CStr(ViewState["ShowOrder"]); } set { ViewState["ShowOrder"] = value; } }
        public string ShowSort { get { return ViewState["ShowSort"] == null ? "desc" : ViewState["ShowSort"].ToString(); } set { ViewState["ShowSort"] = value; } }
        B_KeyWord keyBll = new B_KeyWord();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.HideBread(Master);
                MyBind();
            }
        }
        public void MyBind()
        {
            //RPT.DataSource = keyBll.SelAll(Search_T.Text,ShowOrder,ShowSort);
            RPT.DataSource = keyBll.SelAll(Search_T.Text,"priority",ShowSort);
            RPT.DataBind();
        }
        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            MyBind();
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                keyBll.DelByIDS(Request.Form["idchk"]);
            }
            MyBind();
        }
        protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            keyBll.Del(id);
            MyBind();
        }
        protected void ChangeOrder_Click(object sender, EventArgs e)
        {
            IDOrder_Btn.Text = IDOrder_Btn.Text.Split(' ')[0];
            PriOrder_Btn.Text = PriOrder_Btn.Text.Split(' ')[0];
            LinkButton link = ((LinkButton)sender);
            if (ShowOrder.Equals(link.CommandName))
            {
                ShowSort = link.CommandArgument == "desc" ? "asc" : "desc";//点击取反
            }
            else
            {
                ShowSort = "desc";
            }
            link.CommandArgument = ShowSort;
            ShowOrder = link.CommandName;
            if (ShowSort == "desc") { link.Text += " <i class='fa fa-arrow-up'></i>"; }
            else {  link.Text += " <i class='fa fa-arrow-down'></i>";}
            MyBind();
        }
    }
}