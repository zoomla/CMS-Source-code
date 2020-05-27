var UserAgent = navigator.userAgent.toLowerCase();
var ie4=document.all&&UserAgent.indexOf("opera")==-1
var ns6=document.getElementById&&!document.all


//COOKIE Start
function getCookie(sName){
	var cookie = "" + document.cookie;
	var start = cookie.indexOf(sName);
	if (cookie == "" || start == -1) 
		return "";
	var end = cookie.indexOf(';',start);
	if (end == -1)
		end = cookie.length;
	return unescape(cookie.substring(start+sName.length + 1,end));
}
function setCookie(sName, value) {
	document.cookie = sName + "=" + escape(value) + ";path="+CookiePath+";";
}
function setCookieForever(sName, value) {
	var expdate = new Date();
	expdate.setFullYear(expdate.getFullYear() + 30);
	var DomainStr = CookieDomain ? " domain=" + CookieDomain+"; " : "";
	document.cookie = sName + "=" + escape(value) + ";path="+CookiePath+";"+DomainStr+"expires="+expdate.toGMTString()+";";
}
//COOKIE END


function $(id) {
	return document.getElementById(id);
}


//UrlPost Start
document.write("<form id=UrlPost method=Post></form>")
function UrlPost(Url){
	$("UrlPost").action = Url;
	document.forms['UrlPost'].submit()
}
//UrlPost END

//XmlDom Start
var XmlDom;
function GetXmlDom() {
	if (window.ActiveXObject) {//IE浏览器
		return new ActiveXObject("Microsoft.XMLDOM");
	}
	else if (document.implementation && document.implementation.createDocument) { //其它浏览器
		return document.implementation.createDocument("","",null);
	}
}
function ReadXMLFile(xmlFilePath){	
	if((UserAgent.indexOf("chrome")>-1)){//chrome
		var xmlObj = Ajax_GetXMLHttpRequest();
		xmlObj.onreadystatechange = function(){
			if(xmlObj.readyState == 4 && xmlObj.status == 200){
				XmlDom = xmlObj.responseXML;
			}
		}
		xmlObj.open ('GET', xmlFilePath, false);
		xmlObj.send(null);
	}
	else {//other browser
		XmlDom = GetXmlDom();
		XmlDom.async = false;
		XmlDom.load(xmlFilePath);
	}
}
function GetNodeValue(objXmlElement)
{
	if(window.ActiveXObject) {	//IE浏览器
		return objXmlElement.text;
	}
	else if(window.XMLHttpRequest) {  //其它浏览器
		try {
			return objXmlElement.firstChild.nodeValue;
		}
		catch(ex) {
			return "";
		}
	}
}

function ShowMenuList(XMLUrl){
	ReadXMLFile(XMLUrl);
	var SubMenuStr,MenuNode;
	var XmlDomRoot = XmlDom.documentElement;
	var CategoryNode = XmlDomRoot.getElementsByTagName('Category');
	for (var i=0; i<CategoryNode.length; i++) {
		SubMenuStr="";
		MenuNode = CategoryNode[i].getElementsByTagName('Menu');
		
		for (var j=0; j<MenuNode.length; j++){
			SubMenuStr+="<div class=menuitems><a href=javascript:UrlPost(&quot;"+MenuNode[j].getAttributeNode("Url").nodeValue+"&quot;)>"+GetNodeValue(MenuNode[j])+"</a></div>";
		}
		$("MenuListID").innerHTML += " | <a href=javascript:UrlPost(&quot;"+CategoryNode[i].getAttributeNode("Url").nodeValue+"&quot;) onmouseover=\"showmenu(event,'"+SubMenuStr+"')\">"+CategoryNode[i].getAttributeNode("Name").nodeValue+"</a>";
	}
}
//XmlDom End



//跳转页面显示
function ShowPage(TotalPage,PageIndex,url){
	document.write("<form onsubmit=\"return false;\"><table style='clear: both;'><tr><td valign='baseline' class='PageInation'><a class=MultiPages>"+PageIndex+"/"+TotalPage+"</a>");
	if (PageIndex<6) {
		PageLong=11-PageIndex;
	}
	else
		if (TotalPage-PageIndex<6) {
			PageLong=10-(TotalPage-PageIndex)
		}
		else {
			PageLong=5;
		}
	for (var i=1; i <= TotalPage; i++) {
		if (i < PageIndex+PageLong && i > PageIndex-PageLong || i==1 || i==TotalPage){
			if (PageIndex==i) {
				document.write("<a class=CurrentPage>"+ i +"</a>");
			}
			else {
				document.write("<a class=PageNum href=?PageIndex="+i+"&"+url+">"+ i +"</a>");
			}
		}
	}
	document.write("<input onkeydown=if((event.keyCode==13)&&(this.value!=''))window.location='?PageIndex='+this.value+'&"+url+"'; onkeyup=if(isNaN(this.value))this.value='' class=PageInput></td></tr></table></form>");
}

//全选复选框
function CheckAll(form){
	for (var i=0;i<form.elements.length;i++){
		var e = form.elements[i];
		if (e.name != 'chkall' && e.id != 'checkall_ed2klist' && e.name != 'ed2klist' && e.type=="checkbox" && e.checked!=form.chkall.checked){e.click();}
	}
}




//菜单

var menuOffX=0		//菜单距连接文字最左端距离
var menuOffY=18		//菜单距连接文字顶端距离

function showmenu(e,vmenu,mod){
	if (!vmenu){return false;} //如果空则不显示
	var which=vmenu
	menuobj=$("popmenu")
	menuobj.innerHTML=which
	menuobj.contentwidth=menuobj.offsetWidth
	eventX=e.clientX
	eventY=e.clientY
	var rightedge=document.body.clientWidth-eventX
	var bottomedge=document.body.clientHeight-eventY
	var getlength
	if (rightedge<menuobj.contentwidth){
		getlength=ie4? document.body.scrollLeft+eventX-menuobj.contentwidth+menuOffX : ns6? window.pageXOffset+eventX-menuobj.contentwidth : eventX-menuobj.contentwidth
	}else{
		getlength=ie4? ie_x(event.srcElement)+menuOffX : ns6? window.pageXOffset+eventX : eventX
	}
	menuobj.style.left=getlength+'px'
	
	if (bottomedge<menuobj.contentheight&&mod!=0){
		getlength=ie4? document.body.scrollTop+eventY-menuobj.contentheight-event.offsetY+menuOffY-23 : ns6? window.pageYOffset+eventY-menuobj.contentheight-10 : eventY-menuobj.contentheight
	}
	else{
		getlength=ie4? ie_y(event.srcElement)+menuOffY : ns6? window.pageYOffset+eventY+10 : eventY
	}
	menuobj.style.top=getlength+'px'
	
	menuobj.style.visibility="visible"
}

function ie_y(e){  
	var t=e.offsetTop;  
	while(e=e.offsetParent){  
		t+=e.offsetTop;  
	}  
	return t;  
}  
function ie_x(e){  
	var l=e.offsetLeft;  
	while(e=e.offsetParent){  
		l+=e.offsetLeft;  
	}  
	return l;  
}  

function highlightmenu(e,state){
	if (document.all)
		source_el=event.srcElement
	else if (document.getElementById)
		source_el=e.target
	if (source_el.className!="menuskin" && source_el.className!=""){
		source_el.className=(state=="on")? "mouseoverstyle" : "menuitems";
	}
	else{
		while(source_el.id!="popmenu"){
			source_el=document.getElementById? source_el.parentNode : source_el.parentElement
			if (source_el.className!="menuskin" && source_el.className!=""){
				source_el.className = (state=="on")? "mouseoverstyle" : "menuitems"
			}
		}
	}
}

function hidemenu(){if (window.menuobj)menuobj.style.visibility="hidden"}

function contains_ns6(a, b) {
        while (b.parentNode)
                if ((b = b.parentNode) == a)
                        return true;
        return false;
}
function dynamichide(e){
        if (ie4&&!menuobj.contains(e.toElement))
                hidemenu()
        else if (ns6&&e.currentTarget!= e.relatedTarget&& !contains_ns6(e.currentTarget, e.relatedTarget))
                hidemenu()
}

document.onclick=hidemenu
document.write("<div class=menuskin id=popmenu onmouseover=highlightmenu(event,'on') onmouseout=highlightmenu(event,'off');dynamichide(event)></div>")
// 菜单END


//add area script
function ValidateTextboxAdd(box, button)
{
 var buttonCtrl = $(button);
 if ( buttonCtrl != null )
 {
 if (box.value == "" || box.value == box.Helptext)
 {
 buttonCtrl.disabled = true;
 }
 else
 {
 buttonCtrl.disabled = false;
 }
 }
}
//add area script end

//Ajax Start
function Ajax_GetXMLHttpRequest() {
	if (window.ActiveXObject) {
		return new ActiveXObject("Microsoft.XMLHTTP");
	} 
	else if (window.XMLHttpRequest) {
		return new XMLHttpRequest();
	}
}
function Ajax_CallBack(FormName,ID,URL,IsAlert){
	var x = Ajax_GetXMLHttpRequest();
	var ID = $(ID);
	x.open("POST",URL);
	x.setRequestHeader("REFERER", location.href);
	x.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	x.onreadystatechange = function(){if(x.readyState == 4 && x.status == 200){if(IsAlert){x.responseText?alert(x.responseText):alert('添加成功');}else if(ID){ID.innerHTML=x.responseText;}}}
	var encodedData=""
	if (document.forms.length > 0 && FormName) {
		var form = document.forms[FormName];
		for (var i = 0; i < form.length; ++i) {
			var element = form.elements[i];
			if (element.name) {
				var elementValue = null;
				if (element.nodeName == "INPUT") {
					var inputType = element.getAttribute("TYPE").toUpperCase();
					if (inputType == "TEXT" || inputType == "PASSWORD" || inputType == "HIDDEN") {
						elementValue = escape(element.value);
					} else if (inputType == "CHECKBOX" || inputType == "RADIO") {
						if (element.checked) {
							elementValue = escape(element.value);
						}
					}
				} else if (element.nodeName == "SELECT" || element.nodeName == "TEXTAREA") {
					elementValue = escape(element.value);
				}
				if (elementValue) {
					if(encodedData==""){
						encodedData = element.name + "=" + encodeURIComponent(elementValue);
					}
					else{
						encodedData += "&" + element.name + "=" + encodeURIComponent(elementValue);
					}
				}
			}
		}

	}
	x.send(encodedData);
}

//Ajax End



function loadThreadFollow(Paramater){
	var targetImg =$("followImg");
	var targetDiv =$("follow");
	if (targetDiv.style.display!='block'){
		Ajax_CallBack(false,"followTd","loading.asp?menu=UsersOnline&"+Paramater+"")
		targetDiv.style.display="block";
		targetImg.src="images/minus.gif";
	}else{
		targetDiv.style.display="none";
		targetImg.src="images/plus.gif";
	}
}

function ToggleMenuOnOff (menuName) {
	var menu = $(menuName);
	if (menu.style.display == 'none') {
		menu.style.display = '';
	} else {
		menu.style.display = 'none';
	}	
}

function ForumGroupToggleCollapsed(id) {
	var menu = $("ForumGroup"+id);
	var menuImg =$("ForumGroupImg"+id);
	if (menu.style.display == 'none') {
		menu.style.display = '';
		menuImg.src="images/group_collapse.gif";
		setCookie("ForumGroupDisplay"+id,"")
	} else {
		menu.style.display = 'none';
		menuImg.src="images/group_expand.gif";
		setCookieForever("ForumGroupDisplay"+id,"none")
	}
}

function OpenWindow (target) { 
  window.open(target, "_Child", "toolbar=no,scrollbars=yes,resizable=yes,width=400,height=400"); 
}


function log_out()
{
	ht = document.getElementsByTagName("html");
	ht[0].style.filter = "progid:DXImageTransform.Microsoft.BasicImage(grayscale=1)";
	if (confirm('你确定要退出？'))
	{
		UrlPost("Login.asp?Menu=OUT");
	}
	else
	{
		ht[0].style.filter = "";
		return false;
	}
}


function ShowPannel(btn){
	var idname = new String(btn.id);
	var s = idname.indexOf("_");
	var e = idname.lastIndexOf("_")+1;
	var tabName = idname.substr(0, s);
	var id = parseInt(idname.substr(e, 1));
	var tabNumber = btn.parentNode.childNodes.length;
	for(i=0;i<tabNumber;i++){
		document.getElementById(tabName+"_tab_"+i).style.display = "none";
		document.getElementById(tabName+"_btn_"+i).className = "";
	};
	document.getElementById(tabName+"_tab_"+id).style.display = "block";
	btn.className = "NowTag";
}

function AjaxShowPannel(btn){
	var idname = new String(btn.id);
	var s = idname.indexOf("_");
	var e = idname.lastIndexOf("_")+1;
	var tabName = idname.substr(0, s);
	var id = idname.substr(s, e-s);
	var tabNumber = btn.parentNode.childNodes.length;
	var menu =  new String(btn.getAttributeNode("menu").nodeValue);	//getAttributeNode("menu").nodeValue 兼容FF3。0
	var ajaxurl = new String(btn.parentNode.getAttributeNode("ajaxurl").nodeValue) + '&' + menu;
	for(i=0;i<tabNumber;i++){
		document.getElementById(tabName+id+i).className = "";
	};
	btn.className = "NowTag";
	$(tabName).innerHTML = '<table cellspacing=0 cellpadding=0 width="100%" class="PannelBody"><tr><td><img src="images/loading.gif" border=0 /></td></tr></table>';
	Ajax_CallBack(false,tabName,ajaxurl);
}


function ShowCheckResult(ObjectID, Message, ImageName) {
	obj = $(ObjectID);
	obj.style.display = '';
	obj.innerHTML = '<img src="images/check_'+ImageName+'.gif" align=absmiddle>&nbsp;' + Message;
}

/*显示验证码*/
function getVerifyCode() {
	if(document.getElementById("VerifyCodeImgID"))
		document.getElementById("VerifyCodeImgID").innerHTML = '<img src="VerifyCode.asp?t='+Math.random()+'" alt="点击刷新验证码" style="cursor:pointer;border:0;vertical-align:middle;" onclick="this.src=\'VerifyCode.asp?t=\'+Math.random()" />'
}
function CheckVerifyCode(VerifyCode) {
	var patrn=/^\d+$/;		//纯数字
	if(!patrn.exec(VerifyCode)) {
		ShowCheckResult("CheckVerifyCode", "您没有输入验证码或输入有误。","error");
		return;
	}
	Ajax_CallBack(false,"CheckVerifyCode","Loading.asp?menu=CheckVerifyCode&VerifyCode=" + VerifyCode);
}


function CheckSelected(form,checked,TargetID){
	$(TargetID).className = checked ? "CommonListCellChecked" : "CommonListCell";
	if (checked == false)form.chkall.checked = checked;
}



//讯息提示
function MsgGet()
{
	$(MsgDivID).style.visibility="visible"
	try{
	divTop = parseInt($(MsgDivID).style.top,10)
	divLeft = parseInt($(MsgDivID).style.left,10)
	divHeight = parseInt($(MsgDivID).offsetHeight,10)
	divWidth = parseInt($(MsgDivID).offsetWidth,10)
	docWidth = document.documentElement.clientWidth;
	docHeight = document.documentElement.clientHeight;
	$(MsgDivID).style.top = (parseInt(document.documentElement.scrollTop,10) + docHeight + 10) + 'px';//  divHeight
	$(MsgDivID).style.left = (parseInt(document.documentElement.scrollLeft,10) + docWidth - divWidth) + 'px';
	objTimer = window.setInterval("MsgMove()",10)
	}
	catch(e){}
}

function MsgResize()
{
	try{
	divHeight = parseInt($(MsgDivID).offsetHeight,10)
	divWidth = parseInt($(MsgDivID).offsetWidth,10)
	docWidth = document.documentElement.clientWidth;
	docHeight = document.documentElement.clientHeight;
	$(MsgDivID).style.top = (docHeight - divHeight + parseInt(document.documentElement.scrollTop,10)) + 'px';
	$(MsgDivID).style.left = (docWidth - divWidth + parseInt(document.documentElement.scrollLeft,10)) + 'px';
	}
	catch(e){}
}

function MsgMove()
{
	try
	{
	if(parseInt($(MsgDivID).style.top,10) <= (docHeight - divHeight + parseInt(document.documentElement.scrollTop,10)))
	{
	window.clearInterval(objTimer)
	objTimer = window.setInterval("MsgResize()",1)
	}
	divTop = parseInt($(MsgDivID).style.top,10)
	$(MsgDivID).style.top = (divTop - 1) + 'px';
	}
	catch(e){}
}
function MsgClose()
{
	$(MsgDivID).style.visibility='hidden';
	if(objTimer) window.clearInterval(objTimer)
}
//讯息 END



function checkAll(str,checked) {
    var a = document.getElementsByName(str);
    var n = a.length;

    for (var i = 0; i < n; i++) {
        a[i].checked = checked;
    }
}

function em_size(str) {
    var a = document.getElementsByName(str);
    var n = a.length;
    try {
        var input_checkall = document.getElementById("checkall_"+str);
        input_checkall.checked = true ;
        for (var i=0; i < n; i++) {
            if (!a[i].checked) {
                input_checkall.checked = false;
				break;
            }
        }
    } catch (e) {}
}
function gen_size(val, li, sepa) {
	if (parseInt(val)<1) return 0;
    sep = Math.pow(10, sepa); //小数点后的位数
    li = Math.pow(10, li); //开始截断的长度
    retval  = val;
    unit    = 'Bytes';
    if (val >= li*1000000000) {
        val = Math.round( val / (1099511627776/sep) ) / sep;
        unit  = 'TB';
    } else if (val >= li*1000000) {
        val = Math.round( val / (1073741824/sep) ) / sep;
        unit  = 'GB';
    } else if (val >= li*1000) {
        val = Math.round( val / (1048576/sep) ) / sep;
        unit  = 'MB';
    } else if (val >= li) {
        val = Math.round( val / (1024/sep) ) / sep;
        unit  = 'KB';
    }
    return val +' '+ unit;
}

function copy(str) {
    var a = document.getElementsByName(str);
    var n = a.length;
    var ed2kcopy = "";
    for (var i = 0; i < n; i++) {
        if(a[i].checked) {
            ed2kcopy += a[i].value;
            ed2kcopy += "\r\n";
        }
    }
    copyToClipboard(ed2kcopy);
}
function copyToClipboard(txt) {
	if(window.clipboardData) {
   		window.clipboardData.clearData();
   		window.clipboardData.setData("Text", txt);
	} else if(navigator.userAgent.indexOf("Opera") != -1) {
		window.location = txt;
	} else if (window.netscape) {
		try {
			netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
		} catch (e) {
			alert("被浏览器拒绝！\n请在浏览器地址栏输入'about:config'并回车\n然后将'signed.applets.codebase_principal_support'设置为'true'");
		}
		var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
		if (!clip)
			return;
		var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
		if (!trans)
			return;
		trans.addDataFlavor('text/unicode');
		var str = new Object();
		var len = new Object();
		var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
		var copytext = txt;
		str.data = copytext;
		trans.setTransferData("text/unicode",str,copytext.length*2);
		var clipid = Components.interfaces.nsIClipboard;
		if (!clip)
			return false;
		clip.setData(trans,null,clipid.kGlobalClipboard);
	}
}
function Download(str, i, first) {
    var a = document.getElementsByName(str);
    var n = a.length;

	//尝试使用activex方式批量新增下载
	try {
		var ed2k_links = '';
		var ax = new ActiveXObject("IE2EM.IE2EMUrlTaker");
		var emule_version = ax.GetEmuleVersion();
		if ('e' != emule_version.substr(0,1)) {
			throw {errorCode:'eMule not Installed.'};
		}
		for (var i = i; i < n; i++) {
			if(a[i].checked) {
				if (ed2k_links=='') {
					ed2k_links = a[i].value;
				} else {
					ed2k_links += "\n"+a[i].value;
				}
			}
		}
		ax.SendUrl(ed2k_links, 'dd', document.location);
		delete ax;
		return;
	} catch (e) {}

	if (!window.continueDown) {
		//使用最旧的方法来批量新增下载
		for (var i = i; i < n; i++) {
			if(a[i].checked) {
				window.location=a[i].value;
				if (first)
					timeout = 6000;
				else
					timeout = 500;
				i++;
				window.setTimeout("Download('"+str+"', "+i+", 0)", timeout);
				break;
			}
		}
	} else {
		//使用稍微新一点的方法来批量新增下载
		for (var i = i; i < n; i++) {
			if(a[i].checked) {
				if(first){
					var k = i;
					var current_link = a[k].nextSibling;
					var multi_text = '';
					var tmp_counter = 0;
					var comma = '';
					while(true){
						if(a[k].checked && current_link){//如果是有效节点并且被选中
							if(current_link.href){
								if(current_link.href.indexOf('ed2k') !== 0){
									current_link = current_link.nextSibling;
									continue; 
								}
								if(tmp_counter > 7){//收集超过若干个有效链接后，退出
									multi_text += '<br />…………'; 
									break; 
								}
								var right_link = current_link;
								tmp_counter++;
								if (navigator.userAgent.toLowerCase().indexOf("msie")==-1) {
									multi_text += comma+current_link.text;
								}else{
									multi_text += comma+current_link.innerText;
								}
								comma = '<br />';
							}

							current_link = current_link.nextSibling;
						}else{//未被选中，或往下没有相邻节点了，那么切换到下个父节点
							if(++k >= n){//如果父节点也到底了，那么退出
								break; 
							}
							current_link = a[k].nextSibling;
						}
					}
					downPopup(right_link,multi_text);
				}

				continueDown(a[i].value);
				//window.location=a[i].value;
				if (first)
					timeout = 6000;
				else
					timeout = 500;
				i++;
				window.setTimeout("Download('"+str+"', "+i+", 0)", timeout);
				break;
			}
		}
	}

}

function download(ed2k_links) {
	//尝试使用activex方式批量新增下载
	try {
		var ax = new ActiveXObject("IE2EM.IE2EMUrlTaker");
		var emule_version = ax.GetEmuleVersion();
		if ('e' != emule_version.substr(0,1)) {
			throw {errorCode:'eMule not Installed.'};
		}
		ax.SendUrl(ed2k_links, 'dd', document.location);
		delete ax;
		return;
	} catch (e) {}

	if (!window.continueDown) { //使用最旧的方法来批量新增下载
		window.location=ed2k_links;
	}
}