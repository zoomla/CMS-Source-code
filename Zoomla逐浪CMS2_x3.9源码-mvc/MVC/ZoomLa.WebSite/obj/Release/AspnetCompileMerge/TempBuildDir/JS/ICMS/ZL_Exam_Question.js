
//大题管理
var curScope = null;
var app = angular.module("app", []).controller("appController", function ($scope, $compile) {
    curScope = $scope;
    var listStr = document.getElementById("Qinfo_Hid").value;
    if (listStr != "") { $scope.list = JSON.parse(listStr); }
    else { $scope.list = []; }
    $scope.add = function (questList) {
        if (!questList || questList.length < 1) return;
        for (var i = 0; i < questList.length; i++) {
            $scope.list.pushNoDup(questList[i], "p_id");
        }
    }
    $scope.remove = function (id) {
        $scope.list.forEach(function (v, i, _) {
            if (v.p_id == id) {
                _.splice(i, 1);
            }
        });
    }
    $scope.edit = function (id) {
        ShowAdd(id);
    }
    $scope.getTypeStr = function (type) {
        switch (type) {
            case 0:
                return "单选题";
            case 1:
                return "多选填";
            case 2:
                return "填空题";
            case 3:
                return "解析题";
            case 10:
                return "大题";
            default:
                return type;
        }
    }
});
function SelQuestion(questList,id) {
    if (comdiag) { CloseComDiag(); }
    if (!questList || questList.length < 1) return;
    curScope.$apply(function ($compile) {
        for (var i = 0; i < questList.length; i++) {
            var content = $("<div>"+questList[i].p_Content+"</div>").text();
            if (id > 0) {//修改
                var curdata = curScope.list.GetByID(id, "p_id");
                curdata.p_title = content.length > 65 ? content.substr(0, 65)+"..." : content;
            } else {
                questList[i].p_title = content.length > 65 ? content.substr(0, 65)+"..." : content;
                curScope.list.pushNoDup(questList[i], "p_id");
            }
        }
    });
    ZL_Regex.B_Num(".int");
}
function BigCheck() {
    var ids = curScope.list.GetIDS("p_id");
    if (ids.length > 0) {
        $("#Qids_Hid").val(ids);
        $("#Qinfo_Hid").val(angular.toJson(curScope.list));
    } else { alert("请还没有选择小题!"); return false; }
    return true;
}
function ShowAdd(id) {
    if ($("#txtP_title").val() == "") { alert("请填写试题标题!"); return; }
    comdiag.reload = true;
    ShowComDiag("/User/Exam/AddSmallQuest.aspx"+(id?"?id="+id:""), "添加小题");
}
function ShowSel() {
    var url = "/User/Exam/SelQuestion.aspx?type=1&islage=1&selids=," + $("#Qids_Hid").val() + ",";
    ShowComDiag(url, "选择题目")
}
//--------------------------------------------
var diag = new ZL_Dialog();
function ShowDiag() {//后期增加对公式的支持,已有实现思路
    diag.title = "设定内容";
    diag.maxbtn = false;
    diag.backdrop = true;
    diag.url = "/Manage/Exam/Addoption.html";
    diag.ShowModal();
}
function CloseDiag() { diag.CloseModal(); }
var tabarr = [];//关键字数组
function CheckData() {
    var node = $("#NodeID_Hid").val();
    if (ZL_Regex.isEmpty(node)||node==undefined||node==0) {
        alert("请选择试题科目");
        return false;
    }
    if (ZL_Regex.isEmpty($("#txtP_title").val())) { alert("标题不能为空"); return false; }
    var qtype = $("input[name=qtype_rad]:checked").val();
    if (qtype == 10) {
        if (!BigCheck()) { return false; }
    }
    GetOptions();
    return true;
}
//通用加载事件
$(function () {
    var option = $("#Optioninfo_Hid").val();
    if (option != "") {
        AnalyOption(JSON.parse(option));
    }
    InitKeyWord($("#TagKey_T").val());
    var qtype = $("input[name=qtype_rad]:checked").val();
    //大题处理
    $("input[name=qtype_rad]").click(function () {
        if (this.value == 10) { $(".bigtr").show(); $("#normaltr").show(); }
        else { $(".bigtr").hide(); $("#normaltr").show(); }
    });
    if (qtype == 10) { $(".bigtr").show(); $("#normaltr").show(); }
    //标题字数
    $("#txtP_title").bind("keydown paste cut blur", function () {
        setTimeout(function () { $("#titleNum_sp").html("字数：<strong style='color:green;'>" + $("#txtP_title").val().length) + "</span>"; }, 50);
    })
    //完型填空
    if ($("input[name='qtype_rad']:checked").val() == "4") {
        SetQuestMod();
    }
    $("input[name='qtype_rad']").change(function () {
        if ($(this).val() == "4")//完型填空
        {
            SetQuestMod();
        } else {
            ReBackQuestMod();
        }
    });
})
//用于完型填空页面获取内容
function GetContent() {
    return UE.getEditor("txtP_Content").getContent();
}
//设置试题信息选项
function SetQuestMod() {
    $("#questtype_a").removeAttr("data-toggle");
    $("#questtype_a").attr("href", "javascript:;");
    $("#questtype_a").attr("onclick", "ShowQuestType()");
}
//还原试题信息选项
function ReBackQuestMod() {
    $("#questtype_a").attr("data-toggle", "tab");
    $("#questtype_a").attr("href", "#question");
    $("#questtype_a").removeAttr("onclick");
}
//完型填空回调
function GetQuesType(questdata) {
    $("#Optioninfo_Hid").val(questdata);
    CloseComDiag();
}
//显示完型填空设置
function ShowQuestType() {
    if (UE.getEditor("txtP_Content").getContent() == "") { alert("您还没有填写试题内容!"); return; }
    comdiag.maxbtn = false;
    comdiag.reload = true;
    ShowComDiag("/User/Exam/QuestOption.aspx?id=" + $("#Id_hid").val(), "试题信息");
}
