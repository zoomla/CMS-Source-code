<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="AppertainGuide.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.AppertainGuide" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>附属信息</title>
<script type="text/javascript">    
function Switch(obj)
{
    obj.className = (obj.className == "guideexpand") ? "guidecollapse" : "guideexpand";
    var nextDiv;
    if (obj.nextSibling)
    {
        if(obj.nextSibling.nodeName=="DIV")
        {
            nextDiv = obj.nextSibling;
        }
        else
        {
            if(obj.nextSibling.nextSibling)
            {
                if(obj.nextSibling.nextSibling.nodeName=="DIV")
                {
                    nextDiv = obj.nextSibling.nextSibling;
                }
            }
        }
        if(nextDiv)
        {
            nextDiv.style.display = (nextDiv.style.display != "") ? "" : "none"; 
        }
    }
}
function OpenLink(lefturl,righturl)
{
    if(lefturl!="")
    {
        parent.frames["left"].location =lefturl;
    }

    try {
        parent.MDIOpen(righturl); return false;
    } catch (Error) {
        parent.frames["main_right"].location = righturl;
    }
}

function gotourl(url) {
    try {
        parent.MDIOpen(url); void (0);
    } catch (Error) {
    parent.frames["main_right"].location = "../" + url; void (0);
    }
}
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="Div1">
    <ul>
        <li id="Guide_top">
            <div id="Guide_toptext">模型附属信息</div>
        </li>
        <li id="Guide_main">
            <div id="Guide_box">
                <div class="guideexpand" onclick="Switch(this)">
                    模型附属信息
                </div>                    
                <div class="guide">
                <ul>                        
                    <li class="guideli" onmouseover="this.className='guidelihover'" onmouseout="this.className='guideli'"><a href="RelationManage.aspx" target="main_right">级联选项管理</a></li>
                    <li class="guideli" onmouseover="this.className='guidelihover'" onmouseout="this.className='guideli'"><a href="Relation.aspx" target="main_right">添加级联选项</a></li>
                    <li class="guideli" onmouseover="this.className='guidelihover'" onmouseout="this.className='guideli'"><a href="DictionaryManage.aspx" target="main_right">数据字典管理</a></li>
                    <li class="guideli" onmouseover="this.className='guidelihover'" onmouseout="this.className='guideli'"><a href="Dictionary.aspx" target="main_right">添加数据字典</a></li>
                </ul>
                </div>
            </div>
        </li>
     </ul>
</div>
</asp:Content>