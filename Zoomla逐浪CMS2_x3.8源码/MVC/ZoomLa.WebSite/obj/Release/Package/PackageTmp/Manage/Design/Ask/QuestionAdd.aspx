<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionAdd.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.Ask.QuestionAdd" MasterPageFile="~/Manage/I/Default.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>问题管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<link rel="stylesheet" type="text/css" href="/Plugins/ionic/css/ionic.css"/>
<link rel="stylesheet" type="text/css" href="/Design/ask/js/global.css" />
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
    <li><a href='Default.aspx'>动力模块</a></li>
    <li><a href='AskList.aspx'>问卷调查</a></li>
    <li><a href="QuestionList.aspx?AskID=<%:AskID %>">问题列表(<asp:Label runat="server" ID="AskTitle_T"></asp:Label>)</a></li>
    <li><a href="<%=Request.RawUrl %>">问题管理</a></li>
</ol>
<div ng-app="APP" ng-controller="APPCtrl">
<div class="container padding0-xs lgmargin_top10">
    <div class="list">
        <label class="item item-input">
            <input type="text" placeholder="请输入题目名称" ng-model="model.QTitle">
        </label>
        <div ng-switch="showByQType()">
            <div ng-switch-when="radio">
                <div class="msel_wrap">
                    <div class="col left" ng-click="changeQType('radio')" ng-class="model.QType=='radio'?'active':'';">单选题</div>
                    <div class="col right" ng-click="changeQType('checkbox')" ng-class="model.QType=='checkbox'?'active':'';">多选题</div>
                </div>

                <label class="item item-input" ng-repeat="item in model.QOption track by $index">
                    <input type="text" placeholder="请输入选项名称" ng-model="item.text">
                </label>
                <div style="text-align: center;" class="padding">
                    <button type="button" class="button button-positive button-outline" ng-click="addOption();"><i class="fa fa-plus"></i>添加新项</button></div>
            </div>
            <div ng-switch-when="blank">
                <label class="item item-input item-select">
                    <div class="input-label">
                        输入框行数
                    </div>
                    <select ng-model="model.QFlag.rows" ng-options="m for m in rowArr"></select>
                </label>
            </div>
            <div ng-switch-when="score">
                <label class="item item-input item-select">
                    <div class="input-label">请打分</div>
                    <select ng-model="model.QFlag.maxscore" ng-options="m for m in maxscoreArr"></select>
                </label>
            </div>
            <div ng-switch-when="sort">
                <label class="item item-input" ng-repeat="item in model.QOption track by $index">
                    <input type="text" placeholder="请输入选项名称,为空自动忽略" ng-model="item.text">
                </label>
                <div style="text-align: center;" class="padding">
                    <button type="button" class="button button-positive button-outline" ng-click="addOption();"><i class="fa fa-plus"></i>添加新项</button></div>
            </div>
            <div ng-switch-default></div>
        </div>
        <div class="item item-toggle">
            是否必填
            <label class="toggle toggle-balanced">
                <input type="checkbox" ng-model="model.Required" />
                <div class="track">
                    <div class="handle"></div>
                </div>
            </label>
        </div>
    </div>
    <div class="pcask_addbtn">
        <asp:Button runat="server" ID="Save_Btn" style="display:none;" OnClick="Save_Btn_Click"/>
        <button type="button" class="button button-balanced button-block" ng-click="submit();">确认</button></div>
</div>
</div>
<asp:HiddenField runat="server" ID="Question_Hid" />
<asp:HiddenField runat="server" ID="Ask_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Plugs/angular.min.js"></script>
<script>
    angular.module("APP", []).controller("APPCtrl", function ($scope) {
        $scope.askMod = JSON.parse($("#Ask_Hid").val());
        $scope.model = JSON.parse($("#Question_Hid").val());
        $scope.rowArr = [1, 2, 3, 4, 5, 6];
        $scope.maxscoreArr = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        if (!$scope.model.QOption) { $scope.model.QOption = []; }
        //--------------
        var Question = {
            move: function (from, target, callback) {
                $.post(api + "move", { "from": from, "target": target }, function (data) {
                    APIResult.ifok(data, callback);
                })
            },
            getType: function (qtype) {
                switch (qtype) {
                    case "radio":
                        return "单选";
                    case "checkbox":
                        return "多选";
                    case "blank":
                        return "填空";
                    case "score":
                        return "评分";
                    case "sort":
                        return "排序";
                    default:
                        return "未知[" + qtype + "]";
                }
            },
            newModel: function () { return { "ID": 0, "AskID": "<%:AskID%>", "QTitle": "", "QContent": "", "QOption": [], "QType": "<%:QType%>", "QFlag": { type: "<%:QFlag%>", rows: 1, maxscore: 5 }, "Required": true, "OrderID": 0, "CUser": 0 }; },
            newOption: function (text) {
                //用math生成的小数位过长,在转dt时会截位
                var model = { text: "", value: parseInt(Math.random() * 10000), checked: false }
                if (text) { model.text = text; }
                return model;
            }
            //maxscore:最大分值,rows:最大行数,type:子类型
            //QOption:客户端用[],提交时转为字符串  {text:提交的文本,value:提交的值(预留),checked:是否选中|初始是否选中}
        }
        //--------------
        //根据试题类型显示需填或选择的项
        $scope.showByQType = function () {
            switch ($scope.model.QType) {
                case "radio":
                case "checkbox":
                    return "radio";
                default:
                    return $scope.model.QType;
            }
        }
        $scope.getType = function (qtype) { return Question.getType(qtype); }
        $scope.changeQType = function (qtype) { $scope.model.QType = qtype; }
        $scope.addOption = function () { $scope.model.QOption.push(Question.newOption()); }
        if ($scope.model.ID == 0) {
            $scope.model = Question.newModel();
            switch ($scope.model.QFlag) {
                case "sex"://性别单选
                    $scope.model.QOption.push(Question.newOption("男"));
                    $scope.model.QOption.push(Question.newOption("女"));
                    break;
                case "mobile":
                    $scope.model.QTitle = "请输入手机号码";
                    break;
                case "email":
                    $scope.model.QTitle = "请输入邮箱";
                    break;
                case "area":
                    $scope.model.QTitle = "请选择所在城市";
                    break;
                case "date":
                    $scope.model.QTitle = "请选择日期";
                    break;
            }
            if ($scope.model.QOption.length < 1) {
                $scope.model.QOption.push(Question.newOption());
                $scope.model.QOption.push(Question.newOption());
            }
        }//if end;
        $scope.submit = function () {
            var model = $scope.model;
            model.QFlag = angular.toJson(model.QFlag);
            model.QOption = angular.toJson(model.QOption);
            model = angular.toJson(model);
            $("#Question_Hid").val(model);
            $("#Save_Btn").click();
        }
    });
</script>
</asp:Content>