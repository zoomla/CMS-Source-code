<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserSignin.aspx.cs" Inherits="User_UserFunc_UserSignin" MasterPageFile="~/User/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="/JS/CalendarTable.js"></script>
    <title>会员签到</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="UserSignin"></div>
<div class="container margin_t5">
     <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">签到</li> 
    </ol>
</div>
<div class="container btn_green u_cnt">
    <div style="width:310px;float:left;">
                <table class="table table-bordered" id="caltable" style="margin-bottom: 0px;">
                    <thead>
                        <tr style="vertical-align: middle; text-align: center;">
                            <td colspan="7" style="line-height: 34px;">
                                <a href="javaScript:subMonth();" title="上一月" class="DayButton"><%=lang.LF("上一月")%></a>
                                <input name="year" id="year" class="form-control" type="text" style="text-align: center; width: 65px; display: inline;" size="4" maxlength="4" onkeydown="if (event.keyCode==13){setDate()}" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" onpaste="this.value=this.value.replace(/[^0-9]/g,'')" />
                                <%=lang.LF("年")%>
                                <input name="month" id="month" type="text" class="form-control" style="text-align: center; width: 45px; display: inline;" size="1" maxlength="2" onkeydown="if (event.keyCode==13){setDate()}" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" onpaste="this.value=this.value.replace(/[^0-9]/g,'')" />
                                <%=lang.LF("月")%> <a href="JavaScript:addMonth();" title="下一月" class="DayButton"><%=lang.LF("下一月")%></a></td>
                        </tr>
                        <tr style="vertical-align: middle; text-align: center;">
                            <script>
                                document.write("<td class=\"DaySunTitle\" id=\"diary\" name=\"diary\" >" + days[0] + "</td>");
                                for (var intLoop = 1; intLoop < days.length - 1; intLoop++) {
                                    document.write("<td class=\"DayTitle\" id=\"diary\" name=\"diary\">" + days[intLoop] + "</td>");
                                }
                                document.write("<td class=\"DaySatTitle\" id=\"diary\" name=\"diary\" >" + days[intLoop] + "</td>");
                            </script>
                        </tr>
                    </thead>
                    <tbody id="calendar" class="text-center">
                        <script type="text/javascript">
                            for (var intWeeks = 0; intWeeks < 6; intWeeks++) {
                                document.write("<TR style=\"cursor: hand\">");
                                for (var intDays = 0; intDays < days.length; intDays++) document.write("<td class=\"CalendarTD\" onMouseover=\"buttonOver();\" onMouseOut=\"buttonOut();\"></td>");
                                document.write("</TR>");
                            }
                        </script>
                    </tbody>
                </table>
                <script type="text/javascript">
                    Calendar();
                </script>
 
            </div>
    <div style="float:left;padding-left:10px;">
        <asp:Button runat="server" CssClass="btn btn-primary" Text="签到" ID="Signin_Btn" OnClick="Signin_Btn_Click" />
        <span class="alert alert-info" style="padding: 8px;" runat="server" id="Remind_Span" visible="false"><span class="fa fa-star"></span>今日签到成功!!</span>
    </div> 
</div>
<%--    <asp:TextBox runat="server" ID="T1"></asp:TextBox>--%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .hassign{background-color:#428bca;}
    </style>
    <script type="text/javascript">
        function HasSignDays(days)//1,2,30
        {
            var arr=days.split(',');
            for (var i = 0; i < arr.length; i++) {
                
                $("#caltable tr td").each(function () { if (arr[i] != "" && $(this).text() == arr[i]) { $(this).addClass("hassign"); console.log(arr[i]); } });
            }
        }
    </script>
</asp:Content>