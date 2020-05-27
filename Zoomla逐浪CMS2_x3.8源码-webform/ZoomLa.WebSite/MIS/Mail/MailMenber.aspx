<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailMenber.aspx.cs" MasterPageFile="~/Common/Master/Empty.master" Inherits="MIS_Mail_MailMenber" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
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
<ul><li><a href="Default.aspx">收件箱(<asp:Label ID="ENum" runat="server"></asp:Label>)</a></li>
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
<div id="Mail_rig" class="rig_main  td_center"> 
<div class="cont_left">
<div class="bggrey">联系人</div>
<div class="cont_type">
<div class="cont_Ltit"><a href=""> 所有联系人</a></div>
<ul>
<asp:Repeater ID="Repeater2" runat="server"><ItemTemplate>
<li><a href="MailMenber.aspx?gid=<%#Eval("ID") %>"> <%#Eval("Name") %></a></li>
</ItemTemplate> 
</asp:Repeater></ul>
[<a href="">新建</a>]
</div> 
</div>
<div  class="cont_rig">
<div ><strong><a href="AddMenber.aspx">新建联系人</a></strong> <strong><a href="/user/UserZone/AddStructure.aspx?Group=2">新建联系组</a></strong> <strong><a href="javascript:history.go(-1);">[返回]</a></strong></div>
<table width="100%" class="border" >
<tr><th></th width="5%"><th>联系人名称</th> <th>email </th> <th>手机/电话 </th></tr>
<ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='4' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
<ItemTemplate><tr><td  width="5%"><input type="checkbox" value="1" /></td><td><a href="AddMenber.aspx?ID=<%#Eval("ID") %>"><%#Eval("Name") %></a></td><td><%#Eval("email") %></td><td><%#Eval("Tel") %></td></tr>
</ItemTemplate>
<FooterTemplate></FooterTemplate>
</ZL:ExRepeater> 
</table>
</div>
</div>
</div>
</asp:Content>

