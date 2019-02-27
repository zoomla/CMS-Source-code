<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="ChitChat.aspx.cs" Inherits="User_UserZone_ChitChat" %>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>蒙面聊</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="zone"></div>
    <div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li class="active">蒙面聊</li>
		<div class="clearfix"></div>
    </ol></div>
    <div class="container btn_green">
        <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
        <div class="us_topinfo" style="margin-top: 10px;">
            <iframe id="ifd" src="../../Zone/ChatStart.aspx?id=1" style="height:500px;z-index: 2; background: #fff; visibility: inherit; overflow: auto; overflow-x: hidden; width: 100%;" frameborder="0" tabid="1"></iframe>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
    </script>
</asp:Content>
