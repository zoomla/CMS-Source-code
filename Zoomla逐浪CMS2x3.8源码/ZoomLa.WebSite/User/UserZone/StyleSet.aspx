<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="StyleSet.aspx.cs" Inherits="User_UserZone_StyleSet" %>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server"><title>我的空间</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="zone"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li class="active">设定空间模板</li>
        </ol>
    </div>
    <div class="container btn_green">
        <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
    </div>
    <div class="container" runat="server" id="main_div">
        <div class="alert alert-success">
           当前模板:<asp:Label ID="StyleNameLB" runat="server"></asp:Label>
        </div>
        <ul class="list-ustyled" id="tlp_ul">
            <asp:Repeater runat="server" ID="RPT" OnItemCommand="RPT_ItemCommand">
                <ItemTemplate>
                    <li>
                        <img src="<%#Eval("StylePic") %>"  class="tlpimg" onerror="shownopic(this);" />
                        <div class="tlp_opdiv">
                            <strong class="r_gray"><%#Eval("StyleName") %></strong>
                            <asp:LinkButton runat="server" CssClass="btn btn-xs btn-info pull-right" CommandName="apply" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要应用此模板吗,将会覆盖你原有配置');"><i class="fa fa-adjust"></i> 应用</asp:LinkButton>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
#tlp_ul {}
#tlp_ul li {float:left;margin-left:10px;margin-bottom:10px;}
#tlp_ul .tlp_opdiv {margin-top:5px;}
#tlp_ul .tlpimg {display:inline-block;height:142px;width:182px;border:1px solid #ddd;padding:3px;}
</style>
</asp:Content>