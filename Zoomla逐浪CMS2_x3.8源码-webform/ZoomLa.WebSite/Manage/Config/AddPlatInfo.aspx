<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPlatInfo.aspx.cs" Inherits="Manage_Config_AddPlatInfo" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head"><title>信息管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-striped">
        <tr>
            <td class="td_m">名称</td>
            <td>
                <ZL:TextBox runat="server" ID="Name_T" CssClass="form-control text_300" AllowEmpty="false" /></td>
        </tr>
        <tr>
            <td>标识</td>
            <td>
                <asp:TextBox runat="server" CssClass="form-control text_300" ID="Flag_T"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>APPID</td>
            <td><ZL:TextBox runat="server" CssClass="form-control text_300" ID="APPID_T" /></td>
        </tr>
        <tr>
            <td>Key(ID)</td>
            <td>
                <ZL:TextBox runat="server" ID="APPKey_T" CssClass="form-control text_300" AllowEmpty="false" /></td>
        </tr>
        <tr>
            <td>Secret</td>
            <td>
                <ZL:TextBox runat="server" ID="APPSecret_T" CssClass="form-control text_300" AllowEmpty="false" /></td>
        </tr>
        <tr>
            <td>回调页</td>
            <td>
                <ZL:TextBox runat="server" ID="CallBack_T" CssClass="form-control text_300" /></td>
        </tr>
        <tr>
            <td>用户名</td>
            <td>
                <asp:TextBox runat="server" ID="UserName_T" CssClass="form-control text_300" /></td>
        </tr>
        <tr>
            <td>密码</td>
            <td>
                <asp:TextBox runat="server" ID="UserPwd_T" CssClass="form-control text_300" TextMode="Password" /></td>
        </tr>
        <tr>
            <td>备注</td>
            <td>
                <asp:TextBox runat="server" ID="Remind_T" CssClass="form-control text_300" /></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button runat="server" ID="Save_Btn" CssClass="btn btn-primary" Text="保存信息" OnClick="Save_Btn_Click" />
                <a href="PlatInfoList.aspx" class="btn btn-default">返回列表</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
