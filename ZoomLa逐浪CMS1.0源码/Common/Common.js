String.prototype.trim = function()  
{  
	return this.replace(/(^\s*)|(\s*$)/g, "");  
}
function $(val)
{
    return document.getElementById(val);
}
function IsDisplay(ctl)
{
    var obj=$(ctl);
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
    var thisurl="图片地址"+(obj.length+1)+"|http://";
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
function DealwithUploadPic(path,id)
{
    document.getElementById(id).value = path;
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
function ChangeThumbField(path,id,hid,thunbid)
{
    document.getElementById(thunbid).value = path;
    DealwithPhotoUpload(path,id,hid)
}
//从已上传文件中选择图片
function SelectFiles(selID,hdnID)
{
    var urlstr= "../Common/ShowUploadFiles.aspx";
    var isMSIE= (navigator.appName == "Microsoft Internet Explorer");
    var arr = "";
    if (isMSIE)
    {
        arr= window.showModalDialog(urlstr,"self,width=200,height=150,resizable=yes,scrollbars=yes");
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
        window.open(urlstr,'newWin','modal=yes,width=400,height=200,resizable=yes,scrollbars=yes');
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
        arr= window.showModalDialog(urlstr,"self,width=200,height=150,resizable=yes,scrollbars=yes");
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
        window.open(urlstr,'newWin','modal=yes,width=400,height=300,resizable=yes,scrollbars=yes');
    }
}
function AddToSpecial()
{
    var urlstr= "SpecialList.aspx";
    var isMSIE= (navigator.appName == "Microsoft Internet Explorer");
    var arr = null;
    if (isMSIE)
    {
        arr= window.showModalDialog(urlstr,"self,width=200,height=150,resizable=yes,scrollbars=yes");
        if(arr!=null)
        {
            UpdateSpecial(arr);
        }
    }
    else
    {
        window.open(urlstr,'newWin','modal=yes,width=200,height=150,resizable=yes,scrollbars=yes');
    }
}
function UpdateSpecial(arr)
{
    var arrNodes=arr.split(',');
    var HdnSpecial= document.getElementById("HdnSpec");
    var SelectedSpecial = HdnSpecial.value.split(",");
    var isExist = false;
    for(i=0;i<SelectedSpecial.length;i++)
    {
        if(SelectedSpecial[i] == arrNodes[0])
        {isExist = true;}
    }

    if(!isExist)
    {
        if(HdnSpecial.value != '')
        {HdnSpecial.value = HdnSpecial.value + ',';} 
 
        HdnSpecial.value = HdnSpecial.value + arrNodes[0]; 
  
        var newli = document.createElement("SPAN");  
        newli.setAttribute("id","SpecialSpanId" + arrNodes[0]);
        newli.innerHTML = arrNodes[1] + " ";
        var newlink = document.createElement("INPUT");
        newlink.onclick = function() { DelSpecial(arrNodes[0]);};
        newlink.setAttribute("type", "button");
        newlink.setAttribute("class", "button");
        newlink.setAttribute("value", "从此专题中移除");
        newli.appendChild(newlink);
        var newbr = document.createElement("BR");  
        newli.appendChild(newbr);
        var links = document.getElementById("lblSpec");
        links.appendChild(newli);
        DelSpecial('0');
    }
}
function DelSpecial(specialId)
{
     var li = document.getElementById("SpecialSpanId" + specialId);
     if(li != null)
     {
     li.parentNode.removeChild(li);
     }
     var hdnSpecial = document.getElementById("HdnSpec");
     var SelectedSpecial = hdnSpecial.value.split(",");
     var newselected = '';
     for(i=0;i<SelectedSpecial.length;i++)
     {
      if(SelectedSpecial[i] != specialId){ if(newselected != ''){newselected = newselected + ',';} newselected = newselected+SelectedSpecial[i]; }
     }
     hdnSpecial.value = newselected;
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
    var feature = "dialogWidth:"+w+"px;dialogHeight:"+h+"px;center:yes;status:no;help:no";
    //WinOpenDialog('../Common/SelectTemplate.aspx?ControlId=TextBox1&StartPath='+escape('/Template'),650,480)"
    showModalDialog(url,window,feature);
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
var tid=0;
function ShowTabs(cid)
{
    if(cid!=tid)
    {
        $("TabTitle"+tid).className="tabtitle";
        $("TabTitle"+cid).className="titlemouseover";
        $("Tabs"+tid).style.display="none";
        $("Tabs"+cid).style.display="";
        tid=cid;
    }
}


//获取颜色
function GetColor(img_val,input_val)
{
	var PaletteLeft,PaletteTop
	var obj = $("colorPalette");
	ColorImg = img_val;
	ColorValue = $(input_val);
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
	$("colorPalette").style.visibility="hidden";
}
 function SelectAll(trigger,container)
{
     var obj=$(trigger);
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
    if($(obj).style.display=="")
   {
        $(obj).style.display='none';
       if(abs=="left") 
             Is.className="down_bg";
       else if(abs=="right")
            Is.className="modelTitleDown";  
       Is.title='点击显示' ;
   } 
   else
   {
        $(obj).style.display='';
       if(abs=="left") 
           Is.className="up_bg"; 
       else if(abs=="right")
           Is.className="modelTitle";
       Is.title='点击隐藏' ;
    } 
}


function GetReadUserInfo()
{
    alert("1111111111");
    document.write("<script type='text/javascript' src='~/ajaxpro/prototype.ashx'></script>");
    document.write("<script type='text/javascript' src='~/ajaxpro/core.ashx'></script>");
    document.write("<script type='text/javascript' src='~/ajaxpro/converter.ashx'></script>");
    document.write("<script type='text/javascript' src='~/ajaxpro/common_ReadUserInfo,App_Web_oaidv3np.ashx'></script>");
    alert("22222222222222");
    common_ReadUserInfo.ReadUserInfo("",UserInfo_Call_Back);
}