//博客
function Blog_Chk() {
    $li = $(event.srcElement).parent("li");
    var chk = $li.find(":checkbox")[0];
    chk.checked = !chk.checked;
    setCookie("plat_" + chk.value, chk.checked)
    switch (chk.value) {
        case "sina":
            $li.find("#weibo_icon").css("color", chk.checked ? "#0AA4E7" : "#ccc");
            break;
        case "qqblog":
            $li.find("#qq_icon").css("color", chk.checked ? "#0AA4E7" : "#ccc");
            break;
    }
}
function Blog_StatusChk() {
    var haschk = getCookie("plat_qqblog");
    if (haschk && haschk == "true" && $("#qqblog_li").length > 0) {
        $("#qqblog_li").find("#qq_icon").css("color", "#0AA4E7");
        $("#qqblog_li").find(":checkbox")[0].checked = true;
    }
    haschk = getCookie("plat_sina");
    if (haschk && haschk == "true" && $("#sina_li").length > 0) {
        $("#sina_li").find("#weibo_icon").css("color", "#0AA4E7");
        $("#sina_li").find(":checkbox")[0].checked = true;
    }
}
//站内邮(仅能发送给本公司部门同事)
function PrivateSend() {
    var msgjson = { msg: UE.getEditor("MailContent").getContent(), receuser: $("#ReceUser_Hid").val(), action: "privatesend" };
    if (msgjson.msg == "") { alert("内容不能为空"); return false; }
    if (msgjson.receuser == "") { alert("收信人不能为空"); return false; }
    UE.getEditor("MailContent").setContent(""); document.getElementById("prvatesend_btn").disabled = "disabled";
    $.post("/Plat/Common/Common.ashx", msgjson, function (data) { PrivateCallBack(data); }, "json");
}
//发送成功或失败后,启用发送按钮
function PrivateCallBack(data) {
    document.getElementById("prvatesend_btn").disabled = "";
    switch (data) {
        case "-1":
            alert("发送失败");
            break;
        case "0":
            alert("请先登录");
            break;
        default:
            alert("发送成功!");
            PrivateClose();
            break;
    }
}
function PrivateOpen(uid, uname) {
    $("#ReceUser_Hid").val(uid);
    $("#ReceUser").val(uname);
    $("#privatediv").modal({});
}
//$("#privatediv").modal({});
function PrivateClose() {
    $("#ReceUser").val(""); $("#ReceUser_Hid").val("");
    UE.getEditor("MailContent").setContent("");
    $("#privatediv").modal("hide");
}
//参数,divid,iframeid,#后参数,#后参数对应的txt与hid
function PrivateSelUser(select) {
    $("#User_IFrame").attr("src", "/Plat/Common/SelUser.aspx?Type=AllInfo#" + select);
    //$("#User_IFrame")[0].contentWindow.ClearChk();
    $("#seluserdiv").show();
}
function UserFunc(json, select) {
    var uname = "";
    var uid = "";
    for (var i = 0; i < json.length; i++) {
        uname += json[i].UserName + ",";
        uid += json[i].UserID + ",";
    }
    if (uid) uid = uid.substring(0, uid.length - 1);
    switch (select)//#后带的参数
    {
        case "private":
            $("#ReceUser").val(uname);
            $("#ReceUser_Hid").val(uid);
            break;
        case "atuser"://@user
            for (var i = 0; i < json.length; i++) {
                var tlp = "@" + json[i].UserName + "[uid:" + json[i].UserID + "]";
                var text = $("#MsgContent_T").val();
                $("#MsgContent_T").val(text + tlp + " ");
            }
            CloseComDiag();
            break;
    }
    $("#seluserdiv").hide();
}
//私信聊天
function ChatShow(id, uname) {
    //$("#chatdiv").css("bottom", "0px");
    //$("#chatdiv").show();
    //if (id > 0)
    //    $("#chat_if")[0].contentWindow.ChangeTalker(id, uname);
    window.open("/Common/Chat/Chat.aspx?uid=" + id);
}
function ChatClose() {
    $("#chatdiv").hide();
}
function AddAT(uname, uid) {
    var $msg = $("#MsgContent_T");
    var v = $msg.val();
    var at = "@" + uname + "[uid:" + uid + "]";
    if (v.indexOf(at) < 0) { $msg.val(v + at); }
    $msg.focus();
}
//--------------------------------
function ArtColl() {
    $(event.srcElement).parent().parent().hide().parent().find(".detail_div").show();
}
function ArtUnfold() {
    $(event.srcElement).parent().parent().hide().parent().find(".synposis_div").show();
}
function InitAt($div) {
    if (!atlist || atlist == "" || atlist.length < 1) { console.log("无数据,取消at"); return; }
    if (!$div) { $div = $(".atwho"); } else { $div = $div.find(".atwho"); }
    $div.atwho({
        tpl: '<li data-value="${atwho-at}${name}${suffix}"><img src="${imageUrl}" onerror="this.src=\'/Images/userface/noface.png\'" style="width:25px;height:25px;" />&nbsp;${name}</li>',
        at: "@",
        search_key: "name",
        title: "请选择要@的同事名称",
        data: atlist,
        limit: 8,
        max_len: 20,
        start_with_space: false,
        //data:jsonArr,
        callbacks: {
            remote_filter: function (query, callback)//@之后的语句
            {
                //callback(json);
            }
        }
    });
}
function GetMsgMainID(id) {
    return "msgitem-" + id;
}
function DataChk() {
    var val = $("#MsgContent_T").val();
    if (val == "" || val.replace(/ /g, "") == "") {
        TextAlert("MsgContent_T", 3);
        return false;
    }
    else {
        window.localStorage.PlatMsg = "";
        //disBtn(document.getElementById("Share_Btn"), 2000); 
        //disBtn(document.getElementById("Share_Btn2"), 2000);
    }
    return true;
}
//背景色警告
function TextAlert(id, time) {
    if (!time || time < 1) time = 3;
    for (var i = 0, span = 200; i < time; i++) {
        setTimeout(function () { $("#" + id).css("background-color", "#f9f2f4") }, span);
        span += 200;
        setTimeout(function () { $("#" + id).css("background-color", "#fff") }, span);
        span += 200;
    }
}
function DisReply() {
    $(event.srcElement).parent().parent().parent().find(".reply").show("middle");
}
function DisReplyOP(pid, rid, uname) {
    //$(event.srcElement).parent().siblings(".replyOP").show();
    $("#reply_div_" + pid).show();
    $("#MsgContent_" + pid).val("").attr("placeholder", "回复 " + uname + ":");
    $("#Reply_Rid_Hid_" + pid).val(rid);
}
//-------
function PreView(vpath) {
    $("#Model_Btn").click();
    if (vpath != $("#preview_down_a").attr("href"))//如果预览的文件变更，则重新加载
    {
        $("#preview_down_a").attr("href", vpath);
        $("#preview_if").attr("src", "/PreView.aspx?vpath=" + escape(vpath));
        $("#largepre_a").attr('href', "/PreView.aspx?vpath=" + escape(vpath));
    }
}
function LoadReply(pid, pageSize, pageIndex) {
    $("#reply_" + pid).load("/Plat/Blog/ReplyList.aspx?code=" + Math.random() + "&pid=" + pid + "&PageSize=" + pageSize + "&PageIndex=" + pageIndex + " start");
}
function ClearChk(name) {
    $("input[name='GroupIDS_Chk']").each(function () { this.checked = false; });
}
//--------------投票相关JS
function MsgTypeFunc(css) {
    var s = ".tab1,.tab2";
    $(s).hide();
    $(css).show();
    $("#child_nav_ul a").each(function () {
        if ($(this).attr("data-type") == css) {
            $(this).addClass("active");
        }
        else { $(this).removeClass("active"); }
    });
}
function AddVoteOP() {
    var index = parseInt($(".vote_op_label").last().text().replace(".", "")) + 1;
    var tr = "<tr class='votetr'><td></td><td><label class='vote_op_label'>" + index + ".</label><input type='text' name='VoteOption_T' class='vote_op_input form-control' /><span class='fa fa-remove' onclick='RemoveVoteOP();'></span></td></tr>";
    $(".votetr").last().after(tr);
}
function RemoveVoteOP() {
    $(".votetr").last().remove();
}
function VoteCheck() {
    var validator = $("#form1").validate({ meta: "validate" });
    return validator.form();
}
//this,与控件参数
function ShowMsgDiv(id, args1, arsg2) {
    var parent = $("#" + GetMsgMainID(id));
    $(parent).find(args1).hide();
    $(parent).find(arsg2).show();
}
//转发
function ShowForWard(id) {
    $("#Forward_ID_Hid").val(id);
    var text = " 转发内容：<br />" + $("#" + GetMsgMainID(id)).find(".msg_content_article_div").text();
    $("#forward_his_div").html(text);
    $("#Forward_Btn").click();
}
//话题相关
function ShowDiv(id) {
    var $obj = $("#" + id);
    var flag = $obj.is(":visible");
    $(".msgex").hide();
    switch (id) {
        case "ImgFace_Div":
            if ($("#ImgFace_if").attr("src") == "") {
                $("#ImgFace_if").attr("src", "/Plugins/Ueditor/dialogs/emotion/ImgFace.html");
            }
            break;
            //case "GroupAT_ifr":
            //    //if (!$obj.attr("src") || $obj.attr("src") == "") {
            //    //    $obj.attr("src", "../Common/GroupAT.aspx");
            //    //}
            //    break;
    }
    if (!flag) $obj.show();
}
function ShowGroupAt() {
    ShowComDiag("/Plat/common/selgroup.aspx?source=plat#atuser", "请选择需要@的部门或联系人");
}
function AddTopic() {
    //如果话题中包含str,则不添加str而改为选中,否则添加完成后再选中
    var str = event.srcElement.innerText;
    var id = "#MsgContent_T";
    var index = GetIndexByStr($(id), str);
    if (index > 0) {
        $(id).setSelection(index, index + (str.length - 2));
    }
    else {
        $(id).val($(id).val() + str);
        index = GetIndexByStr($(id), str);
        $(id).setSelection(index, index + (str.length - 2));
    }
    $("#topicDiv").hide();
}
function GetIndexByStr($obj, str) {
    var index = 0;
    if ($obj.val() == "" || $obj.val().length < str.length) {
        return index;
    }
    else return ($obj.val().indexOf(str) + 1);
}
//表情
function InsertSmiley(json) {
    var arr = JSON.parse($("#ImgFace_Hid").val()); arr.push(json);
    $("#ImgFace_Div").hide();
    $("#ImgFace_Hid").val(JSON.stringify(arr));
    $("#MsgContent_T").val($("#MsgContent_T").val() + json.title);
}
//显示用户详情
function ShowUser(uid) {
    $("#ShowUser_Div").show();
    $("#ShowUser_if").attr("src", "/Plat/Common/UserDetail.aspx?ID=" + uid);
}
//哪些组可看见该信息
function CanSeeFun(op) {
    $allChk = $("#viewgroup input[name=GroupIDS_Chk]");
    $edChk = $("#viewgroup input[name=GroupIDS_Chk]:checked");//已选中
    onlymeChk = $("#viewgroup input[name=GOnlyMe_Chk]")[0];
    switch (op) {
        case "all":
            $allChk.each(function () { this.checked = true; });
            $edChk = $("#viewgroup input[name=GroupIDS_Chk]:checked");
            onlymeChk.checked = false;
            break;
        case "single":
            onlymeChk.checked = false;
            break;
        case "onlyme":
            $allChk.each(function () { this.checked = false; });
            break;
    }
    //----------------
    if (onlymeChk.checked) {
        $("#canSee_Span").text("仅自己");
    }
    else if ($allChk.length == $edChk.length || $edChk.length == 0) {
        $("#canSee_Span").text("所有人可见");
    }
    else if ($edChk.length == 1) {
        $("#canSee_Span").text($edChk.attr("data-gname"));
    }
    else {
        $("#canSee_Span").text("已选" + $edChk.length + "项");
    }
}
function GroupAt_Add(json) {
    if (!json || json.length < 1) { return; }
    var text = $("#MsgContent_T").val();
    for (var i = 0; i < json.length; i++) {
        var model = json[i];
        if (!model || !model.gname || !model.gid) { continue; }
        var tlp = "@" + model.gname + "[gid:" + model.gid + "]";
        if (text.indexOf(tlp) > -1) { return; }
        text = text + tlp + " ";
    }
    $("#MsgContent_T").val(text);
}
//----------------AJAX区
function PostDelMsg(msgid) {
    if (confirm("确定要删除该条信息吗!!")) {
        $("#" + GetMsgMainID(msgid)).remove();
        PostToCS("DeleteMsg", msgid, null);
    }
}
//增加自己的头像链接,移除自己的头像链接
function PostLike(id)//点赞
{
    var tlp = "<li title='@uname' data-uid='@uid' class='likeids_li'><a href='javascript:;'><img data-uid='@uid' class='uimg img_xs' src='@uface' onerror='this.error=null;this.src='/Images/userface/noface.png';'/></a></li>", a = "";
    var $main = $("#" + GetMsgMainID(id));
    var uname = $("#UserInfo_Hid").val().split(':')[0];
    var uid = $("#UserInfo_Hid").val().split(':')[2];
    var likeobj = $main.find(".likeids_div_ul").find("li[data-uid='" + uid + "']");
    if (likeobj.length > 0) {
        a = "ReLike";
        likeobj.remove();
        $main.find(".thumbs-o-up").attr("title", "点赞");
    }
    else {
        a = "AddLike";
        var uface = $("#UserInfo_Hid").val().split(':')[1];
        tlp = tlp.replace(/@uname/g, uname).replace(/@uface/g, uface).replace(/@uid/g, uid);
        $main.find(".likeids_div_ul").append(tlp);
        $main.find(".thumbs-o-up").attr("title", "取消赞");
    }
    var num = $main.find(".likeids_li").length;
    $main.find(".likenum_span").html("(" + num + ")");
    if (num > 0) {
        $main.find(".likeids_div_ul:hidden").show("middle");
    }
    else {
        $main.find(".likeids_div_ul").hide("middle");
    }
    PostToCS(a, id, function () { });
}
function PostUserVote(id) {
    var name = "vote_" + id;
    var v = $("input:radio[name='" + name + "']:checked").val();//opid
    if (v) {
        v = id + ":" + v;
        PostToCS("UserVote", v, function (data) { B_Msg.loadVote(id); });
    }
    else { console.log('选项不存在'); }
}
function CollFunc(obj, id)//收藏,取消收藏
{
    if ($(obj).hasClass("colled"))//如已收藏,取消收藏
    {
        $(obj).attr("class", "fa fa-heart-o nocolled");
        PostToCS("ReColl", id, function () { });
    }
    else//加入收藏
    {
        $(obj).attr("class", "fa fa-heart colled");
        PostToCS("AddColl", id, function () { });
    }
}
function AddReply(id) {//回复主信息或子信息
    var msg = $("#MsgContent_" + id).val(); $("#MsgContent_" + id).val("");
    var rid = $("#Reply_Rid_Hid_" + id).val();
    if (msg == "") { alert('信息不能为空!!'); return; }
    var value = id + ":::" + rid + ":::" + msg + ":::" + $("#reply_hid_" + id).val();
    reply.clear(id);
    PostToCS("AddReply", value, function () { LoadReply(id, pageSize, 1); });
}
function AddMessage(id) {//对回复者回复
    var msg = $("#MsgContent_" + id).val();
    var pid = $("#MsgInfo_" + id + "_Hid").val().split(':')[0];
    if (msg == "") { alert('信息不能为空!!'); return; }
    var value = pid + ":::" + id + ":::" + msg + ":::" + $("#reply_hid_" + id).val();
    PostToCS("AddReply2", value, function () { LoadReply(pid, pageSize, 1); });
}
function PostToCS(a, v, CallBack) {
    PostToCS2("/Plat/Blog/Default.aspx", a, v, CallBack);
}//Post To CS end;
function PostToCS2(u, a, v, CallBack) {
    $.ajax({
        type: "Post",
        url: u,
        data: { action: a, value: v },
        success: function (data) {
            if (CallBack) { CallBack(data); }
        },
        error: function (data) {
        }
    });
}
//function showword(obj) {
//    $(obj).hide()[0].error = null;
//    var uid = $(obj).data("uid");
//    if (!uid) { uid = parseInt(Math.random() * 10); }
//    var colorArr = "0094ff,FE7906,852b99,74B512,4B7F8C,00CCFF,A43AE3,22AFC2,F874A4".split(',');
//    var $word = $(obj).siblings(".uword:first").css("background-color", "#" + colorArr[((uid + 10) % 10)]);
//    if ($word.text().length > 1) {
//        $word.text($word.text().substring(0, 1))
//    }
//    $word.show();
//}
/*Attach Begin*/
//WUFile {name: "test.html", size: 76272, type: "text/html", lastModifiedDate: Thu Apr 16 2015 17:41:02 GMT+0800 (China Standard Time), id: "WU_FILE_0"…}
var attachDiag = new ZL_Dialog();
function ShowFileUP() {
    attachDiag.title = "文件上传";
    attachDiag.reload = true;
    attachDiag.backdrop = true;
    attachDiag.maxbtn = false;
    attachDiag.width = "width1100";//Blog
    attachDiag.url = "/Plugins/WebUploader/WebUP.aspx?json={\"ashx\":\"action=Blog\",\"pval\":\"\"}";
    attachDiag.ShowModal();
}
function AddAttach(file, ret, pval) {
    var src = ret._raw;
    if (src == "" || src.indexOf('<') > -1) { alert('请勿上传可疑文件!!'); attachDiag.CloseModal(); return; }
    var imgli = "<li data-name='@name'><p><img src='@src' /></p>"
        + "<div class='file-panel' style='height: 0px;'><span class='cancel'>删除</span></div></li>";
    var divli = "<li data-name='@name'><div class='imgview'><div class='ext @ex'></div><div class='fname'>@fname</div></div><div class='file-panel' style='height: 0px;'><span class='cancel'>删除</span></div></li>";
    $("#uploader").show();
    var li = "", name = GetFname(src);
    if (IsImage(src)) {
        var li = imgli.replace(/@src/, src).replace(/@name/, name);
    }
    else {
        var li = divli.replace("@ex", GetExName(src)).replace("@fname", GetFname(src, 6)).replace(/@name/, name);
    }
    $("#uploader .filelist").append(li);
    $("#Attach_Hid").val($("#Attach_Hid").val() + GetFname(src, 0) + "|");//仅存文件名,用于防止用户随意指定图片
    BindAttachEvent();
    attachDiag.CloseModal();
}
function RemoveAttach(name) {
    var attctArr = $("#Attach_Hid").val().split('|');
    var result = "";
    for (var i = 0; i < attctArr.length; i++) {
        if (attctArr[i] != name) {
            result += attctArr[i] + "|";
        }
    }
    result = result.replace("||", "|").trim("|");
    $("#Attach_Hid").val(result);
    if ($("#uploader .filelist li").length < 1) { $("#uploader").hide(); }
}
function BindAttachEvent() {
    $("#uploader .filelist li").mouseenter(function () {
        $btns = $(this).find(".file-panel");
        $btns.stop().animate({ height: 30 });
    }).mouseleave(function () {
        $btns = $(this).find(".file-panel");
        $btns.stop().animate({ height: 0 });
    });
    $(".filelist li .cancel").click(function () {
        $li = $(this).closest("li");
        RemoveAttach($li.data("name"));
        $li.remove();
    });
}
/*Attach End*/
$("#xs_share").click(function (e) {
    if ($(".xs_share_box").css("display") == "none") {
        $(".xs_share_box").fadeIn();
        $(this).text("×");
    }
    else {
        $(".xs_share_box").fadeOut();
        $(this).text("+");
    }
})
//全屏预览
function LargePre() {
    window.location.href = $("#preview_if").attr('src');
}
//-------------------------------Popover
//仅自定义日期需要点击确定
var datef = {};
datef.tlp = "<div id=\"datef_div\">"
            + "<ul id=\"datef_ul\" class=\"list-unstyled\">"
            + "<li onclick=\"datef.filter(this,'today');\">今天</li>"
            + "<li onclick=\"datef.filter(this,'thismonth');\">本月</li>"
            + "<li onclick=\"datef.filter(this,'lastmonth');\">上月</li>"
            + "<li onclick=\"datef.filter(this,'last-7');\">最近七天</li>"
            + "<li id=\"custom_li\" onclick=\"$('#datef_div .date_sel').toggle();\">自定义日期 </li>"
            + "</ul>"
            + "<div class=\"date_sel\">"
            + "<input type=\"text\" class=\"form-control\" id=\"datef_stime_t\" placeholder=\"起始时间\" onclick=\"WdatePicker({ dateFmt: 'yyyy/MM/dd' })\" />"
            + "<input type=\"text\" class=\"form-control margin_t5\" id=\"datef_etime_t\" placeholder=\"结束时间\" onclick=\"WdatePicker({ dateFmt: 'yyyy/MM/dd' })\" />"
            + "<div class=\"text-center margin_t5\">"
            + "<input type=\"button\" value=\"确定\" style='width:48%;' class=\"btn btn-primary\" onclick=\"datef.filter($('#custom_li'),'customer');\" />"
            + "<input type=\"button\" value=\"取消\" style='width:48%;' class=\"btn btn-default\" onclick=\"datef.close();\" />"
            + "</div>"
            + "</div>"
            + "</div>";
datef.$pop = $(datef.tlp)
datef.filter = function (li, date) {
    $("#datef_ul li").removeClass("active");
    $(li).addClass("active");
    if (date == "customer") { date = $("#datef_stime_t").val() + "|" + $("#datef_etime_t").val(); }
    B_Msg.clear();
    B_Msg.conf.date = date;//日期以(&)切割
    B_Msg.loadMore();
    datef.close();
}
datef.close = function () {
    $("#datef_div .date_sel").hide();
    $("#datef_btn").popover("hide");
}
$("#datef_btn").popover({ animation: true, placement: 'bottom', html: true, title: "", content: function () { return datef.$pop; }, trigger: "click" });
//-----------------------------
var pop = { timer: null, cuid: "", list: [] };
pop.bindEvent = function ($items) {
    $items.popover({
        animation: true, placement: 'right', title: "<i class=\"fa fa-user\"></i> 用户信息<a href=\"javascript:;\" class='pull-right' title='关闭' onclick=\"$('.uinfo').popover('hide');\"><i class='fa fa-remove'></i></a>", content: function () {
            if (pop.list.GetByID(pop.cuid, "UserID")) {
                var data = pop.list.GetByID(pop.cuid, "UserID");
                var items = JsonHelper.FillItem(pop.tlp, data, null);
                return items;
            }
            //-----------------
            $.post("/Plat/Common/Common.ashx", { action: "getuinfo", value: pop.cuid }, function (data) {
                var items = JsonHelper.FillItem(pop.tlp, data, null);
                $(".popover-content").html(items);
                pop.list.push(data);
            }, "json");
            return '<div><i class="fa fa-spinner fa-spin" style="font-size:3em;"></i></div>';
        }, html: true, trigger: 'manual',
    }).hover(function () {
        var uinfo = $(this);
        pop.cuid = uinfo.data("uid");
        pop.timer = setTimeout(function () {
            $(".uinfo").not(uinfo).popover('hide');
            $(uinfo).popover('show'); clearTimeout(pop.timer);
        }, 300);
    }, function () {
        clearTimeout(pop.timer);
    })
}
pop.tlp = "<div class=\"uinfodiv\">"
        + "<div class=\"loadok\">"
        + "<div class=\"info\" style=\"padding-bottom:0px;\">"
        + "<div class=\"pull-left\">"
        + "<img src=\"@UserFace\" onerror=\"shownoface(this);\" class=\"uimg img_mid\" />"
        + "</div>"
        + "<div class=\"uinfo_body\">"
        + "<ul class=\"uinfoul\">"
        + "<li class=\"paddbottom5\">@UserName"
        + "<input type=\"button\" class=\"btn btn-xs btn-info\" style=\"margin-left:8px;\" value=\"AT他\" onclick=\"AddAT('@UserName', '@UserID');\" />"
        + "</li>"
        + "<li class=\"grayremind\">电话：@Mobile</li>"
        + "<li class=\"grayremind\">部门：@GroupName</li>"
        + "<li class=\"grayremind\">状态：在职</li>"
        + "</ul>"
        + "</div>"
        + "<div class=\"clearfix\"></div>"
        + "</div>"
        + "<div class=\"uinfo_bottom\">"
        + "<a href=\"/Plat/Blog/?uids=@UserID\" class=\"btn btn-xs btn-primary\">工作流</a>"
        + "<a href=\"/Plat/Blog/?uids=@UserID&view=timeline\" class=\"btn btn-xs btn-primary\">时间线</a>"
        + "<input type=\"button\" class=\"btn btn-xs btn-primary\" value=\"私信\" onclick=\"ChatShow('@UserID', '@UserName');\" />"
        + "<input type=\"button\" class=\"btn btn-xs btn-primary\" value=\"站内邮\" onclick=\"PrivateOpen('@UserID', '@UserName'); $('.uinfo').popover('hide');\" />"
        + "</div>"
        + "</div>"
        + "</div>"
//-----------------------------
var TextArea = {
    $text: $("#MsgContent_T"),
    init: function () {
        var ref = this;
        ref.$text.focus(function () { ref.expand(ref.$text); }).blur(function () { ref.narrow(ref.$text); });
    },
    expand: function ($text) {
        if ($text.val().length > 0 && $text.height() > 60) { return; }
        $text.animate({ height: '150px' });
    },
    narrow: function ($text) {
        if ($text.val().length > 0) { return; }
        $text.animate({ height: '60px' });
    }
};
var sign = { api: "/Plat/Common/Signin.ashx" };
//初始化签到挂件
sign.init = function () {
    $.post(sign.api, { action: "signinit" }, function (data) {
        var model = APIResult.getModel(data);
        if (APIResult.isok(model)) {
            $("#signin_btn,#signout_btn").addClass("hidden");
            switch (model.result) {
                case "signin":
                    {
                        $("#signin_btn").removeClass("hidden");
                    }
                    break;
                case "signout":
                    {
                        $("#signout_btn").removeClass("hidden");
                        sign.filltr($("#signin_tr"), model.addon[0]);
                    }
                    break;
                case "end":
                    {
                        sign.filltr($("#signin_tr"), model.addon[1]);
                        sign.filltr($("#signout_tr"), model.addon[0]);
                    }
                    break;
            }
        }
    });
}
//根据model展示签到数据
sign.filltr = function (tr, model) {
    var $tr = $(tr);
    if (model) {
        var time;
        if (isIE()) { time = new Date(model.CDate); }
        else { time = new Date(model.CDate.replace("T", " ")); }
        var ip = model.IPLocation;
        var t = sign.checktime(time.getHours()) + ":" + sign.checktime(time.getMinutes())
        switch (model.State) {
            case 0:
                $tr.find(".time").html(t);
                break;
            case 1:
                $tr.find(".time").html("<span title='迟到' style='color:red;'>" + t + "</span>");
                break;
            case 2:
                $tr.find(".time").html("<span title='早退' style='color:red;'>" + t + "</span>");
                break;
        }
        $tr.find(".ip").html("电脑端：" + ip);
    }
}
function isIE() { //ie?  
    if (!!window.ActiveXObject || "ActiveXObject" in window)
        return true;
    else
        return false;
}
//签到
sign.signin = function () {
    $.post(sign.api, { action: "signin" }, function (data) {
        var model = APIResult.getModel(data);
        if (APIResult.isok(model)) {
            sign.init();
        }
    });
}
//签退
sign.signout = function () {
    var now = new Date();
    var edate = new Date().setHours(18);
    if (edate > now && !confirm("还没到下班时间，您确定要签退吗？")) { return; }
    $.post(sign.api, { action: "signout" }, function (data) {
        var model = APIResult.getModel(data);
        if (APIResult.isok(model)) {
            sign.init();
        }
    });
}
//判断时间分、秒是一位还是两位，若是一位则前面加上0
sign.checktime = function (i) {
    if (i < 10)
    { i = "0" + i }
    return i
}