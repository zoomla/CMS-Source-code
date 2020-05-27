<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliverType.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.DeliverType" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>运费模板</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Egv_RowCommand" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无信息！！">
        <Columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                    <%#Eval("ID") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="模板名称">
                <ItemTemplate>
                     <a href="AddDeliverType.aspx?id=<%#Eval("id") %>"><%#Eval("TlpName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="计价方式">
                <ItemTemplate>
                     <%#GetMode() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="备注" DataField="Remind" />
            <asp:BoundField HeaderText="备注(仅卖家)" DataField="Remind2" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a class="option_style" href="AddDeliverType.aspx?id=<%#Eval("id") %>"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton ID="LinkButton1" CssClass="option_style" CommandName="Del" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" runat="server"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    说明：“禁用”某送货方式后，前台订购时将不再显示此送货方式，但已有订单中仍然显示。
</asp:Content>