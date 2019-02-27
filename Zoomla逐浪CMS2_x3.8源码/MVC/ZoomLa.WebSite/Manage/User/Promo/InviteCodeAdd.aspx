<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InviteCodeAdd.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Promo.InviteCodeAdd" MasterPageFile="~/Manage/I/Default.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>邀请码列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered">
    <tr><td class="td_m">邀请码格式</td><td><ZL:TextBox runat="server" ID="Format_T" Text="{00000000AAA}" class="form-control text_300" AllowEmpty="false"/></td></tr>
    <tr><td>所属用户</td><td>
        <ZL:TextBox runat="server" ID="User_T" class="form-control text_300" AllowEmpty="false" />
        <asp:HiddenField runat="server" ID="User_Hid" />
        <input type="button" value="选择用户" onclick="user.sel('User', 'user', '');" class="btn btn-info"/>
    </td></tr>
    <tr><td>会员组</td><td>
        <asp:RadioButtonList runat="server" ID="Group_Rad" DataTextField="GroupName" DataValueField="GroupID" RepeatDirection="Horizontal"></asp:RadioButtonList>
                    </td></tr>
    <tr><td>生成数量</td><td><ZL:TextBox runat="server" ID="Count_T" class="form-control text_300" Text="10" AllowEmpty="false"  ValidType="IntPostive"/></td></tr>
    <tr><td></td><td><asp:Button runat="server" ID="Create_Btn" OnClick="Create_Btn_Click"  Text="生成邀请码" class="btn btn-primary"/></td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    user.hook["User"] = function (list,select) {
        var uinfo = list[0];
        $("#" + select + "_T").val(uinfo.UserName);
        $("#" + select + "_Hid").val(uinfo.UserID);
        CloseComDiag();
    }
</script>
</asp:Content>