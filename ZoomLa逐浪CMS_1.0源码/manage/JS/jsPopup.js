var jsAreaShowTime = 1000;
var JsAreas = new Object();

function GetArea(id)
{
	return JsAreas[id] ? JsAreas[id] : (JsAreas[id] = new JsArea(id));
}

// A single popup window
function JsArea(id)
{
	this.AreaId = id;
}

// Function to return the DIV Layer
JsArea.prototype.ContentArea= function()
{
	var divArea = document.getElementById(this.AreaId);
	if (divArea != null)
	{
		return divArea;
	}
	return null;
}

var activeAreaId = null;

// Function to show the DIV Layer
JsArea.prototype.popareaup = function(x, y)
{
if (activeAreaId != null)	
	jsAreaClose(activeAreaId);
	
	var divLayer = this.ContentArea();
	divLayer.style.position = 'absolute';
	divLayer.style.display = 'block';
	divLayer.style.left = x;
	divLayer.style.top = y;
	divLayer.onmouseover= JsAreaMouseOver;
	divLayer.onmouseout = jsAreaMouseOut;
	activeAreaId = this.AreaId;
	
	return false;
}

// Function to hide the DIV Layer
JsArea.prototype.hide = function()
{
	var divLayer = this.ContentArea();
	if (divLayer != null)
		divLayer.style.display = 'none';
		
	return false;
}

// Function to be called
// by Web forms to show the Popup Window
function PopupArea(e, areaId)
{
    if (self.pageYOffset || self.pageXOffset) {
        posy = self.pageYOffset;
        posx = self.pageXOffset;
    } else if (document.documentElement && document.documentElement.scrollTop ||         document.documentElement.scrollLeft ){ 
        posy = document.documentElement.scrollTop;
        posx = document.documentElement.scrollLeft;
    } else if (document.body) {
        posy = document.body.scrollTop;
        posx = document.body.scrollLeft;
    }

    posx = e.clientX + posx;
    posy = e.clientY + document.body.scrollTop + posy;
	        
	var area = GetArea(areaId);
	area.popareaup(posx, posy);
}

// Function to hide the DIV Layer
function jsAreaClose(areaId)
{
	GetArea(areaId).hide();
	activeAreaId = divHangTimer = null;	
}

var divHangTimer = null;

// Function to keep the Div Layer
// showing for a "period" of time
// after that period, if the mouse
// has been outside the DIV Layer, 
// it will be hidden automatically
function KeepArea(areaId)
{
	if (areaId == activeAreaId && divHangTimer != null)
	{
		clearTimeout(divHangTimer);
		divHangTimer = null;
	}
}

// Function to release the DIV Layer
function RelArea(areaId)
{
	if (areaId == activeAreaId && divHangTimer == null)
		divHangTimer = setTimeout('jsAreaClose(\'' + areaId + '\')', jsAreaShowTime);
}

// Function fired when mouse is over the 
// DIV Layer, used to keep the layer showing
function JsAreaMouseOver(e)
{
	if (!e) 
		var e = window.event;
	var targ = e.target ? e.target : e.srcElement;
	KeepArea(activeAreaId);
}

// Function that fires when mouse is out of
// the scope of the DIV Layer
function jsAreaMouseOut(e)
{
	if (!e) 
		var e = window.event;
	var targ = e.relatedTarget ? e.relatedTarget : e.toElement;
	var activeAreaView = document.getElementById(activeAreaId);
	if (activeAreaView != null && !jsAreaContains(activeAreaView, targ))
		RelArea(activeAreaId);
}
function jsAreaContains(parent, child)
{
	while(child)
		if (parent == child) return true;
		else 
			child = child.parentNode;
		
		return false;
}