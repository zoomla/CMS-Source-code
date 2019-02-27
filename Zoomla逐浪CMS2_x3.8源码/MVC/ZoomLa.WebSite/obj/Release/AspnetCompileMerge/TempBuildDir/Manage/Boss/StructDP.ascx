<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StructDP.ascx.cs" Inherits="ZoomLaCMS.Manage.Boss.StructDP" %>
<div class="input-group" style="width: 200px;float:left;">
    <span class="input-group-addon" style="border-right:none;">分组</span>
    <asp:DropDownList runat="server" ID="Struct_DP" CssClass="form-control text_md" DataTextField="Name" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="Struct_DP_SelectedIndexChanged"></asp:DropDownList>
</div>
