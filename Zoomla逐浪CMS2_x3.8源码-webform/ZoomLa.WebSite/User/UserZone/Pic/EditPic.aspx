<%@ Page Language="C#"  MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="EditPic.aspx.cs" Inherits=" ZoomLa.WebSite.User.UserZone.Pic.EditPic" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>会员中心 >> 我的相册</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="us_topinfo">
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx")%>' target="_blank">会员中心</a>&gt;&gt;<a title="我的相册" href='<%=ResolveUrl("~/User/UserZone/Pic/CategList.aspx")%>'>我的相册</a>&gt;&gt;修改相片信息
</div>
    <uc1:WebUserControlTop id="WebUserControlTop1" runat="server"> </uc1:WebUserControlTop>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td colspan="2"><h2> <%=CategName %> 相册</h2></td>
      </tr>
      <tr>
        <td align="center" colspan="2"><asp:Image ID="Image1" runat="server" /></td>
      </tr>
      <tr>
        <td align="right" style="width: 30%"> 相片名称：</td>
        <td align="left"><asp:TextBox ID="TextBox1" runat="server" Width="300px"></asp:TextBox></td>
      </tr>
      <tr>
        <td align="right" valign="top" style="width: 30%"> 相片注释：</td>
        <td><asp:TextBox ID="TextBox2" Height="150px" Width="300px" runat="server" TextMode="MultiLine"></asp:TextBox></td>
      </tr>
      <tr>
        <td align="center" colspan="2" style="height: 24px"><asp:Button ID="Button1" runat="server" Text="提  交" OnClick="Button1_Click" />
          &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
          &nbsp;
          <asp:Button ID="Button2" runat="server" Text="删  除" OnClick="Button2_Click" /></td>
      </tr>
    </table>
</asp:Content>