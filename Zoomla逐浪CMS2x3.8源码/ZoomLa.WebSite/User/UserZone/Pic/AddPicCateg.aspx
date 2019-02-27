<%@ Page Language="C#"  MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="AddPicCateg.aspx.cs" Inherits=" AddPicCateg" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>我的相册</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="us_topinfo">
 <a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx") %>' target="_blank"> 会员中心</a> &gt;&gt; <a title="我的相册" href='<%=ResolveUrl("~/User/UserZone/Pic/CategList.aspx")%>'>我的相册</a>&gt;&gt;创建我的相册
</div>
    <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
    <br />
    <div class="us_topinfo">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td><h2>创建<%=type %>的相册</h2></td>
        </tr>
        <tr>
          <td><table width="100%" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td align="right"> 相册名称： </td>
                <td><asp:TextBox ID="txtCategName" runat="server"></asp:TextBox>
                  <span style="color: #FF0000">(必填)</span>
                  <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" ErrorMessage="相册名称不能为空" ControlToValidate="txtCategName"></asp:RequiredFieldValidator>
                  <br /></td>
              </tr>
              <tr>
                <td align="right"> 相册状态： </td>
                <td valign="middle"><asp:RadioButtonList ID="rblState" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"  OnSelectedIndexChanged="rblState_SelectedIndexChanged"> </asp:RadioButtonList></td>
              </tr>
              <tr>
                <td colspan="2"><div id="testing" style="background-color: Green;" visible="false" runat="server"> 相册密码：
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1" ErrorMessage="密码不能为空"></asp:RequiredFieldValidator>
                    确认密码：
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    &nbsp;
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox1" ControlToValidate="TextBox2" ErrorMessage="两次密码输入不一致！"></asp:CompareValidator>
                    <asp:Button ID="Button2" runat="server" Text="确定" OnClick="Button2_Click" />
                  </div></td>
              </tr>
              <tr>
                <td colspan="2" align="center"><asp:Button ID="butEnter" runat="server" Text="创 建" OnClick="butEnter_Click" />
                  &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                  <asp:Button ID="Button1" runat="server" Text="取 消" OnClick="Button1_Click" /></td>
              </tr>
            </table></td>
        </tr>
      </table>
    </div>
</asp:Content>