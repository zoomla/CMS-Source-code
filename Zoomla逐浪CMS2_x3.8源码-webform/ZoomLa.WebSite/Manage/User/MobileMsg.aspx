<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileMsg.aspx.cs" Inherits="ZoomLa.Manage.User.MobileMsg" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>发送手机短信</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td colspan="2" class="spacingtitle"><b>发送手机短信</b></td>
        </tr>
        <tr>
            <td class="tdleft td_l" style="height: 28px;"><strong>短信余额：</strong></td>
            <td align="left">
                <asp:Label ID="LblMobile" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="tdleft" style="height: 28px; width: 15%;"><strong>接收人号码：</strong><br />
                (多条信息发送以半角逗号区隔，最多支持100个/次超100个请自行做循环)</td>
            <td>
                <table id="TblAddMessage" width="100%" visible="true" runat="server">
                    <tr>
                        <td>
                            <div  class="input-group text_300 top">
                                <asp:TextBox ID="InceptUser_T" CssClass="form-control text_em" runat="server" />
                                <span class="input-group-btn">
                                    <button type="button" onclick="user.sel('InceptUser', 'user', '')" class="btn btn-primary">选择用户</button>
                                </span>
                            </div>
                            <asp:HiddenField ID="InceptUser_Hid" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdleft" style="height: 23px; width: 15%;"><strong>短信内容：</strong><br />
                (字数70个字以内)</td>
            <td>
                <asp:TextBox ID="Content_T" runat="server" Rows="10" TextMode="MultiLine" class="form-control" Style="max-width: 99%;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ValrContent" runat="server" ControlToValidate="Content_T" ErrorMessage="*短消息内容不能为空" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="2" style="height: 50px;" align="center">
                <asp:Button ID="BtnSend" runat="server" Text="发送" OnClick="BtnSend_Click" class="btn btn-primary" />
                &nbsp;&nbsp;
        <asp:Button ID="BtnReset" runat="server" Text="清除" OnClick="BtnReset_Click" class="btn btn-primary" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
function SelectUser() {
    var url = "/Mis/OA/Mail/SelGroup.aspx?Type=AllInfo#ReferUser";
    comdiag.maxbtn = false;
    ShowComDiag(url, "选择用户");
}

user.hook["InceptUser"] = userdeal;
function userdeal(list, select) {
    var names = "", ids = "";
    for (var i = 0; i < list.length; i++) {
        names += list[i].UserName + ",";
        ids += list[i].UserID + ",";
    }
    $("#" + select + "_T").val(names);
    $("#" + select + "_Hid").val(ids);
    if (comdiag != null) { CloseComDiag(); }
}
</script>
</asp:Content>

