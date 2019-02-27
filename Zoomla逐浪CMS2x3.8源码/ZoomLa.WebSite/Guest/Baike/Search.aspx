<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" MasterPageFile="~/Guest/Baike/Baike.master" ClientIDMode="Static" Inherits="Guestbook_BkSearch" EnableViewStateMac="false" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>搜索词条-<%Call.Label("{$SiteName/}"); %>百科</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
    <ol class="breadcrumb margin_top10">
        <li><a href="/">网站首页</a></li>
        <li><a href="/Baike" target="_self">百科中心</a></li>
        <li><a href="CreatBaike.aspx">创建词条</a></li>
    </ol>
    <div><asp:Label runat="server" ID="lblTittle"></asp:Label></div>
    <ul id="bklist_ul">
        <ZL:ExRepeater runat="server" ID="RPT" PageSize="20" PagePre="<div class='clearfix' style='margin-top:10px;'></div><div class='text-center'>" PageEnd="</div>">
            <ItemTemplate>
                <li>
                    <a href="Details.aspx?ID=<%#Eval("ID") %>" title="点击访问"><%#Eval("Tittle") %></a>
                    <span>(<%#Eval("Classification","").Trim()%>)</span>
                    <span class="r_gray pull-right"><%#Eval("AddTime","{0:yyyy年MM月dd日 HH:mm}") %></span>
                    <span class="r_gray pull-right margin_r5"><i class="fa fa-user"></i> <%#Eval("UserName") %></span>
                    <div class="clearfix"></div>
                </li>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </ul>
</div>
    <div class="ask_bottom">
        <p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
        <p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
#bklist_ul li{line-height:30px;padding-left:10px;padding-right:10px;border-bottom:1px solid #ccc;}
</style>
    <script>
        $(function () {
            var btype = "<%:BType %>";
            $("#top_nav_ul li[title='"+btype+"']").addClass("active");
        })
    </script>
</asp:Content>
