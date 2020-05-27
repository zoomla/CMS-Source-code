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
using System.Text;

namespace ZoomLaCMS.Manage.Page
{
    public partial class AddPageStyle : CustomerPageAction
    {
        B_PageStyle bll = new B_PageStyle();
        private int Sid { get { return DataConverter.CLng(Request.QueryString["sid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.page, "AddPageStyle"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (Sid > 0)
                {
                    M_PageStyle Pagestyle = bll.Getpagestrylebyid(Sid);
                    this.styleName.Text = Pagestyle.PageNodeName.ToString();
                    stylePath.Text = Pagestyle.StylePath;
                    this.orderid.Text = Pagestyle.Orderid.ToString();
                    TemplateIndex_hid.Value = Pagestyle.TemplateIndex;
                    TempImg_T.Text = Pagestyle.TemplateIndexPic;
                }
                string sname = string.IsNullOrEmpty(styleName.Text) ? "添加黄页样式" : "[" + styleName.Text + "]";
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PageManage.aspx'>企业黄页</a></li><li><a href='PageStyle.aspx'>黄页样式管理</a></li><li class='active'>" + sname + "</li>");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            M_PageStyle info = new M_PageStyle();
            if (Sid > 0)
            {
                info = bll.Getpagestrylebyid(Sid);
            }
            info.PageNodeName = styleName.Text;
            info.StylePath = stylePath.Text;
            info.Orderid = DataConverter.CLng(orderid.Text);
            info.Istrue = 1;
            info.IsDefault = 1;
            info.Addtime = DateTime.Now;
            info.TemplateIndex = TemplateIndex_hid.Value;
            info.TemplateIndexPic = TempImg_T.Text;
            if (Sid > 0)
            {
                bll.UpdatePagestyle(info);
            }
            else
            {
                bll.AddPagestyle(info);
            }
            function.WriteSuccessMsg("操作成功", "PageStyle.aspx");
        }
    }
}