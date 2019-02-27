<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchDetail.aspx.cs" Inherits="ZoomLa.WebSite.SearchDetail" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>内容详细搜索</title>
<link href="../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
    function SelectKey()
    {
        window.open('/Common/KeyList.aspx?OpenerText=<%=TxtTagKey.ClientID %>','','width=600,height=450,resizable=0,scrollbars=yes');
    }
</script>
</head>
<body>
<form id="form1" runat="server">    
<div class="us_seta">   
    <h1 style="text-align: center">
            <asp:Label ID="LblModelName" runat="server" Text="Label"></asp:Label></h1>
    <table style="width: 100%; margin: 0 auto;" cellpadding="0" cellspacing="0" class="border">            
        <tr class="tdbg">
            <td style="width:20%; float:left;line-height:30px; text-align:right">
                栏目：
            </td>
            <td style="width:80%;line-height:30px">
       		    <asp:DropDownList ID="NodeID" runat="server" Height="20px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width:20%; float:left;line-height:30px; text-align:right">
                标题：
            </td>
            <td style="width:80%;line-height:30px">
       		    <asp:TextBox ID="TxtTitle" runat="server" Height="13px" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width:20%; float:left;line-height:30px; text-align:right">
                关键词：
            </td>
            <td style="width:80%;line-height:30px">
       		    <asp:TextBox ID="TxtTagKey" runat="server" Height="13px" Width="200px"></asp:TextBox>
       		    &nbsp;<span style="color: #0000ff">[</span><a href="#" onclick="SelectKey();">
                 <span style="text-decoration: underline; color: Green;">选择关键字</span></a><span style="color: #0000ff">]</span>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width:20%; float:left;line-height:30px; text-align:right">
                录入人：
            </td>
            <td style="width:80%;line-height:30px">
       		    <asp:TextBox ID="TxtInputer" runat="server" Height="13px" Width="200px" onclick="this.value='';"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width:20%; float:left;line-height:30px; text-align:right">
                发布时间：
            </td>
            <td style="width:80%;line-height:30px">
       		    从<asp:TextBox ID="BeginDate" runat="server" Height="13px" Width="120px" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox>
       		    到<asp:TextBox ID="EndDate" runat="server" Height="13px" Width="120px" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox>
            </td>
        </tr>
        <asp:Literal ID="ModelSearchHtml" runat="server"></asp:Literal>
        <tr class="tdbgbottom">
            <td colspan="2" align="center">
                <asp:Button ID="Button1" runat="server" Text="搜索" OnClick="Button1_Click"/><asp:HiddenField ID="HdnModel" runat="server" />
            </td>
        </tr>
    </table>        
</div>

</form>
</body>
</html>