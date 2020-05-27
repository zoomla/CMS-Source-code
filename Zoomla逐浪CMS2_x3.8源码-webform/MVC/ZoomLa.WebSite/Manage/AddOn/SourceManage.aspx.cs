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
    public partial class SourceManage : System.Web.UI.Page
    {
        B_Source bsource = new B_Source();
        protected void Page_Load(object sender, EventArgs e)
        {
            Egv.txtFunc = txtPageFunc;
            if (!Page.IsPostBack)
            {
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li class='active'><a href='" + Request.RawUrl + "'>来源管理</a></li>");
        }
        public void DataBind(string key = "")
        {
            DataTable dt = bsource.GetSourceAll();
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = Egv.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = Egv.PageSize;
            }
            Egv.PageSize = pageSize;
            Egv.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
        /// <summary>
        /// 根据来源名搜索,好像没用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void Search_Click(object sender, EventArgs e)
        //{
        //    string strkey = sourcekey.Text.ToString();
        //     B_Source bsource = new B_Source();
        //     GridView1.DataSource =bsource.SearchSource(strkey);
        //     GridView1.DataBind();
        //}
        protected void btndelete_Click(object sender, EventArgs e)
        {
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    int itemID = Convert.ToInt32(chkArr[i]);
                    bsource.DeleteByID(itemID.ToString());
                }
                DataBind();
            }
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del1":
                    bsource.DeleteByID(e.CommandArgument.ToString());
                    break;
                default:
                    break;
            }
            DataBind();
        }
        private string[] GetChecked()
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] chkArr = Request.Form["idchk"].Split(',');
                return chkArr;
            }
            else
                return null;
        }
    }
}