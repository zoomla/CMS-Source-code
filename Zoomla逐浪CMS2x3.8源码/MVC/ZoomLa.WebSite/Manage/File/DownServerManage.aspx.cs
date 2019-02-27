namespace ZoomLaCMS.Manage.FtpFile
{
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
    public partial class DownServerManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            string SId = "";
            if (!Page.IsPostBack)
            {
                MyBind();
                if (Request.QueryString["SId"] != null)
                {
                    SId = Request.QueryString["SId"].Trim();
                    DeleteKeyWords(SId);
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='UploadFile.aspx'>文件管理</a></li><li><a href='DownServerManage.aspx'>下载服务器</a>   <a href='DownServer.aspx'>[添加下载服务器]</a></li>");
            }

        }

        public string Getico(string logo)
        {
            if (logo != "")
            {
                return "<img src='" + logo + "' alt=\"LOGO\" width=\"50px\" height=\"30px\"  onerror=this.onerror=null;this.src='/UploadFiles/nopic.gif'  />";
            }
            else
            {
                return "";
            }
        }

        private void DeleteKeyWords(string SId)
        {
            B_DownServer bdownserver = new B_DownServer();
            if (bdownserver.DeleteByID(SId))
            {
                HttpContext.Current.Response.Write("<script language=javascript> alert('删除成功！');window.document.location.href='DownServerManage.aspx';</script>");
            }
        }
        /// 鼠标移动变色
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.className='tdbgmouseover'");
                e.Row.Attributes.Add("onmouseout", "this.className='tdbg'");
                e.Row.Attributes.Add("class", "tdbg");
            }
        }

        public void MyBind()
        {
            B_DownServer bdownserver = new B_DownServer();
            EGV.DataSource = bdownserver.GetDownServerAll();// GetAuthorPage(0, 0, 10);
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            this.MyBind();
        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            B_DownServer bdownserver = new B_DownServer();
            int i = 0, flag = 0; string sid = "";
            //int f = EGV.Rows.Count;//测试用OnClientClick="return confirm('确定要删除选中的服务器吗？')"
            for (i = 0; i < EGV.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)EGV.Rows[i].Cells[0].FindControl("SelectCheckBox");
                bool check = cbox.Checked;
                if (((CheckBox)EGV.Rows[i].Cells[0].FindControl("SelectCheckBox")).Checked)//check
                {
                    sid = EGV.DataKeys[EGV.Rows[i].RowIndex].Value.ToString();
                    if (bdownserver.DeleteByID(sid))
                        flag++;
                }
            }
            if (flag > 0)
            {
                Response.Write("<script language=javascript> alert('批量删除成功！');window.document.location.href='DownServerManage.aspx';</script>");
            }
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < EGV.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)EGV.Rows[i].FindControl("SelectCheckBox");
                cbox.Checked = this.CheckBox1.Checked;
            }
        }
    }
}