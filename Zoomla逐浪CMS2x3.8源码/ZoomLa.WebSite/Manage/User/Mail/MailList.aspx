<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailList.aspx.cs" Inherits="ZoomLa.WebSite.Manage.User.MailList" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script type="text/javascript" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<title>邮件发送</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
  <tr>
    <td class="tdleft td_l"><strong>邮件模板：</strong></td>
    <td><asp:DropDownList ID="MailTemp_DP" CssClass="form-control text_md" runat="server" AutoPostBack="true" OnSelectedIndexChanged="MailTemp_DP_SelectedIndexChanged">
      </asp:DropDownList></td>
  </tr>
  <tr>
    <td  class="tdleft"><strong>收件人选择：</strong></td>
    <td style="text-align: left">
        <span class="fl"><asp:RadioButton ID="RadUserType0" runat="server" GroupName="UserType" Text="所有会员" /></span>
        <span class="fl"><asp:RadioButton ID="RadUserType2" runat="server" GroupName="UserType" Text="指定用户名" /></span>
        <div class="input-group text_300 hidden" id="text_div">
            <asp:TextBox class="form-control text_300" ID="TxtUserName" runat="server"/>
            <span class="input-group-btn">
		        <input type="button" value="选择" class="btn btn-info" onclick="SelectUser();" />
	        </span>
        </div>
    </td>
  </tr>
  <tr>
    <td class="tdleft"><strong>邮件主题：</strong></td>
    <td><asp:TextBox runat="server" ID="Subject_T"  class="form-control m715-50"/><span class="rd_red">*</span>
      <asp:RequiredFieldValidator ID="ValrSubject" runat="server" ControlToValidate="Subject_T" Display="None" ErrorMessage="邮件主题不能为空!" /></td>
  </tr>
  <tr>
    <td class="tdleft"><strong>邮件内容：</strong></td>
    <td><asp:TextBox ID="TxtContent" runat="server" TextMode="MultiLine" CssClass="m715-50"  Height="300px"/></td>
  </tr>
  <tr>
    <td class="tdleft"><strong>发件人：</strong></td>
    <td><asp:TextBox class="form-control" Style="max-width: 200px;" ID="TxtSenderName" runat="server" Width="350px"/></td>
  </tr>
  <tr>
    <td class="tdleft"><strong>回复Email：</strong></td>
    <td><asp:TextBox class="form-control" Style="max-width: 200px;" ID="TxtSenderEmail" runat="server" Width="350px"/>
      <asp:RegularExpressionValidator ID="ValeSenderEmail" runat="server" ControlToValidate="TxtSenderEmail" Display="None" ErrorMessage="回复Email不是有效的Email地址！" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
  </tr>
  <tr>
    <td class="tdleft"><strong>邮件优先级：</strong></td>
    <td><asp:RadioButtonList ID="RadlPriority" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Value="2">高</asp:ListItem>
        <asp:ListItem Selected="True" Value="0">普通</asp:ListItem>
        <asp:ListItem Value="1">低</asp:ListItem>
      </asp:RadioButtonList></td>
  </tr>
  <tr class="tdbgbottom">
    <td colspan="2" style="text-align: center;"><asp:Button ID="BtnSend" runat="server" Text="发送" OnClick="BtnSend_Click" class="btn btn-primary" />
      <input id="Reset1" type="reset" value="清除" class="btn btn-primary" /></td>
  </tr>
</table>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
<div class="alert alert-info">
<div><i class="fa fa-flag"></i>站点信息：</div>
    <div class="footbox">
        <span class="btn btn-default">邮件标题：{$Title/}</span>
        <span class="btn btn-default">网站名称：{$SiteName/}</span>
        <span class="btn btn-default">网站地址：{$SiteUrl/}</span>
        <span class="btn btn-default">网站标题：{$SiteTitle/}</span>
        <span class="btn btn-default">网站Logo：{$LogoUrl/}</span>
        <span class="btn btn-default">广告图：{$BannerUrl/}</span>
        <span class="btn btn-default">网站版权：{$Copyright/}</span>
        <div class="margin_t5">
            <span class="btn btn-default">网站关键字：{$MetaKeywords/}</span>
            <span class="btn btn-default">网站描述：{$MetaDescription/}</span>
            <span class="btn btn-default">站长姓名：{$Webmaster/}</span>
            <span class="btn btn-default">站长信箱：{$WebmasterEmail/}</span>
        </div>
    </div>
</div>
<div class="alert alert-info">
    <div><i class="fa fa-flag"></i>以下是您在注册会员时填写的基本信息：</div>
    <div class="footbox">
        <span class="btn btn-default">账号: {$UserName/}</span>
        <span class="btn btn-default">密码：{$password/}</span>
        <span class="btn btn-default">公司名：{$Company/}</span>
        <span class="btn btn-default">办公电话：{$OfficePhone/}</span>
        <span class="btn btn-default">昵称: {$HoneyName/}</span>
        <span class="btn btn-default">头像: {$UserFace/}</span>
        <span class="btn btn-default">性别：{$sex</span>
        <span class="btn btn-default">出生日期：{$BirthDay/}</span>
        <div class="margin_t5">
            <span class="btn btn-default">国家：{$Country/}</span>
            <span class="btn btn-default">省份：{$Province</span>
            <span class="btn btn-default">城市：{$City/}</span>
            <span class="btn btn-default">县：{$County/}</span>
            <span class="btn btn-default">地址：{$Address/}</span>
            <span class="btn btn-default">邮政编码：{$ZipCode/}</span>
            <span class="btn btn-default">电话：{$Mobile/}</span>
            <span class="btn btn-default">传真：{$Fax/}</span>
            <span class="btn btn-default">E-mail地址：{$Email/}</span>
        </div>
        <div class="margin_t5">
            <span class="btn btn-default">个人主页：{$HomePage/}</span>
            <span class="btn btn-default">QQ号：{$QQ/}</span>
            <span class="btn btn-default">MSN地址：{$MSN/}</span>
            <span class="btn btn-default">签名档：{$Sign/}</span>
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"> 
<script type="text/javascript" src="/JS/Common.js"></script> 
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    $(function () {
        $("#RadUserType0").attr("Checked", "true");
        $("#RadUserType0").click(function () {
            $("#text_div").addClass("hidden");
        })
        $("#RadUserType2").click(function () {
            $("#text_div").removeClass("hidden");
        })
    })
    function SelectUser() {
        url = "/Mis/OA/Mail/SelGroup.aspx?Type=AllInfo";
        comdiag.maxbtn = false;
        ShowComDiag(url, "选择用户");
    }
    function UserFunc(json, select) {
        var uname = "";
        var uid = "";
        for (var i = 0; i < json.length; i++) {
            uname += json[i].UserName + ",";
            uid += json[i].UserID + ",";
        }
        if (uid) uid = uid.substring(0, uid.length - 1);
        $("#TxtUserName").val($("#TxtUserName").val()+uname);
        CloseComDiag();
    }
</script> 
<%=Call.GetUEditor("TxtContent",2) %> 
</asp:Content>
