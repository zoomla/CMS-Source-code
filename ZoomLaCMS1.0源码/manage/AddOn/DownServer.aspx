<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DownServer.aspx.cs" Inherits="manage_Plus_DownServer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>下载服务器</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>其他管理</span>&gt;&gt;<span><a href="DownServerManage.aspx">下载服务器管理</a></span> &gt;&gt;<span>
                                   添加下载服务器</span>
                                   </div>	
 <div class="clearbox"></div>  
     <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>
                <asp:Label ID="LblTitle" runat="server" Text="添加服务器" Font-Bold="True"></asp:Label>
                </strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 40%">
                <strong>服务器名称：</strong><br />
                在此输入在前台显示的镜像服务器名，如广东下载、上海下载等。
            </td>
            <td class="tdbg" style="text-align: left; width: 60%;">
                <asp:TextBox ID="TxtServerName" runat="server" Width="290px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ValrServerName" runat="server" ErrorMessage="下载服务器名称不能为空"
                    ControlToValidate="TxtServerName"></asp:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 40%">
                <strong>服务器LOGO：</strong><br />
                输入服务器LOGO的绝对地址，如http://www.zoomla.cn/Images/ServerLogo.gif
            </td>
            <td class="tdbg" style="text-align: left; width: 60%;">
                <asp:TextBox ID="TxtServerLogo" runat="server" Width="290px"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 40%; height: 49px;">
                <strong>服务器地址：</strong><br />
                请认真输入正确的服务器地址。<br />
                如http://www.zoomla.cn/这样的地址
            </td>
            <td class="tdbg" style="text-align: left; width: 60%; height: 49px;">
                <asp:TextBox ID="TxtServerUrl" runat="server" Width="290px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ValrServerUrl" runat="server" ErrorMessage="下载服务器地址不能为空"
                    ControlToValidate="TxtServerUrl"></asp:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 40%">
                <strong>显示方式：</strong>
            </td>
            <td class="tdbg" style="text-align: left; width: 60%;">
                <asp:DropDownList ID="DropShowType" runat="server">
                    <asp:ListItem Value="0">显示名称</asp:ListItem>
                    <asp:ListItem Value="1">显示LOGO</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: center" colspan="2">
                <br />
                <asp:Button ID="EBtnModify" Text="修改" OnClick="EBtnModify_Click" runat="server" Visible="false"/>
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="BtnCancel" type="button" class="inputbutton" onclick="javascript:window.location.href='DownServerManage.aspx'"
                    value=" 取消 " />
            </td>
        </tr></table>

    </form>
</body>
</html>
