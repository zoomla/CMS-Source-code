<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditOutLink.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.EditOutLink" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>修改外部链接</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<a href="NodeManage.aspx">节点管理</a>&gt;&gt;<span>修改外部链接</span>
	</div>
    <div class="clearbox"></div>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="margin: 0 auto;">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <asp:Label ID="LblTitle" runat="server" Text="修改外部链接" />
            </td>
        </tr>        
        <tr class="tdbg">
            <td style="width: 288px" class="tdbgleft">
                <strong>所属节点：</strong></td>
            <td>
                &nbsp;<asp:Label ID="LblNodeName" runat="server" Text=""></asp:Label>
                <asp:HiddenField ID="HdnParentId" Value="0" runat="server" />
                <asp:HiddenField ID="HdnDepth" Value="0" runat="server" />
                <asp:HiddenField ID="HdnOrderID" Value="0" runat="server" />
                <asp:HiddenField ID="HdnNodeID" Value="0" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>外部链接名称：</strong></td>
            <td>
                <asp:TextBox ID="TxtNodeName" runat="server" />
                <asp:RequiredFieldValidator ID="ValrNodeName" runat="server" ErrorMessage="单页名称不能为空！"
                    ControlToValidate="TxtNodeName" Display="Dynamic" SetFocusOnError="True" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>单页标识符：</strong></td>
            <td>
                <asp:TextBox ID="TxtNodeDir" runat="server" />
                <asp:RequiredFieldValidator ID="ValrNodeIdentifier" runat="server" ErrorMessage="标识符不能为空！"
                    ControlToValidate="TxtNodeDir" Display="Dynamic" SetFocusOnError="True" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>外部链接地址：</strong></td>
            <td>
                <asp:TextBox ID="TxtNodeUrl" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="链接地址不能为空！"
                    ControlToValidate="TxtNodeUrl" Display="Dynamic" SetFocusOnError="True" /></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 288px" class="tdbgleft">
                <strong>打开方式：</strong></td>
            <td>
                <asp:RadioButtonList ID="RBLOpenType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">原窗口打开</asp:ListItem>
                    <asp:ListItem Value="1">新窗口打开</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 288px" class="tdbgleft">
                <strong>外部链接图片地址：</strong>
                </td>
            <td>
                <asp:TextBox ID="TxtNodePicUrl" MaxLength="255" runat="server" Columns="50"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 288px" class="tdbgleft">
                <strong>外部链接提示：</strong></td>
            <td>
                <asp:TextBox ID="TxtTips" runat="server" Columns="60" Height="30" Width="500" Rows="2"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                &nbsp; &nbsp;
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp; &nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="BtnCancel" value="取消" onclick="window.location.href='NodeManage.aspx'" />                
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
