<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchNode.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.BatchNode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>节点批量设置</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/JavaScript">
        var tID=0;
        var arrTabTitle = new Array("TabTitle0","TabTitle1","TabTitle2");
        var arrTabs = new Array("Tabs0","Tabs1","Tabs2");
        function ShowTabs(ID)
        {
            if(ID!=tID)
            {
                document.getElementById(arrTabTitle[tID].toString()).className = "tabtitle";
                document.getElementById(arrTabTitle[ID].toString()).className = "titlemouseover";
                document.getElementById(arrTabs[tID].toString()).style.display = "none";
                document.getElementById(arrTabs[ID].toString()).style.display = "";
                tID=ID;
            }
        }
        function SelectAll()
        {
            for(var i=0;i<document.getElementById('<%=LstNodes.ClientID%>').length;i++)
            {
                document.getElementById('<%=LstNodes.ClientID%>').options[i].selected=true;
            }
        }
        function UnSelectAll()
        {
            for(var i=0;i<document.getElementById('<%=LstNodes.ClientID%>').length;i++)
            {
                document.getElementById('<%=LstNodes.ClientID%>').options[i].selected=false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<a href="NodeManage.aspx">节点管理</a>&gt;&gt;<span>批量设置</span>
	</div>
    <div class="clearbox"></div>
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                批量设置节点</td>
        </tr>
        <tr class="tdbg">
            <td valign="top" align="center" style="width:213px">
                <table>
                    <tr>
                        <td align="left">
                            <span style="color: Red">提示：</span>可以按住“Shift”<br />
                            或“Ctrl”键进行多个节点的选择</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:ListBox ID="LstNodes" runat="server" DataTextField="NodeName" DataValueField="NodeId"
                                SelectionMode="Multiple" Height="282px" Width="100%"></asp:ListBox></td>
                    </tr>
                    <tr>
                        <td align="center">
                            <input id="BtnSelectAll" onclick="SelectAll()" type="button" class="inputbutton"
                                value="  选定所有节点  " />
                            <input id="BtnCancelAll" onclick="UnSelectAll()" type="button" class="inputbutton"
                                value="取消选定所有节点" />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr align="center">
                        <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)" style="height: 24px">
                            栏目选项</td>
                        <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)" style="height: 24px">
                            模板选项</td>
                        <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)" style="height: 24px">
                            生成选项</td>            
                        <td style="height: 24px">
                            &nbsp;</td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
                <%--栏目选项--%>
                    <tbody id="Tabs0" style="">
                        <tr class="tdbg">
                            <td style="width: 30px; text-align:center" class="tdbgleft">
                                <asp:CheckBox ID="ChkOpenType" runat="server" /></td>
                            <td style="width: 200px; text-align:right" class="tdbgleft">
                                <strong>打开方式：</strong></td>
                            <td>
                                <asp:RadioButtonList ID="RBLOpenType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">原窗口打开</asp:ListItem>
                                    <asp:ListItem Value="1">新窗口打开</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 30px; text-align:center" class="tdbgleft">
                                <asp:CheckBox ID="ChkItemOpen" runat="server" /></td>
                            <td style="width: 200px; text-align:right" class="tdbgleft">
                                <strong>内容打开方式：</strong></td>
                            <td>
                                <asp:RadioButtonList ID="RBLItemOpenType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">原窗口打开</asp:ListItem>
                                    <asp:ListItem Value="1">新窗口打开</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 30px; text-align:center" class="tdbgleft">
                                <asp:CheckBox ID="ChkPurview" runat="server" /></td>
                            <td style="width: 200px; text-align:right" class="tdbgleft">
                                <strong>栏目权限：</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RBLPurviewType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">开放</asp:ListItem>
                                    <asp:ListItem Value="1">认证</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 30px; text-align:center" class="tdbgleft">
                                <asp:CheckBox ID="ChkComment" runat="server" /></td>
                            <td style="width: 200px; text-align:right" class="tdbgleft">
                                <strong>评论权限：</strong></td>
                            <td><asp:RadioButtonList ID="RBLCommentType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">允许评论</asp:ListItem>
                                <asp:ListItem Value="1">不允许评论</asp:ListItem>
                            </asp:RadioButtonList></td>
                        </tr>                        
                        <tr class="tdbg">
                            <td style="width: 30px; text-align:center" class="tdbgleft">
                                <asp:CheckBox ID="ChkHits" runat="server" /></td>
                            <td style="width: 200px; text-align:right" class="tdbgleft">
                                <strong>本栏目热点的点击数最小值：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtHitsOfHot" runat="server" Columns="5">0</asp:TextBox>
                                <asp:RangeValidator ID="ValgHitsOfHot" runat="server" ControlToValidate="TxtHitsOfHot"
                                    ErrorMessage="请输入整数" MinimumValue="0" MaximumValue="2147483647" Type="Integer"
                                    SetFocusOnError="True"></asp:RangeValidator></td>
                        </tr>
                    </tbody>
                    <%--模板选项--%>
                    <tbody id="Tabs1" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 30px; text-align:center" class="tdbgleft">
                                <asp:CheckBox ID="ChkTemp" runat="server" /></td>
                            <td style="width: 200px; text-align:right" class="tdbgleft">
                                <strong>栏目列表页模板：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtTemplate" MaxLength="255" runat="server" Columns="50"></asp:TextBox>
                                <input type="button" value="选择模板" onclick="WinOpenDialog('../Template/TemplateList.aspx?OpenerText='+escape('TxtTemplate')+'&FilesDir=',650,480)" class="button"/>
                                </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 30px; text-align:center" class="tdbgleft">
                                <asp:CheckBox ID="ChkITemp" runat="server" /></td>
                            <td style="width: 200px; text-align:right" class="tdbgleft">
                                <strong>栏目首页模板：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtIndexTemplate" MaxLength="255" runat="server" Columns="50"></asp:TextBox>
                                <input type="button" value="选择模板" onclick="WinOpenDialog('../Template/TemplateList.aspx?OpenerText='+escape('TxtIndexTemplate')+'&FilesDir=',650,480)" class="btn"/>
                                </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 30px; text-align:center" class="tdbgleft">
                                <asp:CheckBox ID="ChkModelID" runat="server" /></td>
                            <td style="width: 200px; text-align:right" class="tdbgleft">
                                <strong>选择内容模型:</strong></td>
                            <td>
                                <asp:HiddenField ID="HdnModeID" runat="server" />
                                <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr class="tdbg">
                                            <td style="width: 100px" class="tdbgleft">
                                                <input type="checkbox" id="ChkModel" name="ChkModel" value="<%# Eval("ModelID") %>" />
                                                <%# Eval("ModelName") %>                                                
                                            </td>
                                            <td>
                                                <input type="text" name="TxtModelTemplate_<%# Eval("ModelID") %>" id="TxtModelTemplate_<%# Eval("ModelID") %>" value="<%# Eval("ContentTemplate") %>" maxlength="255" />                                                
                                                <input type="button" value="选择模板" onclick="WinOpenDialog('../Template/TemplateList.aspx?OpenerText='+escape('TxtModelTemplate_<%# Eval("ModelID") %>')+'&FilesDir=',650,480)" class="btn"/>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </table>
                                </td>
                        </tr>                       
                    </tbody>
                    <%--生成选项--%>
                    <tbody id="Tabs2" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 30px; text-align:center" class="tdbgleft">
                                <asp:CheckBox ID="ChkListEx" runat="server" /></td>
                            <td style="width: 200px; text-align:right" class="tdbgleft">
                                <strong>列表首页扩展名：</strong></td>
                            <td><asp:RadioButtonList ID="RBLListEx" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">.html</asp:ListItem>
                                <asp:ListItem Value="1">.htm</asp:ListItem>
                                <asp:ListItem Value="2">.shtml</asp:ListItem>
                                <asp:ListItem Value="3">.aspx</asp:ListItem>
                            </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 30px; text-align:center" class="tdbgleft">
                                <asp:CheckBox ID="ChkContentEx" runat="server" /></td>
                            <td style="width: 200px; text-align:right" class="tdbgleft">
                                <strong>内容页扩展名：</strong></td>
                            <td><asp:RadioButtonList ID="RBLContentEx" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">.html</asp:ListItem>
                                <asp:ListItem Value="1">.htm</asp:ListItem>
                                <asp:ListItem Value="2">.shtml</asp:ListItem>
                                <asp:ListItem Value="3">.aspx</asp:ListItem>
                            </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 30px; text-align:center" class="tdbgleft">
                                <asp:CheckBox ID="ChkContentRule" runat="server" /></td>
                            <td style="width: 200px; text-align:right" class="tdbgleft">
                                <strong>内容页文件名规则：</strong></td>
                            <td>
                                <asp:DropDownList ID="DDLContentRule" runat="server">
                                    <asp:ListItem Selected="True" Value="0">栏目目录/年/月/日/infoid</asp:ListItem>
                                    <asp:ListItem Value="1">栏目目录/年-月/InfoID</asp:ListItem>
                                    <asp:ListItem Value="2">栏目目录/InfoID</asp:ListItem>
                                    <asp:ListItem Value="3">栏目目录/年月日/标题</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>                       
                    </tbody>
                    <tr class="tdbg">
                            <td colspan="3" class="tdbgleft">
                            <font color="blue">说明：</font><br/> 
                            1、若要批量修改某个属性的值，请先选中其左侧的复选框，然后再设定属性值。<br/>
                            2、这里显示的属性值都是系统默认值，与所选节点的已有属性无关
                            </td>                            
                        </tr>
                </table>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2" class="tdbgleft">
                <asp:Button ID="EBtnBacthSet" Text="执行批处理" OnClick="EBtnBacthSet_Click" runat="server" />
                <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="BtnCancel_Click" /></td>
        </tr>
    </table>    
    
    </form>
</body>
</html>
