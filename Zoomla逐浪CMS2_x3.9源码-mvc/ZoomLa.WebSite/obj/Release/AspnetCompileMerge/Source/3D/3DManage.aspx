<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="3DManage.aspx.cs" Inherits="ZoomLaCMS._3D._3DManage" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>3D商城管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" >
<Columns>
    <asp:BoundField ItemStyle-CssClass="did" HeaderText="ID" DataField="ID"/>
    <asp:BoundField HeaderText="店铺名" DataField="ShopName"/>
    <asp:BoundField HeaderText="店铺图片" DataField="ShopImg"/>
    <asp:BoundField HeaderText="用户名" DataField="UserName"/>
    <asp:BoundField HeaderText="位置X轴" DataField="posX"/>
    <asp:BoundField HeaderText="位置Y轴" DataField="posY"/>
    <asp:TemplateField HeaderText="操作">
    <ItemTemplate>
    <a class="option_style" href="AddShop.aspx?ID=<%#Eval("ID") %>" title="修改"><i class="fa fa-pencil" title="修改"></i></a>
    <asp:LinkButton ID="LinkButton1" CssClass="option_style" runat="server" CommandName="Del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('你确定要删除吗!');" ToolTip="删除"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
    </ItemTemplate>
    </asp:TemplateField>
</Columns>
<PagerStyle HorizontalAlign="Center" />
<RowStyle Height="24px" HorizontalAlign="Center"  />
</ZL:ExGridView>
</div>
<script>
$().ready(function () {
    $("#EGV tr").dblclick(function () {
        var id = $(this).find(".did").text();
        if (id) {
            location = "AddShop.aspx?ID="+id;
        }
    });
});
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>