<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="SelectUserName.aspx.cs" Inherits="manage_AddOn_SelectProjectName" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server"><title>查询用户</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="Flow" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无客户信息！">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="Flow" HeaderStyle-Width="5%" />
            <asp:BoundField HeaderText="客户名称" DataField="P_name" HeaderStyle-Width="15%" />
            <asp:TemplateField HeaderText="客户类别">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%#Eval("Client_Type","{0}")=="1"?"企业":"个人"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="客户编号" DataField="Code" HeaderStyle-Width="15%" />
            <asp:BoundField HeaderText="客户组别" DataField="Client_Group" HeaderStyle-Width="10%" />
            <asp:BoundField HeaderText="客户来源" DataField="Client_Source" HeaderStyle-Width="10%" />
            <asp:TemplateField HeaderText="操作">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <a href="javascript:;" onclick="getUser('<%#Eval("P_name") %>')">选择</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function onstr() {
            parent.Dialog.close();
        }
        function setvalue(objname, valuetxt) {
            if (parent.window.frames['main_right'].document.getElementById(objname)) {
                parent.window.frames['main_right'].document.getElementById(objname).value = valuetxt;
            }
            else {
                parent.document.frames['main_right'].document.getElementById(objname).value = valuetxt;
            }
        }
        function getUser(name) {
            parent.getUser(name);
        }
    </script>
</asp:Content>