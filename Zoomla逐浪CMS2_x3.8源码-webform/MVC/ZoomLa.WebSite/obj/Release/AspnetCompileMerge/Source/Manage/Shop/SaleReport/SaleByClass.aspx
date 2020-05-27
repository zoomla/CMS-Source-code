<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SaleByClass.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.SaleReport.SaleByClass" MasterPageFile="~/Manage/I/Default.Master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>按类统计</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="BreadDiv" class="container-fluid" style="height:10px;">
    <div class="row">
	<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href="ProductManage.aspx">商城管理</a></li>
        <li><a href="TotalSale.aspx">按类别统计</a></li>
        <div id="help" class="pull-right text-center"><a href="javascript:;" class="help_btn active" onclick="selbox.toggle();"><i class="fa fa-search"></i></a></div>
        <div id="sel_box">
            <div class="input-group" style="display: inline-block;">
                <asp:TextBox runat="server" ID="SDate_T" class="form-control text_md" placeholder="起始时间" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' });" />
                <asp:TextBox runat="server" ID="EDate_T" class="form-control text_md" placeholder="起始时间" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' });" />
                <span class="input-group-btn">
                    <asp:LinkButton runat="server" ID="Search_Btn" OnClick="Search_Btn_Click" class="btn btn-default"><i class="fa fa-search"></i></asp:LinkButton>
                </span>
            </div>
        </div>
	</ol>
    </div>
</div>
<div class="date_wrap">
    <div class="item">
        <strong>年份：</strong>
        <div class="btn-group" id="years_div">
            <asp:Literal ID="Years_Li" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="item">
        <strong>月份：</strong>
        <div class="btn-group">
            <asp:Literal ID="Months_Li" runat="server"></asp:Literal>
        </div>
    </div>
</div>
<table class="table table-bordered table-striped">
    <tr><td class="td_m">类别</td><td>销售额</td><td class="td_l">操作</td></tr>
    <asp:Repeater runat="server" ID="RPT">
        <ItemTemplate>
            <tr><td><%#Eval("NodeName") %></td><td><%#Eval("AllMoney","{0:f2}") %></td><td><a href="SaleByProduct.aspx?nodeid=<%#Eval("NodeID") %>"><i class="fa fa-list"></i> 查看详情</a></td></tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<div style="height:50px;"></div>
<div class="Conent_fix" style="font-size:16px;">
<%--    在线支付：<asp:Label runat="server" ID="PayOnline_L" class="rd_red_l" />
    余额支付：<asp:Label runat="server" ID="PayPurse_L" class="rd_red_l" />--%>
    销售总计：<asp:Label runat="server" ID="TotalSale_L" class="rd_red_l" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/Controls/DateHelper.js"></script>
<script>
    $(function () {
        $(".filter_year").click(function () {
            $(".filter_year").removeClass("active");
            $(this).addClass("active");
            filter();
        });
        $(".filter_month").click(function () {
            $(".filter_month").removeClass("active");
            $(this).addClass("active");
            filter();
        });
    })
    function filter() {
        var year = $(".filter_year.active").data("num");
        var month = $(".filter_month.active").data("num");
        var stime = year + "/" + month + "/01";
        var etime = year + "/" + month + "/" + DateHelper.getMonthDays(year, month);
        location = "SaleByClass.aspx?stime=" + stime + "&etime=" + etime;
    }
</script>
</asp:Content>