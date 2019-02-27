<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Attendance.aspx.cs" Inherits="Plat_Blog_Attendance" masterpagefile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>我的考勤</title>
    <style>
    table .th{border-top:1px solid #3792e5!important;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

<div class="container platcontainer">
<div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2"><asp:Label ID="Tit_L" runat="server" Text="我的考勤" /></span><a id="stats_a" runat="server"  href="AttendanceStats.aspx" style="float:right;"><i class="fa  fa-sign-in"></i> 考勤统计</a></div>
<table class="table table-bordered table-striped table-hover text-center">
    <tr>
      <td colspan="7" class="text-center">
          <asp:Button ID="PreMonth_B" style="display:none;" runat="server" OnClick="PreMonth_Btn_Click" />
          <asp:LinkButton runat="server" CssClass="btn btn-default" OnClick="PreMonth_Btn_Click" ToolTip="上一月"><i class="fa fa-chevron-left"></i></asp:LinkButton>&nbsp;
          <span style="font-size: 25px; position: relative; top: 4px;"><%:CurDate.ToString("yyyy年MM月") %></span>
          <asp:Button ID="NextMonth_B" style="display:none;" runat="server" OnClick="NextMonth_Btn_Click" />
          <asp:LinkButton runat="server" CssClass="btn btn-default" OnClick="NextMonth_Btn_Click" ToolTip="下一月"><i class="fa fa-chevron-right"></i></asp:LinkButton></td>
    </tr>
    <tr class="th"><td>本月班次(天)</td><td>实际出勤(天)</td><td>迟到(次)</td><td>早退(次)</td><td>未签到/未签退(次)</td></tr>
    <tr style="color:red;">
        <td style="color:#000;"><asp:Label ID="AllCount_L" runat="server" /></td>
        <td><asp:Label ID="Attendance_L" runat="server" /></td>
        <td><asp:Label ID="Late_L" runat="server" /></td>
        <td><asp:Label ID="LeaveEarly_L" runat="server" /></td>
        <td><asp:Label ID="Lack_L" runat="server" /></td>
    </tr>
</table>
<table class="table table-bordered" id="DateTable">
  <thead>
    <tr class="th">
      <td>周一</td>
      <td>周二</td>
      <td>周三</td>
      <td>周四</td>
      <td>周五</td>
      <td>周六</td>
      <td>周日</td>
    </tr>
  </thead>
  <tbody runat="server" id="DateBody" data-toggle="modal">
    <tr>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W1_D1"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W1_D2"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W1_D3"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W1_D4"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W1_D5"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W1_D6"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W1_D7"></asp:Literal>
    </tr>
    <tr>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W2_D1"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W2_D2"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W2_D3"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W2_D4"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W2_D5"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W2_D6"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" id="Rep_W2_D7"></asp:Literal>
    </tr>
    <tr>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W3_D1"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W3_D2"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W3_D3"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W3_D4"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W3_D5"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W3_D6"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W3_D7"></asp:Literal>
    </tr>
    <tr>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W4_D1"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W4_D2"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W4_D3"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W4_D4"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W4_D5"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W4_D6"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W4_D7"></asp:Literal>
    </tr>
    <tr>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W5_D1"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W5_D2"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W5_D3"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W5_D4"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W5_D5"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W5_D6"></asp:Literal>
      <asp:Literal runat="server" EnableViewState="false" ID="Rep_W5_D7"></asp:Literal>
    </tr>
  </tbody>
</table>
</div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>