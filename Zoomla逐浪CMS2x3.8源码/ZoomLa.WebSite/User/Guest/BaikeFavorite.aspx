<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="BaikeFavorite.aspx.cs" Inherits="User_Guest_BaikeFavorite" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>百科收藏</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="baike"></div>
    <div class="container margin_t10">
<ol class="breadcrumb">
    <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
    <li class="active">百科收藏</li>
	<div class="clearfix"></div>
</ol>
</div>
    <div class="container">
    <div class="us_seta">
        <table class="table table-bordered table-striped table-hover">
            <tr class="title">
                <td>已收藏词条</td>
                <td>收藏时间</td>
            </tr>
            <ZL:ExRepeater ID="RPT" PagePre="<tr><td colspan='2' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <a href="/Guest/Baike/Details.aspx?ID=<%#Eval("InfoID") %>" target="_blank"><%#GetTitle()%></a>
                        </td>
                        <td><%#Convert.ToDateTime(Eval("FavoriteDate")).ToString("yyyy-MM-dd")%></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
        </table>
    </div>
    </div>
</asp:Content>