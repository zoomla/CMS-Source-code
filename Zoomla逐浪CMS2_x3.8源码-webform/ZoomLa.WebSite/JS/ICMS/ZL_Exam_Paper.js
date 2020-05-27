function OpenFormula() {
    ue.execCommand("kityformula", {});
}
function SetContent($curAnswer) {
    ue.setContent($curAnswer.html());
}
function LoadContent() {
    var html = ue.getContent();
    $curAnswer.html(html);
    ue.setContent(""); 
}
//获取各种试题的答案
function PreSubmit() {
    if (!confirm("确定要交卷吗?")) { return false;  }
    //p_id,p_type,p_title   answer
    var questArr = JSON.parse($("#QuestDT_Hid").val());
    for (var i = 0; i < questArr.length; i++) {
        var quest = questArr[i]; quest.answer = "";
        //quest = { p_id: "", p_Type: 0, p_title: "", answer: "" };
        switch (quest.p_Type) {//单,多,填,选
            case 0:
                quest.answer = $("[name=srad_" + quest.p_id + "]:checked").val();
                break;
            case 1:
                $("[name=mchk_" + quest.p_id + "]:checked").each(function () { quest.answer += this.value + ","; });
                break;
            case 2:
                var item = $("#item_" + quest.p_id);
                $(item).find(".answersp").each(function () {
                    quest.answer += $(this).html() + boundary;
                });
                break;
            case 3:
                quest.answer = $("#answer_" + quest.p_id).html();
                break;
            case 4://暂只支持单选
                {
                    //去除标题等信息,减少量
                    var list = page.scope.list["filltextblank_" + quest.p_id];
                    for (var j = 0; j < list.length; j++) {
                        var model = list[j];
                        delete model.title;
                        delete model.opts;
                        delete model.score;
                        delete model.$$hashKey;
                    }
                    quest.answer = list;
                }
                break;
            default:
                quest.answer = "";
                break;
        }//switch end;
        if (!quest.answer || quest.answer == null) { quest.answer = ""; }
        //if (quest.answer == "" && quest.istoshare == 0 && !force) {//如果未开启强制提交,并且不是大题,则检测
        //    alert("[" + quest.p_title + "]未填写,请填写完答案再提交!"); return false;
        //}
    }
    $("#QuestDT_Hid").val(JSON.stringify(questArr));
    localStorage.second=0;
    return true;
}
//用于查看试卷,填充用户答案
function LoadAnswer() {
    var answerArr = JSON.parse($("#Answer_Hid").val());
    for (var i = 0; i < answerArr.length; i++) {
        var model = answerArr[i];
        switch (model.QType) {
            case 0:
                var chk = $("[name=srad_" + model.QID + "][value='" + model.Answer + "']")[0];
                if (chk) { chk.checked = true; }
                break;
            case 1:
                {
                    var valArr = model.Answer.split(',');
                    for (var j = 0; j < valArr.length; j++) {
                        var chk = $("[name=mchk_" + model.QID + "][value='" + valArr[j] + "']")[0];
                        if (chk) { chk.checked = true; }
                    }
                }
                break;
            case 2:
                {
                    var spArr = $("#item_" + model.QID).find(".answersp");
                    var valArr = model.Answer.split(boundary);
                    for (var j = 0; j < spArr.length; j++) {
                        $(spArr[j]).html(valArr[j]);
                    }
                }
                break;
            case 3:
                $("#answer_" + model.QID).html(model.Answer);
                break;
            case 4:
                {
                   //在Angular回调中处理
                }
                break;
            default:
                //quest.answer = null;
                break;
        }
    }
}
//教师批注保存前执行
function PreMark() {
    var answerArr = JSON.parse($("#Answer_Hid").val());
    //var answerArr=[];
    for (var i = 0; i < answerArr.length; i++) {
        var answer = answerArr[i];
        if (answer.IsToShare == 1) continue;
        var rad = $("[name=isright_" + answer.ID + "]:checked")[0];
        if (rad)
        {
            answer.IsRight = rad.value;
            //自定义得分
            answer.Score = ConverToInt($("#score_" + answer.ID).val());
        }
        else
        {
            alert("[" + answer.QTitle + "]尚未批阅!"); return false;
        }
        answer.Remark = $("#remark_" + answer.ID).html();
    }
    $("#Answer_Hid").val(JSON.stringify(answerArr));
    return true;
}
//------------------Tools
function BeginTimer() {
    var timer = $("#time_sp");
    var second = parseInt(timer.data("time")); second++;
    timer.data("time", second);
    localStorage.second = second;
    $("#QuestTime_Hid").val(second);
    timer.text(SecondToDate(second));
    if (second % 60 == 0) { TimeRemind((second / 60)); }
}
//秒转为时间
function SecondToDate(time) {
    if (null != time && "" != time) {
        if (time > 60 && time < 60 * 60) {
            time = parseInt(time / 60.0) + "分钟" + parseInt((parseFloat(time / 60.0) -
            parseInt(time / 60.0)) * 60) + "秒";
        } else if (time >= 60 * 60 && time < 60 * 60 * 24) {
            time = parseInt(time / 3600.0) + "小时" + parseInt((parseFloat(time / 3600.0) -
            parseInt(time / 3600.0)) * 60) + "分钟" +
            parseInt((parseFloat((parseFloat(time / 3600.0) - parseInt(time / 3600.0)) * 60) -
            parseInt((parseFloat(time / 3600.0) - parseInt(time / 3600.0)) * 60)) * 60) + "秒";
        } else {
            time = parseInt(time) + "秒";
        }
    } else {
        time = "0 时 0 分0 秒";
    }
    return time;

}
//提醒,尚余30分钟,10分钟,5分钟
function TimeRemind(minute) {
    if (exTime < 1) { return; }
    var reTime = exTime - minute;
    switch (reTime) {
        case 30:
            alert("离交卷还有30分钟!");
            break;
        case 10:
            alert("离交卷还有10分钟!");
            break;
        case 5:
            alert("离交卷还有5分钟!");
            break;
        case 0://交卷
            $("#Submit_Btn").click();
            break;
    }
}
function ConverToInt(val, def, suf) {
    if (!def) def = 0;
    if (!suf) suf = "";
    if (!val || val == "") { val = def; }
    //val = val.replace(/ /g, "").replace("px", "").replace("em", "");
    val = parseInt(val);
    if (isNaN(val)) { val = def; }
    return val;
}
//----------------添加试题
var opstr = "A,B,C,D,E,F,G,H,I,J".split(',');
var litlp = "<li><div class='input-group' style='width:100%'><span class='btn btn-default input-group-addon'>@op</span><textarea class='ueoption_t' data-id='@op_t' id='@op_t' style='width: 100%; height: 80px; ' value='@val'></textarea></div></li>";
//生成指定数目的选项
var config = {uebar:[['Undo', 'Redo', 'Bold', 'Italic', 'NumberedList', 'BulletedList', 'Smiley', 'ShowBlocks', 'Maximize', 'underline', 'fontborder', 'strikethrough', 'simpleupload', 'insertimage']]};
function AddOption(num) {
    for (var i = 0; i < opstr.length; i++) {
        if ($("#" + opstr[i] + "_t").length > 0) {
            UE.delEditor(opstr[i] + "_t");
        }
    }
    $("#option_ul").html("");
    for (var i = 0; i < num; i++) {
        var li = litlp.replace(/@op/g, opstr[i]).replace("@val", "");
        $("#option_ul").append(li);
        UE.getEditor(opstr[i] + "_t", { toolbars: config.uebar });
    }
}
//解析选项信息
function AnalyOption(json) {
    console.log(json);
    $("#option_ul").html("");
    for (var i = 0; i < json.length; i++) {
        var li = litlp.replace(/@op/g, json[i].op).replace("@val", json[i].val);
        $("#option_ul").append(li);
        UE.getEditor(json[i].op + "_t", { toolbars: config.uebar })
        setTimeout(function () {
            $(".ueoption_t").each(function (i, v) {
                UE.getEditor(json[i].op + "_t", json[i].val).setContent(json[i].val);
            });
        }, 2000)
        //注释说明:百度ready事件不执行(待解决)
        //editor.addListener("ready", function () {
        //    var val = json[$(this)[0].uid].val;
        //    $(this)[0].setContent(jval);
        //});
    }
}
//保存前执行
function GetOptions() {
    var qtype = $("[name=qtype_rad]:checked").val();
    if (qtype == 4) { return; }
    var $liarr = $("#option_ul li"); var oparr = [];
    if ($liarr.length < 1) { return; }
    for (var i = 0; i < $liarr.length; i++) {
        var opMod = { op: "", val: "" }; var $li = $($liarr[i]);
        opMod.op = $li.find(".input-group-addon").text();
        opMod.val = UE.getEditor(opMod.op + "_t").getContent(); //$li.find("#" + opMod.op + "_t").val();//
        oparr.push(opMod);
    }
    $("#Optioninfo_Hid").val(JSON.stringify(oparr));
}