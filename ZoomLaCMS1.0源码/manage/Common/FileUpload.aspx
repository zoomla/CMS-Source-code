<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUpload.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Common.FileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>上传文件</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body class="tdbg">
    <form id="form1" runat="server" enctype="multipart/form-data">
        <table style="height: 100%; border: 0; width: 100%">
            <tr class="tdbg">
                <td valign="top">
                    <asp:FileUpload ID="FupFile" runat="server" /><asp:Button ID="BtnUpload" runat="server"
                        Text="上传" OnClick="BtnUpload_Click" /><asp:RequiredFieldValidator ID="ValFile" runat="server"
                            ErrorMessage="请选择上传路径" ControlToValidate="FupFile"></asp:RequiredFieldValidator><asp:Label
                                ID="LblMessage" runat="server" ForeColor="red" Text=""></asp:Label></td>
            </tr>
        </table>
    </form>
</body>
</html>
