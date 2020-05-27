<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Guest.Default"  MasterPageFile="~/Common/Master/Empty.master"  %>

<asp:content runat="server" contentplaceholderid="head">
<script charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<link href="/App_Themes/Guest.css" rel="stylesheet" type="text/css" />
<title>留言中心首页</title>
</asp:content>
<asp:content runat="server" contentplaceholderid="Content">
    <div id="ask_top">
        <div class="container">
            <div class="row">
                <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                    <div class="ask_top_l">
                        <ul class="list-inline">
                            <li><a onclick="this.style.behavior='url(#default#homepage)';this.setHomePage('<%:ZoomLa.Components.SiteConfig.SiteInfo.SiteUrl %>');" href="javascript:;">设为首页</a></li>
                            <li><a href="javascript:;" type="button" id="dropdownMenu1" data-toggle="dropdown">官网频道<span class="caret"></span></a>
                                <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                                    <%Call.Label("{ZL.Label id=\"输出根节点下一级栏目列表\" ShowNum=\"8\" /}");%>
                                </ul>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                    <div class="ask_top_r">
                        <ul class="list-inline">
                            <li><i class="fa fa-graduation-cap"></i><a href="/Baike">百科</a></li>
                            <li><i class="fa fa-envelope"></i><a href="/Guest">留言</a></li>
                            <li><i class="fa fa-question-circle"></i><a href="/Ask">问答</a></li>
                            <li><i class="fa fa-paw"></i><a href="/Index">贴吧</a></li>
                            <li>
                                <div class="dropdown">
                                    <a id="dLabel" data-target="#" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        <asp:Label runat="server" ID="user"></asp:Label>
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
            <a href="/"> <img src='<%:ZoomLa.Components.SiteConfig.SiteInfo.LogoUrl %>' /></a>
        </div>
        <div class="col-lg-7 col-md-7 col-sm-7 col-xs-12 padding0">
            <div class="padding10" style="margin-top: 25px;">
                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12"></div>
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 padding0">
                    <asp:DropDownList ID="Gtype" runat="server" CssClass="form-control">
                        <asp:ListItem>留言标题</asp:ListItem>
                        <asp:ListItem>留言ID</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12 padding0">
                    <div class="input-group">
                        <asp:TextBox runat="server" ID="txtName" autocomplete="off" class="form-control" TabIndex="1"></asp:TextBox>
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
                        <li id="lmenu0"><a href="Default.aspx" title="留言首页">留言首页</a></li>
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
            <li><a href="Default.aspx">留言中心</a></li>
            <li><a href="Default.aspx">留言板</a></li>
            <li class="active">
                <asp:Literal ID="LitCate" runat="server"></asp:Literal>
            </li>
        </ol>
    </div>



    <div class="container">
        <table id="EGV" class="table table-striped table-bordered table-hover guest_hometable">
            <tr>
                <td style="width:40px;"></td>
                <td style="width:80%;">版块主题</td>
                <td style="width:30px;"></td>
                <td></td>
            </tr>
            <asp:Repeater runat="server" ID="RPT" EnableTheming="false">
                <ItemTemplate>
                    <tr ondblclick="location='GuestShow.aspx?GID=<%# Eval("GID")%>';">
                        <td class="text-center"><i class="fa fa-file" style="color:#0066cc"></i></td>
                        <td>
                            <a href="/Guest/Default.aspx?CateID=<%# Eval("CateID")%>" style="color:#007CD5;">[<%#Eval("CateName")%>]</a>
                            <a href="GuestShow.aspx?GID=<%# Eval("GID")%>" class="post_title" title="点击浏览"><%# GetTitle()%></a></td>
                        <td>
                            <img src="<%#Eval("UserFace") %>" onerror="shownoface(this);" class="img_xs" />
                        </td>
                        <td>
                            <div class="r_gray"><%#GetUName() %></div>
                            <div class="r_gray"><%#Eval("GDate", "{0:yyyy-MM-dd hh:mm}")%></div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <asp:Literal ID="Page_Lit" runat="server"></asp:Literal>
    </div>
    <div class="container">
        <div id="banAnony" class="alert alert-danger" role="alert" runat="server" visible="false">你好请先登录，再发布留言!<a href="/user/Login.aspx?ReturnUrl=/guest/" style="color: #133db6;">[点击登录]</a></div>
        <div id="userDiv" runat="server" class="margin_t10">
            <table class="table table-bordered table-striped">
                <tr><td><i class="fa fa-pencil"></i> 发布信息</td></tr>
                <tr>
                    <td>
                        <div class="input-group" style="width:505px;">
                            <asp:DropDownList runat="server" ID="Cate_DP" DataTextField="CateName" DataValueField="CateID" CssClass="form-control text_x" style="border-right:none;"></asp:DropDownList>
                            <asp:TextBox ID="Title_T" runat="server" MaxLength="100" class="form-control text_300" placeholder="请输入标题" />
                        </div>
                        <asp:RequiredFieldValidator runat="server" ID="R1" ForeColor="Red" ErrorMessage="标题不能为空" Display="Dynamic" ControlToValidate="Title_T" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <textarea runat="server" id="TxtTContent" style="height: 200px; width: 100%;"></textarea>
                        <%=Call.GetUEditor("TxtTContent") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="VCode" MaxLength="6" runat="server" CssClass="form-control text_x" style="display:inline-block;" placeholder="请输入验证码" autocomplete="off" />
                        <img id="VCode_img" title="点击刷新验证码" class="code" style="height: 34px;" />
                        <input type="hidden" id="VCode_hid" name="VCode_hid" />
                        <asp:Button ID="EBtnSubmit2" Style="display: none;" OnClick="EBtnSubmit_Click" CssClass="btn btn-primary" runat="server" />
                        <a href="javascript:;" onclick="ebtn_click();" class="btn btn-success"><i class="fa fa-pencil"></i> 提交留言</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="addDiv" runat="server" class="container text-center margin_t5">
        <a href="Default.aspx?CateID=1" class="btn btn-success btn-lg"><i class="fa fa-pencil"></i>发表留言</a>
    </div>
    <div class="ask_bottom">
        <p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
        <p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
    </div>
</asp:content>
<asp:content runat="server" contentplaceholderid="Script">
<style type="text/css">
    #EGV tr > td {
        border-right: none;
        border-left: none;
    }

    #EGV .post_title {
        margin-left: 5px;
        color: #666;
        font-size: 14px;
    }

        #EGV .post_title:hover {
            color: #007CD5;
        }
</style>
<script src="/JS/ZL_ValidateCode.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script>
    $(function () {
        $("#VCode").ValidateCode();
    })
    function ebtn_click() {
        var txt = UE.getEditor("TxtTContent").getContentTxt();
        if (ZL_Regex.isEmpty(txt)) { alert("内容不能为空"); return false; }
        $("#EBtnSubmit2").click();
    }
</script>
</asp:content>
