<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNode.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.AddNode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>添加栏目节点</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/JavaScript">
        var tID=0;
        var arrTabTitle = new Array("TabTitle0","TabTitle1","TabTitle2","TabTitle3");
        var arrTabs = new Array("Tabs0","Tabs1","Tabs2","Tabs3");
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<a href="NodeManage.aspx">节点管理</a>&gt;&gt;<span>添加子节点</span>
	</div>
    <div class="clearbox"></div>    
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                基本信息</td>
            <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                栏目选项</td>
            <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                模板选项</td>
            <td id="TabTitle3" class="tabtitle" onclick="ShowTabs(3)">
                生成选项</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
        <tr class="tdbg">
            <td valign="top" style="margin-top: 0px; margin-left: 0px;">
            <table width="100%" cellpadding="2" cellspacing="1" style="background-color: white;">
            <%-- 基本信息--%>
            <script type="text/javascript">
            function GetPYDir()
                {
                    var NodeName = document.getElementById("<%= TxtNodeName.ClientID %>");
                    var checkUserNameMessage = document.getElementById("CheckUserNameMessage");                        
                    if(NodeName.value!="")
                    {
                        CallTheServer(NodeName.value,"");
                    }                        
                }
            function CallTheServer(arg,context)
            {
                <%= CallBackReference %>
            }
                    
            function ReceiveServerData(result)
            {
                var NodeDir = document.getElementById("<%= TxtNodeDir.ClientID %>");
                NodeDir.value=result;
            }
            </script>
                    <tbody id="Tabs0">
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>所属栏目：</strong></td>
                            <td>
                                &nbsp;<asp:Label ID="LblNodeName" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="HdnParentId" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnDepth" Value="0" runat="server" />
                                <asp:HiddenField ID="HdnOrderID" Value="0" runat="server" />
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>栏目名称：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtNodeName" runat="server" Columns="30" onblur="GetPYDir()"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtNodeName"
                                    ErrorMessage="RequiredFieldValidator">栏目名不能为空</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>栏目标识名：</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtNodeDir" MaxLength="20" runat="server" Columns="20"></asp:TextBox>
                                <span style="color: Blue">注意，目录名只能是字母、数字、下划线组成，首字符不能是数字
                                    <asp:RegularExpressionValidator ID="RegTxtNodeDir" runat="server" ControlToValidate="TxtNodeDir"
                                        Display="Dynamic" ValidationExpression="[_a-zA-Z][_a-zA-Z0-9]*" ErrorMessage="目录名格式错误"
                                        SetFocusOnError="True"></asp:RegularExpressionValidator></span>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>栏目图片地址：</strong>
                                </td>
                            <td>
                                <asp:TextBox ID="TxtNodePicUrl" MaxLength="255" runat="server" Columns="50"></asp:TextBox></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>栏目提示：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtTips" runat="server" Columns="60" Height="30" Width="500" Rows="2"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>栏目说明：</strong>
                                </td>
                            <td>
                                <asp:TextBox ID="TxtDescription" runat="server" Columns="60" Height="30" Width="500"
                                    Rows="2" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>栏目META关键词：</strong>
                                </td>
                            <td>
                                <asp:TextBox ID="TxtMetaKeywords" runat="server" Height="65" Width="500" Columns="60"
                                    Rows="4" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>栏目META网页描述：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtMetaDescription" runat="server" Height="65" Width="500" Columns="60"
                                    Rows="4" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                    <%--栏目选项--%>
                    <tbody id="Tabs1" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>打开方式：</strong></td>
                            <td>
                                <asp:RadioButtonList ID="RBLOpenType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">原窗口打开</asp:ListItem>
                                    <asp:ListItem Value="1">新窗口打开</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>内容打开方式：</strong></td>
                            <td>
                                <asp:RadioButtonList ID="RBLItemOpenType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">原窗口打开</asp:ListItem>
                                    <asp:ListItem Value="1">新窗口打开</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px;" class="tdbgleft">
                                <strong>栏目权限：</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RBLPurviewType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">开放</asp:ListItem>
                                    <asp:ListItem Value="1">认证</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>评论权限：</strong></td>
                            <td><asp:RadioButtonList ID="RBLCommentType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">允许评论</asp:ListItem>
                                <asp:ListItem Value="1">不允许评论</asp:ListItem>
                            </asp:RadioButtonList></td>
                        </tr>                        
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>本栏目热点的点击数最小值：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtHitsOfHot" runat="server" Columns="5">0</asp:TextBox>
                                <asp:RangeValidator ID="ValgHitsOfHot" runat="server" ControlToValidate="TxtHitsOfHot"
                                    ErrorMessage="请输入整数" MinimumValue="0" MaximumValue="2147483647" Type="Integer"
                                    SetFocusOnError="True"></asp:RangeValidator></td>
                        </tr>
                    </tbody>
                    <%--模板选项--%>
                    <tbody id="Tabs2" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>栏目列表页模板：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtTemplate" MaxLength="255" runat="server" Columns="50"></asp:TextBox>
                                <input type="button" value="选择模板" onclick="WinOpenDialog('../Template/TemplateList.aspx?OpenerText='+escape('TxtTemplate')+'&FilesDir=',650,480)" class="button"/>
                                </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>栏目首页模板：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtIndexTemplate" MaxLength="255" runat="server" Columns="50"></asp:TextBox>
                                <input type="button" value="选择模板" onclick="WinOpenDialog('../Template/TemplateList.aspx?OpenerText='+escape('TxtIndexTemplate')+'&FilesDir=',650,480)" class="btn"/>
                                </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>选择内容模型:</strong></td>
                            <td>
                                <asp:HiddenField ID="HdnModeID" runat="server" />
                                <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr class="tdbg">
                                            <td style="width: 100px" class="tdbgleft">
                                                <%# GetChk(Eval("ModelID","{0}")) %>
                                                <%# Eval("ModelName") %>                                                
                                            </td>
                                            <td>
                                                <input type="text" name="TxtModelTemplate_<%# Eval("ModelID") %>" id="TxtModelTemplate_<%# Eval("ModelID") %>" value="<%# GetTemplate(Eval("ModelID","{0}")) %>" maxlength="255" />                                                
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
                    <tbody id="Tabs3" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>列表首页扩展名：</strong></td>
                            <td><asp:RadioButtonList ID="RBLListEx" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">.html</asp:ListItem>
                                <asp:ListItem Value="1">.htm</asp:ListItem>
                                <asp:ListItem Value="2">.shtml</asp:ListItem>
                                <asp:ListItem Value="3">.aspx</asp:ListItem>
                            </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
                                <strong>内容页扩展名：</strong></td>
                            <td><asp:RadioButtonList ID="RBLContentEx" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">.html</asp:ListItem>
                                <asp:ListItem Value="1">.htm</asp:ListItem>
                                <asp:ListItem Value="2">.shtml</asp:ListItem>
                                <asp:ListItem Value="3">.aspx</asp:ListItem>
                            </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 288px" class="tdbgleft">
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
            </table>
            </td>
        </tr>        
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">                
                &nbsp; &nbsp;
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp; &nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="BtnCancel" value="取消" onclick="window.location.href='NodeManage.aspx'" />                
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
