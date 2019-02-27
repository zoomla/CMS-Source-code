<%@ Page Language="C#" AutoEventWireup="true"  CodeFile ="UpdateLogType.aspx.cs" Inherits="UpdateLogType" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>修改日志类型</title>
<script type="text/javascript" language="javascript">
function dropChang()
{
	var obj= window.document.getElementById("dropReadAre");
	var span=window.document.getElementById("span1");
   if(obj.value=="2")
   {
   span.style.display="";
   }
   else
   {
   span.style.display="none";
   }
}

function spanDisy()
{
   var span=window.document.getElementById("span1");
   span.style.display="";
}
</script>

</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <br />
        <table border="0" cellpadding="0" cellspacing="1" width="100%" bgcolor="#000000">
            <tr>
                <td align="right" bgcolor="#FFFFFF">
                    日志类别名称：</td>
                <td bgcolor="#FFFFFF">
                    <asp:TextBox ID="txtLogTypeName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLogTypeName"
                        Display="Dynamic" ErrorMessage="请填写类别名称" Font-Size="10pt"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right" bgcolor="#FFFFFF">
                    访问权限：</td>
                <td bgcolor="#FFFFFF">
                    <asp:DropDownList ID="dropReadAre" runat="server" onChange="dropChang()">
                        <asp:ListItem Selected="True" Value="1">好友</asp:ListItem>
                        <asp:ListItem Value="0">所有人</asp:ListItem>
                        <asp:ListItem Value="2">凭密码</asp:ListItem>
                    </asp:DropDownList>
                    <span id="span1" style="display: none">设置密码：<asp:TextBox ID="txtPWD" runat="server"
                        MaxLength="10"></asp:TextBox></span>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" bgcolor="#FFFFFF">
                    <asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />
                    <asp:Button ID="Button1" runat="server" Text="关闭" OnClientClick="window.top.hidePopWin(true);" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
