<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTask.aspx.cs" Inherits="ZoomLaCMS.Plat.Task.AddTask" MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>任务管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
    <div class="child_head"><span class="child_head_span1"></span><span class="child_head_span2">任务管理</span></div>
    <table class="table table-bordered">
        <tr>
            <td class="td_m">任务名称：</td>
            <td>
                <ZL:TextBox runat="server" ID="TaskName_T" class="form-control text_300 required" AllowEmpty="false" />
                <span id="AddSpanColor" class="colorSpan_F White" style="top: 10px;"><span class="colorSpan"></span></span></td>
        </tr>
        <tr>
            <td>截止日期：</td>
            <td>
                <asp:TextBox runat="server" ID="EndTime_T" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm'});" CssClass="form-control required date  text_300" /></td>
        </tr>
        <tr>
            <td>任务颜色：</td>
            <td>
                <span class="colorSpan_F White" onclick="SaveColor('White');"><span class="colorSpan"></span></span>
                <span class="colorSpan_F SkyBlue" onclick="SaveColor('SkyBlue');"><span class="colorSpan"></span></span>
                <span class="colorSpan_F Purple" onclick="SaveColor('Purple');"><span class="colorSpan"></span></span>
                <span class="colorSpan_F Pink" onclick="SaveColor('Pink');"><span class="colorSpan"></span></span>
                <span class="colorSpan_F StoneYellow" onclick="SaveColor('StoneYellow');"><span class="colorSpan"></span></span>
                <span class="colorSpan_F BrightYellow" onclick="SaveColor('BrightYellow');"><span class="colorSpan"></span></span>
                <asp:HiddenField runat="server" ID="AddColor_Hid" Value="White" />
            </td>
        </tr>
        <tr>
            <td>主负责人：</td>
            <td>
                 <input type="button" value="选择" onclick="user.sel('manage', 'plat');" class="btn btn-info" />
                 <table class="table table-bordered table-striped margin_t5" style="width:500px;">
                    <thead><tr><td>ID</td><td>用户名</td><td>操作</td></tr></thead>
                    <tbody id="manage_body"></tbody>
                 </table>
                <asp:HiddenField runat="server" ID="manage_hid" />
            </td>
        </tr>
        <tr>
            <td>参与成员：</td>
            <td>
                 <input type="button" value="选择" onclick="user.sel('member', 'plat');" class="btn btn-info" />
                 <table class="table table-bordered table-striped margin_t5" style="width:500px;">
                    <thead><tr><td>ID</td><td>用户名</td><td>操作</td></tr></thead>
                    <tbody id="member_body"></tbody>
                 </table>
                <asp:HiddenField runat="server" ID="member_hid" />
            </td>
        </tr>
        <tr>
            <td>任务详情：</td>
            <td>
                <asp:TextBox runat="server" ID="TaskContent_T" TextMode="MultiLine" CssClass="form-control m715-50" Style="height: 120px;" /></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button runat="server" ID="TaskAdd_Btn" Text="保存信息" OnClick="TaskAdd_Btn_Click" CssClass="btn btn-info" />
                <a href="Default.aspx" class="btn btn-default">取消保存</a>
            </td>
        </tr>
    </table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/SelectCheckBox.js"></script>
<script src="/JS/jquery.validate.min.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<style type="text/css">
.iconDiv{width: 40px;height: 40px;border-radius: 50%;background-color: gray;text-align: center;cursor: pointer;float: left;margin-right: 5px;background-color: #1796BF;}
.iconDiv:hover{background-color: #0066cc;}
.iconDiv .active{background-color: #0066cc;}
.iconDiv .fa{font-size: 1.5em;line-height: 40px;color: white;}
.colorSpan_F{padding: 2px;display: inline-block;border: 2px solid white;border-radius: 50%;margin-left: 5px;margin-right: 5px;cursor: pointer;position: relative;}
.colorSpan{width: 30px;height: 30px;border-radius: 50%;float: left;}
.colorSpan_F:hover{border: 2px solid yellow;}
.ex_Color{float: right;display: none;border: 1px solid #CCC;border-radius: 5px;background: #ddd;height: 50px;z-index: 4;padding-top: 5px;position: absolute;}
.White .colorSpan{background-color: white;border: 1px solid #ddd;}
.SkyBlue .colorSpan{background-color: #3bb3ff;border: 1px solid #0084ff;}
.Purple .colorSpan{background-color: #9f74ff;border: 1px solid #5d16ff;}
.Pink .colorSpan{background-color: #ff7c7c;border: 1px solid red;}
.StoneYellow .colorSpan{background-color: #ffc85d;border: 1px solid #ff9600;}
.BrightYellow .colorSpan{background-color: #fff65f;border: 1px solid #eee100;}
</style>
<script type="text/javascript">
//$(function () {
//    $(".colorSpan_F").click(function () {
//        var color = $(this).attr("data-color");
//        var id = $(this).parent().attr("data-id").replace(" ", "");
//        PostChangeColor(id, color);
//    });
    //});
    $(function () {
        SaveColor($("#AddColor_Hid").val());
    })
    function SaveColor(color) {
        if (ZL_Regex.isEmpty(color)) { return; }
        $("#AddSpanColor").attr("class", "colorSpan_F " + color);
        $("#AddColor_Hid").val(color);
    }
    var manage = { select: "manage", list: [], $db: $("#manage_hid"), $body: $("#manage_body"), tlp: '<tr id="tr_@UserID"><td>@UserID</td><td>@UserName</td><td><a href="javascript:;" onclick="manage.del(@UserID);" title="删除"><i class="fa fa-remove"></a></td></tr>' };
    var member = { select: "member", list: [], $db: $("#member_hid"), $body: $("#member_body"), tlp: '<tr id="tr_@UserID"><td>@UserID</td><td>@UserName</td><td><a href="javascript:;" onclick="member.del(@UserID);" title="删除"><i class="fa fa-remove"></a></td></tr>' };

    manage.init = function () {
        var ref = this;
        var val = ref.$db.val();
        if (val && val != "" && val != "[]") { ref.list = JSON.parse(val); ref.$db.val(ref.list.GetIDS("UserID")); ref.render(); }
        user.hook[ref.select] = function (list, select) {
            ref.list.addAll(list, "UserID");
            console.log(ref.list);
            ref.render();
            //同时添加到组成员
            member.list.addAll(list, "UserID");
            member.render();
            CloseComDiag();
        }
    }
    manage.render = function () {
        var ref = this;
        var $items = JsonHelper.FillItem(ref.tlp, ref.list, null);
        ref.$body.html("").append($items);
        ref.$db.val(ref.list.GetIDS("UserID"));
    }
    manage.del = function (uid) {
        var ref = this;
        ref.$body.find("#tr_" + uid).remove();
        ref.list.RemoveByID(uid, "UserID");
        ref.$db.val(manage.list.GetIDS("UserID"));
    }
    //-----------------------------
    member.init = function () {
        var ref = this;
        var val = ref.$db.val();
        console.log(val);
        if (val && val != "" && val != "[]") { ref.list = JSON.parse(val); ref.render(); }
        user.hook[ref.select] = function (list, select) {
            ref.list.addAll(list, "UserID");
            ref.render();
            CloseComDiag();
        }
    }
    member.render = function () {
        var ref = this;
        var $items = JsonHelper.FillItem(ref.tlp, ref.list, null);
        ref.$body.html("").append($items);
        ref.$db.val(ref.list.GetIDS("UserID"));
    }
    member.del = function (uid) {
        var ref = this;
        ref.$body.find("#tr_" + uid).remove();
        ref.list.RemoveByID(uid, "UserID");
        ref.$db.val(member.list.GetIDS("UserID"));
        //若是管理员，也删除
        manage.del(uid);
    }
    $(function () {
        manage.init();
        member.init();
    });
</script>
</asp:Content>