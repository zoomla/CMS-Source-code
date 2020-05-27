<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplySetTree.aspx.cs" Inherits="ZoomLaCMS.Manage.UserShopMannger.ApplySetTree" %>

<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title><%=lang.LF("商家店铺管理")%></title>
<script type="text/javascript">
function Switch(obj) {
obj.className = (obj.className == "guideexpand") ? "guidecollapse" : "guideexpand";
var nextDiv;
if (obj.nextSibling) {
	if (obj.nextSibling.nodeName == "DIV") {
		nextDiv = obj.nextSibling;
	}
	else {
		if (obj.nextSibling.nextSibling) {
			if (obj.nextSibling.nextSibling.nodeName == "DIV") {
				nextDiv = obj.nextSibling.nextSibling;
			}
		}
	}
	if (nextDiv) {
		nextDiv.style.display = (nextDiv.style.display != "") ? "" : "none";
	}
}
}
function OpenLink(lefturl, righturl) {
if (lefturl != "") {
	parent.frames["left"].location = lefturl;
}
try {
	parent.MDIOpen(righturl); return false;
} catch (Error) {
	parent.frames["main_right"].location = righturl;
}
}

function gotourl(url) {
try {
	parent.MDILoadurl(url); void (0);
} catch (Error) {
	parent.frames["main_right"].location = "../" + url; void (0);
}
}
</script>
</head>
<body id="Guidebody" style="margin: 0px; margin-top: 1px;">
<form id="formGuide" runat="server">
<div id="Div1">
<ul>
	<li id="Guide_top">
		<div id="Guide_toptext">
			<%=lang.LF("商家店铺管理")%></div>
	</li>
	<li id="Guide_main">
		<div id="Guide_box">
			<div class="guideexpand" onclick="Switch(this)">
				<%=lang.LF("店铺申请设置")%></div>
			<div class="guide">
				<ul>
					<li class="guideli" onmouseover="this.className='guidelihover'" onmouseout="this.className='guideli'">
						<a href="../UserShopManage/StoreManage.aspx" target="main_right">
							<%= lang.LF("店铺管理列表") %>
						</a></li>
					<li class="guideli" onmouseover="this.className='guidelihover'" onmouseout="this.className='guideli'">
						<a href="../Content/ModelManage.aspx?ModelType=6" target="main_right"><%=lang.LF("申请模型管理")%></a></li>
					<li class="guideli" onmouseover="this.className='guidelihover'" onmouseout="this.className='guideli'">
						<a href="../Content/AddEditModel.aspx?ModelType=6" target="main_right"><%=lang.LF("添加申请模型")%></a></li>
				</ul>
			</div>
		</div>
	</li>
</ul>
</div>
</form>
</body>
</html>