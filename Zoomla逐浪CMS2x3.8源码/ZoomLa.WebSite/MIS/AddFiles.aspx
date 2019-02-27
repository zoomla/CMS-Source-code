<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddFiles.aspx.cs" Inherits="MIS_AddFiles" %>
<!DOCTYPE html> 
<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="/App_Themes/User.css" rel="stylesheet" type="text/css"/>
<title>添加文档</title>
</head>
<body>
<form id="form1" runat="server">
<table width="100%" class="dialog" style="width: 740px;" border="0" cellSpacing="8" cellPadding="0">
<tr> 
<th width="6%" noWrap="">标题* :</th>
<td><asp:TextBox ID="TxtTit" class="M_input" runat="server" ></asp:TextBox> </td>
</tr> 
<tr> 
<th width="6%" noWrap="">选择文档 :</th>
<td>
    <ZL:FileUpload ID="SFile_UP" runat="server" CssClass="M_input" onkeydown="event.returnValue=false;"   onpaste="return false" />
     <%--<asp:FileUpload ID="Files" runat="server" class="M_input" onkeydown="event.returnValue=false;"   onpaste="return false" />--%>单次上传文件大小不能超过36MB。 </td>
</tr>
<tr><td colspan="2"> <asp:Button ID="Button1" CssClass="i_bottom" OnClick="Button1_Click" runat="server" Text="提交" />
&nbsp;<asp:Button ID="concle" CssClass="i_bottom" runat="server" Text="取消" /></td></tr>
</table>
</form>
</body>
</html>