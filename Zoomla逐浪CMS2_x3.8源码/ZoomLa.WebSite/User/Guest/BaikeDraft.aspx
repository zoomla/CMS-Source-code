<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="BaikeDraft.aspx.cs" Inherits="User_Guest_BaikeDraft" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>草稿箱</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="baike"></div>
<div class="container margin_t10">   
<ol class="breadcrumb">
    <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
    <li class="active">词条草稿箱 </li>
	<div class="clearfix"></div>
</ol>
    </div> 
<div class="container">
<div class="us_seta">
<table class="table table-bordered table-striped table-hover">
    <tr class="title">
        <td>词条</td>
        <td>添加时间</td>
    </tr>
    <ZL:ExRepeater ID="RPT" PagePre="<tr><td colspan='2' class='text-center'>" PageEnd="</td></tr>" runat="server">
        <ItemTemplate>
            <tr>
                <td style="width: 70%; line-height: 22px; padding-left: 10px;">
                    <a href='/Guest/Baike/Details.aspx?soure=manager&tittle=<%#Server.UrlEncode(Eval("Tittle").ToString()) %>' target="_blank"><%# Eval("Tittle")%></a>
                </td>
                <td style="width: 26%; padding-left: 10px;"><%--<label runat="server" id="lbstatus"></label>--%><%#Convert.ToDateTime(Eval("EditTime")).ToString("yyyy-MM-dd")%></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </ZL:ExRepeater>
</table>
</div>
</div>
</asp:Content>