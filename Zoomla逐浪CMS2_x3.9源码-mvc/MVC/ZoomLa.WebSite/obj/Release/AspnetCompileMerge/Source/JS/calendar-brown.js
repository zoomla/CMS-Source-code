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
    "    <table id=tableDay height='100%' width='98%' border=0 cellspacing=1 cellpadding=0>";
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
    this.thisHour   = new Date().getHours();    //定义小时得变量初始值
    this.thisMinute = new Date().getMinutes(); //定义分钟得变量初始值
    this.thisSecond = new Date().getSeconds(); //定义秒得标量的初始值
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
                alert("原文本框里的日期有错误！\n可能与你定义的显示时分秒有冲突！");
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

 String.prototype.trim=function(){return this.replace(/(^\s*)|(\s*$)/g,"");
}
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
        alert("对不起，你输入了错误的日期！");
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
        WebCalendar.dayObj[i].innerText    = a[0];
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
        if(d==null){alert("你设定的日期输出格式不对！\r\n\r\n请重新定义 WebCalendar.format ！"); return false;}

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

Calendar = function (firstDayOfWeek, dateStr, onSelected, onClose) {
	// member variables
	this.activeDiv = null;
	this.currentDateEl = null;
	this.getDateStatus = null;
	this.getDateToolTip = null;
	this.getDateText = null;
	this.timeout = null;
	this.onSelected = onSelected || null;
	this.onClose = onClose || null;
	this.dragging = false;
	this.hidden = false;
	this.minYear = 1970;
	this.maxYear = 2050;
	this.dateFormat = Calendar._TT["DEF_DATE_FORMAT"];
	this.ttDateFormat = Calendar._TT["TT_DATE_FORMAT"];
	this.isPopup = true;
	this.weekNumbers = true;
	this.firstDayOfWeek = typeof firstDayOfWeek == "number" ? firstDayOfWeek : Calendar._FD; // 0 for Sunday, 1 for Monday, etc.
	this.showsOtherMonths = false;
	this.dateStr = dateStr;
	this.ar_days = null;
	this.showsTime = false;
	this.time24 = true;
	this.yearStep = 2;
	this.hiliteToday = true;
	this.multiple = null;
	// HTML elements
	this.table = null;
	this.element = null;
	this.tbody = null;
	this.firstdayname = null;
	// Combo boxes
	this.monthsCombo = null;
	this.yearsCombo = null;
	this.hilitedMonth = null;
	this.activeMonth = null;
	this.hilitedYear = null;
	this.activeYear = null;
	// Information
	this.dateClicked = false;

	// one-time initializations
	if (typeof Calendar._SDN == "undefined") {
		// table of short day names
		if (typeof Calendar._SDN_len == "undefined")
			Calendar._SDN_len = 3;
		var ar = new Array();
		for (var i = 8; i > 0;) {
			ar[--i] = Calendar._DN[i].substr(0, Calendar._SDN_len);
		}
		Calendar._SDN = ar;
		// table of short month names
		if (typeof Calendar._SMN_len == "undefined")
			Calendar._SMN_len = 3;
		ar = new Array();
		for (var i = 12; i > 0;) {
			ar[--i] = Calendar._MN[i].substr(0, Calendar._SMN_len);
		}
		Calendar._SMN = ar;
	}
};

// ** constants

/// "static", needed for event handlers.
Calendar._C = null;

/// detect a special case of "web browser"
Calendar.is_ie = ( /msie/i.test(navigator.userAgent) &&
		   !/opera/i.test(navigator.userAgent) );

Calendar.is_ie5 = ( Calendar.is_ie && /msie 5\.0/i.test(navigator.userAgent) );

/// detect Opera browser
Calendar.is_opera = /opera/i.test(navigator.userAgent);

/// detect KHTML-based browsers
Calendar.is_khtml = /Konqueror|Safari|KHTML/i.test(navigator.userAgent);

// BEGIN: UTILITY FUNCTIONS; beware that these might be moved into a separate
//        library, at some point.

Calendar.getAbsolutePos = function(el) {
	var SL = 0, ST = 0;
	var is_div = /^div$/i.test(el.tagName);
	if (is_div && el.scrollLeft)
		SL = el.scrollLeft;
	if (is_div && el.scrollTop)
		ST = el.scrollTop;
	var r = { x: el.offsetLeft - SL, y: el.offsetTop - ST };
	if (el.offsetParent) {
		var tmp = this.getAbsolutePos(el.offsetParent);
		r.x += tmp.x;
		r.y += tmp.y;
	}
	return r;
};

Calendar.isRelated = function (el, evt) {
	var related = evt.relatedTarget;
	if (!related) {
		var type = evt.type;
		if (type == "mouseover") {
			related = evt.fromElement;
		} else if (type == "mouseout") {
			related = evt.toElement;
		}
	}
	while (related) {
		if (related == el) {
			return true;
		}
		related = related.parentNode;
	}
	return false;
};

Calendar.removeClass = function(el, className) {
	if (!(el && el.className)) {
		return;
	}
	var cls = el.className.split(" ");
	var ar = new Array();
	for (var i = cls.length; i > 0;) {
		if (cls[--i] != className) {
			ar[ar.length] = cls[i];
		}
	}
	el.className = ar.join(" ");
};

Calendar.addClass = function(el, className) {
	Calendar.removeClass(el, className);
	el.className += " " + className;
};

// FIXME: the following 2 functions totally suck, are useless and should be replaced immediately.
Calendar.getElement = function(ev) {
	var f = Calendar.is_ie ? window.event.srcElement : ev.currentTarget;
	while (f.nodeType != 1 || /^div$/i.test(f.tagName))
		f = f.parentNode;
	return f;
};

Calendar.getTargetElement = function(ev) {
	var f = Calendar.is_ie ? window.event.srcElement : ev.target;
	while (f.nodeType != 1)
		f = f.parentNode;
	return f;
};

Calendar.stopEvent = function(ev) {
	ev || (ev = window.event);
	if (Calendar.is_ie) {
		ev.cancelBubble = true;
		ev.returnValue = false;
	} else {
		ev.preventDefault();
		ev.stopPropagation();
	}
	return false;
};

Calendar.addEvent = function(el, evname, func) {
	if (el.attachEvent) { // IE
		el.attachEvent("on" + evname, func);
	} else if (el.addEventListener) { // Gecko / W3C
		el.addEventListener(evname, func, true);
	} else {
		el["on" + evname] = func;
	}
};

Calendar.removeEvent = function(el, evname, func) {
	if (el.detachEvent) { // IE
		el.detachEvent("on" + evname, func);
	} else if (el.removeEventListener) { // Gecko / W3C
		el.removeEventListener(evname, func, true);
	} else {
		el["on" + evname] = null;
	}
};

Calendar.createElement = function(type, parent) {
	var el = null;
	if (document.createElementNS) {
		// use the XHTML namespace; IE won't normally get here unless
		// _they_ "fix" the DOM2 implementation.
		el = document.createElementNS("http://www.w3.org/1999/xhtml", type);
	} else {
		el = document.createElement(type);
	}
	if (typeof parent != "undefined") {
		parent.appendChild(el);
	}
	return el;
};

// END: UTILITY FUNCTIONS

// BEGIN: CALENDAR STATIC FUNCTIONS

/** Internal -- adds a set of events to make some element behave like a button. */
Calendar._add_evs = function(el) {
	with (Calendar) {
		addEvent(el, "mouseover", dayMouseOver);
		addEvent(el, "mousedown", dayMouseDown);
		addEvent(el, "mouseout", dayMouseOut);
		if (is_ie) {
			addEvent(el, "dblclick", dayMouseDblClick);
			el.setAttribute("unselectable", true);
		}
	}
};

Calendar.findMonth = function(el) {
	if (typeof el.month != "undefined") {
		return el;
	} else if (typeof el.parentNode.month != "undefined") {
		return el.parentNode;
	}
	return null;
};

Calendar.findYear = function(el) {
	if (typeof el.year != "undefined") {
		return el;
	} else if (typeof el.parentNode.year != "undefined") {
		return el.parentNode;
	}
	return null;
};

Calendar.showMonthsCombo = function () {
	var cal = Calendar._C;
	if (!cal) {
		return false;
	}
	var cal = cal;
	var cd = cal.activeDiv;
	var mc = cal.monthsCombo;
	if (cal.hilitedMonth) {
		Calendar.removeClass(cal.hilitedMonth, "hilite");
	}
	if (cal.activeMonth) {
		Calendar.removeClass(cal.activeMonth, "active");
	}
	var mon = cal.monthsCombo.getElementsByTagName("div")[cal.date.getMonth()];
	Calendar.addClass(mon, "active");
	cal.activeMonth = mon;
	var s = mc.style;
	s.display = "block";
	if (cd.navtype < 0)
		s.left = cd.offsetLeft + "px";
	else {
		var mcw = mc.offsetWidth;
		if (typeof mcw == "undefined")
			// Konqueror brain-dead techniques
			mcw = 50;
		s.left = (cd.offsetLeft + cd.offsetWidth - mcw) + "px";
	}
	s.top = (cd.offsetTop + cd.offsetHeight) + "px";
};

Calendar.showYearsCombo = function (fwd) {
	var cal = Calendar._C;
	if (!cal) {
		return false;
	}
	var cal = cal;
	var cd = cal.activeDiv;
	var yc = cal.yearsCombo;
	if (cal.hilitedYear) {
		Calendar.removeClass(cal.hilitedYear, "hilite");
	}
	if (cal.activeYear) {
		Calendar.removeClass(cal.activeYear, "active");
	}
	cal.activeYear = null;
	var Y = cal.date.getFullYear() + (fwd ? 1 : -1);
	var yr = yc.firstChild;
	var show = false;
	for (var i = 12; i > 0; --i) {
		if (Y >= cal.minYear && Y <= cal.maxYear) {
			yr.innerHTML = Y;
			yr.year = Y;
			yr.style.display = "block";
			show = true;
		} else {
			yr.style.display = "none";
		}
		yr = yr.nextSibling;
		Y += fwd ? cal.yearStep : -cal.yearStep;
	}
	if (show) {
		var s = yc.style;
		s.display = "block";
		if (cd.navtype < 0)
			s.left = cd.offsetLeft + "px";
		else {
			var ycw = yc.offsetWidth;
			if (typeof ycw == "undefined")
				// Konqueror brain-dead techniques
				ycw = 50;
			s.left = (cd.offsetLeft + cd.offsetWidth - ycw) + "px";
		}
		s.top = (cd.offsetTop + cd.offsetHeight) + "px";
	}
};

// event handlers

Calendar.tableMouseUp = function(ev) {
	var cal = Calendar._C;
	if (!cal) {
		return false;
	}
	if (cal.timeout) {
		clearTimeout(cal.timeout);
	}
	var el = cal.activeDiv;
	if (!el) {
		return false;
	}
	var target = Calendar.getTargetElement(ev);
	ev || (ev = window.event);
	Calendar.removeClass(el, "active");
	if (target == el || target.parentNode == el) {
		Calendar.cellClick(el, ev);
	}
	var mon = Calendar.findMonth(target);
	var date = null;
	if (mon) {
		date = new Date(cal.date);
		if (mon.month != date.getMonth()) {
			date.setMonth(mon.month);
			cal.setDate(date);
			cal.dateClicked = false;
			cal.callHandler();
		}
	} else {
		var year = Calendar.findYear(target);
		if (year) {
			date = new Date(cal.date);
			if (year.year != date.getFullYear()) {
				date.setFullYear(year.year);
				cal.setDate(date);
				cal.dateClicked = false;
				cal.callHandler();
			}
		}
	}
	with (Calendar) {
		removeEvent(document, "mouseup", tableMouseUp);
		removeEvent(document, "mouseover", tableMouseOver);
		removeEvent(document, "mousemove", tableMouseOver);
		cal._hideCombos();
		_C = null;
		return stopEvent(ev);
	}
};

Calendar.tableMouseOver = function (ev) {
	var cal = Calendar._C;
	if (!cal) {
		return;
	}
	var el = cal.activeDiv;
	var target = Calendar.getTargetElement(ev);
	if (target == el || target.parentNode == el) {
		Calendar.addClass(el, "hilite active");
		Calendar.addClass(el.parentNode, "rowhilite");
	} else {
		if (typeof el.navtype == "undefined" || (el.navtype != 50 && (el.navtype == 0 || Math.abs(el.navtype) > 2)))
			Calendar.removeClass(el, "active");
		Calendar.removeClass(el, "hilite");
		Calendar.removeClass(el.parentNode, "rowhilite");
	}
	ev || (ev = window.event);
	if (el.navtype == 50 && target != el) {
		var pos = Calendar.getAbsolutePos(el);
		var w = el.offsetWidth;
		var x = ev.clientX;
		var dx;
		var decrease = true;
		if (x > pos.x + w) {
			dx = x - pos.x - w;
			decrease = false;
		} else
			dx = pos.x - x;

		if (dx < 0) dx = 0;
		var range = el._range;
		var current = el._current;
		var count = Math.floor(dx / 10) % range.length;
		for (var i = range.length; --i >= 0;)
			if (range[i] == current)
				break;
		while (count-- > 0)
			if (decrease) {
				if (--i < 0)
					i = range.length - 1;
			} else if ( ++i >= range.length )
				i = 0;
		var newval = range[i];
		el.innerHTML = newval;

		cal.onUpdateTime();
	}
	var mon = Calendar.findMonth(target);
	if (mon) {
		if (mon.month != cal.date.getMonth()) {
			if (cal.hilitedMonth) {
				Calendar.removeClass(cal.hilitedMonth, "hilite");
			}
			Calendar.addClass(mon, "hilite");
			cal.hilitedMonth = mon;
		} else if (cal.hilitedMonth) {
			Calendar.removeClass(cal.hilitedMonth, "hilite");
		}
	} else {
		if (cal.hilitedMonth) {
			Calendar.removeClass(cal.hilitedMonth, "hilite");
		}
		var year = Calendar.findYear(target);
		if (year) {
			if (year.year != cal.date.getFullYear()) {
				if (cal.hilitedYear) {
					Calendar.removeClass(cal.hilitedYear, "hilite");
				}
				Calendar.addClass(year, "hilite");
				cal.hilitedYear = year;
			} else if (cal.hilitedYear) {
				Calendar.removeClass(cal.hilitedYear, "hilite");
			}
		} else if (cal.hilitedYear) {
			Calendar.removeClass(cal.hilitedYear, "hilite");
		}
	}
	return Calendar.stopEvent(ev);
};

Calendar.tableMouseDown = function (ev) {
	if (Calendar.getTargetElement(ev) == Calendar.getElement(ev)) {
		return Calendar.stopEvent(ev);
	}
};

Calendar.calDragIt = function (ev) {
	var cal = Calendar._C;
	if (!(cal && cal.dragging)) {
		return false;
	}
	var posX;
	var posY;
	if (Calendar.is_ie) {
		posY = window.event.clientY + document.body.scrollTop;
		posX = window.event.clientX + document.body.scrollLeft;
	} else {
		posX = ev.pageX;
		posY = ev.pageY;
	}
	cal.hideShowCovered();
	var st = cal.element.style;
	st.left = (posX - cal.xOffs) + "px";
	st.top = (posY - cal.yOffs) + "px";
	return Calendar.stopEvent(ev);
};

Calendar.calDragEnd = function (ev) {
	var cal = Calendar._C;
	if (!cal) {
		return false;
	}
	cal.dragging = false;
	with (Calendar) {
		removeEvent(document, "mousemove", calDragIt);
		removeEvent(document, "mouseup", calDragEnd);
		tableMouseUp(ev);
	}
	cal.hideShowCovered();
};

Calendar.dayMouseDown = function(ev) {
	var el = Calendar.getElement(ev);
	if (el.disabled) {
		return false;
	}
	var cal = el.calendar;
	cal.activeDiv = el;
	Calendar._C = cal;
	if (el.navtype != 300) with (Calendar) {
		if (el.navtype == 50) {
			el._current = el.innerHTML;
			addEvent(document, "mousemove", tableMouseOver);
		} else
			addEvent(document, Calendar.is_ie5 ? "mousemove" : "mouseover", tableMouseOver);
		addClass(el, "hilite active");
		addEvent(document, "mouseup", tableMouseUp);
	} else if (cal.isPopup) {
		cal._dragStart(ev);
	}
	if (el.navtype == -1 || el.navtype == 1) {
		if (cal.timeout) clearTimeout(cal.timeout);
		cal.timeout = setTimeout("Calendar.showMonthsCombo()", 250);
	} else if (el.navtype == -2 || el.navtype == 2) {
		if (cal.timeout) clearTimeout(cal.timeout);
		cal.timeout = setTimeout((el.navtype > 0) ? "Calendar.showYearsCombo(true)" : "Calendar.showYearsCombo(false)", 250);
	} else {
		cal.timeout = null;
	}
	return Calendar.stopEvent(ev);
};

Calendar.dayMouseDblClick = function(ev) {
	Calendar.cellClick(Calendar.getElement(ev), ev || window.event);
	if (Calendar.is_ie) {
		document.selection.empty();
	}
};

Calendar.dayMouseOver = function(ev) {
	var el = Calendar.getElement(ev);
	if (Calendar.isRelated(el, ev) || Calendar._C || el.disabled) {
		return false;
	}
	if (el.ttip) {
		if (el.ttip.substr(0, 1) == "_") {
			el.ttip = el.caldate.print(el.calendar.ttDateFormat) + el.ttip.substr(1);
		}
		el.calendar.tooltips.innerHTML = el.ttip;
	}
	if (el.navtype != 300) {
		Calendar.addClass(el, "hilite");
		if (el.caldate) {
			Calendar.addClass(el.parentNode, "rowhilite");
		}
	}
	return Calendar.stopEvent(ev);
};

Calendar.dayMouseOut = function(ev) {
	with (Calendar) {
		var el = getElement(ev);
		if (isRelated(el, ev) || _C || el.disabled)
			return false;
		removeClass(el, "hilite");
		if (el.caldate)
			removeClass(el.parentNode, "rowhilite");
		if (el.calendar)
			el.calendar.tooltips.innerHTML = _TT["SEL_DATE"];
		return stopEvent(ev);
	}
};

/**
 *  A generic "click" handler :) handles all types of buttons defined in this
 *  calendar.
 */
Calendar.cellClick = function(el, ev) {
	var cal = el.calendar;
	var closing = false;
	var newdate = false;
	var date = null;
	if (typeof el.navtype == "undefined") {
		if (cal.currentDateEl) {
			Calendar.removeClass(cal.currentDateEl, "selected");
			Calendar.addClass(el, "selected");
			closing = (cal.currentDateEl == el);
			if (!closing) {
				cal.currentDateEl = el;
			}
		}
		cal.date.setDateOnly(el.caldate);
		date = cal.date;
		var other_month = !(cal.dateClicked = !el.otherMonth);
		if (!other_month && !cal.currentDateEl)
			cal._toggleMultipleDate(new Date(date));
		else
			newdate = !el.disabled;
		// a date was clicked
		if (other_month)
			cal._init(cal.firstDayOfWeek, date);
	} else {
		if (el.navtype == 200) {
			Calendar.removeClass(el, "hilite");
			cal.callCloseHandler();
			return;
		}
		date = new Date(cal.date);
		if (el.navtype == 0)
			date.setDateOnly(new Date()); // TODAY
		// unless "today" was clicked, we assume no date was clicked so
		// the selected handler will know not to close the calenar when
		// in single-click mode.
		// cal.dateClicked = (el.navtype == 0);
		cal.dateClicked = false;
		var year = date.getFullYear();
		var mon = date.getMonth();
		function setMonth(m) {
			var day = date.getDate();
			var max = date.getMonthDays(m);
			if (day > max) {
				date.setDate(max);
			}
			date.setMonth(m);
		};
		switch (el.navtype) {
		    case 400:
			Calendar.removeClass(el, "hilite");
			var text = Calendar._TT["ABOUT"];
			if (typeof text != "undefined") {
				text += cal.showsTime ? Calendar._TT["ABOUT_TIME"] : "";
			} else {
				// FIXME: this should be removed as soon as lang files get updated!
				text = "Help and about box text is not translated into this language.\n" +
					"If you know this language and you feel generous please update\n" +
					"the corresponding file in \"lang\" subdir to match calendar-en.js\n" +
					"and send it back to <mihai_bazon@yahoo.com> to get it into the distribution  ;-)\n\n" +
					"Thank you!\n" +
					"http://dynarch.com/mishoo/calendar.epl\n";
			}
			alert(text);
			return;
		    case -2:
			if (year > cal.minYear) {
				date.setFullYear(year - 1);
			}
			break;
		    case -1:
			if (mon > 0) {
				setMonth(mon - 1);
			} else if (year-- > cal.minYear) {
				date.setFullYear(year);
				setMonth(11);
			}
			break;
		    case 1:
			if (mon < 11) {
				setMonth(mon + 1);
			} else if (year < cal.maxYear) {
				date.setFullYear(year + 1);
				setMonth(0);
			}
			break;
		    case 2:
			if (year < cal.maxYear) {
				date.setFullYear(year + 1);
			}
			break;
		    case 100:
			cal.setFirstDayOfWeek(el.fdow);
			return;
		    case 50:
			var range = el._range;
			var current = el.innerHTML;
			for (var i = range.length; --i >= 0;)
				if (range[i] == current)
					break;
			if (ev && ev.shiftKey) {
				if (--i < 0)
					i = range.length - 1;
			} else if ( ++i >= range.length )
				i = 0;
			var newval = range[i];
			el.innerHTML = newval;
			cal.onUpdateTime();
			return;
		    case 0:
			// TODAY will bring us here
			if ((typeof cal.getDateStatus == "function") &&
			    cal.getDateStatus(date, date.getFullYear(), date.getMonth(), date.getDate())) {
				return false;
			}
			break;
		}
		if (!date.equalsTo(cal.date)) {
			cal.setDate(date);
			newdate = true;
		} else if (el.navtype == 0)
			newdate = closing = true;
	}
	if (newdate) {
		ev && cal.callHandler();
	}
	if (closing) {
		Calendar.removeClass(el, "hilite");
		ev && cal.callCloseHandler();
	}
};

// END: CALENDAR STATIC FUNCTIONS

// BEGIN: CALENDAR OBJECT FUNCTIONS

/**
 *  This function creates the calendar inside the given parent.  If _par is
 *  null than it creates a popup calendar inside the BODY element.  If _par is
 *  an element, be it BODY, then it creates a non-popup calendar (still
 *  hidden).  Some properties need to be set before calling this function.
 */
Calendar.prototype.create = function (_par) {
	var parent = null;
	if (! _par) {
		// default parent is the document body, in which case we create
		// a popup calendar.
		parent = document.getElementsByTagName("body")[0];
		this.isPopup = true;
	} else {
		parent = _par;
		this.isPopup = false;
	}
	this.date = this.dateStr ? new Date(this.dateStr) : new Date();

	var table = Calendar.createElement("table");
	this.table = table;
	table.cellSpacing = 0;
	table.cellPadding = 0;
	table.calendar = this;
	Calendar.addEvent(table, "mousedown", Calendar.tableMouseDown);

	var div = Calendar.createElement("div");
	this.element = div;
	div.className = "calendar";
	if (this.isPopup) {
		div.style.position = "absolute";
		div.style.display = "none";
	}
	div.appendChild(table);

	var thead = Calendar.createElement("thead", table);
	var cell = null;
	var row = null;

	var cal = this;
	var hh = function (text, cs, navtype) {
		cell = Calendar.createElement("td", row);
		cell.colSpan = cs;
		cell.className = "button";
		if (navtype != 0 && Math.abs(navtype) <= 2)
			cell.className += " nav";
		Calendar._add_evs(cell);
		cell.calendar = cal;
		cell.navtype = navtype;
		cell.innerHTML = "<div unselectable='on'>" + text + "</div>";
		return cell;
	};

	row = Calendar.createElement("tr", thead);
	var title_length = 7;
	(this.isPopup) && --title_length;
	(this.weekNumbers) && ++title_length;

	this.title = hh("", title_length, 300);
	this.title.className = "title";
	if (this.isPopup) {
		this.title.ttip = Calendar._TT["DRAG_TO_MOVE"];
		this.title.style.cursor = "move";
		hh("&#x00d7;", 1, 200).ttip = Calendar._TT["CLOSE"];
	}

	row = Calendar.createElement("tr", thead);
	row.className = "headrow";

	this._nav_py = hh("&#x00ab;", 1, -2);
	this._nav_py.ttip = Calendar._TT["PREV_YEAR"];

	this._nav_pm = hh("&#x2039;", 1, -1);
	this._nav_pm.ttip = Calendar._TT["PREV_MONTH"];

	this._nav_now = hh(Calendar._TT["TODAY"], this.weekNumbers ? 4 : 3, 0);
	this._nav_now.ttip = Calendar._TT["GO_TODAY"];

	this._nav_nm = hh("&#x203a;", 1, 1);
	this._nav_nm.ttip = Calendar._TT["NEXT_MONTH"];

	this._nav_ny = hh("&#x00bb;", 1, 2);
	this._nav_ny.ttip = Calendar._TT["NEXT_YEAR"];

	// day names
	row = Calendar.createElement("tr", thead);
	row.className = "daynames";
	if (this.weekNumbers) {
		cell = Calendar.createElement("td", row);
		cell.className = "name wn";
		cell.innerHTML = Calendar._TT["WK"];
	}
	for (var i = 7; i > 0; --i) {
		cell = Calendar.createElement("td", row);
		if (!i) {
			cell.navtype = 100;
			cell.calendar = this;
			Calendar._add_evs(cell);
		}
	}
	this.firstdayname = (this.weekNumbers) ? row.firstChild.nextSibling : row.firstChild;
	this._displayWeekdays();

	var tbody = Calendar.createElement("tbody", table);
	this.tbody = tbody;

	for (i = 6; i > 0; --i) {
		row = Calendar.createElement("tr", tbody);
		if (this.weekNumbers) {
			cell = Calendar.createElement("td", row);
		}
		for (var j = 7; j > 0; --j) {
			cell = Calendar.createElement("td", row);
			cell.calendar = this;
			Calendar._add_evs(cell);
		}
	}

	if (this.showsTime) {
		row = Calendar.createElement("tr", tbody);
		row.className = "time";

		cell = Calendar.createElement("td", row);
		cell.className = "time";
		cell.colSpan = 2;
		cell.innerHTML = Calendar._TT["TIME"] || "&nbsp;";

		cell = Calendar.createElement("td", row);
		cell.className = "time";
		cell.colSpan = this.weekNumbers ? 4 : 3;

		(function(){
			function makeTimePart(className, init, range_start, range_end) {
				var part = Calendar.createElement("span", cell);
				part.className = className;
				part.innerHTML = init;
				part.calendar = cal;
				part.ttip = Calendar._TT["TIME_PART"];
				part.navtype = 50;
				part._range = [];
				if (typeof range_start != "number")
					part._range = range_start;
				else {
					for (var i = range_start; i <= range_end; ++i) {
						var txt;
						if (i < 10 && range_end >= 10) txt = '0' + i;
						else txt = '' + i;
						part._range[part._range.length] = txt;
					}
				}
				Calendar._add_evs(part);
				return part;
			};
			var hrs = cal.date.getHours();
			var mins = cal.date.getMinutes();
			var t12 = !cal.time24;
			var pm = (hrs > 12);
			if (t12 && pm) hrs -= 12;
			var H = makeTimePart("hour", hrs, t12 ? 1 : 0, t12 ? 12 : 23);
			var span = Calendar.createElement("span", cell);
			span.innerHTML = ":";
			span.className = "colon";
			var M = makeTimePart("minute", mins, 0, 59);
			var AP = null;
			cell = Calendar.createElement("td", row);
			cell.className = "time";
			cell.colSpan = 2;
			if (t12)
				AP = makeTimePart("ampm", pm ? "pm" : "am", ["am", "pm"]);
			else
				cell.innerHTML = "&nbsp;";

			cal.onSetTime = function() {
				var pm, hrs = this.date.getHours(),
					mins = this.date.getMinutes();
				if (t12) {
					pm = (hrs >= 12);
					if (pm) hrs -= 12;
					if (hrs == 0) hrs = 12;
					AP.innerHTML = pm ? "pm" : "am";
				}
				H.innerHTML = (hrs < 10) ? ("0" + hrs) : hrs;
				M.innerHTML = (mins < 10) ? ("0" + mins) : mins;
			};

			cal.onUpdateTime = function() {
				var date = this.date;
				var h = parseInt(H.innerHTML, 10);
				if (t12) {
					if (/pm/i.test(AP.innerHTML) && h < 12)
						h += 12;
					else if (/am/i.test(AP.innerHTML) && h == 12)
						h = 0;
				}
				var d = date.getDate();
				var m = date.getMonth();
				var y = date.getFullYear();
				date.setHours(h);
				date.setMinutes(parseInt(M.innerHTML, 10));
				date.setFullYear(y);
				date.setMonth(m);
				date.setDate(d);
				this.dateClicked = false;
				this.callHandler();
			};
		})();
	} else {
		this.onSetTime = this.onUpdateTime = function() {};
	}

	var tfoot = Calendar.createElement("tfoot", table);

	row = Calendar.createElement("tr", tfoot);
	row.className = "footrow";

	cell = hh(Calendar._TT["SEL_DATE"], this.weekNumbers ? 8 : 7, 300);
	cell.className = "ttip";
	if (this.isPopup) {
		cell.ttip = Calendar._TT["DRAG_TO_MOVE"];
		cell.style.cursor = "move";
	}
	this.tooltips = cell;

	div = Calendar.createElement("div", this.element);
	this.monthsCombo = div;
	div.className = "combo";
	for (i = 0; i < Calendar._MN.length; ++i) {
		var mn = Calendar.createElement("div");
		mn.className = Calendar.is_ie ? "label-IEfix" : "label";
		mn.month = i;
		mn.innerHTML = Calendar._SMN[i];
		div.appendChild(mn);
	}

	div = Calendar.createElement("div", this.element);
	this.yearsCombo = div;
	div.className = "combo";
	for (i = 12; i > 0; --i) {
		var yr = Calendar.createElement("div");
		yr.className = Calendar.is_ie ? "label-IEfix" : "label";
		div.appendChild(yr);
	}

	this._init(this.firstDayOfWeek, this.date);
	parent.appendChild(this.element);
};

/** keyboard navigation, only for popup calendars */
Calendar._keyEvent = function(ev) {
	var cal = window._dynarch_popupCalendar;
	if (!cal || cal.multiple)
		return false;
	(Calendar.is_ie) && (ev = window.event);
	var act = (Calendar.is_ie || ev.type == "keypress"),
		K = ev.keyCode;
	if (ev.ctrlKey) {
		switch (K) {
		    case 37: // KEY left
			act && Calendar.cellClick(cal._nav_pm);
			break;
		    case 38: // KEY up
			act && Calendar.cellClick(cal._nav_py);
			break;
		    case 39: // KEY right
			act && Calendar.cellClick(cal._nav_nm);
			break;
		    case 40: // KEY down
			act && Calendar.cellClick(cal._nav_ny);
			break;
		    default:
			return false;
		}
	} else switch (K) {
	    case 32: // KEY space (now)
		Calendar.cellClick(cal._nav_now);
		break;
	    case 27: // KEY esc
		act && cal.callCloseHandler();
		break;
	    case 37: // KEY left
	    case 38: // KEY up
	    case 39: // KEY right
	    case 40: // KEY down
		if (act) {
			var prev, x, y, ne, el, step;
			prev = K == 37 || K == 38;
			step = (K == 37 || K == 39) ? 1 : 7;
			function setVars() {
				el = cal.currentDateEl;
				var p = el.pos;
				x = p & 15;
				y = p >> 4;
				ne = cal.ar_days[y][x];
			};setVars();
			function prevMonth() {
				var date = new Date(cal.date);
				date.setDate(date.getDate() - step);
				cal.setDate(date);
			};
			function nextMonth() {
				var date = new Date(cal.date);
				date.setDate(date.getDate() + step);
				cal.setDate(date);
			};
			while (1) {
				switch (K) {
				    case 37: // KEY left
					if (--x >= 0)
						ne = cal.ar_days[y][x];
					else {
						x = 6;
						K = 38;
						continue;
					}
					break;
				    case 38: // KEY up
					if (--y >= 0)
						ne = cal.ar_days[y][x];
					else {
						prevMonth();
						setVars();
					}
					break;
				    case 39: // KEY right
					if (++x < 7)
						ne = cal.ar_days[y][x];
					else {
						x = 0;
						K = 40;
						continue;
					}
					break;
				    case 40: // KEY down
					if (++y < cal.ar_days.length)
						ne = cal.ar_days[y][x];
					else {
						nextMonth();
						setVars();
					}
					break;
				}
				break;
			}
			if (ne) {
				if (!ne.disabled)
					Calendar.cellClick(ne);
				else if (prev)
					prevMonth();
				else
					nextMonth();
			}
		}
		break;
	    case 13: // KEY enter
		if (act)
			Calendar.cellClick(cal.currentDateEl, ev);
		break;
	    default:
		return false;
	}
	return Calendar.stopEvent(ev);
};

/**
 *  (RE)Initializes the calendar to the given date and firstDayOfWeek
 */
Calendar.prototype._init = function (firstDayOfWeek, date) {
	var today = new Date(),
		TY = today.getFullYear(),
		TM = today.getMonth(),
		TD = today.getDate();
	this.table.style.visibility = "hidden";
	var year = date.getFullYear();
	if (year < this.minYear) {
		year = this.minYear;
		date.setFullYear(year);
	} else if (year > this.maxYear) {
		year = this.maxYear;
		date.setFullYear(year);
	}
	this.firstDayOfWeek = firstDayOfWeek;
	this.date = new Date(date);
	var month = date.getMonth();
	var mday = date.getDate();
	var no_days = date.getMonthDays();

	// calendar voodoo for computing the first day that would actually be
	// displayed in the calendar, even if it's from the previous month.
	// WARNING: this is magic. ;-)
	date.setDate(1);
	var day1 = (date.getDay() - this.firstDayOfWeek) % 7;
	if (day1 < 0)
		day1 += 7;
	date.setDate(-day1);
	date.setDate(date.getDate() + 1);

	var row = this.tbody.firstChild;
	var MN = Calendar._SMN[month];
	var ar_days = this.ar_days = new Array();
	var weekend = Calendar._TT["WEEKEND"];
	var dates = this.multiple ? (this.datesCells = {}) : null;
	for (var i = 0; i < 6; ++i, row = row.nextSibling) {
		var cell = row.firstChild;
		if (this.weekNumbers) {
			cell.className = "day wn";
			cell.innerHTML = date.getWeekNumber();
			cell = cell.nextSibling;
		}
		row.className = "daysrow";
		var hasdays = false, iday, dpos = ar_days[i] = [];
		for (var j = 0; j < 7; ++j, cell = cell.nextSibling, date.setDate(iday + 1)) {
			iday = date.getDate();
			var wday = date.getDay();
			cell.className = "day";
			cell.pos = i << 4 | j;
			dpos[j] = cell;
			var current_month = (date.getMonth() == month);
			if (!current_month) {
				if (this.showsOtherMonths) {
					cell.className += " othermonth";
					cell.otherMonth = true;
				} else {
					cell.className = "emptycell";
					cell.innerHTML = "&nbsp;";
					cell.disabled = true;
					continue;
				}
			} else {
				cell.otherMonth = false;
				hasdays = true;
			}
			cell.disabled = false;
			cell.innerHTML = this.getDateText ? this.getDateText(date, iday) : iday;
			if (dates)
				dates[date.print("%Y%m%d")] = cell;
			if (this.getDateStatus) {
				var status = this.getDateStatus(date, year, month, iday);
				if (this.getDateToolTip) {
					var toolTip = this.getDateToolTip(date, year, month, iday);
					if (toolTip)
						cell.title = toolTip;
				}
				if (status === true) {
					cell.className += " disabled";
					cell.disabled = true;
				} else {
					if (/disabled/i.test(status))
						cell.disabled = true;
					cell.className += " " + status;
				}
			}
			if (!cell.disabled) {
				cell.caldate = new Date(date);
				cell.ttip = "_";
				if (!this.multiple && current_month
				    && iday == mday && this.hiliteToday) {
					cell.className += " selected";
					this.currentDateEl = cell;
				}
				if (date.getFullYear() == TY &&
				    date.getMonth() == TM &&
				    iday == TD) {
					cell.className += " today";
					cell.ttip += Calendar._TT["PART_TODAY"];
				}
				if (weekend.indexOf(wday.toString()) != -1)
					cell.className += cell.otherMonth ? " oweekend" : " weekend";
			}
		}
		if (!(hasdays || this.showsOtherMonths))
			row.className = "emptyrow";
	}
	this.title.innerHTML = Calendar._MN[month] + ", " + year;
	this.onSetTime();
	this.table.style.visibility = "visible";
	this._initMultipleDates();
	// PROFILE
	// this.tooltips.innerHTML = "Generated in " + ((new Date()) - today) + " ms";
};

Calendar.prototype._initMultipleDates = function() {
	if (this.multiple) {
		for (var i in this.multiple) {
			var cell = this.datesCells[i];
			var d = this.multiple[i];
			if (!d)
				continue;
			if (cell)
				cell.className += " selected";
		}
	}
};

Calendar.prototype._toggleMultipleDate = function(date) {
	if (this.multiple) {
		var ds = date.print("%Y%m%d");
		var cell = this.datesCells[ds];
		if (cell) {
			var d = this.multiple[ds];
			if (!d) {
				Calendar.addClass(cell, "selected");
				this.multiple[ds] = date;
			} else {
				Calendar.removeClass(cell, "selected");
				delete this.multiple[ds];
			}
		}
	}
};

Calendar.prototype.setDateToolTipHandler = function (unaryFunction) {
	this.getDateToolTip = unaryFunction;
};

/**
 *  Calls _init function above for going to a certain date (but only if the
 *  date is different than the currently selected one).
 */
Calendar.prototype.setDate = function (date) {
	if (!date.equalsTo(this.date)) {
		this._init(this.firstDayOfWeek, date);
	}
};

/**
 *  Refreshes the calendar.  Useful if the "disabledHandler" function is
 *  dynamic, meaning that the list of disabled date can change at runtime.
 *  Just * call this function if you think that the list of disabled dates
 *  should * change.
 */
Calendar.prototype.refresh = function () {
	this._init(this.firstDayOfWeek, this.date);
};

/** Modifies the "firstDayOfWeek" parameter (pass 0 for Synday, 1 for Monday, etc.). */
Calendar.prototype.setFirstDayOfWeek = function (firstDayOfWeek) {
	this._init(firstDayOfWeek, this.date);
	this._displayWeekdays();
};

/**
 *  Allows customization of what dates are enabled.  The "unaryFunction"
 *  parameter must be a function object that receives the date (as a JS Date
 *  object) and returns a boolean value.  If the returned value is true then
 *  the passed date will be marked as disabled.
 */
Calendar.prototype.setDateStatusHandler = Calendar.prototype.setDisabledHandler = function (unaryFunction) {
	this.getDateStatus = unaryFunction;
};

/** Customization of allowed year range for the calendar. */
Calendar.prototype.setRange = function (a, z) {
	this.minYear = a;
	this.maxYear = z;
};

/** Calls the first user handler (selectedHandler). */
Calendar.prototype.callHandler = function () {
	if (this.onSelected) {
		this.onSelected(this, this.date.print(this.dateFormat));
	}
};

/** Calls the second user handler (closeHandler). */
Calendar.prototype.callCloseHandler = function () {
	if (this.onClose) {
		this.onClose(this);
	}
	this.hideShowCovered();
};

/** Removes the calendar object from the DOM tree and destroys it. */
Calendar.prototype.destroy = function () {
	var el = this.element.parentNode;
	el.removeChild(this.element);
	Calendar._C = null;
	window._dynarch_popupCalendar = null;
};

/**
 *  Moves the calendar element to a different section in the DOM tree (changes
 *  its parent).
 */
Calendar.prototype.reparent = function (new_parent) {
	var el = this.element;
	el.parentNode.removeChild(el);
	new_parent.appendChild(el);
};

// This gets called when the user presses a mouse button anywhere in the
// document, if the calendar is shown.  If the click was outside the open
// calendar this function closes it.
Calendar._checkCalendar = function(ev) {
	var calendar = window._dynarch_popupCalendar;
	if (!calendar) {
		return false;
	}
	var el = Calendar.is_ie ? Calendar.getElement(ev) : Calendar.getTargetElement(ev);
	for (; el != null && el != calendar.element; el = el.parentNode);
	if (el == null) {
		// calls closeHandler which should hide the calendar.
		window._dynarch_popupCalendar.callCloseHandler();
		return Calendar.stopEvent(ev);
	}
};

/** Shows the calendar. */
Calendar.prototype.show = function () {
	var rows = this.table.getElementsByTagName("tr");
	for (var i = rows.length; i > 0;) {
		var row = rows[--i];
		Calendar.removeClass(row, "rowhilite");
		var cells = row.getElementsByTagName("td");
		for (var j = cells.length; j > 0;) {
			var cell = cells[--j];
			Calendar.removeClass(cell, "hilite");
			Calendar.removeClass(cell, "active");
		}
	}
	this.element.style.display = "block";
	this.hidden = false;
	if (this.isPopup) {
		window._dynarch_popupCalendar = this;
		Calendar.addEvent(document, "keydown", Calendar._keyEvent);
		Calendar.addEvent(document, "keypress", Calendar._keyEvent);
		Calendar.addEvent(document, "mousedown", Calendar._checkCalendar);
	}
	this.hideShowCovered();
};

/**
 *  Hides the calendar.  Also removes any "hilite" from the class of any TD
 *  element.
 */
Calendar.prototype.hide = function () {
	if (this.isPopup) {
		Calendar.removeEvent(document, "keydown", Calendar._keyEvent);
		Calendar.removeEvent(document, "keypress", Calendar._keyEvent);
		Calendar.removeEvent(document, "mousedown", Calendar._checkCalendar);
	}
	this.element.style.display = "none";
	this.hidden = true;
	this.hideShowCovered();
};

/**
 *  Shows the calendar at a given absolute position (beware that, depending on
 *  the calendar element style -- position property -- this might be relative
 *  to the parent's containing rectangle).
 */
Calendar.prototype.showAt = function (x, y) {
	var s = this.element.style;
	s.left = x + "px";
	s.top = y + "px";
	this.show();
};

/** Shows the calendar near a given element. */
Calendar.prototype.showAtElement = function (el, opts) {
	var self = this;
	var p = Calendar.getAbsolutePos(el);
	if (!opts || typeof opts != "string") {
		this.showAt(p.x, p.y + el.offsetHeight);
		return true;
	}
	function fixPosition(box) {
		if (box.x < 0)
			box.x = 0;
		if (box.y < 0)
			box.y = 0;
		var cp = document.createElement("div");
		var s = cp.style;
		s.position = "absolute";
		s.right = s.bottom = s.width = s.height = "0px";
		document.body.appendChild(cp);
		var br = Calendar.getAbsolutePos(cp);
		document.body.removeChild(cp);
		if (Calendar.is_ie) {
			br.y += document.body.scrollTop;
			br.x += document.body.scrollLeft;
		} else {
			br.y += window.scrollY;
			br.x += window.scrollX;
		}
		var tmp = box.x + box.width - br.x;
		if (tmp > 0) box.x -= tmp;
		tmp = box.y + box.height - br.y;
		if (tmp > 0) box.y -= tmp;
	};
	this.element.style.display = "block";
	Calendar.continuation_for_the_fucking_khtml_browser = function() {
		var w = self.element.offsetWidth;
		var h = self.element.offsetHeight;
		self.element.style.display = "none";
		var valign = opts.substr(0, 1);
		var halign = "l";
		if (opts.length > 1) {
			halign = opts.substr(1, 1);
		}
		// vertical alignment
		switch (valign) {
		    case "T": p.y -= h; break;
		    case "B": p.y += el.offsetHeight; break;
		    case "C": p.y += (el.offsetHeight - h) / 2; break;
		    case "t": p.y += el.offsetHeight - h; break;
		    case "b": break; // already there
		}
		// horizontal alignment
		switch (halign) {
		    case "L": p.x -= w; break;
		    case "R": p.x += el.offsetWidth; break;
		    case "C": p.x += (el.offsetWidth - w) / 2; break;
		    case "l": p.x += el.offsetWidth - w; break;
		    case "r": break; // already there
		}
		p.width = w;
		p.height = h + 40;
		self.monthsCombo.style.display = "none";
		fixPosition(p);
		self.showAt(p.x, p.y);
	};
	if (Calendar.is_khtml)
		setTimeout("Calendar.continuation_for_the_fucking_khtml_browser()", 10);
	else
		Calendar.continuation_for_the_fucking_khtml_browser();
};

/** Customizes the date format. */
Calendar.prototype.setDateFormat = function (str) {
	this.dateFormat = str;
};

/** Customizes the tooltip date format. */
Calendar.prototype.setTtDateFormat = function (str) {
	this.ttDateFormat = str;
};

/**
 *  Tries to identify the date represented in a string.  If successful it also
 *  calls this.setDate which moves the calendar to the given date.
 */
Calendar.prototype.parseDate = function(str, fmt) {
	if (!fmt)
		fmt = this.dateFormat;
	this.setDate(Date.parseDate(str, fmt));
};

Calendar.prototype.hideShowCovered = function () {
	if (!Calendar.is_ie && !Calendar.is_opera)
		return;
	function getVisib(obj){
		var value = obj.style.visibility;
		if (!value) {
			if (document.defaultView && typeof (document.defaultView.getComputedStyle) == "function") { // Gecko, W3C
				if (!Calendar.is_khtml)
					value = document.defaultView.
						getComputedStyle(obj, "").getPropertyValue("visibility");
				else
					value = '';
			} else if (obj.currentStyle) { // IE
				value = obj.currentStyle.visibility;
			} else
				value = '';
		}
		return value;
	};

	var tags = new Array("applet", "iframe", "select");
	var el = this.element;

	var p = Calendar.getAbsolutePos(el);
	var EX1 = p.x;
	var EX2 = el.offsetWidth + EX1;
	var EY1 = p.y;
	var EY2 = el.offsetHeight + EY1;

	for (var k = tags.length; k > 0; ) {
		var ar = document.getElementsByTagName(tags[--k]);
		var cc = null;

		for (var i = ar.length; i > 0;) {
			cc = ar[--i];

			p = Calendar.getAbsolutePos(cc);
			var CX1 = p.x;
			var CX2 = cc.offsetWidth + CX1;
			var CY1 = p.y;
			var CY2 = cc.offsetHeight + CY1;

			if (this.hidden || (CX1 > EX2) || (CX2 < EX1) || (CY1 > EY2) || (CY2 < EY1)) {
				if (!cc.__msh_save_visibility) {
					cc.__msh_save_visibility = getVisib(cc);
				}
				cc.style.visibility = cc.__msh_save_visibility;
			} else {
				if (!cc.__msh_save_visibility) {
					cc.__msh_save_visibility = getVisib(cc);
				}
				cc.style.visibility = "hidden";
			}
		}
	}
};

/** Internal function; it displays the bar with the names of the weekday. */
Calendar.prototype._displayWeekdays = function () {
	var fdow = this.firstDayOfWeek;
	var cell = this.firstdayname;
	var weekend = Calendar._TT["WEEKEND"];
	for (var i = 0; i < 7; ++i) {
		cell.className = "day name";
		var realday = (i + fdow) % 7;
		if (i) {
			cell.ttip = Calendar._TT["DAY_FIRST"].replace("%s", Calendar._DN[realday]);
			cell.navtype = 100;
			cell.calendar = this;
			cell.fdow = realday;
			Calendar._add_evs(cell);
		}
		if (weekend.indexOf(realday.toString()) != -1) {
			Calendar.addClass(cell, "weekend");
		}
		cell.innerHTML = Calendar._SDN[(i + fdow) % 7];
		cell = cell.nextSibling;
	}
};

/** Internal function.  Hides all combo boxes that might be displayed. */
Calendar.prototype._hideCombos = function () {
	this.monthsCombo.style.display = "none";
	this.yearsCombo.style.display = "none";
};

/** Internal function.  Starts dragging the element. */
Calendar.prototype._dragStart = function (ev) {
	if (this.dragging) {
		return;
	}
	this.dragging = true;
	var posX;
	var posY;
	if (Calendar.is_ie) {
		posY = window.event.clientY + document.body.scrollTop;
		posX = window.event.clientX + document.body.scrollLeft;
	} else {
		posY = ev.clientY + window.scrollY;
		posX = ev.clientX + window.scrollX;
	}
	var st = this.element.style;
	this.xOffs = posX - parseInt(st.left);
	this.yOffs = posY - parseInt(st.top);
	with (Calendar) {
		addEvent(document, "mousemove", calDragIt);
		addEvent(document, "mouseup", calDragEnd);
	}
};

// BEGIN: DATE OBJECT PATCHES

/** Adds the number of days array to the Date object. */
Date._MD = new Array(31,28,31,30,31,30,31,31,30,31,30,31);

/** Constants used for time computations */
Date.SECOND = 1000 /* milliseconds */;
Date.MINUTE = 60 * Date.SECOND;
Date.HOUR   = 60 * Date.MINUTE;
Date.DAY    = 24 * Date.HOUR;
Date.WEEK   =  7 * Date.DAY;

Date.parseDate = function(str, fmt) {
	var today = new Date();
	var y = 0;
	var m = -1;
	var d = 0;
	var a = str.split(/\W+/);
	var b = fmt.match(/%./g);
	var i = 0, j = 0;
	var hr = 0;
	var min = 0;
	for (i = 0; i < a.length; ++i) {
		if (!a[i])
			continue;
		switch (b[i]) {
		    case "%d":
		    case "%e":
			d = parseInt(a[i], 10);
			break;

		    case "%m":
			m = parseInt(a[i], 10) - 1;
			break;

		    case "%Y":
		    case "%y":
			y = parseInt(a[i], 10);
			(y < 100) && (y += (y > 29) ? 1900 : 2000);
			break;

		    case "%b":
		    case "%B":
			for (j = 0; j < 12; ++j) {
				if (Calendar._MN[j].substr(0, a[i].length).toLowerCase() == a[i].toLowerCase()) { m = j; break; }
			}
			break;

		    case "%H":
		    case "%I":
		    case "%k":
		    case "%l":
			hr = parseInt(a[i], 10);
			break;

		    case "%P":
		    case "%p":
			if (/pm/i.test(a[i]) && hr < 12)
				hr += 12;
			else if (/am/i.test(a[i]) && hr >= 12)
				hr -= 12;
			break;

		    case "%M":
			min = parseInt(a[i], 10);
			break;
		}
	}
	if (isNaN(y)) y = today.getFullYear();
	if (isNaN(m)) m = today.getMonth();
	if (isNaN(d)) d = today.getDate();
	if (isNaN(hr)) hr = today.getHours();
	if (isNaN(min)) min = today.getMinutes();
	if (y != 0 && m != -1 && d != 0)
		return new Date(y, m, d, hr, min, 0);
	y = 0; m = -1; d = 0;
	for (i = 0; i < a.length; ++i) {
		if (a[i].search(/[a-zA-Z]+/) != -1) {
			var t = -1;
			for (j = 0; j < 12; ++j) {
				if (Calendar._MN[j].substr(0, a[i].length).toLowerCase() == a[i].toLowerCase()) { t = j; break; }
			}
			if (t != -1) {
				if (m != -1) {
					d = m+1;
				}
				m = t;
			}
		} else if (parseInt(a[i], 10) <= 12 && m == -1) {
			m = a[i]-1;
		} else if (parseInt(a[i], 10) > 31 && y == 0) {
			y = parseInt(a[i], 10);
			(y < 100) && (y += (y > 29) ? 1900 : 2000);
		} else if (d == 0) {
			d = a[i];
		}
	}
	if (y == 0)
		y = today.getFullYear();
	if (m != -1 && d != 0)
		return new Date(y, m, d, hr, min, 0);
	return today;
};

/** Returns the number of days in the current month */
Date.prototype.getMonthDays = function(month) {
	var year = this.getFullYear();
	if (typeof month == "undefined") {
		month = this.getMonth();
	}
	if (((0 == (year%4)) && ( (0 != (year%100)) || (0 == (year%400)))) && month == 1) {
		return 29;
	} else {
		return Date._MD[month];
	}
};

/** Returns the number of day in the year. */
Date.prototype.getDayOfYear = function() {
	var now = new Date(this.getFullYear(), this.getMonth(), this.getDate(), 0, 0, 0);
	var then = new Date(this.getFullYear(), 0, 0, 0, 0, 0);
	var time = now - then;
	return Math.floor(time / Date.DAY);
};

/** Returns the number of the week in year, as defined in ISO 8601. */
Date.prototype.getWeekNumber = function() {
	var d = new Date(this.getFullYear(), this.getMonth(), this.getDate(), 0, 0, 0);
	var DoW = d.getDay();
	d.setDate(d.getDate() - (DoW + 6) % 7 + 3); // Nearest Thu
	var ms = d.valueOf(); // GMT
	d.setMonth(0);
	d.setDate(4); // Thu in Week 1
	return Math.round((ms - d.valueOf()) / (7 * 864e5)) + 1;
};

/** Checks date and time equality */
Date.prototype.equalsTo = function(date) {
	return ((this.getFullYear() == date.getFullYear()) &&
		(this.getMonth() == date.getMonth()) &&
		(this.getDate() == date.getDate()) &&
		(this.getHours() == date.getHours()) &&
		(this.getMinutes() == date.getMinutes()));
};

/** Set only the year, month, date parts (keep existing time) */
Date.prototype.setDateOnly = function(date) {
	var tmp = new Date(date);
	this.setDate(1);
	this.setFullYear(tmp.getFullYear());
	this.setMonth(tmp.getMonth());
	this.setDate(tmp.getDate());
};

/** Prints the date in a string according to the given format. */
Date.prototype.print = function (str) {
	var m = this.getMonth();
	var d = this.getDate();
	var y = this.getFullYear();
	var wn = this.getWeekNumber();
	var w = this.getDay();
	var s = {};
	var hr = this.getHours();
	var pm = (hr >= 12);
	var ir = (pm) ? (hr - 12) : hr;
	var dy = this.getDayOfYear();
	if (ir == 0)
		ir = 12;
	var min = this.getMinutes();
	var sec = this.getSeconds();
	s["%a"] = Calendar._SDN[w]; // abbreviated weekday name [FIXME: I18N]
	s["%A"] = Calendar._DN[w]; // full weekday name
	s["%b"] = Calendar._SMN[m]; // abbreviated month name [FIXME: I18N]
	s["%B"] = Calendar._MN[m]; // full month name
	// FIXME: %c : preferred date and time representation for the current locale
	s["%C"] = 1 + Math.floor(y / 100); // the century number
	s["%d"] = (d < 10) ? ("0" + d) : d; // the day of the month (range 01 to 31)
	s["%e"] = d; // the day of the month (range 1 to 31)
	// FIXME: %D : american date style: %m/%d/%y
	// FIXME: %E, %F, %G, %g, %h (man strftime)
	s["%H"] = (hr < 10) ? ("0" + hr) : hr; // hour, range 00 to 23 (24h format)
	s["%I"] = (ir < 10) ? ("0" + ir) : ir; // hour, range 01 to 12 (12h format)
	s["%j"] = (dy < 100) ? ((dy < 10) ? ("00" + dy) : ("0" + dy)) : dy; // day of the year (range 001 to 366)
	s["%k"] = hr;		// hour, range 0 to 23 (24h format)
	s["%l"] = ir;		// hour, range 1 to 12 (12h format)
	s["%m"] = (m < 9) ? ("0" + (1+m)) : (1+m); // month, range 01 to 12
	s["%M"] = (min < 10) ? ("0" + min) : min; // minute, range 00 to 59
	s["%n"] = "\n";		// a newline character
	s["%p"] = pm ? "PM" : "AM";
	s["%P"] = pm ? "pm" : "am";
	// FIXME: %r : the time in am/pm notation %I:%M:%S %p
	// FIXME: %R : the time in 24-hour notation %H:%M
	s["%s"] = Math.floor(this.getTime() / 1000);
	s["%S"] = (sec < 10) ? ("0" + sec) : sec; // seconds, range 00 to 59
	s["%t"] = "\t";		// a tab character
	// FIXME: %T : the time in 24-hour notation (%H:%M:%S)
	s["%U"] = s["%W"] = s["%V"] = (wn < 10) ? ("0" + wn) : wn;
	s["%u"] = w + 1;	// the day of the week (range 1 to 7, 1 = MON)
	s["%w"] = w;		// the day of the week (range 0 to 6, 0 = SUN)
	// FIXME: %x : preferred date representation for the current locale without the time
	// FIXME: %X : preferred time representation for the current locale without the date
	s["%y"] = ('' + y).substr(2, 2); // year without the century (range 00 to 99)
	s["%Y"] = y;		// year with the century
	s["%%"] = "%";		// a literal '%' character

	var re = /%./g;
	if (!Calendar.is_ie5 && !Calendar.is_khtml)
		return str.replace(re, function (par) { return s[par] || par; });

	var a = str.match(re);
	for (var i = 0; i < a.length; i++) {
		var tmp = s[a[i]];
		if (tmp) {
			re = new RegExp(a[i], 'g');
			str = str.replace(re, tmp);
		}
	}

	return str;
};

Date.prototype.__msh_oldSetFullYear = Date.prototype.setFullYear;
Date.prototype.setFullYear = function(y) {
	var d = new Date(this);
	d.__msh_oldSetFullYear(y);
	if (d.getMonth() != this.getMonth())
		this.setDate(28);
	this.__msh_oldSetFullYear(y);
};

// END: DATE OBJECT PATCHES


// global object that remembers the calendar
window._dynarch_popupCalendar = null;

Calendar.setup = function (params) {
	function param_default(pname, def) { if (typeof params[pname] == "undefined") { params[pname] = def; } };

	param_default("inputField",     null);
	param_default("displayArea",    null);
	param_default("button",         null);
	param_default("eventName",      "click");
	param_default("ifFormat",       "%Y/%m/%d");
	param_default("daFormat",       "%Y/%m/%d");
	param_default("singleClick",    true);
	param_default("disableFunc",    null);
	param_default("dateStatusFunc", params["disableFunc"]);	// takes precedence if both are defined
	param_default("dateText",       null);
	param_default("firstDay",       null);
	param_default("align",          "Br");
	param_default("range",          [1900, 2999]);
	param_default("weekNumbers",    true);
	param_default("flat",           null);
	param_default("flatCallback",   null);
	param_default("onSelect",       null);
	param_default("onClose",        null);
	param_default("onUpdate",       null);
	param_default("date",           null);
	param_default("showsTime",      false);
	param_default("timeFormat",     "24");
	param_default("electric",       true);
	param_default("step",           2);
	param_default("position",       null);
	param_default("cache",          false);
	param_default("showOthers",     false);
	param_default("multiple",       null);

	var tmp = ["inputField", "displayArea", "button"];
	for (var i in tmp) {
		if (typeof params[tmp[i]] == "string") {
			params[tmp[i]] = document.getElementById(params[tmp[i]]);
		}
	}
	if (!(params.flat || params.multiple || params.inputField || params.displayArea || params.button)) {
		alert("Calendar.setup:\n  Nothing to setup (no fields found).  Please check your code");
		return false;
	}

	function onSelect(cal) {
		var p = cal.params;
		var update = (cal.dateClicked || p.electric);
		if (update && p.inputField) {
			p.inputField.value = cal.date.print(p.ifFormat);
			if (typeof p.inputField.onchange == "function")
				p.inputField.onchange();
		}
		if (update && p.displayArea)
			p.displayArea.innerHTML = cal.date.print(p.daFormat);
		if (update && typeof p.onUpdate == "function")
			p.onUpdate(cal);
		if (update && p.flat) {
			if (typeof p.flatCallback == "function")
				p.flatCallback(cal);
		}
		if (update && p.singleClick && cal.dateClicked)
			cal.callCloseHandler();
	};

	if (params.flat != null) {
		if (typeof params.flat == "string")
			params.flat = document.getElementById(params.flat);
		if (!params.flat) {
			alert("Calendar.setup:\n  Flat specified but can't find parent.");
			return false;
		}
		var cal = new Calendar(params.firstDay, params.date, params.onSelect || onSelect);
		cal.showsOtherMonths = params.showOthers;
		cal.showsTime = params.showsTime;
		cal.time24 = (params.timeFormat == "24");
		cal.params = params;
		cal.weekNumbers = params.weekNumbers;
		cal.setRange(params.range[0], params.range[1]);
		cal.setDateStatusHandler(params.dateStatusFunc);
		cal.getDateText = params.dateText;
		if (params.ifFormat) {
			cal.setDateFormat(params.ifFormat);
		}
		if (params.inputField && typeof params.inputField.value == "string") {
			cal.parseDate(params.inputField.value);
		}
		cal.create(params.flat);
		cal.show();
		return false;
	}

	var triggerEl = params.button || params.displayArea || params.inputField;
	triggerEl["on" + params.eventName] = function() {
		var dateEl = params.inputField || params.displayArea;
		var dateFmt = params.inputField ? params.ifFormat : params.daFormat;
		var mustCreate = false;
		var cal = window.calendar;
		if (dateEl)
			params.date = Date.parseDate(dateEl.value || dateEl.innerHTML, dateFmt);
		if (!(cal && params.cache)) {
			window.calendar = cal = new Calendar(params.firstDay,
							     params.date,
							     params.onSelect || onSelect,
							     params.onClose || function(cal) { cal.hide(); });
			cal.showsTime = params.showsTime;
			cal.time24 = (params.timeFormat == "24");
			cal.weekNumbers = params.weekNumbers;
			mustCreate = true;
		} else {
			if (params.date)
				cal.setDate(params.date);
			cal.hide();
		}
		if (params.multiple) {
			cal.multiple = {};
			for (var i = params.multiple.length; --i >= 0;) {
				var d = params.multiple[i];
				var ds = d.print("%Y%m%d");
				cal.multiple[ds] = d;
			}
		}
		cal.showsOtherMonths = params.showOthers;
		cal.yearStep = params.step;
		cal.setRange(params.range[0], params.range[1]);
		cal.params = params;
		cal.setDateStatusHandler(params.dateStatusFunc);
		cal.getDateText = params.dateText;
		cal.setDateFormat(dateFmt);
		if (mustCreate)
			cal.create();
		cal.refresh();
		if (!params.position)
			cal.showAtElement(params.button || params.displayArea || params.inputField, params.align);
		else
			cal.showAt(params.position[0], params.position[1]);
		return false;
	};

	return cal;
};

// Calendar CN language
// full day names
Calendar._DN = new Array
("\u65e5",
 "\u4e00",
 "\u4e8c",
 "\u4e09",
 "\u56db",
 "\u4e94",
 "\u516d",
 "\u65e5");

// short day names
Calendar._SDN = new Array
("\u65e5",
 "\u4e00",
 "\u4e8c",
 "\u4e09",
 "\u56db",
 "\u4e94",
 "\u516d",
 "\u65e5");

// First day of the week. "0" means display Sunday first, "1" means display
// Monday first, etc.
Calendar._FD = 0;

// full month names
Calendar._MN = new Array
("\u4e00\u6708",
 "\u4e8c\u6708",
 "\u4e09\u6708",
 "\u56db\u6708",
 "\u4e94\u6708",
 "\u516d\u6708",
 "\u4e03\u6708",
 "\u516b\u6708",
 "\u4e5d\u6708",
 "\u5341\u6708",
 "\u5341\u4e00\u6708",
 "\u5341\u4e8c\u6708");

// short month names
Calendar._SMN = new Array
("\u4e00\u6708",
 "\u4e8c\u6708",
 "\u4e09\u6708",
 "\u56db\u6708",
 "\u4e94\u6708",
 "\u516d\u6708",
 "\u4e03\u6708",
 "\u516b\u6708",
 "\u4e5d\u6708",
 "\u5341\u6708",
 "\u5341\u4e00\u6708",
 "\u5341\u4e8c\u6708");

// tooltips
Calendar._TT = {};
Calendar._TT["INFO"] = "\u5173\u4e8e";

Calendar._TT["ABOUT"] =
"   DHTML \u65e5\u671f/\u65f6\u95f4\u9009\u62e9\u63a7\u4ef6\n" +
"(c) dynarch.com 2002-2005 / Author: Mihai Bazon\n" + // don't translate this this ;-)
"For latest version visit: \u6700\u65b0\u7248\u672c\u8bf7\u767b\u9646http://www.dynarch.com/projects/calendar/\u5bdf\u770b\n" +
"\u9075\u5faaGNU LGPL.  \u7ec6\u8282\u53c2\u9605 http://gnu.org/licenses/lgpl.html" +
"\n\n" +
"\u65e5\u671f\u9009\u62e9:\n" +
"- \u70b9\u51fb\xab(\xbb)\u6309\u94ae\u9009\u62e9\u4e0a(\u4e0b)\u4e00\u5e74\u5ea6.\n" +
"- \u70b9\u51fb" + String.fromCharCode(0x2039) + "(" + String.fromCharCode(0x203a) + ")\u6309\u94ae\u9009\u62e9\u4e0a(\u4e0b)\u4e2a\u6708\u4efd.\n" +
"- \u957f\u65f6\u95f4\u6309\u7740\u6309\u94ae\u5c06\u51fa\u73b0\u66f4\u591a\u9009\u62e9\u9879.";
Calendar._TT["ABOUT_TIME"] = "\n\n" +
"\u65f6\u95f4\u9009\u62e9:\n" +
"-\u5728\u65f6\u95f4\u90e8\u5206(\u5206\u6216\u8005\u79d2)\u4e0a\u5355\u51fb\u9f20\u6807\u5de6\u952e\u6765\u589e\u52a0\u5f53\u524d\u65f6\u95f4\u90e8\u5206(\u5206\u6216\u8005\u79d2)\n" +
"-\u5728\u65f6\u95f4\u90e8\u5206(\u5206\u6216\u8005\u79d2)\u4e0a\u6309\u4f4fShift\u952e\u540e\u5355\u51fb\u9f20\u6807\u5de6\u952e\u6765\u51cf\u5c11\u5f53\u524d\u65f6\u95f4\u90e8\u5206(\u5206\u6216\u8005\u79d2).";

Calendar._TT["PREV_YEAR"] = "\u4e0a\u4e00\u5e74";
Calendar._TT["PREV_MONTH"] = "\u4e0a\u4e2a\u6708";
Calendar._TT["GO_TODAY"] = "\u5230\u4eca\u5929";
Calendar._TT["NEXT_MONTH"] = "\u4e0b\u4e2a\u6708";
Calendar._TT["NEXT_YEAR"] = "\u4e0b\u4e00\u5e74";
Calendar._TT["SEL_DATE"] = "\u9009\u62e9\u65e5\u671f";
Calendar._TT["DRAG_TO_MOVE"] = "\u62d6\u52a8";
Calendar._TT["PART_TODAY"] = " (\u4eca\u5929)";

// the following is to inform that "%s" is to be the first day of week
// %s will be replaced with the day name.
Calendar._TT["DAY_FIRST"] = "%s\u4e3a\u8fd9\u5468\u7684\u7b2c\u4e00\u5929";

// This may be locale-dependent.  It specifies the week-end days, as an array
// of comma-separated numbers.  The numbers are from 0 to 6: 0 means Sunday, 1
// means Monday, etc.
Calendar._TT["WEEKEND"] = "0,6";

Calendar._TT["CLOSE"] = "\u5173\u95ed";
Calendar._TT["TODAY"] = "\u4eca\u5929";
Calendar._TT["TIME_PART"] = "(\u6309\u7740Shift\u952e)\u5355\u51fb\u6216\u62d6\u52a8\u6539\u53d8\u503c";

// date formats
Calendar._TT["DEF_DATE_FORMAT"] = "%Y-%m-%d";
Calendar._TT["TT_DATE_FORMAT"] = "%a, %b %e\u65e5";

Calendar._TT["WK"] = "\u5468";
Calendar._TT["TIME"] = "\u65f6\u95f4:";