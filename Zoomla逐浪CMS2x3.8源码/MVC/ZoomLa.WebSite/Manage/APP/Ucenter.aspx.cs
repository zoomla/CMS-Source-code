using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.APP
{
    public partial class Ucenter : CustomerPageAction
    {
        B_Ucenter bll = new B_Ucenter();
        M_Ucenter model = new M_Ucenter();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.IsSuperManage();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "user/UserManage.aspx'>用户管理</a></li><li><a href='" + CustomerPageAction.customPath2 + "user/UserManage.aspx'>会员管理</a></li><li>跨域网站接入[<a href='AddUcenter.aspx'>添加授权网站</a>]</li>");
        }
        protected void Bind()
        {
            DataTable Cll = new DataTable();
            Cll = bll.Sel();
            RPT.DataSource = Cll;
            RPT.DataBind();
        }
        //文本
        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            ViewState["page"] = "1";
            Bind();
        }
        //分页
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            string items = Request.Form["Btchk"];

            if (items.IndexOf(",") == -1)
            {
                int dsd = DataConverter.CLng(items);
                bll.Del(dsd);

            }

            else if (items.IndexOf(",") > -1)
            {
                string[] deeds = items.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int s = 0; s < deeds.Length; s++)
                {
                    int dsd = DataConverter.CLng(deeds[s]);
                    bll.Del(dsd);
                }
            }
            function.WriteSuccessMsg("批量删除成功！", "Ucenter.aspx?type=" + Request["type"]);
        }
        protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument);
            model = bll.Select(id);
            if (e.CommandName == "Edit")
            {
                Response.Redirect("AddUcenter.aspx?ID=" + id);
            }
            if (e.CommandName == "Delete")
            {
                bll.Del(id);
                function.WriteSuccessMsg("删除成功！", "Ucenter.aspx");
            }

        }
        public string GetState(string id)
        {
            switch (id)
            {
                case "False": return "no";
                case "True": return "yes";
                default: return "no";
            }
        }
        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            string imgurl = ibtn.ImageUrl;
            string imgName = imgurl.Substring(imgurl.LastIndexOf('/') + 1, imgurl.Length - imgurl.LastIndexOf('.') - 1);
            // Label1.Text = imgName;

            if (imgName == "yes")
            {
                ibtn.ImageUrl = "~/Images/no.gif";
            }
            else
            {
                ibtn.ImageUrl = "~/Images/yes.gif";
            }
        }
    }
}