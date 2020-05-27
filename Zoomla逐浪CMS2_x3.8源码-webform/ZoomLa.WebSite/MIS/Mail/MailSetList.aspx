<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Common/Master/Empty.master" CodeFile="MailSetList.aspx.cs" Inherits="MIS_Mail_MailSetList" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>我的邮箱</title>
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
<ul>
<li><a href="NewMail.aspx">写信</a></li>
<li><a href="Default.aspx">收信</a></li>
<li><a href="MailView.aspx">联系人</a></li>
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
<div id="mailMeno" class="  rig_main">
收取、管理邮箱服务<br />
帮助您收取、管理工作单位的电邮或私人的电邮，类似于outlook、
foxmail等客户端。 <br />
并可直接用代收邮箱帐号发信。

<div class="Mis_Title"><strong><a href="MailSet.aspx">新增邮箱</a></strong></div>
<div class="td_center">
<table class="border" width="100%">
<tr><th >邮箱帐号</th><th>状态</th><th>默认发送邮箱</th><th>时间</th><th>操作</th></tr> 
<asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound"  >
<ItemTemplate>
<tr><td><a href="/Mis/Mail/Default.aspx.aspx?Eid=<%#Eval("ID") %>&type=Edit"><%#Eval("UserMail") %></a></td><td><asp:Label ID="labSta" runat="server" Text=' '></asp:Label> </td><td>  <%#GetisDef(Eval("ID").ToString(),Convert.ToBoolean(Eval("IsDefault"))) %></td><td><%#Eval("CreateTime") %></td><td> <a href="/Mis/Mail/MailSet.aspx?ID=<%#Eval("ID") %>&type=Edit">修改</a> | <asp:LinkButton ID="isUser" runat="server"  CommandArgument='<%#Eval("ID") %>' CommandName="isUser" Text="停用"></asp:LinkButton> | <a href="/Mis/Mail/MailSet.aspx?ID=<%#Eval("ID") %>&type=View">查看</a></td></tr> 
</ItemTemplate>
</asp:Repeater>
</table>  
</div>
</div>
<div class="clear"></div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
function setDefault(id)
{ 
$.ajax({
    url: "/Mis/Mail/SetPage.aspx?callback=?",
    type: "GET",
    dataType:"jsonp", 
    jsonpCallback:"person",
    data: "type=IsDef&id=" + id,//uri:域名;uname:用户名;pwd:必须为MD5加密; tid: 1 添加,2 修改, 3 删除
    success: function (msg) { 
        switch (msg) {
            case 0: alert("默认邮箱更新成功"); true;
        }
    }
});
}
</script>
</asp:Content>

