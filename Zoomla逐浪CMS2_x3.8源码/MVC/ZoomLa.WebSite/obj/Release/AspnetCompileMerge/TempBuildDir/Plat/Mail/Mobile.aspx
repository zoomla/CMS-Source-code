<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mobile.aspx.cs" Inherits="ZoomLaCMS.Plat.Mail.Mobile"   MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<title>手机短信</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <div class="mainDiv container platcontainer">
<%--    <div class="container-fluid">
      <ol class="breadcrumb">
        <li><a href="/Plat/Blog/">首页</a></li>
        <li><a href="/Plat/Task/">邮件</a></li>
        <li class="active">手机短信</li>
      </ol>
    </div>--%>
    <div class="btn-group Messge_nav"> <a class="btn btn-info" href="MessageSend.aspx">写邮件</a> <a class="btn btn-info" href="Default.aspx">收件箱</a> <a class="btn btn-info" href="MessageOutbox.aspx">发件箱</a> <a class="btn btn-info" href="MessageDraftbox.aspx">草稿箱</a> <a class="btn btn-info" href="MessageGarbagebox.aspx">垃圾箱</a> <a class="btn btn-info" href="Mobile.aspx">手机短信</a> </div>
    <div class="clearfix"></div>
    <table class="table table-border table-hover" style="margin-top: 10px;">
      <tr>
        <td align="right" style="height: 28px; width: 15%;">接收方手机号码：</td>
        <td><asp:TextBox ID="TxtInceptUser" runat="server" Width="326px" />
          <%--(多条信息发送以半角逗号区隔，最多支持100个/次超100个请自行做循环)--%>
          一次只能发给一个号码一条短信
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtInceptUser" runat="server" ForeColor="Red">手机号码不能为空 </asp:RequiredFieldValidator></td>
      </tr>
      <tr>
        <td align="right" style="height: 23px; width: 15%;">短信内容：<br />
          (字数70个字以内)</td>
        <td><asp:TextBox ID="EditorContent" runat="server" Rows="10" TextMode="MultiLine" Width="99%" class="x_input"></asp:TextBox>
          <asp:RequiredFieldValidator ID="ValrContent" runat="server" ControlToValidate="EditorContent" ErrorMessage="短消息内容不能为空" Display="Dynamic">*</asp:RequiredFieldValidator></td>
      </tr>
      <tr align="center">
        <td colspan="2" style="height: 50px;" align="center"><asp:Button ID="BtnSend" runat="server" Text="发送" OnClick="BtnSend_Click" class="btn btn-info" />
          &nbsp;&nbsp;
          <%--          <asp:Button ID="BtnReset" runat="server" Text="清除" OnClientClick="return CheckIsMobile(document.getElementById('TxtInceptUser').value)" OnClick="BtnReset_Click" class="btn btn-info"/>--%>
          <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" /></td>
      </tr>
    </table>
    <div style="border: 1px solid #CCC; padding: 10px; margin-top: 10px;">
      <asp:Label ID="LblMobile" runat="server" Text="Label" Style="color: Red;"></asp:Label>
      提示：系统支持以Mobile.aspx?MB=[手机号码]&txt=[发送内容]方式GET将手机号码传值发送，如：Mobile.aspx?MB=13177777714&txt=默认短信内容。 </div>
  </div>
</asp:Content>
