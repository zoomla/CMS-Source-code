<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetSearch.aspx.cs" Inherits="Zone_GetSearch" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>空间搜索</title>
<link href="../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" language="javascript">
    function refpage(ret)
    {
        if(typeof(ret)!="undefined")
{
window.location.href="GetSearch.aspx";
}
}
</script>
<body>
<form id="form1" runat="server">
  <div>
    <asp:DataList ID="DataList1" runat="server" Width="100%">
      <ItemTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td width="18%">&nbsp;</td>
            <td width="82%"><a href="#"> <%#DataBinder.Eval(Container.DataItem,"UserName") %> </a></td>
          </tr>
          <tr>
            <td align="center" height="143"><a href="../User/Usershow.aspx?userid=<%#DataBinder.Eval(Container.DataItem,"UserID") %>">
              <asp:Image ID="Image1" runat="server" Height="120px" Width="120px" ImageUrl='<%#ResolveUrl(DataBinder.Eval(Container.DataItem,"UserPic").ToString()) %>' />
              </a></td>
            <td valign="top">&nbsp;<%#DataBinder.Eval(Container.DataItem, "UserLove")%></td>
          </tr>
          <tr>
            <td height="10" align="center">&nbsp;</td>
            <td align="right"><a href="javascript:showPopWin('添加好友','showfriendsearch.aspx?sID=<%#DataBinder.Eval(Container.DataItem,"UserID") %>&Math.random()',400,200, refpage,true)">加为好友</a>&nbsp;</td>
            &nbsp;&nbsp;&nbsp;&nbsp; </tr>
          <tr>
            <td height="1" colspan="2" align="center" bgcolor="#CC6600"></td>
          </tr>
        </table>
      </ItemTemplate>
    </asp:DataList>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
  </div>
</form>
</body>
</html>