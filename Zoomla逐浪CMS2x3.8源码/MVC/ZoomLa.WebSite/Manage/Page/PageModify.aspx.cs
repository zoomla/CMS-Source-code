using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

namespace ZoomLaCMS.Manage.Page
{
    public partial class PageModify : CustomerPageAction
    {
        B_Page pll = new B_Page();
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            M_Page pinfo = pll.GetSelectByGID(id);
            if (pinfo.ID == 0)
            {
                function.WriteErrMsg("无法读取配置信息");
            }
            if (!IsPostBack)
            {
                this.Label1.Text = pinfo.UserName + "的配置信息";
                this.UserName.Text = pinfo.UserName;
                this.PageTitle.Text = pinfo.PageTitle;
                this.HeadHeight.Text = pinfo.HeadHeight.ToString();
                this.txtHeadBackGround.Text = pinfo.HeadBackGround;
                this.HeadColor.Text = pinfo.HeadColor;
                this.KeyWords.Text = pinfo.KeyWords;
                this.Description.Text = pinfo.Description;
                this.TopWords.Text = pinfo.TopWords;
                this.BottonWords.Text = pinfo.BottonWords;
                pageid_Hid.Value = pinfo.ID.ToString();
                Call.SetBreadCrumb(Master, "<li>后台管理</li><li>企业黄页</li><li><a href='PageManage.aspx'>黄页管理</a></li><li>黄页信息配置</li>");
            }
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            int id = DataConverter.CLng(Request.Form["pageid"]);
            M_Page pinfo = pll.GetSelect(id);
            pinfo.UserName = this.UserName.Text;
            pinfo.PageTitle = this.PageTitle.Text;
            pinfo.HeadHeight = DataConverter.CLng(this.HeadHeight.Text);
            pinfo.HeadBackGround = this.txtHeadBackGround.Text;
            pinfo.HeadColor = this.HeadColor.Text;
            pinfo.KeyWords = this.KeyWords.Text;
            pinfo.Description = this.Description.Text;
            pinfo.TopWords = this.TopWords.Text;
            pinfo.BottonWords = this.BottonWords.Text;
            pll.GetUpdate(pinfo);
            function.WriteSuccessMsg("修改成功!", "PageManage.aspx");
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Write("<script>location.href='PageManage.aspx';</script>");
        }
    }
}