<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="CollectionStatus.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.Collect.CollectionStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"><title>采集状态</title></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
<div id="log_div" style="height:500px;border:1px solid #ddd;overflow-y:auto;"></div>
<div class="text-center margin_t5">
    <input type="button" value="停止采集" class="btn btn-warning" onclick="stopWork();" />
</div>
<div style="border: 1px solid #ddd; margin-top: 10px;">
    <label>[提示] 系统已启动多线程模式，您可以离开本页面继续其他操作，采集任务将自动完成...<br /></label>
    <label>帮助说明：采集完成后,会返回以下三种状态</label><br />
    <span>信息为空：网址不正确,或目标页面不存在</span><br />
    <span>采集成功：正常采集,并添加入数据库</span><br />
    <span>采集异常：取值异常,具体见报错信息</span><br />
</div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
#log_div .item{margin-bottom:3px;}
.item.log0{color:green;}
.item.log1{color:red;}
.item.log99{color:#044c92;}
</style>
<script src="/JS/Controls/ZL_Array.js"></script>
<script>
$(function () {
    setInterval(function () {
        $.post("CollectionStatus.aspx?action=getlog", {}, function (data) {
            if (data != "") {
                var list = JSON.parse(data);
                for (var i = 0; i < list.length; i++) {
                    list[i].cdate = list[i].cdate.replace("T", " ").split('.')[0];
                }
                var tlp = '<div class="item log@logType">[@cdate] @msg</div>';
                var $items = JsonHelper.FillItem(tlp, list);
                $("#log_div").append($items);
            }
        });
    }, 1000);
})
function stopWork() {
    if (!confirm("确定要停止采集工作吗")) { return; }
    $.post("CollectionStatus.aspx?action=stop", {}, function () { });
    alert("已向服务端发送停止信号,请等待正在处理的记录完成");
}
</script>
</asp:Content>