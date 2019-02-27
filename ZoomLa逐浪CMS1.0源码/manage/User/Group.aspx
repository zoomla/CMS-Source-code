<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Group.aspx.cs" Inherits="ZoomLa.WebSite.Manage.User.Group" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>会员组编辑</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<a href="GroupManage.aspx">会员组管理</a>&gt;&gt;<span><asp:Literal ID="LNav" runat="server" Text="添加会员组"></asp:Literal></span>
	</div>
    <div class="clearbox"></div>
    <table class="border" width="100%" cellpadding="2" cellspacing="1">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <asp:Literal ID="LTitle" runat="server" Text="添加会员组"></asp:Literal></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 35%">
                <strong>会员组名称：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtGroupName" runat="server" Width="156" MaxLength="200" /><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TxtGroupName">会员组名称不能为空</asp:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>会员组说明：</strong>
            </td>
            <td>                
                <asp:TextBox ID="TxtDescription" runat="server" TextMode="MultiLine" Width="365px" Height="43px" />                
            </td>
        </tr>        
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否注册可选：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RBLReg" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:HiddenField ID="HdnGroupID" runat="server" />
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />
                &nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="取消" onclick="Redirect('GroupManage.aspx')" />                
            </td>
        </tr>
    </table>
</form>
</body>
</html>
