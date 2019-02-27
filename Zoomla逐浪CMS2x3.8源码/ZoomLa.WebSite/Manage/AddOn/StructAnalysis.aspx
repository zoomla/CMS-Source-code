<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="StructAnalysis.aspx.cs" Inherits="manage_AddOn_StructAnalysis" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server"><title>分析结构</title></asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div>
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" IsHoldState="false"
            class="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!"
            OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="<input type='checkbox' id='allChk' />">
                    <ItemTemplate>
                        <input type="checkbox" name="idChk" value="<%#Eval("ID") %>" />
                    </ItemTemplate>
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="ID" DataField="ID" />
                <asp:BoundField HeaderText="名称" DataField="Name" />
                <asp:TemplateField HeaderText="成员数">
                    <ItemTemplate><%#GetNums(Eval("ID")) %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('你确定要删除吗!');" ToolTip="删除">
                                <img src="/App_Themes/AdminDefaultTheme/images/del.png" /></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <RowStyle Height="24px" HorizontalAlign="Center" />
        </ZL:ExGridView>
    </div>
</asp:Content>