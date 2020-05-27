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

function PopZoneAD(_id) {
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
  this.PopType     = 0;
  this.Left        = 0;
  this.Top         = 0;
  this.CookieHour  = 0;

  /* Define Objects */
  this.AllAD       = new Array();
  this.ShowAD      = null;

  /* Define Functions */
  this.AddAD       = PopZoneAD_AddAD;
  this.GetShowAD   = PopZoneAD_GetShowAD;
  this.CookieCheck = PopZoneAD_CookieCheck;
  this.Show        = PopZoneAD_Show;
  this.Struct      = PopZoneAD_Struct;
  this.Window      = PopZoneAD_Window;
  this.GetRight    = PopZoneAD_GetRight;
  this.GetTail     = PopZoneAD_GetTail;
}

function PopZoneAD_AddAD(_AD) {
  var date = new Date();
  var getdate = date.getFullYear() + "/" + (date.getMonth() + 1) + "/" + date.getDate();
  var today = new Date(getdate);
  var overdueDate = new Date(_AD.OverdueDate);
  if(today <= overdueDate) {
    this.AllAD[this.AllAD.length] = _AD;
  }
}

function PopZoneAD_GetShowAD() {
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

function PopZoneAD_Show() {
  if (!this.AllAD) {
    return;
  } else {
    this.GetShowAD();
  }

  if (this.ShowAD == null) return false;
  if (this.CookieCheck()) return false;
  this.DivName = "PopZoneAD_Div" + this.ZoneID;
  if (this.ShowAD.ADDIR=="") this.ShowAD.ADDIR = "AD"
  if (this.LocalityType==2) {
    this.Top = this.GetTail();
  } else if (this.LocalityType==3) {
    this.Left = this.GetRight();
  } else if (this.LocalityType==4) {
    this.Left = this.GetRight();
    this.Top = this.GetTail();
  }
  if (this.PopType == 3) {
    window.open("" + this.ShowAD.InstallDir + this.ShowAD.ADDIR + "/ADShow.aspx?ADID=" + this.ShowAD.ADID + "","","scroll:0;status:0;help:0;toolbar=0;resizable:0;dialogTop:" + this.Top + "px;dialogLeft:" + this.Left + "px;dialogWidth:" + this.ZoneWidth + "px;dialogHeight:" + this.ZoneHeight + "px");
  } else {
    var OK = this.Struct();
    if (OK) OK.document.write(this.Window());
  }
  if (this.ShowAD.CountView) {
    document.write ("<script src='" + this.ShowAD.InstallDir + this.ShowAD.ADDIR + "/ADCount.aspx?Action=View&ADID=" + this.ShowAD.ADID + "'></script>")
  }
}

function PopZoneAD_GetRight(){
    return screen.width - this.Left - this.ZoneWidth;
}

function PopZoneAD_GetTail(){
    return screen.Height - this.Top - this.ZoneHeight;
}

function PopZoneAD_Struct() {
  var winwidth = (this.PopType==4) ? (screen.width - 9): (parseInt((this.ShowAD.ImgWidth) ? this.ShowAD.ImgWidth : 350) - 4);
  var winheight = (this.PopType==4) ? (screen.height - 56): (parseInt((this.ShowAD.ImgHeight) ? this.ShowAD.ImgHeight : 250) - 4);
  var winleft = (this.PopType==4) ? 0: ((this.Left) ? this.Left : 0);
  var wintop = (this.PopType==4) ? 0: ((this.Top) ? this.Top : 0);
  return window.open("about:blank", "","toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,width=" + winwidth + ",height=" + winheight + ",top=" + wintop + ",left=" + winleft);
}

function PopZoneAD_Window() {
  var strWin;
  strWin = "<html><head><meta charset='urf-8'><title>";
  strWin += (this.PopType==4) ? "背投广告" : "POPAD";
  strWin += "</title>";
  if (this.PopType==2||this.PopType==4) {
    strWin += "\n<Script>\n";
    strWin += "blur();\n";
    strWin += "<\/Script>";
  }
  strWin += "</head><body scroll=no style='margin:0;border:none'>";
  strWin += AD_Content(this.ShowAD);
  strWin += "</body></html>";
  return strWin;
}

function PopZoneAD_CookieCheck() {
  if (!this.CookieHour) return false;
  var Now = new Date();
  var strToday = String(Now.getYear()) + String(Now.getMonth() + 1) + String(Now.getDate());
  var ADCookie = "AD_Cookie_" + this.ZoneID
  if (GetCookie(ADCookie) == strToday)
    return true;
  else {
    Now.setTime(Now.getTime() + (parseFloat(typeof(this.CookieHour) == "undefined" ? 24 : parseFloat(this.CookieHour)) * 60 * 60 * 1000));
    AD_SetCookie(ADCookie, strToday, Now);
    return false;
  }
}

function GetCookie(name) {
  var arg = name + "=";
  var alen = arg.length;
  var clen = document.cookie.length;
  var i = 0;
  while (i < clen) {
    var j = i + alen;
    if (document.cookie.substring(i, j) == arg)
      return GetCookieVal (j);
    i = document.cookie.indexOf(" ", i) + 1;
    if (i == 0) break; 
  }
  return null;
}

function GetCookieVal(offset) {
  var endstr = document.cookie.indexOf (";", offset);
  if (endstr == -1)
    endstr = document.cookie.length;
  return unescape(document.cookie.substring(offset, endstr));
}

function AD_SetCookie(name, value)
{
    var argv = AD_SetCookie.arguments;
    var argc = AD_SetCookie.arguments.length;
    var expires = (argc > 2) ? argv[2]: null;
    var path = (argc > 3) ? argv[3]: null;
    var domain = (argc > 4) ? argv[4]: null;
    var secure = (argc > 5) ? argv[5]: false;
    document.cookie = name + "=" + escape(value) + ((expires == null) ? "" : ("; expires=" + expires.toGMTString())) + ((path == null) ? "" : ("; path=" + path)) + ((domain == null) ? "" : ("; domain=" + domain)) + ((secure == true) ? "; secure" : "");
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