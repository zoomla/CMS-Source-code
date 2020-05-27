﻿function ObjectAD() {
  /* Define Variables*/
  this.ADID        = 0;
  this.ADType      = 0;
  this.ADName      = "";
  this.ImgUrl      = "";
  this.ImgWidth    = 0;
  this.ImgHeight   = 0;
  this.FlashWmode  = 0;
  this.LinkUrl     = "";
  this.LinkTarget  = 0;
  this.LinkAlt     = "";
  this.Priority    = 0;
  this.CountView   = 0;
  this.CountClick  = 0;
  this.InstallDir  = "";
  this.ADDIR       = "";
  this.OverdueDate = "";
}

function FloatZoneAD(_id) {
  /* Define Constants */
  this.vmin        = 2;
  this.vmax        = 5;
  this.vr          = 2;

  /* Define Common Variables*/
  this.ID          = _id;
  this.ZoneID      = 0;
  this.ZoneName    = "";
  this.ZoneWidth   = 0;
  this.ZoneHeight  = 0;
  this.ShowType    = 1;
  this.DivName     = "";
  this.Div         = null;

  /* Define Unique Variables*/
  this.LocalityType= 1;
  this.FloatType   = 1;
  this.Left        = 0;
  this.Top         = 0;
  this.Delay       = 50;
  this.ShowCloseAD = true;
  this.CloseFontColor = "#FFFFFF";

  this.Width       = 1;
  this.Height      = 1;
  this.vx          = this.vmin+this.vmax*Math.random();
  this.vy          = this.vmin+this.vmax*Math.random();
  this.timer       = null;

  this.step        = 1;
  this.xin         = true;
  this.yin         = true;

  /* Define Objects */
  this.AllAD       = new Array();
  this.ShowAD      = null;

  /* Define Functions */
  this.AddAD       = FloatZoneAD_AddAD;
  this.GetShowAD   = FloatZoneAD_GetShowAD;
  this.Show        = FloatZoneAD_Show;
  this.Float       = FloatZoneAD_Float;
  this.Pause       = FloatZoneAD_Pause;
  this.Resume      = FloatZoneAD_Resume;
}

function FloatZoneAD_AddAD(_AD) {
  this.AllAD[this.AllAD.length] = _AD;
}

function FloatZoneAD_GetShowAD() {
  if (this.ShowType > 1) {
    this.ShowAD = this.AllAD[0];
    return;
  }
  var num = this.AllAD.length;
  var sum = 0;
  for (var i = 0; i < num; i++) {
    sum = sum + this.AllAD[i].Priority;
  }
  if (sum <= 0) {return ;}
  var rndNum = Math.random() * sum;
  i = 0;
  j = 0;
  while (true) {
    j = j + this.AllAD[i].Priority;
    if (j >= rndNum) {break;}
    i++;
  }
  this.ShowAD = this.AllAD[i];
}

function FloatZoneAD_Show() {
  if (!this.AllAD) {
    return;
  } else {
    this.GetShowAD();
  }

  if (this.ShowAD == null) return false;
  this.DivName = "FloatZoneAD_Div" + this.ZoneID;
  if (!this.ShowAD.ImgWidth) this.ShowAD.ImgWidth = this.ZoneWidth
  if (!this.ShowAD.ImgHeight) this.ShowAD.ImgHeight = this.ZoneHeight
  if (this.ShowAD.ADDIR=="") this.ShowAD.ADDIR = "AD"
  if (navigator.appName == "Netscape") {
      document.write("<layer id='" + this.DivName + "' style='position:absolute;' top='" + this.Top + "' max-width='" + this.ZoneWidth + "' height='" + this.ZoneHeight + "'>" + AD_Content(this.ShowAD) + "</layer>");
  } else {
    document.write("<div id='" + this.DivName + "' onMouseOver='" + this.ID+".Pause()' onMouseOut='" + this.ID+".Resume()' style='position:absolute; visibility:visible; z-index:1; max-width:" + this.ZoneWidth + "; height:" + this.ZoneHeight + "; left:" + this.Left + ";top:" + this.Top + "'>" + AD_Content(this.ShowAD) + "");
  }


  if(this.ShowCloseAD){
    document.write("<div style='position: absolute;right: 0px;bottom: 0px;'><a href='#' onclick='AD_CloseDL(\""+this.DivName+"\");' style='font-size:12px;color:"+this.CloseFontColor+";text-decoration:none;'>关闭</a></div>");
  }
  document.write("</div>");

  if (this.ShowAD.CountView) {
    document.write ("<script src='" + this.ShowAD.InstallDir + this.ShowAD.ADDIR + "/ADCount.aspx?Action=View&ADID=" + this.ShowAD.ADID + "'></script>")
  }
  this.Width = (this.ShowAD.ImgWidth)?this.ShowAD.ImgWidth:this.ZoneWidth
  this.Height = (this.ShowAD.ImgHeight)?this.ShowAD.ImgHeight:this.ZoneHeight
  this.Div = document.getElementById(this.DivName);
  this.Float();
}

function FloatZoneAD_Float() {
  pageX=window.document.body.scrollLeft;
  pageW=window.document.body.offsetWidth-22;
  pageY = window.document.body.scrollTop || document.body.scrollTop;
  pageH=window.document.body.offsetHeight-4;
  if (this.FloatType=="1") {
    this.Left=Number(this.Left)+this.vx;
    this.Top=Number(this.Top)+this.vy;
    this.vx+=this.vr*(Math.random()-0.5);
    this.vy+=this.vr*(Math.random()-0.5);
    if(this.vx>(this.vmax+this.vmin))this.vx=(this.vmax+this.vmin)*2-this.vx;
    if(this.vx<(-this.vmax-this.vmin))this.vx=(-this.vmax-this.vmin)*2-this.vx;
    if(this.vy>(this.vmax+this.vmin))this.vy=(this.vmax+this.vmin)*2-this.vy;
    if(this.vy<(-this.vmax-this.vmin))this.vy=(-this.vmax-this.vmin)*2-this.vy;
    if(this.Left<=pageX){this.Left=pageX;this.vx=this.vmin+this.vmax*Math.random();}
    if(this.Left>=pageX+pageW-this.Width){this.Left=pageX+pageW-this.Width;this.vx=-this.vmin-this.vmax*Math.random();}
    if(this.Top<=pageY){this.Top=pageY;this.vy=this.vmin+this.vmax*Math.random();}
    if(this.Top>=pageY+pageH-this.Height){this.Top=pageY+pageH-this.Height;this.vy=-this.vmin-this.vmax*Math.random();}
    this.Delay=80;
  } else if (this.FloatType=="2") {
    this.Left+=this.step*(this.xin?1:-1);
    this.Top+=this.step*(this.yin?1:-1);
    if(this.Left<=pageX){this.xin=true;this.Left<=pageX} 
    if(this.Left>=pageX+pageW-this.Width){this.xin=false;this.Left=pageX+pageW-this.Width} 
    if(this.Top<=pageY){this.yin=true;this.Top=pageY} 
    if(this.Top>=pageY+pageH-this.Height){this.yin=false;this.Top=pageY+pageH-this.Height}
    this.Delay=15;
  } else if (this.FloatType=="3") {
    this.Top+=this.step*(this.yin?1:-1);
    if(this.Top<=pageY){this.yin=true;this.Top=pageY} 
    if(this.Top>=pageY+pageH-this.Height){this.yin=false;this.Top=pageY+pageH-this.Height}
    this.Delay=15;
  } else if (this.FloatType=="4") {
    this.Left+=this.step*(this.xin?1:-1);
    if(this.Left<=pageX){this.xin=true;this.Left<=pageX} 
    if(this.Left>=pageX+pageW-this.Width){this.xin=false;this.Left=pageX+pageW-this.Width} 
    this.Delay=15;
  }
  this.Div.style.left = this.Left + "px";
  this.Div.style.top = this.Top + "px";
  this.Div.timer=setTimeout(this.ID+".Float()",this.Delay);
}

function FloatZoneAD_Pause() {
  if(this.Div.timer!=null){clearTimeout(this.Div.timer)}
}

function FloatZoneAD_Resume() {
  this.Float();
}

function AD_CloseDL(d){
  document.getElementById(d).style.visibility = "hidden";
}
function AD_Content(o) {
  var str = "";
  if (o.ADType == 1 || o.ADType == 2) {
    o.ImgUrl = o.ImgUrl.toLowerCase()
    if (o.ImgUrl.indexOf(".swf") !=  - 1) {
      str = "<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0'";
      str += " name='AD_" + o.ADID + "' id='AD_" + o.ADID + "'";
      str += " max-width='" + o.ImgWidth + "px'";
      str += " height='" + o.ImgHeight + "px'";
      if (o.style) str += " style='" + o.style + "'";
      if (o.extfunc) str += " " + o.extfunc + " ";
      str += ">";
      str += "<param name='movie' value='" + o.ImgUrl + "'>";
      if (o.FlashWmode == 1) str += "<param name='wmode' value='Transparent'>";
      if (o.play) str += "<param name='play' value='" + o.play + "'>";
      if (typeof(o.loop) != "undefined") str += "<param name='loop' value='" + o.loop + "'>";
      str += "<param name='quality' value='autohigh'>";
      str += "<embed ";
      str += " name='AD_" + o.ADID + "' id='AD_" + o.ADID + "'";
      str += " max-width='" + o.ImgWidth + "px'";
      str += " height='" + o.ImgHeight + "px'";
      if (o.style) str += " style='" + o.style + "'";
      if (o.extfunc) str += " " + o.extfunc + " ";
      str += " src='" + o.ImgUrl + "'";
      if (o.FlashWmode == 1) str += " wmode='Transparent'";
      if (o.play) str += " play='" + o.play + "'";
      if (typeof(o.loop) != "undefined") str += " loop='" + o.loop + "'";
      str += " quality='autohigh'"
      str += " pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash'></embed>";
      str += "</object>";
    } else if (o.ImgUrl.indexOf(".gif") !=  - 1 || o.ImgUrl.indexOf(".jpg") !=  - 1 || o.ImgUrl.indexOf(".jpeg") !=  - 1 || o.ImgUrl.indexOf(".bmp") !=  - 1 || o.ImgUrl.indexOf(".png") !=  - 1) {
      if (o.LinkUrl) {
        if (o.CountClick) o.LinkUrl = o.InstallDir + o.ADDIR + "/ADCount.asp?Action=Click&ADID=" + o.ADID
        str += "<a href='" + o.LinkUrl + "' target='" + ((o.LinkTarget == 0) ? "_self" : "_blank") + "' title='" + o.LinkAlt + "'>";
      }
      str += "<img ";
      str += " name='AD_" + o.ADID + "' id='AD_" + o.ADID + "'";
      if (o.style) str += " style='" + o.style + "'";
      if (o.extfunc) str += " " + o.extfunc + " ";
      str += " src='" + o.ImgUrl + "'";
      if (o.ImgWidth) str += " max-width='" + o.ImgWidth + "px'";
      if (o.ImgHeight) str += " height='" + o.ImgHeight + "px'";
      str += " border='0'>";
      if (o.LinkUrl) str += "</a>";
    }
  } else if (o.ADType == 3 || o.ADType == 4) {
    str = o.ADIntro
  } else if (o.ADType == 5) {
    str = "<iframe id='" + "AD_" + o.ADID + "' marginwidth=0 marginheight=0 hspace=0 vspace=0 frameborder=0 scrolling=no width=100% height=100% src='" + o.ADIntro + "'>wait</iframe>";
  }
  return str;
}
