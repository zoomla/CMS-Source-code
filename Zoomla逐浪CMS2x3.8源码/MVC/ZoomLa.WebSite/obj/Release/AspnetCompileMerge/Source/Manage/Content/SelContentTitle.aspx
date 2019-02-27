<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelContentTitle.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="ZoomLaCMS.Manage.Content.SelContentTitle" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>选择文件</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container-fluid">
    <div class="input-group text_300" style="margin-left:15px;">
        <asp:TextBox ID="ImgName_T" runat="server" CssClass="form-control text_500" placeholder="标题名"></asp:TextBox>
        <span class="input-group-btn">
            <asp:Button ID="Search_B" CssClass="btn btn-info" OnClick="Search_B_Click" runat="server" Text="搜索"></asp:Button>
            <button type="button" onclick="window.location = location;" class="btn btn-info">刷新</button>
        </span>
    </div>
        </div>
        <div class="container-fluid margin_t5">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                <div class="panel panel-info">
                    <div class="panel-heading">可选内容列表(<span id="check_sp">0</span>/<span id="count_sp">0</span>)</div>
                    <div class="list-group content_list" id="conlist_div">
                        <ZL:ExRepeater ID="Content_RPT" runat="server" PageSize="10" PagePre="<div class='panel-footer text-center'>" PageEnd="</div>">
                            <ItemTemplate>
                                <a id="item_<%#Eval("GeneralID") %>" href="javascript:;" onclick="choose(this)" data-id="<%#Eval("GeneralID") %>" class="list-group-item">
                                    <span class="badge"><span class="fa fa-check"></span></span>
                                    <span class="content"><%#Eval("Title") %></span>
                                </a>
                            </ItemTemplate>
                            <FooterTemplate></FooterTemplate>
                        </ZL:ExRepeater>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                <div class="panel panel-success">
                    <div class="panel-heading">已选择内容<%=Call.GetHelp(111) %></div>
                    <div class="panel panel-body choosebody">
                        <ul class="list-group" id="checklist"></ul>
                    </div>
                    <div class="panel-footer text-center">
                        <button type="button" onclick="ConvertData()" class="btn btn-primary">确定</button>
                        <button type="button" class="btn btn-primary">取消</button>
                    </div>
                </div>
            </div>
        </div>
        <ul class="list-group" id="checkTemp" style="display:none;">
            <li class="list-group-item">
                <a href="javascript:;" onclick="unCheck('@ID')" class="badge"><span class="fa fa-trash-o"></span></a>
                @Title
            </li>
        </ul>
        <asp:HiddenField ID="ids_hid" runat="server" />
        <asp:HiddenField ID="ContentCount_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
#conlist_div .choose .badge {display: block !important;}
#conlist_div .badge {display:none;}
.choosebody {height:400px;overflow-y:auto;}
</style>
<script>
$(function () {
    initCheckList();
    $("#count_sp").text($('#ContentCount_Hid').val())
});
//初始化选中数据
function initCheckList() {
    $("#conlist_div a").removeClass("choose");
    $("#checklist").html("");//清空已选内容表里的数据
    var arr = $("#ids_hid").val().split(',');//读取已选择的ids数组 格式为:",id"
    var checkcout = 0;//计算已选数量
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].trim() != "") {
            var $item = $("#item_" + arr[i]);
            $item.addClass("choose")
            $("#checklist").append( $("#checkTemp").html().replace('@ID', $item.data("id")).replace('@Title', $item.text()));//向已选择列表添加一项数据
            checkcout++;
        }
    }
    $("#check_sp").text(checkcout);
}
//点击选中操作
function choose(obj) {
    $obj = $(obj);
    $hid = $("#ids_hid");
    var id = $obj.data("id");
    var title = $obj.text();
    if ($obj.hasClass("choose")) {
        $obj.removeClass("choose");
        $hid.val($hid.val().replace("," + id + ",", ""));
    }
    else {
        $obj.addClass("choose");
        $hid.val($hid.val() + "," + id + ",");
    }
    initCheckList();
}
//已选择列表里的删除操作
function unCheck(id) {
    $("#ids_hid").val($("#ids_hid").val().replace(',' +id + ',', ''));
    initCheckList();
}
//将ids转换为",id,"的格式,并发送到父页面
function ConvertData() {
    var attr = $('#ids_hid').val().split(',');
    var tempdata = "";
    for (var i = 0; i < attr.length; i++) {
        if (attr[i].trim() != "") {
            tempdata += ',' + attr[i].split('|')[0] + ',';
        }
    } 
    parent.PageCallBack('SelContent',tempdata,null);
}
</script>
</asp:Content>
