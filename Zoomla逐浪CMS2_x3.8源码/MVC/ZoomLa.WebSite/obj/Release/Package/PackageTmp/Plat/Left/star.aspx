<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="star.aspx.cs" Inherits="ZoomLaCMS.Plat.Left.star" %>
<ul class="listul">
    <asp:Repeater runat="server" ID="RPT">
        <ItemTemplate>
            <li data-id="<%#Eval("ID") %>" title="点击浏览详情" onclick="leftnav.showmsg('<%#Eval("ID") %>');">
                <div><%#GetContent() %></div>
                <div class="r_gray margin_t5">[<%#Eval("CUName") %>] <i class="fa fa-calendar"></i> <%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %></div>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
<div class="r_gray" runat="server" visible="false" id="empty_div">
    你没有收藏任何信息
</div>
