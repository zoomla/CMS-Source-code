<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchConfig.aspx.cs" Inherits="ZoomLaCMS.MIS.Ke.SchConfig"  MasterPageFile="~/Common/Master/User.Master"%>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<title>基本设置</title>
<style type="text/css">
.hid{display:none;}
.content{height:85px; width: 100%;resize:none;text-align:center;border:none;}
.itemhead{height: 30px;line-height: 30px; width: 100%; background-color: rgba(0, 0, 0, 0.50); text-align: right; padding-right: 5px; color:white;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a href="/User/">用户中心</a></li>
        <li><a href="ConfigList.aspx">课程列表</a></li>
        <li class="active">课表管理 <asp:Label runat="server" ID="TermName_L"></asp:Label></li> 
    </ol>
</div>
    <div class="container">
        <style type="text/css">
            .popover {max-width:400px;background:#3B8BD2;}
            /*.subject_div {background-color:#3B8BD2; }*/
            #subject_tb {text-align:center;}
            #subject_tb tr td {cursor:pointer;padding:8px;background:#3B8BD2;}
            #subject_tb tr td:hover {background-color:#2e6da4}
            /*表格*/
            #maintable .flag_td {vertical-align:middle;text-align:center;}
            #courseTable .item {text-align:center;color:#fff;vertical-align:middle;cursor:pointer;height:120px;}
            #courseTable .item.active {background-color:#5bc0de;}
            #courseTable .item:hover {background-color:#5bc0de;}
        </style>
    <table class="table table-bordered" id="op_table" runat="server">
            <tr style="display:none;"><td class="td_m">学校名称:</td>
                <td>
                    <asp:TextBox runat="server" ID="SchoolName_T" CssClass="form-control text_300"/>
                </td></tr>
            <tr><td>课表名称:</td><td>
                <asp:TextBox runat="server" ID="TermName_T" CssClass="form-control text_300" />
                <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="TermName_T" ForeColor="Red" ErrorMessage="名称不能为空" />
                              </td></tr>
            <tr><td>班级共享:</td><td>
                <asp:Repeater runat="server" ID="ClassRPT">
                    <ItemTemplate>
                        <label>
                            <input type="checkbox" name="ClassIDS_chk" value="<%#Eval("RoomID") %>" /><%#Eval("RoomName") %></label>
                    </ItemTemplate>
                </asp:Repeater>
                <span runat="server" id="Empty_SP" class="r_green" visible="false">你尚未加入班级</span>
            </td></tr>
            <tr><td>课数配置:</td><td>
                <span>每周天数:</span>
                <asp:DropDownList runat="server" ID="WeekDay_DP" onchange="Render();">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                </asp:DropDownList>
                <span>早读:</span>
                <asp:DropDownList runat="server" ID="PreMoning_DP" onchange="Render()">
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="1" Selected="True">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                </asp:DropDownList>
                <span>上午节数:</span>
                <asp:DropDownList runat="server" ID="Moring_DP" onchange="Render();">
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4" Selected="True">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                </asp:DropDownList>
                <span>下午节数:</span>
                <asp:DropDownList runat="server" ID="Afternoon_DP" onchange="Render();">
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3" Selected="True">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                </asp:DropDownList>
                <span>晚上节数:</span>
                <asp:DropDownList runat="server" ID="Evening_DP" onchange="Render();">
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2" Selected="True">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                </asp:DropDownList>
                </td></tr>
    <%--        <tr><td>学校类型:</td><td>
                <label><input type="radio" name="schoolType_rad" value="小学" />小学</label>
                <label><input type="radio" name="schoolType_rad" value="中学" checked="checked" />中学</label>
                <label><input type="radio" name="schoolType_rad" value="院校" />院校</label>
                <label>安排早晚自学:<asp:CheckBox runat="server" ID="StudySelf_Chk" /></label>
                <label>有自主选修课程:<asp:CheckBox runat="server" ID="SelectCourse_Chk" /></label>
            </td></tr>--%>
        <tr><td></td><td>
             <asp:Button runat="server" ID="Save_Btn" Text="保存" CssClass="btn btn-primary" OnClientClick="return CheckData();" OnClick="Save_Btn_Click" />
        <a href="ConfigList.aspx" class="btn btn-primary">返回</a>
                     </td></tr>
        </table>
    <table id="maintable" class="table table-bordered">
        <thead style="text-align:center;"><tr><td class="td_s"></td><td class="td_m">课程时间</td><td>星期一</td><td>星期二</td><td>星期三</td><td>星期四</td><td>星期五</td><td>星期六</td><td>星期日</td></tr></thead>
        <tbody id="courseTable"></tbody>
    </table>    
</div>
   <asp:HiddenField runat="server" ID="Json_Hid" />
   <asp:HiddenField runat="server" ID="BanEdit_Hid" Value="0" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/ICMS/ZL_EDU.js?v=20160524"></script>
<script src="/JS/SelectCheckBox.js"></script>
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script>
    var table = $("#courseTable");
    var config = {};
    $(function () {
        InitTable();
    })
    function CheckData() {
        return SaveConfig();
    }
</script>
</asp:Content>