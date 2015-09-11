<%@ Page Language="C#" AutoEventWireup="true" validateRequest=false CodeFile="TemplateEdit.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Template.TemplateEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑模板</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />

    <script src="../JS/Common.js" type="text/javascript"></script>

    <style type="text/css">
    <!-- 
    .dragspandiv{
        background-color: #FFFBF5;
        FILTER: alpha(opacity=70);
        border: 1px solid #F6B9D6;
        text-align: center;
        overflow:hidden;
        padding:2px;
        height:20px;
    }
    .spanfixdiv{
        background-color: #FFFBF5;
        border: 1px solid #F6B9D6;
        text-align: center;
        overflow:hidden;
        cursor: hand;
        height:20px;
        margin: 4px;
    }
    
    #fixdiv {margin: 7px;}
    
    .nodefixdiv 
    {
        background-color: #FFFBF5;
        border: 1px solid #F6B9D6;
        text-align: center;
        overflow:hidden;
        cursor: hand;
        padding:2px;
        height:20px;
    }
    .alertspandiv
    {
        background-color: #FFEBE5;
        border: 1px solid #F6B9D6;
        text-align: center;
        text-valign: middle; 
        padding:2px;
        height:30px;
        width:100px;
    }
        -->
</style>

<script type="text/javascript">    
<!--
var start=0, end=0;
var x,y;
var dragspan;
var inserttext;
var nn6=document.getElementById&&!document.all;
var isdrag=false;
var labeltype = '0';

function initDrag(e) {
    var oDragHandle = nn6 ? e.target : event.srcElement;
    if (oDragHandle.className=="spanfixdiv")
    {
        isdrag = true;
        dragspan = document.createElement('div');
        dragspan.style.position = "absolute";
        dragspan.className = "dragspandiv";
        y = nn6 ? e.clientY + 5 : event.clientY + 5;
        x = nn6 ? e.clientX + 10 : event.clientX + 10;
        dragspan.style.width = oDragHandle.style.width;
        dragspan.style.height = oDragHandle.style.height;
        dragspan.style.top = y + "px";
        dragspan.style.left = x + "px";
        dragspan.innerHTML = oDragHandle.innerHTML;
        document.body.appendChild(dragspan);
        document.onmousemove = moveMouse;
        
        inserttext = oDragHandle.getAttribute("code");        
        labeltype = oDragHandle.getAttribute("outtype");
        
        return false;
    }
}

function moveMouse(e) {
    if (isdrag) {
        dragspan.style.top = (nn6 ? e.clientY : event.clientY) + document.documentElement.scrollTop + 5 + "px";
        dragspan.style.left = (nn6 ? e.clientX : event.clientX) + document.documentElement.scrollLeft + 10 + "px";
        return false;
    }
}

function dragend(textBox)
{   
    if(isdrag)
    {
        savePos(textBox);
        cit();
    }
}

function savePos(textBox) 
{
    if(typeof(textBox.selectionStart) == "number"){
        start = textBox.selectionStart;
        end = textBox.selectionEnd;
    }
}

function cit()
{
    var target = document.getElementById('<% =textContent.ClientID %>');
    if(nn6)
    {
        var pre = target.value.substr(0, start);
        var post = target.value.substr(end);
        if(labeltype == '1')
        {
            target.value = pre + "{ZL.Label id=\"" +inserttext + "\"/}" + post;            
        }
        else if(labeltype == '2')
        {
            
            var link= "Insertlabel.aspx?n=" + escape(inserttext);
            if(window.showModalDialog != null)
            {
                var ret = showModalDialog(link,'','dialogWidth:500px; dialogHeight:300px; help: no; scroll: no; status: no; edge: sunken;');
                if (ret != null)
                {
                    if (ret.replace(/^\s+|\s+$/g,"") == "")
                    {
                        alert("不能输入空值");
                    }
                    else
                    {
                        target.value = pre + ret + post;
                    }
                }
            }
            else
            {
                window.open(link,window,'modal=yes,width=500,height=300,menubar=no,toolbar=no,location=no,resizable=no,status=no,scrollbars=no');
            }
        }
        else
        {
            target.value = pre + inserttext + post;
        }
    }
    else
    {
        target.focus();
        var range = document.selection.createRange();
        if(labeltype == '1')
        {
            range.text = "{ZL.Label id=\"" +inserttext + "\"/}";            
        }
        else if(labeltype == '2')
        {
            var link= "Insertlabel.aspx?n=" + escape(inserttext);
            if(window.showModalDialog != null)
            {
                var ret = showModalDialog(link,'','dialogWidth:500px; dialogHeight:300px; help: no; scroll: no; status: no; edge: sunken;');
                if (ret != null)
                {
                    if (ret.replace(/^\s+|\s+$/g,"") == "")
                    {
                        alert("不能输入空值");
                    }
                    else
                    {
                        range.text = ret;
                    }
                }
            }
            else
            {
                window.open(link,window,'modal=yes,width=500,height=300,menubar=no,toolbar=no,location=no,resizable=no,status=no,scrollbars=no');
            }            
        }
        else if(labeltype=='3')
        {
            range.text="{ZL.Source id=\"" +inserttext + "\"/}";
        }        
        else
        {
            range.text = inserttext;
        }
    }    
}

function DragPos(textBox) 
{
    if(isdrag)
    {
        if(nn6)
        {
            textBox.focus();
        }
        else
        {
            var rng = textBox.createTextRange(); 
            rng.moveToPoint(event.x,event.y); 
            rng.select(); 
        }
    }
}

document.onmousedown = initDrag;

document.onmouseup = function() {
    isdrag=false;
    if(dragspan != null)
    {
        document.body.removeChild(dragspan);
        dragspan = null;
    }
}
-->
</script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="99%" cellpadding="2" border="0" cellspacing="1" class="border" align="center">
                <tr class="title">
                    <td align="left" colspan=2>
                        编辑模板 文件名：
                        <asp:TextBox ID="TxtFilename" runat="server"></asp:TextBox>
                        <asp:Label ID="lblFielName" runat="server" Text="Label"></asp:Label>
                        路径:
                        <%=ShowPath%>
                        <asp:HiddenField ID="HdnShowPath" runat="server" />
                        <asp:HiddenField ID="Hdnmethod" runat="server" />
                        <asp:HiddenField ID="HdnFilePath" runat="server" />
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="left" style="height: 410px" valign="top">
                    <table style="width: 100%">
                    <tr>
                        <td style="width: 252px; vertical-align: top;" id="frmTitle">
                        <div id="Tab1_header">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">                            
                            <tr align="center">
                                <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                                    自定标签</td>
                                <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                                    数据字段</td>
                                <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                                    系统标签</td>
                                <td id="TabTitle3" class="tabtitle" onclick="ShowTabs(3)">
                                    扩展函数</td>                  
                                <td>&nbsp;</td>            
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">                            
                            <tbody id="Tabs0">
                            <tr align="center">                                                
                                <td>
                                <asp:DropDownList ID="DDLCate" runat="server" OnSelectedIndexChanged="ChangeCate" AutoPostBack="true">
                                </asp:DropDownList>
                                </td>            
                            </tr>
                            <tr align="center">                                                
                                <td><div style="OVERFLOW:auto;height:410px"><asp:Label ID="LblLabel" runat="server"></asp:Label></div></td>            
                            </tr>
                            </tbody>
                            <tbody id="Tabs1" style="display: none">
                            <tr align="center">                                                
                                <td>
                                <asp:DropDownList ID="DDLField" runat="server" OnSelectedIndexChanged="ChangeSourceField" AutoPostBack="true">
                                </asp:DropDownList>
                                </td>            
                            </tr>
                            <tr align="center">                                                
                                <td><div style="OVERFLOW:auto;height:410px"><asp:Label ID="LblSourceField" runat="server"></asp:Label></div></td>            
                            </tr>
                            </tbody>
                            <tbody id="Tabs2" style="display: none">
                            <tr align="center">                                                
                                <td>
                                    <div style="OVERFLOW:auto;height:410px">                                    
                                    <asp:Label ID="lblSys" runat="server"></asp:Label>
                                    </div>
                                </td>            
                            </tr>
                            </tbody>
                            <tbody id="Tabs3" style="display: none">
                            <tr align="center">                                                
                                <td>
                                    <div style="OVERFLOW:auto;height:410px">                                    
                                    <asp:Label ID="lblFun" runat="server"></asp:Label>
                                    </div>
                                </td>            
                            </tr>
                            </tbody>
                        </table>
                        </div>
                        </td>
                        <td height="435x" align="left">
                        <asp:TextBox TextMode="MultiLine" runat="server" ID="textContent" Width="100%" Height="435px"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                    </td>                    
                </tr>
                <tr class="tdbg">
                    <td align="left">
                        <asp:Button ID="btnSave" runat="server" Text="保存模板" CssClass="btn" OnClick="btnSave_Click" />
                        <asp:Button ID="btnReset" CssClass="btn" runat="server" Text="恢复模板" OnClick="btnReset_Click" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
