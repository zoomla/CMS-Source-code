<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lockin.aspx.cs" Inherits="ZoomLaCMS.Manage.Lockin" MasterPageFile="~/Common/Common.master" EnableViewStateMac="false" ClientIDMode="Static" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>解锁</title>
<style type="text/css">
body { font-size: 16px; background-color:rgb(28, 98, 151);}
#lock { height: 100%; background: url(/Install/images/bg.jpg) no-repeat; background-size:cover; color: #1C6297;  padding-top: 170px; padding-bottom: 200px; width: 100%;  z-index: 10001; }
.loFont {  font-size:16px; margin: auto;  color: #FFF; line-height: 25px; }
.pass_div{position:relative; margin:auto; width:320px; font-size:16px;}
.pass_div i{position:absolute; left:10px; top:33px; font-size:20px; color:#5BC0DE; }
.pass_div input{padding-left:30px;}
</style>
<script type="text/javascript">
//设置Cookies
function setCookie(obj) {
if (!navigator.cookieEnabled) {
alert('不允许设置Cookie项!');
} else {
var date = new Date();
date.setTime(date.getTime() + 60000 * 10);
document.cookie = 'SetLock=' + escape(obj) + ';expires=' + date.toGMTString() + ';path=/' + ';domaim=zgdsc.cn' + ':secure';
}
}
function keydown() {
if (event.keyCode == 13) {
event.returnValue = false;
event.cancel = true;
document.getElementById("btn").click();
}
}
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="padding-top: 200px;">
	    <div id="lock">
		    <div class="loFont">
                <div class="pass_div">
			    <span class="fa fa-warning" style="font-size:16px;"></span> 当前界面被管理员锁定<br />
                    <i class="fa fa-lock"></i>
                    <asp:TextBox ID="TxtPassword" TextMode="Password"  runat="server" placeholder="请输入密码按回车解锁" CssClass="form-control" MaxLength="15" TabIndex="2" onkeydown="keydown()"></asp:TextBox>
                <div id="tips" runat='server' style="color: Red"></div>                    
                </div>
		    </div>
	    </div>
	    <asp:Button ID="btn" runat="server" Text="提交" style="margin-top:100px;" OnClick="btn_Click1"  />
    </div>
</asp:Content>
