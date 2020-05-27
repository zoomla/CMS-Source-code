<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleManage.aspx.cs" Inherits="ZoomLaCMS.Manage.User.RoleManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>角色管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" DataKeyNames="RoleID"
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:BoundField DataField="RoleID" HeaderText="ID" ItemStyle-CssClass="td_s"></asp:BoundField>
            <asp:TemplateField HeaderText="角色名">
                <ItemTemplate>
                    <a href="AddRole.aspx?RoleID=<%#Eval("RoleID") %>"><%#Eval("RoleName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="描述"></asp:BoundField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkEdit" CommandName="ModifyRole" CommandArgument='<%# Eval("RoleID")%>' runat="server" CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                    <asp:LinkButton ID="LnkDel" CommandName="Del" CommandArgument='<%# Eval("RoleID")%>' OnClientClick="return confirm('确认删除该角色?')" runat="server" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <asp:LinkButton ID="Userlist" CommandName="ListRolelist" CommandArgument='<%# Eval("RoleID")%>' runat="server" CssClass="option_style"><i class="fa fa-users" title="成员管理"></i>成员管理</asp:LinkButton>
                    <asp:LinkButton ID="AuthEdit" CommandName="ModifyPower" CommandArgument='<%# Eval("RoleID")%>' runat="server" CssClass="option_style"><i class="fa fa-key" title="权限设置"></i>权限设置</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
</ZL:ExGridView>
    <div class="clearbox"></div>
    <div class="alert alert-info"><img src="/Images/notice.gif"/>超级管理员拥有最高的权限，不能修改其权限，删除该组!</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        $().ready(function () { $(".allchk_l").hide(); });
    </script>
</asp:Content>
