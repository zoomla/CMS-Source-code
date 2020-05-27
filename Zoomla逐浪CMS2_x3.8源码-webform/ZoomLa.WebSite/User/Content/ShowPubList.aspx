<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="ShowPubList.aspx.cs" Inherits="User_Content_ShowPubList" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>内容预览</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="content" data-ban="cnt"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li><a href="Mycontent.aspx?NodeID=<%= NodeID%>">投稿管理</a></li>
<li class="active">信息预览</li>
</ol>
</div>
<div class="container">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="text-center">
                <asp:Label ID="Label1" runat="server" Text="信息预览"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DetailsView ID="DetailsView1" runat="server" Width="100%" CellPadding="4" GridLines="None" Font-Size="12px" Style="margin-bottom: 3px; margin-top: 2px;" CssClass="table table-striped table-bordered table-hover">
                    <Fields></Fields>
                    <FooterStyle Font-Bold="True" BackColor="#FFFFFF" />
                    <CommandRowStyle Font-Bold="True" CssClass="tdbgleft" />
                    <RowStyle />
                    <FieldHeaderStyle Font-Bold="True" />
                    <PagerStyle HorizontalAlign="Center" />
                    <HeaderStyle Font-Bold="True" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                </asp:DetailsView>
            </td>
        </tr>
    </table>
</div>
</asp:Content>