<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SourceAdd.aspx.cs" Inherits="manage_AddOn_SourceAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择来源</title>
     <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />  
     <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/RiQi.js" type="text/javascript"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="TxtTemplate" MaxLength="255" runat="server" Columns="50"></asp:TextBox>
                    <input type="button" value="选择来源" onclick="WinOpenDialog('SourceList.aspx?flag=source',650,480)" class="btn"/><br />
    <asp:TextBox ID="TxtAuthor" MaxLength="255" runat="server" Columns="50"></asp:TextBox>
                    <input type="button" value="选择作者" onclick="WinOpenDialog('SourceList.aspx?flag=author',650,480)" class="btn"/><br />
                  <asp:TextBox ID="TxtKeyWord" MaxLength="255" runat="server" Columns="50"></asp:TextBox>
                    <input type="button" value="选择关键字" onclick="WinOpenDialog('SourceList.aspx?flag=keyword',650,480)" class="btn"/>
    </div>
    </form>
</body>
</html>
