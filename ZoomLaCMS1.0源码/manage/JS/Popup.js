
 var tipTimer; 
    function locateObject(n, d) 
    { 
        var p,x; 
        if (!d) d=document; 
        if ((p=n.indexOf('?')) > 0 && parent.frames.length) 
        {
            d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);
            } 
        return x=d.getElementById(n); 
    } 
    function ShowADPreview(ADContent) 
    { 
         showTooltip('dHTMLADPreview', ADContent, '#ffffff','#000000','#000000','6000');
    } 
    function showTooltip(object,tipContent, backcolor, bordercolor, textcolor, displaytime) 
    { 
        window.clearTimeout(tipTimer) 
        var evt=getEvent();
        if (document.all) { 
            locateObject(object).style.top=document.body.scrollTop+evt.clientY+20 
            locateObject(object).innerHTML='<table style="border-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: '+backcolor+'" border="0" cellspacing="0" cellpadding="0"><tr><td nowrap><font style="font-family:宋体; font-size: 9pt; color: '+textcolor+'">'+unescape(tipContent)+'</font></td></tr></table> ' 
            if ((evt.clientX + locateObject(object).clientWidth) > (document.body.clientWidth + document.body.scrollLeft)) { 
                locateObject(object).style.left = (document.body.clientWidth + document.body.scrollLeft) - locateObject(object).clientWidth-10; 
            } else { 
                locateObject(object).style.left=document.body.scrollLeft+evt.clientX 
            } 
            locateObject(object).style.visibility='visible'; 
            tipTimer=window.setTimeout("hideTooltip('"+object+"')", displaytime); 
            return true; 
        } else if (document.layers) {    
            locateObject(object).document.write('<table width="10" border="0" cellspacing="1" cellpadding="1"><tr bgcolor="'+bordercolor+'"><td><table width="10" border="0" cellspacing="0" cellpadding="0"><tr bgcolor="'+backcolor+'"><td nowrap><font style="font-family:宋体; font-size: 9pt; color: '+textcolor+'">'+unescape(tipContent)+'</font></td></tr></table></td></tr></table>') 
            locateObject(object).document.close() 
            locateObject(object).top=evt.clientY+20 
            if ((evt.clientX + locateObject(object).clip.width) > (window.pageXOffset + window.innerWidth)) { 
                locateObject(object).left = window.innerWidth - locateObject(object).clip.width-10; 
            } else { 
                locateObject(object).left=evt.clientX; 
            } 
            locateObject(object).visibility='show'; 
            tipTimer=window.setTimeout("hideTooltip('"+object+"')", displaytime); 
            return true; 
            } else { 
                locateObject(object).style.top=document.body.scrollTop+evt.clientY+5+"px";
                locateObject(object).innerHTML='<table width="100" height="100" style="border-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; background-color: '+backcolor+'" border="0" cellspacing="0" cellpadding="0"><tr><td nowrap><font style="font-family:宋体; font-size: 9pt; color: '+textcolor+'">'+unescape(tipContent)+'</font></td></tr></table> ' 
                if ((evt.clientX + locateObject(object).clientWidth) > (document.body.clientWidth + document.body.scrollLeft)) { 
                    locateObject(object).style.left = (document.body.clientWidth + document.body.scrollLeft) - locateObject(object).clientWidth-10; 
                } else { 
                    locateObject(object).style.left=document.body.scrollLeft+evt.clientX+5+"px"; 
                } 
                locateObject(object).style.visibility='visible'; 
                tipTimer=window.setTimeout("hideTooltip('"+object+"')",displaytime); 
                return true; 
        } 
    } 

    function getEvent() //同时兼容ie和ff的写法
    {  
        if(document.all)    return window.event;    
        func=getEvent.caller;        
        while(func!=null){  
            var arg0=func.arguments[0];
            if(arg0)
            {
                if((arg0.constructor==Event || arg0.constructor ==MouseEvent) || (typeof(arg0)=="object" && arg0.preventDefault && arg0.stopPropagation))
                {  
                    return arg0;
                    }
            }
            func=func.caller;
            }
         return null;
    }


    function hideTooltip(object) { 
        if (document.all) { 
            locateObject(object).style.visibility = 'hidden'; 
            locateObject(object).style.left = 1; 
            locateObject(object).style.top = 1; 
            return false; 
        } else { 
            if (document.layers) {
                locateObject(object).visibility = 'hide'; 
                locateObject(object).left = 1; 
                locateObject(object).top = 1; 
                return false; 
            } else {  
                locateObject(object).style.visibility = 'hidden'; 
                locateObject(object).style.left = 1; 
                locateObject(object).style.top = 1; 
                return false; 
            } 
        } 
    } 