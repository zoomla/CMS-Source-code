<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgioList.aspx.cs" Inherits="manage_Shop_AgioList" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>促销方案管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Egv_RowCommand" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="方案名称">
                <ItemTemplate>
                    <%=proName %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="数量区限">
                <ItemTemplate>
                    <%# Eval("SIULimit") + " 到 " +  Eval("SILLimit")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="折扣">
                <ItemTemplate>
                    <%# Eval("SIAgio")%>%
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href='AddAgio.aspx?AID=<%=Request.QueryString["ID"].ToString()%>&ID=<%# Eval("ID")%>'>修改</a>
                    <asp:LinkButton ID="LinkButton1" CommandName="Del" CommandArgument='<%# Eval("ID") %>' runat="server">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
