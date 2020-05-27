<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app.aspx.cs" Inherits="ZoomLaCMS.Plat.Left.app" %>

<ul class="listul">
    <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
        <ItemTemplate>
            <%-- <li>
                <span class="icon"><%#GroupPic.GetExtNameMini(GetExt()) %></span>
                <span class="icon_txt"><%#Eval("FileName") %></span>
            </li>--%>
            <li onclick="window.open('<%#Eval("fileUrl") %>');">
                <div class="icon bk<%#Container.ItemIndex%10 %>" style="color: #fff;padding-top:4px;text-decoration:none;"><i class="<%#Eval("ico") %> fa-2x"></i></div>
                <span class="icon_txt"><%#Eval("name") %></span>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>