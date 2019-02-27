<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderList.aspx.cs" MasterPageFile="~/User/Default.master" Inherits="User_Order_OrderList" %>
<%@ Register Src="~/User/ASCX/OrderTop.ascx" TagPrefix="ZL" TagName="OrderTop" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>我的订单</title>
<style>
.table {margin-bottom:0px;}
.orderlist .order-item{border:1px solid #ddd;margin-top:10px;}
.orderlist .gray9{color:#999;}
.orderlist .orderinfo { line-height:20px;}
.orderlist .shopinfo{line-height:20px;}
.orderlist .opinfo {line-height:20px;text-align:right;padding-right:15px;font-size:1.2em;color:gray;}
.orderlist .tips_div{background-color:#f5f5f5;}
.orderlist .top_div{line-height:30px; background-color:#f5f5f5;margin-top:10px;}
.orderlist .prolist td{ line-height:70px; border-left:1px solid #ddd;height:70px;text-align:center;}
.orderlist .prolist td:last-child{border-right:none;}
.orderlist .proname div{padding:5px;}
.orderlist .goodservice {text-align:right;padding-right:20px;}
.orderlist .prolist .rowtd {line-height:20px;padding-top:30px;}
.orderlist .order_navs{position:relative;}
.orderlist .search_div{position:absolute;right:0px;top:3px;}
.orderlist .ordertime{color:#999;font-size:12px;}
.orderlist .order_bspan { font-size:1.2em;}
.orderlist .idcmessage{color:#999;line-height:normal;margin:0;}
.orderlist .idm_tr td{line-height:normal;height:auto;text-align:left;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="shop"></div> 
<div class="container margin_t5">
<ol class="breadcrumb" style="margin-bottom:5px;">
    <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
    <li class="active"><a href="OrderList.aspx">我的订单</a></li>
</ol>
<ZL:OrderTop runat="server" />
</div>
    <div class="container orderlist margin_t5">
        <div class="order_navs">
            <ul class="nav nav-tabs" id="OrderType_ul" role="tablist">
                <li role="presentation" data-flag="all"><a href="?filter=&ordertype=<%=Request.QueryString["ordertype"] %>">全部订单</a></li>
                <li role="presentation" data-flag="needpay"><a href="?filter=needpay&ordertype=<%=Request.QueryString["ordertype"] %>">待付款</a></li>
                <li role="presentation" data-flag="receive"><a href="?filter=receive&ordertype=<%=Request.QueryString["ordertype"] %>">待收货</a></li>
                <li role="presentation" data-flag="comment"><a href="?filter=comment&ordertype=<%=Request.QueryString["ordertype"] %>">待评价</a></li>
                <li role="presentation" data-flag="recycle"><a href="?filter=recycle&ordertype=<%=Request.QueryString["ordertype"] %>">回收站</a></li>
              </ul>
            <div class="input-group search_div text_300">
                <asp:TextBox ID="Skey_T" runat="server" placeholder="商品名称/商品编号/订单号" CssClass="form-control"></asp:TextBox>
              <span class="input-group-btn">
                <asp:LinkButton class="btn btn-default" ID="Search_Btn" runat="server" OnClick="Search_Btn_Click"><span class="fa fa-search"></span></asp:LinkButton>
              </span>
            </div>
        </div>
        <table class="table">
            <thead class="top_div"><tr>
                <th class="text-center">订单详情</th>
                <th class="td_md">数量</th>
                <th class="td_md">总计</th>
                <th class="td_md">状态</th>
                <th class="td_md">操作</th>
            </tr></thead>
        </table>
        <ZL:ExRepeater ID="Order_RPT" runat="server" OnPreRender="Order_RPT_PreRender" PagePre="<div class='clearfix'></div><div class='text-center' style='border:1px solid #ddd;padding:5px;border-top:none;'>" PageEnd="</div>"
             OnItemDataBound="Order_RPT_ItemDataBound" OnItemCommand="Order_RPT_ItemCommand" PageSize="10">
            <ItemTemplate>
                <div class="order-item">
                    <table class="table prolist">
                        <thead>
                        <tr class="tips_div">
                            <th class="orderinfo" colspan="1"><span class="order_ico<%#Eval("OrderType")%>"></span> <span class="gray9" style="font-size:12px;"><%#Eval("AddTime") %></span> ID：<%#Eval("ID") %></th>
                            <th class="shopinfo" colspan="4">
                            	<asp:Label ID="Store_L" runat="server" />
                            </th>
                            <th class="opinfo">
                                <asp:LinkButton Visible="false" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del2" OnClientClick="return confirm('您确定要删除该订单吗？');"><i class="fa fa-trash"title="删除"></i></asp:LinkButton></th>
                               </tr></thead>
                        <asp:Repeater ID="Pro_RPT" runat="server" EnableViewState="false" OnItemDataBound="Pro_RPT_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align:left;border-right:none;border-left:none;">
                                        <span>
                                            <a href="<%#GetShopUrl() %>" target="_blank"><img src="<%#GetImgUrl() %>" onerror="shownopic(this);" class="img50" /></a>
                                            <span><%#Eval("Proname") %><%#GetSnap() %></span>
                                        </span>
                                    </td>
                                    <td class="td_md goodservice" style="border-left:none;"><%#GetRepair() %></td>
                                    <td class="td_md gray9">x<%#Eval("Pronum") %></td>
                                    <asp:Literal runat="server" ID="Order_Lit" EnableViewState="false"></asp:Literal>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%#GetMessage() %>
                    </table>
                </div>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/DateHelper.js"></script>
<script>
	function ConfirmAction(obj, a) {
		if (!confirm("确定要执行该操作吗?")) { return false; }
		var oid = $(obj).data("oid");
		$.post("", { action: a, "oid": oid }, function (data) {
			location = location;
		})
	}
	function ShowDrawback(oid) {
		comdiag.maxbtn = false;
		comdiag.title = "取消订单";
		comdiag.url = "/User/Order/DrawBack.aspx?id=" + oid;
		comdiag.ShowModal();
	}
	function CheckOrderType(flag) {
		$("#OrderType_ul li").removeClass('active');
		$("#OrderType_ul [data-flag='" + flag + "']").addClass('active');
	}
	$(function () {
		ComputeTime();
		setInterval(function () { ComputeTime(); }, 1000);
	})
	//倒计时
	function ComputeTime() {
		$(".ordertime").each(function () {
			var seconds = parseInt($(this).data("time") - 1);
			var timeMod = DateHelper.SecondToTime((seconds));
			if (timeMod.isHasTime()) {
				var str = timeMod.hour + '小时' + timeMod.minute + '分'+timeMod.second+'秒';
				if (timeMod.day > 0) { str = timeMod.day + "天" + str; }
				$(this).html('<span class="fa fa-clock-o"></span>' + str);
				$(this).data("time", seconds);
			}
			else { $(this).html('订单关闭'); }
		});
	}
	$().ready(function(e) {
        $(".order_ico0").html("<i class='fa fa-shopping-cart'></i>");//正常订单
        $(".order_ico1").html("<i class='fa fa-building'></i>");//酒店订单
		$(".order_ico2").html("<i class='fa fa-plane'></i>");//航班订单
		$(".order_ico3").html("<i class='fa fa-plane'></i>");//旅游订单
		$(".order_ico4").html("<i class='fa fa-database'></i>");//积分商品订单
		$(".order_ico5").html("<i class='fa fa-shopping-cart'></i>");//域名订单
		$(".order_ico6").html("<i class='fa fa-rmb'></i>");//余额充值订单
		$(".order_ico7").html("<i class='fa fa-server'></i>");//IDC订单
		$(".order_ico8").html("<i class='fa fa-credit-card-alt'></i>");//虚拟商品订单
		$(".order_ico9").html("<i class='fa fa-server'></i>");//IDC续费订单
		$(".order_ico10").html("<i class='fa fa-shopping-cart'></i>");//代购订单
    });
</script>
</asp:Content>