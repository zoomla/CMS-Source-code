<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlipayBank.aspx.cs" Inherits="ZoomLaCMS.Manage.Pay.AlipayBank"MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>支付宝单网银</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="2" cellspacing="1" class="table table-striped table-bordered table-hover">
                <tr align="center">
                    <td class="spacingtitle" colspan="2">
                        <asp:Label ID="LblTitle" runat="server" Text="支付宝单网银" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>支付名称：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="DDLPayName" runat="server" class="form-control text_md pull-left"></asp:TextBox>
                        <span style="color: red" class="tips">* 必填</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>商户ID：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtAccountID" runat="server" class="form-control text_md pull-left" />
                        <span style="color: red" class="tips">* 必填</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="商户ID不能为空！" CssClass="tips" ControlToValidate="TxtAccountID" Display="Dynamic" SetFocusOnError="True" />
                    </td>
                </tr>
                <tr id="trMD5Key" runat="server" >
                    <td>
                        <strong>安全校验码：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtMD5Key" runat="server" class="form-control text_md pull-left" />
                        <span style="color: red" class="tips">* 必填</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="安全校验码不能为空!" CssClass="tips" ControlToValidate="TxtMd5Key" Display="Dynamic" SetFocusOnError="true">  </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr >
                    <td>
                        <strong>卖家Email：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtSellerEmail" runat="server" class="form-control text_md pull-left" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtSellerEmail" Display="Dynamic" ErrorMessage="*" CssClass="tips" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">邮箱格式不正确</asp:RegularExpressionValidator>
                        <span style="color: red" class="tips">* 必填:如支付宝接口必须填写卖家Email,否则会出错</span>
                    </td>
                </tr>
                <tr >
                    <td>
                        <strong>分成：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtRate" runat="server" Text="0" class="form-control text_md pull-left" /><sapn class="tips">%</sapn>
                        <span style="color: black" class="tips">*此项如设置为10%，则商户得到90%</span>
                        <asp:RegularExpressionValidator runat="server" ID="Rev_1" ValidationExpression="^[0-9]\d*|0$" CssClass="tips" ControlToValidate="TxtRate" Display="Dynamic" ErrorMessage="只能输入整数"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr >
                    <td><strong>支持银行：</strong></td>
                    <td class="style3">
                        <asp:CheckBoxList ID="bankList" runat="server" Height="83px" RepeatColumns="6">
                            <asp:ListItem Value='ICBCB2C' Selected="True">中国工商银行 </asp:ListItem>
                            <asp:ListItem Value='CMB' Selected="True">招商银行 </asp:ListItem>
                            <asp:ListItem Value='CCB' Selected="True">中国建设银行 </asp:ListItem>
                            <asp:ListItem Value='ABC' Selected="True">中国农业银行 </asp:ListItem>
                            <asp:ListItem Value='CIB' Selected="True">兴业银行 </asp:ListItem>
                            <asp:ListItem Value='COMM' Selected="True">交通银行 </asp:ListItem>
                            <asp:ListItem Value='CEBBANK' Selected="True">光大银行 </asp:ListItem>
                            <asp:ListItem Value='BOCB2C' Selected="True">中国银行 </asp:ListItem>
                            <asp:ListItem Value='CITIC' Selected="True">中信银行 </asp:ListItem>
                            <asp:ListItem Value='PSBC-DEBIT' Selected="True">中国邮政储蓄银行</asp:ListItem>
                            <asp:ListItem Value='CMBC' Selected="True">中国民生银行</asp:ListItem>
                            <asp:ListItem Value='BJBANK' Selected="True">北京银行</asp:ListItem>
                        </asp:CheckBoxList>
                        <span>选择支持哪些银行交易</span></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="table table-bordered">
        <tr>
            <td align="center">&nbsp;&nbsp; &nbsp;
        <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" class="btn btn-primary" />&nbsp; &nbsp;
        <input name="Cancel" type="button" id="BtnCancel" value="取消" onclick="window.location.href = 'PayPlatManage.aspx'" class="btn btn-primary" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
</asp:Content>
