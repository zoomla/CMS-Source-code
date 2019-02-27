using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data.SqlClient;
namespace ZoomLaCMS.Manage.Shop
{
    public partial class MtrlsMrktng : CustomerPageAction
    {
        protected B_Rebates brb = new B_Rebates();
        protected B_User bu = new B_User();
        private B_CartPro bcp = new B_CartPro();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            int id = DataConverter.CLng(Request.QueryString["id"]);
            this.MoneyID.Text = formatcs(brb.GetBalanceMoney(id).ToString(), "1", "4");
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "delete")
                {
                    brb.DeleteByGroupID(DataConverter.CLng(Request.QueryString["id"]));
                }
                RepNodeBind();
            }
            Call.SetBreadCrumb(Master, "<li>商城管理</li><li><a href='MtrlsMrktng.aspx'>商品推广管理</a></li>");
        }


        #region 截取字符串
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="money"></param>
        /// <param name="ProClass"></param>
        /// <param name="point"></param>
        /// <returns></returns>

        public string formatcs(string money, string ProClass, string point)
        {
            string outstr;
            double doumoney, tempmoney;
            doumoney = DataConverter.CDouble(money);
            tempmoney = System.Math.Round(doumoney, 2);
            outstr = tempmoney.ToString();
            if (ProClass != "3")
            {
                if (outstr.IndexOf(".") == -1)
                {
                    outstr = outstr + ".00";
                }
            }
            else
            {
                outstr = point;
            }
            return outstr;
        }
        #endregion
        #region 数据绑定
        /// <summary>
        /// 数据邦定
        /// </summary>
        private void RepNodeBind()
        {
            brb = new B_Rebates();
            int id = DataConverter.CLng(Request.QueryString["id"]);
            SqlParameter[] sp = new SqlParameter[]
            {
             new SqlParameter("@id", id)
            };
            DataTable mmm = brb.GetSelectTableBySql("select * from ZL_Rebates where userid=@id", sp);
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = mmm.DefaultView;
            EGV.DataSource = pds;
            this.EGV.DataBind();
        }
        #endregion

        #region 页数
        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            ViewState["page"] = "1";
            this.RepNodeBind();
        }
        #endregion

        #region 读取User
        public string getUserNameById(int id)
        {
            return bu.GetUserByUserID(id).UserName;
        }
        #endregion
    }
}