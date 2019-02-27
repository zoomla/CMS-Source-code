<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZoneEditApply.aspx.cs" Inherits="User_UserZone_ZoneEditApply" %>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>我的空间</title>
<link href="../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script language="javascript">
    function CheckAll(spanChk)//CheckBox全选
    {
    var oItem = spanChk.children;
    var theBox=(spanChk.type=="checkbox")?spanChk:spanChk.children.item[0];
    xState=theBox.checked;
    elm=theBox.form.elements;
    for(i=0;i<elm.length;i++)
    if(elm[i].type=="checkbox" && elm[i].id!=theBox.id)
    {
        if(elm[i].checked!=xState)
        elm[i].click();
    }
    }
</script>
</head>
<body>
<form id="form1" runat="server">
<div style="margin:auto; width:100%">
<div class="us_topinfo">
<a title="会员中心" href="/User/Default.aspx" target="_parent">会员中心</a>&gt;&gt;<a title="我的空间" href="/User/Userzone/Default.aspx">我的空间</a>
</div>
<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" />
<div class="us_topinfo" style="margin-top: 10px;">修改开通空间信息</div>
<div class="us_topinfo" style="margin-top: 10px;">
	<ul class="us_seta" style="margin-top: 10px;" id="Auditing" runat="server" visible="false">
		<li style="text-align: center">
			<br />
			<br />
			<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
			<a href=""></a>
			<br />
			<br />
			<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">修改我提交的信息!</asp:LinkButton><br />
			<br />
			<br />
		</li>
	</ul>
	<ul class="us_seta" style="margin-top: 10px;" id="add" runat="server">
		<li style="width: 120px; float: left; line-height: 30px; text-align: right;">空间名称： </li>
		<li style="width: 70%; line-height: 30px">
			<asp:TextBox ID="Nametxt" runat="server" Text='' Width="35%" MaxLength="30"></asp:TextBox>
			<span><font color="red">*</font></span>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="Nametxt">名称必填</asp:RequiredFieldValidator></li>
		<li style="width: 120px; float: left; line-height: 30px; text-align: right">空间简介： </li>
		<li style="width: 70%; line-height: 30px">
			<textarea id="textareacontent" style="width: 294px; height: 138px" runat="server"></textarea></li>
		<li style="width: 120px; float: left; line-height: 30px; text-align: right"></li>
		<li>
			<asp:Button ID="EBtnSubmit" Text="信息提交" runat="server" OnClick="EBtnSubmit_Click" />
			&nbsp;
			<input id="Button1" type="button" value="返  回" onclick="javascript:history.go(-1)" />
			<asp:HiddenField ID="HiddenField1" runat="server" />
		</li>
		</ul>
</div>
</div>
</form>
</body>
</html>