<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditCus.aspx.cs" Inherits="manage_User_EditCus" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>编辑来源</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
     <table class="table table-striped table-bordered table-hover">
      <tr align="center" class="title">
        <td><strong>升级提示</strong></td>
      </tr>
      <tr class="tdbg">
        <td valign="top" height="100"><br />
          该客户已存在，请选择以下操作:
          <asp:RadioButtonList ID="rbSelect" runat="server" CssClass="radioBut">
            <asp:ListItem Value="1">系统自动改名升级</asp:ListItem>
            <asp:ListItem Value="2">覆盖旧有客户信息</asp:ListItem>
            <asp:ListItem Value="3">取消操作返回列表</asp:ListItem>
          </asp:RadioButtonList></td>
      </tr>
      <tr align="center" class="tdbg">
        <td><asp:Button ID="btnTrue" runat="server" Text="确　定" class="btn btn-primary" Width="60px" onclick="btnTrue_Click" />
          <asp:Button ID="btnCan" runat="server" Text="取  消" class="btn btn-primary" Width="60px"  onclick="btnCan_Click"/></td>
      </tr>
    </table>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
