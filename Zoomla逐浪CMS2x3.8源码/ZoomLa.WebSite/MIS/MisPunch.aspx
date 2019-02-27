<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MisPunch.aspx.cs" Inherits="MIS_MisPunch" %>
<!DOCTYPE html>
<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>考勤打卡</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script>
function ShowDivs(div1,div2) {
    document.getElementById(div1).style.display = "none";
    document.getElementById(div2).style.display = "block";
    return false;
}
function ShowDiv() {
    document.getElementById("EditBtn").style.display = "none";
    document.getElementById("EditComm").innerText=document.getElementById("lblComm").innerText;
    document.getElementById("EditTxt").style.display = "block";
    return false;
}
function show2() {
    var ShowDate = document.getElementById("ShowDate");
    var dates = document.getElementById("showdates");
    var ShowDay = document.getElementById("ShowDay");
    var ShowDail = document.getElementById("ShowDail");
    var Begin = document.getElementById("lblBtime");
    var End = document.getElementById("lblEtime");
    var hda = document.getElementById("HidDate");
    var btimes = document.getElementById("lblBend");
    var etimes = document.getElementById("lblEend");
    var Digital = new Date();
    var year = Digital.getFullYear();
    var months = Digital.getMonth() + 1;
    var Day = Digital.getDay();
    var Days = Digital.getDate();
    var hours = Digital.getHours();
    var minutes = Digital.getMinutes();
    var seconds = Digital.getSeconds();
    if (hours == 0)
        hours = 12;
    if (minutes <= 9)
        minutes = "0" + minutes;
    if (seconds <= 9)
        seconds = "0" + seconds;
    var ctime = hours + ":" + minutes + ":" + seconds;
    var arr_week = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    //ShowDate.innerHTML = year + "-" + months + "-" + Days;
    dates.innerText = year + "-" + months + "-" + Days;
    hda.value = year + "-" + months + "-" + Days;
    ShowDay.innerText = Days;
    ShowDail.innerText = arr_week[Day];
    if (btimes.innerText == null || btimes.innerText == "") {
        Begin.innerText = ctime;
    }
    else {
        Begin.innerText = "";
    }
    if (etimes.innerText == null || etimes.innerText == "") {
        End.innerText = ctime;
    }
    else {
        End.innerText = "";
    }
    setTimeout("show2()",1000);
}
window.onload = show2();
</script>
<style type="text/css">
.Cright { width: 533px; }
</style>
</head>
<body>
<form id="form1" runat="server">
  <input type="hidden" id="HidDate" runat="server" />
  <div class="CardBody" style="overflow:hidden;padding-top:10px;">
    <div class="Cleft">
      <div style="width: 120px;margin-left:18px; text-align:center;" id="ShowDate" runat="server">
        <asp:Label ID="showdates" runat="server"></asp:Label>
      </div>
      <div style="width: 120px;margin-left:18px;text-align:center; font-size:60px; color:#0094ff; font-weight:bold;" id="ShowDay"></div>
      <div style="width: 120px;margin-left:18px;text-align:center;" id="ShowDail"></div>
    </div>
    <div class="Cright">
      <table border="0" style="width: 538px">
        <tr class="TrAtt">
          <td class="TBeginTime">上班 :
            <asp:Label ID="lblBegin" runat="server"></asp:Label></td>
          <td class="TBTimes"><span style="float:left">
            <asp:Label ID="lblBtime" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <asp:Label ID="lblBend" runat="server"></asp:Label>
            </span> <span style="float:right; margin-right:4px;">
            <asp:Button ID="BtnBegin" CssClass="i_bottom" Text="签到" runat="server" OnClick="BtnBegin_Click"/>
            </span></td>
        </tr>
        <tr class="TrAtt">
          <td class="TEndTime">下班 :
            <asp:Label ID="lblEnd" runat="server"></asp:Label></td>
          <td class="TBTimes"><span style="float:left;">
            <asp:Label ID="lblEtime" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <asp:Label ID="lblEend" runat="server"></asp:Label>
            </span> <span style="float:right; margin-right:4px;">
            <asp:Button ID="BtnEnd" CssClass="i_bottom" Text="签退" runat="server" OnClick="BtnEnd_Click" />
            </span></td>
        </tr>
        <tr class="TrAtt">
          <td class="TxtComms">备注 :</td>
          <td class="TxtCommbody"><input type="hidden" id="HidIDs" runat="server" />
            <div id="EditBtn">
              <asp:Label ID="lblComm" runat="server"></asp:Label>
              &nbsp;<a href="#" onclick="ShowDiv()">编辑</a> </div>
            <div id="EditTxt" style="display:none;">
              <asp:TextBox TextMode="MultiLine" ID="EditComm" runat="server" Height="70px" Width="372px"></asp:TextBox>
              <asp:Button ID="BtnSub" runat="server" OnClick="BtnSub_Click" Text="确定"/>
              &nbsp;
              <asp:Button ID="BtnClose" runat="server" Text="取消" OnClientClick="return ShowDivs('EditTxt','EditBtn')" />
            </div></td>
        </tr>
      </table>
    </div>
  </div>
</form>
</body>
</html>