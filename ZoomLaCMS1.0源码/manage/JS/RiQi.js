//==================================================== 参数设定部分 =======================================================
var bMoveable=true; //设置日历是否可以拖动
var _VersionInfo="" //版本信息

//==================================================== WEB 页面显示部分 =====================================================
var strFrame; //存放日历层的HTML代码
document.writeln('<iframe id=nickDateLayer scrolling=0 frameborder=0 style="position: absolute; width: 143px; height: 178px; z-index: 9998; display: none"></iframe>');
strFrame='<style>';
strFrame+='INPUT.button{BORDER-RIGHT: gray 1px solid;BORDER-TOP: gray 1px solid;BORDER-LEFT: gray 1px solid;';
strFrame+='BORDER-BOTTOM: gray 1px solid;BACKGROUND-COLOR: #ffffff;font-family:宋体;cursor:hand}';
strFrame+='TD{FONT-SIZE: 9pt;font-family:宋体;}';
strFrame+='</style>';
strFrame+='<scr' + 'ipt>';
strFrame+='var datelayerx,datelayery; /*存放日历控件的鼠标位置*/';
strFrame+='var bDrag; /*标记是否开始拖动*/';
strFrame+='function document.onmousemove() /*在鼠标移动事件中，如果开始拖动日历，则移动日历*/';
strFrame+='{if(bDrag && window.event.button==1)';
strFrame+=' {var DateLayer=parent.document.all.nickDateLayer.style;';
strFrame+=' DateLayer.posLeft += window.event.clientX-datelayerx;/*由于每次移动以后鼠标位置都恢复为初始的位置，因此写法与div中不同*/';
strFrame+=' DateLayer.posTop += window.event.clientY-datelayery;}}';
strFrame+='function DragStart() /*开始日历拖动*/';
strFrame+='{var DateLayer=parent.document.all.nickDateLayer.style;';
strFrame+=' datelayerx=window.event.clientX;';
strFrame+=' datelayery=window.event.clientY;';
strFrame+=' bDrag=true;}';
strFrame+='function DragEnd(){ /*结束日历拖动*/';
strFrame+=' bDrag=false;}';
strFrame+='</scr' + 'ipt>';
strFrame+='<div style="z-index:9999;position: absolute; left:0; top:0;" onselectstart="return false"><span id=tmpSelectYearLayer style="z-index: 9999;position: absolute;top: 3; left: 19;display: none"></span>';
strFrame+='<span id=tmpSelectMonthLayer style="z-index: 9999;position: absolute;top: 3; left: 78;display: none"></span>';
strFrame+='<table border=0 cellspacing=1 cellpadding=0 width=142 height=160 bgColor="gray">';
strFrame+=' <tr ><td width=142 height=23 bgcolor=#FFFFFF><table border=0 cellspacing=1 cellpadding=0 width=140 height=23>';
strFrame+=' <tr align=center ><td width=16 align=center bgcolor=gray style="font-size:12px;cursor: hand;color: #FFFFFF" ';
strFrame+=' onclick="parent.nickPrevM()" title="向前翻 1 月" ><b >&lt;</b>';
strFrame+=' </td><td width=60 align=center style="font-size:12px;cursor:default" ';
strFrame+='onmouseover="style.backgroundColor=\'#C6C7EF\'" onmouseout="style.backgroundColor=\'white\'" ';
strFrame+='onclick="parent.tmpSelectYearInnerHTML(this.innerText.substring(0,4))" title="点击这里选择年份"><span id=nickYearHead></span></td>';
strFrame+='<td width=48 align=center style="font-size:12px;cursor:default" onmouseover="style.backgroundColor=\'#C6C7EF\'" ';
strFrame+=' onmouseout="style.backgroundColor=\'white\'" onclick="parent.tmpSelectMonthInnerHTML(this.innerText.length==3?this.innerText.substring(0,1):this.innerText.substring(0,2))"';
strFrame+=' title="点击这里选择月份"><span id=nickMonthHead ></span></td>';
strFrame+=' <td width=16 bgcolor=gray align=center style="font-size:12px;cursor: hand;color: #FFFFFF" ';
strFrame+=' onclick="parent.nickNextM()" title="向后翻 1 月" ><b >&gt;</b></td></tr>';
strFrame+=' </table></td></tr>';
strFrame+=' <tr ><td width=142 height=18 >';
strFrame+='<table border=0 cellspacing=1 cellpadding=0 bgcolor=gray ' + (bMoveable? 'onmousedown="DragStart()" onmouseup="DragEnd()"':'');
strFrame+=' BORDERCOLORLIGHT=#9496E1 BORDERCOLORDARK=#FFFFFF width=140 height=20 style="cursor:' + (bMoveable ? 'move':'default') + '">';
strFrame+='<tr align=center valign=bottom><td style="font-size:12px;color:#FFFFFF" >日</td>';
strFrame+='<td style="font-size:12px;color:#FFFFFF" >一</td><td style="font-size:12px;color:#FFFFFF" >二</td>';
strFrame+='<td style="font-size:12px;color:#FFFFFF" >三</td><td style="font-size:12px;color:#FFFFFF" >四</td>';
strFrame+='<td style="font-size:12px;color:#FFFFFF" >五</td><td style="font-size:12px;color:#FFFFFF" >六</td></tr>';
strFrame+='</table></td></tr>';
strFrame+=' <tr ><td width=142 height=120 >';
strFrame+=' <table border=0 cellspacing=1 cellpadding=0 BORDERCOLORLIGHT=#9496E1 BORDERCOLORDARK=#FFFFFF bgcolor=#fff8ec width=140 height=120 >';
var n=0; for (j=0;j<5;j++){ strFrame+= ' <tr align=center >'; for (i=0;i<7;i++){
strFrame+='<td width=20 height=20 id=nickDay'+n+' style="font-size:12px" onclick=parent.nickDayClick(this.innerText,0)></td>';n++;}
strFrame+='</tr>';}
strFrame+=' <tr align=center >';
for (i=35;i<39;i++)strFrame+='<td width=20 height=20 id=nickDay'+i+' style="font-size:12px" onclick="parent.nickDayClick(this.innerText,0)"></td>';
strFrame+=' <td colspan=3 align=right ><span onclick=parent.closeLayer() style="font-size:12px;cursor: hand"';
strFrame+=' title="' + _VersionInfo + '"><u>关闭</u></span>&nbsp;</td></tr>';
strFrame+=' </table></td></tr><tr style="display:none"><td >';
strFrame+=' <table border=0 cellspacing=1 cellpadding=0 width=100% bgcolor=#FFFFFF >';
strFrame+=' <tr ><td align=left><input type=button class=button value="<<" title="向前翻 1 年" onclick="parent.nickPrevY()" ';
strFrame+=' onfocus="this.blur()" style="font-size: 12px; height: 20px">&nbsp;<input class=button title="向前翻 1 月" type=button ';
strFrame+=' value="< " onclick="parent.nickPrevM()" onfocus="this.blur()" style="font-size: 12px; height: 20px"></td><td ';
strFrame+=' align=center><input type=button class=button value=今天 onclick="parent.nickToday()" ';
strFrame+=' onfocus="this.blur()" title="当前日期" style="font-size: 12px; height: 20px; cursor:hand"></td><td ';
strFrame+=' align=right><input type=button class=button value=" >" onclick="parent.nickNextM()" ';
strFrame+=' onfocus="this.blur()" title="向后翻 1 月" class=button style="font-size: 12px; height: 20px">&nbsp;<input ';
strFrame+=' type=button class=button value=">>" title="向后翻 1 年" onclick="parent.nickNextY()"';
strFrame+=' onfocus="this.blur()" style="font-size: 12px; height: 20px"></td>';
strFrame+='</tr></table></td></tr></table></div>';

window.frames.nickDateLayer.document.writeln(strFrame);
window.frames.nickDateLayer.document.close(); //解决ie进度条不结束的问题

//==================================================== WEB 页面显示部分 ======================================================
var outObject;
var outButton; //点击的按钮
var outDate=""; //存放对象的日期
var odatelayer=window.frames.nickDateLayer.document.all; //存放日历对象
function setday(tt,obj) //主调函数
{
if (arguments.length > 2){alert("对不起！传入本控件的参数太多！");return;}
if (arguments.length == 0){alert("对不起！您没有传回本控件任何参数！");return;}
var dads = document.all.nickDateLayer.style;
var th = tt;
var ttop = tt.offsetTop; //TT控件的定位点高
var thei = tt.clientHeight; //TT控件本身的高
var tleft = tt.offsetLeft; //TT控件的定位点宽
var ttyp = tt.type; //TT控件的类型
while (tt = tt.offsetParent){ttop+=tt.offsetTop; tleft+=tt.offsetLeft;}
dads.top = (ttyp=="image")? ttop+thei : ttop+thei+6;
dads.left = tleft;
outObject = (arguments.length == 1) ? th : obj;
outButton = (arguments.length == 1) ? null : th; //设定外部点击的按钮
//根据当前输入框的日期显示日历的年月
var reg = /^(\d+)-(\d{1,2})-(\d{1,2})$/;
var r = outObject.value.match(reg);
if(r!=null){
r[2]=r[2]-1;
var d= new Date(r[1], r[2],r[3]);
if(d.getFullYear()==r[1] && d.getMonth()==r[2] && d.getDate()==r[3]){
outDate=d; //保存外部传入的日期
}
else outDate="";
nickSetDay(r[1],r[2]+1);
}
else{
outDate="";
nickSetDay(new Date().getFullYear(), new Date().getMonth() + 1);
}
dads.display = '';

event.returnValue=false;
}

var MonHead = new Array(12); //定义阳历中每个月的最大天数
MonHead[0] = 31; MonHead[1] = 28; MonHead[2] = 31; MonHead[3] = 30; MonHead[4] = 31; MonHead[5] = 30;
MonHead[6] = 31; MonHead[7] = 31; MonHead[8] = 30; MonHead[9] = 31; MonHead[10] = 30; MonHead[11] = 31;

var nickTheYear=new Date().getFullYear(); //定义年的变量的初始值
var nickTheMonth=new Date().getMonth()+1; //定义月的变量的初始值
var nickWDay=new Array(39); //定义写日期的数组

function document.onclick() //任意点击时关闭该控件 //ie6的情况可以由下面的切换焦点处理代替
{
with(window.event)
{ if (srcElement.getAttribute("Author")==null && srcElement != outObject && srcElement != outButton)
closeLayer();
}
}

function document.onkeyup() //按Esc键关闭，切换焦点关闭
{
if (window.event.keyCode==27){
if(outObject)outObject.blur();
closeLayer();
}
else if(document.activeElement)
if(document.activeElement.getAttribute("Author")==null && document.activeElement != outObject && document.activeElement != outButton)
{
closeLayer();
}
}

function nickWriteHead(yy,mm) //往 head 中写入当前的年与月
{
odatelayer.nickYearHead.innerText = yy + " 年";
odatelayer.nickMonthHead.innerText = mm + " 月";
}

function tmpSelectYearInnerHTML(strYear) //年份的下拉框
{
if (strYear.match(/\D/)!=null){alert("年份输入参数不是数字！");return;}
var m = (strYear) ? strYear : new Date().getFullYear();
if (m < 1000 || m > 9999) {alert("年份值不在 1000 到 9999 之间！");return;}
var n = m - 10;
if (n < 1000) n = 1000;
if (n + 26 > 9999) n = 9974;
var s = "<select name=tmpSelectYear style='font-size: 12px' "
s += "onblur='document.all.tmpSelectYearLayer.style.display=\"none\"' "
s += "onchange='document.all.tmpSelectYearLayer.style.display=\"none\";"
s += "parent.nickTheYear = this.value; parent.nickSetDay(parent.nickTheYear,parent.nickTheMonth)'>\r\n";
var selectInnerHTML = s;
n = n-70;
for (var i = n; i < n + 200; i++)
{
if (i == m)
{selectInnerHTML += "<option value='" + i + "' selected>" + i + "年" + "</option>\r\n";}
else {selectInnerHTML += "<option value='" + i + "'>" + i + "年" + "</option>\r\n";}
}
selectInnerHTML += "</select>";
odatelayer.tmpSelectYearLayer.style.display="";
odatelayer.tmpSelectYearLayer.innerHTML = selectInnerHTML;
odatelayer.tmpSelectYear.focus();
}

function tmpSelectMonthInnerHTML(strMonth) //月份的下拉框
{
if (strMonth.match(/\D/)!=null){alert("月份输入参数不是数字！");return;}
var m = (strMonth) ? strMonth : new Date().getMonth() + 1;
var s = "<select name=tmpSelectMonth style='font-size: 12px' "
s += "onblur='document.all.tmpSelectMonthLayer.style.display=\"none\"' "
s += "onchange='document.all.tmpSelectMonthLayer.style.display=\"none\";"
s += "parent.nickTheMonth = this.value; parent.nickSetDay(parent.nickTheYear,parent.nickTheMonth)'>\r\n";
var selectInnerHTML = s;
for (var i = 1; i < 13; i++)
{
if (i == m)
{selectInnerHTML += "<option value='"+i+"' selected>"+i+"月"+"</option>\r\n";}
else {selectInnerHTML += "<option value='"+i+"'>"+i+"月"+"</option>\r\n";}
}
selectInnerHTML += "</select>";
odatelayer.tmpSelectMonthLayer.style.display="";
odatelayer.tmpSelectMonthLayer.innerHTML = selectInnerHTML;
odatelayer.tmpSelectMonth.focus();
}

function closeLayer() //这个层的关闭
{
document.all.nickDateLayer.style.display="none";
}

function IsPinYear(year) //判断是否闰平年
{
if (0==year%4&&((year%100!=0)||(year%400==0))) return true;else return false;
}

function GetMonthCount(year,month) //闰年二月为29天
{
var c=MonHead[month-1];if((month==2)&&IsPinYear(year)) c++;return c;
}
function GetDOW(day,month,year) //求某天的星期几
{
var dt=new Date(year,month-1,day).getDay()/7; return dt;
}

function nickPrevY() //往前翻 Year
{
if(nickTheYear > 999 && nickTheYear <10000){nickTheYear--;}
else{alert("年份超出范围（1000-9999）！");}
nickSetDay(nickTheYear,nickTheMonth);
}
function nickNextY() //往后翻 Year
{
if(nickTheYear > 999 && nickTheYear <10000){nickTheYear++;}
else{alert("年份超出范围（1000-9999）！");}
nickSetDay(nickTheYear,nickTheMonth);
}
function nickToday() //Today Button
{
var today;
nickTheYear = new Date().getFullYear();
nickTheMonth = new Date().getMonth()+1;
if (nickTheMonth < 10){nickTheMonth = "0" + nickTheMonth;}
today=new Date().getDate();
if (today < 10){today = "0" + today;}
//nickSetDay(nickTheYear,nickTheMonth);
if(outObject){
outObject.value=nickTheYear + "-" + nickTheMonth + "-" + today;
}
closeLayer();
}
function nickPrevM() //往前翻月份
{
if(nickTheMonth>1){nickTheMonth--}else{nickTheYear--;nickTheMonth=12;}
nickSetDay(nickTheYear,nickTheMonth);
}
function nickNextM() //往后翻月份
{
if(nickTheMonth==12){nickTheYear++;nickTheMonth=1}else{nickTheMonth++}
nickSetDay(nickTheYear,nickTheMonth);
}

function nickSetDay(yy,mm) //主要的写程序**********
{
nickWriteHead(yy,mm);
//设置当前年月的公共变量为传入值
nickTheYear=yy;
nickTheMonth=mm;

for (var i = 0; i < 39; i++){nickWDay[i]=""}; //将显示框的内容全部清空
var day1 = 1,day2=1,firstday = new Date(yy,mm-1,1).getDay(); //某月第一天的星期几
for (i=0;i<firstday;i++)nickWDay[i]=GetMonthCount(mm==1?yy-1:yy,mm==1?12:mm-1)-firstday+i+1 //上个月的最后几天
for (i = firstday; day1 < GetMonthCount(yy,mm)+1; i++){nickWDay[i]=day1;day1++;}
for (i=firstday+GetMonthCount(yy,mm);i<39;i++){nickWDay[i]=day2;day2++}
for (i = 0; i < 39; i++)
{ var da = eval("odatelayer.nickDay"+i) //书写新的一个月的日期星期排列
if (nickWDay[i]!="")
{
//初始化边框
da.borderColorLight="#9496E1";
da.borderColorDark="#FFFFFF";
if(i<firstday) //上个月的部分
{
da.innerHTML="<font color=gray>" + nickWDay[i] + "</font>";
da.title=(mm==1?12:mm-1) +"月" + nickWDay[i] + "日";
da.onclick=Function("nickDayClick(this.innerText,-1)");
if(!outDate)
da.style.backgroundColor = ((mm==1?yy-1:yy) == new Date().getFullYear() &&
(mm==1?12:mm-1) == new Date().getMonth()+1 && nickWDay[i] == new Date().getDate()) ?
"#C6C7EF":"#E0E0E0";
else
{
da.style.backgroundColor =((mm==1?yy-1:yy)==outDate.getFullYear() && (mm==1?12:mm-1)== outDate.getMonth() + 1 &&
nickWDay[i]==outDate.getDate())? "#FFD700" :
(((mm==1?yy-1:yy) == new Date().getFullYear() && (mm==1?12:mm-1) == new Date().getMonth()+1 &&
nickWDay[i] == new Date().getDate()) ? "#C6C7EF":"#E0E0E0");
//将选中的日期显示为凹下去
if((mm==1?yy-1:yy)==outDate.getFullYear() && (mm==1?12:mm-1)== outDate.getMonth() + 1 &&
nickWDay[i]==outDate.getDate())
{
da.borderColorLight="#FFFFFF";
da.borderColorDark="#9496E1";
}
}
}
else if (i>=firstday+GetMonthCount(yy,mm)) //下个月的部分
{
da.innerHTML="<font color=gray>" + nickWDay[i] + "</font></b>";
da.title=(mm==12?1:mm+1) +"月" + nickWDay[i] + "日";
da.onclick=Function("nickDayClick(this.innerText,1)");
if(!outDate)
da.style.backgroundColor = ((mm==12?yy+1:yy) == new Date().getFullYear() &&
(mm==12?1:mm+1) == new Date().getMonth()+1 && nickWDay[i] == new Date().getDate()) ?
"#C6C7EF":"#E0E0E0";
else
{
da.style.backgroundColor =((mm==12?yy+1:yy)==outDate.getFullYear() && (mm==12?1:mm+1)== outDate.getMonth() + 1 &&
nickWDay[i]==outDate.getDate())? "#FFD700" :
(((mm==12?yy+1:yy) == new Date().getFullYear() && (mm==12?1:mm+1) == new Date().getMonth()+1 &&
nickWDay[i] == new Date().getDate()) ? "#C6C7EF":"#E0E0E0");
//将选中的日期显示为凹下去
if((mm==12?yy+1:yy)==outDate.getFullYear() && (mm==12?1:mm+1)== outDate.getMonth() + 1 &&
nickWDay[i]==outDate.getDate())
{
da.borderColorLight="#FFFFFF";
da.borderColorDark="#9496E1";
}
}
}
else //本月的部分
{
da.innerHTML= "<font color=#0000ee>"+nickWDay[i]+"</font>" ;
da.title=mm +"月" + nickWDay[i] + "日";
da.onclick=Function("nickDayClick(this.innerText,0)"); //给td赋予onclick事件的处理
//如果是当前选择的日期，则显示暗黄色的背景；如果是当前日期，则显示亮紫色的背景
if(!outDate)
da.style.backgroundColor = (yy == new Date().getFullYear() && mm == new Date().getMonth()+1 && nickWDay[i] == new Date().getDate())?
"#C6C7EF":"#E0E0E0";
else
{
da.style.backgroundColor =(yy==outDate.getFullYear() && mm== outDate.getMonth() + 1 && nickWDay[i]==outDate.getDate())?
"#FFD700":((yy == new Date().getFullYear() && mm == new Date().getMonth()+1 && nickWDay[i] == new Date().getDate())?
"white":"#E0E0E0");
//将选中的日期显示为凹下去
if(yy==outDate.getFullYear() && mm== outDate.getMonth() + 1 && nickWDay[i]==outDate.getDate())
{
da.borderColorLight="#FFFFFF";
da.borderColorDark="#9496E1";
}
}
}
da.style.cursor="hand"
}
else{da.innerHTML="";da.style.backgroundColor="";da.style.cursor="default"}
}
}

function nickDayClick(n,ex) //点击显示框选取日期，主输入函数*************
{
var yy=nickTheYear;
var mm = parseInt(nickTheMonth)+ex; //ex表示偏移量，用于选择上个月份和下个月份的日期
//判断月份，并进行对应的处理
if(mm<1){
yy--;
mm=12+mm;
}
else if(mm>12){
yy++;
mm=mm-12;
}

if (mm < 10){mm = "0" + mm;}
if (outObject)
{
if (!n) {//outObject.value="";
return;}
if ( n < 10){n = "0" + n;}
outObject.value= yy + "-" + mm + "-" + n ; //注：在这里你可以输出改成你想要的格式
closeLayer();
}
else {closeLayer(); alert("您所要输出的控件对象并不存在！");}
}


 
 
