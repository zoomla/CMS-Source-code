<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShopGuide.ascx.cs" Inherits="Manage_I_ASCX_ShopGuide" %>
<div class="tvNavDiv">
    <div class="input-group">
        <input type="text" ID="keyWord" class="form-control" onkeydown="return ASCX.OnEnterSearch('<%:CustomerPageAction.customPath2+"Shop/ProductManage.aspx?keyWord=" %>',this);" placeholder="请输入IDC订单号" />
        <span class="input-group-btn">
            <button class="btn btn-default" type="button" onclick="ASCX.Search('<%:CustomerPageAction.customPath2+"Shop/ProductManage.aspx?keyWord=" %>','keyWord');"><span class="fa fa-search"></span></button>
        </span>
    </div>
    <div class="left_ul">
        <ul>
            <li><a href="CartManage.aspx"><%=lang.LF("购物车记录")%></a></li>
            <li><a href="OrderList.aspx"><%=lang.LF("商城订单")%></a></li>
            <li><a href="UserOrderlist.aspx?type=0"><%=lang.LF("店铺订单")%></a></li>
            <li><a href="/User/Shopfee/OrderList.aspx"><%=lang.LF("代购订单")%></a></li>
            <li><a href="OrderSql.aspx"><%=lang.LF("账单管理")%></a></li>
            <li><a href="OtherOrder/Hotel_Order_Manager.aspx"><%=lang.LF("酒店订单管理")%></a></li>
            <li><a href="OtherOrder/Flight_Order_Management.aspx"><%=lang.LF("机票订单管理")%></a></li>
            <li><a href="OtherOrder/TravelOrder_Manager.aspx"><%=lang.LF("旅游订单管理")%></a></li>
            <li><a href="OtherOrder/DomainOrder.aspx?OrderType=5">域名订单管理</a></li>
            <li><a href="OtherOrder/IDCOrder.aspx?OrderType=7">IDC服务订单</a></li>
            <li><a href="FillOrder.aspx">补订单</a></li>
            <li><a href="LocationReport.aspx"><%=lang.LF("省市报表")%></a>
            <li><a href="MonthlyReport.aspx"><%=lang.LF("月报表")%></a></li>
            <li><a href="OrderConfi.aspx"><%=lang.LF("订单信息配置")%></a></li>
            <li><a href="SystemOrderModel.aspx?Type=site"><%=lang.LF("网站订单参数")%></a></li>
            <li><a href="SystemOrderModel.aspx?Type=shop"><%=lang.LF("店铺订单参数")%></a></li>
        </ul>
    </div>
</div>
