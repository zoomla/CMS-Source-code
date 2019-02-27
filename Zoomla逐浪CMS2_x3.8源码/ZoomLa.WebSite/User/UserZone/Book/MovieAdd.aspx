<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MovieAdd.aspx.cs" Inherits="MovieAdd" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc3" %>
<!DOCTYPE HTML>
<html>
<head>
<title>添加电影</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div style="width: 100%">
<div class="us_topinfo" style="width: 98%">
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx") %>' target="_parent"> 会员中心</a>电影
</div>
  <uc3:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc3:WebUserControlTop>
  <br />
  <div>
    <table border="1" cellpadding="0" cellspacing="0" height="167" width="100%" align="center">
      <tr>
        <td><h3> &nbsp; &nbsp; <img src="../Images/ico_movie_b.gif" style="width: 57px; height: 54px" />添加电影</h3></td>
      </tr>
      <tr>
        <td style="height: 92px"><table width="100%" height="246" border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td width="29%" align="center">&nbsp;<font style="font-size: 12px">电影名称：</font></td>
              <td width="71%">&nbsp;
                <asp:TextBox ID="titletxt" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="titletxt" ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
              <td align="center">&nbsp;<font style="font-size: 12px">又名：</font></td>
              <td>&nbsp;
                <asp:TextBox ID="othertitletxt" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
              <td align="center">&nbsp;<font style="font-size: 12px">导演：</font></td>
              <td>&nbsp;
                <asp:TextBox ID="antxt" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="antxt" ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
              <td align="center">&nbsp;<font style="font-size: 12px">语言：</font></td>
              <td>&nbsp;
                <asp:TextBox ID="isbntxt" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="isbntxt" ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
              <td align="center">&nbsp;<font style="font-size: 12px">制片国家/地区：</font></td>
              <td>&nbsp;
                <asp:TextBox ID="concermtxt" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="yeartxt" ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
              <td align="center">&nbsp;<font style="font-size: 12px">上映年度：</font></td>
              <td>&nbsp;
                <asp:TextBox ID="yeartxt" runat="server" MaxLength="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="不能为空" ControlToValidate="yeartxt"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="yeartxt" ErrorMessage="请输入正确的上映年份 如：2008" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></td>
            </tr>
            <tr>
              <td align="center">&nbsp;<font style="font-size: 12px">图片：</font></td>
              <td>&nbsp;
                  <ZL:FileUpload runat="server" ID="FileUpload1" name="FileUpload1" />
                <%--<input type="file" name="FileUpload1" id="FileUpload1" runat="server" />--%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUpload1"	ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="图片大小不得超过1024KB"></asp:Label></td>
            </tr>
            <tr>
              <td align="center">&nbsp;<font style="font-size: 12px">内容：</font></td>
              <td>&nbsp;
                <textarea id="contenttxt" runat="server" rows="10" style="width: 330px"></textarea>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="contenttxt"
									ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
              <td>&nbsp;</td>
              <td>&nbsp;
                <asp:Button ID="sBtn" runat="server" OnClick="sBtn_Click" Text="添加保存" /></td>
            </tr>
          </table></td>
      </tr>
      <tr>
        <td style="height: 100px">&nbsp;</td>
      </tr>
    </table>
  </div>
</form>
</body>
</html>