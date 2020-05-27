<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WordTlp.aspx.cs" Inherits="Manage_Plat_WordTlp" EnableViewState="false" MasterPageFile="~/Common/Master/Empty.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>能力报告</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href="/Admin/Main.aspx">工作台</a></li>
    <li><a href="CompList.aspx">能力中心</a></li>
    <li><a href="<%:Request.RawUrl %>">公司报表</a></li>
</ol>
<div style="height:44px;"></div>
<div class="panel panel-default" style="width:960px;margin:0 auto;">
    <div id="report_div" class="panel-body">
    <h2 style="text-align:center;">OA办公系统用户操作手册</h2>
    <table class="beautful" style="width:100%;">
        <tr><td colspan="2" style="text-align:center;">基本信息</td></tr>
        <tr><td style="width:120px;">企业名称：</td><td><asp:Label runat="server" ID="CompName_L"></asp:Label></td></tr>
        <tr><td>管理员：</td><td><asp:Label runat="server" ID="CUserName_L"></asp:Label></td></tr>
        <tr><td>管理员邮箱：</td><td><asp:Label runat="server" ID="CUserEmail_L"></asp:Label></td></tr>
        <tr><td>创建日期：</td><td><asp:Label runat="server" ID="CDate_L"></asp:Label></td></tr>
    </table>
    <table class="beautful" style="width:100%;margin-top:10px;">
    <tr><td colspan="3" style="text-align:center;">子级账号列表</td></tr>
    <tr><td style="width:60px;">序号</td><td style="width:200px;">部门</td><td>会员名</td></tr>
    <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
        <ItemTemplate>
            <tr><td><%#Container.ItemIndex+1 %></td><td><%#Eval("GroupName","").Trim(',') %></td><td><%#Eval("UserName") %></td></tr>
        </ItemTemplate>
    </asp:Repeater>
    </table>
    <table style="margin-top:10px;">
        <tr><td style="width:120px;">登录网址：</td><td><a href="<%:ZoomLa.Components.SiteConfig.SiteInfo.SiteUrl+"/Plat/" %>" target="_blank"><%:ZoomLa.Components.SiteConfig.SiteInfo.SiteUrl+"/Plat/" %></a></td></tr>
        <tr><td>初始密码：</td><td>111111</td></tr>
    </table>
    <div style="margin-top:10px;text-align:right;">
        <div style="text-align:left;font-weight:bold;">请接受我们的诚意,妥善保存此信息,享受强大的办公自动化与大数据平台！</div>
        <div style="text-align:left;font-weight:bold;">顺颂台绥</div>
        <div style="font-weight:bold;"><%:Call.SiteName %></div>
        <div style="font-weight:bold;"><%:DateTime.Now.ToString("yyyy-MM-dd") %></div>
    </div>
    </div>
    <div class="panel-footer">
        <div style="text-align:center;">
        <asp:Button runat="server" ID="DownDoc_Btn" Text="保存下载" OnClick="DownDoc_Btn_Click" class="btn btn-info" />
        <a href="CompList.aspx" class="btn btn-default">返回列表</a>
    </div>
    </div>
</div>
<input type="hidden" id="html_hid" name="html_hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
//美化表格
function beautful() {
    $(".beautful td").each(function () {
        var $this = $(this);
        $this.css("border", "1px solid #ddd");
        $this.css("padding","8px");
        $this.css("vertical-align", "top");
    });
    var $trs = $(".beautful tr");
    for (var i = 0; i < $trs.length; i++) {
        if (i % 2 == 0) { $($trs[i]).css("background-color", "#f9f9f9"); }
    }
}
$(function () {
    beautful();
    $("#html_hid").val($("#report_div").html());
})
</script>
</asp:Content>