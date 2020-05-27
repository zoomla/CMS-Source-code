<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NodeScript.aspx.cs" Inherits="ZoomLaCMS.Manage.User.NodeScript" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>设置会员组权限</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
     <table class="table table-striped table-bordered table-hover">
    <tr align="center">
      <td colspan="2" class="spacingtitle"><b>会员组权限设置</b></td>
    </tr>
    <tr class="tdbg">
      <td class="tdbgleft" align="right" style="width: 100px;"><asp:ListBox ID="NodeList" runat="server" Height="282px" Width="258px"  SelectionMode="Multiple"></asp:ListBox></td>
      <td valign="top" style="padding:10px;" ><asp:CheckBoxList ID="CheckBoxList1" runat="server" CellSpacing="10">
          <asp:ListItem Value="look">查看</asp:ListItem>
          <asp:ListItem Value="addTo">录入</asp:ListItem>
          <asp:ListItem Value="Modify">修改</asp:ListItem>
          <asp:ListItem Value="Deleted">删除</asp:ListItem>
          <asp:ListItem Value="Columns">仅当前节点</asp:ListItem>
          <asp:ListItem Value="Comments">评论管理</asp:ListItem>
        </asp:CheckBoxList>
        <br />
        <asp:Button ID="Button1" runat="server" Text="添加" class="btn btn-primary" onclick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="取消" class="btn btn-primary" OnClientClick="parent.Dialog.close();return false;" /></td>
    </tr>
  </table>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Console.js"></script>
<script type="text/javascript" src="/js/Drag.js"></script>
<script type="text/javascript" src="/js/Dialog.js"></script>
<script>
    function GotoWirte(rows) {
        var mainright = window.top.main_right;
        //mainright.frames["main_right_" + parent.nowWindow].document.getElementById("G_Makers").value = rows.innerText;
        parent.Dialog.close();
    }
    function GotoWirteddd() {
        var mainright = window.top.main_right;
        mainright.frames["main_right_" + parent.nowWindow].location.reload();
        //parent.location.reload();
        parent.Dialog.close();
    }
</script>
</asp:Content>
