<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendanceStats.aspx.cs" Inherits="ZoomLaCMS.Plat.Blog.AttendanceStats" Masterpagefile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
    table tr th{text-align:center;}
    </style>
    <title>考勤统计</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
    <div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">考勤统计</span></div>
    <table class="table table-bordered table-striped table-hover text-center">
        <tr>
          <td colspan="7" class="text-center">
              <asp:Button ID="PreMonth_B" style="display:none;" runat="server" OnClick="PreMonth_Btn_Click" />
              <asp:LinkButton runat="server" CssClass="btn btn-default" OnClick="PreMonth_Btn_Click" ToolTip="上一月"><i class="fa fa-chevron-left"></i></asp:LinkButton>&nbsp;
              <span style="font-size: 25px; position: relative; top: 4px;"><%:CurDate.ToString("yyyy年MM月") %></span>
              <asp:Button ID="NextMonth_B" style="display:none;" runat="server" OnClick="NextMonth_Btn_Click" />
              <asp:LinkButton runat="server" CssClass="btn btn-default" OnClick="NextMonth_Btn_Click" ToolTip="下一月"><i class="fa fa-chevron-right"></i></asp:LinkButton></td>
        </tr>
        <tr class="trfirst"><td>本月班次(天)</td><td>迟到(人次)</td><td>早退(人次)</td><td>缺勤(人次)</td><td>无效考勤(人次)</td></tr>
        <tr>
            <td><asp:Label ID="AllCount_L" runat="server" /></td>
            <td><asp:Label ID="Late_L" runat="server" /></td>
            <td><asp:Label ID="LeaveEarly_L" runat="server" /></td>
            <td><asp:Label ID="Lack_L" runat="server" /></td>
            <td><asp:Label ID="Invalid_L" runat="server"></asp:Label></td>
        </tr>
    </table>
    <ul class="nav nav-tabs">
        <li data-tabs="0"><a href="?tabs=0">考勤概览</a></li>
        <li data-tabs="1"><a href="?tabs=1">出勤清单</a></li>
    </ul>
    <div class="tab-content panel-body padding0">
        <div class="tab-pane" id="Tabs0">
            <table class="table table-bordered table-striped table-hover text-center">
                <tr><th>姓名</th><th>部门</th><th>实际出勤(天)</th><th>迟到(次)</th><th>早退(次)</th><th>未签到/未签退(次)</th><th>无效考勤(人次)</th></tr>
                <ZL:Repeater ID="RPT" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><a href='Attendance.aspx?uid=<%#Eval("UserID") %>'><%#Eval("TrueName") %></a></td>
                            <td><%#Eval("CompName") %></td>
                            <td><%#Eval("attendance") %></td>
                            <td><%#Eval("late") %></td>
                            <td><%#Eval("leave") %></td>
                            <td><%#Eval("lack") %></td>
                            <td><%#Eval("invalid") %></td>
                        </tr>
                    </ItemTemplate>
                </ZL:Repeater>
            </table>
        </div>
        <div class="tab-pane" id="Tabs1">
            <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnPageIndexChanging="EGV_PageIndexChanging" PageSize="20" CssClass="table table-bordered table-striped table-hover text-center">
                <Columns>
                    <asp:BoundField HeaderText="日期" DataField="Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:TemplateField HeaderText="姓名">
                        <ItemTemplate>
                            <a href='Attendance.aspx?uid=<%#Eval("UserID") %>'><%#Eval("TrueName") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="签到" DataField="InTime" />
                    <asp:BoundField HeaderText="签退" DataField="OutTime" />
                    <asp:BoundField HeaderText="工时" DataField="TimeLen" />
                </Columns>
            </ZL:ExGridView>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    //选择滑动门
    function CheckTabs(id) {
        $("[data-tabs='" + id + "']").addClass('active');
        $("#Tabs" + id).addClass("active");
    }
</script>
</asp:Content>