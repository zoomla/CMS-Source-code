<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Redindulgence.aspx.cs" Inherits="PromotUnion_Redindulgence" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>红包涵推广</title>
<script type="text/javascript" language="javascript">

	//email检查
	function isValidEmail(inEmail) {
		var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
		if (filter.test(inEmail)) return true;
		else return false;
	}
	
	function checkemail() {
		var email = document.getElementById("email_name").value;
		if (!isValidEmail(email)) {
			alert("您填写的电子邮件地址, 可能不是一个有效的邮件地址，请检查后重新提交！");
			document.getElementById("email_name").focus();
			return false;
		}
		else if (email.length > 32) {
			alert("您输入的电子邮件地址长度超过允许范围，请检查后重新提交。");
			document.getElementById("email_name").focus();
			return false;
		}
		if (document.getElementById("f_name").value == "") {
			alert("请填写收件人的姓名");
			document.getElementById("f_name").focus();
			return false;
		}
		if (document.getElementById("r_name").value == "") {
			alert("请填写发件人的姓名");
			document.getElementById("r_name").focus();
			return false;
		}
	}
</script>
</head>
<body>
<form id="form1" runat="server">
<div>
    <div>
        <div style="font-size: 14px; padding-top: 20px;">
            您目前还剩余 <span style="color: #EB3D00;">
                <asp:Label ID="lblNumChance" runat="server"></asp:Label></span> 封5元红包可发放给您的朋友(红包由返利网提供)
        </div>
        <div style="font-size: 12px; color: #7c7c7c; margin-top: 13px;">
            注意：您可发送红包的数量和您被[确认有效]的购物订单数量相同，请珍惜您的红包发放机会！</div>
        <div style="margin-top: 29px; font-size: 14px; color: #313131;">
            E-Mail<span style="margin-left: 138px;">朋友姓名</span><span style="margin-left: 88px; margin-left: 90px;">邀请人</span></div>
        <div style="margin-top: 10px;">
            <asp:TextBox ID="email_name" runat="server" name="email_name" Style="width: 170px;
                border: 1px solid #C9C9C9;"></asp:TextBox>
            <asp:TextBox ID="f_name" runat="server" name="f_name" Style="width: 134px; border: 1px solid #c9c9c9; margin-left: 6px;"></asp:TextBox>
            <asp:TextBox ID="r_name" runat="server" name="r_name" value="" Style="width: 134px;
                border: 1px solid #c9c9c9; margin-left: 6px;"></asp:TextBox>
            <asp:Button ID="btn" runat="server" Text="发送红包" Style="width: 74px; height: 21px; border: 0; vertical-align: top; margin-left: 6px;" OnClientClick="return checkemail();"  OnClick="btn_Click" /></div>
        <div style="font-size: 12px; color: #7C7C7C; line-height: 22px; padding: 30px 0;">
            注意：<br />
            1、如果朋友没收到邮件，请让对方到"邮件垃圾箱"里查找，或者让对方把service@post.ttfanli.com加入到邮箱的联系人名单后，您再发送。
            <br />
            2、如果好友一直收不到邮件，您可以使用返利网<a href="ref_email.aspx" target="_blank">自助查询红包函链接</a>的功能查询到注册链接，然后把该链接直接发送给您的好友，好友通过点击该链接注册返利网，您和您的好友一样可以获得奖励！<br />
            3、会员不得自己推荐自己来虚假领取红包，一经发现，严格处理！请珍惜您的红包发放机会！</div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
    <asp:HiddenField ID="hfNum" runat="server" />
    <asp:HiddenField ID="hfRid" runat="server" />
</div>
</form>
</body>
</html>