// JScript 文件
function CorrectReport()
{
    kdocTitle = document.title;//标题 
    if(kdocTitle == null)
    { 
        var t_titles = document.getElementByTagName("title");
        if(t_titles && t_titles.length >0) 
        { 
           kdocTitle = t_titles[0]; 
        }else
        { 
           kdocTitle = ""; 
        }         
     }
     var url=window.location.href;
     var curl="/Prompt/correct.aspx?t="+kdocTitle+"&u="+url;
     window.location.href=curl;
}
