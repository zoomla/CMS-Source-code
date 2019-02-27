<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="SetSecondPwd.aspx.cs" Inherits="User_SetSecondPwd" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>支付密码设置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="group" data-ban="SetSecondPwd"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li class="active">支付密码<a href="SetSecondPwd.aspx">[设置支付密码]</a></li>
        </ol>
    </div>
    <div class="container btn_green u_cnt">
        <div runat="server" id="DV_Set" visible="false">
            <div class="us_topinfo"><span class="r_red">初次使用请先设置支付密码：</span></div>
            <table class="table table-striped table-bordered">
                <tr>
                    <td class="td_m"><strong>密 码：</strong></td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" TextMode="Password" CssClass="form-control text_md" />
                        <font style="color: Red">*</font>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox1" ValidationExpression="[\S]{6,}" ErrorMessage="密码至少6位" Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" SetFocusOnError="false" ErrorMessage="密码不能为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><strong>确认密码：</strong></td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" CssClass="form-control text_md" />
                        <span class="r_red">*</span>
                        <asp:CompareValidator ID="CompareValidator2" ControlToValidate="TextBox2" ControlToCompare="TextBox1" ErrorMessage="两次输入的密码不一致！" runat="server" /></td>
                </tr>
                <tr><td></td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="保存"
                            class="btn btn-primary" OnClick="Button1_Click" />
                        &nbsp;&nbsp;
	<asp:Button ID="Button2" runat="server" Text="取消" ValidationGroup="BtnCancel"
        class="btn btn-primary" OnClick="Button2_Click" /></td>
                </tr>
            </table>
        </div>
        <div runat="server" id="DV_show">
            <table class="table table-striped table-bordered">
                <tr>
                    <td class="td_m"><strong>原 密 码：</strong></td>
                    <td>
                        <asp:TextBox ID="TxtOldPassword" runat="server" CssClass="form-control text_md" TextMode="Password" />
                    </td>
                </tr>
                <tr>
                    <td><strong>新 密 码：</strong></td>
                    <td>
                        <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" CssClass="form-control text_md" />
                        <font style="color: Red">*</font>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server"
                            ControlToValidate="TxtPassword" ValidationExpression="[\S]{6,}" ErrorMessage="密码至少6位"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="ReqTxtPassword" runat="server" ControlToValidate="TxtPassword"
                            SetFocusOnError="false" ErrorMessage="密码不能为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><strong>确认密码：</strong></td>
                    <td>
                        <asp:TextBox ID="TxtPassword2" runat="server" TextMode="Password" CssClass="form-control text_md" />
                        <font style="color: Red">*</font>
                        <asp:CompareValidator ID="CompareValidator1" ControlToValidate="TxtPassword2" ControlToCompare="TxtPassword" ErrorMessage="两次输入的密码不一致！" runat="server" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="BtnSubmit" runat="server" Text="修改" OnClick="BtnSubmit_Click" class="btn btn-primary" />
                        <asp:Button ID="BtnCancle" runat="server" Text="取消" OnClick="BtnCancle_Click" ValidationGroup="BtnCancel" class="btn btn-primary" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
