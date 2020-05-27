<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="ZoomLaCMS.Common.Upload" %>
<!DOCTYPE HTML>
<html>
<head><title>上传文件</title></head>
<body class="tdbg">
<form id="form1" runat="server" enctype="multipart/form-data">
    <div>
        <ZL:FileUpload ID="File_UP" runat="server" />
        <asp:Button ID="BtnUpload" runat="server" Text="上传" OnClick="BtnUpload_Click" />
        <asp:RequiredFieldValidator ID="ValFile" runat="server" ErrorMessage="请选择上传路径" ControlToValidate="File_UP" ForeColor="Red" Display="Dynamic" />
        <asp:Label runat="server" ID="LblMsg_L" ForeColor="Red"></asp:Label>
    </div>
</form>
</body>
</html>