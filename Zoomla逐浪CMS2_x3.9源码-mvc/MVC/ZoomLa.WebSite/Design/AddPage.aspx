<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPage.aspx.cs" Inherits="ZoomLaCMS.Design.AddPage" MasterPageFile="~/Common/Common.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head"><title>新建页面</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-striped">
        <tr><td class="td_m">访问路径：</td><td>
            <asp:TextBox runat="server" ID="Path_T" CssClass="form-control eng" MaxLength="50" />
            <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="Path_T" ErrorMessage="路径不能为空" ForeColor="Red" />
        </td></tr>
        <tr><td>页面标题：</td><td>
            <asp:TextBox runat="server" ID="Title_T" CssClass="form-control" MaxLength="50" />
            <asp:RequiredFieldValidator runat="server" ID="R2"  ControlToValidate="Title_T" ErrorMessage="标题不能为空" ForeColor="Red" />
        </td></tr>
        <tr><td>类型：</td><td>
            <label><input type="radio" value="0" name="type_rad" checked="checked" />普通页面</label>
        <%--    <label><input type="radio" value="1" name="type_rad"/>模板</label>--%>
            <label><input type="radio" value="2" name="type_rad" />全局组件</label></td></tr>
        <tr><td></td><td><asp:Button runat="server" ID="Save_Btn" CssClass="btn btn-primary" Text="新建" OnClick="Save_Btn_Click" /></td></tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/SelectCheckBox.js"></script>
</asp:Content>