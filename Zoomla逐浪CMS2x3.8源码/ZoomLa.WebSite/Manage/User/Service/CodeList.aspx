<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CodeList.aspx.cs" Inherits="Manage_User_CodeList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>客服代码列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" 
        OnPageIndexChanging="EGV_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate><input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="名称" DataField="Str1" />
            <asp:BoundField HeaderText="日期" DataField="CDate"/>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <a href="ServiceCode.aspx?ID=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i>修改</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>