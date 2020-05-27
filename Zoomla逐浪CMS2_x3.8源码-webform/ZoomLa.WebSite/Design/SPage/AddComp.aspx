<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddComp.aspx.cs" Inherits="Design_SPage_AddComp" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>添加组件</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="list-group">
    <a href="javascript:;" class="list-group-item" onclick="add('carousel')">
        <div class="left">
            <i class="fa fa-image"></i>
        </div>
        <div class="middle">
            <strong>图片轮播</strong>
            <div>将多张广告图片以滚动轮播的方式进行动态展示，可调整轮播显示高度</div>
        </div>
    </a>
    <a href="javascript:;" class="list-group-item" onclick="add('content')">
        <div class="left">
            <i class="fa fa-file-text-o"></i>
        </div>
        <div class="middle">
            <strong>自定义内容</strong>
            <div>您可以通过在编辑器输入文字/图片的形式自定义编辑内容</div>
        </div>
    </a>
    <a href="javascript:;" class="list-group-item" onclick="add('nav')">
        <div class="left">
            <i class="fa fa-list"></i>
        </div>
        <div class="middle">
            <strong>菜单栏</strong>
            <div>网站链接导航</div>
        </div>
    </a>
  <%--  <a href="javascript:;" class="list-group-item" onclick="add('qrcode')">
        <div class="left">
            <i class="fa fa-list"></i>
        </div>
        <div class="middle">
            <strong>无线二维码</strong>
            <div>无线手机店铺二维码展示位，公告栏</div>
        </div>
    </a>--%>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
.list-group-item {height:100px;border-top: none; border-left: none; border-right: none;}
.list-group-item .left {background-color:#ECECEC;width:80px;height:80px;padding-top:15px;text-align:center;position:absolute;}
.list-group-item .left .fa {color: #9C9C9C; font-size: 50px;}
.list-group-item .middle {margin-left:100px;}
</style>
<%--<script src="/JS/Plugs/angular.min.js"></script>--%>
<script>
    function add(type) {
        parent.scope.Comp.add(type);
        parent.closeDiag();
    }
</script>
</asp:Content>