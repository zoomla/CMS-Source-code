<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MisModelManage.aspx.cs" Inherits="ZoomLaCMS.Manage.WorkFlow.MisModelManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>模板管理</title>
    <style>
        .allchk_l{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style=" margin-bottom:10px;">
        <div class="input-group" style="width:300px;">
            <asp:TextBox runat="server" ID="searchText" placeholder="请输入需要查询的信息" CssClass=" form-control" />
            <span class="input-group-btn">
                <asp:Button runat="server" CssClass="btn btn-primary" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click" />
            </span>
        </div>
    </div>
    <div>
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" EnableTheming="False"
            GridLines="None" CellPadding="2" CellSpacing="1" Width="100%" CssClass="table table-striped table-bordered table-hover" 
            OnPageIndexChanging="EGV_PageIndexChanging" DataKeyNames="ID" OnRowDataBound="EGV_RowDataBound" OnRowCommand="EGV_RowCommand"  
            AllowUserToOrder="true" EmptyDataText="当前没有类型!!">
            <Columns>
                <asp:BoundField DataField="ID" ItemStyle-CssClass="td_s" />
                <asp:BoundField HeaderText="模板名称" DataField="ModelName" />
                <asp:TemplateField HeaderText="模板类型" ItemStyle-CssClass="td_l">
                    <ItemTemplate><%#bmis.GetDocType(Convert.ToInt32(Eval("DocType"))) %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="创建时间" DataField="CreateTime"  ItemStyle-CssClass="td_l"/>
                <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="td_l">
                    <ItemTemplate>
                        <a href="AddMisModel.aspx?ID=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                        <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("ID") %>' runat="server" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
    </div>
</asp:Content>
