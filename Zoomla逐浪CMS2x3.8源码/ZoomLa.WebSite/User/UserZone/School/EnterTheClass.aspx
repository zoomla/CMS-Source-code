<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterTheClass.aspx.cs" Inherits="User_UserZone_School_EnterTheClass" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>sns</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <div>
    <table width="100%"  border="0" cellpadding="0" style="height:200px" cellspacing="0">
    <tr>
    <td align="center">学校：</td>
    <td id="tdSchool" runat="server" ></td>
    </tr>
    <tr>
    <td align="center">班级名称：</td>
    <td id="tdClass" runat="server" ></td>
    </tr>
    <tr>
    <td align="center">老师：</td>
    <td id="tdTeacher" runat="server" ></td>
    </tr>
    <tr>
    <td align="center">副管理员：</td>
    <td id="tdAdviser" runat="server" ></td>
    </tr>
    <tr>
    <td align="center">班长：</td>
    <td id="tdMonitor" runat="server" ></td>
    </tr>
    <tr>
    <td align="center">创建时间：</td>
    <td id="tdCreation" runat="server" ></td>
    </tr>
    <tr>
    <td align="center">创建人(管理员)：</td>
    <td id="tdCreateUser" runat="server" ></td>
    </tr>
    <tr>
    <td align="center">班级介绍：</td>
    <td id="tdClassinfo" runat="server" style=" white-space:normal"></td>
    </tr>
    <tr>
    <td align="center">我的身份：</td>
    <td>
        <asp:RadioButtonList ID="rdlStatusType" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Value="1" Selected="true">学生</asp:ListItem>
        <asp:ListItem Value="2">老师</asp:ListItem>
        <asp:ListItem Value="3">家长</asp:ListItem>
        </asp:RadioButtonList></td>
    </tr>
    <tr>
    <td align="center">申请理由：</td>
    <td>
        <asp:TextBox ID="txtContext" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
    <td colspan="2" align="center" >
        <asp:Button ID="Button1" runat="server" Text="加  入" OnClick="Button1_Click" /></td>
    </tr>
    </table>
    </div>
</form>
</body>
</html>
