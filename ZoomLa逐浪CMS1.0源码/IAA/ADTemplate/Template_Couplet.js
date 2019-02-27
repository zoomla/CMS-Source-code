function ObjectAD() {
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

function CoupletZoneAD(_id) {
  /* Define Common Variables*/
  this.ID          = _id;
  this.ZoneID      = 0;
  this.ZoneName    = "";
  this.ZoneWidth   = 0;
  this.ZoneHeight  = 0;
  this.ShowType    = 1;
  this.DivNameLeft = "";
  this.DivLeft     = null;
  this.DivNameRight= "";
  this.DivRight    = null;

  /* Define Unique Variables*/
  this.Left        = 0;
  this.Top         = 0;
  this.Delta       = 0.15;
  this.ShowCloseAD = false;
  this.CloseFontColor = "#FFFFFF";

  /* Define Objects */
  this.AllAD       = new Array();
  this.ShowLeftAD  = null;
  this.ShowRightAD = null;

  /* Define Functions */
  this.AddAD         = CoupletZoneAD_AddAD;
  this.GetShowAD     = CoupletZoneAD_GetShowAD;
  this.Show          = CoupletZoneAD_Show;
  this.Move          = CoupletZoneAD_Move;
  this.GetRight      = CoupletZoneAD_GetRight;
  this.GetRandomNum  = CoupletZoneAD_GetRandomNum;
  this.WriteAD       = CoupletZoneAD_WriteAD;
  this.GetMove       = CoupletZoneAD_GetMove;
}

function CoupletZoneAD_AddAD(_AD) {
  var date = new Date();
  var getdate = date.getFullYear() + "/" + (date.getMonth() + 1) + "/" + date.getDate();
  var today = new Date(getdate);
  var overdueDate = new Date(_AD.OverdueDate);
  if(today <= overdueDate)
  {
    this.AllAD[this.AllAD.length] = _AD;
  }
}

function CoupletZoneAD_GetShowAD() {
  if (this.ShowType >1) {
    if(this.AllAD.length <= 1){
      this.ShowLeftAD = this.AllAD[0];
    }
    else{
      this.ShowLeftAD = this.AllAD[0];
      this.ShowRightAD = this.AllAD[1];
    }
    return;
  }
  if(this.AllAD.length <= 1){
    this.ShowLeftAD = this.AllAD[this.GetRandomNum()];
  }
  else{
    this.ShowLeftAD = this.AllAD[this.GetRandomNum()];
    this.ShowRightAD = this.AllAD[this.GetRandomNum()];
  }
}

function CoupletZoneAD_GetRandomNum(){
  var sum = 0;
  for (var i = 0; i < this.AllAD.length; i++) {
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
  return i;
}

function CoupletZoneAD_Show() {
  if (!this.AllAD) {
    return;
  } else {
    this.GetShowAD();
  }
  if (this.ShowLeftAD == null){
    return false;
  }
  else{
    this.DivNameLeft = "MoveZoneAD_Div" + this.ZoneID+"_left";
    this.DivNameRight = "MoveZoneAD_Div" + this.ZoneID+"_right";
    this.WriteAD(this.ShowLeftAD,this.DivNameLeft,this.Left);
    this.DivLeft = document.getElementById(this.DivNameLeft);
    if(this.AllAD.length <= 1){
      this.WriteAD(this.ShowLeftAD,this.DivNameRight,this.GetRight());
      this.DivRight = document.getElementById(this.DivNameRight);
    }
    else{
      this.WriteAD(this.ShowRightAD,this.DivNameRight,this.GetRight());
      this.DivRight = document.getElementById(this.DivNameRight);
    }
  }
  setInterval(this.ID + ".Move()", 10);
}

function CoupletZoneAD_WriteAD(ShowAD,DivName,Left){
  if (!ShowAD.ImgWidth) ShowAD.ImgWidth = this.ZoneWidth
  if (!ShowAD.ImgHeight) ShowAD.ImgHeight = this.ZoneHeight
  if (ShowAD.ADDIR=="") ShowAD.ADDIR = "AD"
  document.write("<div id='" + DivName + "' style='position:absolute; z-index:1; width:" + this.ZoneWidth + "px;height:" + this.ZoneHeight + "px;left:" + Left + "px;top:" + this.Top + "px'>" + AD_Content(ShowAD) + "");
  if(this.ShowCloseAD){
    document.write("<div style='position: absolute;right: 0px;bottom: 0px;'><a href='#' onclick='AD_CloseDL(\""+DivName+"\");' style='font-size:12px;color:"+this.CloseFontColor+";text-decoration:none;'>关闭</a></div>");
  }
  document.write("</div>");
  if (ShowAD.CountView) {
    document.write ("<script src='" + ShowAD.InstallDir + ShowAD.ADDIR + "/ADCount.aspx?Action=View&ADID=" + ShowAD.ADID + "'></script>")
  }
}

function CoupletZoneAD_GetRight(){
  if (window.innerWidth) {
    return window.innerWidth-16 - this.Left - this.ZoneWidth;
  }else{
    return document.documentElement.clientWidth - this.Left - this.ZoneWidth;
  }
}

function CoupletZoneAD_Move() {
  if(document.all)  {
    pageX=window.document.documentElement.scrollLeft;
    pageY=window.document.documentElement.scrollTop;
  }
  else {
    pageX=window.pageXOffset;
    pageY=window.pageYOffset;
  }
  if(this.DivLeft != null){
    this.GetMove(pageX,pageY,this.DivLeft,this.Left);
  }
  if(this.DivRight != null){
    this.GetMove(pageX,pageY,this.DivRight,this.GetRight());
  }
}

function CoupletZoneAD_GetMove(pageX,pageY,Div,Left){
  if (Div.offsetLeft != (pageX + Left)) {
    var dx = (pageX + Left - Div.offsetLeft) * this.Delta;
    dx = (dx > 0 ? 1 :  - 1) * Math.ceil(Math.abs(dx));
    Div.style.left = Div.offsetLeft + dx + "px";
  }
  if (Div.offsetTop != (pageY + this.Top)) {
    var dy = (pageY + this.Top - Div.offsetTop) * this.Delta;
    dy = (dy > 0 ? 1 :  - 1) * Math.ceil(Math.abs(dy));
    Div.style.top = Div.offsetTop + dy + "px";
  }
  Div.style.display = '';
}

function AD_CloseDL(d){
  document.getElementById(d).style.visibility = "hidden";
}

function AD_Content(o) {
  var str = "";
  if (o.ADType == 1 || o.ADType == 2) {
  imgurl = o.ImgUrl .toLowerCase()
    if (o.InstallDir.indexOf("http://") != - 1) imgurl = o.InstallDir.substr(0, o.InstallDir.length - 1) + imgurl;
    if (imgurl.indexOf(".swf") !=  - 1) {
      str = "<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0'";
      str += " name='AD_" + o.ADID + "' id='AD_" + o.ADID + "'";
      str += " width='" + o.ImgWidth + "px'";
      str += " height='" + o.ImgHeight + "px'";
      if (o.style) str += " style='" + o.style + "'";
      if (o.extfunc) str += " " + o.extfunc + " ";
      str += ">";
      str += "<param name='movie' value='" + imgurl + "'>";
      if (o.FlashWmode == 1) str += "<param name='wmode' value='Transparent'>";
      if (o.play) str += "<param name='play' value='" + o.play + "'>";
      if (typeof(o.loop) != "undefined") str += "<param name='loop' value='" + o.loop + "'>";
      str += "<param name='quality' value='autohigh'>";
      str += "<embed ";
      str += " name='AD_" + o.ADID + "' id='AD_" + o.ADID + "'";
      str += " width='" + o.ImgWidth + "px'";
      str += " height='" + o.ImgHeight + "px'";
      if (o.style) str += " style='" + o.style + "'";
      if (o.extfunc) str += " " + o.extfunc + " ";
      str += " src='" + imgurl + "'";
      if (o.FlashWmode == 1) str += " wmode='Transparent'";
      if (o.play) str += " play='" + o.play + "'";
      if (typeof(o.loop) != "undefined") str += " loop='" + o.loop + "'";
      str += " quality='autohigh'"
      str += " pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash'></embed>";
      str += "</object>";
    } else if (imgurl.indexOf(".gif") !=  - 1 || imgurl.indexOf(".jpg") !=  - 1 || imgurl.indexOf(".jpeg") !=  - 1 || imgurl.indexOf(".bmp") !=  - 1 || imgurl.indexOf(".png") !=  - 1) {
      if (o.LinkUrl) {
        if (o.CountClick) o.LinkUrl = o.InstallDir + o.ADDIR + "/ADCount.aspx?Action=Click&ADID=" + o.ADID
        str += "<a href='" + o.LinkUrl + "' target='" + ((o.LinkTarget == 0) ? "_self" : "_blank") + "' title='" + o.LinkAlt + "'>";
      }
      str += "<img ";
      str += " name='AD_" + o.ADID + "' id='AD_" + o.ADID + "'";
      if (o.style) str += " style='" + o.style + "'";
      if (o.extfunc) str += " " + o.extfunc + " ";
      str += " src='" + imgurl + "'";
      if (o.ImgWidth) str += " width='" + o.ImgWidth + "px'";
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