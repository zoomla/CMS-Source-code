<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyPlan_Add.aspx.cs" Inherits="ZoomLaCMS.MIS.Ke.DailyPlan_Add" MasterPageFile="~/Plat/Empty.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="Head"><title>添加日程</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="addsubject_div">
    <table id="Add_Table" class="table">
      <tr>
        <td class="text-right td_m"><span><span style="color: red; margin-left: 1em;">*</span>日程名称:</span></td>
        <td><asp:TextBox runat="server" type="text" ID="Name_T" class="form-control text_300 day_text" ></asp:TextBox></td>
      </tr>
      <tr>
        <td class="text-right"><span><span style="color: red; margin-left: 1em; text-decoration: none;">*</span>开始时间:</span></td>
        <td> 
            <div class="input-group" style="width: 400px;">
                <asp:TextBox class="form-control text_x day_text formdate" ID="txtBeginTime" runat="server"></asp:TextBox>
                <select name="startHour" class="date_hour form-control text_x"></select>
                <select name="startMinitue" class="date_minitue form-control text_x"></select>
            </div>
            <asp:HiddenField ID="StartDate_Hid" runat="server" />
        </td>
      </tr>
    <tr>
        <td class="text-right">
            <span><span style="color: red; margin-left: 1em; text-decoration: none;">*</span>结束时间:</span>
        </td>
        <td>
            <div class="input-group" style="width:400px;">
                <asp:TextBox class="form-control text_x day_text formdate" ID="txtEndTime" runat="server"></asp:TextBox>
                <select name="endHour" class="date_hour form-control text_x"></select>
                <select name="endMinitue" class="date_minitue form-control text_x"></select>
            </div>
            <asp:HiddenField ID="EndDate_Hid" runat="server" />
        </td>
    </tr>
      <tr>
        <td class="text-right"><span>日程描述:</span></td>
        <td><textarea class="form-control day_text" style="width: 75%; height: 8em;" name="describe" placeholder="点击此处添加日程描述"></textarea></td>
      </tr>
      <tr>
          <td colspan="2" style="text-align: center;">
              <asp:Button ID="Add_Btn" runat="server" Text="提交" CssClass="btn btn-primary" OnClientClick="return SetData()" OnClick="BtnAdd_Click" ValidationGroup="Add" />
              <input type="button" class="btn btn-default" onclick="closeDiag();" value="取消" /></td>
      </tr>
      <tr>
        <td></td>
        <td></td>
      </tr>
    </table>
</div>
<div id="addtype_div" style="display:none;">
    <div class="input-group text_300">
    <asp:TextBox ID="Type_T" CssClass="form-control" placeholder="日程名" Text="" runat="server"></asp:TextBox>
        <asp:HiddenField ID="TypeID_Hid" runat="server" />
        <span class="input-group-btn">
            <asp:Button ID="AddType_B" runat="server" OnClick="AddType_B_Click" Text="确定" OnClientClick="return CheckTypeData()" CssClass="btn btn-primary" />
        </span>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<link type="text/css" rel="stylesheet" href="/JS/Plugs/date/bootstrap-datetimepicker.css" />
<script src="/JS/Plugs/date/bootstrap-datetimepicker.js"></script>
<script>
$(function () {
    InitDateData();
    $(".formdate").datetimepicker({
        format: "yyyy-mm-dd",
        language: "zh-CN",
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        startView: 2,
        minView: 2,
        maxView: 2,
        initialDate: new Date()
    })
    //-----默认时间
    var now = new Date();
    var year = now.getUTCFullYear();
    var month = now.getMonth() + 1;
    var day = now.getDate();
    if (month < 10) { month = "0" + month; }
    if (day < 10) { day = "0" + day;}
    $(".formdate").val(year+"-"+month+"-"+day);
    $(".date_hour").val(now.getHours());
})
//初始化时间数据
function InitDateData() {
    var hourstr = "";
    for (var i = 0; i < 24; i++) {//初始化小时
        hourstr += "<option>" + i + "</option>";
    }
    $(".date_hour").html(hourstr);
    var minites = "";
    var startvalue = -5;//初始分钟
    for (var i = 0; i < 12; i++) {
        startvalue += 5;
        var tempdata = startvalue;
        if (tempdata < 10)
            tempdata = "0" + tempdata;//强制两位数
        minites += "<option>" + tempdata + "</option>";
    }
    $(".date_minitue").html(minites);
}
function closeDiag() {
    parent.CloseComDiag();
}
function refresh()
{
    closeDiag();
    parent.location = parent.location;
}
//替换开始时间与结束时间格式
function SetData() {
    if ($("#Name_T").val() == "") {
        alert('日程名称不能为空!');
        return false;
    }
    if ($("#txtBeginTime").val() == "" || $("#txtEndTime").val() == "") {
        alert('开始时间或结束时间不能为空!');
        return false;
    }
    if ($("[name='place']").val() == "") {
        alert("日程类别不能为空!")
        return false;
    }
    var startdate = $("#txtBeginTime").val() + " " + $("select[name='startHour']").val() + ":" + $("select[name='startMinitue']").val();//开始时间
    var enddate = $("#txtEndTime").val() + " " + $("select[name='endHour']").val() + ":" + $("select[name='endMinitue']").val();//结束时间
    if (Date.parse(startdate.replace('-', '/')) > Date.parse(enddate.replace('-', '/'))) {
        alert("开始时间不能早于结束时间!");
        //$("select[name='endMinitue']").next().show();//时间错误提示
        return false;
    }
    $("select[name='endMinitue']").next().hide();
    return true;
}
</script>
</asp:Content>