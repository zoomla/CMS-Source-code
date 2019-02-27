<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TradeMark.aspx.cs" Inherits="ZoomLaCMS.Plugins.TradeMark" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>商标查询</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="panel panel-default" runat="server" id="query_div" style="width:400px;margin:0 auto;margin-top:20px;">
    <div class="panel-heading">商标查询</div>
    <div class="panel-body">
        <div class="input-group">
            <input type="text" id="skey_t" runat="server" class="form-control" placeholder="请输入商标名称" />
            <span class="input-group-btn">
                <a href="javascript:;" class="btn btn-info" onclick="getTo();"><i class="fa fa-search"></i> 开始查询</a>
            </span>
        </div>
    </div>
</div>
<div runat="server" id="list_div" style="margin-top:5px;">
    <table class="table table-bordered table-striped">
        <tr>
            <td>商标名</td>
            <td>代理公司</td>
            <td>初审公告期号</td>
            <td>初审公告日期</td>
            <td>申请日期</td>
            <td>申请人(中文)</td>
            <td>国际分类</td>
            <td>注册公告日期</td>
            <td>注册公告期号</td>
            <td>注册号</td>
        </tr>
        <asp:Repeater runat="server" ID="RPT">
            <ItemTemplate>
                <tr>
                        <td><%#Eval("tmName") %></td>
                        <td><%#Eval("Agent") %></td>
                        <td><%#Eval("announcementIssue") %></td>
                        <td><%#Eval("announcementDate") %></td>
                        <td><%#Eval("appDate") %></td>
                        <td><%#Eval("applicantCn") %></td>
                        <td><%#Eval("currentStatus") %></td>
                        <td><%#Eval("intCls") %></td>
                        <td><%#Eval("regDate") %></td>
                        <td><%#Eval("regIssue") %></td>
                        <td><%#Eval("regNo") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
    function getTo() {
        var key = $("#skey_t").val();
        var url = "TradeMark.aspx?key=" + encodeURI(key);
        location = url;
    }
</script>
</asp:Content>
