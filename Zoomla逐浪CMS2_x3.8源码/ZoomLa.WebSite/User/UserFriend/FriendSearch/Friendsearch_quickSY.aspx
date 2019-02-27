<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Friendsearch_quickSY.aspx.cs" Inherits="Friendsearch_quickSY" EnableEventValidation="false" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>会员中心 >> 搜索好友</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">
function submitinfo() {
	if (document.getElementById("radioSex1").checked) {
		document.getElementById("sex").value = "男生";
	}
	else {
		document.getElementById("sex").value = "女生";
	}
	document.getElementById("age1").value = document.getElementById("txtAge1").value;
	document.getElementById("age2").value = document.getElementById("txtAge2").value;
	document.getElementById("wcounty").value = document.getElementById("DropDownList3").value;
	if (document.getElementById("DropDownList3").value != "") {
		document.getElementById("wcity").value = document.getElementById("DropDownList4").value;
	}
	document.forms[1].submit();

}
</script>
</head>
<body>
<form id="form1" runat="server">
<div class="us_topinfo" style="padding: 10px; width: 100%">
	<table border="0" class="us_showinfo" cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td align="right">
				性别：
			</td>
			<td width="69%" align="left">
				<input id="radioSex1" type="radio" value="男生" name="sex" checked="checked" />男生<input id="radioSex2" type="radio" name="sex" value="女生" />女生
			</td>
		</tr>
		<tr>
			<td align="right">
				年龄：
			</td>
			<td align="left">
				&nbsp;<input id="txtAge1" type="text" style="width: 48px" onkeyup="value=value.replace(/[^\d\.]/g,'')" />~<input id="txtAge2" type="text" style="width: 48px" onkeyup="value=value.replace(/[^\d\.]/g,'')" />
			</td>
		</tr>
		<tr>
			<td align="right">
				地区：
			</td>
			<td align="left">
				&nbsp;<asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
				</asp:DropDownList>
				<asp:DropDownList ID="DropDownList4" runat="server" Visible="false">
				</asp:DropDownList>
			</td>
		</tr>
	</table>
</div>
</form>
<form id="form2" method="post" action="Friend_quickSYResult.aspx">
<div class="us_topinfo" style="padding: 10px; width: 600px !important;">
	<input type="hidden" id="sex" name="sex" runat="server" />
	<input type="hidden" id="age1" name="age1" runat="server" />
	<input type="hidden" id="age2" name="age2" runat="server" />
	<input type="hidden" id="wcounty" name="wcounty" runat="server" />
	<input type="hidden" id="wcity" name="wcity" runat="server" />
	<input type="button" id="submitBtn" name="submitBtn" value="搜索" onclick="submitinfo()" />
</div>
</form>
</body>
</html>