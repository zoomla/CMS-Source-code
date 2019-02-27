namespace ZoomLaCMS.Plugins.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Site;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.Model;

    /*
     * 服务购买页面,一份商品对应一份订单
     */
    public partial class ViewProduct : System.Web.UI.Page
    {
        //订单
        B_OrderList OCl = new B_OrderList();
        M_OrderList Odata = new M_OrderList();
        B_Payment payBll = new B_Payment();
        //购物车
        B_CartPro bcart = new B_CartPro();
        B_Product bll = new B_Product();
        B_User buser = new B_User();

        B_Product proBll = new B_Product();
        B_Site_SiteList siteBll = new B_Site_SiteList();
        M_Product proModel = new M_Product();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.Params["ProID"]))
                {
                    GetToPay(Request.Params["ProID"], DataConverter.CLng(Request.Params["num"]));
                }
                else
                {
                    DataBind();
                }
            }
        }
        //----EGV
        private void DataBind(string key = "")
        {
            DataTable dt = proBll.GetProByProClass(M_Product.ClassType.IDC);
            if (!string.IsNullOrEmpty(key))
            {
                dt.DefaultView.RowFilter = "ProName like '%" + key + "%'";
                dt = dt.DefaultView.ToTable();
            }
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void searchBtn_Click(object sender, EventArgs e)
        {
            DataBind(keyWord.Text.Trim());
        }
        //确定购买
        protected void sureBtn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            proModel = proBll.GetproductByid(Convert.ToInt32(dataField.Value));
            int num = Convert.ToInt32(proNum.Text.Trim());

            Odata.Ordersamount = Convert.ToDouble(num * proModel.LinPrice);
            Odata.OrderNo = B_OrderList.CreateOrderNo(M_OrderList.OrderEnum.IDC);
            Odata.Ordertype = (int)M_OrderList.OrderEnum.IDC; ;//服务，主机等订单
            Odata.Receiver = mu.UserName;
            Odata.Reuser = mu.UserName;
            Odata.Userid = mu.UserID;
            //Odata.AddUser = siteListDP.SelectedValue;//跟单员,此处记录对应ID
            //Odata.Internalrecords = siteListDP.SelectedItem.Text;//内部记录,此处用来存与主机的关联信息
            //添加订单，添加数据库购物车
            Odata.id = OCl.Adds(Odata);
            if (Odata.id > 0)
            {
                //写入购物车记录
                M_CartPro cartModel = new M_CartPro();
                cartModel.Orderlistid = Odata.id;
                cartModel.ProID = proModel.ID;
                cartModel.Proname = proModel.Proname;
                cartModel.Shijia = proModel.LinPrice;
                cartModel.Pronum = num;
                cartModel.AllMoney = Odata.Ordersamount;
                cartModel.EndTime = DateTime.Now;//支付时再更新日期, proBll.GetEndTime(proModel, num);
                cartModel.type = (int)M_OrderList.OrderEnum.IDC;
                bcart.Add(cartModel);
                Response.Redirect("~/PayOnline/Orderpay.aspx?OrderCode=" + Odata.OrderNo);
            }
            else
            {
                function.WriteErrMsg("目标订单:" + Odata.OrderNo + "不存在!!!");
            }
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void EGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            EGV.EditIndex = -1;
            EGV.Columns[4].Visible = false;
            DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit2":
                    EGV.EditIndex = Convert.ToInt32(e.CommandArgument as string);
                    EGV.Columns[4].Visible = true;
                    break;
                case "wantPay":
                    GetToPay(e.CommandArgument as string, 1);
                    break;
                default: break;
            }
            DataBind();
        }
        public string GetServerPeriod(object period, object type)
        {
            //return bll.GetServerPeriod(Convert.ToInt32(period), Convert.ToInt32(type));
            return "";
        }
        //进入购买页面
        public void GetToPay(string proID, int num)
        {
            num = num < 1 ? 1 : num;
            dataField.Value = proID;
            proModel = proBll.GetproductByid(Convert.ToInt32(proID));
            if (proModel.ProClass != (int)M_Product.ClassType.IDC) function.WriteErrMsg("该商品并不是IDC服务商品,不允许在该页购买!!!");
            viewDiv.Visible = false;
            payDiv.Visible = true;
            proNameL.Text = proModel.Proname;
            proNameL.NavigateUrl = "/Shop.aspx?ID=" + proModel.ItemID;
            proNameL.Target = "_ViewDetail";
            proNum.Text = num.ToString();
            //proPeriod.Text = bll.GetServerPeriod(proModel.ServerPeriod, proModel.ServerType);
            proRemind.Text = proModel.ExpRemind > 0 ? "提前" + proModel.ExpRemind + "天" : "不提醒";
            proPic.Src = proModel.ThumbPath;
            proPrice.Text = proModel.LinPrice.ToString(".00");
            proDetail.Text = proModel.Proinfo;
        }
    }
}