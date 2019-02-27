<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DailyPlan.aspx.cs" Inherits="Plat_DocContent" masterpagefile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head"><title runat="server" id="title_str"></title>
<script type="text/javascript" src="/JS/jquery.validate.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<nav class="navbar navbar-default ">
      <div class="container-fluid">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-7" aria-expanded="false">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand nav_title" href="/home"><span class="fa fa-home"></span> </a><a class="navbar-brand" href="/home"><asp:Literal runat="server" id="UserName_Li"></asp:Literal>的日程</a>
        </div>

        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-7">
          <ul class="nav navbar-nav dailyplan_nav">
            <li class="active"><a href="DailyPlan.aspx<%=Request.Url.Query %>"><span class="fa fa-calendar"></span> 月</a></li>
            <li><a href="SubjectForWeek.aspx<%=Request.Url.Query %>"><span class="fa-calendar-plus-o"></span> 周</a></li>
            <li><a href="SubjectForDay.aspx<%=Request.Url.Query %>"><span class="fa fa-clock-o"></span> 日</a></li>
          </ul>
        </div><!-- /.navbar-collapse -->
      </div>
    </nav>
<div class="container-fluid">
<div class="col-md-2">
    <div class="panel panel-default">
    <div class="panel-heading">日程列表[<a href="DailyPlan.aspx?type=-1">所有日程</a>]<span class="pull-right"><a href="javascript:;" title="添加日程" onclick="ShowTypeDiag()"><span class="fa fa-plus"></span></a></span></div>
        <ul class="list-group ullist" id="sublist">
        <asp:Repeater ID="SubList_RPT" runat="server" OnItemCommand="SubList_RPT_ItemCommand">
            <ItemTemplate>
                <li class="list-group-item" data-uid="<%#Eval("MID") %>" data-type="<%#Eval("ID") %>"><%#GetSubName() %>
                    <span class="pull-right">
                        <a href="javascript:;" title="修改" onclick="event.stopPropagation();ShowEditType(<%#Eval("ID") %>,'<%#Eval("Title") %>')"><span class="fa fa-pencil"></span></a>
                        <asp:LinkButton runat="server" CommandName="DelType" OnClientClick="event.stopPropagation(); return confirm('确认删除该日程吗?')" CommandArgument='<%#Eval("ID") %>'><span class="fa fa-trash-o"></span></asp:LinkButton>
                    </span>
                </li>
            </ItemTemplate>
        </asp:Repeater>
        </ul>
        <div class="panel-body empty_div" runat="server" visible="false" id="EmptySub_Div">
            <p>没有相关日程!</p>
        </div>
    </div>
</div>
    <div class="col-md-7 platcontainer" style="margin-top:0px;">
            <div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">日程管理</span></div>
   <table class="table table-bordered" id="DateTable">
  <thead>
    <tr>
      <td colspan="7" class="text-center">
          <asp:Button ID="PreMonth_B" style="display:none;" runat="server" OnClick="PreMonth_Btn_Click" />
          <asp:LinkButton runat="server" CssClass="btn btn-primary" OnClick="PreMonth_Btn_Click" ToolTip="上一月"><i class="fa fa-chevron-left"></i></asp:LinkButton>
          <strong style="font-size: 25px; position: relative; top: 4px;"><%:CurDate.ToString("yyyy年MM月") %></strong>
          <asp:Button ID="NextMonth_B" style="display:none;" runat="server" OnClick="NextMonth_Btn_Click" />
          <asp:LinkButton runat="server" CssClass="btn btn-primary" OnClick="NextMonth_Btn_Click" ToolTip="下一月"><i class="fa fa-chevron-right"></i></asp:LinkButton></td>
    </tr>
    <tr>
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
<div class="col-md-3">
    <div class="panel panel-default">
    <div class="panel-heading"><asp:Literal ID="TopSubName_Li" runat="server"></asp:Literal>日程</div>
        <ul class="list-group ullist" id="lastlist">
        <asp:Repeater ID="MyTop_RPT" runat="server">
            <ItemTemplate>
                <li class="list-group-item" data-id="<%#Eval("ID") %>"><span class="last_title"><%#Eval("Name") %></span> <span class="pull-right last_date"><%#Eval("StartDate") %></span></li>
            </ItemTemplate>
        </asp:Repeater>
        </ul>
        <div class="panel-body empty_div" runat="server" visible="false" id="listempty_div">
            <p>您还没有日程!</p>
        </div>
    </div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
/*超出部分点击查详情*/
.datas {height:100px;width:100px;}
</style>
<script>
    function VoteCheck() {
        var validator = $("#form1").validate({ meta: "validate" });
        return validator.form();
    }
    $(function () {
        //上一个月
        $("#DateBody .premonth").click(function () {
            $("#PreMonth_B").click();
        });
        //下一个月
        $("#DateBody .nextmonth").click(function () {
            $("#NextMonth_B").click();
        });
        $("#DateBody .datas").dblclick(function () {
            ShowComDiag("/Mis/Ke/DailyPlan_Add.aspx?TypeID=<%=TypeID %>", "添加日程");
        });
        //日程列表
        var type = '<%=TypeID %>', userid = "<%=UserID %>";
        $("#sublist [data-uid='" + userid + "'][data-type='" + type + "']").addClass("sub_active");
        $("#sublist li").click(function () {
            window.location.href = "DailyPlan.aspx?userid=" + $(this).data('uid') + "&type=" + $(this).data("type");
        });
        //最近日程点击事件
        $("#lastlist li").click(function () { ViewDetail($(this).data('id')); });
    });
    //日程详情弹窗
    function ViewDetail(id) {
        ShowComDiag("DailyDetail.aspx?ID=" + id, "日程详情");
    }
    //关闭弹窗
    function HideMe() {
        CloseComDiag();
    }
    function HideTypeDialog(){
        HideMe();
        location.href = "DailyPlan.aspx";
    }
    function UpdateData(id, content) {
        $(".td_content_div[data-id='" + id + "'] a").text(content);
        $("#lastlist [data-id='" + id + "'] .last_title").text(content);
    }
    function DelData(id) {
        $(".td_content_div[data-id='" + id + "']").remove();
        $("#lastlist [data-id='" + id + "']").remove();
    }
    //添加日程类型
    function ShowTypeDiag() {
        ShowComDiag("DailyType_Add.aspx","日程类别");
    }
    //修改日程类型
    function ShowEditType(id, title) {
        ShowComDiag("DailyType_Add.aspx?id=" + id, title);
    }
    setactive("日程");
</script> 
</asp:Content>

