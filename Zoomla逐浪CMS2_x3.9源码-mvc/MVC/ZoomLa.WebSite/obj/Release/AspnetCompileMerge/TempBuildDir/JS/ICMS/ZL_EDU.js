//排课系统JS
//var tdTlp = "<td class='item' data-time='@time' data-day='@day'  data-num='@num'><div class='itemhead'><span class='fa fa-cog bantd' title='禁用|启用'></span></div><textarea class='content'></textarea></td>";
var tdTlp = "<td class='item' data-time='@time' data-day='@day' data-num='@num'></td>";
//--------------数组
function GetItem(arr, $td) {
    if (!arr || arr.length < 1) return;
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].time == $td.data("time") && arr[i].day == $td.data("day") && arr[i].num == $td.data("num"))
        { return arr[i]; }
    }
}
//初始化生成html
function InitTable() {
    table.html("");
    config = JSON.parse($("#Json_Hid").val());
    config.items = JSON.parse(config.items);
    if (!config.CourseTime) { config.CourseTime = []; }
    else { config.CourseTime = JSON.parse(config.CourseTime); }
    var courseTimeTD = '<td class="courseTime_item" style="width:150px;" data-time="@time" data-index="@index">';
    courseTimeTD += '<div style="padding-top:15px;" class="input-group"><span class="input-group-addon">开始</span><input type="text" class="stime_t form-control" value="@stime" onclick="WdatePicker({dateFmt: \'HH:mm\',minDate: \'@min\', maxDate: \'@max\'})" /></div>';
    courseTimeTD += '<div style="margin-top:10px;" class="input-group"><span class="input-group-addon">结束</span><input type="text" class="etime_t form-control" value="@etime" onclick="WdatePicker({dateFmt: \'HH:mm\',minDate: \'@min\', maxDate: \'@max\'})" /></div></td>';
    //-------------------------------------------
    //有几节课,则产生几行TR
    for (var i = 0; i < config.premoning; i++) {
        var html = "", index = (i + 1), time = "premoning";
        if (i == 0) {//如果是第一行第一列,则加一个标志td
            html = '<tr><td class="flag_td" rowspan="' + config.premoning + '">早读</td>';
        }
        else { html = '<tr>'; }
        //----填充课程时间
        var course = config.CourseTime.GetCourseItem(time, index);
        html += courseTimeTD.replace(/@min/g, "6:00").replace(/@max/g, "9:00")
            .replace(/@index/, index).replace(/@time/, time).replace("@stime", course.stime).replace("@etime", course.etime);
        //----每周上几天课,则产生几列td
        for (var j = 0; j < config.weekday; j++) {
            html += tdTlp.replace(/@time/g, time).replace(/@num/g, index).replace(/@day/g, (j + 1));
        }
        html += "</tr>";
        table.append(html);
    }
    //-------------------------------------------
    for (var i = 0; i < config.moring; i++) {
        var html = "", index = (i + 1), time = "moring";
        //左方时间标识
        if (i == 0) {
            html = '<tr><td class="flag_td" rowspan="' + config.moring + '">上午</td>';
        }
        else { html = '<tr>'; }
        var course = config.CourseTime.GetCourseItem(time, index);
        html += courseTimeTD.replace(/@min/g, "7:00").replace(/@max/g, "12:30").replace(/@index/, index).replace(/@time/, time)
        .replace("@stime", course.stime).replace("@etime", course.etime);
        //生成横列
        for (var j = 0; j < config.weekday; j++) {
            html += tdTlp.replace("@time", "moring").replace("@num", index).replace("@day", (j + 1));
        }
        html += "</tr>";
        table.append(html);
    }
    //-------------------------------------------午休,需要多增加一列
    table.append('<tr><td style="background-color:#d9edf7;text-align:center;height:30px;line-height:30px;" colspan="' + (config.weekday+2) + '">午  休</td></tr>');
    for (var i = 0; i < config.afternoon; i++) {
        var html = "", index = (i + 1), time = "afternoon";
        if (i == 0) {
            html = '<tr><td class="flag_td" rowspan="' + config.afternoon + '">下午</td>';
        }
        else { html = '<tr>'; }
        var course = config.CourseTime.GetCourseItem(time, index);
        html += courseTimeTD.replace(/@min/g, "1:00").replace(/@max/g, "20:00").replace(/@index/, (i + 1)).replace(/@time/, time)
         .replace("@stime", course.stime).replace("@etime", course.etime);
        for (var j = 0; j < config.weekday; j++) {
            html += tdTlp.replace("@time", time).replace("@num", index).replace("@day", (j + 1));
        }
        html += "</tr>";
        table.append(html);
    }
    //-------------------------------------------
    for (var i = 0; i < config.evening; i++) {
        var html = "", index = (i + 1), time = "evening";
        if (i == 0) {
            html = '<tr><td class="flag_td" rowspan="' + config.evening + '">晚自习</td>';
        }
        else { html = '<tr>'; }
        var course = config.CourseTime.GetCourseItem(time, index);
        html += courseTimeTD.replace(/@min/g, "18:00").replace(/@max/g, "23:30").replace(/@index/, (i + 1)).replace(/@time/, time)
         .replace("@stime", course.stime).replace("@etime", course.etime);
        for (var j = 0; j < config.weekday; j++) {
            html += tdTlp.replace("@time", time).replace("@num", index).replace("@day", (j + 1));
        }
        html += "</tr>";
        table.append(html);
    }
    if ($("#BanEdit_Hid").val() == "1") {
        $(".stime_t,.etime_t").attr("disabled", "disabled");
    }
    //-------------------------------------------
    HideTBColumn(config.weekday);
    var oldpop = null;
    //绑定事件
    table.find(".item").click(function () {
        $this = $(this);
        //禁止修改
        if ($("#BanEdit_Hid").val() == "1") { return false; }
        //忽略此次点击(空白)
        if ($this.hasClass("ignore")) { $this.removeClass("ignore"); return; }
        if ($this.hasClass("active")) {  return; }
        table.find(".item").removeClass("active");
        $this.addClass("active");
        //table.find(".item").popover("destory");
        //隐去其他
        if (oldpop != null) { oldpop.popover("hide"); }
        var btn = $("<a class='pop_btn'>");
        oldpop = btn;
        //----开始显示,用一个超链接附载,不能直接附,否则会造成表格错乱
        btn.popover({
            trigger: 'manual',
            placement: 'top',
            html: true,
            content: subject_html
        });
        $this.append(btn);
        btn.popover('show');
        $("#subject_tb tr .custom_btn").click(function () {
            $("#custom_div").show();
        });
        $("#addCustom_btn").click(function () {
            addToTable($("#Subject_T").val());
        });
        $("#subject_tb tr td:not(.noclick)").click(function () {
            //-----执行
            var $this = $(this);
            addToTable($this.text());
        });
    });
    var addToTable = function (text) {
        var $td = getCurSelected();
        var color = "";
        switch ($td.data("day")) {
            case 1:
                color = "#8577FA";
                break;
            case 2:
                color = "#D7CA3A";
                break;
            case 3:
                color = "#40AAFF";
                break;
            case 4:
                color = "#42D2A8";
                break;
            case 5:
                color = "#73C3EC";
                break;
            case 6:
                color = "#A9D338";
                break;
            case 7:
                color = "#E9A582";
                break;
            default:
                alert("星期数据不正确");
                break;
        }
        switch (text) {
            case "空白":
                $td.css("background-color", "");               
                $td.text("");
                break;
            default:
                $td.css("background-color", color);
                $td.text(text);
                break;
        }
        $td.addClass("ignore");
        $td.removeClass("active");
        $("#custom_div").hide();
    }
    //获取当前需要添加的位置
    var getCurSelected = function () { return $("#courseTable").find(".active:first"); }
    //===============================================
    table.find(".item").each(function () {
        var $td = $(this);
        var model = GetItem(config.items, $td);
        if (model && model.text!="") {
            table.find(".item").removeClass("active");
            $td.addClass("active");
            addToTable(model.text);
        }
    });
}
function UpdateConfig() {
    config.weekday = $("#WeekDay_DP").val();
    config.premoning = $("#PreMoning_DP").val();
    config.moring = $("#Moring_DP").val();
    config.afternoon = $("#Afternoon_DP").val();
    config.evening = $("#Evening_DP").val();
}
function Render() {
    SaveConfig();
    InitTable();
}
//读取其td配置,拼接为json
function SaveConfig() {
    UpdateConfig();
    var jsonArr = [];
    table.find(".item").each(function () {
        //所属时间周期(permoning,moning,afternoon),第几天,节数,是否禁用,自定义文本
        var item = { time: "", day: "", num: "", disabled: "", text: "" };
        //$text = $(this).find(".content");
        //jsontd.disabled = $text.hasClass("disabled");
        var $td = $(this);
        item.time = $td.data("time");
        item.day = $td.data("day");
        item.num = $td.data("num");
        item.text = $(this).text();
        jsonArr.push(item);
    });
    config.items = JSON.stringify(jsonArr);
    //课时
    var CourseTime = [];
    table.find(".courseTime_item").each(function () {
        //开始时间,结束时间,所属时间周期,第几节课
        var item = { stime: "", etime: "", time: "", index: 0 };
        var $td = $(this);
        item.time = $td.data("time");
        item.index = $td.data("index");
        item.stime = $td.find(".stime_t").val();
        item.etime = $td.find(".etime_t").val();
        CourseTime.push(item);
    });
    config.CourseTime = JSON.stringify(CourseTime);
    //------------------------
    $("#Json_Hid").val(JSON.stringify(config));
    return true;
}
//--------------表格相关操作
function HideTBColumn(weekday) {
    weekday = parseInt(weekday) + 1;
    var $table = $("#maintable");
    $table.find("thead td").show();
    $table.find("thead td:gt(" + weekday + ")").hide();
    $table.find("tr").each(function () {
        $(this).find("td:gt(" + weekday + ")").addClass("hid");
    });
}
//--------------
//返回一天的td
Array.prototype.GetDayItem = function (day) {
    var itemday = [];
    for (var i = 0; i < this.length; i++) {
        if (this[i].day == day) itemday.push(this[i]);
    }
    return itemday;
}
//返回课程的起始与结束时间
Array.prototype.GetCourseItem = function (time, index) {
    for (var i = 0; i < this.length; i++) {
        if (this[i].time == time && this[i].index == index) { return this[i]; }
    }
    return { stime: "", etime: "" };
}
//--------------
var subject_html = "<div class=\"subject_div\" id=\"subject_div\">"
+ "<table class=\"table table-bordered\" style=\"color:#fff;\" id=\"subject_tb\">"
+ "<tr><td>语文</td><td>数学</td><td>英语</td><td>物理</td><td>化学</td><td>生物</td></tr>"
+ "<tr><td>地理</td><td>历史</td><td>政治</td><td>体育</td><td>科学</td><td>信息</td></tr>"
+ "<tr><td>美术</td><td>音乐</td><td>自习</td><td>空白</td><td colspan=\"2\" class=\"noclick custom_btn\"></td></tr>"
+ "</table>"
+ "<div id=\"custom_div\" class=\"input-group\">"
+ "<input type=\"text\" placeholder=\"科目名称\" id=\"Subject_T\" class=\"form-control\"  style=\"width:300px;\"/>"
+ "<span class=\"input-group-btn\">"
+ "<input value=\"确定\" id=\"addCustom_btn\" class=\"btn btn-default\" style=\"width:80px;\" />"
+ "</span>"
+ "</div>"
+ "</div>";