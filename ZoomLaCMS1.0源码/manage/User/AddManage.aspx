<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="AddManage.aspx.cs" Inherits="ZoomLaManage.WebSite.Manage.User.AddManage" Title="编辑管理员" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑管理员</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
<div class="r_n_pic"></div>
<span>后台管理</span>&gt;&gt;<span><a href="AdminManage.aspx">管理员管理管理</a></span> &gt;&gt;添加管理员
</div>
<div class="clearbox"></div>
      <table width="100%" border="0" cellpadding="0" cellspacing="1" class="border" align="center">
          <tr>
            <td class="spacingtitle" align="center" colspan="2" dir="ltr" valign="middle"　style="height: 23px">
                <asp:Label ID="lbTitle" runat="server" Text="添加管理员"></asp:Label>&nbsp;</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 81px; height: 23px;" valign="middle">
                <strong>管理员名：</strong></td>
            <td style="height: 23px">
                <asp:TextBox ID="tbdName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="管理员名不能为空" ControlToValidate="tbdName" Display="None"></asp:RequiredFieldValidator>                
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbdName"
                    ErrorMessage="不能包含特殊字符，如@，#，$，%，^，&，*，(，)，'，?，{，}，[，]，;，:等" ValidationExpression="^[^@#$%^&*()'?{}\[\];:]*$"
                    SetFocusOnError="True" Display="None"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbdName"
                    ErrorMessage="管理员名必须大于等于4个字符并且不能超过20个字符！" ValidationExpression="^[a-zA-Z0-9_\u4e00-\u9fa5]{4,20}$"
                    SetFocusOnError="True" Display="None"></asp:RegularExpressionValidator></td>
        </tr>        
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 81px; height: 23px" valign="middle">
                <strong>初始密码：</strong></td>
            <td >
                <asp:TextBox ID="tbPwd" runat="server" TextMode="Password" Width="150px"></asp:TextBox><strong>&nbsp;
                </strong></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 81px; height: 41px;" valign="middle">
                <strong>&nbsp;确认密码：</strong></td>
            <td>
                <asp:TextBox ID="tbPwd1" runat="server" TextMode="Password" Width="149px"></asp:TextBox>
                <asp:HiddenField ID="HdnPwd" runat="server" />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbPwd"
                    ControlToValidate="tbPwd1" ErrorMessage="密码与初始密码不一致"></asp:CompareValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>&nbsp;角色设置：</strong></td>
            <td align="left" valign="top">
                <asp:CheckBoxList ID="cblRoleList" runat="server" BorderStyle="Solid" 
                    DataTextField="RoleName" DataValueField="RoleID"/>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 81px;" valign="middle">
                <strong>&nbsp;选项设置：</strong></td>
            <td>
                <asp:CheckBox ID="cb1" runat="server" />允许多人同时使用此帐号登录<br/>
                <asp:CheckBox ID="cb2" runat="server" />允许管理员修改密码<br/>
                <asp:CheckBox ID="cb3" runat="server" />是否锁定</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" colspan="2" style="height: 23px" align="center">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="提　交" />
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="取 消" OnClick="btnCancel_Click" />&nbsp;</td>
        </tr>
        </table>    
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>