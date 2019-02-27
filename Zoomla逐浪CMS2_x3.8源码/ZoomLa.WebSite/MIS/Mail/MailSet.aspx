<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Common/Master/Empty.master" CodeFile="MailSet.aspx.cs" Inherits="MIS_Mail_MailSet" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>邮箱设置</title>
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
    loadPage("Mail_left", "/Mis/Mail/Mail_Left.aspx");
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="Meno" class="Mis_pad">  
<div class="Mis_Title"><strong><a href="javascript:void(0)" onclick="loadPage('Meno', 'NewMail.aspx')">新建邮件</a></strong></div>
<div id="Mail_left" class="left_menu">
 </div>
<div id="Mail_rig" class="rig_main">
<strong>邮箱设置：</strong> <br />
SMTP： <asp:TextBox ID="tbxSmtpServer" runat="server"></asp:TextBox><br />
POP3： <asp:TextBox ID="tbxPOP3Server" runat="server"></asp:TextBox><br />
邮 箱： <asp:TextBox ID="tbxUserMail" runat="server"></asp:TextBox> <br />
<asp:Label ID="Labpwd" runat="server">密 码：</asp:Label>   <asp:TextBox ID="txbPassword" runat="server"  TextMode="Password"></asp:TextBox><br />
<asp:Button ID="Button1" CssClass="i_bottom" runat="server" OnClick="Button1_Click" Text="确定" />   &nbsp;<input type="button" class="i_bottom" value="返回" onclick="javascript:history.go(-1);" />
</div>
</div>
</asp:Content>



