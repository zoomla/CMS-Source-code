<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProject.aspx.cs"  MasterPageFile="~/Manage/I/Default.master" Inherits="ZoomLaCMS.Manage.AddOn.Project.AddProject" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>创建项目</title>
    <style>
        body{font-size:12px;font-family:Verdana,Arial,"宋体";}
        a:link {color:#464646;text-decoration:none;}
        a:visited {color:#464646;text-decoration:none;}
        a:hover{color:#ed145b;text-decoration:underline;}
        a:active{color:#ed145b;text-decoration:underline;}
        td{font-size:12px}
        /*想要改输入日历控件的样子就改下面的CSS样式就可以了*/
        /*Date*/
        .header {font: 12px Arial, Tahoma !important;font-weight: bold !important;font: 11px Arial, Tahoma;font-weight: bold;color: #154BA0;background:#C2DEED;height: 25px;padding-left: 10px;
        }
        .header td {padding-left: 10px;}
        .header a {color: #154BA0;}
        .header input {background:none;vertical-align: middle;height: 16px;}
        .category {font: 12px Arial, Tahoma !important;font: 11px Arial, Tahoma;color: #92A05A;height:20px;background-color: #FFFFD9;}
        .category td {border-bottom: 1px solid #DEDEB8;}
        .expire, .expire a:link, .expire a:visited {color: #999999;}
        .default, .default a:link, .default a:visited {color: #000000;}
        .checked, .checked a:link, .checked a:visited {color: #FF0000;}
        .today, .today a:link, .today a:visited {color: #00BB00;}
        #calendar_year {display: none;line-height: 130%;background: #FFFFFF;position: absolute;z-index: 10;}
        #calendar_year .col {float: left;background: #FFFFFF;margin-left: 1px;border: 1px solid #86B9D6;padding: 4px;}
        #calendar_month {display: none;background: #FFFFFF;line-height: 130%;border: 1px solid #86B9D6;padding: 4px;position: absolute;z-index: 11;}
        .tableborder {background: white;border: 1px solid #86B9D6;}
        #year,#month{padding-right:10px;background:url(attachments/month_0701/8200714125245.gif) no-repeat center right;}/*图片路径可以改成自己的*/
        /*Date*/
    </style>
    <script>
    //这段脚本如果你的页面里有，就可以去掉它们了
    //欢迎访问我的网站queyang.com
    var ie =navigator.appName=="Microsoft Internet Explorer"?true:false;
    function $(objID){
        return document.getElementById(objID);
    }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <script type="text/javascript">
var controlid = null;
var currdate = null;
var startdate = null;
var enddate  = null;
var yy = null;
var mm = null;
var hh = null;
var ii = null;
var currday = null;
var addtime = false;
var today = new Date();
var lastcheckedyear = false;
var lastcheckedmonth = false;
function _cancelBubble(event) {
 e = event ? event : window.event ;
 if(ie) {
  e.cancelBubble = true;
 } else {
  e.stopPropagation();
 }
}
function getposition(obj) {
 var r = new Array();
 r['x'] = obj.offsetLeft;
 r['y'] = obj.offsetTop;
 while(obj = obj.offsetParent) {
  r['x'] += obj.offsetLeft;
  r['y'] += obj.offsetTop;
 }
 return r;
}
function loadcalendar() {
 s = '';
 s += '<div id="calendar" style="display:none; position:absolute; z-index:9;" onclick="_cancelBubble(event)">';
 if (ie)
 {
  s += '<iframe width="200" height="160" src="about:blank" style="position: absolute;z-index:-1;"></iframe>';
 }
 s += '<div style="width: 200px;"><table class="tableborder" cellspacing="0" cellpadding="0" width="100%" style="text-align: center">';
 s += '<tr align="center" class="header"><td class="header"><a href="#" onclick="refreshcalendar(yy, mm-1);return false" title="上一月"><<</a></td><td colspan="5" style="text-align: center" class="header"><a href="#" onclick="showdiv(\'year\');_cancelBubble(event);return false" title="点击选择年份" id="year"></a>  -  <a id="month" title="点击选择月份" href="#" onclick="showdiv(\'month\');_cancelBubble(event);return false"></a></td><td class="header"><A href="#" onclick="refreshcalendar(yy, mm+1);return false" title="下一月">>></A></td></tr>';
 s += '<tr class="category"><td>日</td><td>一</td><td>二</td><td>三</td><td>四</td><td>五</td><td>六</td></tr>';
 for(var i = 0; i < 6; i++) {
  s += '<tr class="altbg2">';
  for(var j = 1; j <= 7; j++)
   s += "<td id=d" + (i * 7 + j) + " height=\"19\">0</td>";
  s += "</tr>";
 }
 s += '<tr id="hourminute"><td colspan="7" align="center"><input type="text" size="1" value="" id="hour" onKeyUp=\'this.value=this.value > 23 ? 23 : zerofill(this.value);controlid.value=controlid.value.replace(/\\d+(\:\\d+)/ig, this.value+"$1")\'> 点 <input type="text" size="1" value="" id="minute" onKeyUp=\'this.value=this.value > 59 ? 59 : zerofill(this.value);controlid.value=controlid.value.replace(/(\\d+\:)\\d+/ig, "$1"+this.value)\'> 分</td></tr>';
 s += '</table></div></div>';
 s += '<div id="calendar_year" onclick="_cancelBubble(event)"><div class="col">';
 for(var k = 1930; k <= 2019; k++) {
  s += k != 1930  &&  k % 10 == 0 ? '</div><div class="col">' : '';
  s += '<a href="#" onclick="refreshcalendar(' + k + ', mm);$(\'calendar_year\').style.display=\'none\';return false"><span' + (today.getFullYear() == k ? ' class="today"' : '') + ' id="calendar_year_' + k + '">' + k + '</span></a><br />';
 }
 s += '</div></div>';
 s += '<div id="calendar_month" onclick="_cancelBubble(event)">';
 for(var k = 1; k <= 12; k++) {
  s += '<a href="#" onclick="refreshcalendar(yy, ' + (k - 1) + ');$(\'calendar_month\').style.display=\'none\';return false"><span' + (today.getMonth()+1 == k ? ' class="today"' : '') + ' id="calendar_month_' + k + '">' + k + ( k < 10 ? ' ' : '') + ' 月</span></a><br />';
 }
 s += '</div>';
 var nElement = document.createElement("div");
 nElement.innerHTML=s;
 document.getElementsByTagName("body")[0].appendChild(nElement);
// document.write(s);
 document.onclick = function(event) {
  $('calendar').style.display = 'none';
  $('calendar_year').style.display = 'none';
  $('calendar_month').style.display = 'none';
 }
 $('calendar').onclick = function(event) {
  _cancelBubble(event);
  $('calendar_year').style.display = 'none';
  $('calendar_month').style.display = 'none';
 }
}
function parsedate(s) {
 /(\d+)\-(\d+)\-(\d+)\s*(\d*):?(\d*)/.exec(s);
 var m1 = (RegExp.$1  &&  RegExp.$1 > 1899  &&  RegExp.$1 < 2101) ? parseFloat(RegExp.$1) : today.getFullYear();
 var m2 = (RegExp.$2  &&  (RegExp.$2 > 0  &&  RegExp.$2 < 13)) ? parseFloat(RegExp.$2) : today.getMonth() + 1;
 var m3 = (RegExp.$3  &&  (RegExp.$3 > 0  &&  RegExp.$3 < 32)) ? parseFloat(RegExp.$3) : today.getDate();
 var m4 = (RegExp.$4  &&  (RegExp.$4 > -1  &&  RegExp.$4 < 24)) ? parseFloat(RegExp.$4) : 0;
 var m5 = (RegExp.$5  &&  (RegExp.$5 > -1  &&  RegExp.$5 < 60)) ? parseFloat(RegExp.$5) : 0;
 /(\d+)\-(\d+)\-(\d+)\s*(\d*):?(\d*)/.exec("0000-00-00 00\:00");
 return new Date(m1, m2 - 1, m3, m4, m5);
}
function settime(d) {
 $('calendar').style.display = 'none';
 controlid.value = yy + "-" + zerofill(mm + 1) + "-" + zerofill(d) + (addtime ? ' ' + zerofill($('hour').value) + ':' + zerofill($('minute').value) : '');
}
function showcalendar(event, controlid1, addtime1, startdate1, enddate1) {
 controlid = controlid1;
 addtime = addtime1;
 startdate = startdate1 ? parsedate(startdate1) : false;
 enddate = enddate1 ? parsedate(enddate1) : false;
 currday = controlid.value ? parsedate(controlid.value) : today;
 hh = currday.getHours();
 ii = currday.getMinutes();
 var p = getposition(controlid);
 $('calendar').style.display = 'block';
 $('calendar').style.left = p['x']+'px';
 $('calendar').style.top = (p['y'] + 20)+'px';
 _cancelBubble(event);
 refreshcalendar(currday.getFullYear(), currday.getMonth());
 if(lastcheckedyear != false) {
  $('calendar_year_' + lastcheckedyear).className = 'default';
  $('calendar_year_' + today.getFullYear()).className = 'today';
 }
 if(lastcheckedmonth != false) {
  $('calendar_month_' + lastcheckedmonth).className = 'default';
  $('calendar_month_' + (today.getMonth() + 1)).className = 'today';
 }
 $('calendar_year_' + currday.getFullYear()).className = 'checked';
 $('calendar_month_' + (currday.getMonth() + 1)).className = 'checked';
 $('hourminute').style.display = addtime ? '' : 'none';
 lastcheckedyear = currday.getFullYear();
 lastcheckedmonth = currday.getMonth() + 1;
}
function refreshcalendar(y, m) {
 var x = new Date(y, m, 1);
 var mv = x.getDay();
 var d = x.getDate();
 var dd = null;
 yy = x.getFullYear();
 mm = x.getMonth();
 $("year").innerHTML = yy;
 $("month").innerHTML = mm + 1 > 9  ? (mm + 1) : '0' + (mm + 1);
 for(var i = 1; i <= mv; i++) {
  dd = $("d" + i);
  dd.innerHTML = " ";
  dd.className = "";
 }
 while(x.getMonth() == mm) {
  dd = $("d" + (d + mv));
  dd.innerHTML = '<a href="###" onclick="settime(' + d + ');return false">' + d + '</a>';
  if(x.getTime() < today.getTime() || (enddate  &&  x.getTime() > enddate.getTime()) || (startdate  &&  x.getTime() < startdate.getTime())) {
   dd.className = 'expire';
  } else {
   dd.className = 'default';
  }
  if(x.getFullYear() == today.getFullYear()  &&  x.getMonth() == today.getMonth()  &&  x.getDate() == today.getDate()) {
   dd.className = 'today';
   dd.firstChild.title = '今天';
  }
  if(x.getFullYear() == currday.getFullYear()  &&  x.getMonth() == currday.getMonth()  &&  x.getDate() == currday.getDate()) {
   dd.className = 'checked';
  }
  x.setDate(++d);
 }
 while(d + mv <= 42) {
  dd = $("d" + (d + mv));
  dd.innerHTML = " ";
  d++;
 }
 if(addtime) {
  $('hour').value = zerofill(hh);
  $('minute').value = zerofill(ii);
 }
}
function showdiv(id) {
 var p = getposition($(id));
 $('calendar_' + id).style.left = p['x']+'px';
 $('calendar_' + id).style.top = (p['y'] + 16)+'px';
 $('calendar_' + id).style.display = 'block';
}
function zerofill(s) {
 var s = parseFloat(s.toString().replace(/(^[\s0]+)|(\s+$)/g, ''));
 s = isNaN(s) ? 0 : s;
 return (s < 10 ? '0' : '') + s.toString();
}
loadcalendar();</script>
            <table class="table table-bordered table-condensed">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <asp:Label ID="LblTitle" runat="server" Text="创建项目" Font-Bold="True"></asp:Label>
                
                </td>
        </tr>
        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>项目名称：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtProjectName" runat="server" class="l_input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ValrKeywordText" ControlToValidate="TxtProjectName"
                    runat="server" ErrorMessage="项目名称不能为空！" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>项目分类：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TBCate" runat="server" class="l_input"></asp:TextBox>
                <asp:DropDownList ID="DLProCate" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="DLProCate_SelectIndexChanging">
                </asp:DropDownList>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TBCate"
                    runat="server" ErrorMessage="项目分类不能为空！" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>项目简述：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtProjectIntro" runat="server" TextMode="MultiLine" Rows="8" Height="60px" Columns="50" class="l_input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtProjectIntro"
                    runat="server" ErrorMessage="项目简述不能为空！" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>开始时间：&nbsp;</strong></td>
            <td class="tdbg" align="left">
            <input name="member.birth"  id="tbUBday" class="l_input"
 runat="server" type="text" value="" size="14" readonly="readonly" onclick="showcalendar(event, this);" onfocus="showcalendar(event, this);if(this.value=='0000-00-00')this.value=''" />                            
                                     </td>
        </tr>
         <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>完成时间：&nbsp;</strong></td>
            <td class="tdbg" align="left">
             <input name="member.birth"  id="txtEndDay" class="l_input"
 runat="server" type="text" value="" size="14" readonly="readonly" onclick="showcalendar(event, this);" onfocus="showcalendar(event, this);if(this.value=='0000-00-00')this.value=''" />                            
                                    </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
              <asp:Button ID="EBtnModify" Text="修改" OnClick="EBtnModify_Click" runat="server" Visible="false" class="C_input"/>
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" class="C_input"/>&nbsp;&nbsp;
                <input name="Cancel" type="button"  id="Cancel" value="取消"  class="C_input" runat="server" onclick="javascript:window.location.href='ProjectRequireList.aspx'"/>
            </td>
        </tr>
    </table>
</asp:Content>
 
 
     
		
		
