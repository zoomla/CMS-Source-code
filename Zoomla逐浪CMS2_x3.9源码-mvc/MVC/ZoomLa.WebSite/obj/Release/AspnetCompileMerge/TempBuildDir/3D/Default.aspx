<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS._3D.Default" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>3D在线商城</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
            <div id="mainDiv" style="background: url('/Images/style3D/bg.jpg') no-repeat; background-position: center; left: 0px; top: 0px; right: 0px; bottom: 0px; position: absolute; background-repeat: no-repeat; background-size: cover;">
            <div class="scenceDiv" style="left:50px;bottom:100px;">
                <img src="/Images/style3D/rainbowDoor.gif" style="width:200px;height:200px;" />
            </div>
        </div>
        <div class="bottom">
            <div id="loginDiv" class="bottomDiv">
                <input type="text" id="uname_T" placeholder="用户"/>
                <input type="password" id="upwd_T" placeholder="密码" onkeydown="IsEnter();"/>
                <input type="button" id="login_Btn" value="登录" onclick="Login();" "/>
            </div>
            <div id="logged_Div" class="bottomDiv" style="display:none;">
                <span id="welcome_Span"></span>
                <input type="button" id="exit_Btn" value="退出" onclick="location = '/User/Logout.aspx';"/>
            </div>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
     <style type="text/css">
        * {font-size:14px;font-family:'Microsoft YaHei';}
        /*由内部元素决定大小*/
        .shopDiv { position:fixed;cursor:pointer; }
        .shopTitle { width:100%;text-align:center;color:white; }
        .shopImg { }
        .scenceDiv {position:fixed;}
        .bottom {width:100%;height:40px;background:rgba(0,0,0,0.8);position:fixed;bottom:0px;}
        .bottom input[type="text"],.bottom input[type="password"]{padding-left:5px;border-radius:3px;}
        .bottomDiv {float:right;margin:8px 0 0px 10px; }
        #welcome_Span {color:white;margin-right:20px;}
    </style>
     <script type="text/javascript">
        $().ready(function () {
            Checklogin();
            //进入对应的店铺
            $(".shopDiv").click(function () {
                if (logFlag) {
                    var sid = $(this).attr("sid");
                    location = "TalkIndex.aspx?sid=" + sid;
                }
                else { alert("请先登录!!"); }
            });
        });//ready end;
        var shopHtml = "<div class='shopDiv' sid='{ID}' style='top:{posY}px;left:{posX}px' title='进入小店'> <div class='shopTitle' >{ShopName}</div>  <img src='{ShopImg}' /></div>";
        function ShopInit(json)//初始化显示
        {
            json = eval(json);
            for (var i = 0; i < json.length; i++) {
                console.lo
                var sHtml = shopHtml.replace("{ID}", json[i].ID).replace("{ShopName}", json[i].ShopName).replace("{ShopImg}", json[i].ShopImg).replace("{posY}", json[i].posY).replace("{posX}", json[i].posX);
                $("#mainDiv").append(sHtml);
            }
        }
        var logFlag = false;
        function IsEnter() {
            if (event.keyCode == 13) {
                Login();
                return false;
            }
        }
        function Checklogin()//用户是否登录
        {
            var uname = $("#uname_T").val();
            var upwd = $("#upwd_T").val();
            this.callBack = function (data) {
                if (data && data != "") {
                    $("#loginDiv").hide();
                    $("#welcome_Span").text(data + ",欢迎回来!!");
                    $("#logged_Div").show();
                    logFlag = true;
                }
            }
            PostToCS("CheckLogin", "", this.callBack);
        }
        function Login() {
            var uname = $("#uname_T").val();
            var upwd = $("#upwd_T").val();
            this.callBack = function (data) {
                if (data && data != "") {
                    $("#loginDiv").hide();
                    $("#welcome_Span").text(data + ",欢迎回来!!");
                    $("#logged_Div").show();
                    logFlag = true;
                }
                else {
                    alert("登录失败,用户名或密码有误!!!")
                }
            }
            PostToCS("Login", uname + ":" + upwd, this.callBack);
        }
        function PostToCS(a, v, callBack) {
            $.ajax({
                type: "Post",
                url: "/API/UserCheck.ashx",
                data: { action: a, value: v },
                success: function (data) { callBack(data); },
            })
        }
    </script>
</asp:Content>
