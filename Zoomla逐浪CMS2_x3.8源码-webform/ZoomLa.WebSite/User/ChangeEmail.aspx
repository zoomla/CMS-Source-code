<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeEmail.aspx.cs" Inherits="User_ChangeEmail" EnableViewStateMac="false" MasterPageFile="~/User/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>修改邮箱</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="group" data-ban="ChangeEmail"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li><a title="用户信息" href="/User/Info/UserInfo.aspx">用户信息</a></li>
            <li class="active">修改邮箱</li>
        </ol>
    </div>
    <div class="container btn_green">
        <div class="alert alert-info" runat="server" id="Remind_Div" visible="false" style="margin-top:0px;margin-bottom:5px;"></div>
        <asp:Panel ID="step1_div" runat="server" Visible="false">
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td class="td_m">原邮箱：</td>
                    <td>
                        <div class="input-group text_400">
                            <asp:Label ID="SEmail_T" runat="server" CssClass="form-control text_400"></asp:Label>
                            <span class="input-group-btn">
                                  <asp:Button runat="server" ID="SendEMail_Btn" Text="发送验证邮件" OnClick="SendEMail_Btn_Click" CssClass="btn btn-info" />
                            </span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>验证码：</td>
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
                    <td class="td_m">新邮箱：</td>
                    <td>
                        <div class="input-group text_400">
                            <asp:TextBox ID="NewEmail_T" runat="server" CssClass="form-control text_400" />
                            <span class="input-group-btn">
                                <asp:Button runat="server" ID="SendNewEmail_Btn" Text="发送邮件" CssClass="btn btn-info" OnClick="SendNewEmail_Btn_Click" />
                            </span>
                        </div>
                        <asp:RequiredFieldValidator ID="RV2" runat="server"  ControlToValidate="NewEmail_T" ForeColor="Red" Display="Dynamic" ErrorMessage="Email不能为空" />
                        <asp:RegularExpressionValidator ID="RV1" runat="server" ControlToValidate="NewEmail_T" ErrorMessage="邮件地址不规范" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" />
                    </td>
                </tr>
                <tr><td>验证码：</td><td>
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
