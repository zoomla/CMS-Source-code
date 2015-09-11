<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" CodeFile="labelSql.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Template.labelSql" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Sql查询设置</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
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
    if (oDragHandle.className=="spanfixdiv"||oDragHandle.className=="spanfixdiv1")
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

function dragend2(textBox)
{
    if(isdrag)
    {
        savePos(textBox);
        var target = document.getElementById('<% =TxtTjValue.ClientID %>');
        if(nn6)
        {
            var pre = target.value.substr(0, start);
            var post = target.value.substr(end);
            target.value = pre + inserttext + post;
        }
        else
        {
            target.focus();
            var range = document.selection.createRange();
            range.text = inserttext ;
        }
    }
}
function dragend3(textBox)
{
    if(isdrag)
    {
        savePos(textBox);
        var target = document.getElementById('<% =TextBox1.ClientID %>');
        if(nn6)
        {
            var pre = target.value.substr(0, start);
            var post = target.value.substr(end);
            target.value = pre + inserttext + post;
        }
        else
        {
            target.focus();
            var range = document.selection.createRange();
            range.text = inserttext ;
        }
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
function changecate()
{
    var obj=document.getElementById("DropLabelType");
    var tar=document.getElementById("TxtLabelType");
    var text=obj.value;
    if(text!="")
    {
        tar.value=text;
    }
}
-->
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<a href="LabelManage.aspx">标签管理</a>&gt;&gt;<span>动态标签设置</span>
	</div>
    <div class="clearbox"></div>
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center" style="height: 26px">
                <b>动态标签设置</b></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 80px">
                <strong>标签名称：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelName" runat="server" Width="288px" />
                <asp:RequiredFieldValidator runat="server" ID="NReq" ControlToValidate="TxtLabelName"
                    Display="Dynamic" ErrorMessage="请输入标签名称" ></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="TxtLabelName"
                    ErrorMessage="标签名称重复" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 80px">
                <strong>标签分类：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelType" runat="server" Width="216px"></asp:TextBox>
                <asp:DropDownList ID="DropLabelType" runat="server" onchange="changecate();">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtLabelType"
                    ErrorMessage="标签分类不能为空"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 80px; text-align: right;">
                <strong>标签类型：&nbsp;</strong></td>
            <td class="tdbg">
                <asp:RadioButtonList ID="RBLType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="2">动态标签</asp:ListItem>
                    <asp:ListItem Value="4">分页列表标签</asp:ListItem>
                    <asp:ListItem Value="3">数据源标签</asp:ListItem>
                </asp:RadioButtonList>   
            </td>
        </tr>  
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 80px">
                <strong>标签说明：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelIntro" runat="server" TextMode="MultiLine" Columns="50" Rows="3"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 80px">
                <strong>选择数据：</strong>
            </td>
            <td>
                <table style="width: 100%; margin: 0 auto;">                            
                    <tr>
                        <td class="tdbg" colspan="3">
                            <div id="selectdatediv" class="selectlist">
                                <table>
                                    <tr>
                                        <td style="width: 50px; text-align: right; height: 22px;">
                                            主表：</td>
                                        <td style="height: 22px">
                                            <asp:DropDownList ID="DbTableDownList" runat="server" Width="200px" AutoPostBack="True"
                                                OnSelectedIndexChanged="DBTableDownList_SelectedIndexChanged" /></td>
                                        <td style="width: 50px; text-align: right; height: 22px;">
                                            从表：</td>
                                        <td style="height: 22px">
                                            <asp:DropDownList ID="DbTableDownList2" runat="server" Width="200px" AutoPostBack="True"
                                                OnSelectedIndexChanged="DBTableDownList2_SelectedIndexChanged" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px; text-align: right;">
                                            输出字段：</td>
                                        <td>
                                            <asp:ListBox ID="DbFieldDownList" runat="server" Height="220px" Width="200px" AutoPostBack="True" SelectionMode="Multiple" /></td>
                                        <td style="width: 50px; text-align: right;">
                                            输出字段：</td>
                                        <td >
                                            <asp:ListBox ID="DbFieldDownList2" runat="server" Height="220px" Width="200px" AutoPostBack="True" SelectionMode="Multiple" /></td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>                                        
        <tbody id="tj" runat="server" visible="false">
            <tr>
                <td class="tdbgleft" style="width: 80px; text-align: right;">
                    约束字段：</td>
                <td class="tdbg">
                    <div id="ycdiv" class="selectlist">
                        <asp:DropDownList ID="Dbtj" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TableJoin">
                            <asp:ListItem Value="Inner join">InnerJoin</asp:ListItem>
                            <asp:ListItem Value="left join">LeftJoin</asp:ListItem>
                            <asp:ListItem Value="outer join">OuterJoin</asp:ListItem>
                            <asp:ListItem Value="right join">RightJoin</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="DbFieldList" runat="server" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="TableJoin" />
                        =
                        <asp:DropDownList ID="DbFieldList2" runat="server" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="TableJoin" />&nbsp;
                    </div>
                </td>
            </tr>
        </tbody> 
        <tr>
            <td class="tdbgleft" style="width: 80px; text-align: right;">
                查询表：</td>
            <td class="tdbg">
                <asp:TextBox ID="TxtSqlTable" runat="server" Columns="60" Rows="3" TextMode="MultiLine" AutoPostBack="true"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="从选择数据中选择主表，若选择了从表，请在约束字段中设定表连接条件"></asp:Label></td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 80px; text-align: right;">
                查询字段：</td>
            <td class="tdbg">
                <asp:TextBox ID="TxtSqlField" runat="server" Columns="60" Rows="3" TextMode="MultiLine" AutoPostBack="true"></asp:TextBox>
                <asp:Button ID="Button3" runat="server" Text="设定查询字段" OnClick="Button3_Click" />
                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="从输出字段中选择查询的字段"></asp:Label></td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 80px; text-align: right;">
                添加参数：</td>
            <td class="tdbg">
                    <asp:Repeater ID="repParam" runat="server" OnItemCommand="repParam_ItemCommand">
                    <HeaderTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="border" style="text-align:center">        
                        <tr class="tdbg" align="center" style="height:25px;">
                            <td style="width:10%">参数名称</td><td style="width:10%">默认值</td><td style="width:15%">参数类型</td><td style="width:25%">参数说明</td><td>操作</td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">            
                            <td align="center">
                            <%#Eval("ParamName")%>				
			                </td>
			                <td align="center"> <%#Eval("ParamValue") %>
			                <td align="center"><%# GetParamType(Eval("ParamType","{0}"))%></td>
			                <td align="center"><%#Eval("ParamDesc")%></td>				
			                <td align="center">&nbsp;
				                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Param") %>'>修改</asp:LinkButton> | 
				                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Del" CommandArgument='<%# Eval("Param") %>' >删除</asp:LinkButton>
			                </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                    </asp:Repeater>                    
                    <br />
                    <table border="0" cellpadding="0" cellspacing="1" class="border" style="text-align:center">                        
                        <tr class="tdbg" align="center" style="height:25px;">
                            <td style="height: 25px; width: 80px;">
                                <asp:TextBox ID="TxtParamName" runat="server" Text="参数名称" Width="80px" /></td>
                            <td style="width: 80px;height: 25px">
                            <asp:TextBox ID="TxtParamValue" runat="server" Text="默认值" Width="80px" /></td>
                            <td align="left" style="width: 100px;height: 25px">
                                <asp:DropDownList ID="DDLParamType" runat="server">
                                    <asp:ListItem Selected="True" Value="1">普通参数</asp:ListItem>
                                    <asp:ListItem Value="2">页面参数</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="width: 100px;height: 25px" align="left">
                            <asp:TextBox ID="TxtParamDesc" runat="server" Text="参数说明" Width="80px" /></td>
                            <td style="width: 80px;height: 25px">
                            <asp:Button ID="BtnAddParam" runat="server"
                                        Text="添加" OnClick="BtnAddParam_Click" Style="cursor: pointer; cursor: hand;" /></td>
                            <td style="height: 25px">
                            <asp:HiddenField ID="HdnParam" runat="server" />
                <asp:HiddenField ID="HdnTempParam" runat="server" /></td>
                        </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 80px; text-align: right;">
                自定义参数：<br />
                （拖放添加）</td>
            <td class="tdbg">
                <div id="plist" class="plist">
                    <asp:Label ID="attlist" runat="server"></asp:Label>
                </div>
            </td>
        </tr>                           
        <tr>
            <td class="tdbgleft" style="width: 80px; text-align: right; height: 19px;">
                查询条件：</td>
            <td class="tdbg">
                <div id="gridviewclause" class="fielddiv">
                    <asp:DropDownList ID="DDLJointj" runat="server">
                        <asp:ListItem Selected="True">And</asp:ListItem>
                        <asp:ListItem>OR</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DDLFTable" runat="server" AutoPostBack="True" OnSelectedIndexChanged="BindTableField">
                        <asp:ListItem Selected="True" Value="1">主表</asp:ListItem>
                        <asp:ListItem Value="2">从表</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DDLTjField" runat="server" Width="120px" />
                    <asp:DropDownList ID="DDLtj" runat="server">                        
                        <asp:ListItem>等于</asp:ListItem>
                        <asp:ListItem>大于</asp:ListItem>
                        <asp:ListItem>小于</asp:ListItem>
                        <asp:ListItem>大于等于</asp:ListItem>
                        <asp:ListItem>小于等于</asp:ListItem>
                        <asp:ListItem>不等于</asp:ListItem>
                        <asp:ListItem>在</asp:ListItem>
                        <asp:ListItem>象</asp:ListItem>
                        <asp:ListItem>不在</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="TxtTjValue" runat="server"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="添加查询条件" OnClick="Button1_Click" /><br />
                    <asp:TextBox ID="TxtSqlWhere" runat="server" Columns="60" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </div>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 80px; text-align: right; height: 19px;">
                字段排序：</td>
            <td class="tdbg">
                <div id="Div2" class="fielddiv">                    
                    <asp:DropDownList ID="DDLOrderTable" runat="server" AutoPostBack="True" OnSelectedIndexChanged="BindOrderField">
                        <asp:ListItem Selected="True">主表</asp:ListItem>
                        <asp:ListItem>从表</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DDLOrderField" runat="server" Width="120px" />
                    <asp:DropDownList ID="DDLOrder" runat="server">
                        <asp:ListItem Value="ASC">升序</asp:ListItem>
                        <asp:ListItem Value="DESC">降序</asp:ListItem>
                    </asp:DropDownList>                    
                    <asp:Button ID="Button2" runat="server" Text="添加排序字段" OnClick="Button2_Click" Style="cursor: pointer; cursor: hand;" /><br />
                    <asp:TextBox ID="TxtOrder" runat="server" Columns="60" Rows="3" TextMode="MultiLine"></asp:TextBox><span style="color:Red;">(当标签为分页标签时,字段排序不能为空)</span>
                    </div>
            </td>
        </tr>        
        <tr>
            <td class="tdbgleft" style="width: 80px; text-align: right;">
                <strong>数据数目：</strong></td>
            <td class="tdbg">
                <asp:TextBox ID="TextBox1" runat="server" Text="10" Width="50px"></asp:TextBox></td>
        </tr>        
        <tr>
            <td class="tdbgleft" style="width: 100%; text-align: left;" colspan="2">
                <strong>标签内容:</strong>
            </td>
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
                                                字段标签</td>
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
                                            <td><div style="OVERFLOW:auto;height:410px"><asp:Label id="LblColumn" runat="server" Text="标签字段"></asp:Label></div></td>            
                                        </tr>
                                        </tbody>
                                        <tbody id="Tabs2" style="display: none">
                                        <tr align="center">                                                
                                            <td>
                                                <div style="OVERFLOW:auto;height:410px">                                    
                                                <asp:Label id="LblSysLabel" runat="server"></asp:Label>
                                                </div>
                                            </td>            
                                        </tr>
                                        </tbody>
                                        <tbody id="Tabs3" style="display: none">
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
            <td align="center" colspan="2" style="height: 21px">
                <asp:HiddenField ID="HdnlabelID" runat="server" />
                &nbsp;
                &nbsp;<asp:Button ID="BtnSave" OnClick="BtnSave_Click"
                        runat="server" Style="cursor: pointer; cursor: hand; width: 88px;" Text="保存标签" />&nbsp;&nbsp;&nbsp;<input id="BtnCancel" type="button"
                                class="inputbutton" value="取　消" onclick="window.location.href='LabelManage.aspx'" style="cursor: pointer;
                                cursor: hand; width: 88px;" />
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>