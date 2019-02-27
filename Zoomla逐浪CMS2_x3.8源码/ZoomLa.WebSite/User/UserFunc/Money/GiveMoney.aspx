<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GiveMoney.aspx.cs" Inherits="User_UserFunc_Money_GiveMoney" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>赠送金额</title><style>.text-right{line-height:30px!important;}</style></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered">
        <tr>
            <td class="td_m text-right">当前<%=TypeName %>:</td>
            <td>
                <asp:Label ID="UserScore_L" style="color:green;line-height:30px;" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td_m text-right">受赠会员名:</td>
            <td>
                <asp:TextBox ID="UserName_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserReq" runat="server" Display="Dynamic" ControlToValidate="UserName_T" ForeColor="Red" ErrorMessage="用户名不能为空!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="text-right">赠送<%=TypeName %>:</td>
            <td>
                <asp:TextBox ID="Score_T" runat="server" CssClass="form-control text_300 num"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="Score_T" ForeColor="Red" ErrorMessage="赠送金额不能为空!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="text-right">备注:</td>
            <td>
                <asp:TextBox ID="Remark_T" runat="server" CssClass="form-control text_300" TextMode="MultiLine" Height="80"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="SendScore_Btn" runat="server" CssClass="btn btn-primary" OnClick="SendScore_Btn_Click" Text="赠送" />
                <button type="button" class="btn btn-primary" onclick="parent.CloseComDiag()">关闭</button>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript" src="/JS/ZL_Regex.js"></script>
    <script>
        $(function () {
            ZL_Regex.B_Num(".num");
        });
    </script>
</asp:Content>