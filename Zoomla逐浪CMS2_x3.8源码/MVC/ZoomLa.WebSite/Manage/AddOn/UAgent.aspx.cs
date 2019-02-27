using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.AddOn
{
    public partial class UAgent : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='UAgent.aspx'>移动与微信</a></li><li>自适应设备<a href='AddUAgent.aspx' title='添加自适配设备'>[" + lang.LF("添加自适配设备") + "]</a>&nbsp;<a href='../Config/SiteOption.aspx' title='开启设备自适应'>[开启设备自适配]</a></li>");
            }
        }
        protected void Bind(DataTable dt)
        {
            DataTable Cll = new DataTable();
            Cll = dt;
            RPT.DataSource = Cll;
            RPT.DataBind();
        }
        //分页
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            Bind(dt);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }
        protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {




        }
        public string GetState(string id)
        {
            switch (id)
            {
                case "0": return "no";
                case "1": return "yes";
                default: return "no";
            }
        }
        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            string imgurl = ibtn.ImageUrl;
            string imgName = imgurl.Substring(imgurl.LastIndexOf('/') + 1, imgurl.Length - imgurl.LastIndexOf('.') - 1);

            if (imgName == "yes")
            {
                ibtn.ImageUrl = "~/Images/no.gif";
            }
            else
            {
                ibtn.ImageUrl = "~/Images/yes.gif";
            }
        }
        protected string retEnab(int ID)
        {
            if (ID < 7)
                return "disabled";
            else
                return "";
        }
    }
}