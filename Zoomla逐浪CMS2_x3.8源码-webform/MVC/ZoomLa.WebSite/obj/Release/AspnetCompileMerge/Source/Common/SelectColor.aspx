<HTML>
<HEAD>
<META content="text/html;" charset="utf-8" http-equiv=Content-Type>
<STYLE type=text/css>
TD { FONT-SIZE: 10.8pt }
BODY { FONT-SIZE: 10.8pt }
BUTTON { WIDTH: 5em }
</STYLE>
<SCRIPT language=JavaScript>
var color = "" ;
// 是否有效颜色值
function IsColor(color){
	var temp=color;
	if (temp=="") return true;
	if (temp.length!=7) return false;
	return (temp.search(/\#[a-fA-F0-9]{6}/) != -1);
}
// 默认显示值
if (!color) color = "#000000";
// 返回有背景颜色属性的对象
function GetParent(obj){
	while(obj!=null && obj.tagName!="TD" && obj.tagName!="TR" && obj.tagName!="TH" && obj.tagName!="TABLE")
		obj=obj.parentElement;
	return obj;
}
// 返回标签名的选定控件
function GetControl(obj, sTag){
	obj=obj.item(0);
	if (obj.tagName==sTag){
		return obj;
	}
	return null;
}
// 数值转为RGB16进制颜色格式
function N2Color(s_Color){
	s_Color = s_Color.toString(16);
	switch (s_Color.length) {
	case 1:
		s_Color = "0" + s_Color + "0000"; 
		break;
	case 2:
		s_Color = s_Color + "0000";
		break;
	case 3:
		s_Color = s_Color.substring(1,3) + "0" + s_Color.substring(0,1) + "00" ;
		break;
	case 4:
		s_Color = s_Color.substring(2,4) + s_Color.substring(0,2) + "00" ;
		break;
	case 5:
		s_Color = s_Color.substring(3,5) + s_Color.substring(1,3) + "0" + s_Color.substring(0,1) ;
		break;
	case 6:
		s_Color = s_Color.substring(4,6) + s_Color.substring(2,4) + s_Color.substring(0,2) ;
		break;
	default:
		s_Color = "";
	}
	return '#' + s_Color;
}
// 初始值
function InitDocument(){
	ShowColor.bgColor = color;
	RGB.innerHTML = color;
	SelColor.value = color;
}
var SelRGB = '';
var DrRGB = '';
var SelGRAY = '120';
var hexch = new Array('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F');
function ToHex(n) {	
	var h, l;
	n = Math.round(n);
	l = n % 16;
	h = Math.floor((n / 16)) % 16;
	return (hexch[h] + hexch[l]);
}
function initColor()	{
	if ( location.search != '' && location.search != '?' )	{
		if (!document.all) { return false; }
		var old_color = location.search.substr(1).split("&")[2];
		if ( /^[0-9A-Fa-f]{6}$/.test(old_color) ) {
			SelRGB = '#'+old_color;
			RGB.innerText = SelRGB;
			EndColor();
		} else {
			SelColor.value = old_color;
			ShowColor.bgColor = SelColor.value;
		}
	}
}
function DoColor(c, l)
{ var r, g, b;

  r = '0x' + c.substring(1, 3);
  g = '0x' + c.substring(3, 5);
  b = '0x' + c.substring(5, 7);  
  if(l > 120)
  {
    l = l - 120;

    r = (r * (120 - l) + 255 * l) / 120;
    g = (g * (120 - l) + 255 * l) / 120;
    b = (b * (120 - l) + 255 * l) / 120;
  }else
  {
    r = (r * l) / 120;
    g = (g * l) / 120;
    b = (b * l) / 120;
  }
  return '#' + ToHex(r) + ToHex(g) + ToHex(b);
}
function EndColor()
{ 
    var i;
  if(DrRGB != SelRGB)
  {
    DrRGB = SelRGB;
    for(i = 0; i <= 30; i ++)
      GrayTable.rows(i).bgColor = DoColor(SelRGB, 240 - i * 8);
  }
	if ( RGB.innerText == '')	{
		initColor();
	} else {
	  SelColor.value = DoColor(RGB.innerText, GRAY.innerText);
	  ShowColor.bgColor = SelColor.value;
	}
}
function ff(t)	{
	if ( !document.all ){
		var len=location.search.lastIndexOf("&");
        var id= location.search.substr(len+1,location.search.length-len);
		opener.document.getElementById(id).value = t.bgColor;
		window.close();
	}
}
</SCRIPT>
<SCRIPT LANGUAGE=JavaScript FOR=ColorTable EVENT=onclick>
  SelRGB = event.srcElement.bgColor;
  EndColor();
</SCRIPT>
<SCRIPT LANGUAGE=JavaScript FOR=ColorTable EVENT=ondblclick>
  window.returnValue = SelColor.value;
  window.close();
</SCRIPT>
<SCRIPT LANGUAGE=JavaScript FOR=ColorTable EVENT=onmouseover>
  RGB.innerText = event.srcElement.bgColor;
  EndColor();
</SCRIPT>
<SCRIPT LANGUAGE=JavaScript FOR=ColorTable EVENT=onmouseout>
  RGB.innerText = SelRGB;
  EndColor();
</SCRIPT>
<SCRIPT LANGUAGE=JavaScript FOR=GrayTable EVENT=onclick>
  SelGRAY = event.srcElement.title;
  EndColor();
  window.returnValue = SelColor.value;
  window.close();
</SCRIPT>
<SCRIPT LANGUAGE=JavaScript FOR=GrayTable EVENT=onmouseover>
  GRAY.innerText = event.srcElement.title;
  EndColor();
</SCRIPT>
<SCRIPT LANGUAGE=JavaScript FOR=GrayTable EVENT=onmouseout>
  GRAY.innerText = SelGRAY;
  EndColor();
</SCRIPT>
<SCRIPT LANGUAGE=JavaScript FOR=Ok EVENT=onclick>
  window.returnValue = SelColor.value;
  window.close();
</SCRIPT>
<SCRIPT event=onclick for=Ok language=JavaScript>
	color = SelColor.value;
	if (!IsColor(color)){
		alert('无效的颜色值！！');
		return;
	}
	if ( !document.all )	{
		var id = location.search.substr(1);
		alert(id);
		opener.document.getElementById(id).value = t.bgColor;
		window.close();
	}else{
	    window.returnValue = color;
	}
	window.close();
</SCRIPT>
</HEAD>
<BODY bgColor=#D4D0C8 onLoad="initColor()">
<DIV align=center>
  <CENTER>
    <TABLE border=0 cellPadding=0 cellSpacing=10>
      <TBODY>
        <TR>
          <TD><TABLE border=0 cellPadding=0 cellSpacing=0 id="ColorTable" style="CURSOR: pointer">
              <SCRIPT language=JavaScript>
          function wc(r, g, b, n){
            r = ((r * 16 + r) * 3 * (15 - n) + 0x80 * n) / 15;
	        g = ((g * 16 + g) * 3 * (15 - n) + 0x80 * n) / 15;
	        b = ((b * 16 + b) * 3 * (15 - n) + 0x80 * n) / 15;
            document.write('<TD BGCOLOR=#' + ToHex(r) + ToHex(g) + ToHex(b) + ' height="8" width="8" onclick="ff(this)" ></TD>');
          }
         var cnum = new Array(1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 0, 0);
        for(i = 0; i < 16; i ++){
	   document.write('<TR>');
	   for(j = 0; j < 30; j ++){
		n1 = j % 5;
		n2 = Math.floor(j / 5) * 3;
		n3 = n2 + 3;
		wc((cnum[n3] * n1 + cnum[n2] * (5 - n1)),
		(cnum[n3 + 1] * n1 + cnum[n2 + 1] * (5 - n1)),
		(cnum[n3 + 2] * n1 + cnum[n2 + 2] * (5 - n1)), i);
     	   }
          document.writeln('</TR>');
         }
       </SCRIPT>
              <TBODY>
              </TBODY>
            </TABLE></TD>
          <TD><TABLE border=0 cellPadding=0 cellSpacing=0 id="GrayTable" style="CURSOR: pointer">
              <SCRIPT language=JavaScript>
          for(i = 255; i >= 0; i -= 8.5)
            document.write('<TR BGCOLOR=#' + ToHex(i) + ToHex(i) + ToHex(i) + '><TD TITLE=' + Math.floor(i * 16 / 17) + ' height=4 width=20></TD></TR>');
        </SCRIPT>
              <TBODY>
              </TBODY>
            </TABLE></TD>
        </TR>
      </TBODY>
    </TABLE>
  </CENTER>
</DIV>
<DIV align=center>
  <CENTER>
    <TABLE border=0 cellPadding=0 cellSpacing=10>
      <TBODY>
        <TR>
          <TD align="center" rowSpan=2>选中色彩
            <TABLE border=1 cellPadding=0 cellSpacing=0 height=30 id="ShowColor" width=40 bgcolor="">
              <TBODY>
                <TR>
                  <TD></TD>
                </TR>
              </TBODY>
            </TABLE></TD>
          <TD rowSpan=2>基色： <SPAN id="RGB"></SPAN><BR>
            亮度： <SPAN id="GRAY">120</SPAN><BR>
            代码：
            <INPUT id=SelColor size=7 value=""></TD>
          <TD><BUTTON id="Ok" type=submit>确定</BUTTON></TD>
        </TR>
        <TR>
          <TD><BUTTON onclick=window.close();>取消</BUTTON></TD>
        </TR>
      </TBODY>
    </TABLE>
  </CENTER>
</DIV>
</BODY>
</HTML>