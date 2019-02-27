<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IPWeb.ascx.cs" Inherits="Manage_I_Config_IPWeb" Debug="true" ClientIDMode="AutoID"%>

<div style="width: 420px" runat="server">
    <asp:Panel ID="Panel1" runat="server" Width="100%">
        <ZL:ExGridView ID="EgvIPLock" PageSize="20" runat="server" Width="100%" OnRowEditing="EgvIPLock_RowEditing"
                OnRowDeleting="EgvIPLock_RowDeleting" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="IPFrom" HeaderText="起始IP">
                        <HeaderStyle Width="40%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IPTo" HeaderText="结尾IP">
                        <HeaderStyle Width="40%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="操 作">
                        <HeaderStyle Width="20%" />
                        <ItemTemplate>
                            <asp:LinkButton ID="LbtnEdit" runat="server" CommandName="Edit" CausesValidation="False">编辑</asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="LbtnDelete" runat="server" CommandName="Delete" CausesValidation="False">删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </ZL:ExGridView>
            <div style="text-align: center">
                <span class="pull-left" style="line-height:32px;">起始IP：</span><asp:TextBox ID="TxtFrom" CssClass="form-control pull-left" runat="server" Width="100px"></asp:TextBox>
                <span class="pull-left" style="line-height:32px;margin-left:5px;">结尾IP：</span><asp:TextBox ID="TxtTo" CssClass="form-control pull-left" runat="server" Width="100px"></asp:TextBox>
                <asp:Button ID="BtnSave" OnClick="BtnSave_Click" CssClass="btn btn-primary" runat="server" Text="添加"></asp:Button><br />
            </div>
    </asp:Panel>
    <div style="text-align: center">
        <asp:RegularExpressionValidator ID="ValeIPFrom" runat="server" ControlToValidate="TxtFrom"
            Display="None" ErrorMessage="起始IP不是有效的IP地址！" SetFocusOnError="True" ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="ValrIPFrom" runat="server" ControlToValidate="TxtFrom"
            Display="None" ErrorMessage="起始IP不能为空！" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="ValrIPTo" runat="server" ControlToValidate="TxtTo"
            Display="None" ErrorMessage="结尾IP不能为空！" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="ValeIPTo" runat="server" ControlToValidate="TxtTo"
            Display="None" ErrorMessage="结尾IP不是有效的IP地址！" SetFocusOnError="True" ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$"></asp:RegularExpressionValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </div>
</div>