<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="animate.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.Common.animate" MasterPageFile="~/Design/Master/Edit.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>动画效果</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped" ng-controller="appCtrl">
    <tr><td>是否启用</td><td><input type="checkbox" ng-model="animate.enabled" /></td></tr>
    <tr><td style="width:120px;">动画效果</td><td>
    <select ng-model="animate.effect" ng-change="play();">
        <optgroup label="强调显示">
          <option value="bounce">反弹</option>
          <option value="flash">闪烁</option>
          <option value="pulse">波动</option>
          <option value="rubberBand">伸缩</option>
          <option value="shake">摇动</option>
          <option value="swing">摆动</option>
          <option value="tada">抖动</option>
          <option value="wobble">摇晃</option>
        </optgroup>
        <optgroup label="弹入">
          <option value="bounceIn">弹入</option>
          <option value="bounceInDown">从上弹入</option>
          <option value="bounceInLeft">从左弹入</option>
          <option value="bounceInRight">从右弹入</option>
          <option value="bounceInUp">从下弹入</option>
        </optgroup>
        <optgroup label="弹出">
          <option value="bounceOut">弹出</option>
          <option value="bounceOutDown">向下弹出</option>
          <option value="bounceOutLeft">向左弹出</option>
          <option value="bounceOutRight">向右弹出</option>
          <option value="bounceOutUp">向上弹出</option>
        </optgroup>
        <optgroup label="淡入">
          <option value="fadeIn">淡入</option>
          <option value="fadeInDown">从上淡入</option>
          <option value="fadeInDownBig">从上淡入(强)</option>
          <option value="fadeInLeft">从左淡入</option>
          <option value="fadeInLeftBig">从左淡入(强)</option>
          <option value="fadeInRight">从右淡入</option>
          <option value="fadeInRightBig">从右淡入(强)</option>
          <option value="fadeInUp">从下淡入</option>
          <option value="fadeInUpBig">从下淡入(强)</option>
        </optgroup>
        <optgroup label="淡出">
          <option value="fadeOut">淡出</option>
          <option value="fadeOutDown">向下淡出</option>
          <option value="fadeOutDownBig">向下淡出(强)</option>
          <option value="fadeOutLeft">向左淡出</option>
          <option value="fadeOutLeftBig">向左淡出(强)</option>
          <option value="fadeOutRight">向右淡出</option>
          <option value="fadeOutRightBig">向右淡出(强)</option>
          <option value="fadeOutUp">向上淡出</option>
          <option value="fadeOutUpBig">向上淡出(强)</option>
        </optgroup>
        <optgroup label="翻转">
          <option value="flip">翻转</option>
          <option value="flipInX">垂直转入</option>
          <option value="flipInY">水平转入</option>
          <option value="flipOutX">垂直转出</option>
          <option value="flipOutY">垂直转入</option>
        </optgroup>
        <optgroup label="滑动">
          <option value="lightSpeedIn">滑入</option>
          <option value="lightSpeedOut">滑出</option>
        </optgroup>
        <optgroup label="旋进">
          <option value="rotateIn">旋进</option>
          <option value="rotateInDownLeft">左上旋入</option>
          <option value="rotateInDownRight">右上旋入</option>
          <option value="rotateInUpLeft">左下旋入</option>
          <option value="rotateInUpRight">右下旋入</option>
        </optgroup>
        <optgroup label="旋出">
          <option value="rotateOut">旋出</option>
          <option value="rotateOutDownLeft">左下旋出</option>
          <option value="rotateOutDownRight">右下旋出</option>
          <option value="rotateOutUpLeft">左上旋出</option>
          <option value="rotateOutUpRight">右上旋出</option>
        </optgroup>
        <optgroup label="滑入">
          <option value="slideInUp">从下滑入</option>
          <option value="slideInDown">从上滑入</option>
          <option value="slideInLeft">从左滑入</option>
          <option value="slideInRight">从右滑入</option>
        </optgroup>
        <optgroup label="滑出">
          <option value="slideOutUp">向上滑出</option>
          <option value="slideOutDown">向下滑出</option>
          <option value="slideOutLeft">向左滑出</option>
          <option value="slideOutRight">向右滑出</option>
        </optgroup>
        <optgroup label="放大">
          <option value="zoomIn">放大</option>
          <option value="zoomInDown">从上放大</option>
          <option value="zoomInLeft">从左放大</option>
          <option value="zoomInRight">从右放大</option>
          <option value="zoomInUp">从下放大</option>
        </optgroup>
        <optgroup label="缩小">
          <option value="zoomOut">缩出</option>
          <option value="zoomOutDown">向下缩出</option>
          <option value="zoomOutLeft">向左缩出</option>
          <option value="zoomOutRight">向右缩出</option>
          <option value="zoomOutUp">向上缩出</option>
        </optgroup>
        <optgroup label="其它">
          <option value="hinge">跌落</option>
          <option value="rollIn">转入</option>
          <option value="rollOut">转出</option>
        </optgroup>
      </select>
   </td></tr>
    <tr><td>持续时间</td><td><input type="text" ng-model="animate.duration" /></td></tr>
    <tr><td>延迟时间</td><td><input type="text" ng-model="animate.delay" /></td></tr>
    <tr><td>执行次数</td><td><input type="text" ng-model="animate.count" /><span>0为循环执行</span></td></tr>
    <tr><td></td><td colspan="2">
        <input type="button" value="确定保存" class="btn btn-info" ng-click="save();" />
        <input type="button" value="清除动画" class="btn btn-danger" ng-click="clear();" />
        </td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/Plugs/angular.min.js"></script>
<script>
angular.module("app", []).controller("appCtrl", function ($scope, $compile) {
    $scope.animate = editor.model.config.animate;
    //$scope.effect_sel = "";
    if (!$scope.animate) {
        //后期扩展以数组形式,多个动画播放
        $scope.animate = {
            enabled: true,//是否使用动画效果
            duration: 1,//持续几秒
            delay: 0,//延迟几秒执行
            effect: "bounce",//动画效果,以css的方式加入目标元素
            count: 1,//执行次数,0为循环
        };
    }
    if (!$scope.animate.count) { $scope.animate.count = 1; }
    $scope.save = function () {
        //--清除已动画效果
        var css = editor.model.instance.attr("swiper-animate-effect");
        editor.model.instance.removeClass("animated").removeClass(css);
        //--保存新动画效果
        editor.model.config.animate = $scope.animate;
        editor.model.SetAnimate();
        ////必须预览一次,否则效果会丢失
        //if (top.scence) { top.scence.preview(); }
        NotifyUpdate();
        CloseSelf();
    }
    $scope.clear = function () {
        $scope.animate.enabled = false;
        $scope.save();
    }
    //选择后,实时演示效果
    $scope.play = function () {
        //--清除已动画效果
        var css = editor.model.instance.attr("swiper-animate-effect");
        editor.model.instance.removeClass("animated").removeClass(css);
        //--保存新动画效果
        editor.model.config.animate = $scope.animate;
        editor.model.SetAnimate();
        //--保存新动画效果
        top.scence.play(editor.model.instance);
    }
})
</script>
</asp:Content>