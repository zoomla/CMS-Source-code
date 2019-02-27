<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Spec.aspx.cs" Inherits="Plat_Blog_Spec" MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>话题</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
<header><h2>星标话题</h2><hr/></header>
<div class="rptdiv" id="star_ul"></div>
<header><h2>我的话题</h2><hr/></header>
<div class="rptdiv" id="me_ul"></div>
<header><h2>其它话题</h2><hr/></header>
<div class="rptdiv" id="system_ul"></div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
#wait_div {text-align:center;padding-top:30px;}
.rptdiv {min-height:220px;}
</style>
<script>
var waitdiv = '<div id="wait_div"><i class="fa fa-spinner fa-spin" style="font-size:80px;"></i></div>';
var topic = {
    star: { page: 1, psize: 9, filter: "star", $body: $("#star_ul") },
    system: { page: 1, psize: 9, filter: "system", $body: $("#system_ul") },
    me: { page: 1, psize: 9, filter: "me", $body: $("#me_ul") },
    sel: function (conf) {
        var url = "/Plat/Blog/SpecBody.aspx";
        var param = $.extend({}, conf);
        delete param.$body;
        conf.$body.html("").append(waitdiv);
        $.post(url, param, function (data) { conf.$body.html("").append(data); })
    },
    load: function (filter, query, page) {
        var url = "/Plat/Blog/SpecBody.aspx";
        var conf = topic[filter]; conf.page = page;
        topic.sel(conf);
        //var param = $.extend({}, conf);
        //delete param.$body;
        //$.post(url, param, function (data) {
        //    conf.$body.html("").append(data);
        //})
        //location = location.href.split("#")[0] + "#" + filter + "_anchor";
    },
    init: function () {
        topic.sel(topic.star);
        topic.sel(topic.me);
        topic.sel(topic.system);
    }
};
$(function () {
    topic.init();
    setactive("话题");
})
</script>
</asp:Content>