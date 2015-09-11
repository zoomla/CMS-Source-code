// JavaScript Document
function g(o){return document.getElementById(o);}
function HoverLi(n){
for(var i=1;i<=2;i++){g('tb_'+i).className='normaltab';g('tbc_0'+i).className='undis';}g('tbc_0'+n).className='dis';g('tb_'+n).className='hovertab';
}
function GetT()
{
var kdocTitle = document.title;//БъЬт 
if(kdocTitle == null)
{ 
	var t_titles = document.getElementByTagName("title");
	if(t_titles && t_titles.length >0) 
	{ 
	   kdocTitle = t_titles[0]; 
	}else{ 
	   kdocTitle = ""; 
	} 
} 
//alert(kdocTitle);
document.getElementById("tii").innerText=kdocTitle;
}


