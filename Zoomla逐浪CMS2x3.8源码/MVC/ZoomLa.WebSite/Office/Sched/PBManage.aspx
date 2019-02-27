<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PBManage.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Sched.PBManage"   MasterPageFile="~/Common/Master/UserEmpty.master"  ValidateRequest="false"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>排班管理</title>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script type="text/javascript">
    $().ready(function () {
        $("#month_td").find("tr:gt(0)").addClass("tdbg");
        $("#month_td").find("tr:gt(0)").mouseover(function () { $(this).removeClass("tdbg").addClass("tdbgmouseover") }).mouseout(function () { $(this).removeClass("tdbgmouseover").addClass("tdbg") });
        $("#week_td").find("tr:gt(0)").addClass("tdbg");
        $("#week_td").find("tr:gt(0)").mouseover(function () { $(this).removeClass("tdbg").addClass("tdbgmouseover") }).mouseout(function () { $(this).removeClass("tdbgmouseover").addClass("tdbg") });
        $("select").each(function () {
            $(this).parent().css("background", $(this).find("option:selected").attr("MyColor"))
        })
        for (var i = 0; i < $("#month_td tr").length; i++) {
            for (var j = 17; j < 33; j++) {
                $("#month_td tr")[i].cells[j].style.display = "none";
            }
        }
    });
    function DataBind(v) {
        $(".bcdp").append("<option value='0'>请选择</option>");
        jsonD = eval(v);
        for (var i = 0; i < jsonD.length; i++) {
            $(".bcdp").append("<option value='" + jsonD[i].ID + "' MyColor='" + jsonD[i].BackColor + "' >" + jsonD[i].BCName + "</option>").parent().css("background", "");
        }
    }
    //根据信息,设定好值
    function SetValue(v) {
        jsonD = eval(v);
        for (var i = 0; i < jsonD.length; i++) {
            $("select[name='" + jsonD[i].UserID + "bcDP1']").val(jsonD[i].Monday);
            $("select[name='" + jsonD[i].UserID + "bcDP2']").val(jsonD[i].Tuesday);
            $("select[name='" + jsonD[i].UserID + "bcDP3']").val(jsonD[i].Wednesday);
            $("select[name='" + jsonD[i].UserID + "bcDP4']").val(jsonD[i].Fourday);
            $("select[name='" + jsonD[i].UserID + "bcDP5']").val(jsonD[i].Friday);
            $("select[name='" + jsonD[i].UserID + "bcDP6']").val(jsonD[i].Saturday);
            $("select[name='" + jsonD[i].UserID + "bcDP7']").val(jsonD[i].Sunday);
        }
    }
    function changecolor(obj) {
        $(obj).parent().css("background", $(obj).find("option:selected").attr("MyColor"));
    }
    function checkSelect() {
        var sel = $("select").find("option:selected[value=0]");
        if (sel.length > 0) {
            alert("有尚未选择的排班!!")
        }
    }
    function showtable(obj) {
        if ($(obj).attr("status") == "0") {
            $(obj).find("img")[0].src = "/Plugins/JqueryUI/Sly/img/ArrowL.png";
            $(obj).attr("status", "1");
        }
        else {
            $(obj).find("img")[0].src = "/Plugins/JqueryUI/Sly/img/ArrowR.png";
            $(obj).attr("status", "0");
        }
        for (var i = 0; i < $("#month_td tr").length; i++) {
            for (var k = 2; k < 17; k++) {
                $("#month_td tr")[i].cells[k].style.display = $("#month_td tr")[i].cells[k].style.display == "none" ? "" : "none";
            }
            for (var j = 17; j < 33; j++) {
                $("#month_td tr")[i].cells[j].style.display = $("#month_td tr")[i].cells[j].style.display == "none" ? "" : "none";
            }
        }
    }
</script>
<style>
.btn-primary:hover{ text-decoration:none;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="draftnav">
    <a href="/Office/Main.aspx">工作台</a>/<span>排班管理</span>
</div>
<div class="mainDiv">
    <div class="us_seta" style="margin-top: 5px;"><!--后期加上职务-->
        <div id="site_main">
            <asp:TextBox runat="server" ID="searchText" placeholder="请输入需要查询的信息" CssClass="form-control" />
            <asp:Button runat="server" CssClass="btn-primary" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click"/>
            <a href="bcadd.aspx" style="padding-top:10px;padding-bottom:10px;" class="btn-primary" >添加班次</a>
                <%--<input type="button" class="btn-primary" value="添加班次" onclick="window.location = 'bcadd.aspx';"/>--%>
            <asp:RadioButton runat="server" ID="radioWeek" Text="按星期" Checked="true" AutoPostBack="true" OnCheckedChanged="radioWeek_CheckedChanged" name="dateR"/>
            <asp:RadioButton runat="server" ID="radioMonth" Text="按月份" AutoPostBack="true" OnCheckedChanged="radioMonth_CheckedChanged" name="dateR"/>
            <span id="Week_Next" style="float:right;" runat="server">
                <asp:Button runat="server" ID="preWeekBtn" OnClick="preWeekBtn_Click" Text="上周" CssClass="btn-primary"/>
                <asp:Button runat="server" ID="nextWeekBtn" OnClick="nextWeekBtn_Click" Text="下周" CssClass="btn-primary"/>
                <asp:Button runat="server" ID="thisWeekBtn" OnClick="thisWeekBtn_Click" Text="返回本周" CssClass="btn-primary"/>
            </span>
            <span id="Month_Next" style="float:right;" runat="server" visible="false">
                <asp:Button ID="preMonthBtn" runat="server" Text="上月" OnClick="preMonthBtn_Click" CssClass="btn-primary" />
                <asp:Button ID="NextMonthBtn" runat="server" Text="下月" OnClick="NextMonthBtn_Click" CssClass="btn-primary" />
                <asp:Button runat="server" ID="thisMonthBtn" OnClick="thisMonthBtn_Click" Text="返回本月" CssClass="btn-primary"/>
            </span>
            <div id="week_div" style="margin-top:5px;" runat="server">
                <table id="week_td" class="table table-striped table-bordered table-hover" style="text-align:center;">
                    <tr><asp:Literal runat="server" ID="dateHtml"></asp:Literal></tr>
                    <asp:Repeater runat="server" ID="pbRepeater">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("GroupName") %><input type="hidden" name="userID" value="<%#Eval("UserID") %>"/></td>
                                <td><%#Eval("HoneyName") %></td>
                                <td><select onchange="changecolor(this)" class="bcdp" name="<%#Eval("UserID")+"bcDP1" %>"></select></td>
                                <td><select onchange="changecolor(this)" class="bcdp" name="<%#Eval("UserID")+"bcDP2" %>"></select></select></td>
                                <td><select onchange="changecolor(this)" class="bcdp" name="<%#Eval("UserID")+"bcDP3" %>"></select></select></td>
                                <td><select onchange="changecolor(this)" class="bcdp" name="<%#Eval("UserID")+"bcDP4" %>"></select></select></td>
                                <td><select onchange="changecolor(this)" class="bcdp" name="<%#Eval("UserID")+"bcDP5" %>"></select></select></td>
                                <td><select onchange="changecolor(this)" class="bcdp" name="<%#Eval("UserID")+"bcDP6" %>"></select></select></td>
                                <td><select onchange="changecolor(this)" class="bcdp" name="<%#Eval("UserID")+"bcDP7" %>"></select></select></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Button runat="server" ID="saveBtn" CssClass="btn-primary" Text="保存" OnClick="saveBtn_Click" OnClientClick="checkSelect()" style="margin-top:10px;" />
            </div>
            <div runat="server" id="monthDiv" visible="false" style="margin-top:10px;"><!--只有查看权,改为打开一个新窗口显示月度-->
                <table class="table-border" style="width:100%;">
                    <tr>
                        <td colspan="2" style="text-align:center;font-size:16px;font-weight:bold;line-height:30px;height:30px;"><%= StartMonth.ToString("yyyy年MM月") %>排班</td>
                    </tr>
                    <tr>
                        <td>
                            <table id="month_td" class="table table-striped table-bordered table-hover" style="text-align:center;">
                                <tr>
                                    <asp:Literal runat="server" ID="monthHtml"></asp:Literal>
                                </tr>
                                <asp:Repeater runat="server" ID="monthRepeater">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("GroupName") %><input type="hidden" name="userID" value="<%#Eval("UserID") %>"/></td>
                                            <td><%#Eval("HoneyName") %></td>
                                            <td style="background:<%# Eval("BCColor1")%>"><%#Eval("BC1") %></td>
                                            <td style="background:<%# Eval("BCColor2")%>"><%#Eval("BC2") %></td>
                                            <td style="background:<%# Eval("BCColor3")%>"><%#Eval("BC3") %></td>
                                            <td style="background:<%# Eval("BCColor4")%>"><%#Eval("BC4") %></td>
                                            <td style="background:<%# Eval("BCColor5")%>"><%#Eval("BC5") %></td>
                                            <td style="background:<%# Eval("BCColor6")%>"><%#Eval("BC6") %></td>
                                            <td style="background:<%# Eval("BCColor7")%>"><%#Eval("BC7") %></td>
                                            <td style="background:<%# Eval("BCColor8")%>"><%#Eval("BC8") %></td>
                                            <td style="background:<%# Eval("BCColor9")%>"><%#Eval("BC9") %></td>
                                            <td style="background:<%# Eval("BCColor10")%>"><%#Eval("BC10") %></td>
                                            <td style="background:<%# Eval("BCColor11")%>"><%#Eval("BC11") %></td>
                                            <td style="background:<%# Eval("BCColor12")%>"><%#Eval("BC12") %></td>
                                            <td style="background:<%# Eval("BCColor13")%>"><%#Eval("BC13") %></td>
                                            <td style="background:<%# Eval("BCColor14")%>"><%#Eval("BC14") %></td>
                                            <td style="background:<%# Eval("BCColor15")%>"><%#Eval("BC15") %></td>
                                            <td style="background:<%# Eval("BCColor16")%>"><%#Eval("BC16") %></td>
                                            <td style="background:<%# Eval("BCColor17")%>"><%#Eval("BC17") %></td>
                                            <td style="background:<%# Eval("BCColor18")%>"><%#Eval("BC18") %></td>
                                            <td style="background:<%# Eval("BCColor19")%>"><%#Eval("BC19") %></td>
                                            <td style="background:<%# Eval("BCColor20")%>"><%#Eval("BC20") %></td>
                                            <td style="background:<%# Eval("BCColor21")%>"><%#Eval("BC21") %></td>
                                            <td style="background:<%# Eval("BCColor22")%>"><%#Eval("BC22") %></td>
                                            <td style="background:<%# Eval("BCColor23")%>"><%#Eval("BC23") %></td>
                                            <td style="background:<%# Eval("BCColor24")%>"><%#Eval("BC24") %></td>
                                            <td style="background:<%# Eval("BCColor25")%>"><%#Eval("BC25") %></td>
                                            <td style="background:<%# Eval("BCColor26")%>"><%#Eval("BC26") %></td>
                                            <td style="background:<%# Eval("BCColor27")%>"><%#Eval("BC27") %></td>
                                            <td style="background:<%# Eval("BCColor28")%>"><%#Eval("BC28") %></td>
                                            <td style="background:<%# Eval("BCColor29")%>"><%#Eval("BC29") %></td>
                                            <td style="background:<%# Eval("BCColor30")%>"><%#Eval("BC30") %></td>
                                            <td style="background:<%# Eval("BCColor31")%>"><%#Eval("BC31") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                        <td onclick="showtable(this)" title="显示其他排班" style="border:1px solid #ccc;cursor:pointer;" status="0">
                            <img src="/Plugins/JqueryUI/Sly/img/ArrowR.png" alt="" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div><div class="clearfix"></div>
</div><!--MainDiv-->
</asp:Content>

