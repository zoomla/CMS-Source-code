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
namespace ZoomLa.WebSite.Manage
{
    public partial class CreateHtmlContent : System.Web.UI.Page
    {
        protected B_Node bnode = new B_Node();

        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            if (!badmin.ChkPermissions("CreateHtmL"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            DataTable dColumn = this.bnode.GetNodeListContain(0);
            this.ColumnList.DataSource = dColumn;
            this.ColumnList.DataTextField = "NodeName";
            this.ColumnList.DataValueField = "NodeID";
            this.ColumnList.DataBind();

            this.lbColumn.DataSource = dColumn;
            this.lbColumn.DataTextField = "NodeName";
            this.lbColumn.DataValueField = "NodeID";
            this.lbColumn.DataBind();

            this.lbSingle.DataSource = this.bnode.GetSingleList();
            this.lbSingle.DataTextField = "NodeName";
            this.lbSingle.DataValueField = "NodeID";
            this.lbSingle.DataBind();
        }
        /// <summary>
        /// 发布站点主页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateHtml.aspx?Type=index");
        }
        /// <summary>
        /// 发布所有内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewsContent_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateHtml.aspx?Type=infoall");
        }
        /// <summary>
        /// 按ID发布内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateId_Click(object sender, EventArgs e)
        {
            string idstart = this.txtIdStart.Text;
            string idend = this.txtIdEnd.Text;
            if (string.IsNullOrEmpty(idstart) || string.IsNullOrEmpty(idend))
            {
                function.WriteErrMsg("按ID发布内容必须输入起始ID和结束ID");
            }
            if (!DataValidator.IsNumber(idstart) || !DataValidator.IsNumber(idend))
            {
                function.WriteErrMsg("输入的起始ID和结束ID必须是正整形数字");
            }
            string InfoId = idstart + "," + idend;
            Response.Redirect("CreateHtml.aspx?Type=infoid&InfoID=" + InfoId);
        }
        /// <summary>
        /// 发布最新数量的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewsCount_Click(object sender, EventArgs e)
        {
            string count = this.txtNewsCount.Text;
            if (string.IsNullOrEmpty(count) || !DataValidator.IsNumber(count))
            {
                function.WriteErrMsg("要生成的最新内容个数必须是正整形数字");
            }
            Response.Redirect("CreateHtml.aspx?Type=lastinfocount&InfoID=" + count);
        }
        /// <summary>
        /// 按日期发布内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                string startDate = this.txtStartDate.Text;
                string endDate = this.txtEndDate.Text;
                if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
                {
                    function.WriteErrMsg("按日期发布内容必须设定起始日期和结束日期");
                }
                string InfoId = startDate + "," + endDate;
                Response.Redirect("CreateHtml.aspx?Type=infodate&InfoID=" + InfoId);
            }
        }
        /// <summary>
        /// 按栏目发布内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnColumnCreate_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                string InfoId = "";
                InfoId = base.Request.Form["ColumnList"].ToString();
                
                Response.Redirect("CreateHtml.aspx?Type=infocolumn&InfoID=" + InfoId);
            }
        }
        /// <summary>
        /// 发布所有栏目页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateColumnAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateHtml.aspx?Type=columnall");
        }
        /// <summary>
        /// 发布选定的栏目页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateColumn_Click(object sender, EventArgs e)
        {
            string InfoId = "";
            InfoId = base.Request.Form["lbColumn"].ToString();
            
            Response.Redirect("CreateHtml.aspx?Type=columnbyid&InfoID=" + InfoId);
        }
        /// <summary>
        /// 发布所有单页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateSingleAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateHtml.aspx?Type=single");
        }
        /// <summary>
        /// 发布选定的单页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateSingle_Click(object sender, EventArgs e)
        {
            string InfoId = "";
            InfoId = base.Request.Form["lbSingle"].ToString();
            
            Response.Redirect("CreateHtml.aspx?Type=singlebyid&InfoID=" + InfoId);
        }
        /// <summary>
        /// 发布选定的专题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateSpeacil_Click(object sender, EventArgs e)
        {
            string InfoId = "";
            InfoId = base.Request.Form["lbSpeacil"].ToString();
            
            Response.Redirect("CreateHtml.aspx?Type=special&InfoID=" + InfoId);
        }
    }
}