<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="ZoomLaCMS.Common.FileUpload" MasterPageFile="~/Common/Common.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>文件上传</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content"> 
<div style="margin-top:5px;">
    <ZL:FileUpload ID="FupFile" runat="server" Style="display: inline; line-height: 10px;" />
    <asp:Button ID="BtnUpload" runat="server" Text="上传" CssClass="btn btn-info btn-xs" OnClick="BtnUpload_Click" />
    <asp:RequiredFieldValidator ID="ValFile" runat="server" ErrorMessage="请选择上传路径" ForeColor="Red" ControlToValidate="FupFile" Display="Dynamic" />
    <asp:Label ID="LblMessage" runat="server" ForeColor="red"></asp:Label>
    <asp:HiddenField ID="HiddenNodeDir" runat="server" />
</div>
</asp:Content> 
