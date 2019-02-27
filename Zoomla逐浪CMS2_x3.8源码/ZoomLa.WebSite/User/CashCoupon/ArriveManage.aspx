<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="ArriveManage.aspx.cs" Inherits="User_CashCoupon_ArriveManage" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>优惠劵管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <div id="pageflag" data-nav="group" data-ban="ArriveManage"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a href="/User/">会员中心</a></li>
        <li><a href="/User/Info/UserInfo.aspx">账户管理</a></li>
        <li>优惠劵管理</li> 
    </ol>
</div>
<div class="container margin_t5 arrive">
   <div runat="server" id="empty_div" visible="false">
       <div class="alert alert-info">没有可用的优惠券</div>
   </div>
   <div runat="server" id="data_div">
        <ul class="list-unstyled">
        <ZL:ExRepeater runat="server" ID="RPT" OnItemCommand="RPT_ItemCommand" PageSize="12"
            PagePre="<li class='clearfix'></li></ul><div class='text-center'>" PageEnd="</div>">
            <ItemTemplate>
                <li class="a_item boxborder <%:State.Equals(1)?"":"disabled" %>" title="点击领取">
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
                        <a href="/Class_2/Default.aspx" target="_blank">立即使用</a>
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
   </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>