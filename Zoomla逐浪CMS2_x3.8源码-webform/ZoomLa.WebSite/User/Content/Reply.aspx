<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Reply.aspx.cs" Inherits="User_Content_Reply" ClientIDMode="Static" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>信息回复</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="pub"></div>
<div class="container margin_t10">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li class="active">信息回复</li>
</ol>
</div>
<div class="btn_green">
<table class="table table-striped table-bordered table-hover">
<tr>
<td colspan="2" class="text-center"><asp:Label ID="Label2" runat="server" Text="信息回复"></asp:Label></td>
</tr>
<tr>
<td style="text-align: right">标题：
</td>
<td>
<asp:TextBox ID="txtTitle" CssClass="form-control" runat="server" style="width:400px;max-width:400px;"></asp:TextBox>
</td>
</tr>
<tr>
<td style="text-align: right">内容：
</td>
<td>
<asp:TextBox ID="txtContent" runat="server" CssClass="form-control" style="width:400px;max-width:400px;" Height="200px" TextMode="MultiLine"></asp:TextBox>
</td>
</tr>
<tr>
<td colspan="2" align="center" class="btn_green">
<asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="提交" OnClick="Button1_Click" />
</td>
</tr>
</table>
</div>

</asp:Content>
