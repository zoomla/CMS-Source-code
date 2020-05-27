<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login_Ajax.aspx.cs" Inherits="ZoomLaCMS.login_Ajax" MasterPageFile="~/Common/Master/Empty.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link href="/App_Themes/User.css" rel="stylesheet" />
<style type="text/css">
.code{display:none;}      
#loginModal .close{ padding:5px; background:#ccc; border-radius:100%; font-size:inherit;}
#cart_login{ letter-spacing:20px;}
.login_body{ padding:30px; padding-top:10px;} 
.login_body li{ padding-top:10px; padding-bottom:10px;}
.login_body li .pull-left{ font-size:1.4em;}
.login_body li .pull-right{ font-size:1.2em; color:#f00;}
.login_body li .pull-right a{ color:#f00;}
.login_body li .fa-user{ padding-left:5px; padding-right:5px;}
.login_body li .fa-key{ padding-left:4px; padding-right:3px;} 
.login_body li .input-group{ width:100%;}
.login_body li .input-group-addon{ color:#999;} 
.login_body .CodeLi{ list-style:none; margin-left:30%;}
.login_body .CodeLi input{ width:70px;}
.login_body li .center-block{ width:100%;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="login_body">
            <ul class="list-unstyled">
                <li><span class="pull-left"><%:Call.SiteName%>会员</span>
                    <span class="pull-right"><i class="fa fa-chevron-circle-right"></i>
                        <a id="reg_a" href="/User/Register.aspx" target="_blank">立即注册</a></span></li>
                <li class="clearfix"></li>
                <li class="input-group">
                    <span class="input-group-addon"><i class="fa fa-user"></i></span>
                    <input type="text" id="uname_t" class="form-control text_max" placeholder="用户名/用户ID" data-enter="0"/>
                </li>
                <li class="input-group">
                    <span class="input-group-addon"><i class="fa fa-key"></i></span>
                    <input type="password" id="passwd_t" class="form-control text_max" placeholder="密码" data-enter="1"/>
                </li>
                <li id="code_li" style="height:32px;">
                    <input type="text" id="VCode" placeholder="验证码" maxlength="6" class="form-control text_x code" data-enter="2" />
                    <img id="VCode_img" title="点击刷新验证码" class="code"  style="height:34px;"/>
                    <input type="hidden" id="VCode_hid" name="VCode_hid" />
                </li>
                <li><input type="button" id="login_btn" onclick="Login();" value="登录" class="btn btn-primary center-block" data-enter="3"/></li>
            </ul>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script src="/JS/ZL_ValidateCode.js"></script>
    <script src="/JS/Controls/B_User.js"></script>
    <script src="/JS/Controls/Control.js"></script>
    <script>
        var buser = new B_User();
        $(function () {
            var rurl = parent.location.href.replace("://", "");
            rurl = rurl.substr(rurl.indexOf("/"), rurl.length - (rurl.indexOf("/")));
            $("#reg_a").attr("href", "/User/Register.aspx?ReturnUrl=" + rurl);
            Control.EnableEnter();
        })
        function Login() {
            var model = { name: $("#uname_t").val(), pwd: $("#passwd_t").val(), key: $("#VCode_hid").val(), code: $("#VCode").val() };
            if (model.name == "" || model.pwd == "") { alert("用户名或密码不能为空!"); return; }
            $("#login_btn").attr('disabled', 'disabled');
            $("#login_btn").val('登录中');
            buser.Login(model, function (data) {
                $("#login_btn").removeAttr('disabled');
                $("#login_btn").val('登录');
                if (data == -1) { alert("用户名或密码错误"); }
                else if (data == -10) {
                    alert("用户名或密码错误");
                    EnableCode();
                }
                else if (data == -2) { alert("验证码错误"); }
                else {
                    parent.LoginSuccess(model);
                }
            });
        }
        function EnableCode() {
            if ($(".code").is(":hidden")) {
                $(".code").show();
                $("#VCode_img").click();
                $("#VCode").ValidateCode();
            }
        }
    </script>
</asp:Content>
