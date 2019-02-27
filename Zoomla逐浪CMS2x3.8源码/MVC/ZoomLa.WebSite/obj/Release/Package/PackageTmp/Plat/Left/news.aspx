<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news.aspx.cs" Inherits="ZoomLaCMS.Plat.Left.news" %>
<ul class="listul">
    <asp:Repeater runat="server" ID="RPT">
        <ItemTemplate>
            <li data-id="<%#Eval("ID") %>" title="浏览详情" onclick="leftnav.showmsg('<%#Eval("ID") %>');">
                <div><%#GetContent() %></div>
                <div class="r_gray margin_t5">[<%#Eval("CUName") %>] <i class="fa fa-calendar"></i> <%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %></div>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
<div class="pull-right" style="margin-right:10px;" title="@我的信息"><a href="/Plat/Blog/Default.aspx?filter=atuser">查看更多 <i class="fa fa-chevron-circle-right"></i></a></div>
<div class="r_gray" runat="server" visible="false" id="empty_div">
   还没有人@AT你
</div>
