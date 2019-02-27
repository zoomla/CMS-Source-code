<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BorrowList.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Doc.BorrowList" MasterPageFile="~/Office/OAMain.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>借阅列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href="/Office/Main.aspx">办公管理</a></li>
        <li><a href="FiledList.aspx">已归档公文</a></li>
        <li class="active">借阅记录</li>
    </ol>
    <div style="height: 40px;"></div>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" 
        OnPageIndexChanging="EGV_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有借阅记录">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="借阅公文">
                <ItemTemplate><%#SubStr(Eval("DocTitles","")) %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="借阅人">
                <ItemTemplate><%#SubStr(Eval("UNames","")) %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate><%#SubStr(Eval("Remind","")) %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="AddBorrow.aspx?id=<%#Eval("ID") %>">修改</a>
                    <asp:LinkButton runat="server" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">

</asp:Content>