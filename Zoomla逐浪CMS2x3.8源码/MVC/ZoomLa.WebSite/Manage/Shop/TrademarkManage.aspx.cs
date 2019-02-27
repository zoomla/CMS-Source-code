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
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class TrademarkManage : CustomerPageAction
    {
        protected B_Trademark bll = new B_Trademark();
        B_Admin badmin = new B_Admin();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "DeliverType"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }

            string menu = Request.QueryString["menu"];
            string sid = Request.QueryString["id"];
            int id = 0;
            id = Convert.ToInt32(sid);
            switch (menu)
            {
                case "edit":
                    Response.Redirect("Producer.aspx?menu=edit&id=" + id + "");
                    break;
                case "stop":
                    bll.Upotherdata("1", id.ToString());
                    Response.Write("<script language=javascript>location.href='TrademarkManage.aspx';</script>");
                    break;
                case "istop":
                    bll.Upotherdata("2", id.ToString());
                    Response.Write("<script language=javascript>location.href='TrademarkManage.aspx';</script>");
                    break;
                case "tuijian":
                    bll.Upotherdata("3", id.ToString());
                    Response.Write("<script language=javascript>location.href='TrademarkManage.aspx';</script>");
                    break;
                case "qiyong":
                    bll.Upotherdata("4", id.ToString());
                    Response.Write("<script language=javascript>location.href='TrademarkManage.aspx';</script>");
                    break;
                case "notop":
                    bll.Upotherdata("5", id.ToString());
                    Response.Write("<script language=javascript>location.href='TrademarkManage.aspx';</script>");
                    break;
                case "nojian":
                    bll.Upotherdata("6", id.ToString());
                    Response.Write("<script language=javascript>location.href='TrademarkManage.aspx';</script>");
                    break;
                case "delete":
                    bll.DeleteByID(id);
                    Response.Write("<script language=javascript>location.href='TrademarkManage.aspx';</script>");
                    break;
                default:
                    break;
            }

            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li class='active'>品牌管理</li>  <a href='Trademark.aspx'>[添加品牌]</a>");
            }
        }
        public void MyBind()
        {
            DataTable cc = bll.GetTrademarkAll();
            RPT.DataSource = cc;
            RPT.DataBind();
        }

        public string showstop(string id)
        {
            int tempid;
            string returnstr = "";
            tempid = DataConverter.CLng(id);
            M_Trademark temp = bll.GetTrademarkByid(tempid);
            int isstop = temp.Isuse;
            if (isstop == 1)
            {
                returnstr = "<a href=?menu=stop&id=" + id + ">禁用</a>";
            }
            else if (isstop != 1)
            {
                returnstr = "<a href=?menu=qiyong&id=" + id + ">启用</a>";
            }
            return returnstr;
        }
        public string showstop2(string id)
        {
            int tempid;
            string returnstr = "";
            tempid = DataConverter.CLng(id);
            M_Trademark temp = bll.GetTrademarkByid(tempid);
            int isstop = temp.Isuse;
            if (isstop == 1)
            {
                returnstr = "<font color=blue>√</font>";
            }
            else if (isstop != 1)
            {
                returnstr = "<font color=red>×</font>";
            }
            return returnstr;
        }

        public string showtop(string id)
        {
            int tempid;
            string returnstr = "";
            tempid = DataConverter.CLng(id);
            M_Trademark temp = bll.GetTrademarkByid(tempid);
            int istop = temp.Istop;

            if (istop == 1)
            {
                returnstr = "<a href=?menu=notop&id=" + id + ">解固</a>";
            }
            else if (istop != 1)
            {
                returnstr = "<a href=?menu=istop&id=" + id + ">固顶</a>";
            }
            return returnstr;
        }

        public string showtop2(string id)
        {
            int tempid;
            string returnstr = "";
            tempid = DataConverter.CLng(id);
            M_Trademark temps = bll.GetTrademarkByid(tempid);
            int Istop = temps.Istop;
            if (Istop == 1)
            {
                returnstr = "<font color=blue>固</font>";
            }
            else if (Istop != 1)
            {
                returnstr = "&nbsp;&nbsp;";
            }
            return returnstr;
        }

        public string showjian(string id)
        {
            int tempid;
            string returnstr = "";
            tempid = DataConverter.CLng(id);
            M_Trademark temp = bll.GetTrademarkByid(tempid);
            int isjian = temp.Isbest;

            if (isjian == 1)
            {
                returnstr = "<a href=?menu=nojian&id=" + id + ">解荐</a>";
            }
            else if (isjian != 1)
            {
                returnstr = "<a href=?menu=tuijian&id=" + id + ">推荐</a>";
            }
            return returnstr;
        }

        public string showjian2(string id)
        {
            int tempid;
            string returnstr = "";
            tempid = DataConverter.CLng(id);
            M_Trademark temp = bll.GetTrademarkByid(tempid);
            int isjian = temp.Isbest;

            if (isjian == 1)
            {
                returnstr = "<font color=blue>荐</font>";
            }
            else if (isjian != 1)
            {
                returnstr = "&nbsp;&nbsp;";
            }
            return returnstr;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string itemdata = Request.Form["idchk"];

            if (!string.IsNullOrEmpty(itemdata) && bll.DeleteByList(itemdata) == true)
            {
                Response.Write("<script language=javascript>alert('批量删除成功!');location.href='TrademarkManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量删除失败!');location.href='TrademarkManage.aspx';</script>");
            }
        }
    }
}