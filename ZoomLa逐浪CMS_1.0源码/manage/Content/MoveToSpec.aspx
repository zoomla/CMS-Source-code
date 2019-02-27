<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoveToSpec.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.MoveToSpec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>移动到其他专题</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span>专题内容管理</span>&gt;&gt;<span>移动到其他专题</span>
	</div>
    <div class="clearbox"></div>
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr class="tdbg" align="center">
            <td colspan="2" class="spacingtitle">
                移动专题信息到其他专题
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>选定的专题信息ID：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtGeneralID" runat="server" Width="280px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>移动到目标专题：</strong>
                <br />
                <span style="color: Red"></span></td>
            <td>
                <asp:ListBox ID="LstSpec" runat="server" Rows="10" Width="280px"></asp:ListBox>
                <br />                
                <input id="Button1" onclick="SelectAll()" type="button" class="inputbutton" value="  选定所有专题  " />
                <input id="Button2" onclick="UnSelectAll()" type="button" class="inputbutton" value="取消选定所有专题" />
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" colspan="2">            
                <asp:Button ID="BtnBacthSet" runat="server" Text="执行批处理" CssClass="inputbutton" OnClick="BtnBacthSet_Click" />&nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="取消" CssClass="inputbutton" OnClick="BtnCancel_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>