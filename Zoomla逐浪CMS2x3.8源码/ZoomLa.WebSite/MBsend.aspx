<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MBsend.aspx.cs" MasterPageFile="~/User/Empty.master" Inherits="MBsend" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>发送手机短信</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
      <table class="table table-striped table-bordered table-hover">
    <tr align="center">
      <td colspan="2" class="spacingtitle"><b>发送手机短信</b></td>
    </tr>
    <tr>
      <td align="right" style="height: 28px; width: 25%;">短信余额：</td>
      <td align="left"><asp:Label ID="LblMobile" runat="server" Text="Label"></asp:Label></td>
    </tr>
     <tr>
      <td align="right" style="height: 28px; width: 25%;">发送人：</td>
      <td align="left"><asp:Label ID="lblReceiveName" runat="server" Text="Label"></asp:Label></td>
    </tr>
    <tr>
      <td align="right" style="height: 28px; width: 25%;">接收人：</td>
      <td align="left"><asp:TextBox ID="txtReceiveName" CssClass="form-control text_300" runat="server" Text="Label" 
              Width="329px"></asp:TextBox></td>
    </tr>
    <tr>
      <td align="right" style="height: 28px; width: 25%;">网站名：</td>
      <td align="left"><asp:TextBox ID="txtSiteName" CssClass="form-control text_300" runat="server" Text="Label" 
              Width="328px"></asp:TextBox></td>
    </tr>
    <tr>
      <td align="right" style="height: 28px; width: 15%;">&nbsp;接收人号码：<br />
        (多条信息发送以半角逗号区隔，最多支持100个/次超100个请自行做循环))</td>
      <td><table id="TblAddMessage" width="100%" visible="true" runat="server">
          <tr>
            <td><asp:TextBox ID="TxtInceptUser" runat="server" Width="326px" class="form-control"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtInceptUser" runat="server">号码不能为空 </asp:RequiredFieldValidator></td>
            <td>&nbsp;</td>
          </tr>
        </table></td>
    </tr>
    <tr>
      <td align="right" style="height: 23px; width: 15%;">短信内容：<br />
        (字数70个字以内)</td>
      <td><asp:TextBox ID="EditorContent" runat="server" Rows="10" TextMode="MultiLine" Width="99%" class="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ValrContent" runat="server" ControlToValidate="EditorContent" ErrorMessage="*短消息内容不能为空" Display="Dynamic"></asp:RequiredFieldValidator></td>
    </tr>
    <tr align="center" class="tdbg">
      <td colspan="2" style="height: 50px;" align="center"><asp:Button ID="BtnSend" runat="server" Text="发送" OnClick="BtnSend_Click" class="C_input" />
        &nbsp;&nbsp;
        <asp:Button ID="BtnReset" runat="server" Text="清除" OnClick="BtnReset_Click" class="btn btn-primary" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" /></td>
    </tr>
  </table>
</asp:Content>

