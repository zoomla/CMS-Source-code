namespace ZoomLa.WebSite.Manage.Content
{
    using System;
    using System.Collections.Generic;
    using ZoomLa.Components;
    using ZoomLa.BLL.CreateJS;
    using ZoomLa.Model.CreateJS;

    public partial class CreateHtml : CustomerPageAction
    {
        B_Release relBll = new B_Release();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                relBll.Start();
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='CreateHtmlContent.aspx'>生成发布</a></li><li class='active'>发布结果</li>");
            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            string Tlp = "<li>{0} <a href='{1}' target='_blank'>{1}</a></li>";
            foreach (M_ReleaseResult model in B_Release.ResutList)
            {
                if (model.IsEnd) 
                { 
                    Timer1.Enabled = false;
                }
                infoHtml.Text += string.Format(Tlp, model.ResultMsg, model.Url);
            }
            B_Release.ResutList.Clear();
        }
    }
}