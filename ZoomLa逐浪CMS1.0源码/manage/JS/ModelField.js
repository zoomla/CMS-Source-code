// JScript 文件
function SelectModelType()
{
    var TypeCount=document.getElementsByName("Type"); 
    
    for(var i=1;i<TypeCount.length;i++)
    { 
        var DivType=eval("Div"+TypeCount[i].value);
        
        if(TypeCount[i].checked)
        {
            DivType.style.display="";
        }
        else
        {
            DivType.style.display="none";
        }
    }
}

function AutoSelectModelType()
{
    var TypeCount=document.getElementsByName("Type"); 
    
    for(var i=1;i<TypeCount.length;i++)
    {         
        if(TypeCount[i].checked)
        {
            return TypeCount[i].value;
        }
    }
}

function isok()
{
    var txt = $('Name').value;
    var patt1= /^[0-9]+$/;
    
    if(txt.trim().length==0)
    {
        alert('字段名称不能够为空！'); 
        $("Name").focus();
        return false; 
    } 
    var patt = /^[a-zA-Z0-9_]+$/ 
    if(!patt.test(txt)) 
    {
        alert('字段名称只能由字母或数字组成！');
        $("Name").focus();
        return false; 
    }
    
    if($("Alias").value=="")
    {
        alert('字段别名不能够为空！');
        $("Alias").focus();
        return false; 
    }
    
    if(AutoSelectModelType()=="TextType")
    {
        txt=$('TitleSize').value;
        if($("TitleSize").value=="")
        {
            alert("文本框长度不能够为空！")
            $("TitleSize").focus();
            return false;
        }
        
        if(!patt1.test(txt))
        {
            alert("文本框长度只能够由数字组成！")
            $("TitleSize").focus();
            return false;            
        }
    }
    
    if(AutoSelectModelType()=="MultipleTextType")
    {
        txt=$('MultipleTextType_Width').value;
        if($("MultipleTextType_Width").value=="")
        {
            alert("显示的宽度不能够为空！")
            $("MultipleTextType_Width").focus();
            return false;
        }
        
        if(!patt1.test(txt))
        {
            alert("显示的宽度只能够由数字组成！")
            $("MultipleTextType_Width").focus();
            return false;            
        }
        
        txt=$('MultipleTextType_Height').value;
        if($("MultipleTextType_Height").value=="")
        {
            alert("显示的高度不能够为空！")
            $("MultipleTextType_Height").focus();
            return false;
        }
        
        if(!patt1.test(txt))
        {
            alert("显示的高度只能够由数字组成！")
            $("MultipleTextType_Height").focus();
            return false;            
        }
    }
    
    if(AutoSelectModelType()=="MultipleHtmlType")
    {
        txt=$('MultipleHtmlType_Width').value;
        if($("MultipleHtmlType_Width").value=="")
        {
            alert("显示的宽度不能够为空！")
            $("MultipleHtmlType_Width").focus();
            return false;
        }
        
        if(!patt1.test(txt))
        {
            alert("显示的宽度只能够由数字组成！")
            $("MultipleHtmlType_Width").focus();
            return false;            
        }
        
        txt=$('MultipleHtmlType_Height').value;
        if($("MultipleHtmlType_Height").value=="")
        {
            alert("显示的高度不能够为空！")
            $("MultipleHtmlType_Height").focus();
            return false;
        }
        
        if(!patt1.test(txt))
        {
            alert("显示的高度只能够由数字组成！")
            $("MultipleHtmlType_Height").focus();
            return false;            
        }
    }
    
    if(AutoSelectModelType()=="RadioType")
    {
        if($("RadioType_Content").value=="")
        {
            alert("分行键入每个选项不能够为空！")
            $("RadioType_Content").focus();
            return false;
        }
    }
    
    if(AutoSelectModelType()=="ListBoxType")
    {
        if($("ListBoxType_Content").value=="")
        {
            alert("分行键入每个选项不能够为空！")
            $("ListBoxType_Content").focus();
            return false;
        }
    }
    
    return true;
}

function SelectDictionary(val)
{
    WinOpenDialog('../infomodel/SelectDictionary.aspx?ControlId='+val+'','550','400');
}