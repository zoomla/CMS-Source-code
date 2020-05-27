<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupAdmin.aspx.cs" Inherits="ZoomLaCMS.Plat.Admin.GroupAdmin"  MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>部门管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
  <div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">部门列表</span> <a href="AddGroup.aspx" class="child_head_a">+创建新部门</a> </div>
<table class="table table-bordered table-striped">
    <tr><td class="td_s">ID</td><td>名称</td><td>成员数</td><td>管理员</td><td>操作</td></tr>
    <tr><td></td><td><i class="fa fa-folder-open"></i><asp:Label runat="server" ID="CompName_L"></asp:Label></td>
        <td></td>
        <td></td>
        <td>
            <a href="AddGroup.aspx" class="option_style"><i class="fa fa-plus"></i>添加</a>
            <a href="javascript:;" onclick="showsort('0');"><i class="fa fa-sort"></i>排序</a>
        </td>
    </tr>
    <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
        <ItemTemplate>
            <tr id="tr_<%#Eval("id") %>" ondblclick="location.href='AddGroup.aspx?ID=<%#Eval("ID") %>'">
                <td><%#Eval("ID") %></td>
                <td><%#ShowName() %></td>
                <td><%#Eval("MemberIDS").ToString().Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries).Length %></td>
                <td><%# GetManageName(Eval("ManageIDS").ToString()) %></td>
                <td>
                    <a href="AddGroup.aspx?ID=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil"></i>修改</a>
                    <a href="AddGroup.aspx?Pid=<%#Eval("ID") %>" class="option_style"><i class="fa fa-plus"></i>添加</a>
                    <a href="javascript:;" onclick="showsort('<%#Eval("ID") %>');"><i class="fa fa-sort"></i>排序</a>
                    <a href="javascript:;" class="option_style" onclick="del('<%#Eval("id") %>');"><i class="fa fa-trash"></i>删除</a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    function del(id) {
        if (!confirm("确定要移除该部门吗")) { return false; }
        $("#tr_" + id).remove();
        $.post("GroupAdmin.aspx?action=del", { "id": id }, function (data) {
            console.log(data);
        });
    }
    function showsort() {
        ShowComDiag("GroupSort.aspx","部门排序");
    }
</script>
</asp:Content>