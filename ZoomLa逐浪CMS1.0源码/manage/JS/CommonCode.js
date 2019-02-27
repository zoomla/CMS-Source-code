
String.prototype.endWith = function(oString)
{   
    var reg = new RegExp(oString + "$");
    return reg.test(this);
}

function batchconfirm(prompt, nocheckprompt)
{
    var prompt = (arguments.length > 0) ? arguments[0] : "确定要进行此批量操作？";
    var nocheckprompt = (arguments.length > 1) ? arguments[1] : "请选择所要操作的记录！";
    var haschecked = false;
    for (var i=0; i<document.forms[0].length; i++) 
    { 
        var o = document.forms[0][i]; 
        if (o.type == "checkbox" && o.name.endWith("CheckBoxButton") && o.checked == true) 
        { 
            haschecked = true;
            break;
        } 
    } 
    if (!haschecked)
    {
        alert(nocheckprompt);
        return false;
    }
    else
    {
        if (!confirm(prompt))
        {
            return false;
        }
    }
}

function JumpToLeft(url)
{
    parent.frames["left"].location = url;
}

function ReloadLeft()
{
    parent.frames["left"].location.reload();
}

function Redirect(url)
{
    window.location.href = url;
}
