<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContentFile.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Common.FileUpload" EnableViewStateMac="false" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>文件上传</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ZL:FileUpload ID="File_UP" accept="image/*" runat="server" Style="display: inline-block;" />
<asp:Button ID="BtnUpload" runat="server" Text="上传" CssClass="btn btn-info btn-xs" OnClick="BtnUpload_Click" />
<span runat="server" visible="false" id="water_div">
    <span>是否添加水印</span>
    <label>
        <input type="radio" value="1" name="water_rad" />是</label>
    <label>
        <input type="radio" value="0" name="water_rad" checked="checked" />否</label>
</span>
<asp:RequiredFieldValidator ID="ValFile" runat="server" ErrorMessage="请选择上传路径" ForeColor="Red" ControlToValidate="File_UP" Display="Dynamic" />
<asp:Label ID="LblMessage" runat="server" ForeColor="red"></asp:Label>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/SelectCheckBox.js"></script>
</asp:Content>
