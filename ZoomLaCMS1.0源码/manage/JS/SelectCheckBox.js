// JScript 文件

function CheckExcBox(box)    
{
   for (var i=0;i<document.form1.elements.length;i++)
   {
      var e = document.form1.elements[i];
      if ( (e.type=='checkbox') )
        {                    
           var o=e.name.lastIndexOf('ChkSelect');
                   
           if(o!=-1)
           {
			    e.checked = box.checked;
           }
         }
    }
            
    if(document.form1.checkSel.checked==true)
    {
		ShowA.innerHTML="取消选择";
	}
	else
	{
		ShowA.innerHTML="选择全部";
	}
}
function IsSelectedId()
{ 
    var flag = false;    
    var s = document.form1.getElementsByTagName("INPUT")
    
    for(var i=0;i<s.length;i++)
    {
        if(s[i].type=="checkbox")
        {
            if(s[i].checked)
            {
                flag = true; 
                break;
            } 
        }
    }
   return flag; 
}