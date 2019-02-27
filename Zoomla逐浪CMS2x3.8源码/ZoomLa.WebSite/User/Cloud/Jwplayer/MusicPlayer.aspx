<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MusicPlayer.aspx.cs" Inherits="User_Cloud_Jwplayer_MusicPlayer" EnableViewStateMac="false" %>
<!DOCTYPE html>
<html>
<head runat="server">
<title>音乐播放器</title>
<script type="text/javascript" src="ufo.js"></script>
<style>
body{
	margin:0 auto;
	padding:0;
	color:#fff;
	font:12px Arial;
	line-height:150%;
	width:100%;
	}
a{
	color:#fff;}
a:hover{
	color:#f60;}

.fr{
	float:right;}
	
</style>
</head>
<body>
<div id="player1" style=" width:300px; height:400px;" >
	<script type="text/javascript">
	    var FU = {
	        movie: "mediaplayer.swf",
	        width: "300",
	        height: "400",
	        majorversion: "7",
	        build: "5",
	        flashvars: "file=<%=current %>&displayheight=0&repeat=true&lightcolor=0xff6600&backcolor=0x000000&frontcolor=0xffffff&overstretch=true&autostart=true"
	    };
	    UFO.create(FU, "player1");        
	</script>
</div>
</body>
</html>
