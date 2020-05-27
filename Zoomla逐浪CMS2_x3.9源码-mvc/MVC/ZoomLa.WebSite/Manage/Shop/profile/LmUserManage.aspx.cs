using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZoomLaCMS.Manage.Shop.profile
{
    public partial class LmUserManage : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "shop/ProductManage.aspx'>商城管理</a></li><li><a href='StatisticsBriefing.aspx'>推广返利</a></li><li>联盟会员</li>");
            if (!IsPostBack)
            {
                Bind(1);
            }
        }
        /// <summary>
        /// 个人所得佣金
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string[] GetLi(string userid)
        {
            return null;
        }
        /// <summary>
        /// 单击佣金
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetCli(string userid)
        {
            return GetLi(userid)[1] + "元";
        }
        /// <summary>
        /// 销售佣金
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetXs(string userid)
        {
            return GetLi(userid)[0] + "元";
        }

        /// <summary>
        /// 引导
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetGuide(string userid)
        {
            return GetLi(userid)[2] + "元";
        }

        /// <summary>
        /// 佣金总额
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetYj(string userid)
        {
            return (Convert.ToDouble(GetLi(userid)[0]) + Convert.ToDouble(GetLi(userid)[1])).ToString() + "元";
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="page"></param>
        public void Bind(int page)
        {

        }


        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Toppage_Click(object sender, EventArgs e)
        {
            Bind(1);
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Nextpage_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ViewState["DPage"]) > 1)
                Bind(Convert.ToInt32(ViewState["DPage"]) - 1);
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Downpage_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ViewState["DPage"]) < Convert.ToInt32(ViewState["PageCount"]))
                Bind(Convert.ToInt32(ViewState["DPage"]) + 1);
        }
        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Endpage_Click(object sender, EventArgs e)
        {
            Bind(Convert.ToInt32(ViewState["PageCount"]));
        }
        /// <summary>
        /// 显示数量改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            Bind(Convert.ToInt32(ViewState["DPage"]));
        }
        /// <summary>
        /// 下拉选定指定页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind(Convert.ToInt32(DropDownList3.SelectedValue));
        }
        /// <summary>
        /// 用户上级
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        public string Hrefnone(string hid)
        {
            if (hid == "0")
            {
                return "%";
            }
            else
            {
                return CustomerPageAction.customPath2 + "User/Userinfo.aspx?id=" + hid;
            }
        }
        /// <summary>
        /// gridview鼠标经过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.className='tdbgmouseover'");
                e.Row.Attributes.Add("onmouseout", "this.className='tdbg'");
                e.Row.Style.Add("cursor", "pointer");
            }
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
    }
}