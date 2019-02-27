<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DownMail.aspx.cs" Inherits="Plat_EMail_ReceiveMail" MasterPageFile="~/Plat/Empty.master" EnableViewState="false" %>
<asp:Content runat="server" ContentPlaceHolderID="Head"><title>接收邮件</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div style="width: 500px;margin: 0 auto;">
    <div id="prog_sp">正在接收来自:<span id="email_t" class="r_red"></span>的邮件<span>共有:</span><span id="total_t" class="r_red">0</span>封,当前正在接收第<span id="index_t" class="r_red">0</span>封</div>
    <div class="progress margin_t5" id="prog_wrap">
        <div class="progress-bar progress-bar-success progress-bar-striped" role="progressbar"><span class="sr-only" style="position: relative;"></span></div>
    </div>
    <div id="remind_sp" class="r_red"></div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
var interal;
function BeginCheck() { interal = setInterval(GetProgress, 1000); }
function GetProgress() {
    $.post("", { action: "getprogress" }, function (data) {
        //没有任务存在
        if (data == "-1") { $("#remind_sp").text("当前没有下载邮件任务"); }
        else {
            var model = JSON.parse(data);
            $("#email_t").text(model.email);
            $("#total_t").text(model.total);
            $("#index_t").text(model.index);
            if (model.iscomplete === true) { prog.set(100); clearInterval(interal); $("#remind_sp").text("邮件已经下载完成,请刷新邮件列表页"); }
            else { prog.set((model.index / model.total) * 100); }
        }
    })
}
var prog = {
    $obj: $('#prog_wrap'),
    set: function (num, txt) {
        var ref = this;
        num = parseFloat(num).toFixed(2);
        if (!num || isNaN(num)) { console.log("进度值错误," + num); }
        num = num + "%";
        if (!txt) { txt =  num; }
        ref.$obj.find(".progress-bar").css("width", num);
        ref.$obj.find(".sr-only").text(txt);
    }
};
</script>
</asp:Content>