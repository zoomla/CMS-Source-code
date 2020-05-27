<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MisSignSet.aspx.cs" Inherits="MIS_MisSignSet" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>考勤设置</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript">

</script>
<style type="text/css">
.auto-style1 { width: 260px; padding-left: 4px; height: 13px; }
.auto-style2 { width: 180px; padding-left: 4px; height: 13px; }
.auto-style3 { width: 140px; padding-left: 4px; height: 13px; }
.auto-style4 { width: 80px; padding-left: 4px; height: 13px; }
</style>
</head>
<body>
<form id="form1" runat="server">
  <div id="ShowSign" runat="server" style="padding-left:14px; padding-top:10px;">
    <table>
      <tr class="TrSign">
        <td class="workDay">工作日</td>
        <td class="Times">上下班时间</td>
        <td class="BeginDate">开始执行日期</td>
        <td class="Play">操作</td>
      </tr>
      <asp:Repeater ID="repSign" runat="server">
        <ItemTemplate>
          <tr class="TrShows">
            <td class="workDay"><%#Eval("WorkDate")%></td>
            <td class="Times"><%#Eval("WorkBegin")%> ~ <%#Eval("WorkEnd")%></td>
            <td class="BeginDate"><%#StatusName(Convert.ToInt32(Eval("Status")))%></td>
            <td class="Play"><a href="MisSignSetInfo.aspx?ID=<%#Eval("ID")%>">修改</a></td>
          </tr>
        </ItemTemplate>
      </asp:Repeater>
    </table>
  </div>
</form>
</body>
</html>