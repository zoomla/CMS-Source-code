<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.MIS.Mail.Default" MasterPageFile="~/Common/Master/Empty.master" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>邮件系统</title>
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
<div class="Mis_Title"><strong><a href="NewMail.aspx" onclick="loadPage('Meno', 'NewMail.aspx')">新建邮件</a></strong></div>
<div id="Mail_left" class="left_menu">
<ul>
<li><a href="NewMail.aspx">写信</a></li>
<li><a href="Default.aspx">收信</a></li>
<li><a href="MailMenber.aspx">联系人</a></li>
</ul>
<ul><li><a href="Default.aspx">收件箱(<asp:Label ID="ENum" runat="server"></asp:Label>)</a></li>
<li><a href="Default.aspx?type=no">草稿箱 </a></li>
<li><a href="Default.aspx?type=yes">已发邮件 </a></li>
<li><a href="Default.aspx?type=del">已删除   </a></li>
<li><a href="Default.aspx">垃圾邮件(86)  </a></li>
<li><a href="flieslist.aspx">附件管理 </a></li>
<li><a href="Default.aspx">手机短信提醒  </a></li>
</ul>
<ul>
<li><a href="MailSetList.aspx">邮箱设置 </a></li>
</ul> 
</div>
<div id="Mail_rig" class="rig_main">
 <asp:HiddenField ID="tbxUserMail" runat="server" ></asp:HiddenField> 
<asp:HiddenField ID="txbPassword" runat="server"></asp:HiddenField> 
<asp:LinkButton ID="btnLogout" runat="server" Text="注销" OnClick="btnLogout_Click_1"></asp:LinkButton>
<asp:LinkButton ID="Button1" runat="server" Text="刷新" OnClick="btnRefreshMailList_Click"></asp:LinkButton><br />
<asp:TextBox ID="txtMsgList" runat="server" TextMode="MultiLine"  Style="display:none;"></asp:TextBox> 
     
<asp:HiddenField ID="tbxSmtpServer" runat="server"></asp:HiddenField> 
<asp:HiddenField ID="tbxPOP3Server" runat="server"></asp:HiddenField> 
 
邮件：获取邮件 共<asp:Label ID="tbxMailboxInfo" runat="server"></asp:Label>封<br />
<table width="100%" class="border" border="0" cellSpacing="1" cellPadding="0">
<tr><th></th><th></th><th>发件人</th><th>主题</th> <th width="130">时间</th> </tr>
<ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='5' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>" OnItemCommand="Repeater1_ItemCommand">
<ItemTemplate><tr class="tdbg" onmouseover="this.className='tdbgmouseover';" onmouseout="this.className='tdbg';" ondblclick="javascript:location.href='MailView.aspx?ID=<%#Eval("ID") %>'" style="cursor:pointer;"><td><checkbox></checkbox></td><td>
    <%#getfile(Convert.ToInt32(Eval("Count"))) %></td><td><a href="MailView.aspx?ID=<%#Eval("ID") %>" ><%#Eval("From") %></a> </td><td><%#Eval("Subject") %></td> <td  width="130"><%#Eval("Time","{0:yyyy-MM-dd hh:mm}")  %></td> </tr></ItemTemplate>
    <FooterTemplate></FooterTemplate>
</ZL:ExRepeater>
选择： 全部 -  反选  -  已读 -  未读 
</table> 
  <div id="mailView" runat="server">  
  
       
  <asp:Button ID="btnRefreshMailList" CssClass="i_bottom" runat="server" Text="刷新" OnClick="btnRefreshMailList_Click"></asp:Button> 
</div>
</div>
</div>
</asp:Content>
