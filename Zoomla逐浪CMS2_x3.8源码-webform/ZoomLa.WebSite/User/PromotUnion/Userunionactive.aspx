<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Userunionactive.aspx.cs" Inherits="User_PromotUnion_Userunionactive" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>推广联盟活动</title>
<link href="../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
    //url检查
    function chkurl(url) {
        var urlRegS = /^http:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^\"\"])*$/;
        if (urlRegS.exec(url)) { return true; }
        else { return false; }
    }
    function createprolink(id1) {
        var url = document.getElementById(id1).value;
        if (!chkurl(url)) { alert('请输入正确的活动网址！'); document.getElementById(id1).focus(); return false; }
        else {return true;}
    }
    //复制命令
    function copytext(inputid) {
        var text = document.getElementById(inputid);
        if (text.value == "") { alert("没有内容"); text.focus(); return; }
        text.select(); //选择对象
        document.execCommand("Copy"); //执行浏览器复制命令
        alert("已复制好，可贴粘。");
    } 
</script>
</head>
<body>
<form id="form1" runat="server">
<div>
 <div id="container">
	<div style="background:#EAF5FE; margin-top:12px;">
		<div style="font-size:14px; font-weight:bold; color:#535353; height:16px; padding:23px 0 0 23px; ">获取您自己的活动推广链接：</div>
		<div style="height:22px; overflow:hidden; font-size:14px; margin-top:30px; margin-left:23px;">输入页面地址：
		<asp:TextBox ID="prolink" runat="server" style="width:425px; border:1px solid #C9C9C9;"></asp:TextBox>
		<asp:Button ID="btn" runat="server" style="vertical-align:top; margin-left:16px;" Text="生成推广链接" OnClientClick="return createprolink('prolink')" onclick="btn_Click"/>
	</div>
	<div style="height:22px; overflow:hidden; font-size:14px; margin-top:22px; margin-left:23px;">您的推广链接：
		<asp:TextBox ID="unionprolink" runat="server" style="width:425px; border:1px solid #C9C9C9; padding:1px 0;"></asp:TextBox>
		<input type="button" style="vertical-align:top; margin-left:16px;" value="复制推广链接"  onclick="copytext('unionprolink')"/>
	   </div>
		<div style="background:#fff; margin:30px 16px 16px 16px;">
	   </div>
		<div style="clear:both; height:0px;overflow:hidden;">&nbsp;</div>
	 </div>   
</div>
</div>
</form>
</body>
</html>