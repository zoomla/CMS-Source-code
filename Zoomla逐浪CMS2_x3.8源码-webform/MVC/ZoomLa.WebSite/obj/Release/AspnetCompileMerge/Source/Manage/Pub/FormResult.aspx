<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormResult.aspx.cs" Inherits="ZoomLaCMS.Manage.Pub.FormResult"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>结果查询</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tbody><tr><th>ID</th><asp:Literal runat="server" ID="TableHead_Lit"></asp:Literal><th>IP地址</th><th>操作</th></tr></tbody>
        <asp:Repeater runat="server" ID="RPT" OnItemDataBound="RPT_ItemDataBound" OnItemCommand="RPT_ItemCommand">
            <ItemTemplate>
                <tr><td><label><input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /> <%#Eval("ID") %></label></td><asp:Literal runat="server" ID="TableValue_Lit" EnableViewState="false"></asp:Literal><td><%#Eval("IP地址") %></td>
                    <td><asp:LinkButton runat="server" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="reutrn confirm('确定要删除吗?');">删除</asp:LinkButton></td></tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div runat="server" id="EmptyDiv" visible="false" class="alert alert-info">该表单下尚无互动信息!</div>
    <asp:Literal runat="server" ID="Page_Lit" EnableViewState="false"></asp:Literal>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
