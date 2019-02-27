<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModelContract.aspx.cs" Inherits="Manage_Content_Addon_ModelContract" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>模型对比</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered">
        <tr>
            <td><asp:Label runat="server" ID="SModel_L" /></td>
            <td><asp:Label runat="server" ID="TModel_L" /></td>
            <td>操作</td>
        </tr>
    <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
        <ItemTemplate>
            <tr <%#Eval("op","{0}").Equals("新增")?"class='addtr'":"" %>>
                <td><%#Eval("FieldName") %><span>(别名:<%#Eval("FieldAlias") %>)</span><span>(类型:<%#Eval("FieldType") %>)</span></td>
                <td><%#Eval("tfield") %></td>
                <td><%#Eval("op") %></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
        </table>
    <div class="alert alert-info">提示:仅对比非系统字段</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .addtr {background-color:#f2dede;color:#a94442;}
        .addtr td {border-color:#ebccd1;}
    </style>
</asp:Content>