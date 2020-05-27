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
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class AddToSpecial : CustomerPageAction
    {
        B_Content bll = new B_Content();
        B_Spec specBll = new B_Spec();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li class='active'>批量添加内容到专题</li>");
            if (!IsPostBack)
            {
                B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
                string id = base.Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                    function.WriteErrMsg("没有指定要添加到专题的内容ID", "../Content/ContentManage.aspx");
                else
                    this.TxtContentID.Text = id.Trim();
                this.ListSpecial.DataSource = specBll.GetSpecAll();
                this.ListSpecial.DataTextField = "SpecName";
                this.ListSpecial.DataValueField = "SpecID";
                this.ListSpecial.DataBind();
            }
        }
        /// <summary>
        /// 执行批处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            string Ids = this.TxtContentID.Text;
            foreach (ListItem item in this.ListSpecial.Items)
            {
                if (item.Selected)
                {
                    specBll.AddToSpec(Ids, DataConverter.CLng(item.Value));
                }
            }
            function.WriteSuccessMsg("内容添加到专题完成！", "../Content/ContentManage.aspx");
        }
    }
}