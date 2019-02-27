var XmlHttp = null;

function GetXmlHttpObj()
{
	var XmlHttpObj = null;
	try
	{ 
		if(window.ActiveXObject)
		{ 
			for(var i = 5;i;i--)
			{ 
				try
				{
					if( i == 2 )
					{ 
						XmlHttpObj = new ActiveXObject( "Microsoft.XMLHTTP" ); 
					}
					else
					{ 
						XmlHttpObj = new ActiveXObject( "Msxml2.XMLHTTP." + i + ".0" ); 
					} 
					XmlHttpObj.setRequestHeader("Content-Type","text/xml"); 
					XmlHttpObj.setRequestHeader("Content-Type","utf-8"); 
					break;
				}
				catch(e)
				{}
			}
		}
		else if( window.XMLHttpRequest )
		{ 
			XmlHttpObj = new XMLHttpRequest(); 
			if (XmlHttpObj.overrideMimeType) 
			{
				XmlHttpObj.overrideMimeType('text/xml');
			}
		}
	}
	catch(e)
	{}
	return XmlHttpObj
}


function XmlHttpGetMethod(uri)
{
	XmlHttp = GetXmlHttpObj();
	if(XmlHttp == null)
	{
		alert('很抱歉，你的浏览器版本不支持，操作终止');
		return;
	}
	XmlHttp.open("GET",uri,false);
	XmlHttp.send();
}


function XmlHttpPostMethod(uri,parmsStr)
{
	XmlHttp = GetXmlHttpObj();
	if(XmlHttp == null)
	{
		alert('很抱歉，你的浏览器版本不支持，操作终止');
		return;
	}
	XmlHttp.open("POST",uri,false);	
	XmlHttp.setRequestHeader("Content-type","application/x-www-form-urlencoded");
	XmlHttp.send(parmsStr);
}

function XmlHttpGetMethodText(uri)
{
	XmlHttpGetMethod(uri);
	return XmlHttp.responseText;
}

function XmlHttpPostMethodText(uri,parmsStr)
{
	XmlHttpPostMethod(uri,parmsStr);
	return XmlHttp.responseText;
}

function XmlHttpGetMethodXml(uri)
{
	XmlHttpGetMethod(uri);
	return XmlHttp.responseXML;
}

function XmlHttpPostMethodXml(uri,parmsStr)
{
	XmlHttpPostMethod(uri,parmsStr);
	return XmlHttp.responseXML;
}