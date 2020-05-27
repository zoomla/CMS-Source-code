<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetAdminKey.aspx.cs" Inherits="ZoomLaCMS.Manage.User.SetAdminKey"  MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/SPwd.ascx" TagPrefix="uc1" TagName="SPwd" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
        .lock_code{font-size:2em;color:#2a6496;cursor:pointer;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<uc1:SPwd runat="server" ID="SPwd" Visible="false" />
<div id="maindiv" runat="server" visible="false">
    <div class="text-center">
        <asp:Image ID="Key_Img" runat="server" />
    </div>
    <div class="text-center margin_t10">
        <asp:Button ID="EnableKey_B" CssClass="btn btn-primary" Text="点击启用" disabled="disabled" runat="server" OnClientClick="return confirm('确定启用验证器吗?')" OnClick="EnableKey_B_Click" />
    </div>
    <div class="alert alert-success margin_t10 text-center" role="alert">
  <span class="fa fa-exclamation-circle" aria-hidden="true"></span>
        <strong>说明：</strong>
        <ul>
            <li>IOS:在应用商店搜索"google Authenticator"</li>
            <li>Android:在应用商城或下载软件搜索"google 身份验证器"</li>
            <li>Windows Phone:在应用商城搜索"验证器"</li>
        </ul>
</div>
</div>
<div id="EditDiv" runat="server" visible="false">
    <table class="table table-bordered table-hover table-striped">
        <tr>
            <td class="td_m text-right">口令二维码:</td>
            <td>
                <span class="fa fa-lock lock_code" title="获取密钥" onclick="ShowCheck(0)"></span>
                <asp:Image ID="Code_Img" Visible="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="text-right">管理密钥:</td>
            <td>
                <span class="fa fa-lock lock_code" title="获取密钥" onclick="ShowCheck(0)"></span>
                <asp:Label ID="Keys_L" Visible="false" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text-right">操作:</td>
            <td>
                <button class="btn btn-primary" onclick="ShowCheck(1)" type="button">停用口令</button>
            </td>
        </tr>
    </table>
    <div id="checkcode" style="display:none;">
        <div class="text-center">
            动态口令:
            <asp:TextBox ID="Code_T" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="margin_t10 text-center">
            <asp:HiddenField ID="Type_Hid" runat="server" />
            <asp:Button ID="Check_B" Text="获取" runat="server" CssClass="btn btn-primary" OnClick="Check_B_Click" />
            <button type="button" onclick="CloseCheck()" class="btn btn-primary">取消</button>
        </div>
    </div>
</div>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        $("#Key_Img").load(function () {
            $("#EnableKey_B").removeAttr("disabled");
        });
        $().ready(function () {
            if ($("#Keys_L")[0]) {
                $(".lock_code").hide();
            }
        });
        var checkDiag = new ZL_Dialog();
        function ShowCheck(type) {
            checkDiag.title = "口令验证";
            checkDiag.content = "checkcode";
            checkDiag.ShowModal();
            $("#Check_B").val('获取');
            $("#Code_T").val('');
            $("#Type_Hid").val(type);
            if (type == 1) $("#Check_B").val('停用');
            
        }
        function CloseCheck() {
            checkDiag.CloseModal();
        }
        
    </script>
</asp:Content>