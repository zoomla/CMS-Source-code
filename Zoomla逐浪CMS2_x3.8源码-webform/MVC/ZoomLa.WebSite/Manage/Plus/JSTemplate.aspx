<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JSTemplate.aspx.cs" Inherits="ZoomLaCMS.Manage.Plus.JSTemplate" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>广告管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="JSTemplateID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" OnRowDataBound="Egv_RowDataBound" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无相关数据！">
         <Columns>
            <asp:BoundField DataField="JSTemplateID" HeaderText="类型ID">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="JSTemplateName" HeaderText="版块类型名称">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="JSTemplatePath" HeaderText="版块文件所在路径">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="JSTemplateSize" HeaderText="版块大小">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="EditJSTemplate.aspx?ZoneType=<%# Eval("JSTemplateID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i>修改模板</a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js" ></script>
    <script type="text/javascript" src="/js/Common.js"></script>
</asp:Content>