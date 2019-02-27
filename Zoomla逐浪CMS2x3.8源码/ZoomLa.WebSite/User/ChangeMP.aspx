<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeMP.aspx.cs" Inherits="User_ChangeMP" EnableViewStateMac="false" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>修改手机</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="ChangeMP"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a title="用户信息" href="/User/Info/UserInfo.aspx">用户信息</a></li>
        <li class="active">修改手机</li>
    </ol>
</div>
<div class="container btn_green">
     <div class="alert alert-info" runat="server" id="Remind_Div" visible="false" style="margin-top:0px;margin-bottom:5px;"></div>
        <asp:Panel ID="step1_div" runat="server" Visible="false">
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td class="td_m">原手机：</td>
                    <td>
                        <asp:TextBox ID="SMobile_T" runat="server" CssClass="form-control text_300" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>验证码：</td>
                    <td>
                        <asp:TextBox ID="VCode" placeholder="验证码" MaxLength="6" runat="server" CssClass="form-control text_x" />
                        <img id="VCode_img" class="codeimg" title="点击刷新验证码" />
                        <input type="hidden" id="VCode_hid" name="VCode_hid" />
                        <asp:Button runat="server" ID="SendCode_Btn" CssClass="btn btn-info" Text="发送校验码" OnClick="SendEMail_Btn_Click" />
                    </td>
                </tr>
                <tr>
                    <td>校验码：</td>
                    <td>
                        <asp:TextBox runat="server" ID="CheckNum_T" CssClass="form-control text_300"></asp:TextBox></td>
                </tr>
                <tr><td></td>
                    <td>
                        <asp:Button runat="server" ID="Next_Btn" Text="下一步" OnClick="Next_Btn_Click" CssClass="btn btn-info" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="step2_div" runat="server" Visible="false">
            <table class="table table-bordered table-striped">
                <tr>
                    <td class="td_m">新手机号：</td>
                    <td>
                        <ZL:TextBox ID="NewMobile_T" runat="server" CssClass="form-control text_300" AllowEmpty="false" ValidType="MobileNumber" />
                    </td>
                </tr>
                <tr><td>验证码：</td><td>
                    <asp:TextBox ID="NewVCode" placeholder="验证码" MaxLength="6" runat="server" CssClass="form-control text_x" />
                    <img id="NewVCode_img" class="codeimg" title="点击刷新验证码" />
                    <input type="hidden" id="NewVCode_hid" name="NewVCode_hid" />
                    <asp:Button runat="server" ID="SendNewEmail_Btn" Text="发送校验码" CssClass="btn btn-info" OnClick="SendNewEmail_Btn_Click" />
                   <%-- <asp:RequiredFieldValidator runat="server" ID="RC1" Display="Dynamic" ErrorMessage="验证码不能为空" ForeColor="Red" ControlToValidate="NewVCode" />--%>
                </td>
                </tr>
                <tr><td>校验码：</td><td>
                    <asp:TextBox runat="server" ID="NewCheckNum_T" CssClass="form-control text_300"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="BtnSubmit" runat="server" Text="提交" class="btn btn-primary" OnClick="BtnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
 </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/ZL_ValidateCode.js"></script>
   <script>
       $(function () {
           $("#VCode").ValidateCode({ submitchk: false });
           $("#NewVCode").ValidateCode({ submitchk: false });
       })
   </script>
</asp:Content>
