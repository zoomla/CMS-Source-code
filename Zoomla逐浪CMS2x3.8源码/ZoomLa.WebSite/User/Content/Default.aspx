<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ZoomLa.WebSite.User.Content.Default" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>内容管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<script type="text/javascript">
function SetWinHeight(obj) {
	var win = obj;
	if (document.getElementById) {
		if (win && !window.opera) {
			if (win.contentDocument && win.contentDocument.body.offsetHeight) {
				win.height = win.contentDocument.body.offsetHeight;
				win.width = win.contentDocument.body.offsetWidth;
			}
			else if (win.Document && win.Document.body.scrollHeight) {
				win.height = win.Document.body.scrollHeight;
				win.width = win.Document.body.scrollWidth;
			}
		}
	}
}
function JumpToMain(val) {
	var objLeft = window.frames['main_left'];
	var objContent = window.frames['main_right'];
	switch (val) {
		default:
		case 1:
			try {
				objLeft.location.href = "NodeTree.aspx?t=1";
				objContent.location.href = "MyContent.aspx";
			} catch (Error)
	{ }
			break;
		case 2:
			try {
				objLeft.location.href = "NodeTree.aspx?t=2";
				objContent.location.href = "MyContent.aspx?type=UnAudit";
			} catch (Error)
	{ }
			break;
		case 3:
			try {
				objLeft.location.href = "NodeTree.aspx?t=3";
				objContent.location.href = "MyContent.aspx?type=Audit";
			} catch (Error)
	{ }
			break;
		case 4:
			try {
				objLeft.location.href = "NodeTree.aspx?t=4";
				objContent.location.href = "MyFavori.aspx";
			} catch (Error)
	{ }
			break;
		case 5:
			try {
				objLeft.location.href = "NodeTree.aspx?t=5";
				objContent.location.href = "MyComment.aspx"
			} catch (Error)
	 { }
			break;
		case 6:
			try {
				objLeft.location.href = "NodeTree.aspx?t=6";
				objContent.location.href = "Pub.aspx";
			} catch (Error)
	 { }
			break;
	}
}
</script>
<style>
#nav_box { width:100%; position:fixed;_position:absolute;bottom:auto;_top:expression(eval(document.documentElement.scrollTop));top:0; /*left:0px;*/height:32px;z-index:999999;}
.us_topinfo{ display:block; position:relative;background:#e6f2f8; margin-top:0px;}
</style>
<div id="nav_box"> 
<div class="us_topinfo" style="display:none;">
    <div class="us_pynews">
<a title="会员中心" href="/User/Default.aspx" target="_parent">会员中心</a> &gt;&gt; 投稿管理
    </div>
<div class="cleardiv"></div>
</div>
</div>
<div runat="server" id="Login" class="us_seta"   style="position:absolute;top:40%;left:40%" visible="false">
    <table >
        <tr>
            <td colspan="2"><font color="red">本页需支付密码才能登录请输入支付密码</font></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="Second" runat="server" TextMode="Password"></asp:TextBox></td>
            <td><asp:Button ID="sure" runat="server" Text="确定" onclick="sure_Click" /></td>
        </tr>
    </table>
</div>
<div runat="server" id="DV_show">
<div class="s_body hidden" style="width:100%">  
<iframe id="I2" style="border-style: none; border-color: inherit; border-width: 0px;width: 100%; height:1200px;" src="MyContent.aspx" frameborder="0" scrolling="no" name="I2"></iframe>
</div> 
</div>
</asp:Content>