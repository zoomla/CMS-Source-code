<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogTypeEdit.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="manage_Zone_LogTypeEdit" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>添加族群类型</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
    <div class="clearbox"></div> 
<table class="table table-striped table-bordered table-hover">
  <tr class="tdbg">
    <td colspan="2" align="center"  class="spacingtitle" >族群类型修改</td>
  </tr>
  <tr class="tdbg">
    <td width="24%" align="center" class="tdbgleft">类型名称：</td>
    <td width="76%">&nbsp;<asp:TextBox ID="Nametxt" runat="server" MaxLength="15"></asp:TextBox>
        <span style="color: #ff0000">* </span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Nametxt"
            ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
  </tr>
  <tr class="tdbg">
    <td align="center" class="tdbgleft">&nbsp;</td>
    <td>&nbsp;<asp:Button ID="addbtn" CssClass="btn btn-primary" runat="server" Text="保  存" Width="84px" OnClick="addbtn_Click" /></td>
  </tr>
</table>
    </div>
</asp:Content>
