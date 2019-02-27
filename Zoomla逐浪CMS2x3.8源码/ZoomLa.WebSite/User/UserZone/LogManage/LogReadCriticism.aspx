<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogReadCriticism.aspx.cs" Inherits="LogReadCriticism" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>我的日志</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div class="us_topinfo">
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx")%>' target="_parent"> 会员中心</a>&gt;&gt;<a title="我的日志" href='<%=ResolveUrl("~/User/UserZone/LogManage/SelfLogManage.aspx")%>'>我的日志</a>&gt;&gt;日志详细
</div>
  <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
  <table border="0" cellpadding="4" cellspacing="0" width="100%">
    <tr>
      <td style="height: 19px"><asp:Label ID="labTitle" runat="server" Text=""></asp:Label></td>
      <td style="height: 19px"><a href="CreatLog.aspx?LogID=<%=LogID %>&where=2">编辑</a> |
        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="return confirm('确定删除吗？');">删除</asp:LinkButton></td>
    </tr>
    <tr>
      <td colspan="2"><asp:Label ID="labTime" runat="server"></asp:Label>
        (分类：
        <asp:Label ID="labLogType" runat="server"></asp:Label>
        ) </td>
    </tr>
    <tr>
      <td colspan="2"></td>
    </tr>
    <tr>
      <td colspan="2"><asp:Label ID="labContent" runat="server"></asp:Label></td>
    </tr>
    <tr>
      <td colspan="2"></td>
    </tr>
    <tr>
      <td><asp:Label ID="Label2" runat="server" Text="阅读"></asp:Label>
        (
        <asp:Label ID="labReadCount" runat="server"></asp:Label>
        )
        <asp:Label ID="Label4" runat="server" Text="评论"></asp:Label>
        (
        <asp:Label ID="labCrCount" runat="server"></asp:Label>
        ) </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td colspan="2" style="width: 2px">&nbsp;</td>
    </tr>
    <tr>
      <td colspan="2" align="center"><ZL:ExGridView ID="gvCriticism" runat="server" AutoGenerateColumns="False" CellPadding="4" Width="90%" DataKeyNames="ID">
          <Columns>
          <asp:TemplateField>
            <ItemTemplate>
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td rowspan="3" align="left" style="width: 120px"><asp:Image ID="Image1" runat="server" ImageUrl='<%#GetPic(DataBinder.Eval(Container.DataItem,"UserID").ToString())%>' /></td>
                  <td align="left"><a href="#">
                    <asp:Label ID="labName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UserName") %>'></asp:Label>
                    </a></td>
                  <td align="right"><asp:Label ID="labDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CreatTime") %>'></asp:Label>
                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">删除</asp:LinkButton></td>
                </tr>
                <tr>
                  <td colspan="2" align="left"><asp:Label ID="labContent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CriticismConten") %>'></asp:Label></td>
                </tr>
              </table>
            </ItemTemplate>
          </asp:TemplateField>
          </Columns>
          <EmptyDataTemplate>
            <table class="tinputbody" cellpadding="0" cellspacing="1" border="0" width="100%">
              <tr>
                <td align="center">当前无评论</td>
              </tr>
            </table>
          </EmptyDataTemplate>
        </ZL:ExGridView></td>
    </tr>
    <tr>
      <td><table border="0" cellpadding="0" cellspacing="0" width="90%">
          <tr>
            <td align="left"><asp:TextBox ID="txtContext" runat="server" TextMode="MultiLine" Style="width: 414px; height: 126px"></asp:TextBox></td>
          </tr>
          <tr>
            <td align="left"><asp:Button ID="btnOK1" runat="server" Text="发表评论" OnClick="btnOK_Click" /></td>
          </tr>
        </table></td>
    </tr>
  </table>
</form>
</body>
</html>