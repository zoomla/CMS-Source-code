<%@ Page Language="C#"　 AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="User.AddUser" Title="添加会员" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>添加新会员</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
<div class="r_navigation">
<div class="r_n_pic"></div>
<span>后台管理</span>&gt;&gt;<span><a href="UserManage.aspx">会员管理</a></span> &gt;&gt;添加会员
</div>
<div class="clearbox"></div>
   
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">                        
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 100px; height: 25px;">
                用户名：</td>
            <td style="width: 100px; height: 25px;" align="left">
                <asp:TextBox ID="tbUserName" runat="server" TabIndex="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbUserName"
                    ErrorMessage="用户名不能为空" Display="Dynamic">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="tbUserName"
                    ErrorMessage="用户名已存在" OnServerValidate="CustomValidator1_ServerValidate" Visible="False" Display="Dynamic">*</asp:CustomValidator></td>
            <td class="tdbgleft" style="width: 100px; height: 25px;" align="right">
                用户密码：</td>
            <td style="width: 100px; height: 25px;" align="left">
                <asp:TextBox ID="tbPwd" runat="server" TabIndex="2" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbPwd"
                    ErrorMessage="密码不能为空" Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 100px">
                电子邮件：</td>
            <td style="width: 100px" align="left">
                <asp:TextBox ID="tbEmail" runat="server" TabIndex="3"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbEmail"
                    Display="Dynamic" ErrorMessage="不能为空">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbEmail"
                    Display="Dynamic" ErrorMessage="格式不正确" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                    Visible="False"></asp:RegularExpressionValidator></td>
            <td class="tdbgleft" style="width: 100px" align="right">
                提示问题：</td>
            <td style="width: 100px" align="left">
                <asp:TextBox ID="tbQuestion" runat="server" TabIndex="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbQuestion"
                    Display="Dynamic" ErrorMessage="请输入提示问题">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 100px; height: 26px;">
                头像地址：</td>
            <td style="width: 100px; height: 26px;" align="left">
                <asp:TextBox ID="tbPhotoPlace" runat="server" TabIndex="5"></asp:TextBox></td>
            <td class="tdbgleft" style="width: 100px; height: 26px;">
                提示问题答案：</td>
            <td style="width: 100px; height: 26px;" align="left">
                <asp:TextBox ID="tbAnswer" runat="server" TabIndex="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbAnswer"
                    ErrorMessage="提示答案不能为空">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 100px">
                头像宽度：</td>
            <td style="width: 100px" align="left">
                <asp:TextBox ID="tbPhoWidth" runat="server" TabIndex="7">16</asp:TextBox></td>
            <td class="tdbgleft" style="width: 100px" align="right">
                头像高度：</td>
            <td style="width: 100px" align="left">
                <asp:TextBox ID="tbPhoHeight" runat="server" TabIndex="8">16</asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 100px; height: 22px">
                用户签名：</td>
            <td style="width: 100px; height: 22px" align="left">
                <asp:TextBox ID="tbUserWrite" runat="server" TabIndex="10" Columns="30" Rows="5" TextMode="MultiLine"></asp:TextBox></td>
            <td class="tdbgleft" style="width: 100px; height: 22px" align="right">
                </td>
            <td style="width: 100px; height: 22px" align="left">
                </td>
        </tr>                        
        <tr class="tdbg">
            <td style="width: 100px; height: 21px">
            </td>
            <td style="width: 100px; height: 21px" align="center">
<asp:Button ID="btnSave" runat="server" Text="保存会员信息" Width="90px" OnClick="btnSave_Click" TabIndex="12" /></td>
            <td style="width: 100px; height: 21px">
<asp:Button ID="btnCancel" runat="server" Text="取　消" Width="53px" OnClick="btnCancel_Click" TabIndex="13" /></td>
            <td style="width: 100px; height: 21px">
            </td>
        </tr>
    </table>
                
            
    
    </form>
</body>
</html>