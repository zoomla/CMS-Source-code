<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FriendAdd.aspx.cs" Inherits="User_UserFriend_FriendAdd" EnableViewStateMac="false" %>
<%@ Register Src="../UserZone/WebUserControlTop.ascx" TagName="WebUserControlTop"  TagPrefix="uc2" %>
<%@ Register Src="WebUserControlMessage.ascx" TagName="WebUserControlMessage" TagPrefix="uc1" %>
<%@ Register Src="../UserFriend/WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc3" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>会员中心 >> 我的好友</title>
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
<div class="s_bright">

	<div class="us_topinfo">
<a title="会员中心" href="/User/Default.aspx" target="_parent">会员中心</a>&gt;&gt;<a title="我的好友" href="/User/UserFriend/Index.aspx">我的好友</a>&gt;&gt;好友管理
	</div>
    <uc2:WebUserControlTop ID="WebUserControlTop2" runat="server" />
    <uc3:WebUserControlTop ID="WebUserControlTop1" runat="server" />
    <div class="us_topinfo" style="margin-top:10px;">
    <ul>
        <li style="width:120px; float:left;line-height:30px; text-align:right;">用户ID：</li>
        <li style="width:70%;line-height:30px">
            <asp:TextBox ID="Nametxt" runat="server" Text='' Width="35%" MaxLength="30"></asp:TextBox>
            <span><font color="red">*</font></span>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2"  runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="Nametxt">名称必填</asp:RequiredFieldValidator>
        </li>
        <li style="width:120px; float:left;line-height:30px; text-align:right;">选择分组：</li>
        <li style="width:70%;line-height:30px">
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
        </li>
        <li >
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="提交" />
            <input id="Button2" type="button" value="返  回" OnClick="javascript:history.go(-1)"/>
        </li>
    </ul>
    </div>
</div>
</form>
</body>
</html>