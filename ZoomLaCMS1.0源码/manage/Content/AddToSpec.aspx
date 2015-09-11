<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddToSpec.aspx.cs" Inherits="Zoomla.WebSite.Manage.Content.AddToSpec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加到其他专题</title>
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
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span>专题内容管理</span>&gt;&gt;<span>添加到其他专题</span>
	</div>
    <div class="clearbox"></div>
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr class="tdbg" align="center">
            <td colspan="2" class="spacingtitle">
                添加内容到专题
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>选定的内容ID：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtGeneralID" runat="server" Width="280px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>添加到目标专题：</strong>
                <br />
                <span style="color: Red">提示：</span>可以按住“Shift”<br />
                或“Ctrl”键进行多个专题的选择</td>
            <td>
                <asp:ListBox ID="LstSpec" runat="server" Rows="10" SelectionMode="Multiple" Width="280px"></asp:ListBox>
                <br />                
                <input id="Button1" onclick="SelectAll()" type="button" class="inputbutton" value="  选定所有专题  " />
                <input id="Button2" onclick="UnSelectAll()" type="button" class="inputbutton" value="取消选定所有专题" />
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" colspan="2">            
                <asp:Button ID="BtnBacthSet" runat="server" Text="执行批处理" CssClass="inputbutton" OnClick="BtnBacthSet_Click" />&nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="取消" CssClass="inputbutton" OnClick="BtnCancel_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
