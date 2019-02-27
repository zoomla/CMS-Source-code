<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="ChangPSW.aspx.cs" Inherits="ZoomLa.WebSite.User.ChangPSW" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>会员修改密码</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="home" data-ban="UserInfo"></div>
<ol class="breadcrumb container">
    <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
    <li class="active"><a href="<%:Request.RawUrl %>">修改密码</a>
        <a href="javascript:;" id="Enable_Btn" visible="false" runat="server" onclick="SetKey(0)">[点此启用动态口令] </a>
        <span id="Enabled_Span" runat="server" class="r_gray">已启用动态口令 (<a href="javascript:;" onclick="ChangeKey(0)">点此修改绑定</a>
            <a href="javascript:;" onclick="ChangeKey(1)">解除绑定</a>)</span>
    </li>
    <div class="clearfix"></div>
</ol>
<div class="container btn_green margin_t5">
    <div runat="server" id="changePwd_div">
        <table class="table table-bordered table-hover table-striped">
            <tr>
                <td class="text-right"><strong>原 密 码：</strong></td>
                <td>
                    <asp:TextBox ID="TxtOldPassword" runat="server" CssClass="form-control text_300" TextMode="Password" autofocus="true" data-enter="0" />
                    <span style="color: Red">*</span>
                    <asp:RequiredFieldValidator ID="RV1" ValidationGroup="UpPass" runat="server" ErrorMessage="原密码不能为空" ControlToValidate="TxtOldPassword" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td class="text-right"><strong>新 密 码：</strong></td>
                <td>
                    <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" CssClass="form-control text_300" data-enter="1" /><span style="color: Red">*</span>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="UpPass" ID="R2" Display="Dynamic" ErrorMessage="新密码不能为空" ControlToValidate="TxtPassword" ForeColor="Red" />
                    <asp:RegularExpressionValidator ID="R3" ValidationGroup="UpPass" Display="Dynamic" runat="server" ErrorMessage="密码不能少于6位" ControlToValidate="TxtPassword" ValidationExpression="[\s\S]{6,}" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td class="text-right"><strong>确认密码：</strong></td>
                <td>
                    <asp:TextBox ID="TxtPassword2" runat="server" TextMode="Password" CssClass="form-control text_300" data-enter="2" />
                    <span style="color: Red">*</span>
                    <asp:RequiredFieldValidator ValidationGroup="UpPass" runat="server" ID="R4" Display="Dynamic" ErrorMessage="确认密码不能为空" ControlToValidate="TxtPassword" ForeColor="Red" />
                    <asp:CompareValidator ValidationGroup="UpPass" ID="CompareValidator1" ControlToValidate="TxtPassword2" ControlToCompare="TxtPassword" ErrorMessage="新密码和确认密码必须一致!" runat="server" ForeColor="Red" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="UpPass" Text="修改" OnClick="BtnSubmit_Click" class="btn btn-primary" data-enter="3" />
                    <a href="Info/UserInfo.aspx" class="btn btn-primary">取消</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="setkey_diag" style="display: none;">
        <div class="alert alert-danger" role="alert">
            <span class="fa fa-exclamation-circle"></span>请确保手机客户端没有名为[<span class="keys"></span>]的密钥（否则请删除后执行本操作），以确保修改成功！
        </div>
        <div class="text-center">
            <img id="keyimg" src="#" />
        </div>
        <div class="text-center">
            <div class="alert alert-info margin_t5" role="alert">↑扫码会造用户名为[<span class="keys"></span>]的密钥,您也可以手工填写密钥：<span class="keys"></span></div>
            <h3><strong>验证器获取方式:</strong></h3>
            <ul>
                <li>IOS:在应用商店搜索"google Authenticator"</li>
                <li>Android:在应用商城或下载软件搜索"google 身份验证器"</li>
                <li>Windows Phone:在应用商城搜索"验证器"</li>
            </ul>
        </div>
        <div class="text-center">
            <asp:Button ID="SetUserKey_B" OnClick="SetUserKey_B_Click" runat="server" Enabled="false" CssClass="btn btn-primary" Text="绑定" />
            <button type="button" onclick="closeKeyDiag()" class="btn btn-primary">取消</button>
        </div>
    </div>
    <div id="changekey_diag" style="display: none;">
        <table class="table table-bordered table-hover table-striped">
            <tr>
                <td class="text-right">用户名:</td>
                <td>
                    <asp:Label ID="UserName_L" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="text-right">密码:</td>
                <td>
                    <asp:TextBox ID="PassWord_T" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right">动态口令:</td>
                <td>
                    <asp:TextBox ID="Code_T" runat="server" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" class="text-center">
                    <asp:HiddenField ID="ChangeType_Hid" runat="server" />
                    <asp:Button ID="ChangePWD_B" OnClientClick="return confirm('确定执行此操作?')" runat="server" Text="解除绑定" CssClass="btn btn-primary" OnClick="ChangePWD_B_Click" />
                    <button type="button" onclick="CloseChDiag()" class="btn btn-primary">取消操作</button>
                </td>
            </tr>
        </table>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/Control.js"></script>
<script>
    $(function () {
        Control.EnableEnter();
    })
    var codeDiag = new ZL_Dialog();
    function SetKey(type) {
        $.ajax({
            type: 'POST',
            data: { action: 'getkey' },
            success: function (data) {
                if (data) {
                    var obj = JSON.parse(data);
                    codeDiag.title = type == 0 ? '绑定验证器' : '修改验证器';
                    codeDiag.content = 'setkey_diag';
                    codeDiag.ShowModal();
                    $("#keyimg").load(function () {
                        $("#SetUserKey_B").removeAttr("disabled");
                    });
                    $("#keyimg").attr("src", '/Common/Common.ashx?url=otpauth://totp/' + obj.sitename + '?secret=' + obj.key);
                    $(".keys").text(obj.key);
                }
            }
        });
    }
    function closeKeyDiag() {
        codeDiag.CloseModal();
    }
    var keyDiag = new ZL_Dialog();
    function ChangeKey(type) {
        keyDiag.title = "用户验证";
        keyDiag.content = "changekey_diag";
        keyDiag.ShowModal();
        $("#Code_T").val('');
        $("#ChangePWD_B").val('解除绑定');
        $("#ChangeType_Hid").val(type);
        if (type == 0) $("#ChangePWD_B").val("修改绑定");
    }
    function CloseChDiag() {
        keyDiag.CloseModal();
    }
</script>
</asp:Content>
