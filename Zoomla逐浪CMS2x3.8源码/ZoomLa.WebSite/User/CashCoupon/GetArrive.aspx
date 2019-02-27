<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetArrive.aspx.cs" Inherits="User_CashCoupon_GetArrive" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>优惠券领取</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="ArriveManage"></div>
<div class="container arrive">
    <ol class="breadcrumb">
        <li><a href="/User/">会员中心</a></li>
        <li><a href="ArriveManage.aspx">我的优惠券</a></li>
        <li>领取优惠券</li>
    </ol>
    <div class="margin_t5">
        <div runat="server" id="empty_div" visible="false">
            <div class="alert alert-info">没有待领的优惠券</div>
        </div>
        <div runat="server" id="data_div" visible="false">
             <ul class="list-unstyled">
            <ZL:ExRepeater runat="server" ID="RPT" OnItemCommand="RPT_ItemCommand" PageSize="12"
                PagePre="<li class='clearfix'></li></ul><div class='text-center'>" PageEnd="</div>">
                <ItemTemplate>
                    <li class="a_item boxborder" title="点击领取">
                        <div class="type">
                            <div>
                                <i class="fa fa-rmb"></i>
                                <span class="amount"><%#Eval("AMount","{0:f0}")%></span>
                                <span class="region"><%#GetMoneyRegion() %></span>
                            </div>
                            <div class="r_gray r_item">全平台可用</div>
                            <div class="r_gray r_item"><%#Eval("AgainTime","{0:yyyy.MM.dd}")+"-"+Eval("EndTime","{0:yyyy.MM.dd}") %></div>
                        </div>
                        <div class="opbtns">
                            <b></b>
                            <asp:LinkButton runat="server" CommandName="get" CommandArgument='<%#Eval("Flow") %>'>立即领取</asp:LinkButton>
                        </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>