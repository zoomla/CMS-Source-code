using System;
using System.IO;
using System.Web;
using System.Xml;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Runtime.Serialization.Json;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using Microsoft.Web.Administration;
//using URLRewriter;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.BLL.Site;
namespace ZoomLaCMS.Manage.Shop
{
    public partial class FillOrder : System.Web.UI.Page
    {
        public string userid, shopid;
        protected B_Admin badmin = new B_Admin();
        protected B_CartPro bcart = new B_CartPro();
        protected B_Product proBll = new B_Product();
        protected B_IDC_DomainOrder orderBll = new B_IDC_DomainOrder();
        protected B_User buser = new B_User();
        protected M_UserInfo muser = new M_UserInfo();
        protected B_OrderList OCl = new B_OrderList();
        protected M_OrderList Odata = new M_OrderList();
        protected M_Product proModel = new M_Product();
        public string bar = Resources.L.选择会员;
        protected void Page_Load(object sender, EventArgs e)
        {
            badmin.CheckIsLogin();
            EGV.txtFunc = txtPageFunc;
            EGV1.txtFunc = txtPageFunc1;
            if (!IsPostBack)
            {
                DataBind();
                DataBind1();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='ProductManage.aspx'>" + Resources.L.商城管理 + "</a></li><li><a href='OrderList.aspx'>" + Resources.L.订单管理 + "</a></li><li class='active'>" + Resources.L.补单 + "</li>");
        }
        private void DataBind(string keys = "")
        {
            EGV.DataSource = buser.Sel();
            EGV.DataBind();
        }
        private void DataBind1(string keys = "")
        {
            EGV1.DataSource = proBll.GetProByProClass(M_Product.ClassType.IDC);
            EGV1.DataBind();
        }
        //分页
        public void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = EGV.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = EGV.PageSize;
            }
            EGV.PageSize = pageSize;
            EGV.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind("");
        }
        public void txtPageFunc1(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = EGV1.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = EGV.PageSize;
            }
            EGV1.PageSize = pageSize;
            EGV1.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind1();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "select":
                    bar = Resources.L.商品选择;
                    this.shoplist.Visible = true;
                    this.userlist.Visible = false;
                    HiddenField1.Value = e.CommandArgument.ToString();
                    Call.SetBreadCrumb(Master, "<li>" + Resources.L.后台管理 + "</li><li>" + Resources.L.商城管理 + "</li><li><a href='OrderList.aspx'>" + Resources.L.订单管理 + "</a></li><li><a href='FillOrder.aspx'>" + Resources.L.补单 + "</a></li><li>" + bar + "</li>");
                    break;
            }
        }
        public string GetGroupName(string GroupID)
        {
            B_Group bgp = new B_Group();
            return bgp.GetByID(DataConverter.CLng(GroupID)).GroupName;
        }
        public string GetStatus(string Status)
        {
            switch (Status)
            {
                case "0":
                    return Resources.L.正常;
                case "1":
                    return Resources.L.锁定;
                case "2":
                    return Resources.L.待认证;
                case "3":
                    return Resources.L.双认证;
                case "4":
                    return Resources.L.邮件认证;
                case "5":
                    return Resources.L.待认证;
            }
            return Resources.L.正常;
        }
        //商品图片
        public string getproimg(string type)
        {
            string upload = SiteConfig.SiteOption.UploadDir.Replace("/", "") + "/";
            string restring;
            restring = "";
            if (string.IsNullOrEmpty(type))
            {
                restring = "<img src=/UploadFiles/nopic.gif border=0 width=60 height=45>";
            }
            if (!string.IsNullOrEmpty(type))
            {
                type = type.ToLower();
            }
            if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
            {
                if (type.StartsWith("http://", true, CultureInfo.CurrentCulture) || type.StartsWith("/", true, CultureInfo.CurrentCulture) || type.StartsWith(upload, true, CultureInfo.CurrentCulture))
                    restring = "<img src=../../" + type + " width=60 height=45 onerror=\"shownopic(this);\">";
                else
                {
                    restring = "<img src=../../" + upload + type + " border=0 width=60 height=45 onerror=\"shownopic(this);\">";
                }
            }
            else
            {
                restring = "<img src=/UploadFiles/nopic.gif border=0 width=60 height=45>";
            }
            return restring;
        }
        //商品价格
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
        protected void EGV1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "select":
                    bar = Resources.L.订单信息;
                    this.shoplist.Visible = false;
                    this.orderinfo.Visible = true;
                    HiddenField2.Value = e.CommandArgument.ToString();
                    Call.SetBreadCrumb(Master, "<li>" + Resources.L.后台管理 + "</li><li>" + Resources.L.商城管理 + "</li><li><a href='OrderList.aspx'>" + Resources.L.订单管理 + "</a></li><li><a href='FillOrder.aspx'>" + Resources.L.补单 + "</a></li><li>" + bar + "</li>");
                    break;
            }
        }
        protected void EGV1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV1.PageIndex = e.NewPageIndex;
            DataBind1();
        }
        //添加订单
        protected void Button1_Click(object sender, EventArgs e)
        {
            proModel = proBll.GetproductByid(Convert.ToInt32(HiddenField2.Value));
            muser = buser.SeachByID(Convert.ToInt32(HiddenField1.Value));
            Odata.OrderNo = "DD" + function.GetFileName();
            if (DropDownList1.SelectedValue == "1")
                Odata.Payment = 1;
            else
                Odata.Payment = 0;
            Odata.Ordersamount = Convert.ToDouble(proModel.LinPrice * Convert.ToInt32(TextBox1.Text));
            Odata.Ordertype = (int)M_OrderList.OrderEnum.IDC;
            Odata.Receiver = muser.UserName;
            Odata.Reuser = muser.UserName;
            Odata.Rename = muser.UserName;
            Odata.Userid = muser.UserID;
            //Odata.AddUser = siteListDP.SelectedValue;//跟单员,此处记录对应ID
            //Odata.Internalrecords = siteListDP.SelectedItem.Text;//内部记录,此处用来存与主机的关联信息
            //添加订单，添加数据库购物车
            if (OCl.Add(Odata))
            {
                DataTable tempDT = OCl.GetOrderbyOrderNo(Odata.OrderNo);//获取刚插入的ID，这个需要改,应该插入时返回最新的ID
                if (tempDT != null & tempDT.Rows.Count > 0)
                {
                    M_CartPro cartModel = new M_CartPro();
                    cartModel.Orderlistid = Convert.ToInt32(tempDT.Rows[0]["ID"]);
                    cartModel.Addtime = DateTime.Now;
                    //cartModel.EndTime = proBll.GetEndTime(proModel, Convert.ToInt32(TextBox1.Text));
                    cartModel.ProID = proModel.ID;
                    cartModel.Proname = proModel.Proname;
                    cartModel.Username = muser.UserName;
                    cartModel.Shijia = proModel.ShiPrice;
                    cartModel.Pronum = Convert.ToInt32(TextBox1.Text);
                    cartModel.AllMoney = Odata.Ordersamount;
                    cartModel.type = (int)M_OrderList.OrderEnum.IDC;
                    bcart.Add(cartModel);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');", true);
                    Response.Redirect("OtherOrder/IDCOrder.aspx?OrderType=7");
                }
                else
                {
                    function.WriteErrMsg(Resources.L.目标订单 + ":" + Odata.OrderNo + Resources.L.不存在 + "!!!");
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = (DataConvert.CLng(TextBox1.Text) + 1).ToString();
        }
        public string getshoptype(string id)
        {
            proModel = proBll.GetproductByid(DataConvert.CLng(id));
            if (proModel.Nodeid == DataConvert.CLng(StationGroup.NodeID))
                return Resources.L.IDC服务商品;
            else
                return Resources.L.普通商品;
        }
    }
}