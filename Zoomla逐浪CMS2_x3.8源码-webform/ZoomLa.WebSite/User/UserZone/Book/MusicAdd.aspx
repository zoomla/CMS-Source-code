<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MusicAdd.aspx.cs" Inherits="MusicAdd" Title="音乐" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc3" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>添加音乐</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div style="width: 100%"> 
<div class="us_topinfo" style="width: 98%">
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx") %>' target="_parent"> 会员中心</a>音乐
</div>
    <uc3:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc3:WebUserControlTop>
    <br />
    <div class="us_topinfo" style="margin-top: 10px;">
      <table border="1" cellpadding="0" cellspacing="0" height="167" width="100%" align="center">
        <tr>
          <td><h3> &nbsp; &nbsp; <img src="../Images/c94b5c389e8cbfebb311c764.jpg" style="width: 57px; height: 54px" />添加音乐</h3></td>
        </tr>
        <tr>
          <td style="height: 92px"><table width="100%" height="246" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="29%" align="center">&nbsp;<font style="font-size: 12px">名称</font>： </td>
                <td width="71%">&nbsp;
                  <asp:TextBox ID="titletxt" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="titletxt"  ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
              </tr>
              <tr>
                <td align="center">&nbsp;<font style="font-size: 12px">演唱者</font>： </td>
                <td>&nbsp;
                  <asp:TextBox ID="othertitletxt" runat="server"></asp:TextBox></td>
              </tr>
              <tr>
                <td align="center">&nbsp;<font style="font-size: 12px">词</font>： </td>
                <td>&nbsp;
                  <asp:TextBox ID="antxt" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="antxt" ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
              </tr>
              <tr>
                <td align="center">&nbsp;<font style="font-size: 12px">曲</font>： </td>
                <td>&nbsp;
                  <asp:TextBox ID="isbntxt" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="isbntxt" ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
              </tr>
              <tr>
                <td align="center">&nbsp;<font style="font-size: 12px">出版者</font>： </td>
                <td>&nbsp;
                  <asp:TextBox ID="concermtxt" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="yeartxt"   ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
              </tr>
              <tr>
                <td align="center">&nbsp;<font style="font-size: 12px">发行时间</font>： </td>
                <td>&nbsp;
                  <asp:TextBox ID="yeartxt" runat="server" MaxLength="4"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="不能为空" ControlToValidate="yeartxt"></asp:RequiredFieldValidator>
                  <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="yeartxt"
                                        ErrorMessage="请输入正确的发行年份 如：2008" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></td>
              </tr>
              <tr>
                <td align="center">&nbsp;<font style="font-size: 12px">图片</font>： </td>
                <td>&nbsp;
                    <ZL:FileUpload runat="server" ID="FileUpload1"  name="FileUpload1"/>
                  <%--<input type="file" name="FileUpload1" id="FileUpload1" runat="server" />--%>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUpload1"  ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                  <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="图片大小不得超过 1024KB"></asp:Label></td>
              </tr>
              <tr>
                <td align="center">&nbsp;<font style="font-size: 12px">内容</font>： </td>
                <td>&nbsp;
                  <textarea id="contenttxt" runat="server" rows="10" style="width: 330px"></textarea>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="contenttxt" ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
              </tr>
              <tr>
                <td>&nbsp;</td>
                <td>&nbsp;
                  <asp:Button ID="sBtn" runat="server" OnClick="sBtn_Click" Text="添加保存" /></td>
              </tr>
            </table></td>
        </tr>
      </table>
    </div>
  </div>
</asp:Content>