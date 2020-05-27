using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Data;
using System.Data.SqlClient;
namespace ZoomLaCMS.Manage.Shop
{
    public partial class Balance : CustomerPageAction
    {
        B_Product bp = new B_Product();
        B_ArticlePromotion bap = new B_ArticlePromotion();
        B_User bu = new B_User();
        B_OrderList bol = new B_OrderList();
        B_CartPro bcp = new B_CartPro();
        B_Rebates brb = new B_Rebates();
        B_Admin ba = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            ba.CheckIsLogin();


            int id = DataConverter.CLng(Request["id"]);

            if (Request["item"] != null && Request["item"] != "")
            {
                SafeSC.CheckIDSEx(Request["item"]);
                DataTable dt = new DataTable();
                M_CartPro mcp = bcp.GetCartProByid(DataConverter.CLng(Request["item"].Split(',')[0]));
                M_ArticlePromotion map = bap.GetSelectBySqlParams("select * from ZL_ArticlePromotion where CartProId=" + mcp.ID, null);

                M_UserInfo mui = bu.GetUserByUserID(map.PromotionUserId);
                if (mcp.ID <= 0 || map.Id <= 0)
                {
                    function.Alert("参数错误!");
                    return;
                }
                if (dt == null) return;


                Label2.Text = dt.Rows[0]["moneysum"].ToString();
                Label1.Text = mui.UserName.ToString();
                Label3.Value = Label3.Value;
                label4.Text = (DataConverter.CDouble(Label2.Text) * DataConverter.CDouble(Label3.Value) / 100).ToString();
            }
            else if (id > 0)
            {

                M_ArticlePromotion map = bap.GetSelect(id);
                M_UserInfo mui = bu.GetUserByUserID(map.PromotionUserId);
                M_CartPro mcp = bcp.GetCartProByid(map.CartProId);
                hfId.Value = map.Id.ToString();
                Label2.Text = mcp.AllMoney.ToString();
                Label1.Text = mui.UserName;
                Label3.Value = Label3.Value;
                label4.Text = (DataConverter.CDouble(Label2.Text) * DataConverter.CDouble(Label3.Value) / 100).ToString();
            }
            Call.SetBreadCrumb(Master, "<li>商城管理</li><li>推广</li>");
        }

        #region 计算总价
        public string GetCount(string count)
        {
            M_CartPro mcp = bcp.GetCartProByid(DataConverter.CLng(count));
            if (mcp != null && mcp.ID > 0)
            {
                return mcp.AllMoney.ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (this.Label3.Value.Trim() == "")
            {
                Label5.Text = "<font style='color:red'></font>";
                return;
            }
            else
            {

                Label5.Text = "";
            }
            string[] proid;
            if (Request["item"] != null && Request["item"] != "")
            {
                proid = Request["item"].Split(',');

                M_ArticlePromotion map = new M_ArticlePromotion();
                for (int i = 0; i < proid.Length; i++)
                {
                    M_Rebates mr = new M_Rebates();
                    M_UserInfo mui = bu.GetUserIDByUserName(Label1.Text.Trim());
                    M_CartPro mcp = bcp.GetCartProByid(DataConverter.CLng(proid[i]));
                    map = bap.GetSelectBySqlParams("select * from ZL_ArticlePromotion where cartproid=" + mcp.ID, null);

                    mr.Money = mcp.AllMoney;
                    mr.UserID = mui.UserID;
                    mr.Scale = DataConverter.CFloat(this.Label3.Value) / 100;
                    mr.BalanceMoney = (mcp.AllMoney * DataConverter.CFloat(Label3.Value) / 100);
                    map.RebatesId = brb.GetInsert(mr);
                    map.IsBalance = true;
                    bap.GetUpdate(map);

                }

                if (map.RebatesId > 0)
                {
                    function.Script(this, "alert('结算成功！');gotourl('shop/OrderBlanace.aspx?id=" + map.PromotionUserId + "&balance=0');Dialog.close();");
                }
                else
                {
                    function.Script(this, "alert('添加失败！');gotourl('shop/OrderBlanace.aspx?id=" + map.PromotionUserId + "&balance=0');Dialog.close();");
                }
            }
            else
            {


                M_Rebates mr = new M_Rebates();
                M_ArticlePromotion map = bap.GetSelect(DataConverter.CLng(hfId.Value));
                mr.Money = DataConverter.CLng(this.Label2.Text);
                mr.UserID = map.PromotionUserId;
                mr.Scale = DataConverter.CFloat(this.Label3.Value) / 100;
                mr.BalanceMoney = DataConverter.CDouble(this.label4.Text);




                map.RebatesId = brb.GetInsert(mr);
                map.IsBalance = true;
                bap.GetUpdate(map);
                if (map.RebatesId > 0)
                {
                    function.Script(this, "alert('结算成功！');gotourl('shop/OrderBlanace.aspx?id=" + map.PromotionUserId + "&balance=0');Dialog.close();");
                }
                else
                {
                    function.Script(this, "alert('添加失败！');gotourl('shop/OrderBlanace.aspx?id=" + map.PromotionUserId + "&balance=0');Dialog.close();");
                }
            }
        }
    }
}