<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuickPrint.aspx.cs" Inherits="Manage_Shop_Feyin_QuickPrint" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>模板管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate><input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="模板名称" ItemStyle-CssClass="td_l">
                <ItemTemplate><a href="AddQuickPrint.aspx?ID=<%#Eval("ID") %>"><%#Eval("Alias") %></a></ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="内容">
               <ItemTemplate><%#GetContent() %></ItemTemplate>
           </asp:TemplateField>
            <asp:BoundField HeaderText="日期" DataField="CDate" DataFormatString="{0:yyyy年MM月dd日}" ItemStyle-CssClass="td_l" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a class="option_style" href="AddQuickPrint.aspx?id=<%#Eval("ID") %>"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
