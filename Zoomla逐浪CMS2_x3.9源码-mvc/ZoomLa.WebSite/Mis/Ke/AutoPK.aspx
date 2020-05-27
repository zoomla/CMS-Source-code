<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoPK.aspx.cs" Inherits="ZoomLaCMS.MIS.Ke.AutoPK" MasterPageFile="~/User/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
      .table>tbody>tr>td{padding:0px;}
      .item{cursor:pointer;height:120px;}
      .hid{display:none;}
      .content{height:85px; width: 100%;resize:none;text-align:center;border:none;}
      .itemhead{height: 30px;line-height: 30px; width: 100%; background-color: rgba(0, 0, 0, 0.50); text-align: right; padding-right: 5px; color:white;}
        /*班级*/
        #classul li{float:left;margin-left:5px;}
    </style>
    <title>自动排课</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a href="/User/">用户中心</a></li>
        <li><a href="ConfigList.aspx">排课配置</a></li>
        <li class="active">自动排课</li> 
    </ol>
</div>
<div class="container btn_green">
    <div class="panel panel-danger">
        <div class="panel-heading"><span class="fa fa-user"></span><span class="margin_l5">筛选条件</span></div>
        <div class="panel-body">
            <div id="Set_Div" style="display:inline;" runat="server">
            <asp:DropDownList ID="SchList_Radio" CssClass="form-control text_md" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SchList_Radio_SelectedIndexChanged" DataTextField="SchoolName" DataValueField="ID">
            </asp:DropDownList>
            <select name="class_rad" class="form-control text_md">
                <option value="-1">请选择班级</option>
                 <asp:Repeater runat="server" ID="ClassRPT">
                    <ItemTemplate>
                         <option value="<%#Eval("GroupID") %>"><%#Eval("Regulationame")%></option>
                    </ItemTemplate>
                </asp:Repeater>
            </select>
            
             时期:
            <asp:TextBox ID="SDate_T" CssClass="form-control text_md td_l" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' });" runat="server"></asp:TextBox> - <asp:TextBox ID="EDate_T" runat="server" CssClass="form-control text_md td_l" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' });"></asp:TextBox>
        
            <button type="button" class="btn btn-primary" onclick="ShowInfo()">查询班级课表</button>
            </div>
            <div id="Sel_Div" style="display:inline;" runat="server" visible="false">
                <asp:RadioButtonList id="DateList_R" AutoPostBack="true" OnSelectedIndexChanged="SeachDate_B_Click" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList>
            </div>
        </div>
    </div>
    
    
    <table class="table table-bordered table-striped margin_t10">
        <tr><td>ID</td><td>教师名</td><td>学科</td><td>职位</td><td>排课</td></tr>
        <ZL:ExRepeater runat="server" ID="Teacher_RPT" PageSize="10" PagePre="<tr><td colspan='5'>" PageEnd="</tr></td>">
            <ItemTemplate>
                <tr>
                    <td class="td_s">
                        <label><input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /><%#Eval("ID") %></label>
                    </td>
                    <td><span id="tname_<%#Eval("ID") %>"><%#Eval("TName")%></span></td>
                    <td><%#Eval("Post") %></td>
                    <td><%#Eval("Teach")%></td>
                    <td>
                        <select id="pktype_<%#Eval("ID") %>"><option value="everyday">每天必排</option><option value="everyweek">每周必排</option></select>
                        <input id="pknum_<%#Eval("id") %>" type="text" value="2" class="text_xs margin_l5" /><span>节课</span>
                    </td>
                </tr>
            </ItemTemplate>
        </ZL:ExRepeater>
        <tr>
            <td colspan="5" style="padding:5px;">
                <input type="button" class="btn btn-primary" value="开始排课" onclick="BeginPK();" />
                <asp:Button runat="server" ID="Save_Btn" CssClass="btn btn-primary" Text="保存" OnClick="Save_Btn_Click" OnClientClick="BeginSave();" /></td>
        </tr>
    </table>
    <table id="maintable" class="table table-bordered">
        <thead style="text-align:center;"><tr><td class="td_s"></td><td>星期一</td><td>星期二</td><td>星期三</td><td>星期四</td><td>星期五</td><td>星期六</td><td>星期日</td></tr></thead>
        <tbody id="courseTable"></tbody>
    </table>
    <div id="setteach_div" style="display:none;">
        <div class="text-center">
            <asp:DropDownList ID="TeacList_Dr" runat="server" DataTextField="TName" DataValueField="ID" CssClass="form-control text_md"></asp:DropDownList>
            <asp:TextBox ID="Date_T" runat="server" CssClass="form-control text_md" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' });" placeholder="代课日期"></asp:TextBox>
            <asp:HiddenField ID="Teach_Hid" runat="server" />
            <asp:HiddenField ID="Time_Hid" runat="server" />
        </div>
        <div class="text-center margin_t5">
            <asp:Button ID="SaveD_T" runat="server" OnClientClick="return SaveTeach()" OnClick="SaveD_T_Click" CssClass="btn btn-primary" Text="保存" />
            <button type="button" onclick="CloseDiag()" class="btn btn-primary">取消</button>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="Json_Hid" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/ICMS/ZL_EDU.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        var table = $("#courseTable");
        var config = {};
        var items = {};//单元格Json
        if($("#Json_Hid").val() != ""){
            config = JSON.parse($("#Json_Hid").val());
            items = JSON.parse(config.items);//单元格Json
        }
        var jsontd = { time: "", day: "", num: "", disabled: "", text: "" };
        var diag = new ZL_Dialog();
        $(function () {
            InitTable();
            if (items[0]&&items[0].text!="") {
                $(".itemhead").append(" <span class='fa fa-user setteach' title='代理设置'></span>");
                $(".setteach").click(function () {//代理教师操作
                    diag.title = "设置代课教师";
                    diag.content = "setteach_div";
                    diag.ShowModal();
                    $item = $(this).closest("td");
                    $("#Teach_Hid").val($item.find(".content").val());
                    $("#Time_Hid").val($item.data("time") + "," + $item.data("day") + "," + $item.data("num"));//保存课程位置信息
                });
            }
        })
        
        function CloseDiag() {
            diag.CloseModal();
        }
        function SaveTeach() {//保存代课教师信息
            var datas = $("#Time_Hid").val().split(',');//[0]:time; [1]:day; [2]:num
            for (var i = 0; i < items.length; i++) {
                if (datas[0]==items[i].time&&datas[1]==items[i].day&&datas[2]==items[i].num) {
                    items[i].text = " (代课教师:" + $("#TeacList_Dr option:checked").text() + ")";
                    break;
                }
            }
            config.items = JSON.stringify(items);
            $("#Json_Hid").val(JSON.stringify(config));
            return true;
        }
        function BeginPK() {
            //检测配置项是否齐全
            $idchk = $("input[name=idchk]:checked");
            if ($idchk.length < 1) {
                alert("请先选定需要排课的教师"); return false;
            }
            //开始读取配置
            var techArr = [];
            for (var i = 0; i < $idchk.length; i++) {
                var tech = { id: "", tname: "", pktype: "", pknum: "" };//ID,排课选项,课数
                tech.id = $idchk[i].value;
                tech.tname = $("#tname_" + tech.id).text();
                tech.pktype = $("#pktype_" + tech.id).val();
                tech.pknum = $("#pknum_" + tech.id).val();
                techArr.push(tech);
            }
            //根据基本配置和教师配置,开始生成排课
            //先暂只实现每天必排,其优先以高,再实现每周必排,如此循环
            if ($("[name=class_rad] option:checked").val() < 1) { alert("请先选定班级"); return false; }
            else
            {
                config.class = $("[name=class_rad] option:checked").val();
            }
            items = JSON.parse(config.items);//初始化
            for (var i = 0; i <= config.weekday ; i++) {
                itemday = items.GetDayItem(i); //从第一天开始排
                FinishDayPK(itemday, techArr);//完成排课
            }
            FinishWeekPK(items, techArr);
            InitTable();
        }
        //完成一天的排课,用于每天必排
        function FinishDayPK(itemday, techArr) {
            for (var i = 0; i < techArr.length; i++) {
                PushDayPK(itemday, techArr[i]);
            }
        }
        function PushDayPK(itemday, tech) {
            if (tech.pktype == "everyday") {
                var index = 0;
                for (var i = 0; i < itemday.length && index < tech.pknum ; i++) {
                    if (itemday[i].text != "") continue;//被禁用或已安排,则不处理,跳到下一次
                    else { itemday[i].text = tech.tname; index++; }
                }
            }
        }
        //用于每周必排
        function FinishWeekPK(items, techArr) {
            for (var i = 0; i < techArr.length; i++) {
                PushWeekPK(items, techArr[i]);
            }
        }
        function PushWeekPK(items, tech) {
            if (tech.pktype == "everyweek") {
                var index = 0;
                for (var i = 0; i < items.length && index < tech.pknum ; i++) {
                    if (items[i].text != "") continue;//被禁用或已安排,则不处理,跳到下一次
                    else { items[i].text = tech.tname; index++; }
                }
            }
        }
        function BeginSave()
        {
            config.items = JSON.stringify(items);
            $("#Json_Hid").val(JSON.stringify(config));
            return true;
        }
        //-----------------------
        function SelRad(val) {
            $("[name=class_rad] option[value=" + val + "]")[0].checked = true;
        }
        function SelChk(vals) {
            var arr = vals.split(',');
            for (var i = 0; i < arr.length; i++) {
                $("input[name=idchk][value=" + arr[i] + "]")[0].checked = true;
            }
        }
        var infodiag = new ZL_Dialog();
        function ShowInfo() {
            if ($("[name=class_rad] option:checked").length < 1) { alert("请先选定班级"); return; }
            infodiag.title = "班级时期表";
            infodiag.url = "ClassInfo.aspx?id=" + $("[name=class_rad] option:checked").val();
            infodiag.reload = true;
            infodiag.ShowModal();
        }
        function CloseInfo() {
            infodiag.CloseModal();
        }

    </script>
</asp:Content>