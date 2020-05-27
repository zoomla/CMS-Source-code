<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileMsgConfig.aspx.cs"  MasterPageFile="~/Manage/I/Default.master" Inherits="ZoomLaCMS.Manage.Config.MobileMsgConfig" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>短信配置</title>
<link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
<script src="/dist/js/bootstrap-switch.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered">
<tr>
    <td class="td_l"><strong><%:lang.LF("选择默认短信接口") %>：</strong></td>
    <td>
        <asp:DropDownList ID="ddlMessageCheck_DP" runat="server" CssClass="form-control text_300" OnSelectedIndexChanged="ddlMessageCheck_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="0">关闭</asp:ListItem>
            <asp:ListItem Value="1">深圳全网(推荐)</asp:ListItem>
            <asp:ListItem Value="2">北京东时方</asp:ListItem>
            <asp:ListItem Value="3">亿美软通</asp:ListItem>
            <asp:ListItem Value="4">云通讯</asp:ListItem>
        </asp:DropDownList>
        <span class="rd_red">*必选</span></td>
</tr>
<tbody runat="server" id="ShenZhen_P" visible="false">
    <tr>
        <td class="td_l"><strong><%:lang.LF("短信通用户") %>：</strong></td>
        <td>
            <asp:TextBox ID="TxtMssUser" runat="server" class="form-control text_300"/>
            <span class="rd_red">* 必填</span></td>
    </tr>
    <tr>
        <td><strong><%:lang.LF("短信通密码") %>：</strong></td>
        <td>
            <asp:TextBox ID="TxtMssPsw" runat="server" TextMode="Password" class="form-control text_300"/>
            <span class="rd_red">* 必填</span></td>
    </tr>
<%--    <tr>
        <td colspan="2">
            <strong><a href="http://www.z01.com/server" target="view_window">没有帐号?点此联系官方授权短信通>></a></strong>
        </td>
    </tr>--%>
</tbody>
<tbody runat="server" id="East_P">
    <tr>
        <td class="td_s"><strong><%:lang.LF("东时方企业号") %>：</strong></td>
        <td>
            <asp:TextBox ID="txtg_eid" runat="server" class="form-control text_300"/>
            <span class="rd_red">* 必填</span></td>
    </tr>
    <tr>
        <td><strong><%:lang.LF("东时方用户") %>：</strong></td>
        <td>
            <asp:TextBox ID="txtg_uid" runat="server" class="form-control text_300"/>
            <span class="rd_red">* 必填</span></td>
    </tr>
    <tr>
        <td><strong><%:lang.LF("东时方密码") %>：</strong></td>
        <td>
            <asp:TextBox ID="txtg_pwd" runat="server" TextMode="Password" class="form-control text_300"/>
            <span class="rd_red">* 必填</span></td>
    </tr>
    <tr>
        <td><strong><%:lang.LF("东时方通道") %>：</strong></td>
        <td>
            <asp:TextBox ID="txt_h_gate_id" runat="server" class="form-control text_300"/>
            <span class="rd_red">* 必填</span></td>
    </tr>
<%--    <tr>
        <td colspan="2"><strong><a href="http://www.xhsms.com/" target="view_window">东时方短信接口申请>></a></strong></td>
    </tr>--%>
</tbody>
<tbody runat="server" id="YiMei_P" visible="false">
    <tr>
        <td class="td_l"><strong><%:lang.LF("序列号") %>：</strong></td>
        <td>
            <asp:TextBox ID="smskeyT" runat="server" class="form-control text_300" /><span class="rd_red">* 必填</span></td>
    </tr>
    <tr>
        <td><strong>密码：</strong></td>
        <td>
            <asp:TextBox ID="smspwdT" runat="server" class="form-control text_300" TextMode="Password" /><span class="rd_red">* 必填</span></td>
    </tr>
<%--       <tr>
        <td colspan="2"><strong><a href="http://www.emay.cn/" target="view_window">亿美软通接口申请>></a></strong></td>
    </tr>--%>
</tbody>
<tbody runat="server" id="CCPRest_Div" visible="false">
    <tr>
        <td class="td_l">应用ID:</td>
        <td>
            <asp:TextBox ID="CCAppID_T" Text="8a48b55152a56fc20152aa33f3d60673" runat="server" CssClass="form-control text_300"/>
        </td>
    </tr>
    <tr>
        <td>ACCOUNT_SID:</td>
        <td>
            <asp:TextBox ID="CCAccount_T" Text="aaf98f8952a572be0152aa32e1b606e4" runat="server" CssClass="form-control text_300"/>
        </td>
    </tr>
    <tr>
        <td>AUTH_TOKEN:</td>
        <td>
            <asp:TextBox ID="CCToken_T" Text="595fc398965a433a9a57290fa7179fc4" runat="server" CssClass="form-control text_300"/>
        </td>
    </tr>
    <tr>
        <td>短信模板ID:</td>
        <td>
            <asp:TextBox ID="CCTemplate_T" runat="server" CssClass="form-control text_300"/>
        </td>
    </tr>
      <tr>
        <td colspan="2"><strong><a href="http://www.yuntongxun.com/" target="view_window">云通讯接口申请>></a></strong></td>
    </tr>
</tbody>
<tr>
    <td class="td_l"><strong>每日手机号发送次数：</strong></td>
    <td>
        <asp:TextBox ID="MaxPhoneMsg" runat="server" Text="10" CssClass="form-control text_s num"/><span class="rd_green">为0则不按手机号限定次数</span>
    </td>
</tr>
<tr>
    <td><strong>每日ip发送次数：</strong></td>
    <td>
        <asp:TextBox ID="MaxIpMsg" runat="server" Text="50" CssClass="form-control text_s num"/><span class="rd_green">为0则不按IP限定次数</span>
    </td>
</tr>
<tr>
    <td><strong><%:lang.LF("黑名单") %>：</strong></td>
    <td>
        <asp:TextBox ID="blackList" runat="server" class=" form-control text_300"/>
        <span>用户ID用“,”隔开</span></td>
</tr>
<tr>
    <td><strong><%:lang.LF("会员变更手机号权限") %>：</strong></td>
    <td>
        <asp:RadioButtonList runat="server" ID="userMobilAuth" RepeatDirection="Horizontal">
            <asp:ListItem Value="0">短信验证</asp:ListItem>
            <asp:ListItem Value="1" Selected="True">自由修改</asp:ListItem>
        </asp:RadioButtonList></td>
</tr>
<tr><td></td><td><asp:Button runat="server" ID="Save_Btn" Text="保存配置" OnClick="Save_Btn_Click" CssClass="btn btn-primary" /></td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/ZL_Regex.js"></script>
    <script>
        $(function () {
            ZL_Regex.B_Num('.num');
        });
    </script>
</asp:Content>