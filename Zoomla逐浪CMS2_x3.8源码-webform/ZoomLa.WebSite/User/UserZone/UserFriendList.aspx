<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserFriendList.aspx.cs" Inherits="User_Guild_UserFriendList" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>查找会员</title>
<link href="../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="fcontent">
        <div class="note">
            共找到 <b>
        <asp:Label ID="lblFriendCount" runat="server" Text=""></asp:Label>
            </b> 位相关用户 <span align="right"><a href="UserQuestFriend.aspx">重新查找</a></span> </div>
      </div>
    <asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
     <div>
      <table width="100%">
      <tr><td width="5%">
      <img src="<%#GetImg(Eval("UserFace","{0}")) %>"  border="0"/>
      </td>
      <td width="20%">
      <table width="100%" cellpadding="0" cellspacing="0">
      <tr><td><a href="#" target="_blank"><b>
      <%# GetUserName(Eval("UserID","{0}"))%>
      </b></a></td></tr>
    <tr><td>
    <font color="#AAAAAA">性别：
    <%# GetUserSex(Eval("UserID","{0}"))%>
    </font>
    </td></tr>
      <tr><td>
    <font color="#AAAAAA">年龄：
     <%# GetUserAge(Eval("UserID","{0}"))%>
    </font>
    </td></tr>
    <tr><td>
        <font color="#AAAAAA">地区：
        <%# GetUserCity(Eval("UserID","{0}"))%>
        &nbsp;
    <%--  <%#Eval("County")%>--%>
    </font>
          </td></td>
  <tr><td>
  <a href="#" target="_blank">发送站内信</a>
			&nbsp;&nbsp;|&nbsp;&nbsp;
			<a href="#">查看TA的好友</a>
  </td>
  <td align="right">
  <a href="AddHailfellow.aspx?fid=<%#Eval("UserID") %>">加为好友</a><br />
  <a href="#" target="_blank">查看TA的空间</a>
  </td>
  </tr>
      </table>
      </table>
      </td>
      </tr>
      </table>
    </ItemTemplate>
    </asp:Repeater>
    <div align="right">
        <asp:Panel ID="panelpage" runat="server">
    总共<asp:Label ID="Count" runat="server"></asp:Label>记录 &nbsp; 当前页:<asp:Label ID="CurrentPage"
                            runat="server"></asp:Label>&nbsp; 共<asp:Label ID="PageCount" runat="server"></asp:Label>页
                        <asp:Label ID="PageSize" runat="server"></asp:Label>条记录/页 &nbsp;<asp:Label ID="FirstPage"
                            runat="server"></asp:Label>
        <asp:Label ID="RePage" runat="server"></asp:Label>
        <asp:Label ID="NextPage" runat="server"></asp:Label>
        <asp:Label ID="EndPage" runat="server"></asp:Label>
        </asp:Panel>
     </div>
</body>
</html>
