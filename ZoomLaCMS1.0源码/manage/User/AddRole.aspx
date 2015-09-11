<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="AddRole.aspx.cs" Inherits="ZoomLa.WebSite.Manage.User.AddRole" Title="添加角色" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <img src="../../App_Themes/AdminDefaultTheme/images/localGIF.gif" align="absmiddle" /><span>后台管理</span>&gt;&gt;<span>用户管理</span> &gt;&gt;
    <a href="RoleManage.aspx">角色管理</a>&gt;&gt;<asp:Literal ID="Literal1" Text="添加角色"  runat="Server"></asp:Literal>
    <table width="99%" cellspacing="1" cellpadding="0" class="border" align="center">        
            <tr>
                <td align="center" class="spacingtitle" colspan="2" style="height: 22px">
                    <asp:Literal ID="LblTitle" Text="添加角色"  runat="Server"></asp:Literal>
                  </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width: 100px" valign="middle">
                    角色名：</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txbRoleName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txbRoleName"
                        Display="Dynamic" ErrorMessage="角色名不能为空">*</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvRole" runat="server" ControlToValidate="txbRoleName"
                        Display="Dynamic" ErrorMessage="角色名已经存在" OnServerValidate="CustomValidator1_ServerValidate"
                        Visible="False">*</asp:CustomValidator></td>
            </tr>
            <tr class="tdbg">
                <td align="right" class="tdbgleft" style="width: 100px; height: 20px" valign="middle">
                    角色描述：</td>
                <td align="left" style="width: 100px; height: 20px" valign="middle">
                    <asp:TextBox ID="tbRoleInfo" runat="server" Height="72px" TextMode="MultiLine" Width="228px"></asp:TextBox></td>
            </tr>
        </table>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        &nbsp; &nbsp;
        <asp:Button ID="btnSave"
            runat="server" Text="保存角色" Width="152px" OnClick="Button2_Click" />
        &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="返回角色管理" Width="85px" OnClick="btnBack_Click" />
    </div>
    </form>
</body>
</html>
