<%@ Page Language="C#" AutoEventWireup="true" validateRequest=false CodeFile="LabelHtml.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Template.LabelHtml" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>静态标签</title>
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
    .spanfixdiv1{
    	background-color: #FFFBF5;
        border: 1px solid #F6B9D6;
        padding: 5px 5px 5px 5px; 
        width: 80px;
        height: 20px;
        float: left;
        text-align: center;
        margin: 5px;
        overflow:hidden;
        cursor: hand;
    }
   .selectlist{
        background-color: #f6fdf6;
        border: 1px dashed #2F4F4F;
        padding: 5px 5px 5px 5px; 
        width: 98%;
    }       
    .plist{
        background-color: #f6fdf6;
        border: 1px dashed #2F4F4F;
        padding: 5px 5px 5px 5px; 
        width: 98%;
        vertical-align: middle;
    }
    .fielddiv{
        background-color: #f6fdf6;
        border: 1px dashed #2F4F4F;
        padding: 5px 5px 5px 5px; 
        
        float: left;
        width: 98%;
        height: 100%;
        text-align: left
    }
        -->
</style>
<script src="../JS/Common.js" type="text/javascript"></script>
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
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<a href="LabelManage.aspx">标签管理</a>&gt;&gt;<span>静态标签设置</span>
	</div>
    <div class="clearbox"></div>
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <span id="LblTitle" style="font-weight: bold;">静态标签设置</span></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>标签名称：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelName" runat="server" Width="288px" />
                <asp:RequiredFieldValidator runat="server" ID="NReq" ControlToValidate="TxtLabelName"
                    Display="Dynamic" ErrorMessage="请输入标签名称" >*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="TxtLabelName"
                    ErrorMessage="标签名称重复" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>标签分类：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelType" runat="server" Width="216px"></asp:TextBox>
                <asp:DropDownList ID="DropLabelType" runat="server" OnSelectedIndexChanged="SelectCate" AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtLabelType"
                    ErrorMessage="标签分类不能为空">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>标签说明：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelIntro" runat="server" TextMode="MultiLine" Columns="50" Rows="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 105px; text-align: right;" colspan=2>
                标签内容:</td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100%; text-align: right;" colspan="2">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 252px; vertical-align: top;" id="frmTitle">                        
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">                            
                            <tbody id="ss">
                            <tr align="center">                                                
                                <td>
                                <div id="Tab1_header">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">                            
                                        <tr align="center">
                                            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                                                自定标签</td>                                            
                                            <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                                                系统标签</td>
                                            <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
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
                                                <div style="OVERFLOW:auto;height:410px">                                    
                                                <asp:Label id="LblSysLabel" runat="server"></asp:Label>
                                                </div>
                                            </td>            
                                        </tr>
                                        </tbody>
                                        <tbody id="Tabs2" style="display: none">
                                        <tr align="center">                                                
                                            <td>
                                                <div style="OVERFLOW:auto;height:410px">                                    
                                                <asp:Label id="LblFunLabel" runat="server"></asp:Label>
                                                </div>
                                            </td>            
                                        </tr>
                                        </tbody>
                                    </table>
                                </div>
                                </td>            
                            </tr>
                            </tbody> 
                        </table>                       
                        </td>
                        <td align="left">
                        <asp:TextBox TextMode="MultiLine" runat="server" ID="textContent" Width="100%" Height="410px"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
            </td>            
        </tr>
        <tr class="tdbg">
            <td colspan="2" align="center" style="height: 55px">
                <asp:HiddenField ID="HdnLabelID" runat="server" />
                <asp:Button ID="BtnSave" runat="server"
                        Text="保　存" OnClick="BtnSave_Click" Style="cursor: pointer; cursor: hand; width: 88px;" />&nbsp;&nbsp;
                <input id="BtnCancel" type="button" class="inputbutton" value="取　消" onclick="window.location.href='LabelManage.aspx'"
                    style="cursor: pointer; cursor: hand; width: 88px;" />
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>