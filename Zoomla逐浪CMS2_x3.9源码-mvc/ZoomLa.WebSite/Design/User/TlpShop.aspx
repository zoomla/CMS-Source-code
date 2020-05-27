<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TlpShop.aspx.cs" Inherits="ZoomLaCMS.Design.User.TlpShop"  MasterPageFile="~/Design/Master/User.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>模板列表</title>
<link href="/design/res/css/user.css" rel="stylesheet" />
<link href="/App_Themes/User.css" rel="stylesheet" />
<script src="/JS/ICMS/ZL_Common.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ul class="list-unstyled" id="tlp_ul" style="min-height:500px;">
<asp:Repeater runat="server" ID="RPT" OnItemCommand="RPT_ItemCommand">
<ItemTemplate>
<li>
    <img class="tlpimg" src="<%#Eval("PreviewImg") %>" onerror="this.src=null;this.src='/Images/nopic.gif';" />
    <div class="tlp_opdiv">
        <strong class="r_gray"><%#Eval("TlpName") %></strong>
        <span class="r_gray">价格:<span class="r_red"><%#GetPrice() %></span></span>
        <div>
            <a href="/Design/Preview.aspx?TlpID=<%#Eval("ID") %>" class="btn btn-xs btn-info" target="_blank"><i class="fa fa-search"></i> 浏览</a>
            <asp:LinkButton runat="server" CssClass="btn btn-xs btn-info" CommandName="apply" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要应用此模板吗,将会覆盖你原有配置');"><i class="fa fa-adjust"></i> 应用</asp:LinkButton>
        </div>
    </div>
</li>
</ItemTemplate>
</asp:Repeater>
<li class="more" title="更多模板" onclick="window.open('/Class_2/Default.aspx');">
    <i class="fa fa-files-o r_gray"></i>
    <div class="text-center r_gray margin_t10">更多模板</div>
</li>
</ul>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
#tlp_ul .tlpimg {display:inline-block;height:142px;width:182px;border:1px solid #ddd;padding:3px;}
#tlp_ul .more {border:2px solid #ddd;width:182px;height:142px;padding-top:15px;cursor:pointer;text-align:center;}
#tlp_ul .more i {font-size:80px;}
#tlp_ul .more div {font-size:16px;}
</style>
</asp:Content>