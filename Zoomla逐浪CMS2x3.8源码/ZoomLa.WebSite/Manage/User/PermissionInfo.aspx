<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PermissionInfo.aspx.cs" Inherits="manage_Config_PermissionInfo" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>角色管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"   EmptyDataText="无数据"
        EnableTheming="False" OnRowDataBound="EGV_RowDataBound" class="table table-striped table-bordered table-hover" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" DataKeyNames="ID">
        <Columns>
            <asp:TemplateField HeaderStyle-CssClass="td_x" >
                <ItemTemplate>
                    <input type="checkbox" value="<%#Eval("ID") %>" name="idchk" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="角色名称" >
                <ItemTemplate>
                    <a href="Permissionadd.aspx?menu=edit&id=<%#Eval("ID") %>"><%#Eval("RoleName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="角色说明" DataField="info" />
            <asp:BoundField HeaderText="优先级别" DataField="Precedence" />
            <asp:TemplateField HeaderText="是否启用" >
                <ItemTemplate>
                    <%#Eval("IsTrue","{0}")=="True"?"<font color=green>启用</font>":"<font color=red>停用</font>"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="相关操作">
                <ItemTemplate>
                    <a href="Permissionadd.aspx?menu=edit&id=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil"></i> </a>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del2" OnClientClick="return confirm('确定删除?');" CssClass="option_style"><i class="fa fa-trash" title="删除"></i> </asp:LinkButton>
                    <a href="CompetenceAdd.aspx?id=<%#Eval("ID") %>" class="option_style"><i class="fa fa-key" title="权限设置"></i>权限设置</a>
                    <a href="SelUsersToRole.aspx?id=<%#Eval("ID") %>" class="option_style"><i class="fa fa-users" title="用户管理"></i>用户管理</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button runat="server" ID="BatDel_Btn" CssClass="btn btn-primary" Text="批量移除" OnClick="BatDel_Btn_Click" OnClientClick="return confirm('确定要移除吗?');" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript">
    function Modify(id) {
        location.href = "Permissionadd.aspx?menu=edit&id=" + id;
    }
</script>
</asp:Content>


