<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuestShow.aspx.cs" Inherits="ZoomLaCMS.Guest.GuestShow" MasterPageFile="~/Common/Master/Empty.master" %>

<asp:content runat="server" contentplaceholderid="head"><title>留言列表</title></asp:content>
<asp:content runat="server" contentplaceholderid="Content">
<div id="ask_top">
    <div class="container">
        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <div class="ask_top_l">
                    <ul class="list-inline">
                        <li><a onclick="this.style.behavior='url(#default#homepage)';this.setHomePage('{$SiteURL/}');" href="javascript:;">设为首页</a></li>
                        <li><a href="javascript:;" id="dropdownMenu1" data-toggle="dropdown">官网频道<span class="caret"></span></a>
                            <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                                <%Call.Label("{ZL.Label id=\"输出根节点下一级栏目列表\" ShowNum=\"8\" /}");%>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <div class="ask_top_r">
                    <ul class="list-inline">
                        <li><i class="fa fa-graduation-cap"></i><a href="/Baike">百科</a></li>
                        <li><i class="fa fa-envelope"></i><a href="/Guest">留言</a></li>
                        <li><i class="fa fa-question-circle"></i><a href="/Ask">问答</a></li>
                        <li><i class="fa fa-paw"></i><a href="/Index">贴吧</a></li>
                        <li>
                            <div class="dropdown">
                                <a id="dLabel" data-target="#" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <asp:Label runat="server" ID="Label1"></asp:Label>
                                </a><span class="caret"></span></a>
            <ul class="dropdown-menu" role="menu" aria-labelledby="dLabel">
                <li role="presentation"><a role="menuitem" tabindex="-1" href="/User/" target="_blank">我的空间</a></li>
                <li role="presentation"><a role="menuitem" tabindex="-1" href="/User/" target="_blank">我的帖子</a></li>
                <li role="presentation"><a role="menuitem" tabindex="-1" href="/User/Content/MyFavori.aspx" target="_blank">我的喜欢</a></li>
                <li role="presentation"><a role="menuitem" tabindex="-1" href="/User/" target="_blank">我的关注</a></li>
                <li role="presentation"><a role="menuitem" tabindex="-1" href="/User/" target="_blank">我的投票</a></li>
                <li role="presentation"><a role="menuitem" tabindex="-1" href="/User/Info/UserBase.aspx" target="_blank">个人设置</a></li>
                <li role="presentation"><a role="menuitem" tabindex="-1" href="/user/Logout.aspx">安全退出</a></li>
            </ul>
                            </div>
                        </li>
                        <li><a href="/User/Login.aspx?ReturnUrl=/guest/" target="_blank">登录</a>|<a href="/User/Register.aspx?ReturnUrl=/Guest/" target="_blank">注册</a></li>
                    </ul>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
</div>
<div class="container">
    <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12 ask_logo">
        <a href="/Guest/Ask/Default.aspx">
            <img src='<%Call.Label("{$LogoUrl/}"); %>' alt="<%Call.Label("{$SiteName/}"); %>留言系统" /></a>
    </div>
    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-12 padding0">
        <div class="padding10" style="margin-top: 25px;">
            <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12"></div>
            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 padding0">
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                    <asp:ListItem>留言标题</asp:ListItem>
                    <asp:ListItem>留言ID</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12 padding0">
                <div class="input-group">
                    <asp:TextBox runat="server" ID="txtName" onmouseover="this.focus()" autocomplete="off" class="form-control" TabIndex="1"></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Button ID="Search_B" runat="server" Text="搜索" UseSubmitBehavior="false" CausesValidation="False" class="btn btn-primary" />
                    </span>
                </div>
            </div>
        </div>
    </div>
</div>
<div style="position: relative;">
    <div class="navbar navbar-default navbar-static-top" role="navigation" id="guest_nav">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"><span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button>
                <a class="navbar-brand" href="/Guest/Default.aspx">留言反馈</a>
            </div>
            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav">
                    <li class="active"><a href="Default.aspx" title="留言首页">留言首页</a></li>
                    <asp:Repeater ID="Cate_RPT" runat="server">
                        <ItemTemplate>
                            <li id='lmenu<%#Eval("CateID") %>'><a href="Default.aspx?CateID=<%#Eval("CateID") %>"><%#Eval("CateName") %></a> </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>
</div>
<div class="container">
    <ol class="breadcrumb">
        <li><a href="/">网站首页</a></li>
        <li><a href="/Guest/">留言中心</a></li>
        <li class="active">查看留言</li>
    </ol>
    <div class="s_body">
        <div id="g_show">
            <div class="gbook_title">
                <asp:Label runat="server" ID="GTitle_L"></asp:Label>
            </div>
            <ul class="list-unstyled" style="background-color: #FBFBFD;">
                <asp:Repeater ID="RPT" runat="server">
                    <ItemTemplate>
                        <li style="padding: 10px; border-bottom: 1px solid #ddd;min-height: 80px;">
                             <div style="position:absolute;"><img src="<%#Eval("UserFace") %>" style="width:60px;height:60px;" onerror="shownoface(this);" /></div>
                            <div class="g_show_txt" style="margin-left:80px;">
                                 <%# Eval("TContent")%>
                            </div>
                            <div class="r_gray text-right">
                                <%# GetUserName(Eval("UserID","{0}")) %>
                                <i class="fa fa-clock-o"></i><%# Eval("GDate")%>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div id="g_send" class="rg_inout">
            <div id="banAnony" class="alert alert-danger" role="alert" runat="server" visible="false">你好请先登录，再发布留言!<a href="/user/Login.aspx?ReturnUrl=/guest/" style="color: #133db6;">[点击登录]</a></div>
            <div id="replyDiv" runat="server">
                <table class="table table-bordered">
                    <tr>
                        <td colspan="2">
                            <textarea id="TxtContents" style="width: 100%; height: 200px;" name="TxtTContent" runat="server"></textarea>
                            <span id="sp2"></span>
                            <%=Call.GetUEditor("TxtContents") %>
                            <input type="hidden" id="txt_Config\" value="" />
                            <asp:TextBox ID="FilePicPath" runat="server" Text="fbangd" Style="display: none"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="VCode" MaxLength="6" CssClass="form-control text_x" style="display:inline-block;" runat="server" autocomplete="off" />
                            <img id="VCode_img" title="点击刷新验证码" class="code" style="height: 34px;" />
                            <input type="hidden" id="VCode_hid" name="VCode_hid" /><span id="sp1"></span>
                            <asp:Button ID="EBtnSubmit2" runat="server" OnClick="EBtnSubmit_Click"/>
                            <a href="javascript:;" onclick="ebtn_click();" class="btn btn-success"><i class="fa fa-pencil"></i>发表回复</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="ask_bottom">
        <p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
        <p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
    </div>
</div>
</asp:content>
<asp:content runat="server" contentplaceholderid="Script">
<link href="/App_Themes/Guest.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .gbook_title {
        padding: 5px;
        border-bottom: 2px solid #ddd;
        font-size: 16px;
        font-weight: bolder;
    }
</style>
<script src="/JS/ZL_ValidateCode.js"></script>
<script charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<script>
    $(function () {
        $("#VCode").ValidateCode();
    })
    function ebtn_click() {
        if (CheckDirty()) { $("#EBtnSubmit2").click(); }
    };
    function CheckDirty() {
        var value = Date.now;
        var TxtValidateCode = document.getElementById("VCode").value;

        if (value == "" || TxtValidateCode == "") {
            if (value == "") {
                var obj2 = document.getElementById("sp2");
                obj2.innerHTML = "<font color='red'>回复内容不能为空！</font>";
            }
            else {
                var obj2 = document.getElementById("sp2");
                obj2.innerHTML = "";
            }
            if (TxtValidateCode == "") {
                var obj3 = document.getElementById("sp1");
                obj3.innerHTML = "<font color='red'>验证码不能为空！</font>";
            } else {
                var obj3 = document.getElementById("sp1");
                obj3.innerHTML = "";
            }
            return false;
        }
        else {
            var obj = document.getElementById("sp2");
            obj.innerHTML = "";
            var obj3 = document.getElementById("sp1");
            obj3.innerHTML = "";
            return true;
        }
    }
    $("#Search_B").click(function () {
        var skey = $("#txtName").val().replace(/ /g, "");
        if (skey == "") { alert("不能为空"); return false; }
        location = "/Guest/Default.aspx?Skey=" + escape(skey);
    });
</script>
</asp:content>
