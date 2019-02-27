<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContentMove.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.ContentMove" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>内容批量移动</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>内容管理</span> &gt;&gt;<span>内容批量移动</span>
	</div>
	<div class="clearbox"></div>
	<table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
	    <tr align="center">
            <td colspan="2" class="spacingtitle">
                批量移动内容到其他节点</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 20%" align="right">                    
                内容ID：</td>
            <td class="bqright">
                <asp:TextBox ID="TxtContentID" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtContentID"
                    ErrorMessage="内容ID不能为空"></asp:RequiredFieldValidator></td>
         </tr>
         <tr class="tdbg">
            <td class="tdbgleft" style="width: 20%" align="right">                    
                目标节点：</td>
            <td class="bqright">
                <asp:DropDownList ID="DDLNode" runat="server">
                </asp:DropDownList>
            </td>
         </tr>
         <tr class="tdbg">
            <td class="bqright" align="left" colspan="2">                    
                &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="批量处理" OnClick="Button1_Click" />&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="BtnCancel" value="取消" onclick="Redirect('ContentManage.aspx')" />
            </td>            
         </tr>
	</table>
    </form>
</body>
</html>
