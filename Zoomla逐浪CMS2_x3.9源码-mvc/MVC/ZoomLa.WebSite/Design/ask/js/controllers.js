angular.module('starter.controllers', [])

.controller("IndexCtrl", function ($scope,$state, $http) {
   
})
.controller("AskAddCtrl", function ($scope, $ionicLoading, $state, User, Loading, Ask) {
    User.check();
    $scope.model = { PreViewImg: "" };
    $scope.model.EndDate = new Date(moment().add(365, "days").format("YYYY/MM/DD"));
    picup.up_before = function () { Loading.show(); }
    picup.up_after = function (data) { $scope.model.PreViewImg = data; $scope.$digest(); Loading.hide(); }
    $scope.add = function () {
        Loading.show();
        Ask.add($scope.model,
            function (result) { Loading.hide(); $state.go("tab.ask_qlist", { "askid": result }); },
            function (result) { Loading.hide(); alert("failed:" + result); })
    }
    //------different
})
.controller("AskEditCtrl", function ($scope, $state, $stateParams,Loading, Ask) {
    $scope.model = { PreViewImg: "" };
    picup.up_before = function () { Loading.show(); }
    picup.up_after = function (data) { $scope.model.PreViewImg = data; $scope.$digest(); Loading.hide(); }
    $scope.add = function () {
        Loading.show();
        Ask.add($scope.model,
            function (result) { Loading.hide(); $state.go("tab.ask_qlist", { "askid": result }); },
            function (result) { Loading.hide(); alert("failed:" + result); })
    }
    //------different
    Loading.show();
    Ask.get($stateParams.id, function (result) { $scope.model = result; $scope.$digest();Loading.hide(); });
})
.controller("AskQListCtrl", function ($scope, $state, $stateParams, $ionicPopup, Ask,User, Loading, Question, DB) {
    User.check();
    var mypop = null;
    $scope.askMod = {};
    $scope.showOrder = false;
    Loading.show();
    Ask.get($stateParams.askid, function (result) { $scope.askMod = result;});
    Question.list({ "askid": $stateParams.askid }, function (result) { $scope.qlist = result; Loading.hide(); });
    $scope.getType = function (qtype) { return Question.getType(qtype); }
    //获取问卷备注
    $scope.getRemind = function () {
        if (ZL_Regex.isEmpty($scope.askMod.Remind)) { return "修改问卷标题与说明"; }
        else{
            return StrHelper.getSubStr($scope.askMod.Remind, 20);
        }
    }
    //选择问题类型,用于创建
    $scope.selQType = function () {
        mypop=$ionicPopup.confirm({
            title: '问题类型',
            //template: 'Are you sure you want to eat this ice cream?',
            templateUrl: "templates/diag/selqtype.html?v=" + Math.random(),
            scope: $scope,
            buttons: [{ text: "取消" }, { text: "确定", type: 'button-positive', onTap: function (e) { } }]
        });
    }
    //新建试题
    $scope.tonew = function (qtype, type) {
        var questMod = Question.newModel();
        questMod.AskID = $stateParams.askid;
        questMod.QType = qtype;
        questMod.QFlag.type = type ? type : "";//大类型下的小类
        switch (type) {
            case "sex"://性别单选
                questMod.QOption.push(Question.newOption("男"));
                questMod.QOption.push(Question.newOption("女"));
                break;
            case "mobile":
                questMod.QTitle = "请输入手机号码";
                break;
            case "email":
                questMod.QTitle = "请输入邮箱";
                break;
            case "area":
                questMod.QTitle = "请选择所在城市";
                break;
            case "date":
                questMod.QTitle = "请选择日期";
                break;
        }
        DB.set(questMod);
        $state.go("tab.question_add", { "askid": $stateParams.askid });
        mypop.close();
    }
    //------操作按钮
    $scope.toedit = function (item) {
        $state.go("tab.question_edit", { id: item.ID });
    }
    $scope.toview = function () {
        DB.settemp("view");
        $state.go("tab.ask_view", { askid: $scope.askMod.ID });
    }
    $scope.move = function (dir, item) {
        var tar = ArrCOM.GetNear($scope.qlist, { "dir": dir, order: item.OrderID, field: "OrderID" });
        var temp = tar.OrderID;
        tar.OrderID = item.OrderID;
        item.OrderID = temp;
        Question.move(tar.ID, item.ID);
    }
    $scope.del = function (item) {
        if (!confirm("确定要删除吗")) { return false; }
        Question.del(item.ID, function (result) { console.log(result); });
        for (var i = 0; i < $scope.qlist.length; i++) {
            if ($scope.qlist[i].ID == item.ID) { $scope.qlist.splice(i, 1); return; }
        }
    }
})
.controller('AskMyCtrl', function ($scope, $state, Loading, Ask, User, Question, DB) {
    User.check();
    $scope.list = [];
    Loading.show();
    Ask.list({}, function (result) { $scope.list = result; Loading.hide(); });
    $scope.getRemind = function (item) { return StrHelper.getSubStr(item.Remind, 30); }
    $scope.del = function (item) {
        if (!confirm("确定要删除吗")) { return false; }
        Ask.del(item.ID, function (result) { console.log(result); });
        for (var i = 0; i < $scope.list.length; i++) {
            if ($scope.list[i].ID == item.ID) { $scope.list.splice(i, 1); return; }
        }
    }
    $scope.toedit = function (item) { $state.go("tab.ask_qlist", { askid: item.ID }); }
    $scope.toshare = function (item) { DB.settemp("share"); $state.go("tab.ask_view", { askid: item.ID }); }
})
.controller("AskViewCtrl", function ($scope, $state, $stateParams, $ionicPopup, $ionicScrollDelegate, Loading, Ask, Question, Answer, DB) {
    cfg.scope = $scope;
    $scope.askMod = {};
    $scope.qlist = [];
    $scope.mode = DB.gettemp();
    $scope.finished = false;//是否已完成
    $scope.showShare = ($scope.mode == "share");
    $scope.cansubmit = !$scope.mode;//view(预览),share(显示分享提示),answer(我的回答),不存在则可提交
    Loading.show();
    //---------------------------------
    Ask.get($stateParams.askid, function (result) { $scope.askMod = result; $("title").text($scope.askMod.Title); });
    Question.list({ "askid": $stateParams.askid }, function (result) {
        $scope.qlist = result;
        for (var i = 0; i < $scope.qlist.length; i++) {
            $scope.qlist[i].answer = "";//用户回答
        }
        setTimeout(function () {
            $(".rating").rating('refresh', { showClear: false }).on('rating.change', function (event, value, caption) {
                //点击后事件,暂不用
            });
        }, 500);
        Loading.hide();
    });
    //---------------------------------
    $scope.hideShare = function () {$scope.showShare = false; }
    $scope.geticon = function (qtype) {
        switch (qtype) {
            case "email":
                return "fa fa-envelope";
            case "mobile":
                return "fa fa-mobile";
            case "area":
                return "fa fa-home";
            case "date":
                return "fa fa-calendar";
        }
    }
    //提交问卷
    $scope.submit = function () {
        var alertmsg = function (questMod, msg) {
            //alert("[" + questMod.QTitle + "]" + msg);
            $ionicPopup.alert({
                title: '信息提示',
                template:"问题:[" + questMod.QTitle + "]" + msg,
                buttons: [{ text: "关闭", type: 'button-calm', onTap: function (e) { } }]
            });
        }
        //单选,填空,打分,直接返answer,多选需要遍历再返,排序后期设定
        var anslist = [];
        for (var i = 0; i < $scope.qlist.length; i++) {
            var questMod = $scope.qlist[i];
            var ansMod = { "qid": questMod.ID, "qtype": questMod.QType, "answer": questMod.answer };
            switch (questMod.QType)//根据问题类型,获取答案
            {
                case "blank":
                    questMod.answer = $("#text_" + questMod.ID).val();
                    ansMod.answer = questMod.answer;
                    if (questMod.Required && ZL_Regex.isEmpty(questMod.answer)) { alertmsg(questMod,"不能为空"); return; }
                    if (!ZL_Regex.isEmpty(ansMod.answer))//不为空检测字符串是否规范
                    {
                        switch (questMod.QFlag.type) {
                            case "mobile":
                                if (!ZL_Regex.isMobilePhone(ansMod.answer)) { alertmsg(questMod, "手机号码格式不正确"); return; }
                                break;
                            case "email":
                                if (!ZL_Regex.isEmail(ansMod.answer)) { alertmsg(questMod, "邮箱格式不正确"); return; }
                                break;
                        }
                    }

                    anslist.push(ansMod);
                    break;
                case "radio":
                    if (questMod.Required && ZL_Regex.isEmpty(questMod.answer)) { alertmsg(questMod, "不能为空"); return; }
                    anslist.push(ansMod);
                    break
                case "score":
                    var score = document.getElementById("score_" + questMod.ID).value;
                    score = Convert.ToInt(score, 0);
                    if (questMod.Required && score <= 0) { alertmsg(questMod, "未评分"); return; }
                    ansMod.answer = score;
                    anslist.push(ansMod);
                    break;
                case "checkbox":
                    var chks = "";
                    questMod.QOption.forEach(function (item, index, array) {
                        if (item.checked) { chks += item.value + ","; }
                    });
                    if (questMod.Required && ZL_Regex.isEmpty(chks)) { alertmsg(questMod, "未选中"); return; }
                    ansMod.answer = chks.substr(0, chks.length - 1);
                    anslist.push(ansMod);
                    break;
                case "sort":
                default:
                    console.log(questMod.QType + "未命中");
                    break;
            }
        }//for end;
        Loading.show();
        Answer.submit($scope.askMod.ID, JSON.stringify(anslist), function (data) {
            $scope.finished = true;
            $ionicScrollDelegate.scrollTop(false);
            Loading.hide();
        })
    }//submit end;
})
.controller("QuestionAddCtrl", function ($scope, $stateParams, $state, Loading, User, Ask, Question, DB) {
    User.check();
    //$stateParams.askid
    //添加  askid,qtype
    //修改  id
    $scope.askMod = {};
    $scope.rowArr = [1, 2, 3, 4, 5, 6];
    $scope.maxscoreArr = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    //--------------
    $scope.model = DB.get();
    //如无选项值,则初始化两项
    if ($scope.model.QOption.length < 1) {
        $scope.model.QOption.push(Question.newOption());
        $scope.model.QOption.push(Question.newOption());
    }
    Loading.show();
    Ask.get($stateParams.askid, function (result) { $scope.askMod = result; Loading.hide(); });
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
    $scope.submit = function () {
        Loading.show();
        Question.add($scope.model,
         function (result) { Loading.hide(); $state.go("tab.ask_qlist", { "askid": $scope.model.AskID }, { reload: true }); },
         function (result) { Loading.hide(); alert(result); })
    }
})
.controller("QuestionEditCtrl", function ($scope, $stateParams, $state, User, Loading, Ask, Question, DB) {
    User.check();
    $scope.askMod = {};
    $scope.rowArr = [1, 2, 3, 4, 5, 6];
    $scope.maxscoreArr = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    $scope.model = {};
    //--------------
    Loading.show();
    Question.get($stateParams.id, function (result) {
        $scope.model = result;
        Ask.get($scope.model.AskID, function (result) { $scope.askMod = result;  Loading.hide(); });
    })
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
    $scope.submit = function () {
        Loading.show();
        Question.add($scope.model,
         function (result) { Loading.hide(); $state.go("tab.ask_qlist", { "askid": $scope.model.AskID }, { reload: true }); },
         function (result) { Loading.hide(); alert(result); })
    }
})
.controller("AnswerMyCtrl", function ($scope, User, Loading, Answer) {
    User.check();
    $scope.list = [];
    Loading.show();
    Answer.list({}, function (result) {
        $scope.list = result;
        Loading.hide();
    });
})