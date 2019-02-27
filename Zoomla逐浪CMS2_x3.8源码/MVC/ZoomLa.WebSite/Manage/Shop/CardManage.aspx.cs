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

namespace ZoomLaCMS.Manage.Shop
{
    public partial class CardManage : CustomerPageAction
    {
        B_Card bc = new B_Card();
        B_User bu = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            if (!IsPostBack)
            {
                string menu = Request.QueryString["menu"];
                int pageid = DataConverter.CLng(Request.QueryString["id"]);

                switch (menu)
                {
                    case "del":
                        if (bc.delcards(pageid.ToString()))
                        {
                            Response.Write("<script language=javascript>alert('删除成功!');location.href='CardManage.aspx';</script>");
                        }
                        else
                        {
                            Response.Write("<script language=javascript>alert('删除失败!请选择您要删除的数据');location.href='CardManage.aspx';</script>");
                        }
                        break;

                    case "isuse":
                        bc.OpenOrStopCard(pageid, 1);

                        break;

                    case "stop":
                        bc.OpenOrStopCard(pageid, 2);

                        break;
                }
                Bind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li>VIP卡管理</a></li><li><a href='AddCard.aspx'>[ 生成VIP卡 ]</a><a href='CardTypeManage.aspx'>[ 卡类型管理 ]</a><a href='Addcardtype.aspx'>[添加卡类型 ]</a></li>");
            }
        }

        private void Bind()
        {
            DataTable Cll = bc.SelectAll();
            gvCard.DataSource = Cll;
            gvCard.DataBind();
        }

        protected string GetUserName(string uid)
        {
            if (DataConverter.CLng(uid) == 0)
            {
                return "暂无用户";
            }
            else
            {
                return bu.GetUserByUserID(int.Parse(uid)).UserName;
            }

        }

        protected string GetState(string str)
        {
            string state = "";
            switch (str)
            {
                case "2":
                    state = "  <span style=' color:red;'>√</span>";
                    break;
                case "1":
                    state = "×";
                    break;
            }
            return state;
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bc.delcards(CID))
            {
                Response.Write("<script language=javascript>alert('批量删除成功!');location.href='CardManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量删除失败!请选择您要删除的数据');location.href='CardManage.aspx';</script>");
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["idchk"];
            if (!String.IsNullOrEmpty(CID) && bc.OpenOrStopCards(CID, 2))
            {
                Response.Write("<script language=javascript>alert('批量开启成功!');location.href='CardManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量开启失败');location.href='CardManage.aspx';</script>");
            }
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["idchk"];
            if (!String.IsNullOrEmpty(CID) && bc.OpenOrStopCards(CID, 1))
            {
                Response.Write("<script language=javascript>alert('批量关闭成功!');location.href='CardManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量关闭失败');location.href='CardManage.aspx';</script>");
            }
        }

        public string showuse(string id)
        {
            int sid = DataConverter.CLng(id);
            string str = "";
            M_Card tp = bc.GetSelect(sid);
            if (tp.CardState == 1)
            {
                str = "<a href=?menu=stop&id=" + sid + "><i class='fa fa-check'></i>启用</a>";
            }
            else
            {
                str = "<a href=?menu=isuse&id=" + sid + "><i class='fa fa-ban'></i>禁用</a>";
            }
            return str;
        }
    }
}