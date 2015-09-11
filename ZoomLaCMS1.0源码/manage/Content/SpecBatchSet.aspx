<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpecBatchSet.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.SpecBatchSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>专题批量设置</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <script language="JavaScript" type="text/JavaScript">
        function SelectAll()
        {
            for(var i=0;i<document.getElementById('<%=LstSpec.ClientID%>').length;i++)
            {
                document.getElementById('<%=LstSpec.ClientID%>').options[i].selected=true;
            }
        }
        function UnSelectAll()
        {
            for(var i=0;i<document.getElementById('<%=LstSpec.ClientID%>').length;i++)
            {
                document.getElementById('<%=LstSpec.ClientID%>').options[i].selected=false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<a href="SpecialManage.aspx">专题管理</a>&gt;&gt;<span>批量设置</span>
	</div>
    <div class="clearbox"></div>
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                批量设置专题</td>
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
                            <asp:ListBox ID="LstSpec" runat="server" DataTextField="SpecName" DataValueField="SpecID"
                                SelectionMode="Multiple" Height="282px" Width="100%"></asp:ListBox></td>
                    </tr>
                    <tr>
                        <td align="center">
                            <input id="BtnSelectAll" onclick="SelectAll()" type="button" class="inputbutton"
                                value="  选定所有专题  " />
                            <input id="BtnCancelAll" onclick="UnSelectAll()" type="button" class="inputbutton"
                                value="取消选定所有专题" />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">                
                <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">                
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
                                <asp:CheckBox ID="ChkTemp" runat="server" /></td>
                            <td style="width: 200px; text-align:right" class="tdbgleft">
                                <strong>列表页模板：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtTemplate" MaxLength="255" runat="server" Columns="50"></asp:TextBox>
                                <input type="button" value="选择模板" onclick="WinOpenDialog('../Template/TemplateList.aspx?OpenerText='+escape('TxtTemplate')+'&FilesDir=',650,480)" class="button"/>
                                </td>
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
