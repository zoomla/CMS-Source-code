<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="se_conf.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.se_conf" MasterPageFile="~/Design/Master/Edit.master" %>

<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
    <style>
        #Tabs1 table { width: 60%;float: left; }
        .eraser_tbody .setting-row {padding: 0;}
        .img_div {width: 40%;float: right;height: 100%;text-align: center;}
        .img_div img { max-height: 80%;width: 35%; margin: 5px auto;}
        .foot_div {text-align: center;margin-top: -5px;}
        .r_green {margin-left: 5px;color: green;font-size: 12px;}
    </style>
    <title>场景设置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div ng-controller="ZLCtrl">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#Tabs0" data-toggle="tab">基本设置</a></li>
            <li><a href="#Tabs1" data-toggle="tab">特效设置</a></li>
        </ul>
        <div class="tab-content padding0">
            <div id="Tabs0" class="tab-pane active manage_content">
                <table class="table table-bordered">
                    <tr>
                        <td class="td_md">页面方向</td>
                        <td>
                            <label ng-repeat="item in directArr">
                                <input type="radio" name="direction_rad" value="{{item.direction}}" ng-model="conf.direction" />{{item.name}}</label>
                        </td>
                    </tr>
                    <tr>
                        <td>屏幕方向提示</td>
                        <td>
                            <label ng-repeat="item in screenArr">
                                <input type="radio" name="screen_rad" value="{{item.screen}}" ng-model="conf.screen" />{{item.name}}</label>
                        </td>
                    </tr>
                    <tr>
                        <td>页面切换</td>
                        <td>
                            <label ng-repeat="item in effectArr">
                                <input type="radio" name="effect_rad" value="{{item.effect}}" ng-model="conf.effect" />{{item.name}}</label>
                        </td>
                    </tr>
                    <tr>
                        <td>自动播放</td>
                        <td>
                            <input type="text" class="form-control text_150" ng-model="conf.autoplay" /><span class="r_green">0为不自动播放,单位:毫秒</span></td>
                    </tr>
                    <tr>
                        <td>循环播放</td>
                        <td>
                            <input type="checkbox" ng-model="conf.loop" /></td>
                    </tr>
                    <tr>
                        <td>音乐自动播放</td>
                        <td>
                            <input type="checkbox" ng-model="conf.automusic" /></td>
                    </tr>
                    <tr>
                        <td>浏览密码</td>
                        <td>
                            <asp:TextBox runat="server" TextMode="Password" ID="pwd_t" class="form-control text_150" /><span class="r_green">其他用户是否输入密码后才可访问</span></td>
                    </tr>
                </table>
            </div>
            <div id="Tabs1" class="tab-pane">
                <table class="table table-bordered">
                    <tr>
                        <td>
                            <label>涂抹特效</label></td>
                        <td>
                            <input type="checkbox" ng-model="eraser.able" id="eraser_chk" /></td>
                    </tr>
                    <tbody class="eraser_tbody">
                        <tr>
                            <td>覆盖效果</td>
                            <td>
                                <select id="eraser_sel" ng-model="eraser.src" class="form-control text_200">
                                    <option selected="selected" value="/design/res/mbh5/eraser/waterdrop.jpg">水珠</option>
                                    <option value="/design/res/mbh5/eraser/board.jpg">木板</option>
                                    <option value="/design/res/mbh5/eraser/rain.jpg">雨滴</option>
                                    <option value="/design/res/mbh5/eraser/nebula.jpg">星云</option>
                                    <option value="/design/res/mbh5/eraser/scenery.jpg">风景</option>
                                    <option value="/design/res/mbh5/eraser/vague.jpg">模糊</option>
                                    <option value="/design/res/mbh5/eraser/heaven.jpg">天空</option>
                                    <option value="/design/res/mbh5/eraser/colorful.jpg">炫彩</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>透明度</td>
                            <td>
                                <div class="setting-row">
                                    <div id="opacity_slider" class="slider_min"></div>
                                    <input type="text" id="opacity_t" class="inputer min" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>涂抹比例</td>
                            <td>
                                <div class="setting-row">
                                    <div id="progscale_slider" class="slider_min"></div>
                                    <input type="text" id="progscale_t" class="inputer min" />
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="img_div">
                    <img src="{{eraser.src}}" />
                </div>
            </div>
        </div>
        <div class="foot_div">
            <input type="button" value="保存" class="btn btn-info" ng-click="save();" />
            <input type="button" value="关闭" class="btn btn-default" onclick="CloseSelf();" />
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script src="/dist/js/bootstrap-switch.js"></script>
    <script src="/JS/Plugs/angular.min.js"></script>
    <script>
        angular.module("app", []).controller("ZLCtrl", function ($scope) {
            $scope.directArr = [{ direction: "vertical", name: "垂直" }, { direction: "horizontal", name: "横向" }];
            $scope.screenArr = [{ name: "不提示", screen: "none" }, { name: "推荐垂直", screen: "vertical" }, { name: "推荐横向", screen: "cross" }];
            $scope.effectArr = [{ effect: "slide", name: "滑动" }, { effect: "fade", name: "渐隐" }, { effect: "cube", name: "立方" }, { effect: "coverflow", name: "封面" }, { effect: "flip", name: "翻转" }]
            $scope.conf = top.scence.conf;
            if (typeof ($scope.conf.automusic) == "undefined") { $scope.conf.automusic = true; }
            //-------------------------------------------
            $scope.eraser = { src: '/design/res/mbh5/eraser/waterdrop.jpg', opacity: 0.3, prog: 30, able: false };
            if (typeof (top.page.eraser.data) != 'undefined') {
                $scope.eraser = top.page.eraser.data;
            }
            //涂抹特效数据
            var opacity = $scope.eraser.opacity;
            var prog = $scope.eraser.prog;
            $("#opacity_t").val(opacity * 100);
            $("#progscale_t").val(prog);
            $(".img_div img").css("opacity", opacity);
            $("#opacity_slider").slider({
                range: "min", min: 1, max: 100, value: opacity * 100,
                slide: function (event, ui) {
                    $("#opacity_t").val(ui.value);
                    $scope.eraser.opacity = ui.value / 100;
                    $(".img_div img").css("opacity", ui.value / 100);
                }
            });
            $("#progscale_slider").slider({
                range: "min", min: 1, max: 100, value: prog,
                slide: function (event, ui) {
                    $("#progscale_t").val(ui.value);
                    $scope.eraser.prog = ui.value;
                }
            });
            //是否可以angular绑定
            $("#eraser_sel").change(function () {
                $(".img_div img").attr("src", $(this).val());
            });
            $scope.save = function () {
                $.post("", { pwd: $("#pwd_t").val(), action: "update" }, function (data) { })
                top.page.eraser.set($scope.eraser);
                CloseSelf();
            }
        });
    </script>
</asp:Content>
