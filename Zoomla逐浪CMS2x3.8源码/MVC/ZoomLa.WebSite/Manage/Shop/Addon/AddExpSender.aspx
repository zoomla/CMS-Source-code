<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddExpSender.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.AddExpSender" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>编辑发件信息</title>
    <link href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered">
        <tr>
            <td class="td_m"><strong>收件人:</strong></td>
            <td>
                <ZL:TextBox ID="Name_T" runat="server" CssClass="form-control text_300" AllowEmpty="false" />
            </td>
        </tr>
        <tr>
            <td class="td_m"><strong>公司名称:</strong></td>
            <td>
                <ZL:TextBox ID="CompName_T" runat="server" CssClass="form-control text_300" />
            </td>
        </tr>
        <tr>
            <td class="td_m"><strong>手机号码:</strong></td>
            <td>
                <ZL:TextBox ID="Mobile_T" runat="server" CssClass="form-control text_300" ValidType="MobileNumber" AllowEmpty="false" />
            </td>
        </tr>
        <tr>
            <td><strong>是否默认:</strong></td>
            <td>
                <input type="checkbox" runat="server" id="IsDefault_C" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td class="td_m"><strong>详细地址:</strong></td>
            <td>
                <ZL:TextBox ID="Address_T" runat="server" CssClass="form-control" TextMode="MultiLine" Height="87px" Width="500px" AllowEmpty="false" />
            </td>
        </tr>
        <tr>
            <td><strong>备注信息:</strong></td>
            <td>
                <asp:TextBox ID="Remind_T" TextMode="MultiLine" runat="server" class="form-control" Height="87px" Width="500px" /></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="Save_Btn" class="btn btn-primary" runat="server" OnClick="Save_Btn_Click" Text="保存信息" />
                <a href="ExpSenderManage.aspx" class="btn btn-primary">返回列表</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/dist/js/bootstrap-switch.js"></script>
</asp:Content>