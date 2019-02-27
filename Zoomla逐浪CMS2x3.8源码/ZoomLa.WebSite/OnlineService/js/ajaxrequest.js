/*------------------------------------------
Name: AJAXRequest
@License: http://www.gnu.org/licenses/gpl.html GPL
@Copyright: Copyright (c) 2007, All Rights Reserved
@Author: xujiwei
@E-mail: vipxjw@163.com
@Website: http://www.xujiwei.cn
@Version: 0.7

AJAXRequest Deveoper Manual:
  http://www.xujiwei.cn/works/ajaxrequest/
------------------------------------------*/
function AJAXRequest() {
	var xmlPool=[],AJAX=this,ac=arguments.length,av=arguments;
	var xmlVersion=["MSXML2.XMLHTTP","Microsoft.XMLHTTP"];
	var emptyFun=function(){};
	var av=ac>0?typeof(av[0])=="object"?av[0]:{}:{};
	var encode=av.charset?av.charset.toUpperCase()=="UTF-8"?encodeURIComponent:escape:encodeURIComponent;
	this.url=getp(av.url,"");
	this.oncomplete=getp(av.oncomplete,emptyFun);
	this.content=getp(av.content,"");
	this.method=getp(av.method,"POST");
	this.async=getp(av.async,true);
	this.onexception=getp(av.onexception,emptyFun);
	this.ontimeout=getp(av.ontimeout,emptyFun);
	this.timeout=getp(av.timeout,3600000);
	this.onrequeststart=getp(av.onstartrequest,emptyFun);
	this.onrequestend=getp(av.onendrequest,emptyFun);
	if(!getObj()) return false;
	function getp(p,d) { return p?p:d; }
	function getObj() {
		var i,j,tmpObj;
		for(i=0,j=xmlPool.length;i<j;i++) if(xmlPool[i].readyState==0||xmlPool[i].readyState==4) return xmlPool[i];
		try { tmpObj=new XMLHttpRequest; }
		catch(e) {
			for(i=0,j=xmlVersion.length;i<j;i++) {
				try { tmpObj=new ActiveXObject(xmlVersion[i]); } catch(e2) { continue; }
				break;
			}
		}
		if(!tmpObj) return false;
		else { xmlPool[xmlPool.length]=tmpObj; return xmlPool[xmlPool.length-1]; }
	}
	function $(id){return document.getElementById(id);}
	function $N(n,d){n=parseInt(n);return(isNaN(n)?d:n);}
	function $VO(v) {
		if(typeof(v)=="string") {
			if(v=$(v)) return v;
			else return false; }
		else return v;
	}
	function $ST(obj,text) {
		var nn=obj.nodeName.toUpperCase();
		if("INPUT|TEXTAREA".indexOf(nn)>-1) obj.value=text;
		else try{obj.innerHTML=text;} catch(e){};
	}
	function $CB(cb) {
		if(typeof(cb)=="function") return cb;
		else {
			cb=$VO(cb);
			if(cb) return(function(obj){$ST(cb,obj.responseText);});
			else return emptyFun; }
	}
	function send(purl,pc,pcbf,pm,pa) {
		var purl,pc,pcbf,pm,pa,ct,ctf=false,xmlObj=getObj(),ac=arguments.length,av=arguments;
		if(!xmlObj) return false;
		if(!pm||!purl||!pa) return false;
		var ev={url:purl, content:pc, method:pm};
		purl+=(purl.indexOf("?")>-1?"&":"?")+Math.random();
		xmlObj.open(pm,purl,pa);
		AJAX.onrequeststart(ev);
		if(pm=="POST") xmlObj.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
		ct=setTimeout(function(){ctf=true;xmlObj.abort();},AJAX.timeout);
		xmlObj.onreadystatechange=function() {
			if(ctf) { AJAX.ontimeout(ev); AJAX.onrequestend(ev); }
			else if(xmlObj.readyState==4) {
				ev.status=xmlObj.status;
				try{ clearTimeout(ct); } catch(e) {};
				try{ if(xmlObj.status==200) pcbf(xmlObj); else AJAX.onexception(ev); }
				catch(e) { AJAX.onexception(ev); }
				AJAX.onrequestend(ev);
			}
		}
		if(pm=="POST") xmlObj.send(pc); else xmlObj.send("");
	}
	this.setcharset=function(cs) {
		if(cs.toUpperCase()=="UTF-8") encode=encodeURIComponent; else encode=escape;
	}
	this.get=function() {
		var purl,pcbf,ac=arguments.length,av=arguments;
		purl=ac>0?av[0]:this.url;
		pcbf=ac>1?$CB(av[1]):this.oncomplete;
		if(!purl&&!pcbf) return false;
		send(purl,"",pcbf,"GET",true);
	}
	this.update=function() {
		var purl,puo,pinv,pcnt,ac=arguments.length,av=arguments;
		puo=ac>0?$CB(av[0]):emptyFun;
		purl=ac>1?av[1]:this.url;
		pinv=ac>2?$N(av[2],1000):null;
		pcnt=ac>3?$N(av[3],0):null;
		if(pinv) {
			send(purl,"",puo,"GET",true);
			if(pcnt&&--pcnt) {
				var cf=function(cc) {
					send(purl,"",puo,"GET",true);
					if(cc<1) return; else cc--;
					setTimeout(function(){cf(cc);},pinv);
				}
				setTimeout(function(){cf(--pcnt);},pinv);
			}
			else return(setInterval(function(){send(purl,"",puo,"GET",true);},pinv));
		}
		else send(purl,"",puo,"GET",true);
	}
	this.post=function() {
		var purl,pcbf,pc,ac=arguments.length,av=arguments;
		purl=ac>0?av[0]:this.url;
		pc=ac>1?av[1]:"";
		pcbf=ac>2?$CB(av[2]):this.oncomplete;
		if(!purl&&!pcbf) return false;
		send(purl,pc,pcbf,"POST",true);
	}
	this.postf=function() {
		var fo,vaf,pcbf,purl,pc,pm,ac=arguments.length,av=arguments;
		fo=ac>0?$VO(av[0]):false;
		if(!fo||(fo&&fo.nodeName!="FORM")) return false;
		vaf=fo.getAttribute("onsubmit");
		vaf=vaf?(typeof(vaf)=="string"?new Function(vaf):vaf):null;
		if(vaf&&!vaf()) return false;
		pcbf=ac>1?$CB(av[1]):this.oncomplete;
		purl=ac>2?av[2]:(fo.action?fo.action:this.url);
		pm=ac>3?av[3]:(fo.method?fo.method.toUpperCase():"POST");
		if(!pcbf&&!purl) return false;
		pc=this.formToStr(fo);
		if(!pc) return false;
		if(pm) {
			if(pm=="POST") send(purl,pc,pcbf,"POST",true);
			else if(purl.indexOf("?")>0) send(purl+"&"+pc,"",pcbf,"GET",true);
				else send(purl+"?"+pc,"",pcbf,"GET",true);
		}
		else send(purl,pc,pcbf,"POST",true);
	}
	/* formToStr
	// from SurfChen <surfchen@gmail.com>
	// @url     http://www.surfchen.org/
	// @license http://www.gnu.org/licenses/gpl.html GPL
	// modified by xujiwei
	// @url     http://www.xujiwei.cn/
	*/
	this.formToStr=function(fc) {
		var i,qs="",and="",ev="";
		for(i=0;i<fc.length;i++) {
			e=fc[i];
			if (e.name!='') {
				if (e.type=='select-one'&&e.selectedIndex>-1) ev=e.options[e.selectedIndex].value;
				else if (e.type=='checkbox' || e.type=='radio') {
					if (e.checked==false) continue;
					ev=e.value;
				}
				else ev=e.value;
				ev=encode(ev);
				qs+=and+e.name+'='+ev;
				and="&";
			}
		}
		return qs;
	}
}