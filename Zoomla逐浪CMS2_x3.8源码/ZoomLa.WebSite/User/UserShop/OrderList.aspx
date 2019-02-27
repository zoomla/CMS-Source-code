<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="OrderList.aspx.cs" Inherits="User_UserShop_OrderList" ClientIDMode="Static" ValidateRequest="false" %>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>我的店铺</title>
<link href="/App_Themes/V3.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="store"></div> 
<div class="container margin_t5">
<ol class="breadcrumb">
	<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
	<li><a href="ProductList.aspx">我的店铺</a></li>
	<li class="active">我的店铺订单</li> 
</ol>
</div>
<div class="container btn_green">
<uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
<div class="top_opbar">
    <div class="input-group pull-left text_300">
        <span class="input-group-addon">快速筛选</span>
        <asp:DropDownList ID="QuickSearch_DP" CssClass="form-control text_md" runat="server" AutoPostBack="True" OnSelectedIndexChanged="QuickSearch_DP_SelectedIndexChanged">
            <asp:ListItem Value="0" Text="<%$Resources:L,所有订单 %>" Selected="True"></asp:ListItem>
            <asp:ListItem Value="2" Text="<%$Resources:L,今天的新订单 %>"></asp:ListItem>
            <asp:ListItem Value="4" Text="<%$Resources:L,最近10天内的新订单 %>"></asp:ListItem>
            <asp:ListItem Value="5" Text="<%$Resources:L,最近一个月内的新订单 %>"></asp:ListItem>
            <asp:ListItem Value="6" Text="<%$Resources:L,未确认的订单 %>"></asp:ListItem>
            <asp:ListItem Value="7" Text="<%$Resources:L,未付款的订单 %>"></asp:ListItem>
            <asp:ListItem Value="8" Text="<%$Resources:L,未付清的订单 %>"></asp:ListItem>
            <asp:ListItem Value="9" Text="<%$Resources:L,未送货的订单 %>"></asp:ListItem>
            <asp:ListItem Value="10" Text="<%$Resources:L,未签收的订单 %>"></asp:ListItem>
            <asp:ListItem Value="11" Text="<%$Resources:L,未结清的订单 %>"></asp:ListItem>
            <asp:ListItem Value="12" Text="<%$Resources:L,未开发票的订单 %>"></asp:ListItem>
            <asp:ListItem Value="13" Text="<%$Resources:L,已经作废的订单 %>"></asp:ListItem>
            <asp:ListItem Value="14" Text="<%$Resources:L,暂停处理的订单 %>"></asp:ListItem>
            <asp:ListItem Value="15" Text="<%$Resources:L,已发货的订单 %>"></asp:ListItem>
            <asp:ListItem Value="16" Text="<%$Resources:L,已签收的订单 %>"></asp:ListItem>
            <asp:ListItem Value="17" Text="<%$Resources:L,已结清的订单 %>"></asp:ListItem>
            <asp:ListItem Value="18" Text="<%$Resources:L,已申请退款的订单 %>"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="input-group pull-left" style="width:505px;">
        <span class="input-group-addon">高级查询</span>
        <asp:DropDownList ID="SkeyType_DP" CssClass="form-control text_md" runat="server" style="border-right:none;">
            <asp:ListItem Selected="True" Value="1" Text="<%$Resources:L,订单编号 %>"></asp:ListItem>
            <asp:ListItem Value="2" Text="<%$Resources:L,客户名称 %>"></asp:ListItem>
            <asp:ListItem Value="3" Text="<%$Resources:L,用户名 %>"></asp:ListItem>
            <asp:ListItem Value="4" Text="<%$Resources:L,收货人 %>"></asp:ListItem>
            <asp:ListItem Value="5" Text="<%$Resources:L,联系地址 %>"></asp:ListItem>
            <asp:ListItem Value="6" Text="<%$Resources:L,联系电话 %>"></asp:ListItem>
            <asp:ListItem Value="7" Text="<%$Resources:L,下单时间 %>"></asp:ListItem>
            <asp:ListItem Value="8" Text="<%$Resources:L,备注留言 %>"></asp:ListItem>
            <asp:ListItem Value="9" Text="<%$Resources:L,商品名称 %>"></asp:ListItem>
            <asp:ListItem Value="10" Text="<%$Resources:L,收货人邮箱 %>"></asp:ListItem>
            <asp:ListItem Value="11" Text="<%$Resources:L,发票信息 %>"></asp:ListItem>
            <asp:ListItem Value="12" Text="<%$Resources:L,内部记录 %>"></asp:ListItem>
            <asp:ListItem Value="13" Text="<%$Resources:L,跟单员 %>"></asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox runat="server" ID="Skey_T" class="form-control text_md" placeholder="<%$Resources:L,请输入需要搜索的内容 %>" />
        <span class="input-group-btn">
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="Skey_Btn" OnClick="Skey_Btn_Click"><span class="fa fa-search"></span></asp:LinkButton>
        </span>
    </div>
    <div class="clearfix"></div>
</div>
</div>
<div class="container u_cnt">
    <table id="store_tb" class="table table-bordered table-hover">
        <tr>
            <td></td>
            <td>订单编号</td>
            <td>客户名称</td>
            <td>用户名</td>
            <td>下单时间</td>
            <td>实际金额</td>
            <td>收款金额</td>
            <td>需要发票</td>
            <td>订单状态</td>
            <td>支付方式</td>
            <td>付款状态</td>
            <td>物流状态</td>
            <td>操作</td>
        </tr>
        <ZL:ExRepeater runat="server" ID="Store_RPT" PageSize="10" PagePre="<tr id='page_tr'><td><input type='checkbox' id='chkAll'/></td><td colspan='12' id='page_td'>" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr ondblclick="location='Orderlistinfo.aspx?id=<%#Eval("ID") %>';">
                    <td><%#GetChkStatus()%></td>
                    <td><a href="OrderListInfo.aspx?ID=<%#Eval("ID") %>"><%#Eval("OrderNo") %></a></td>
                    <td><%#Eval("Reuser")%></td>
                    <td><%#Eval("Reuser")%></td>
                    <td><%#Eval("AddTime") %></td>
                    <td><%#GetPriceStr(Eval("id", "{0}"))%></td>
                    <td><%#Eval("Receivablesamount","{0:F2}")%></td>
                    <td><%#IsNeedInvo() %></td>
                    <td><%#formatzt(Eval("OrderStatus", "{0}"),"0")%></td>
                    <td><%#formatzt(Eval("Delivery","{0}"),"3") %></td>
                    <td><%#formatzt(Eval("Paymentstatus", "{0}"),"1")%></td>
                    <td><%#formatzt(Eval("StateLogistics", "{0}"),"2")%></td>
                    <td><%#GetOP() %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr>
                    <td colspan="13"><span>实际金额合计:</span><span class="rd_red_l"><%#GetTotalSum() %></span></td>
                </tr>
            </FooterTemplate>
        </ZL:ExRepeater>
        <asp:HiddenField runat="server" ID="TotalSum_Hid" />
    </table>
<div class="btn_green"><asp:Button ID="Bat_Del" class="btn btn-primary" Text="删除订单" runat="server" OnClick="BatDel_Btn_Click" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('不可恢复性删除数据,你确定将该数据删除吗？')}" /></div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/SelectCheckBox.js"></script>
<script>
	$(function () {
		$("#chkAll").click(function () {
			selectAllByName(this, "idchk");
		});
	})
	function IsSelectedId() {
		var checkArr = $("input[type=checkbox][name=idchk]:checked");
		if (checkArr.length > 0)
			return true
		else
			return false;
	}
	function ShowDetail(orderno) {
		comdiag.maxbtn = false;
		comdiag.reload = true;
		ShowComDiag("/User/Order/OrderDetails.aspx?OrderNo=" + orderno, "订单明细");
	}
	function CloseDetail() {
		CloseComDiag();
	}
	function ShowElement(id) {
		$("#" + id).show();
	}
</script>
</asp:Content>
