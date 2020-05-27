<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectsView.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.Project.ProjectsView" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>项目预览</title>
<script language="JavaScript1.2"> 
//-- 控制层删除start of script -->
function ssdel(){
if (event)
{
lObj = event.srcElement ;
while (lObj && lObj.tagName != "DIV") lObj = lObj.parentElement ;
}
var id=lObj.id
     document.getElementById(id).removeNode(true);
     //document.getElementById(id).outerHTML="";//
  }
//-- 控制层删除End of script -->
</script>
<script> 
//-- 控制层移动start of script -->
var Obj=''
var index=10000;//z-index;
document.onmouseup=MUp
document.onmousemove=MMove
 
function MDown(Object){
Obj=Object.id
document.all(Obj).setCapture()
pX=event.x-document.all(Obj).style.pixelLeft;
pY=event.y-document.all(Obj).style.pixelTop;
}
 
function MMove(){
if(Obj!=''){
 document.all(Obj).style.left=event.x-pX;
 document.all(Obj).style.top=event.y-pY;
 }
}
 
function MUp(){
if(Obj!=''){
 document.all(Obj).releaseCapture();
 Obj='';
 }
}
//-- 控制层移动end of script -->
//获得焦点;
function getFocus(obj)
{
       if(obj.style.zIndex!=index)
       {
               index = index + 2;
               var idx = index;
               obj.style.zIndex=idx;
               //obj.nextSibling.style.zIndex=idx-1;
       }
}
//查找祝福纸条
function Srch(obj)
{//alert(obj);
	if(obj.style.zIndex!=index)
	{
		index = index + 1000;
		var idx = index;
		obj.style.zIndex = idx;
		obj.style.width = 400;
		obj.style.height = 300;
		obj.style.left = 200;
		obj.style.top = 200;
	}
}
function ChkSrch()
{
if (SrchFrm.srchId.value == "")
	{alert("\n\n\n你总得告诉我查找哪一个吧！！\n\n\n");
	SrchFrm.srchId.focus();
	return false; }
return true;
}
 
function checkNum(obj){
	var checkOK = "0123456789 ";
	var checkStr = obj.value;
	var allValid = true;
	for (i = 0;  i < checkStr.length;  i++){
		ch = checkStr.charAt(i);
		for (j = 0;  j < checkOK.length;  j++)
		if (ch == checkOK.charAt(j))
			break;
		if (j == checkOK.length){
			allValid = false;
			break;
		}
	}
	if (!allValid){
		alert("只能由数字组成！");
		obj.select();
		return (false);
	}
	return (true);	
}
</script>

</head>
<style type="text/css">
body {
	margin-left: 5px;
	margin-top: 0px;
	margin-right: 5px;
	margin-bottom: 0px;
}
table {word-break:break-all;}
.tdb {
	background-image: url(images/love_04.jpg);
	background-repeat: no-repeat;
	background-position: right top;
}
td {
	font-size: 12px;
	color: #000000;
	line-height: 18px;
}
.white {
	color: #FFFFFF;
	text-decoration: none; 
}
</style>
<body style="background-image: url('images/pic738x57.jpg')">
<table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/topbg.jpg">
  <tr>
    <td width="289" ><a href="/" title="返回首页"><img src="/Skin/ncdxlm/logo.png" width="289" height="79" class="tdb" alt="" /></a></td>
    <td align="right" valign="bottom" class="tdb">
	<table width="300" border="0" cellspacing="4" cellpadding="0">
	<form name="SrchFrm" action="find.asp" onsubmit="return ChkSrch();">
      <tr>
        <td align="left" valign="bottom"><label>
			第<input size=5 name="srchId" type="text" onkeyup="checkNum(this);"/>条
			<input type="submit" value="查找心愿纸条" style="background-color:#F33B78; border:1px outset #F33B78;color:#FFFFFF;font-weight:bold; "/> 
        </label></td>
      </tr>
	  </form>
    </table></td>
  </tr>
</table>
<table width="100%" border="0" cellpadding="1" cellspacing="0" bgcolor="F33B78">
  <tr>
    <td><a href="input.htm"><img src="images/love_04.gif" width="153" height="20" border="0" /></a></td>
    <td align="right"><span class="white">&nbsp;&nbsp;&nbsp;&nbsp;将您的祝福语编辑填写好即可发布到本站许愿墙[发布许愿全部免费]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;共有 <asp:Label ID="lblCount" runat="server"></asp:Label> 张祝福&nbsp;</span>&nbsp;</td>
  </tr>
</table>
<div id='<%#Eval("ID") %>' class='Cccc' style='position:absolute;left:185px;top:430px;background-color:;z-index:52;' onmousedown='getFocus(this)'>
<table border="0">
<tr>
<td style='cursor:move;' width='98%' onmousedown="MDown(cc52)">第[<asp:Label ID="lblList" runat="server"></asp:Label>]条&nbsp;<%#Eval("ApplicationTime")%></td>
<td style='cursor:hand;' onclick='ssdel()' width='2%'><%#Eval("Name")%></td>
</tr>
<tr>
<td style='height:100px;padding:5px;' colspan='2'>
<div style='padding:5px;float:right;'><%#Eval("Leader")%></div>
</td>
</tr>
</table>
</div>
</body>
</html>