<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserBody.aspx.cs" Inherits="ZoomLaCMS.Common.Dialog.UserBody" %>
<script src="/JS/ICMS/ZL_Common.js"></script>
<asp:Repeater ID="User_RPT" runat="server">
    <ItemTemplate>
        <div class="userlist_item" data-uid="<%#Eval("UserID") %>">
            <div class="item_face pull-left"><img src="<%#GetHeadIcon() %>" onerror="shownoface(this);"/></div>
            <div class="pull-left item_name"><%#GetName() %></div>
            <div class="pull-right item_add">添加</div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<asp:Repeater ID="UserPlat_RPT" runat="server">
    <ItemTemplate>
        <div class="userlist_item" data-uid="<%#Eval("UserID") %>">
            <div class="item_face pull-left"><img src="<%#Eval("UserFace") %>" onerror="shownoface(this);"/></div>
            <div class="pull-left item_name"><%#Eval("TrueName") %></div>
            <div class="pull-right item_add">添加</div>
        </div>
    </ItemTemplate>
</asp:Repeater>