<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Guest/Baike/Baike.master" ClientIDMode="Static" CodeFile="Class.aspx.cs" Inherits="Guest_Baike_Class" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>开放分类--<%Call.Label("{$SiteName/}"); %>百科</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
<ol class="breadcrumb margin_top10">
    <li><a href="/">网站首页</a></li>
    <li><a href="/Baike" target="_self">百科中心</a></li>
    <li class="active">按开放分类浏览</li>
</ol>
<asp:Repeater ID="RPT" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
    <ItemTemplate>
        <div style="border-bottom:1px solid #ddd;padding-bottom:5px;margin-bottom:5px;">
            <a href="/Baike/Search.aspx?btype=<%#Eval("GradeName") %>" class="btn btn-info" target="_blank" title="点击浏览"><%#Eval("GradeName") %></a>
        </div>
        <ul>
            <asp:Repeater ID="Child_RPT" runat="server">
                <ItemTemplate>
                    <li class="btn btn-default"><a href="Search.aspx?btype=<%#Eval("GradeName") %>" title="<%#Eval("GradeName") %>" target="_blank"><%#Eval("GradeName") %></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </ItemTemplate>
</asp:Repeater>  
<div class="clearfix"></div> 
</div>
<div class="ask_bottom">
    <p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
    <p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
</div>
</asp:Content>
