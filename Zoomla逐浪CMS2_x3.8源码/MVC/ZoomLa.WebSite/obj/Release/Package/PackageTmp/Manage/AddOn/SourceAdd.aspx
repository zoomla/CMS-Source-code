<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SourceAdd.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.SourceAdd" %>

<!DOCTYPE HTML>
<html>
<head runat="server">
<title>选择来源</title>
<script src="/JS/Common.js" type="text/javascript"></script>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">
<div>
<asp:TextBox ID="TxtTemplate" MaxLength="255" runat="server" Columns="50" class="l_input"></asp:TextBox>
<input type="button" value="选择来源" onclick="WinOpenDialog('SourceList.aspx?flag=source',650,480)" class="btn" class="C_input"/><br />
<asp:TextBox ID="TxtAuthor" MaxLength="255" runat="server" Columns="50" class="l_input"></asp:TextBox>
<input type="button" value="选择作者" onclick="WinOpenDialog('SourceList.aspx?flag=author',650,480)" class="btn" class="C_input"/><br />
<asp:TextBox ID="TxtKeyWord" MaxLength="255" runat="server" Columns="50" class="l_input"></asp:TextBox>
<input type="button" value="选择关键字" onclick="WinOpenDialog('SourceList.aspx?flag=keyword',650,480)" class="btn" class="C_input"/>
</div>
</form>
</body>
</html>