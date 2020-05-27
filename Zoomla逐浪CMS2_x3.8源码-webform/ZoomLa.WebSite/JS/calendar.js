document.write("<div align='center' id=meizzCalendarLayer style='BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 5px; Z-INDEX: 999999; BACKGROUND: #ffffff; FILTER: Alpha(opacity=85); LEFT: 205px; PADDING-BOTTOM: 5px; BORDER-LEFT: #000000 1px solid; WIDTH: 215px; LINE-HEIGHT: 5px; PADDING-TOP: 5px; BORDER-BOTTOM: #000000 1px solid; POSITION: absolute; TOP: 220px; HEIGHT: 215px; display: none'>");
document.write("<iframe name=meizzCalendarIframe scrolling=no frameborder=0 width=100% height=100%></iframe></div>");
function writeIframe()
{
    var strIframe = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=gb2312'><style>"+
    "*{font-size: 12px; font-family: 宋体}"+
    "</style></head><body onselectstart='return false' style='margin: 0px' oncontextmenu='return false'><form name=meizz>";
    if (WebCalendar.drag){ strIframe += "<scr"+"ipt language=javascript>"+
    "var drag=false, cx=0, cy=0, o = parent.WebCalendar.calendar; function document.onmousemove(){"+
    "if(parent.WebCalendar.drag && drag){if(o.style.left=='')o.style.left=0; if(o.style.top=='')o.style.top=0;"+
    "o.style.left = parseInt(o.style.left) + window.event.clientX-cx;"+
    "o.style.top = parseInt(o.style.top) + window.event.clientY-cy;}}"+
    "function document.onkeydown(){ switch(window.event.keyCode){ case 27 : parent.hiddenCalendar(); break;"+
    "case 37 : parent.prevM(); break; case 38 : parent.prevY(); break; case 39 : parent.nextM(); break; case 40 : parent.nextY(); break;"+
    "case 84 : document.forms[0].today.click(); break;} window.event.keyCode = 0; window.event.returnValue= false;}"+
    "function dragStart(){cx=window.event.clientX; cy=window.event.clientY; drag=true;}</scr"+"ipt>"}
    strIframe += "<select name=tmpYearSelect onblur='parent.hiddenSelect(this)' style='z-index:1;position:absolute;top:3;left:40;display:none'"+
    " onchange='parent.WebCalendar.thisYear =this.value; parent.hiddenSelect(this); parent.writeCalendar();'></select>"+
    "<select name=tmpMonthSelect onblur='parent.hiddenSelect(this)' style='z-index:1; position:absolute;top:3;left:110;display:none'"+
    " onchange='parent.WebCalendar.thisMonth=this.value; parent.hiddenSelect(this); parent.writeCalendar();'></select>"+
    "<select name=tmpHourSelect onblur='parent.hiddenSelect(this)' style='z-index:1; position:absolute;top:179;left:35;display:none'"+
    " onchange='parent.WebCalendar.thisHour=this.value; parent.hiddenSelect(this); parent.writeCalendar();'></select>"+
    "<select name=tmpMinuteSelect onblur='parent.hiddenSelect(this)' style='z-index:1; position:absolute;top:179;left:80;display:none'"+
    " onchange='parent.WebCalendar.thisMinute=this.value; parent.hiddenSelect(this); parent.writeCalendar();'></select>"+
    "<select name=tmpSecondSelect onblur='parent.hiddenSelect(this)' style='z-index:1; position:absolute;top:179;left:125;display:none'"+
    " onchange='parent.WebCalendar.thisSecond=this.value; parent.hiddenSelect(this); parent.writeCalendar();'></select>"+
    "<table id=tableMain border='1' cellpadding='0' cellspacing='0' bordercolor='#C0D0E8' bordercolorlight='#C0D0E8' bordercolordark='#C0D0E8' width='100%' height='100%'>"+
    "<tr><td height=19 bgcolor='"+ WebCalendar.lightColor +"'>"+
    "    <table width='98%' id=tableHead border=0 cellspacing=1 cellpadding=0 align='center'><tr align=center>"+
    "    <td title='向前翻 1 年' onclick='parent.prevY()' style='cursor: hand' class=bg><<</td>"+
    "    <td width=15 height=19 class=bg title='向前翻 1 月' style='cursor: hand' onclick='parent.prevM()'><</td>"+
    "    <td width=60 id=meizzYearHead title='点击此处选择年份' onclick='parent.funYearSelect(parseInt(this.innerText, 10))'"+
    "        onmouseover='this.bgColor=parent.WebCalendar.darkColor; this.style.color=parent.WebCalendar.lightColor'"+
    "        onmouseout='this.bgColor=parent.WebCalendar.lightColor; this.style.color=parent.WebCalendar.wordColor'></td>"+
    "    <td width=50 id=meizzYearMonth title='点击此处选择月份' onclick='parent.funMonthSelect(parseInt(this.innerText, 10))'"+
    "        onmouseover='this.bgColor=parent.WebCalendar.darkColor; this.style.color=parent.WebCalendar.lightColor'"+
    "        onmouseout='this.bgColor=parent.WebCalendar.lightColor; this.style.color=parent.WebCalendar.wordColor'></td>"+
    "    <td width=15 class=bg title='向后翻 1 月' onclick='parent.nextM()' style='cursor: hand'>></td>"+
    "    <td title='向后翻 1 年' onclick='parent.nextY()' style='cursor: hand' class=bg>>></td>"+
    "</tr></table>"+
    "</td></tr><tr><td height=20 align='center'><table id=tableWeek border=1 width='98%' cellpadding=0 cellspacing=0 ";
    if(WebCalendar.drag){strIframe += "onmousedown='dragStart()' onmouseup='drag=false' onmouseout='drag=false'";}
    strIframe += " borderColorLight='#FFFFFF' borderColorDark='#FFFFFF'>"+
    "    <tr align=center bgcolor='#6699FF'><td height=20><font color='#FF0000'>日</font></td><td>一</td><td>二</td><td>三</td><td>四</td><td>五</td><td><font color='#FF0000'>六</font></td></tr></table>"+
    "</td></tr><tr><td valign=top align='center' bgcolor='"+ WebCalendar.lightColor +"'>"+
    "    <table id=tableDay height='100%' width='98%' border=0 cellspacing=1 cellpadding=2>";
         for(var x=0; x<5; x++){ strIframe += "<tr>";
         for(var y=0; y<7; y++) strIframe += "<td class=out id='meizzDay"+ (x*7+y) +"'></td>"; strIframe += "</tr>";}
         strIframe += "<tr>";
         for(var x=35; x<39; x++) strIframe += "<td class=out id='meizzDay"+ x +"'></td>";
         strIframe +="<td colspan=3 class=out title='"+ WebCalendar.regInfo +"'><input style=' background-color: "+
         WebCalendar.btnBgColor +";cursor: hand; padding-top: 4px; width: 100%; height: 100%; border: 0' onfocus='this.blur()'"+
         " type=button value='    关闭' onclick='parent.hiddenCalendar()'></td></tr></table>"+
    "</td></tr><tr><td height=20 align='center' bgcolor='"+ WebCalendar.lightColor +"'>"+
    "    <table border=0 cellpadding=1 cellspacing=0 width='98%'>"+
    "    <tr>"+
    "        <td width=15 height=19 title='向前翻 1 小时' onclick='parent.prevH()' style='cursor: hand' class=bg><<</td>"+
    "        <td width=15 height=19 title='向前翻 1 分钟' onclick='parent.prevMi()' style='cursor: hand' class=bg><</td>"+
    "        <td width=60 id=meizzHourHead title='点击此处选择小时' onclick='parent.funHourSelect(parseInt(this.innerText, 10))'"+
    "        onmouseover='this.bgColor=parent.WebCalendar.darkColor; this.style.color=parent.WebCalendar.lightColor'"+
    "        onmouseout='this.bgColor=parent.WebCalendar.lightColor; this.style.color=parent.WebCalendar.wordColor'></td>"+
    "    <td width=50 id=meizzMinuteHead title='点击此处选择分钟' onclick='parent.funMinuteSelect(parseInt(this.innerText, 10))'"+
    "        onmouseover='this.bgColor=parent.WebCalendar.darkColor; this.style.color=parent.WebCalendar.lightColor'"+
    "        onmouseout='this.bgColor=parent.WebCalendar.lightColor; this.style.color=parent.WebCalendar.wordColor'></td>"+
    "    <td width=50 id=meizzSecondHead title='点击此处选择秒' onclick='parent.funSecondSelect(parseInt(this.innerText, 10))'"+
    "        onmouseover='this.bgColor=parent.WebCalendar.darkColor; this.style.color=parent.WebCalendar.lightColor'"+
    "        onmouseout='this.bgColor=parent.WebCalendar.lightColor; this.style.color=parent.WebCalendar.wordColor'></td>"+
    "        <td title='向后翻 1 分钟' onclick='parent.nextMi()' style='cursor: hand' class=bg>></td>"+
    "        <td title='向后翻 1 小时' onclick='parent.nextH()' style='cursor: hand' class=bg>>></td>"+
    "</tr></table>"+
    "</td></tr><table></form></body></html>";
    with(WebCalendar.iframe)
    {
        document.writeln(strIframe); document.close();
        for(var i=0; i<39; i++)
        {
            WebCalendar.dayObj[i] = eval("meizzDay"+ i);
            WebCalendar.dayObj[i].onmouseover = dayMouseOver;
            WebCalendar.dayObj[i].onmouseout = dayMouseOut;
            WebCalendar.dayObj[i].onclick     = returnDate;
        }
    }
}
function WebCalendar() //初始化日历的设置
{
    this.daysMonth = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
    this.day        = new Array(39);            //定义日历展示用的数组
    this.dayObj     = new Array(39);            //定义日期展示控件数组
    this.dateStyle = null;                     //保存格式化后日期数组
    this.objExport = null;                     //日历回传的显示控件
    this.eventSrc   = null;                     //日历显示的触发控件
    this.inputDate = null;                     //转化外的输入的日期(d/m/yyyy)
    this.thisYear   = new Date().getFullYear(); //定义年的变量的初始值
    this.thisMonth = new Date().getMonth()+ 1; //定义月的变量的初始值
    this.thisDay    = new Date().getDate();     //定义日的变量的初始值
    this.thisHour   = 00;    //定义小时得变量初始值
    this.thisMinute = 00; //定义分钟得变量初始值
    this.thisSecond = 00; //定义秒得标量的初始值
    this.today      = this.thisDay +"/"+ this.thisMonth +"/"+ this.thisYear;   //今天(d/m/yyyy)
    this.iframe     = window.frames["meizzCalendarIframe"]; //日历的 iframe 载体
    this.calendar   = getObjectById("meizzCalendarLayer"); //日历的层
    this.dateReg    = "";           //日历格式验证的正则式
    this.yearFall   = 50;           //定义年下拉框的年差值
    this.format     = "yyyy-mm-dd"; //回传日期的格式
    this.timeShow   = true;        //是否返回时间
    this.drag       = false;         //是否允许拖动
    this.darkColor = "#0000D0";    //控件的暗色
    this.lightColor = "#FFFFFF";    //控件的亮色
    this.btnBgColor = "#E6E6FA";    //控件的按钮背景色
    this.wordColor = "#000080";    //控件的文字颜色
    this.wordDark   = "#DCDCDC";    //控件的暗文字颜色
    this.dayBgColor = "#F5F5FA";    //日期数字背景色
    this.todayColor = "#FF0000";    //今天在日历上的标示背景色
    this.DarkBorder = "#D4D0C8";    //日期显示的立体表达色
}   var WebCalendar = new WebCalendar();
function calendar() //主调函数
{
    var e = window.event.srcElement;   writeIframe();
    var o = WebCalendar.calendar.style; WebCalendar.eventSrc = e;
if (arguments.length == 0) WebCalendar.objExport = e;
    else WebCalendar.objExport = eval(arguments[0]);
    WebCalendar.iframe.tableWeek.style.cursor = WebCalendar.drag ? "move" : "default";
var t = e.offsetTop, h = e.clientHeight, l = e.offsetLeft, p = e.type;
while (e = e.offsetParent){t += e.offsetTop; l += e.offsetLeft;}
    o.display = ""; WebCalendar.iframe.document.body.focus();
    var cw = WebCalendar.calendar.clientWidth, ch = WebCalendar.calendar.clientHeight;
    var dw = document.body.clientWidth, dl = document.body.scrollLeft, dt = document.body.scrollTop;
    
    if (document.body.clientHeight + dt - t - h >= ch) o.top = (p=="image")? t + h : t + h + 6;
    else o.top = (t - dt < ch) ? ((p=="image")? t + h : t + h + 6) : t - ch;
    if (dw + dl - l >= cw) o.left = l; else o.left = (dw >= cw) ? dw - cw + dl : dl;
    if (!WebCalendar.timeShow) WebCalendar.dateReg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
    else WebCalendar.dateReg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
    try{
        if (WebCalendar.objExport.value.trim() != ""){
            WebCalendar.dateStyle = WebCalendar.objExport.value.trim().match(WebCalendar.dateReg);
            if (WebCalendar.dateStyle == null)
            {
                WebCalendar.thisYear   = new Date().getFullYear();
                WebCalendar.thisMonth = new Date().getMonth()+ 1;
                WebCalendar.thisDay    = new Date().getDate();
                //alert("原文本框里的日期有错误！\n可能与你定义的显示时分秒有冲突！");
                writeCalendar(); return false;
            }
            else
            {
                WebCalendar.thisYear   = parseInt(WebCalendar.dateStyle[1], 10);
                WebCalendar.thisMonth = parseInt(WebCalendar.dateStyle[3], 10);
                WebCalendar.thisDay    = parseInt(WebCalendar.dateStyle[4], 10);
                WebCalendar.inputDate = parseInt(WebCalendar.thisDay, 10) +"/"+ parseInt(WebCalendar.thisMonth, 10) +"/"+ 
                parseInt(WebCalendar.thisYear, 10); writeCalendar();
            }
        } else writeCalendar();
    } catch(e){writeCalendar();}
}
function funMonthSelect() //月份的下拉框
{
    var m = isNaN(parseInt(WebCalendar.thisMonth, 10)) ? new Date().getMonth() + 1 : parseInt(WebCalendar.thisMonth);
    var e = WebCalendar.iframe.document.forms[0].tmpMonthSelect;
    for (var i=1; i<13; i++) e.options.add(new Option(i +"月", i));
    e.style.display = ""; e.value = m; e.focus(); window.status = e.style.top;
}
function funYearSelect() //年份的下拉框
{
    var n = WebCalendar.yearFall;
    var e = WebCalendar.iframe.document.forms[0].tmpYearSelect;
    var y = isNaN(parseInt(WebCalendar.thisYear, 10)) ? new Date().getFullYear() : parseInt(WebCalendar.thisYear);
        y = (y <= 1000)? 1000 : ((y >= 9999)? 9999 : y);
    var min = (y - n >= 1000) ? y - n : 1000;
    var max = (y + n <= 9999) ? y + n : 9999;
        min = (max == 9999) ? max-n*2 : min;
        max = (min == 1000) ? min+n*2 : max;
    for (var i=min; i<=max; i++) e.options.add(new Option(i +"年", i));
    e.style.display = ""; e.value = y; e.focus();
}

function funHourSelect() //小时的下拉框
{
    var e = WebCalendar.iframe.document.forms[0].tmpHourSelect;
    var h = isNaN(parseInt(WebCalendar.thisHour, 10)) ? new Date().getHours() : parseInt(WebCalendar.thisHour);
    for (var i=0; i<=23; i++) e.options.add(new Option(appendZero(i) +"时", i));
    e.style.display = ""; e.value = h; e.focus();
}

function funMinuteSelect() //分钟的下拉框
{
    var e = WebCalendar.iframe.document.forms[0].tmpMinuteSelect;
    var mi = isNaN(parseInt(WebCalendar.thisMinute, 10)) ? new Date().getMinutes() : parseInt(WebCalendar.thisMinute);
    for (var i=0; i<=59; i++) e.options.add(new Option(appendZero(i) +"分", i));
    e.style.display = ""; e.value = mi; e.focus();
}

function funSecondSelect() //秒的下拉框
{
    var e = WebCalendar.iframe.document.forms[0].tmpSecondSelect;
    var s = isNaN(parseInt(WebCalendar.thisSecond, 10)) ? new Date().getSecondes() : parseInt(WebCalendar.thisSecond);
    for (var i=0; i<=59; i++) e.options.add(new Option(appendZero(i) +"秒", i));
    e.style.display = ""; e.value = s; e.focus();
}

function prevM() //往前翻月份
{
    WebCalendar.thisDay = 1;
    if (WebCalendar.thisMonth==1)
    {
        WebCalendar.thisYear--;
        WebCalendar.thisMonth=13;
    }
    WebCalendar.thisMonth--; writeCalendar();
}
function nextM() //往后翻月份
{
    WebCalendar.thisDay = 1;
    if (WebCalendar.thisMonth==12)
    {
        WebCalendar.thisYear++;
        WebCalendar.thisMonth=0;
    }
    WebCalendar.thisMonth++; writeCalendar();
}

function prevH()   //往前翻小时
{
    WebCalendar.thisHour--;
    if(WebCalendar.thisHour==-1)
    {
         WebCalendar.thisHour=23;
    }
    writeCalendar();
}
function nextH()   //往后翻小时
{ 
    WebCalendar.thisHour++;
    if(WebCalendar.thisHour==24)
    {
         WebCalendar.thisHour=0;
    }
    writeCalendar();
}


function prevMi()   //往前翻小时
{
    WebCalendar.thisMinute--;
    if(WebCalendar.thisMinute==-1)
    {
         WebCalendar.thisMinute=59;
    }
    writeCalendar();
}
function nextMi()   //往后翻小时
{ 
    WebCalendar.thisMinute++;
    if(WebCalendar.thisMinute==60)
    {
         WebCalendar.thisMinute=0;
    }
    writeCalendar();
}


function prevY(){WebCalendar.thisDay = 1; WebCalendar.thisYear--; writeCalendar();}//往前翻 Year
function nextY(){WebCalendar.thisDay = 1; WebCalendar.thisYear++; writeCalendar();}//往后翻 Year
function hiddenSelect(e){for(var i=e.options.length; i>-1; i--)e.options.remove(i); e.style.display="none";}
function getObjectById(id){ if(document.all) return(eval("document.all."+ id)); return(eval(id)); }
function hiddenCalendar(){getObjectById("meizzCalendarLayer").style.display = "none";};

//function appendZero(n){return(("00"+ n).substr(("00"+ n).length-2));}//日期自动补零程序
function appendZero(n){return(n);}

String.prototype.trim=function(){return this.replace(/(^\s*)|(\s*$)/g,"");}
function dayMouseOver()
{
    this.className = "over";
    this.style.backgroundColor = WebCalendar.darkColor;
    if(WebCalendar.day[this.id.substr(8)].split("/")[1] == WebCalendar.thisMonth)
    this.style.color = WebCalendar.lightColor;
}
function dayMouseOut()
{
    this.className = "out"; var d = WebCalendar.day[this.id.substr(8)], a = d.split("/");
    this.style.removeAttribute('backgroundColor');
    if(a[1] == WebCalendar.thisMonth && d != WebCalendar.today)
    {
        if(WebCalendar.dateStyle && a[0] == parseInt(WebCalendar.dateStyle[4], 10))
        this.style.color = WebCalendar.lightColor;
        this.style.color = WebCalendar.wordColor;
    }
}
function writeCalendar() //对日历显示的数据的处理程序
{
    var y = WebCalendar.thisYear;
    var m = WebCalendar.thisMonth; 
    var d = WebCalendar.thisDay;
    var h = WebCalendar.thisHour;
    var mi = WebCalendar.thisMinute;
    var s = WebCalendar.thisSecond;
    WebCalendar.daysMonth[1] = (0==y%4 && (y%100!=0 || y%400==0)) ? 29 : 28;
    if (!(y<=9999 && y >= 1000 && parseInt(m, 10)>0 && parseInt(m, 10)<13 && parseInt(d, 10)>0)){
        //alert("对不起，你输入了错误的日期！");
        WebCalendar.thisYear   = new Date().getFullYear();
        WebCalendar.thisMonth = new Date().getMonth()+ 1;
        WebCalendar.thisDay    = new Date().getDate(); 
        WebCalendar.thisHour   = new Date().getHours();    
        WebCalendar.thisMinute = new Date().getMinutes();
        WebCalendar.thisSecond = new Date().getSeconds(); 
        }
        y = WebCalendar.thisYear;
        m = WebCalendar.thisMonth;
        d = WebCalendar.thisDay;
        h = WebCalendar.thisHour;
        mi = WebCalendar.thisMinute;
        s = WebCalendar.thisSecond;

    WebCalendar.iframe.meizzYearHead.innerText = y +" 年";
    WebCalendar.iframe.meizzYearMonth.innerText = parseInt(m, 10) +" 月";
    WebCalendar.iframe.meizzHourHead.innerText =appendZero(parseInt(h, 10)) +" 时";
    WebCalendar.iframe.meizzMinuteHead.innerText =appendZero(parseInt(mi, 10)) +" 分";
    WebCalendar.iframe.meizzSecondHead.innerText =appendZero(parseInt(s, 10))+" 秒";

    WebCalendar.daysMonth[1] = (0==y%4 && (y%100!=0 || y%400==0)) ? 29 : 28; //闰年二月为29天
    var w = new Date(y, m-1, 1).getDay();
    var prevDays = m==1 ? WebCalendar.daysMonth[11] : WebCalendar.daysMonth[m-2];
    for(var i=(w-1); i>=0; i--) //这三个 for 循环为日历赋数据源（数组 WebCalendar.day）格式是 d/m/yyyy
    {
        WebCalendar.day[i] = prevDays +"/"+ (parseInt(m, 10)-1) +"/"+ y;
        if(m==1) WebCalendar.day[i] = prevDays +"/"+ 12 +"/"+ (parseInt(y, 10)-1);
        prevDays--;
    }
    for(var i=1; i<=WebCalendar.daysMonth[m-1]; i++) WebCalendar.day[i+w-1] = i +"/"+ m +"/"+ y;
    for(var i=1; i<39-w-WebCalendar.daysMonth[m-1]+1; i++)
    {
        WebCalendar.day[WebCalendar.daysMonth[m-1]+w-1+i] = i +"/"+ (parseInt(m, 10)+1) +"/"+ y;
        if(m==12) WebCalendar.day[WebCalendar.daysMonth[m-1]+w-1+i] = i +"/"+ 1 +"/"+ (parseInt(y, 10)+1);
    }
    for(var i=0; i<39; i++)    //这个循环是根据源数组写到日历里显示
    {
        var a = WebCalendar.day[i].split("/");
        WebCalendar.dayObj[i].innerText = a[0];
        WebCalendar.dayObj[i].style.textAlign = 'center';
        WebCalendar.dayObj[i].title        = a[2] +"-"+ appendZero(a[1]) +"-"+ appendZero(a[0]);
        WebCalendar.dayObj[i].bgColor      = WebCalendar.dayBgColor;
        WebCalendar.dayObj[i].style.color = WebCalendar.wordColor;
        if ((i<10 && parseInt(WebCalendar.day[i], 10)>20) || (i>27 && parseInt(WebCalendar.day[i], 10)<12))
            WebCalendar.dayObj[i].style.color = WebCalendar.wordDark;
        if (WebCalendar.inputDate==WebCalendar.day[i])    //设置输入框里的日期在日历上的颜色
        {WebCalendar.dayObj[i].bgColor = WebCalendar.darkColor; WebCalendar.dayObj[i].style.color = WebCalendar.lightColor;}
        if (WebCalendar.day[i] == WebCalendar.today)      //设置今天在日历上反应出来的颜色
        {WebCalendar.dayObj[i].bgColor = WebCalendar.todayColor; WebCalendar.dayObj[i].style.color = WebCalendar.lightColor;}
    }
}
function returnDate() //根据日期格式等返回用户选定的日期
{
    if(WebCalendar.objExport)
    {
        var returnValue;
        var a = (arguments.length==0) ? WebCalendar.day[this.id.substr(8)].split("/") : arguments[0].split("/");
        var d = WebCalendar.format.match(/^(\w{4})(-|\/)(\w{1,2})\2(\w{1,2})$/);
        if (d == null) {
            //alert("你设定的日期输出格式不对！\r\n\r\n请重新定义 WebCalendar.format ！");
         return false;}

        var flag = d[3].length==2 || d[4].length==2; //判断返回的日期格式是否要补零
        returnValue = flag ? a[2] +d[2]+ appendZero(a[1]) +d[2]+ appendZero(a[0]) : a[2] +d[2]+ a[1] +d[2]+ a[0];

        if(WebCalendar.timeShow)
        {
            var h = WebCalendar.thisHour, m = WebCalendar.thisMinute, s = WebCalendar.thisSecond;
            returnValue += flag ? " "+ appendZero(h) +":"+ appendZero(m) +":"+ appendZero(s) : " "+ h +":"+ m +":"+ s;
        }
        WebCalendar.objExport.value = returnValue;
        hiddenCalendar();
    }
}

document.onclick=function()
{
    if(WebCalendar.eventSrc != window.event.srcElement) hiddenCalendar();
}

