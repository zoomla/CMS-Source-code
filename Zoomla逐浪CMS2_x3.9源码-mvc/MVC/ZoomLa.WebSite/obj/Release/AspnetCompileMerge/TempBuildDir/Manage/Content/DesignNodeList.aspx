<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesignNodeList.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.DesignNodeList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>动力节点详情</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
    <ZL:ExGridView runat="server" ID="EGV" AllowPaging="true" PageSize="20" OnRowDataBound="EGV_RowDataBound" AutoGenerateColumns="false" OnPageIndexChanging="EGV_PageIndexChanging" CssClass="table table-striped table-bordered table-hover nodelist_div">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="NodeID" />
            <asp:BoundField HeaderText="节点名称" DataField="NodeName" />
            <asp:TemplateField HeaderText="节点类型">
                <ItemTemplate>
                    <%#GetNodeType(Eval("NodeType","{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField HeaderText="文章总数" DataField="ItemCount" />--%>
            <asp:BoundField HeaderText="创建时间" DataField="CDate" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <%#GetOper()%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>