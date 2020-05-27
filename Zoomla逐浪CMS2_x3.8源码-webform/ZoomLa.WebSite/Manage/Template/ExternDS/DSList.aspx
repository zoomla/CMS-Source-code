<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DSList.aspx.cs" Inherits="Manage_Template_ExternDS_DSList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>数据源列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ZL:ExGridView ID="EGV" runat="server" AllowPaging="True" AutoGenerateColumns="false" IsHoldState="false" 
    CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EmptyDataText="没有任何数据！" EnableModelValidation="True"
    OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <input type="checkbox" name="idchk" value="<%# Eval("ID") %>" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="DSName" ControlStyle-CssClass="form-control text_md" HeaderText="名称" />
        <asp:TemplateField HeaderText="类型">
            <ItemTemplate>
                <%#Eval("Type","")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="连接字符串">
            <ItemTemplate>
                <%# Eval("ConnectionString") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="创建时间">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "CreateTime", "{0:yyyy年M月d日}")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CreateMan" HeaderText="创建者" ReadOnly="true" />
        <asp:TemplateField HeaderText="描述" ItemStyle-Width="20%">
            <ItemTemplate>
                <%# Eval("Remind") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                <a href="DSAdd.aspx?ID=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                <asp:LinkButton runat="server" CommandName="del2" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('确定删除该数据源?');" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>