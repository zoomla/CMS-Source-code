<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeSet.aspx.cs" Inherits="FreeHome.Home.HomeSet" %>

<!DOCTYPE HTML>
<html>
<head runat="server">
<title>我的空间</title>
<link href="../style/style.css" type="text/css" runat="server" rel="stylesheet" />
<script language="JavaScript">
<!--        // RightClickMenu
function showmenu() {
	var rightedge = event.clientX
	var bottomedge = event.clientY
	if (rightedge > 480)
		ie5menu.style.left = 480
	else
		ie5menu.style.left = rightedge + 20
	if (bottomedge > 480)
		ie5menu.style.top = 480
	else
		ie5menu.style.top = bottomedge
	ie5menu.style.visibility = "visible"
	return false
}
function showmenu2() {
	var rightedge = event.clientX
	var bottomedge = event.clientY
	if (rightedge > 480)
		ie5menu2.style.left = 480
	else
		ie5menu2.style.left = rightedge + 20
	if (bottomedge > 480)
		ie5menu2.style.top = 480
	else
		ie5menu2.style.top = bottomedge
	ie5menu2.style.visibility = "visible"
	return false
}

// -->
</script>
<script language="JavaScript">
var cursorX, cursorY, obj = null;
var moveAble = true;
function enableMove(index) {
	cursorX = event.clientX;
	cursorY = event.clientY;
	obj = index;
}
function img_border(obj) {
	obj.className = "img_border"
}
function no_img_border(obj) {
	obj.className = ""
}
function disableMove() { obj = null; }
function move_ie() {
	if (obj && moveAble) {
		DIV_name = obj.id;
		moveX = event.clientX;
		moveY = event.clientY;
		if (moveX > 550)
			moveX = 550;
		if (moveX < 10)
			moveX = 10;
		if (moveY > 480)
			moveY = 480;
		if (moveY < 10)
			moveY = 10;
		document.all[DIV_name].style.posLeft += moveX - cursorX;
		document.all[DIV_name].style.posTop += moveY - cursorY;
		cursorX = moveX;
		cursorY = moveY;
	}
}
</script>
<script language="JavaScript" type="text/JavaScript">
<!--
function MM_reloadPage(init) {  //reloads the window if Nav4 resized
	if (init == true) with (navigator) {
		if ((appName == "Netscape") && (parseInt(appVersion) == 4)) {
			document.MM_pgW = innerWidth; document.MM_pgH = innerHeight; onresize = MM_reloadPage;
		} 
	}
	else if (innerWidth != document.MM_pgW || innerHeight != document.MM_pgH) location.reload();
}
MM_reloadPage(true);
//-->
</script>
</head>
<body>
    <form name="RoomForm" id="RoomForm" runat="server">
    <span onmousemove="move_ie()" id="RoomSpan" style="z-index: 1; width: 100%; position: absolute;
        top: 0px; height: 600px; left: 1px;">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:Label ID="messageLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <input onclick="SavePos()" type="submit" name="Submit1" value="保存设置" id="Submit1"
                        onserverclick="Submit1_ServerClick" runat="server">
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    我的商品
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                        Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="商品图片">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%#getpic(DataBinder.Eval(Container.DataItem, "ProductPic").ToString())%>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProductName" HeaderText="商品名称">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="商品状态">
                                <ItemTemplate>
                                    <%#GetState(DataBinder.Eval(Container.DataItem,"ID").ToString()) %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CommandName='<%#GetLink(DataBinder.Eval(Container.DataItem,"ID").ToString()) %>'><%#GetLink(DataBinder.Eval(Container.DataItem,"ID").ToString()) %></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            无消息
                        </EmptyDataTemplate>
                    </ZL:ExGridView>
                </td>
            </tr>
        </table>
    </span>
    </form>
</body>
</html>
