<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMailConfig.aspx.cs" Inherits="Plat_EMail_AddMailConfig" MasterPageFile="~/Plat/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head"><title>邮箱配置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered">
    <tr><td>名称</td><td><ZL:TextBox runat="server" ID="Alias_T" CssClass="form-control text_300" MaxLength="20" AllowEmpty="false" ValidType="string" /></td></tr>
    <tr><td style="width:120px;">邮箱POP</td><td>
        <div class="input-group"  style="width:580px;">
        <ZL:TextBox runat="server" ID="POP_T" class="form-control text_300" AllowEmpty="false" />
        <div id="poplist" class="input-group-btn">
            <button type="button" class="btn btn-default" data-pop="pop.exmail.qq.com">QQ企业</button>
            <button type="button" class="btn btn-default" data-pop="pop3.163.com">163</button>
            <button type="button" class="btn btn-default" data-pop="pop.sina.cn">新浪</button>
            <button type="button" class="btn btn-default" data-pop="pop.139.com">139</button>
            <button type="button" class="btn btn-default" data-pop="pop.tom.com">Tom</button>
        </div></div></td></tr>
        <tr><td style="width:120px;">邮箱SMTP</td><td>
            <div class="input-group"  style="width:580px;">
                <ZL:TextBox runat="server" ID="SMTP_T" class="form-control text_300" AllowEmpty="false" />
                <span id="smtplist" class="input-group-btn">
                    <input type="button" class="btn btn-default" data-smtp="smtp.exmail.qq.com" value="QQ企业">
                    <input type="button" class="btn btn-default" data-smtp="smtp.163.com" value="163">
                    <input type="button" class="btn btn-default" data-smtp="smtp.sina.cn" value="新浪">
                    <input type="button" class="btn btn-default" data-smtp="smtp.139.com" value="139">
                    <input type="button" class="btn btn-default" data-smtp="smtp.tom.com" value="Tom">
                </span>
            </div></td></tr>
    <tr><td>邮箱帐户</td><td><ZL:TextBox runat="server" ID="Acount_T" CssClass="form-control text_300" AllowEmpty="false" ValidType="Mail" /></td></tr>
    <tr><td>邮箱密码</td><td><ZL:TextBox runat="server" ID="Passwd_T" TextMode="Password" AllowEmpty="false" CssClass="form-control text_300"/></td></tr>
    <tr><td>天数</td><td><asp:TextBox runat="server" ID="Days_T" CssClass="form-control text_300" Text="30"/><span>(接收多少天内的邮件)</span></td></tr>
    <tr><td></td><td>
        <asp:Button runat="server" ID="Save_Btn" Text="保存配置" OnClick="Save_Btn_Click" CssClass="btn btn-info" />
        <a href="javascript:;" onclick="parent.CloseComDiag();" class="btn btn-default">关闭窗口</a></td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/ZL_Regex.js"></script>
<script type="text/javascript">
    $(function () {
        $("#poplist button").click(function () {
            $("#POP_T").val($(this).data("pop"));
        });
        $("#smtplist .btn").click(function () {
            $("#SMTP_T").val($(this).data("smtp"));
        });
    })
</script>
</asp:Content>