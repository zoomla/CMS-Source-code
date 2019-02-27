<%@ Page Language="C#" AutoEventWireup="true" CodeFile="APIList.aspx.cs" Inherits="Manage_Mobile_Push_APIList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>API列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="BreadDiv" class="container-fluid mysite">
    <div class="row">
        <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
            <li><a href="/Admin/Main.aspx">工作台</a></li>
            <li><a href="Default.aspx">消息推送</a></li>
            <li class="active"><a href="/admin/Mobile/Push/APIList.aspx">API列表</a> [<a href="AddAPI.aspx">添加API</a>]</li>
             <a href="https://www.jpush.cn" target="_blank" class="pull-right" style="margin-right:10px;"><i class="fa fa-lightbulb-o"></i> 注册账号</a>
        </ol>
    </div>
</div> 
 <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="尚未添加需要推送的APP">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="APP名称" DataField="Alias" />
            <asp:BoundField HeaderText="Key" DataField="APPKey" />
            <asp:BoundField HeaderText="Secret" DataField="APPSecret" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a class="option_style" href="AddAPI.aspx?id=<%#Eval("ID") %>"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>