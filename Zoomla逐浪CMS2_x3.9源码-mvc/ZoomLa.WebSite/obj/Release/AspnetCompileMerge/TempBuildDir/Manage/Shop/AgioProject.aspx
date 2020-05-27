<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgioProject.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.AgioProject" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>促销方案管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Egv_RowCommand" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无促销方案！！">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="打折类型">
                <ItemTemplate>
                    <%#Gettype(Eval("SType").ToString()) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="方案名称">
                <ItemTemplate>
                    <a href="AddAgioProject.aspx?ID=<%# Eval("ID") %>"><%# Eval("SName")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="有效期">
                <ItemTemplate>
                    <%#GetDate( Eval("SStartTime").ToString(),Eval("SEndTime").ToString())%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="商品列表">
                <ItemTemplate>
                    <a href="#" onclick='SelectProducer(<%# Eval("ID")%>,<%# Eval("SType")%>)'>详细商品列表</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="方案打折列表">
                <ItemTemplate>
                    <a href='AgioList.aspx?ID=<%# Eval("ID")%>'>方案打折详细列表</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" CommandName="Del" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('确定删除吗？');" runat="server" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function SelectProducer(num, type) {
            window.open('AgioCommodityShow.aspx?KeyWord=' + num + '&KeyType=' + type, '', 'width=600,height=450,resizable=0,scrollbars=yes');
        }
    </script>
</asp:Content>