<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiPicUpload.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Common.MultiPicUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>上传多张图片</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body class="tdbg">
    <form id="form1" runat="server">
        <input type="radio" name="ThumbIndex" value="0" checked="checked" /><asp:FileUpload ID="FileUpload0" runat="server" />
        <input type="radio" name="ThumbIndex" value="1" /><asp:FileUpload ID="FileUpload1" runat="server" /><br />
        <input type="radio" name="ThumbIndex" value="2" /><asp:FileUpload ID="FileUpload2" runat="server" />
        <input type="radio" name="ThumbIndex" value="3" /><asp:FileUpload ID="FileUpload3" runat="server" /><br />
        <input type="radio" name="ThumbIndex" value="4" /><asp:FileUpload ID="FileUpload4" runat="server" />
        <input type="radio" name="ThumbIndex" value="5" /><asp:FileUpload ID="FileUpload5" runat="server" /><br />
        <input type="radio" name="ThumbIndex" value="6" /><asp:FileUpload ID="FileUpload6" runat="server" />
        <input type="radio" name="ThumbIndex" value="7" /><asp:FileUpload ID="FileUpload7" runat="server" /><br />
        <input type="radio" name="ThumbIndex" value="8" /><asp:FileUpload ID="FileUpload8" runat="server" />
        <input type="radio" name="ThumbIndex" value="9" /><asp:FileUpload ID="FileUpload9" runat="server" /><br />
        若选中文件名前的单选框，则表示将此图片设为缩略图。
        <asp:Button ID="BtnUpload" runat="server" Text="开始上传" OnClick="BtnUpload_Click" /><asp:Label ID="LblMessage" ForeColor="red" runat="server"></asp:Label>
    </form>
</body>
</html>
