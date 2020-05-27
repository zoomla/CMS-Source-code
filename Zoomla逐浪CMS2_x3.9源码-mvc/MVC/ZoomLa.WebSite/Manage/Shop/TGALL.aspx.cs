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
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class TGALL : CustomerPageAction
    {
        B_ZL_GroupBuy Groupbuy = new B_ZL_GroupBuy();
        M_ZL_GroupBuy mgb = new M_ZL_GroupBuy();
        B_Product pll = new B_Product();
        B_BindPro bll = new B_BindPro();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();

            int id = DataConverter.CLng(Request.QueryString["shopid"]);
            DataTable gby = Groupbuy.GetGroupBuyByShopID(id);
            if (gby != null && gby.Rows.Count > 0)
            {
                Page_list(gby);
            }
            Call.SetBreadCrumb(Master, "<li>商城管理</li><li>团购列表</li>");
            view.InnerHtml = "<table width='80%' height='48' style='font-size:12px'><tr><td width='10%' id='id0'>价格：</td>";
            string tr1 = "";
            string tr2 = "<td width='10%' height='21' style='font-size:12px'>人数：</td>";
            if (gby != null)
            {
                int index = 1;
                foreach (DataRow dr in gby.Rows)
                {
                    int trnumber = DataConverter.CLng(dr["number"]);
                    tr1 += "<td width='14%' id='id1'  style='background-color:#C" + 2 * (gby.Rows.Count - index + 1) + "0; font-size:12px' align='center'>￥" + DataConverter.CDouble(dr["price"]).ToString("0.00") + "元</td>";
                    //if(index==1) 从一开始
                    if (index > trnumber) //从你设置的最低价格开始
                    {
                        tr2 += "<td width='14%' align='center' style='font-size:12px'><span style='float:left'>1</span><span style='float:right'>" + dr["number"] + "</span></td>";
                    }
                    else
                    {
                        tr2 += "<td width='14%' align='center' style='font-size:12px'><span style='float:left'>" + dr["number"] + "</span></td>";
                    }
                    index++;
                }
            }
            view.InnerHtml += tr1 + "</tr><tr>" + tr2 + " </tr></table>";

        }

        #region 通用分页过程
        /// <summary>
        /// 通用分页过程　by h.
        /// </summary>
        /// <param name="Cll"></param>
        public void Page_list(DataTable Cll)
        {
            Pagetable.DataSource = Cll;
            Pagetable.DataBind();
        }
        #endregion

        #region 取消按钮
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>window.close();</script>");
        }
        #endregion

        #region 根据ID删除
        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Pagetable_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                int id = DataConverter.CLng(e.CommandArgument);
                Groupbuy.DelGroupID(id);
                DataTable gby = Groupbuy.GetGroupBuyByShopID(DataConverter.CLng(Request.QueryString["ShopID"]));
                Page_list(gby);

            }
        }
        #endregion

        #region 总价
        /// <summary>
        /// 总价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Pagetable_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblCount = e.Item.FindControl("labelcount") as Label;
                Label lblNum = e.Item.FindControl("lblNum") as Label;
                Label lblPrice = e.Item.FindControl("lblPrice") as Label;
                lblCount.Text = (DataConverter.CLng(lblNum.Text) * DataConverter.CDouble(lblPrice.Text)).ToString() + '元';
            }
        }
        #endregion

        #region 调用价格对比图
        /// <summary>
        /// 调用价格对比图
        /// </summary>
        public void GetPricePhotes()
        {
            int id = DataConverter.CLng(Request.QueryString["shopid"]);
            DataTable gby = Groupbuy.GetGroupBuyByShopID(id);
            view.InnerHtml = "<table width='80%' height='48'><tr><td width='10%' id='id0'>价格：</td>";
            string tr1 = "";
            string tr2 = "<td width='10%' height='21'>人数：</td>";
            if (gby != null)
            {
                int index = 1;
                foreach (DataRow dr in gby.Rows)
                {
                    int trnumber = DataConverter.CLng(dr["number"]);
                    tr1 += "<td width='14%' id='id1'  style='background-color:#C" + 3 * (gby.Rows.Count - index + 1) + "0; font-size:12px' align='center'>￥" + DataConverter.CDouble(dr["price"]).ToString("0.00") + "元</td>";
                    tr2 += "<td width='14%' align='center' style='font-size:12px'>" + dr["number"] + "</td>";
                    index++;
                }
            }
            view.InnerHtml += tr1 + "</tr><tr>" + tr2 + " </tr></table>";
        }
        #endregion
    }
}