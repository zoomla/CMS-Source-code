<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMenber.aspx.cs" Inherits="ZoomLaCMS.MIS.Mail.AddMenber" MasterPageFile="~/Common/Master/Empty.master"  %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>添加联系人</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script>
function loadPage(id, url) {
    $("#" + id).addClass("loader");
    $("#" + id).append("Loading......");
    $.ajax({
        type: "get",
        url: url,
        cache: false,
        error: function () { alert('加载页面' + url + '时出错！'); },
        success: function (msg) {
            $("#" + id).empty().append(msg);
            $("#" + id).removeClass("loader");
        }
    });
}
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="Meno" class="Mis_pad"> 
<div class="Mis_Title"><strong><a href="javascript:void(0)" onclick="loadPage('Meno', 'NewMail.aspx')">新建邮件</a></strong></div>
<div id="Mail_left" class="left_menu">
<ul>
<li><a href="NewMail.aspx">写信</a></li>
<li><a href="Default.aspx">收信</a></li>
<li><a href="MailMenber.aspx">联系人</a></li>
</ul>
<ul><li><a href="Default.aspx">收件箱(1)</a></li>
<li><a href="Default.aspx">草稿箱 </a></li>
<li><a href="Default.aspx">已发邮件 </a></li>
<li><a href="Default.aspx">已删除   </a></li>
<li><a href="Default.aspx">垃圾邮件(86)  </a></li>
<li><a href="Default.aspx">附件管理 </a></li>
<li><a href="Default.aspx">手机短信提醒  </a></li>
</ul>
<ul>
<li><a href="MailSetList.aspx">邮箱设置 </a></li>
</ul>
</div>
<div id="mailMeno" class="rig_main"> 
<div ><strong><a href="AddMenber.aspx">新建联系人</a></strong> <strong><a href="javascript:void(0)" onclick="loadPage('Meno', '/user/UserZone/AddStructure.aspx?Group=2')" >新建联系组</a></strong> <strong><a href="javascript:history.go(-1);">[返回]</a></strong></div>
<table width="100%" class="border">
<tr><th width="18%" align="right">姓名*:</th><td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td></tr>
<tr><th align="right">email*: </th><td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td></tr>
<tr><th align="right">手机/电话 :</th><td><asp:TextBox ID="txtTel" runat="server"></asp:TextBox></td></tr>
<tr><th align="right">公司 :</th><td><asp:TextBox ID="txtCompany" runat="server"></asp:TextBox></td></tr>
<tr><th align="right">所属组 :</th><td><asp:TextBox ID="txtGroup" runat="server"></asp:TextBox></td></tr>
<tr><th align="right">备注 :</th><td><asp:TextBox ID="txtRemark" runat="server"></asp:TextBox></td></tr>
<tr><td></td><td><asp:Button ID="Button1" CssClass="i_bottom" runat="server" OnClick="Button1_Click" Text="确定" />  &nbsp;<input type="button" class="i_bottom" value="返回" onclick="javascript: history.go(-1);" /></td></tr>
</table>
</div>
</div>
</asp:Content>
