<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BKList.aspx.cs" Inherits="Manage_Guest_BKList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>词条版本浏览</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True">
        <Columns>
          <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="版本号">
                <ItemTemplate>
                    <%#Eval("VerStr","").Equals(VerStr)?Eval("VerStr")+"<span style='color:red;'>(当前版本)</span>":Eval("VerStr") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="提交人" DataField="UserName" />
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                   <%#GetStatus() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="/Guest/Baike/Details.aspx?EditID=<%#Eval("ID") %>" target="_blank" class="option_style"><i class="fa fa-eye" title="预览"></i></a>
                    <a href="/Baike/BKEditor.aspx?EditID=<%#Eval("ID") %>&mode=admin" target="_blank" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton runat="server" OnClientClick="return confirm('确实要删除吗？');" CommandArgument='<%#Eval("ID") %>' CommandName="Del" CausesValidation="false" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <asp:LinkButton runat="server" OnClientClick="return confirm('确定要使用该版词条吗?');" CommandName="apply" CommandArgument='<%#Eval("ID") %>' class="option_style"><i class="fa fa-flag"></i>应用</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>