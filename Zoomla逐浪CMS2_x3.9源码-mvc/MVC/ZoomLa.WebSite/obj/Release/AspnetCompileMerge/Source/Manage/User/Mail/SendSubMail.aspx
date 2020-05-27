<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendSubMail.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Mail.SendSubMail" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>发送订阅邮件</title>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="td_m text-right">收件人选择:</td>
            <td>
                <asp:RadioButtonList ID="SubUsers_Radio" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Selected="True" Value="alluser" Text="所有订阅用户"></asp:ListItem>
                    <%--<asp:ListItem Value="setuser" Text="自定义用户"></asp:ListItem>--%>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr style="display:none;" id="SetUser">
            <td class="text-right">订阅用户:</td>
            <td>
                <asp:TextBox TextMode="MultiLine" Enabled="false" CssClass="form-control text_300" ID="SubUsers_T" Height="150" runat="server"></asp:TextBox>
                <asp:HiddenField ID="SubIDS_Hid" runat="server" />
                <button type="button" class="btn btn-info">选择订阅用户</button>
            </td>
        </tr>
        <tr>
            <td class="text-right">邮件主题:</td>
            <td>
                <asp:TextBox ID="Subject_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
                <button type="button" class="btn btn-info" onclick="ShowContents()">选择订阅内容</button>
            </td>
        </tr>
        <tr>
            <td class="text-right">邮件内容:</td>
            <td>
                <asp:TextBox ID="Content_T" runat="server" style="margin-bottom:5px;" CssClass="m715-50" Height="300" TextMode="MultiLine"></asp:TextBox>
                <%=Call.GetUEditor("Content_T",3) %>
            </td>
        </tr>
        <tr>
            <td class="text-right">邮件签名:</td>
            <td>
                <asp:DropDownList ID="Graph_Drop" runat="server" CssClass="form-control text_300" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                <asp:Button ID="Send_Btn" runat="server" OnClick="Send_Btn_Click" CssClass="btn btn-primary" Text="发送邮件" />
                <a href="SubscriptListManage.aspx" class="btn btn-primary">返回列表</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        $(function () {
            $("#SubUsers_Radio input").click(function () {
                if ($(this)[0].checked&& $(this).val() == "setuser") {
                    $("#SetUser").show();
                } else {
                    $("#SetUser").hide();
                }
            });
        });
        function ShowContents() {
            comdiag.maxbtn = false;
            ShowComDiag("/Common/ContentList.aspx", "选择内容");
        }
        function DealResult(cid) {
            CloseComDiag();
            UE.getEditor("Content_T").setContent("读取中...",false);
            $.post("SendSubMail.aspx", { action: "getcon", cid: cid }, function (data) {
                if (!data) { alert('未知错误!'); return; }
                var conarr = JSON.parse(data);
                console.log(conarr);
                $("#Subject_T").val(conarr[0].Title);
                UE.getEditor("Content_T").setContent(conarr[0].content, false);
            });
        }
    </script>
</asp:Content>


