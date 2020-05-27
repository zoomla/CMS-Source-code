<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.Setting" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>配置中心</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-striped">
        <tr>
            <td class="td_m">难度更新</td>
            <td>
                <asp:TextBox runat="server" ID="STime_T" CssClass="form-control text_md" />
                <asp:TextBox runat="server" ID="ETime_T" CssClass="form-control text_md" />
                <asp:Button runat="server" ID="UpdateDiff_Btn" CssClass="btn btn-primary" OnClick="UpdateDiff_Btn_Click" Text="难度更新" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>