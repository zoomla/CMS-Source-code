//图片滚动列表 mengjia 080322
var Speed = 5; //速度(毫秒)
var Space = 15; //每次移动(px)
var PageWidth = 450; //翻页宽度
var fill = 0; //整体移位
var MoveLock = false;
var MoveWay = "right";
var MoveTimeObj;
var Comp = 0;
var AutoPlayObj=null;
var ISL_Pages = Math.floor(GetObj("List1").scrollWidth / PageWidth);
var ISL_Page = 1;
var GotoLock = false;
GetObj("List2").innerHTML=GetObj("List1").innerHTML;
GetObj('ISL_Cont').scrollLeft=fill>=0?fill:GetObj('List1').scrollWidth-Math.abs(fill);
GetObj("ISL_Cont").onmouseover=function(){clearInterval(AutoPlayObj)}
GetObj("ISL_Cont").onmouseout=function(){AutoPlay()}
GetObj("arrLeft").onmousedown=function(){ISL_GoUp()}
GetObj("arrLeft").onmouseup=function(){ISL_StopUp()}
GetObj("arrLeft").onmouseout=function(){ISL_StopUp()}
GetObj("arrLeft").ondragstart=function(){return false}
GetObj("arrRight").onmousedown=function(){ISL_GoDown()}
GetObj("arrRight").onmouseup=function(){ISL_StopDown()}
GetObj("arrRight").onmouseout=function(){ISL_StopDown()}
GetObj("arrRight").ondragstart=function(){return false}
AutoPlay();
function GetObj(objName){if(document.getElementById){return eval('document.getElementById("'+objName+'")')}else{return eval('document.all.'+objName)}};
function AutoPlay(){
  clearInterval(AutoPlayObj);
  AutoPlayObj=setInterval('ISL_GoDown();ISL_StopDown();',5000)
};
	function ISL_GotoPage(i){
	if(MoveLock)return;
	MoveLock=true;
	GotoLock=true;
	Comp=(i-1)*PageWidth-GetObj('ISL_Cont').scrollLeft;
	ISL_Page=i;
	clearInterval(AutoPlayObj);
	CompScr();
	AutoPlay()
 };
 function ISL_GoUp(){
   if(MoveLock)return;
   clearInterval(AutoPlayObj);
   MoveLock=true;
   MoveWay="left";
   MoveTimeObj=setInterval('ISL_ScrUp();',Speed)};
   function ISL_StopUp(){
     if(GotoLock){return};
	 if(MoveWay=="right"){return};
	 clearInterval(MoveTimeObj);
	 if((GetObj('ISL_Cont').scrollLeft-fill)%PageWidth!=0){
	   Comp=fill-(GetObj('ISL_Cont').scrollLeft%PageWidth);
	   CompScr()
	 }else{
	  MoveLock=false
	 };
	 AutoPlay()};
	 function ISL_ScrUp(){
	   if(GetObj('ISL_Cont').scrollLeft<=0){
	     GetObj('ISL_Cont').scrollLeft=GetObj('ISL_Cont').scrollLeft+GetObj('List1').offsetWidth};
	     GetObj('ISL_Cont').scrollLeft-=Space
	   };
	 function ISL_GoDown(){
	   clearInterval(MoveTimeObj);
	   if(MoveLock)return;
	   clearInterval(AutoPlayObj);
	   MoveLock=true;
	   MoveWay="right";
	   ISL_ScrDown();
	   MoveTimeObj=setInterval('ISL_ScrDown()',Speed)
	 };
	 function ISL_StopDown(){
	   if(GotoLock){return};
	   if(MoveWay=="left"){return};
	   clearInterval(MoveTimeObj);
	   if(GetObj('ISL_Cont').scrollLeft%PageWidth-(fill>=0?fill:fill+1)!=0){
	     Comp=PageWidth-GetObj('ISL_Cont').scrollLeft%PageWidth+fill;
		 CompScr()
	   }else{
	     MoveLock=false
		};
		AutoPlay()
	  };
	  function ISL_ScrDown(){
	    if(GetObj('ISL_Cont').scrollLeft>=GetObj('List1').scrollWidth){
		  GetObj('ISL_Cont').scrollLeft=GetObj('ISL_Cont').scrollLeft-GetObj('List1').scrollWidth
		};
		GetObj('ISL_Cont').scrollLeft+=Space
	  };
	  function CompScr(){
	    if(Comp==0){
		  ISL_Page=Math.round((GetObj('ISL_Cont').scrollLeft-fill)/PageWidth)+1;
		  if(ISL_Page>ISL_Pages){ISL_Page=1};
		  MoveLock=false;
		  GotoLock=false;return
		};
		var num,TempSpeed=Speed,TempSpace=Space;
		if(Math.abs(Comp)<PageWidth/5){
		  TempSpace=Math.round(Math.abs(Comp/5));
		  if(TempSpace<1){TempSpace=1}
		 };
		
         if(Comp<0){
		   if(Comp<-TempSpace){
		     Comp+=TempSpace;num=TempSpace
		   }else{
			 num=-Comp;
			 Comp=0
			};
		   GetObj('ISL_Cont').scrollLeft-=num;
		   setTimeout('CompScr()',TempSpeed)
		 }else{
		   if(Comp>TempSpace){
			 Comp-=TempSpace;
			 num=TempSpace
		   }else{
		     num=Comp;
			 Comp=0
			};
		    GetObj('ISL_Cont').scrollLeft+=num;
		    setTimeout('CompScr()',TempSpeed)
		  }
		}