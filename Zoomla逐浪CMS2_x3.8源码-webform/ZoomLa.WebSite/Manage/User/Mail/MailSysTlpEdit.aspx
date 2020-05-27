<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailSysTlpEdit.aspx.cs" Inherits="Manage_User_Mail_MailSysTlpEdit" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master" ValidateRequest="false"  %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>修改系统模板</title>
    <script src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="tdleft td_l"><strong>模板名称：</strong></td>
            <td style="text-align: left">
                <asp:TextBox ID="TxtTempName" class="form-control m715-50" runat="server" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>模板内容：</strong></td>
            <td>
                <textarea id="TxtContent" class="m715-50" style="height:200px;" runat="server"></textarea>
            </td>
        </tr>
        <tr><td></td>
            <td>
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存" runat="server" OnClick="BtnSubmit_Click" />
                <a href="MailSysTlp.aspx" class="btn btn-primary">返回</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        $(function () {
            UE.getEditor("TxtContent");
        })
    </script>
</asp:Content>