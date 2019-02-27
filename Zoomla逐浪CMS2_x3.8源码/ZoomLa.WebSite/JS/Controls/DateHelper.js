//标准格式为  yyyy-MM-dd HH:mm:ss  不支持到毫秒级
var DateHelper = {};
//转化秒为时间,返回模型
DateHelper.SecondToTime = function (time) {
    var model = this.getModel();
    if (!time || null == time || "" == time) return model;
    model.day = parseInt(time / (60 * 60 * 24));
    if (model.day > 0) { time = time - ((60 * 60 * 24) * model.day); }
    model.hour = parseInt(time / (60 * 60));
    if (model.hour > 0) { time = time - ((60 * 60) * model.hour); }
    model.minute = parseInt(time / 60);
    if (model.minute > 0) { time = time - (60 * model.minute); }
    model.second = time;
    return model;
}
DateHelper.getDate = function (formatStr) {
    if (!formatStr) { formatStr = "yyyy-MM-dd HH:mm:ss"; }
    var myDate = new Date();
    var str = formatStr;
    var Week = ['日', '一', '二', '三', '四', '五', '六'];

    str = str.replace(/yyyy|YYYY/, myDate.getFullYear());
    str = str.replace(/yy|YY/, (myDate.getYear() % 100) > 9 ? (myDate.getYear() % 100).toString() : '0' + (myDate.getYear() % 100));
    var month = (myDate.getMonth() + 1); if (month < 10) { month = "0" + month; }
    str = str.replace(/MM/, month);
    str = str.replace(/M/g, month);

    str = str.replace(/w|W/g, Week[myDate.getDay()]);

    str = str.replace(/dd|DD/, myDate.getDate() > 9 ? myDate.getDate().toString() : '0' + myDate.getDate());
    str = str.replace(/d|D/g, myDate.getDate());

    str = str.replace(/hh|HH/, myDate.getHours() > 9 ? myDate.getHours().toString() : '0' + myDate.getHours());
    str = str.replace(/h|H/g, myDate.getHours());
    str = str.replace(/mm/, myDate.getMinutes() > 9 ? myDate.getMinutes().toString() : '0' + myDate.getMinutes());
    str = str.replace(/m/g, myDate.getMinutes());

    str = str.replace(/ss|SS/, myDate.getSeconds() > 9 ? myDate.getSeconds().toString() : '0' + myDate.getSeconds());
    str = str.replace(/s|S/g, myDate.getSeconds());

    return str;
}
//时间间隔,返回秒,非负数
DateHelper.getInterval = function (sdate, edate) {
    var ref = this;
    var startTime = new Date(Date.parse(sdate.replace(/-/g, "/"))).getTime();
    var endTime = new Date(Date.parse(edate.replace(/-/g, "/"))).getTime();
    var second = Math.abs((startTime - endTime)) / (1000);//* 60 * 60 * 24
    return second;
}
//1980-01-12 00:00:00||1980/01/12 00:00:00||1980-01-12T00:00:00
DateHelper.dateToModel = function (str) {
    var ref = this;
    str = str.replace(/\//ig, "-").replace("T", " ");
    var model = ref.getModel();
    var date = str.split(' ')[0];
    var time = str.split(' ')[1];
    model.year = date.split('-')[0];
    model.month = date.split('-')[1];
    model.day = date.split('-')[2];
    model.hour = time.split(':')[0];
    model.minute = time.split(':')[1];
    model.second = time.split(':')[2];
    return model;
}
//模型即可用于描述时间,也可用于描述间隔
DateHelper.getModel = function () {
    var model = { year: 0, month: 0, day: 0, hour: 0, minute: 0, second: 0 };
    model.isHasTime = function () {
        return (this.year > 0 || this.month > 0 || this.day > 0 || this.hour > 0 || this.minute > 0 || this.second > 0);
    }
    return model;
}