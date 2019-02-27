<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisitDetail.aspx.cs" Inherits="Manage_Design_Addon_VisitDetail" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>访问详情</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="20" IsHoldState="false" BoxType="dp"
    OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
    CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
    <Columns>
        <asp:BoundField HeaderText="ID" DataField="ID" />
        <asp:BoundField HeaderText="场景标题" DataField="InfoTitle" />
        <asp:TemplateField HeaderText="访问人">
            <ItemTemplate>
                <%#GetUser() %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="类型">
            <ItemTemplate>
                <%#GetSEType() %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="IP地址">
            <ItemTemplate>
                <span title="<%#Eval("IP") %>" style="cursor: pointer"><%#GetIpLocation() %></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="访问时间" DataField="CDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
        <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                <asp:LinkButton ID="del_btn" runat="server" CssClass="option_style" CommandName="del" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定删除这条浏览记录吗?')"><i class="fa fa-trash-o"></i>删除</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</ZL:ExGridView>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
var userdiag = new ZL_Dialog();
function showuser(id) {
    userdiag.reload = true;
    userdiag.title = "查看用户";
    userdiag.url = "../../User/Userinfo.aspx?id="+id;
    userdiag.backdrop = true;
    userdiag.maxbtn = false;
    userdiag.ShowModal();
}
</script>
</asp:Content>