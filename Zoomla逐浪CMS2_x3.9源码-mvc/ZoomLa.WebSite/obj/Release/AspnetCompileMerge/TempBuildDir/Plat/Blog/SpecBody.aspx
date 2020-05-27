<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecBody.aspx.cs" Inherits="ZoomLaCMS.Plat.Blog.SpecBody" %>
<ul class="OspcList">
<asp:Repeater runat="server" ID="RPT">
<ItemTemplate>
    <li class="project list<%#Container.ItemIndex%10 %>" onclick="location='/Plat/Blog/?skey=<%#HttpUtility.UrlEncode(Eval("TName",""))+"&write="+HttpUtility.UrlEncode(Eval("TName","")) %>';">
        <div class="project-info">
            <div><h3><%#GetTName() %></h3></div>
            <p>共有<%#Eval("Count") %>条记录，最近一条发布在<%#Eval("CDate","{0:yyyy-MM-dd hh:mm}") %></p>
            <span>
                [<%#Eval("CUName") %>]
                <%#GetContent() %>
            </span>
        </div>
    </li>
</ItemTemplate>
</asp:Repeater>
</ul>
<div class="clearfix"></div>
<div>
    <asp:Literal runat="server" ID="Page_L"></asp:Literal>
</div>