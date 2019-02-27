<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddDevice.aspx.cs" Inherits="Manage_Shop_Printer_AddDevice" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>设备管理</title>
    <link href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered">
        <tr>
            <td class="td_m"><strong>商户编码:</strong></td>
            <td>
                <asp:TextBox ID="MemberCode_T" runat="server" class="form-control text_300" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="MemberCode_T" ErrorMessage="商户编码不能为空" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td class="td_m"><strong>API密钥:</strong></td>
            <td>
                <asp:TextBox ID="SecurityKey_T" runat="server" class="form-control text_300" MaxLength="30" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="SecurityKey_T" ErrorMessage="API密钥不能为空" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td class="td_m"><strong>设备名称:</strong></td>
            <td>
                <asp:TextBox ID="Alias_T" runat="server" class="form-control text_300" MaxLength="30" />
                <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="Alias_T" ErrorMessage="别名不能为空" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td><strong>所属门店:</strong></td>
            <td>
                <asp:TextBox ID="ShopName_T" runat="server" class="form-control text_300" MaxLength="50" /></td>
        </tr>
        <tr>
            <td><strong>设备编码:</strong></td>
            <td>
                <asp:TextBox ID="DeviceNo_T" runat="server" class="form-control text_300" />
                <asp:RequiredFieldValidator runat="server" ID="R2" ControlToValidate="DeviceNo_T" ErrorMessage="设备编码不能为空" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td><strong>默认设备:</strong></td>
            <td>
                <input type="checkbox" runat="server" id="IsDefault_C" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td><strong>备注信息:</strong></td>
            <td>
                <asp:TextBox ID="Remind_T" TextMode="MultiLine" runat="server" class="form-control" Height="87px" Width="500px" /></td>
        </tr>
        <tbody runat="server" id="addon_tb" visible="false">
            <tr>
                <td><strong>激活日期</strong></td>
                <td>
                    <asp:Label ID="Since_L" runat="server" /></td>
            </tr>
            <%--    <tr>
                <td><strong>连接状态</strong></td>
                <td>
                    <asp:Label ID="DeviceStatus_L" runat="server" /></td>
            </tr>--%>
        </tbody>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="Save_Btn" class="btn btn-primary" runat="server" OnClick="Save_Btn_Click" Text="保存信息" />
                <a href="ListDevice.aspx" class="btn btn-primary">返回列表</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/dist/js/bootstrap-switch.js"></script>
</asp:Content>

