<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowTypeList.aspx.cs" Inherits="ZoomLaCMS.Manage.WorkFlow.FlowTypeList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>类型列表</title>
    <style>
        #AllID_Chk{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="margin-bottom:10px;">
        <div class="input-group" style="width:300px;">
            <asp:TextBox runat="server" ID="searchText" placeholder="请输入需要查询的信息" CssClass=" form-control" />
            <span class="input-group-btn">
                <asp:Button runat="server" CssClass="btn btn-primary" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click"/>
            </span>
        </div>
    </div>
<div>
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"  EnableTheming="False"  GridLines="None" CellPadding="2" CellSpacing="1"  Width="100%" CssClass="table table-striped table-bordered table-hover" RowStyle-CssClass="tdbg" AllowUserToOrder="true" BackColor="White" OnPageIndexChanging="EGV_PageIndexChanging" DataKeyNames="ID" OnRowDataBound="EGV_RowDataBound" OnRowCommand="EGV_RowCommand"  EmptyDataText="当前没有类型!!">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:BoundField HeaderText="类型名称" DataField="TypeName" />
            <asp:BoundField HeaderText="类型描述" DataField="TypeDescribe" />
            <asp:BoundField HeaderText="创建时间" DataField="CreateTime" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="AddWorkFlowType.aspx?ID=<%# Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton ID="LinkButton2" CommandName="Del"  CommandArgument='<%# Eval("ID") %>' runat="server" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center"/>
        <RowStyle HorizontalAlign="Center" />
    </ZL:ExGridView>
</div>
</asp:Content>