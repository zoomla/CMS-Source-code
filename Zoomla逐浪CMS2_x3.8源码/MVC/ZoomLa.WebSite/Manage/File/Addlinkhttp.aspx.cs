namespace ZoomLaCMS.Manage.FtpFile
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using System.Data;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using ZoomLa.Model;
    public partial class Addlinkhttp : System.Web.UI.Page
    {
        protected B_Content_WordChain wordBll = new B_Content_WordChain();
        protected M_Content_WordChain wordMod = new M_Content_WordChain();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='Addlinkhttp.aspx'>站内链接</a></li><li><a href='Addlinkhttp.aspx'>链接管理</a>    <a href='tjlink.aspx'>[添加链接]</a></li>");
        }
        public void DataBind(string key = "")
        {
            DataTable dt = wordBll.Sel();
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        //处理页码
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
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToString())
            {
                case "del1":
                    wordBll.Del(Convert.ToInt32(e.CommandArgument.ToString()));
                    break;
            }
            DataBind();
        }
        protected void Button3_Click1(object sender, EventArgs e)
        {
            bool flag = false;
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    int itemID = Convert.ToInt32(chkArr[i]);
                    if (wordBll.Del(itemID))
                        flag = true;
                    else
                        flag = false;
                }
            }
            if (flag)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('批量删除成功');location.href='Addlinkhttp.aspx';", true);
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('批量删除失败!');", true);
        }
        public string GetUrl(string keyvalue)
        {
            if (!string.IsNullOrEmpty(keyvalue))
                return M_Content_WordChain.GetValue(keyvalue, "href=\"", "\"");
            else
                return "";
        }
        //获取选中的checkbox
        private string[] GetChecked()
        {
            if (!string.IsNullOrEmpty(Request.Form["chkSel"]))
            {
                string[] chkArr = Request.Form["chkSel"].Split(',');
                return chkArr;
            }
            else
                return null;
        }
    }
}