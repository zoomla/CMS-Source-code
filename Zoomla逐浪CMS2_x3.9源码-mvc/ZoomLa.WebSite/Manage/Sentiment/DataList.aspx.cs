using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Sentiment;
using ZoomLa.Common;
using ZoomLa.Model.Sentiment;


namespace ZoomLaCMS.Manage.Sentiment
{
    public partial class DataList : CustomerPageAction
    {
        B_Sen_Data sdataBll = new B_Sen_Data();
        B_Sen_Task taskBll = new B_Sen_Task();
        public string Skey { get { return HttpUtility.UrlDecode(Request.QueryString["Skey"]); } }
        public string Source { get { return HttpUtility.UrlDecode(Request.QueryString["Source"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Default.aspx'>企业办公</a></li><li><a href='Default.aspx'>舆情监测</a></li><li class='active'>数据入库[" + Skey + "]</li>");
            }
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        public void MyBind()
        {
            if (string.IsNullOrEmpty(Skey))//为空抽出第一个关词的信息
            {
                M_Sen_Task taskMod = taskBll.SelLastModel();
                if (taskMod == null) function.WriteErrMsg("尚未定义任务,无法抓取数据");
                Response.Redirect("DataList.aspx?Skey=" + HttpUtility.UrlEncode(taskMod.Condition));
            }
            else
            {
                EGV.DataSource = sdataBll.SelByKey(Skey, Source);
                EGV.DataBind();
            }
        }
        public string GetTitle()
        {
            string title = Eval("Title").ToString();
            return HttpUtility.HtmlDecode(title);
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                default://catch
                    int id = Convert.ToInt32(e.CommandArgument);
                    M_Sen_Data sdataMod = sdataBll.SelReturnModel(id);
                    sdataMod.Title = HttpUtility.UrlEncode(StringHelper.StripHtml(sdataMod.Title, 50));
                    sdataMod.Source = HttpUtility.UrlEncode(sdataMod.Source);
                    sdataMod.Author = HttpUtility.UrlEncode(sdataMod.Author);
                    Response.Redirect(string.Format("/Common/AddContent.aspx?Title={0}&Source={1}&Author={2}&Url={3}", sdataMod.Title, sdataMod.Source, sdataMod.Author, sdataMod.Link));
                    break;
            }
        }
    }
}