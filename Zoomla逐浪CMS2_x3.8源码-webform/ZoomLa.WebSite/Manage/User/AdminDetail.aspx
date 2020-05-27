<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminDetail.aspx.cs" Inherits="manage_User_AdminDetail" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>管理员预览</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr><td class="tdbgleft td_l">管理员名：</td>
            <td><asp:Label ID="tbdName" runat="server"></asp:Label></td></tr>
        <tr><td class="tdbgleft">真实姓名：</td>
            <td><asp:Label ID="txtAdminTrueName" runat="server"></asp:Label></td></tr>
        <tr><td class="tdbgleft">选项设置：</td>
            <td><asp:Label ID="cb1" runat="server"></asp:Label>
                <asp:Label ID="cb2" runat="server"></asp:Label>
                <asp:Label ID="cb3" runat="server"></asp:Label></td>
        </tr>
        <tr><td class="tdbgleft">管理员角色：</td><td><asp:Label ID="cblRoleList1" runat="server"></asp:Label></td></tr>
        <tr><td class="tdbgleft">默认节点控制权限：</td><td><asp:Label ID="DropDownList11" runat="server"></asp:Label></td></tr>
        <tr><td class="tdbgleft">是否前台审核互动：</td><td><asp:Label ID="CheckBox1" runat="server"></asp:Label></td></tr>
        <tr><td class="tdbgleft">发布内容默认状态：</td><td><asp:Label ID="DefaultStart1" runat="server"></asp:Label></td></tr>
        <tr>
            <td class="tdbgleft"></td>
            <td>
                <a class="btn btn-primary" href="AddManage.aspx?id=<%:Mid %>">重新修改</a>
                <a class="btn btn-primary" href="AddManage.aspx">继续添加管理员</a>
                <a class="btn btn-primary" href="AdminManage.aspx">返回列表</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
