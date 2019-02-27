using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Components;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class CardTypeManage : CustomerPageAction
    {
        B_CardType bc = new B_CardType();
        B_User bu = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();

            if (!IsPostBack)
            {
                string menu = Request.QueryString["menu"];
                int pageid = DataConverter.CLng(Request.QueryString["id"]);
                if (menu == "del")
                {
                    if (bc.delid(pageid))
                    {
                        Response.Write("<script language=javascript>alert('删除成功!');location.href='CardTypeManage.aspx';</script>");
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('删除失败!请选择您要删除的数据');location.href='CardTypeManage.aspx';</script>");
                    }

                }
                Bind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li><a href='CardManage.aspx'>VIP卡管理</a></li><li><a href='AddCard.aspx'>[ 生成VIP卡 ]</a><a href='CardTypeManage.aspx'>[ 卡类型管理 ]</a><a href='Addcardtype.aspx'>[添加卡类型 ]</a></li>");
            }
        }
        private void Bind()
        {
            DataTable Cll = bc.SelectAll();
            gvCard.DataSource = Cll;
            gvCard.DataBind();
        }


        protected void Button3_Click1(object sender, EventArgs e)
        {
            string CID = Request.Form["idchk"];
            if (!String.IsNullOrEmpty(CID) && bc.Delall(CID))
            {
                Response.Write("<script language=javascript>alert('批量删除成功!');location.href='CardTypeManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量删除失败!请选择您要删除的数据');location.href='CardTypeManage.aspx';</script>");
            }
        }
    }
}