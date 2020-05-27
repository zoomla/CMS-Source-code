﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintOrder.aspx.cs" Inherits="Manage_Shop_Addon_PrintOrder" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>订单打印</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
    <div class="plogo"><img src="<%=Call.LogoUrl %>" /></div>
    <div class="poinfo">
        <div class="pull-left">订单编号：<asp:Label runat="server" ID="OrderNo_T"></asp:Label></div>
        <div class="pull-right">订购时间： <asp:Label runat="server" ID="AddTime_T"></asp:Label></div>
        <div class="clearfix"></div>
    </div>
    <div class="inforow">客户姓名：<asp:Label runat="server" ID="ReUserName_L"></asp:Label></div>
    <div class="inforow">联系方式：<asp:Label runat="server" ID="Mobile_L"></asp:Label></div>
    <div class="inforow">客户地址：<asp:Label runat="server" ID="Address_L"></asp:Label></div>
    <hr />
    <div class="prolist">
        <table class="table table-bordered table-striped">
            <tr><td class="td_l">商品编号</td><td>商品名称</td><td>数量</td><td>商品金额</td></tr>
            <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
                <ItemTemplate>
                   <tr><td><%#Eval("ProCode") %></td><td><%#Eval("ProName") %></td><td><%#Eval("ProNum") %></td><td><%#Eval("AllMoney","{0:f2}") %></td></tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="price_div">
            <div><span class="sp_l">商品总金额：</span>
                 <span class="sp_r"><asp:Label runat="server" ID="P_Pro_L"></asp:Label></span>
            </div>
<%--            <div>优惠券：<asp:Label runat="server" ID="P_Arrive_L"></asp:Label></div>
            <div>积分折扣：<asp:Label runat="server" ID="P_Point_L"></asp:Label></div>--%>
            <div><span class="sp_l">运费：</span>
                 <span class="sp_r"><asp:Label runat="server" ID="P_Exp_L"></asp:Label></span>
            </div>
            <div>
                <span class="sp_l">订单支付金额：</span>
                <span class="sp_r"><strong><i class="fa fa-rmb"></i><asp:Label runat="server" ID="TotalMoney_L"></asp:Label></strong></span>
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="text-right margin_t5 opbtns noprint">
            <a href="javascript:;" class="btn btn-lg btn-primary" onclick="window.print();"><i class="fa fa-print"></i> 打印</a>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style media="print" type="text/css">
    .noprint {display:none;}
</style>
<style type="text/css">
hr {margin-top:5px;margin-bottom:5px;height:1px;background-color:#ddd;}
.plogo {border-bottom:2px solid #808080;padding:10px;}
.poinfo {border-bottom:1px solid #ddd;padding-top:10px;padding-bottom:10px;}
.inforow {height:30px;line-height:30px;}
.price_div {width:300px;float:right;}
.sp_l {display:inline-block;text-align:right;width:120px;height:25px;line-height:25px;}
.sp_r {display:inline-block;text-align:right;width:170px;height:25px;line-height:25px;}
.opbtns {border-top:1px solid #ddd;border-bottom:1px solid #ddd;padding:5px 0;}
</style>
</asp:Content>