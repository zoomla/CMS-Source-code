String.prototype.trim = function()  
{  
	return this.replace(/(^\s*)|(\s*$)/g, "");  
}
//去除字符串头尾空格或指定字符  
String.prototype.Trim = function (c) {
    if (c == null || c == "") {
        var str = this.replace(/^\s*/, '');
        var rg = /\s/;
        var i = str.length;
        while (rg.test(str.charAt(--i)));
        return str.slice(0, i + 1);
    }
    else {
        var rg = new RegExp("^" + c + "*");
        var str = this.replace(rg, '');
        rg = new RegExp(c);
        var i = str.length;
        while (rg.test(str.charAt(--i)));
        return str.slice(0, i + 1);
    }
}
function GetID(val)
{
    return document.getElementById(val);
}
function IsDisplay(ctl)
{
    var obj = document.getElementById(ctl); 
    obj.style.display = obj.style.display=='none' ? obj.style.display='' :obj.style.display='none';
}
function WinOpen(url,n,w,h)
{
        var left = (screen.width-w)/2;
        var top = (screen.height-h)/2;
        var f = "width="+w+",height="+h+",top="+top+",left="+left;
        var c = window.open(url,n,f);
        return c;
}
//添加图片地址外部链接
function AddPhotoUrl(id,hid)
{
    var obj = document.getElementById(id);
    var thisurl="文件地址"+(obj.length+1)+"|http://";
    var url=prompt("请输入图片地址名称和链接，中间用“|”隔开：",thisurl);
    if(url!=null&&url!='')
    {
        obj.options[obj.length]=new Option(url,url);
    }
    ChangeHiddenFieldValue(id,hid);
}
//添加下载地址外部链接
function AddSoftUrl(id,hid)
{
    var obj = document.getElementById(id);
    var thisurl="下载地址"+(obj.length+1)+"|http://";
    var url=prompt("请输入下载地址名称和链接，中间用“|”隔开：",thisurl);
    if(url!=null&&url!='')
    {
        obj.options[obj.length]=new Option(url,url);
    }
    ChangeHiddenFieldValue(id,hid);
}
//添加运行平台
function ToSystem(addTitle,clientID)
{
    var str=document.getElementById(clientID).value;
    if (str=="")
    {
        document.getElementById(clientID).value=str+addTitle;
    }
    else
    {
        if (str.substr(str.length-1,1)=='/')
        {
            document.getElementById(clientID).value=str+addTitle;
        }
        else
        {
            document.getElementById(clientID).value=str + '/' + addTitle;
        }
    }
    document.getElementById(clientID).focus();
}
function DealwithUploadErrMessage(message)
{
    alert(message);
}
function DealwithUpload(path,size,id,hid,sizeid)
{
    var obj = document.getElementById(id);
    var url='下载地址'+(obj.length+1)+'|'+path;
    obj.options[obj.length]=new Option(url,url);
    ChangeHiddenFieldValue(id,hid);
    if(sizeid!='')
    {
        document.getElementById(sizeid).value = tofloat((size / 1024),2);
    }
}

function DealwithUploadImg(path, id) { 
    document.getElementById(id).src = path;
    $("#" + id).attr('rel', path);
    $(".jqzoom").imagezoom();
}

function DealwithUploadPic(path, id) {
    if (parent.document.getElementById(id)) {
        parent.document.getElementById(id).value = path;
    }
    else {
        document.getElementById(id).value = path;
    }

}
//自动缩放图片， 居中显示
function AutoSetSize(source, maxWidth, maxHeigh) {
    // alert('请等待下， 马上显示预览的图片...');
    //setTimeout("", 2000);
    var width = source.width;
    var height = source.height;
    if (width > maxWidth && height > maxHeigh) {
        if (width / height > maxWidth / maxHeigh) {
            height = maxWidth * height / width;
            width = maxWidth;
        } else {
            width = maxHeigh * width / height;
            height = maxHeigh;
        }
    } else if (width > maxWidth) {
        height = maxWidth * height / width;
        width = maxWidth;
    } else if (height > maxHeigh) {
        width = maxHeigh * width / height;
        height = maxHeigh;
    }
    else {
        width = source.width;
        height = source.height;
    }
    source.style.width = width + "px";
    source.style.height = height + "px";
    source.style.paddingLeft = parseInt((maxWidth - width + 5) / 2) + "px";
    source.style.paddingTop = parseInt((maxHeigh - height + 5) / 2) + "px";
}
function UpdateMultiDrop(values, id) {
    document.getElementsByName(id)[0].value = values;
}
function   tofloat(f,dec)   {     
  if(dec<0)   return   "Error:dec<0!";     
    result=parseInt(f)+(dec==0?"":".");     
    f-=parseInt(f);     
  if(f==0)     
    for(i=0;i<dec;i++)   result+='0';     
  else   {     
    for(i=0;i<dec;i++)   f*=10;     
    result+=parseInt(Math.round(f));     
  }     
  return result;     
}
//上传多图片并给页面控件赋值
function DealwithPhotoUpload(path,id,hid)
{
    var obj = document.getElementById(id);
    var url='图片地址'+(obj.length+1)+'|'+path;
    obj.options[obj.length]=new Option(url,url);
    ChangeHiddenFieldValue(id,hid);    
}
//给缩略图文本框赋值 并改变图片地址框和隐藏值的值
function ChangeThumbField(path,id,hid,thunbid) {
    if (document.getElementById(thunbid) != null) {
        document.getElementById(thunbid).value = path;
    }
        DealwithPhotoUpload(path, id, hid)
    
}
//从已上传文件中选择图片
function SelectFiles(selID,hdnID)
{
    var urlstr= "../Common/ShowUploadFiles.aspx";
    var isMSIE = (navigator.appName == "Microsoft Internet Explorer");
    var arr = "";
    if (isMSIE)
    {
        arr = window.showModalDialog(urlstr, "self,width=570,height=460,resizable=yes,scrollbars=yes");
        if(arr!=null)
        {
            var obj = document.getElementById(selID);
            var url='图片地址'+(obj.length+1)+'|'+arr;
            obj.options[obj.length]=new Option(url,url);
            ChangeHiddenFieldValue(selID,hdnID);
        }
    }
    else
    {
        urlstr = urlstr + "?ClientId="+selID+"&HiddenFieldId="+hdnID+"&type=pic";
        window.open(urlstr,'newWin','modal=yes,width=570,height=460,resizable=yes,scrollbars=yes');
    }
}
function BatchAddFiles(selID,hdnID,NodeID)
{
    var urlstr= "../Common/BatchAddFile.aspx?NodeID="+NodeID;
    var isMSIE= (navigator.appName == "Microsoft Internet Explorer");
    var arr = "";
    if (isMSIE)
    {
        arr= window.showModalDialog(urlstr,"self,width=200,height=50,resizable=yes,scrollbars=yes");
        if(arr!=null)
        {
            var obj = document.getElementById(selID);
            var urlarr=arr.split("|");
            for(i=0;i<urlarr.length;i++)
            {
                var url='图片地址'+(obj.length+1)+'|'+urlarr[i];
                obj.options[obj.length]=new Option(url,url);
            }
            ChangeHiddenFieldValue(selID,hdnID);
        }
    }
    else
    {
        urlstr = urlstr + "?ClientId="+selID+"&HiddenFieldId="+hdnID+"&type=pic";
        window.open(urlstr,'newWin','modal=yes,width=200,height=50,resizable=yes,scrollbars=yes');
    }
}
//从已上传文件中选择文件
function SelectFile(selID,hdnID)
{
    var urlstr= "../Common/ShowUploadFiles.aspx";
    var isMSIE= (navigator.appName == "Microsoft Internet Explorer");
    var arr = "";
    if (isMSIE)
    {
        arr= window.showModalDialog(urlstr,"self,width=570,height=460,resizable=yes,scrollbars=yes");
        if(arr!=null)
        {
            var obj = document.getElementById(selID);
            var url='下载地址'+(obj.length+1)+'|'+arr;
            obj.options[obj.length]=new Option(url,url);
            ChangeHiddenFieldValue(selID,hdnID);
        }
    }
    else
    {
        urlstr = urlstr + "?ClientId="+selID+"&HiddenFieldId="+hdnID+"&type=file";
        window.open(urlstr,'newWin','modal=yes,width=570,height=460,resizable=yes,scrollbars=yes');
    }
}
//修改隐藏文本内容
function ChangeHiddenFieldValue(selID,HdnID)
{
    var obj = document.getElementById(HdnID);
    var photoUrls = document.getElementById(selID);
    var value = "";
    for(i=0;i<photoUrls.length;i++)
    {
        if(value!="")
        {
            value = value+ "$";
        }
        value = value + photoUrls.options[i].value;
    }
    obj.value = value;
}

//从已上传文件中选择缩略图
function SelectThumbFiles(thumbID)
{
    var urlstr= "../Common/ShowUploadFiles.aspx";
    var isMSIE= (navigator.appName == "Microsoft Internet Explorer");
    var arr = "";
    if (isMSIE)
    {
        arr= window.showModalDialog(urlstr,"self,width=200,height=150,resizable=yes,scrollbars=yes");
        if(arr!=null)
        {
            document.getElementById(thumbID).value= arr;
        }
    }
    else
    {
        urlstr = urlstr + "?ThumbClientId="+thumbID;
        window.open(urlstr,'newWin','modal=yes,width=400,height=300,resizable=yes,scrollbars=yes');
    }
}
//修改地址
function ModifyPhotoUrl(id,hid)
{
    var obj = document.getElementById(id);
    if(obj==null)return false;
    if(obj.length==0)return false;
    var thisurl = obj.value;
    if(thisurl=='')
    {
        alert('请先选择一个地址，再点修改按钮！');
        return false;
    }
    var url = prompt('请输入地址名称和链接，中间用“|”隔开：',thisurl);
    if(url!=thisurl&&url!=null&&url!='')
    {
        obj.options[obj.selectedIndex]=new Option(url,url);
    }
    ChangeHiddenFieldValue(id,hid);    
}
//删除地址
function DelPhotoUrl(id,hid)
{  
    var obj = document.getElementById(id);
    if(obj.length==0) return false;
    var thisurl=obj.value; 
    if (thisurl=='')
    {
        alert('请先选择一个地址，再点删除按钮！');
        return false;
    }
    obj.options[obj.selectedIndex]=null;
    ChangeHiddenFieldValue(id,hid);    
}
//返回日期
function returnDate()
{
    var day="";
    var month="";
    var ampm="";
    var ampmhour="";
    var myweekday="";
    var year="";
    mydate=new Date();
    myweekday=mydate.getDay();
    mymonth=mydate.getMonth()+1;
    myday= mydate.getDate();
    myyear= mydate.getYear();
    year=(myyear > 200) ? myyear : 1900 + myyear;
    if(myweekday == 0)
    weekday=" 星期日 ";
    else if(myweekday == 1)
    weekday=" 星期一 ";
    else if(myweekday == 2)
    weekday=" 星期二 ";
    else if(myweekday == 3)
    weekday=" 星期三 ";
    else if(myweekday == 4)
    weekday=" 星期四 ";
    else if(myweekday == 5)
    weekday=" 星期五 ";
    else if(myweekday == 6)
    weekday=" 星期六 ";
    return year+"年"+mymonth+"月"+myday+"日"+weekday;
}

function WinOpenDialog(url,w,h)
{
    if (!w || w == 0) w = 500;
    if (!h || h == 0) h = 500;
    var feature = "dialogWidth:" + w + "px;dialogHeight:" + h + "px;center:yes;status:no;help:no";
    var iefeature = "top=200,left=500,scrollbars=yes,dialog=yes,modal=no,width="+w+",height="+h+",resizable=yes";
    var isMSIE = (navigator.appName == "Microsoft Internet Explorer");
    if (isMSIE) {
        showModalDialog(url, window, feature);
    }
    else {
        window.open(url, window, iefeature);
    }
}
function CheckNumber(val)
{
   var patt=/^\d+$/;
   return patt.test(val) ;
}
function CheckNumberNotZero(val)
{
    var patt=/^[^0]\d*$/
    return patt.test(val) ; 
}
function ImgSize(val)
{

			var height = val.height;
			var width = val.width; 
			if(height>150&&width<=330)
			{
				val.height = 150;
			}
			else if(height<=150&&width>330)
			{
				val.width = 330;
			}
			else if(height>150&&width>330)
			{
				if(height/width>=150/330)
				{
					val.height = 150;
				}
				else
				{
					val.width = 330;
				}
			}
				

}

function CheckHas(uri,input,fileldName,tableName)
{
   var inputStr = escape(input);
   var fileldNameStr = escape(fileldName);
   var tableNameStr = escape(tableName);
   var paramStr = "Input="+input+"&FileldName="+fileldNameStr+"&TableName="+tableNameStr;
   var flag = XmlHttpPostMethodText(uri,paramStr)
   if(flag==1)
   {
        return true;
   }
   else
   {
        return false;
   } 
}
var tid = 0;
var ttid = 4;
function ShowTabs(cid) {
    document.getElementById("TabTitle" + tid).className = "tabtitle";
    document.getElementById("TabTitle" + cid).className = "titlemouseover";
    document.getElementById("Tabs" + tid).style.display = "none";
    document.getElementById("Tabs" + cid).style.display = "";
        tid = cid;
}
//获取颜色
function GetColor(img_val,input_val)
{
	var PaletteLeft,PaletteTop
	var obj = $("colorPalette");
	ColorImg = img_val;
	ColorValue = GetID(input_val);
	if (obj){
		PaletteLeft = getOffsetLeft(ColorImg)
		PaletteTop = (getOffsetTop(ColorImg) + ColorImg.offsetHeight)
		if (PaletteLeft+150 > parseInt(document.body.clientWidth)) PaletteLeft = parseInt(event.clientX)-260;
		if (PaletteTop > parseInt(document.body.clientHeight)) PaletteTop = parseInt(document.body.clientHeight)-165;
		obj.style.left = PaletteLeft + "px";
		obj.style.top = PaletteTop + "px";
		if (obj.style.visibility=="hidden")
		{
			obj.style.visibility="visible";
		}else {
			obj.style.visibility="hidden";
		}
	}
}
function getOffsetTop(elm) {
	var mOffsetTop = elm.offsetTop;
	var mOffsetParent = elm.offsetParent;
	while(mOffsetParent){
		mOffsetTop += mOffsetParent.offsetTop;
		mOffsetParent = mOffsetParent.offsetParent;
	}
	return mOffsetTop;
}
function getOffsetLeft(elm) {
	var mOffsetLeft = elm.offsetLeft;
	var mOffsetParent = elm.offsetParent;
	while(mOffsetParent) {
		mOffsetLeft += mOffsetParent.offsetLeft;
		mOffsetParent = mOffsetParent.offsetParent;
	}
	return mOffsetLeft;
}

function setColor(color)
{
	if(ColorImg.id=='FontColorShow' && color=="#") color='#000000';
	if(ColorImg.id=='FontBgColorShow' && color=="#") color='#FFFFFF';
	if (ColorValue){ColorValue.value = color;}
	if (ColorImg && color.length>1){
		ColorImg.src='../../Images/Rect.gif';
		ColorImg.style.backgroundColor = color;
	}else if(color=='#'){ ColorImg.src='../../Images/rectNoColor.gif';}
	GetID("colorPalette").style.visibility="hidden";
}
function SelectAll(trigger,container)
{
     var obj=GetID(trigger);
     var chks=document.getElementById(container).getElementsByTagName("input");
      for(var i=0;i<chks.length;i++)
      {
           if(chks[i].type=="checkbox")
            {
                chks[i].checked=obj.checked;
             } 
      }
}

 function   coder(str)   
  {   
        var   s   =   "";   
        if   (str.length   ==   0)   return   "";   
        for   (var   i=0;   i<str.length;   i++)   
        {   
              switch   (str.substr(i,1))   
              {   
                      case   "<"     :   s   +=   "&lt;";       break;   
                      case   ">"     :   s   +=   "&gt;";       break;   
                      case   "&"     :   s   +=   "&amp;";     break;   
                      case   "   "     :   s   +=   "&nbsp;";   break;   
                      case   "\""   :   s   +=   "&quot;";   break;   
                      case   "\n"   :   s   +=   "<br>";       break;   
                      default       :   s   +=   str.substr(i,1);   break;   
              }   
        }   
        return   s;   
  }   
//IE和firefox通用的复制到剪贴板的JS函数
function copyToClipboard(txt) 
{        
     if(window.clipboardData)
     {
        window.clipboardData.clearData(); 
        window.clipboardData.setData("Text", txt);
        alert("复制成功！")
     } 
     else if(navigator.userAgent.indexOf("Opera") != -1) 
     {
        window.location = txt;
     }
     else if (window.netscape)
     {
       try 
       {
          netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
       }
       catch (e)
       {
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
        alert("复制成功！") 
     } 
}  
function ShowHidden(obj,Is,abs)
{
    if(GetID(obj).style.display=="")
   {
        GetID(obj).style.display='none';
       if(abs=="left") 
             Is.className="down_bg";
       else if(abs=="right")
            Is.className="modelTitleDown";  
       Is.title='点击显示' ;
   } 
   else
   {
        GetID(obj).style.display='';
       if(abs=="left") 
           Is.className="up_bg"; 
       else if(abs=="right")
           Is.className="modelTitle";
       Is.title='点击隐藏' ;
    } 
}
function SelectColor(t, clientId) {
    var url = "/Common/SelectColor.aspx?d=f&t=6";
    var old_color = (document.getElementById(clientId).value.indexOf('#') == 0) ? '&' + document.getElementById(clientId).value.substr(1) : '&' + document.getElementById(clientId).value;
    if (document.all) {
        var color = showModalDialog(url + "&" + clientId + old_color, "", "dialogWidth:18.5em; dialogHeight:16.0em; status:0");
        if (color != null) {
            document.getElementById(clientId).value = color;
        } else {
            document.getElementById(clientId).focus();
        }
    } else {
        var color = window.open(url + '&' + clientId, "hbcmsPop", "top=200,left=200,scrollbars=yes,dialog=yes,modal=no,width=300,height=260,resizable=yes");
    }
}
function esckeydown() {
    if (event.keyCode == 27) {
        try {
            parentDialog.close();
        } catch (Error) {
        }
    }
}
$(document).keydown(esckeydown);

function SwfFileUpload(path, id, hid) {
    var obj = document.getElementById(id);
    var url = '文件地址' + (obj.length + 1) + '|' + path;
    obj.options[obj.length] = new Option(url, url);
    ChangeHiddenFieldValue(id, hid);
}
function RemoteFile(path, id, hid) {
    var obj = document.getElementById(id);
    var url = path;
    obj.options[obj.length] = new Option(url, url);
    ChangeHiddenFieldValue(id, hid);
}
var Num = 0;
var nn = 0;
function help_show(helpid) {
         Num++;
        var newDiv = document.createElement('div');
        var str = "<div id='help_content'  style='z-index:999;'></div><div id='help_hide'  style='z-index:999;'><a onclick='help_hide(Num)' style='width:20px;color:#666' title='关闭'><span class='fa fa-remove'></span></a></div> ";
        newDiv.innerHTML = str;
        newDiv.setAttribute("Id", "help_div" + Num);
        nn = Num - 1
        jQuery("#help").append(newDiv);
        help_hide(nn);
        jQuery("#help_content").load("/manage/help/" + helpid + ".html", function () { jQuery("#help").show();});
}
function help_hide(Num) {
    jQuery("#help_div" + Num).remove();
}
//------------Coffee
//使用:控件中加个txt='要提示的信息',参数为控件名,详可见SiteDetail.aspx
function checkinfo()//Detect whether the domains and ports is empty;
{
    for (i = 0; i < arguments.length; i++) {
        var arr = document.getElementsByName(arguments[i]);
        for (j = 0; j < arr.length; j++) {
            if (arr[j].value == "") { alert(GetID(arr[j]).attr("txt")); arr[j].focus(); return false; }
        }
    }
    return true;
}
function getParam(paramName) {
    paramValue = "";
    isFound = false;
    if (this.location.search.indexOf("?") == 0 && this.location.search.indexOf("=") > 1) {
        arrSource = decodeURI(this.location.search).substring(1, this.location.search.length).split("&");
        i = 0;
        while (i < arrSource.length && !isFound) {
            if (arrSource[i].indexOf("=") > 0) {
                if (arrSource[i].split("=")[0].toLowerCase() == paramName.toLowerCase()) {
                    paramValue = arrSource[i].split("=")[1];
                    isFound = true;
                }
            }
            i++;
        }
    }
    return paramValue;
}
//注:不同浏览器Cookie不通用
function setCookie(name, value) {
    var today = new Date()
    var expires = new Date()
    expires.setTime(today.getTime() + 1000 * 60 * 60 * 24 * 365)
    document.cookie = name + "=" + escape(value) + "; expires=" + expires.toGMTString()
}
function getCookie(cookie_name) {
    var allcookies = document.cookie;
    var cookie_pos = allcookies.indexOf(cookie_name);
    // 如果找到了索引，就代表cookie存在，
    // 反之，就说明不存在。  
    if (cookie_pos != -1) {
        // 把cookie_pos放在值的开始，只要给值加1即可。   
        cookie_pos += cookie_name.length + 1;
        var cookie_end = allcookies.indexOf(";", cookie_pos);
        if (cookie_end == -1) {
            cookie_end = allcookies.length;
        }
        var value = unescape(allcookies.substring(cookie_pos, cookie_end));
    }
    return value;
}
//------Control
function selectAllByName(obj, name) {
    var allInput = document.getElementsByName(name);
    var loopTime = allInput.length;
    for (i = 0; i < loopTime; i++) {
        if (allInput[i].type == "checkbox") {
            allInput[i].checked = obj.checked;
        }
    }
}
//检测到输入框按回车后的操作,a操作,v值
//页面使用:见InquiryDomName,建议不要submit或return GetEnterCode阻止默认触发
function GetEnterCode(a, v) {
    if (event.keyCode == 13) {
        switch (a) {
            case "click"://解发指定控件单击事件
                $("#" + v).trigger("click");
                break;
            case "focus"://焦点移到指定控件
                $("#" + v).focus();
                break;
        }
        return false;
    }//回车判断结束
    else { return true;}
}
//点击一次后禁止，一般用于提交钮
function disableBtn(o) { setTimeout(function () { o.disabled = true; }, 50) }
function setCutUrl(name, url, dir) {
    $("#Img_" + name).attr("src", url);
    $("#txt_" + name).val(url.replace(dir, ""));
    CloseDiag();
}

function SetCitys(name, value) {
    $("#txt_" + name).val(value);
}
