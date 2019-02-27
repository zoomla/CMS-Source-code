<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ZoomLaCMS.Manage.Copyright.Register" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>版权配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div runat="server" id="Prompt_Div" visible="false">
        <div class="alert alert-info in margin_b2px">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <h4><%=Resources.L.提示 %>!</h4>
            <p style="padding-left: 50px; line-height: 40px;">
                <strong><i class="fa fa-bullhorn" style='font-size: 18px;'></i>
                    让每一个字节的版权都受到保护，版权保护由Zoomla!逐浪CMS官方与北京版银科技有限责任公司旗下“版权印”平台联合提供，版权印平台负责相应细节和分成。<br />
                    系统目前未配置版权印AppID，若要使用版权功能，请<a href="/Admin/APP/Suppliers.aspx" style='margin-left: 5px;' class='btn btn-default'>前往配置</a>
                </strong>
            </p>
        </div>
    </div>
    <div runat="server" id="Register_Div">
        <table class="table table-striped table-bordered table-hover">
            <tbody>
                <tr class="tdbg">
                    <td width="100%" height="24" align="center" class="title" colspan="2">版权印注册  <a href="?type=1">已有账号>></a></td>
                </tr>
                <tr>
                    <td class="tdleft" width="20%"><strong>邮箱:</strong></td>
                    <td>
                        <asp:TextBox ID="Email_T" runat="server" class="form-control text_300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RfvEmail" runat="server" ControlToValidate="Email_T" ForeColor="Red" Display="Dynamic" ErrorMessage="Email不能为空" />
                        <asp:RegularExpressionValidator ID="RevEmail" runat="server" ControlToValidate="Email_T" Display="Dynamic" ForeColor="Red" ErrorMessage="邮箱地址不规范" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" />
                    </td>
                </tr>
                <tr>
                    <td class="tdleft"><strong>密码:</strong></td>
                    <td>
                        <asp:TextBox ID="Password_T" runat="server" class="form-control text_300"></asp:TextBox>
                        <asp:RegularExpressionValidator runat="server" ID="RevPassword" ControlToValidate="Password_T" ValidationExpression="[\S]{6,}" ErrorMessage="密码至少6位" Display="Dynamic" ForeColor="Red" />
                        <asp:RequiredFieldValidator runat="server" ID="RfvTxtPassword" ControlToValidate="Password_T" SetFocusOnError="false" ErrorMessage="密码不能为空" Display="Dynamic" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td class="tdleft"><strong>确认密码:</strong></td>
                    <td>
                        <asp:TextBox ID="ConfirmPwd_T" runat="server" class="form-control text_300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RefConfirmPwd" runat="server" ControlToValidate="ConfirmPwd_T" ForeColor="Red" Display="Dynamic" ErrorMessage="确认密码不能为空" />
                        <asp:CompareValidator ID="req2" runat="server" ControlToValidate="ConfirmPwd_T" ControlToCompare="Password_T"
                            Operator="Equal" SetFocusOnError="false" ErrorMessage="两次密码输入不一致" Display="Dynamic" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td width="100%" align="center" colspan="2" style="height: 24px">&nbsp;
                    <asp:Button ID="Register_B" runat="server" Text="注　册" OnClick="Register_B_Click" class="btn btn-primary" /></td>
                </tr>
            </tbody>
        </table>
    </div>
    <div runat="server" id="Config_Div" visible="false">
        <table class="table table-striped table-bordered table-hover">
            <tbody>
                <tr class="tdbg">
                    <td width="100%" height="24" align="center" class="title" colspan="2">填写授权信息</td>
                </tr>
                <tr>
                    <td class="tdleft" width="20%">真实姓名:</td>
                    <td><asp:TextBox ID="RealName_T" runat="server" class="form-control text_300"></asp:TextBox><span style="color:red;">*</span></td>
                </tr>
                <tr>
                    <td class="tdleft">笔名:</td>
                    <td><asp:TextBox ID="Nick_T" runat="server" class="form-control text_300"></asp:TextBox><span style="color:red;">*</span></td>
                </tr>
                <tr>
                    <td class="tdleft">身份证号:</td>
                    <td><asp:TextBox ID="CardNo_T" runat="server" class="form-control text_300"></asp:TextBox><span style="color:red;">*</span></td>
                </tr>
                <tr>
                    <td class="tdleft">联系人:</td>
                    <td><asp:TextBox ID="Contact_T" runat="server" class="form-control text_300"></asp:TextBox><span style="color:red;">*</span></td>
                </tr>
                <tr>
                    <td class="tdleft">联系邮箱:</td>
                    <td><asp:TextBox ID="Email_T2" runat="server" class="form-control text_300"></asp:TextBox><span style="color:red;">*</span></td>
                </tr>
                <tr>
                    <td class="tdleft">手机号码:</td>
                    <td><asp:TextBox ID="Mobile_T" runat="server" class="form-control text_300"></asp:TextBox><span style="color:red;">*</span></td>
                </tr>
                <tr>
                    <td class="tdleft">联系电话:</td>
                    <td><asp:TextBox ID="Tel_T" runat="server" class="form-control text_300"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <input id="Next_B" class="btn btn-primary" value="下一步" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
