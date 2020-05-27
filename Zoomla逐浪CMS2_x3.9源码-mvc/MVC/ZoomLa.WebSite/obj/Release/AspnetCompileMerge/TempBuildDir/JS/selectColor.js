var activeElementId;
var colorPickerType = 0;
var colors = new Array("00", "33", "66", "99", "CC", "FF");

//特殊颜色值
var specialColors = new Array('#000000', '#333333', '#666666', '#999999', '#CCCCCC', '#FFFFFF', '#FF0000', '#00FF00', '#0000FF', '#FFFF00', '#00FFFF', '#FF00FF');


function SetSwitchColor(x, y, color)
{
	document.getElementById("sw" + x + "-" + y).style.backgroundColor = color;
}

//初始化颜色面版
function InitColorPicker(setPickerType)
{
	colorPickerType = setPickerType;
	document.getElementById("colorPickerType").value = setPickerType;

	var y, x, i, j;

	if (setPickerType < 2)
	{
		for (y = 0; y < 12; y++)
		{
			SetSwitchColor(0, y, '#000000');
			SetSwitchColor(1, y, specialColors[y]);
			SetSwitchColor(2, y, '#000000');
		}
	}

	switch(setPickerType)
	{
		case 0:
		{
			green = new Array(5, 4, 3, 2, 1, 0, 0, 1, 2, 3, 4, 5);
			blue = new Array(0, 0, 0, 5, 4, 3, 2, 1, 0, 0, 1, 2, 3, 4, 5, 5, 4, 3, 2, 1, 0);

			for (y = 0; y < 12; y++)
			{
				for (x = 3; x < 21; x++)
				{
					r = Math.floor((20 - x) / 6) * 2 + Math.floor(y / 6);
					g = green[y];
					b = blue[x];

					SetSwitchColor(x, y, "#" + colors[r] + colors[g] + colors[b]);
				}
			}
		}
		break;

		case 1:
		{
			green = new Array(0, 0, 0, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5);
			blue = new Array(0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5);

			for (y = 0; y < 12; y++)
			{
				for (x = 3; x < 21; x++)
				{
					r = Math.floor((x - 3) / 6) + Math.floor(y / 6) * 3;
					g = green[x];
					b = blue[y];

					SetSwitchColor(x, y, "#" + colors[r] + colors[g] + colors[b]);
				}
			}
		}
		break;

		case 2:
		{
			i = 255;
			j = -1;

			for (y = 0; y < 12; y++)
			{
				for (x = 0; x < 21; x++)
				{
					SetSwitchColor(x, y, "rgb(" + i + "," + i + "," + i + ")");
					i--;
					if (i == 4)
					{
						i = 0;
					}
				}
			}
		}
		break;

		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		{
			i = 255;
			j = 255;

			for(y = 0; y < 12; y++)
			{
				for (x = 0; x < 21; x++)
				{
					acolor = Math.round(j);
					bcolor = Math.round(i);

					if (acolor < 0)
					{
						acolor = 0;
					}

					switch(setPickerType)
					{
						// 红
						case 3: r = acolor; g = bcolor; b = bcolor; break;
						// 绿
						case 4: r = bcolor; g = acolor; b = bcolor; break;
						// 蓝
						case 5: r = bcolor; g = bcolor; b = acolor; break;
						// 黄
						case 6: r = acolor; g = acolor; b = bcolor; break;
						// 紫
						case 7: r = acolor; g = bcolor; b = acolor; break;
						// 青
						case 8: r = bcolor; g = acolor; b = acolor; break;
					}

					SetSwitchColor(x, y, "rgb(" + r + "," + g + "," + b + ")");

					if (i > 1)
					{
						i -= 2.03174;
					}
					else
					{
						i = 0;
						if (j > 1.03)
						{
							j -= 2.03174;
						}
					}
				}
			}
		}
		break;

		default: return false;
	}
	return true;
}

function SwitchColorPicker(direction)
{
	if (direction > 0)
	{
		if (colorPickerType < 8)
		{
			colorPickerType++;
		}
		else
		{
			colorPickerType = 0;
		}
	}
	else
	{
		if (colorPickerType > 0)
		{
			colorPickerType--;
		}
		else
		{
			colorPickerType = 8;
		}
	}

	InitColorPicker(colorPickerType);
}

// 打开颜色面版
function OpenColorPicker(clickedElementId, e)
{

	pickerElement = document.getElementById("colorPicker");
	if (activeElementId == clickedElementId && pickerElement.style.display != "none")
	{
		pickerElement.style.display = "none";
	}
	else
	{
		activeElementId = clickedElementId;
		colorElement = document.getElementById(clickedElementId);
		var thecolor = null;
		if (colorElement.style.background)
		{
			thecolor = colorElement.style.backgroundColor;
			document.getElementById("oldColor").style.background = thecolor;
			document.getElementById("newColor").style.background = thecolor;
			document.getElementById("txtColor").value = thecolor;
		}
		if (!e)
		{
			e = window.event;
		}
		if(typeof(e.pageX) == "number")
		{
			xpos = e.pageX;
			ypos = e.pageY;
		}
		else if (typeof(e.clientX) == "number")
		{
			xpos = e.clientX + document.documentElement.scrollLeft;
			ypos = e.clientY + document.documentElement.scrollTop;
		}
		xpos += 10;
		ypos += 5;
		if ((xpos + 270) >= document.body.clientWidth)
		{
			xpos = document.body.clientWidth - 270-5;
		}
		pickerElement.style.left = xpos + "px";
		pickerElement.style.top = ypos + "px";
		pickerElement.style.display = "";
	}
}

// 关闭颜色面版
function CloseColorPicker()
{
	activeElementId = 0;
	document.getElementById("colorPicker").style.display = "none";
}

function SwitchOver(e)
{
	ColorOver(this);
}
function SwitchClick(e)
{
	ColorClick(this);
}

function ColorOver(element)
{
	color = FetchHexColor(element.style.backgroundColor);
	document.getElementById("newColor").style.background = color;
	document.getElementById("txtColor").value = color;
}

function ColorClick(element)
{
	color = element == "transparent" ? element : FetchHexColor(element.style.backgroundColor);
	document.getElementById(activeElementId).style.background = color;
	
	if (activeElementId == "FontColor" || activeElementId == "BgColor") {
		Result(activeElementId,color);
	}
	else if (activeElementId == "WatermarkFontColor"){
		document.form.WatermarkFontColor.value=color;
	}
	
	CloseColorPicker();
}

function FetchHexColor(color)
{
	if (color.substr(0, 1) == "r")
	{
		colorMatch = color.match(/rgb\((\d+),\s*(\d+),\s*(\d+)\)/i);
		for (var i = 1; i < 4; i++)
		{
			colorMatch[i] = parseInt(colorMatch[i]).toString(16);
			if (colorMatch[i].length < 2)
			{
				colorMatch[i] = "0" + colorMatch[i];
			}
		}
		color = "#" + (colorMatch[1] + colorMatch[2] + colorMatch[3]).toUpperCase();
	}

	return color.toUpperCase();
}

function IsTransparent(value)
{
	if (value == "" || value == "none" || value == "transparent")
	{
		return true;
	}
	else
	{
		return false;
	}
}



function FetchTags(parentobj, tag)
{
	if (parentobj == null)
	{
		return new Array();
	}
	else if (typeof parentobj.getElementsByTagName != 'undefined')
	{
		return parentobj.getElementsByTagName(tag);
	}
	else if (parentobj.all && parentobj.all.tags)
	{
		return parentobj.all.tags(tag);
	}
	else
	{
		return new Array();
	}
}


document.writeln('<style type="text/css">');
document.writeln('#colorPicker { background: #EFEFEF; position: absolute; left: 0px; top: 0px; width: 270px;}');
document.writeln('#colorFeedback { border: solid 1px #000; border-bottom: none; width: 270px; }');
document.writeln('#colorFeedback input { font: 11px verdana, arial, helvetica, sans-serif; }');
document.writeln('#colorFeedback button { height: 19px; padding-left:1px;padding-right:1px}');
document.writeln('#txtColor { border: inset 1px; width: 60px; }');
document.writeln('#colorSurround { border: inset 1px; white-space: nowrap; width: 66px; height: 15px; }');
document.writeln('#colorSurround td { background-color: none; border: none; width: 33px; height: 15px; }');
document.writeln('#swatches { background-color: #000; width: 270px; }');
document.writeln('#swatches td { background: #EFEFEF; border: none; width: 11px; height: 11px; }');
document.writeln('.tcat { color: #001F30; background: #EEF0F2; background-repeat:repeat-x; border: outset 2px;}');
document.writeln('.colorpreview { font-weight: normal; background: none; border: inset 1px #DEE0E2; width: 45px; height: 21px;}');
document.writeln('</style>');
document.writeln('<div id="colorPicker" style="display:none" oncontextmenu="SwitchColorPicker(1); return false" onmousewheel="SwitchColorPicker(event.wheelDelta * -1); return false;">');
document.writeln('<table id="colorFeedback" class="tcat" cellpadding="0" cellspacing="4" border="0" width="100%">');
document.writeln('<tr>');
document.writeln('<td><button onclick="ColorClick(\'transparent\'); return false" title="透明" style="width:40px">透明</button></td>');
document.writeln('<td>');
document.writeln('<table id="colorSurround" cellpadding="0" cellspacing="0" border="0"><tr>');
document.writeln('<td id="oldColor" onClick="CloseColorPicker()"></td>');
document.writeln('<td id="newColor"></td>');
document.writeln('</tr></table>');
document.writeln('</td>');
document.writeln('<td width="100%"><input id="txtColor" type="text" value="" size="8" /></td>');
document.writeln('<td style="white-space:nowrap">');
document.writeln('<input type="hidden" name="colorPickerType" id="colorPickerType" value="" />');
document.writeln('<button onClick="SwitchColorPicker(1); return false" title="颜色组切换" style="width:40px">切换</button>');
document.writeln('<button onClick="CloseColorPicker(); return false" title="关闭">Ｘ</button>');
document.writeln('</td>');
document.writeln('</tr>');
document.writeln('</table>');


document.writeln('<table id="swatches" cellpadding="0" cellspacing="1" border="0">');

var ColorPannelArray = Array(
	Array('#000000','#000000','#000000','#CCFFFF','#CCFFCC','#CCFF99','#CCFF66','#CCFF33','#CCFF00','#66FF00','#66FF33','#66FF66','#66FF99','#66FFCC','#66FFFF','#00FFFF','#00FFCC','#00FF99','#00FF66','#00FF33','#00FF00'),
	Array('#000000','#333333','#000000','#CCCCFF','#CCCCCC','#CCCC99','#CCCC66','#CCCC33','#CCCC00','#66CC00','#66CC33','#66CC66','#66CC99','#66CCCC','#66CCFF','#00CCFF','#00CCCC','#00CC99','#00CC66','#00CC33','#00CC00'),
	Array('#000000','#666666','#000000','#CC99FF','#CC99CC','#CC9999','#CC9966','#CC9933','#CC9900','#669900','#669933','#669966','#669999','#6699CC','#6699FF','#0099FF','#0099CC','#009999','#009966','#009933','#009900'),
	Array('#000000','#999999','#000000','#CC66FF','#CC66CC','#CC6699','#CC6666','#CC6633','#CC6600','#666600','#666633','#666666','#666699','#6666CC','#6666FF','#0066FF','#0066CC','#006699','#006666','#006633','#006600'),
	Array('#000000','#CCCCCC','#000000','#CC33FF','#CC33CC','#CC3399','#CC3366','#CC3333','#CC3300','#663300','#663333','#663366','#663399','#6633CC','#6633FF','#0033FF','#0033CC','#003399','#003366','#003333','#003300'),
	Array('#000000','#FFFFFF','#000000','#CC00FF','#CC00CC','#CC0099','#CC0066','#CC0033','#CC0000','#660000','#660033','#660066','#660099','#6600CC','#6600FF','#0000FF','#0000CC','#000099','#000066','#000033','#000000'),
	Array('#000000','#FF0000','#000000','#FF00FF','#FF00CC','#FF0099','#FF0066','#FF0033','#FF0000','#990000','#990033','#990066','#990099','#9900CC','#9900FF','#3300FF','#3300CC','#330099','#330066','#330033','#330000'),
	Array('#000000','#00FF00','#000000','#FF33FF','#FF33CC','#FF3399','#FF3366','#FF3333','#FF3300','#993300','#993333','#993366','#993399','#9933CC','#9933FF','#3333FF','#3333CC','#333399','#333366','#333333','#333300'),
	Array('#000000','#0000FF','#000000','#FF66FF','#FF66CC','#FF6699','#FF6666','#FF6633','#FF6600','#996600','#996633','#996666','#996699','#9966CC','#9966FF','#3366FF','#3366CC','#336699','#336666','#336633','#336600'),
	Array('#000000','#FFFF00','#000000','#FF99FF','#FF99CC','#FF9999','#FF9966','#FF9933','#FF9900','#999900','#999933','#999966','#999999','#9999CC','#9999FF','#3399FF','#3399CC','#339999','#339966','#339933','#339900'),
	Array('#000000','#00FFFF','#000000','#FFCCFF','#FFCCCC','#FFCC99','#FFCC66','#FFCC33','#FFCC00','#99CC00','#99CC33','#99CC66','#99CC99','#99CCCC','#99CCFF','#33CCFF','#33CCCC','#33CC99','#33CC66','#33CC33','#33CC00'),
	Array('#000000','#FF00FF','#000000','#FFFFFF','#FFFFCC','#FFFF99','#FFFF66','#FFFF33','#FFFF00','#99FF00','#99FF33','#99FF66','#99FF99','#99FFCC','#99FFFF','#33FFFF','#33FFCC','#33FF99','#33FF66','#33FF33','#33FF00')
);

for(var i=0;i<ColorPannelArray.length;i++){
	document.writeln('<tr>');
	for (var j=0;j<ColorPannelArray[i].length;j++){
		document.writeln('<td style="background:'+ColorPannelArray[i][j]+'" id="sw'+j+'-'+i+'"></td>');
	}
	document.writeln('</tr>');
}
	
var tds = FetchTags(document.getElementById("swatches"), "td");
for (var i = 0; i < tds.length; i++)
{
	tds[i].onclick = SwitchClick;
	tds[i].onmouseover = SwitchOver;
}
document.writeln('</table>');
document.writeln('</div>');