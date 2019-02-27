<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="User_PromotUnion_Default" EnableViewStateMac="false" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>推广联盟</title> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="promot"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">推广赚钱</li> 
    </ol>
</div>
<div class="container">
    <div class="infodiv col-lg-6 col-md-6 col-sm-6 col-xs-6">
        <div>
            <div>
                <div style="text-align: center">通过链接邀请好友注册</div>
                <div style="text-align: center">--把包含您ID的链接发给别人即可赚钱</div>
            </div>
            <div style="clear: both; height: 1px; overflow: hidden;"></div>
        </div>
        <div>
            --把包含您ID 的链接发在博客、论坛等地方，每邀请1位朋友成功注册返利网，您可以获得最高3元奖励！
        </div>
        <div><a href="linkUnion.aspx">获得您的推广联盟</a></div>
    </div>
    <div class="infodiv col-lg-6 col-md-6 col-sm-6 col-xs-6">
        <div>
            <div>
                <div style="text-align: center">“红包函”邀好友注册</div>
                <div style="text-align: center">--您和好友双方都可以获得现金奖励。</div>
            </div>
            <div style="clear: both; height: 1px; overflow: hidden;"></div>
        </div>
        <div>
            通过发送“红包函”到朋友邮箱，每邀请1位朋友注册成功，您可以获得最高3元奖励，好友更可获得5元奖金。
        </div>
        <div><a href="Redindulgence.aspx">获得您的推广链接</a></div>
    </div>
    <div class="infodiv col-lg-6 col-md-6 col-sm-6 col-xs-6">
        <div>
            <div>
                <div style="text-align: center">推广商品赚钱</div>
                <div style="text-align: center">--把商品推广链接发给别人即可赚钱。</div>
            </div>
            <div style="clear: both; height: 1px; overflow: hidden;"></div>
        </div>
        <div>在博客、论坛、QQ、MSN、邮件、校内、开心等地方，发京东商城、1号店超市、99书城等商城的商品推广链接，有人通过该链接成功购物，您就能获得此订单相应的返利！</div>
        <div><a href="Userunionprolink.aspx">获得您的推广链接</a></div>
    </div>
    <div class="infodiv col-lg-6 col-md-6 col-sm-6 col-xs-6">
        <div>
            <div>
                <div style="text-align: center">推广商城赚钱</div>
                <div style="text-align: center">--把商城推广链接发给别人即可赚钱。</div>
            </div>
            <div style="clear: both; height: 1px; overflow: hidden;"></div>
        </div>
        <div>
            您可以在博客、论坛、QQ、MSN、邮件、校内、开心等地方，发京东商城、1号店超市、99书城商城的推广链接，有人通过该链接成功购物，您就能获得相应返利！
        </div>
        <div><a href="Userunionshop.aspx">获得您的推广链接</a></div>
    </div>
    <div class="infodiv col-lg-6 col-md-6 col-sm-6 col-xs-6">
        <div>
            <div>
                <div style="text-align: center">推广活动赚钱</div>
                <div style="text-align: center">--把商城推广链接发给别人即可赚钱。</div>
            </div>
            <div style="clear: both; height: 1px; overflow: hidden;"></div>
        </div>
        <div>在博客、论坛、QQ、MSN、邮件、校内、开心等地方，发京东商城、1号店超市、99书城等商城活动页面的推广链接，有人通过该链接成功购物，您就能获得相应奖励。</div>
        <div><a href="Userunionactive.aspx">获得您的推广链接</a></div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .infodiv {margin-top:20px;padding:10px;}
    </style>
</asp:Content>
