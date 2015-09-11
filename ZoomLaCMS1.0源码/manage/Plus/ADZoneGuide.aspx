<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ADZoneGuide.aspx.cs" Inherits="manage_Plus_ADZoneGuide" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>广告版位引擎</title>
     <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/MasterPage.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />  
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/RiQi.js" type="text/javascript"></script>    
</head>
<body>
    <form id="form1" runat="server">
      <div class="guideexpand"> 广告版位管理</div>
        <div class="guide">
            <ul>
                <li><a id="EahADManage" href="ADZoneManage.aspx" target="main_right"><span style="color: #000000">
                    广告版位管理</span></a> </li>
                <li><a id="EahADAdd" href="ADZone.aspx" target="main_right"><span style="color: #000000">
                    添加广告版位</span></a> </li>
                <li><a id="A1" href="Advertisement.aspx" target="main_right"><span style="color: #000000">
                    广告JS版位</span></a> </li>
            </ul>
        </div>
        <div class="guideexpand" onclick="Switch(this)">
            广告搜索</div>
        <div class="guide">
            <table border="0" cellpadding="0" cellspacing="0" class="border" width="100%">
                <tr>
                    <td style="height: 23px">
                        &nbsp;<asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>版位名称</asp:ListItem>
                            <asp:ListItem>版位介绍</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" /></td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">
    function OpenMainRight()
    {
        var field=0;
        var keyword = document.getElementById("TxtKeyWord").value;
        var objSel = document.getElementById("SelField");
       
        if (keyword =="")
        {
            alert("请输入要查询的条件！"); 
            return false; 
        }
      field = objSel.options[objSel.options.selectedIndex].value;
     
       var url = "ADManage.aspx?ListType="+ field +"&KeyWord="+escape(keyword);
       JumpToMainRight(url);
    }
 </script>
 </form>
</body>
</html>

