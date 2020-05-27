using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Data;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class InvtoManage : CustomerPageAction
    {
        B_InvtoType bll = new B_InvtoType();
        B_Admin badmin = new B_Admin();

        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            string menu = Request.QueryString["menu"];
            int id = DataConverter.CLng(Request.QueryString["id"]);
            if (menu == "del")
            {
                if (bll.DeleteByGroupID(id))
                {
                    Response.Write("<script language=javascript>alert('删除成功!');location.href='InvtoManage.aspx';</script>");
                }
                else
                {
                    Response.Write("<script language=javascript>alert('删除失败!');location.href='InvtoManage.aspx';</script>");
                }
            }
            if (!IsPostBack)
            {
                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li>发票类型管理  [<a href='AddInvoType.aspx'>添加发票类型</a>] </li>");
        }
        public void MyBind()
        {
            IType_RPT.DataSource = bll.Select_All();
            IType_RPT.DataBind();
        }
    }
}