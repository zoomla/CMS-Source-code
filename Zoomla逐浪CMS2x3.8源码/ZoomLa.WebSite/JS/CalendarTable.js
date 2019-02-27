var months = new Array("一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二");
var daysInMonth = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
var days = new Array("日", "一", "二", "三", "四", "五", "六");
var classTemp;
var today = new getToday();
var year = today.year;
var month = today.month;
var newCal;
function getDays(month, year) {
    if (1 == month) return ((0 == year % 4) && (0 != (year % 100))) || (0 == year % 400) ? 29 : 28;
    else return daysInMonth[month];
}
function getToday() {
    this.now = new Date();
    this.year = this.now.getFullYear();
    this.month = this.now.getMonth();
    this.day = this.now.getDate();
}
function Calendar() {
    newCal = new Date(year, month, 1);
    today = new getToday();
    var day = -1;
    var startDay = newCal.getDay();
    var endDay = getDays(newCal.getMonth(), newCal.getFullYear());

    var daily = 0;
    if ((today.year == newCal.getFullYear()) && (today.month == newCal.getMonth())) {
        day = today.day;
    }
    var caltable = window.document.getElementById("calendar");

    var intDaysInMonth = getDays(newCal.getMonth(), newCal.getFullYear());

    for (var intWeek = 0; intWeek < caltable.rows.length; intWeek++)

        for (var intDay = 0; intDay < caltable.rows[intWeek].cells.length; intDay++) {
            var cell = caltable.rows[intWeek].cells[intDay];
            var montemp = (newCal.getMonth() + 1) < 10 ? ("0" + (newCal.getMonth() + 1)) : (newCal.getMonth() + 1);
            if ((intDay == startDay) && (0 == daily)) { daily = 1; }
            var daytemp = daily < 10 ? ("0" + daily) : (daily);
            var d = "<" + newCal.getFullYear() + "-" + montemp + "-" + daytemp + ">";
            if (day == daily) cell.className = "DayNow";
            else if (intDay == 6) cell.className = "DaySat";
            else if (intDay == 0) cell.className = "DaySun";
            else cell.className = "Day";
            if ((daily > 0) && (daily <= intDaysInMonth)) {
                cell.innerHTML = daily;
                daily++;
            } else {
                cell.className = "CalendarTD";
                cell.innerHTML = "";
            }
        }
    document.getElementById("year").value = year;
    document.getElementById("month").value = month + 1;
}
function subMonth() {
    if ((month - 1) < 0) {
        month = 11;
        year = year - 1;
    } else {
        month = month - 1;
    }
    Calendar();
}
function addMonth() {
    if ((month + 1) > 11) {
        month = 0;
        year = year + 1;
    } else {
        month = month + 1;
    }
    Calendar();
}
function setDate() {
    if (document.getElementById("month").value < 1 || document.getElementById("month").value > 12) {
        alert("月的有效范围在1-12之间!");
        return;
    }
    year = Math.ceil(document.getElementById("year").value);
    month = Math.ceil(document.getElementById("month").value - 1);
    Calendar();
}
function getEvent() {
	if (document.all) {
		return window.event; //如果是ie
	}
	func = getEvent.caller;
	while (func != null) {
		var arg0 = func.arguments[0];
		if (arg0) {
			if ((arg0.constructor == Event || arg0.constructor == MouseEvent) || (typeof (arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) {
				return arg0;
			}
		}
		func = func.caller;
	}
	return null;
}
function buttonOver() {
	var evt = getEvent();
	var obj = evt.srcElement || evt.target;
    obj.style.cssText = " background-color:#FFFFFF;";
}

function buttonOut() {
	var obj = window.event.srcElement;
	 obj.style.cssText = "";
}
window.onload = function () { var obj = document.getElementById("times"); if (!obj) return; obj.innerHTML = (new Date().getHours() < 10 ? "0" + new Date().getHours().toString() : new Date().getHours().toString()) + ":" + (new Date().getMinutes() < 10 ? "0" + new Date().getMinutes().toString() : new Date().getMinutes().toString()) + ":" + (new Date().getSeconds() < 10 ? "0" + new Date().getSeconds() : new Date().getSeconds().toString()); }
setInterval(function () { var obj = document.getElementById("times"); if (!obj) return; obj.innerHTML = (new Date().getHours() < 10 ? "0" + new Date().getHours().toString() : new Date().getHours().toString()) + ":" + (new Date().getMinutes() < 10 ? "0" + new Date().getMinutes().toString() : new Date().getMinutes().toString()) + ":" + (new Date().getSeconds() < 10 ? "0" + new Date().getSeconds() : new Date().getSeconds().toString()); }, 1000);
