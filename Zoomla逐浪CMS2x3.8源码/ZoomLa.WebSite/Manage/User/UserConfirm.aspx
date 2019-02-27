<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserConfirm.aspx.cs" Inherits="manage_User_UserConfirm" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>印证情况</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
     <div> 印证情况：
    <asp:RadioButtonList ID="Confirm" runat="server" RepeatDirection="Horizontal" >
      <asp:ListItem Value="0">没有</asp:ListItem>
      <asp:ListItem Value="1" Selected="True">有</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <asp:Button ID="submit" runat="server" Text="提交" onclick="submit_Click" class="btn btn-primary"/>
  </div>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
