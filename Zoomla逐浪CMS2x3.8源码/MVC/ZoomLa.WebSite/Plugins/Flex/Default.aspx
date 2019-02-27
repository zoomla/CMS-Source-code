<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Plugins.Flex.Default" EnableViewState="false" %>
<!DOCTYPE html>
<html >
<head runat="server">
<title>在线设计</title>
<style type="text/css" media="screen"> 
html, body	{ height:100%; }
body { margin:0; padding:0; overflow:auto; text-align:center; }   
#flashContent { display:none; }
</style>
<link rel="stylesheet" type="text/css" href="history/history.css" />
<script type="text/javascript" src="history/history.js"></script>
<script type="text/javascript" src="swfobject.js"></script>
<script type="text/javascript">
        //For version detection, set to min. required Flash Player version, or 0 (or 0.0.0), for no version detection.  
        var swfVersionStr = "10.0.0";
        // To use express install, set to playerProductInstall.swf, otherwise the empty string.
        var xiSwfUrlStr = "playerProductInstall.swf";
        var flashvars = {};
        var params = {};
        params.quality = "high";
        params.bgcolor = "#201e1e";
        params.allowscriptaccess = "sameDomain";
        params.allowfullscreen = "true";
        var attributes = {};
        attributes.id = "ZoomAdvTest2";
        attributes.name = "ZoomAdvTest2";
        attributes.align = "middle";
        swfobject.embedSWF(
            "ZoomAdvTest2.swf", "flashContent", 
            "99%", "100%", 
            swfVersionStr, xiSwfUrlStr, 
            flashvars, params, attributes);
		//JavaScript enabled so display the flashContent div in case it is not replaced with a swf object. -->
		swfobject.createCSS("#flashContent", "display:block;text-align:left;");
			
//		window.moveTo(0,0);        
//		window.resizeTo(screen.availWidth,screen.availHeight);
//		window.outerWidth=screen.availWidth;        
//		window.outerHeight=screen.availHeight; 
//        //setInterval(document.title="在线设计",1000);
    </script>
</head>
<body>
     <div id="flashContent">
        	<p>
	        	To view this page ensure that Adobe Flash Player version 
				10.0.0 or greater is installed. 
			</p>
			<a href="http://www.adobe.com/go/getflashplayer">
				<img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash Player" />
			</a>
        </div>
	   	
       	<noscript>
            <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="99%" height="100%" id="ZoomAdvTest">
                <param name="movie" value="ZoomAdvTest.swf" />
                <param name="quality" value="high" />
                <param name="bgcolor" value="#201e1e" />
                <param name="allowScriptAccess" value="sameDomain" />
                <param name="allowFullScreen" value="true" />
                <!--[if !IE]>
                <object type="application/x-shockwave-flash" data="ZoomAdvTest.swf" width="99%" height="100%">
                    <param name="quality" value="high" />
                    <param name="bgcolor" value="#201e1e" />
                    <param name="allowScriptAccess" value="sameDomain" />
                    <param name="allowFullScreen" value="true" />
                <![endif]-->
                <!--[if gte IE 6]>
                	<p> 
                		Either scripts and active content are not permitted to run or Adobe Flash Player version
                		10.0.0 or greater is not installed.
                	</p>
                <![endif]-->
                    <a href="http://www.adobe.com/go/getflashplayer">
                        <img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash Player" />
                    </a>
                <!--[if !IE]>
                </object>
                <![endif]-->
            </object>
	    </noscript>		
		<%--<div  style="width:100%; height:60px; display:block;"></div>--%>
</body>
</html>
