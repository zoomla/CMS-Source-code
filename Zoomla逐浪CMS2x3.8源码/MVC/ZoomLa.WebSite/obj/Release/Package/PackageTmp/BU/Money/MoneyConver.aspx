<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoneyConver.aspx.cs" Inherits="ZoomLaCMS.BU.Money.MoneyConver" MasterPageFile="~/Common/Common.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script src="/JS/ZL_Regex.js"></script>
    <title>虚拟币转换</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-hover table-bordered">
    <tr>
        <td class="td_m text-right">虚拟币种:</td>
        <td>
            <asp:DropDownList ID="VirtualType_Drop" AutoPostBack="true" OnSelectedIndexChanged="VirtualType_Drop_SelectedIndexChanged" runat="server" CssClass="form-control text_300"></asp:DropDownList>
            <asp:Label ID="VirtualScore_L" style="color:green;" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="text-right">兑换币种:</td>
        <td>
            <asp:DropDownList ID="TargetVirtual_Drop" AutoPostBack="true" OnSelectedIndexChanged="TargetVirtual_Drop_SelectedIndexChanged" runat="server" CssClass="form-control text_300">
            </asp:DropDownList>
            <asp:Label ID="TargetScore_L" style="color:green;" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="text-right">兑换公式:</td>
        <td>
            <asp:Label ID="Formula_L" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="text-right">兑换值:</td>
        <td>
            <asp:TextBox ID="Score_T" runat="server" CssClass="form-control text_300 num"></asp:TextBox>
            <span id="score_tips"></span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="Score_T" ForeColor="Red" ErrorMessage="兑换值不能为空!"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="Convert_Btn" Enabled="false" runat="server" CssClass="btn btn-primary" OnClick="Convert_Btn_Click" Text="兑换" />
            <button type="button" onclick="parent.CloseComDiag()" class="btn btn-primary">关闭</button>
        </td>
    </tr>
</table>
<asp:HiddenField ID="UserScore_Hid" runat="server" />
<asp:HiddenField ID="ConvertRate_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript">
        $(function () {
            ZL_Regex.B_Num(".num");
            $("#Score_T").keyup(function () {
                $("#Convert_Btn").attr("disabled", "disabled");
                var value = parseFloat($(this).val());
                var rate = parseFloat($("#ConvertRate_Hid").val());
                var userscore = parseFloat($("#UserScore_Hid").val());//当前用户的币值
                if (isNaN(value) || isNaN(parseFloat(rate))) {
                    $("#score_tips").text('');
                    return;
                }
                if (userscore < value) {
                    $("#score_tips").text('兑换值超过了您所拥有的币值!'); $("#Convert_Btn").attr("disabled", "disabled");
                    return;
                }
                var value = parseFloat($(this).val()) * parseFloat($("#ConvertRate_Hid").val());
                $("#score_tips").text('兑换后的值为:' + parseInt(value));
                if (parseInt(value) > 0) { $("#Convert_Btn").removeAttr("disabled"); };
            });
        })
    </script>
    <style type="text/css">
        .text-right{line-height:30px!important;}
    </style>
</asp:Content>