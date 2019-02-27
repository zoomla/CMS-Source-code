<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostList.aspx.cs" Inherits="Guest_Bar_PostList" MasterPageFile="~/Guest/Guest.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link href="/Plugins/Ueditor/third-party/video-js/video-js.min.css" rel="stylesheet" />
<script src="/Plugins/Ueditor/bar.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<script src="/Plugins/Ueditor/third-party/video-js/video.js"></script>
<script type="text/javascript" src="/JS/jquery-ui.min.js"></script>
<title><asp:Literal runat="server" ID="Title_L" />_<%:Call.SiteName+"贴吧" %></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
<div class="bar_RelButton">
<a href="<%=GetRouteUrl("BarIndex", new { })%>" title="返回列表"><i class="fa fa fa-arrow-circle-left"></i>返回社区列表</a>
</div>
<div id="bar_hdiv" class="bar_hdiv">
    <div class="bar_imgDiv" >
        <img id="BarImage_img" onerror="shownopic(this);"/>
    </div>
    <div class="bar_Title">
        <div>
            <a href="javascript:;" onclick="location=location;" title="贴吧"><asp:Label runat="server" ID="BarName_L" /></a>
            <span runat="server" id="totalspan" class="countInfo">主题总数：<span class="card_menNum" runat="server" id="dnum_span2"></span>回复数：<span id="replycount" class="card_menNum" runat="server"></span></span>
        </div>
        <div><asp:Label runat="server" ID="BarInfo_L" /></div>
    </div>
    <div class="bar_date">
        <div class="disinline">
            <input type="button" id="sinin_bu" class="btn btn-lg btn-primary" onclick="SinIn(this)" value="签到"  />
        </div>
        <div class="date">
            <span><%:DateTime.Now.ToString("MM月dd日") %></span><br />
            <span class="sinDays" id="sinDays">未签到</span>
        </div>
    </div>
    <div class="clearboth"></div>
</div>
<div id="childBar" runat="server">
<h5 class="well well-sm"><strong>子版块</strong></h5>
    <asp:Repeater runat="server" ID="ChildRPT" EnableViewState="false">
        <HeaderTemplate>
            <table class="table bar_table">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="bar_imgDiv">
                    <img src="<%#Eval("BarImage") %>" onerror="shownopic(this);" /></td>
                <td class="barname">
                    <a href="/PClass?id=<%#Eval("CateID") %>"><%#Eval("CateName") %></a>
                </td>
                <td>
                    <span class="card_menNum" title="主题"><%#Eval("ItemCount") %></span><span class="reply_num" title="回贴">/<%#Eval("ReCount") %></span>
                </td>
                <td class="tie_info">
                    <div>
                        最新帖子：<a href="/PClass?id=<%#Eval("ID") %>"><%#Eval("Title") %></a>
                    </div>
                    <div class="tie_date">
                        回复时间：<%#ConverDate(Eval("CDate"),"yyyy年MM月dd日 HH:mm") %>
                    </div>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></table></FooterTemplate>
    </asp:Repeater>
</div>
<div id="emptydiv" class="emptydiv" runat="server" visible="false">
    <span class="fa fa-comment margin-right5"></span>
    <span>当前还没有内容,快点发一条吧</span>
</div>
<div id="contentdiv" runat="server" visible="false">
    <asp:Repeater runat="server" ID="RPT">
        <ItemTemplate>
            <div class="tie_list">
                <div class="checks">
                    <div class="rcountnum"><span class="margin-right5"><%#Eval("RCount") %></span>
                        <%#DisCheckBox() %>
                    </div>
                </div>
                <div class="subdiv">
                    <div><%#GetTitle()%> <span><%#GetTieStaues() %></span></div>
                    <div id="sub_div_<%#Eval("ID") %>" class="subtitle" data-id="<%#Eval("ID") %>">
                        <%#GetSubTitle() %></div>
                    <div id="sub_video_div_<%#Eval("ID") %>" style="display: none;">
                        <div class="font12">
                            <span><span class="fa fa-upload"></span><a href="javascript:;" onclick="Collapse();">收起</a></span>
                        </div>
                        <div class="video_div"></div>
                    </div>
                    <div id="sub_qvideo_div_<%#Eval("ID") %>" style="display: none;">
                         <div class="font12">
                             <span><span class="fa fa-upload" ></span><a href="javascript:;" onclick="Collapse();">收起</a><span class="sperspan">|</span></span>
                             <span><span class="fa fa-th-large"></span><a class="fullscreen_href" href="#" >全屏</a></span>
                         </div>
                         <div class="qvideo_div"></div>
                     </div>
                    <div id="view_div_<%#Eval("ID") %>" class="view_div" style="display: none;">
                        <div class="font12">
                            <span><span class="fa fa-upload"></span><a href="javascript:;" onclick="Collapse(<%#Eval("ID") %>);">收起</a><span class="sperspan">|</span></span>
                            <span><i class="fa fa-mail-reply"></i><a href="javascript:;" onclick="RoteImg('view_img_<%#Eval("ID") %>',1);">左转</a><span class="sperspan">|</span></span>
                            <span><i class="fa fa-mail-forward"></i><a href="javascript:;" onclick="RoteImg('view_img_<%#Eval("ID") %>',2);">右转</a><span class="sperspan">|</span></span>
                            <span><i class="fa fa-arrows-alt"></i><a href="javascript:;" onclick="ViewImg(<%#Eval("ID") %>);">查看大图</a></span>
                            <button type="button" id="view_btn_<%#Eval("ID") %>" onclick="Collapse()" style="display:none;"></button>
                        </div>
                        <div class="view_imgdiv"><div class="view_preimg"></div><div class="view_nextimg"></div><span><img id="view_img_<%#Eval("ID") %>" data-angle="0" src="#" onclick="Collapse();" /></span></div>
                    </div>
                </div>
                <div class="font12 tie_rel_count">
                    <div><span class="fa fa-user"></span><a href="<%#"PostSearch?uid="+Eval("CUser") %>"><%# GetUName()%></a></div>
                    <div><%#GetRUser() %></div>
                </div>
                <div class="clearboth"></div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div runat="server" class="send_div" id="send_div">
    <asp:Literal runat="server" ID="MsgPage_L" EnableViewState="false"></asp:Literal>
    <div class="barcount">共有主题数<span class="card_menNum" runat="server" id="dnum_span"></span>个，共<span class="card_menNum" runat="server" id="pagenum_span1"></span>页</div>
    <div id="SendDiv" runat="server">
    <div class="tie_title">
        <span class="fa fa-comment"></span><strong>发表贴子</strong>
        <span runat="server" id="Anony_Span" visible="false" class="card_menNum font12">[匿名发帖模式]</span></div>
    <div><asp:TextBox runat="server" ID="MsgTitle_T" data-type="normal" CssClass="form-control"/></div>
    <div class="tiecontent">
        <asp:TextBox runat="server" ID="MsgContent_T" data-type="normal" TextMode="MultiLine" style="height: 200px;width:100%;"/></div>
        <asp:TextBox ID="VCode" placeholder="验证码" MaxLength="6" runat="server" CssClass="form-control text_x" autocomplete="off"/>
        <img id="VCode_img" class="codeimg" title="点击刷新验证码"/>
        <input type="hidden" id="VCode_hid" name="VCode_hid" />
      <asp:Button runat="server" ID="PostMsg_Btn" Text="发表主题"  OnClick="PostMsg_Btn_Click" class="disinline"  OnClientClick="return CheckData();" CssClass="btn btn-primary" />
    </div>
</div>
<div id="noauth_div" runat="server" visible="false">您未登录,没有发贴权限<span><a href="/User/Login.aspx?returnUrl=<%=Request.RawUrl %>">[点此登录]</a></span></div>
<%=Call.GetUEditor("MsgContent_T",4)%>
<div class="floattool">
    <ul>
        <!--<a href="javascript:;"><span class="txtSpan">回 到 顶 部</span></a>-->
        <li onclick="returnTop()" onmouseout="hideTxt(this)" onmouseover="showTxt(this)"><a href="javascript:;"><span class="fa fa-arrow-up"></span></a><span class="txtSpan">回 到 顶 部</span></li>
        <li onclick="returnDown()" onmouseout="hideTxt(this)" onmouseover="showTxt(this)"><a href="<%:Request.RawUrl %>"><span class="fa fa-edit"></span></a><span class="txtSpan">发 表 帖 子</span></li>
        <li onclick="returnPost()" onmouseout="hideTxt(this)" onmouseover="showTxt(this)"><a href="<%=GetRouteUrl("BarIndex", new { })%>" class="last"><span class="fa fa-th"></span></a><span class="txtSpan">回 到 社 区</span></li>
    </ul>
</div>

</div>
<div runat="server" id="barowner_div" visible="false" class="zIndex9">
    <div id="funcdiv" class="panel panel-primary candrag">    
        <div class="panel_left_border"></div>     
        <div class="panel-body">
            <div style="padding: 5px;">                              
                <div class="func_item"><asp:LinkButton runat="server" CommandArgument="AddRecom" OnClick="Bar_Btn_Click"><i class="fa fa-eye"><span> 精华</span></i>很好的帖子</asp:LinkButton></div>
                <div class="func_item"><asp:LinkButton runat="server" CommandArgument="Del" OnClick="Bar_Btn_Click"><i class="fa fa-close"><span> 删除</span></i>真的不要了吗</asp:LinkButton></div>
                <div class="func_item"><asp:LinkButton runat="server" CommandArgument="Checked" OnClick="Bar_Btn_Click"><i class="fa fa-check"><span> 批量审核</span></i>点击通过审核</asp:LinkButton></div>
                <div class="func_item"><a href="javascript:;" onclick="$('#movediv').show();"><i class="fa fa-exchange"><span>移动版块</span></i> 移到别的版块</a></div> 
                <div class="func_item"><asp:LinkButton runat="server" CommandArgument="RemoveRecom" OnClick="Bar_Btn_Click"  ><i class="fa fa-close"><span> 取消精华</span></i>换其他的看看</asp:LinkButton></div>                
                
            </div>
            <div style="padding: 5px;">
                <div class="func_item"><asp:LinkButton runat="server" CommandArgument="AddAllTop" OnClick="Bar_Btn_Click" ><i class="fa fa-arrow-circle-up"><span> 全局置顶</span></i>置为全局帖子</asp:LinkButton></div>
                <div class="func_item"><asp:LinkButton runat="server" CommandArgument="AddTop" OnClick="Bar_Btn_Click" ><i class="fa fa-level-up"><span> 版面置顶</span></i>置为头条帖子</asp:LinkButton></div>
                <div class="func_item"><asp:LinkButton runat="server" CommandArgument="AddBottom" OnClick="Bar_Btn_Click" ><i class="fa  fa-level-down"><span> 沉底</span></i>放在最底下</asp:LinkButton></div>
                <div class="func_item"><asp:LinkButton runat="server" CommandArgument="RemoveTop" OnClick="Bar_Btn_Click" ><i class="fa fa-refresh"><span> 位置还原</span></i>还原贴子位置</asp:LinkButton></div>                
                <div class="func_item"><asp:LinkButton runat="server" CommandArgument="UnCheck" OnClick="Bar_Btn_Click"><i class="fa fa-question"><span> 取消审核</span></i>改为未审核</asp:LinkButton></div>
            </div>
        </div>
    </div>
    <div id="movediv" class="panel panel-primary candrag" style="width:300px;">
         
        <div class="panel-body">        	 
             <asp:Button runat="server" ID="SureMove_Btn" Text="确定移动" OnClientClick="return CheckShift();" OnClick="SureMove_Btn_Click" class="btn btn-primary btn-xs pull-right" />
             <a href="javascript:;" onclick="$(this).parent().parent().hide();" class="btn btn-primary btn-xs pull-right">取消</a>
             <div class="clearfix"></div>
            <div class="dropdown" style="position:inherit">
                <button class="btn btn-default dropdown-toggle text-left" type="button" id="dropdown1" runat="server" data-toggle="dropdown" aria-expanded="true">
                    <span id="dr_text">请选择版面</span>
                    <span class="caret pull-right" style="margin-top: 7px;"></span>
                    <asp:HiddenField ID="selected_Hid" runat="server" />
                </button>
                <ul id="PCate_ul" runat="server" class="dropdown-menu bar-dropdown-menu" role="menu" aria-labelledby="dropdownMenu1"></ul>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
</div>
<div class="container footer">
Copyright &copy;<script>
var year = ""; mydate = new Date(); myyear = mydate.getYear(); year = (myyear > 200) ? myyear : 1900 + myyear; document.write(year);
</script>
<a href="<%:ZoomLa.Components.SiteConfig.SiteInfo.SiteUrl %>" target="_blank"><%:Call.SiteName %></a>版权所有
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/ZL_ValidateCode.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/js/jquery.rotate.min.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>
<script>
    $("#VCode").ValidateCode();
    function CheckData() {
        var title = $("#MsgTitle_T").val().replace(" ", "");
        var msg = UE.getEditor("MsgContent_T").getContent();
        if (title == "") { alert("标题不能为空"); return false; }
        if (msg == "") { alert("内容不能为空"); return false; }
        if ($("#TxtValidateCode").val() == "") { alert("验证码不能为空"); return false; }
        return true;
    }
    //移贴前检测
    function CheckShift() { 
        var flag = true;
        var len = $(":checkbox[name=idchk]:checked").length;
        var cid = $("#selected_Hid").val();
      
        if (len < 1 || !cid || cid == "")
        { flag = false; }
        if (!flag)
        { alert("请先选定需要移往的版块");}
        return flag;
    }

    
    $().ready(function () {
        $(".subtitle img").click(function () {
            var type = $(event.srcElement).attr("data-type");
            switch (type) {
                case "video":
                    ShowVideo();
                    break;
                case "quotevideo":
                    ShowQVideo();
                    break;
                default:
                    ShowImgView();
                    break;
            }
        });//click end;
        $(".header_index_login_run").attr("href", '/User/Login.aspx?returnUrl=<%=Request.RawUrl %>');
        $.ajax({
            url: "/API/UserSinIn.ashx",
            data: { action: "sinstatu", localtion: "1" },
            type: "POST",
            success: function (d) {
                if (d != "none") {
                    $("#sinin_bu").val("已签到").attr("disabled", "disabled");
                    $("#sinDays").text("连续" + d + "天");
                }
            }
        });
        $(".subtitle img").attr("title", "点击预览");
        $(":checkbox[name=idchk]").click(function (e) {
            if ($(":checkbox[name=idchk]:checked").length > 0){
                var e = event || window.event;
                 $("#barowner_div").css({
                    top: e.clientY-35,
                    left:e.clientX+38,
                })
                $("#barowner_div").show();
            }
                
            else $("#barowner_div").hide();
        });
        //版面下拉
        $(".barli").click(function () {
            $("#selected_Hid").val($(this).data("barid"));
            $("#dr_text").text($(this).text());
        });
        $(".candrag").draggable
           ({
               addClasses: false,
               axis: false,
               cursor: 'crosshair',
               containment: 'document'
           });
    });
    function showTxt(data) {
        $(data).children().first().hide();
    }
    function hideTxt(data) {
        $(data).children().first().show();
    }
    function returnPost() {
        window.location = "<%=GetRouteUrl("BarIndex", new { })%>";
}
function returnTop() {
    setTimeout(setScollTop, 1);
}
function setScollTop() {
    if ($(window).scrollTop() > 1) {
        $(window).scrollTop($(window).scrollTop() - 30);
        setTimeout(setScollTop, 1);
    }

}
function returnDown() {
    window.location = "/EditContent?Cid=<%=CateID %>&ID=-1";
}
function setScollDown() {
    var top = $(window).scrollTop();
    $(window).scrollTop($(window).scrollTop() + 30);
    if (top != $(window).scrollTop())
        setTimeout(setScollDown, 1);
}
//<embed type="application/x-shockwave-flash" class="edui-faked-video" pluginspage="http://www.macromedia.com/go/getflashplayer" src="http://player.youku.com/player.php/sid/XODU2MDQwMTc2/v.swf" width="420" height="280" wmode="transparent" play="true" loop="false" menu="false" allowscriptaccess="never" allowfullscreen="true">
//引用视频预览
function ShowQVideo() {
    var tlp = "<embed type='application/x-shockwave-flash' class='edui-faked-video' pluginspage='http://www.macromedia.com/go/getflashplayer' src='@src' width='420' height='280' allowfullscreen='true' allowscriptaccess='never'"
        + "menu='false' loop='false' play='true' wmode='transparent'>";
    var pobj = $(event.srcElement).closest(".subtitle");
    var id = pobj.attr("data-id");
    var content = $(event.srcElement).attr("data-content");
    pobj.find("img").hide();
    $("#sub_qvideo_div_" + id).find(".fullscreen_href").attr("href", content);
    $("#sub_qvideo_div_" + id).find(".qvideo_div").html("").append(tlp.replace("@src", content));
    $("#sub_qvideo_div_" + id).show();
}
//--------图片预览
function ShowImgView() {
    clearCurPreImg();
    var pobj = $(event.srcElement).closest(".subtitle");
    var id = pobj.attr("data-id");
    pobj.find("img").hide();
    $("#view_img_" + id).attr("src", $(event.srcElement).attr("src"));
    $("#view_div_" + id).show();
    curPreImg=event.srcElement;
    $("#view_div_" + id).find(".view_preimg").click(function () { eachImg(id,0)});
    $("#view_div_" + id).find(".view_nextimg").click(function () { eachImg(id, 1); });
    checkNextImg(id);
}
var curPreImg;//当前预览图
//清空当前预览视图
function clearCurPreImg() {
    if (!curPreImg) return;
    var pobj = $(curPreImg).closest(".subtitle");
    var id = pobj.attr("data-id");
    DisPreView($("#view_img_" + id));
}
//浏览多图片，action=0;上一张 action=1;下一张
function eachImg(preid, action) {
    var $li = $(curPreImg).parent();
    if (action == 1 && $li.next().children().attr("src")) {
        $("#view_img_" + preid).attr("src", $li.next().children().attr("src"));
        curPreImg = $li.next().children()[0];
    }
    if (action == 0 && $li.prev().children().attr('src')) {
        $("#view_img_" + preid).attr("src", $li.prev().children().attr("src"));
        curPreImg = $li.prev().children()[0];
    }
    checkNextImg(preid);
}
//检查是否还有下一张(上一张)图片
function checkNextImg(id) {
    var $li = $(curPreImg).parent();
    if (!$li.next()[0])
        $("#view_div_" + id).find(".view_nextimg").hide();
    else
        $("#view_div_" + id).find(".view_nextimg").show();
    if (!$li.prev()[0])
        $("#view_div_" + id).find(".view_preimg").hide();
    else
        $("#view_div_" + id).find(".view_preimg").show();
}

//--------视频预览开始
function ShowVideo() {
    var tlp = "<video width='534' height='386' class='edui-upload-video  vjs-default-skin video-js' src='@src' preload='none' controls='' data-setup='{}'><source src='@src' type='video/mp4'></video>";
    var pobj = $(event.srcElement).closest(".subtitle");
    var id = pobj.attr("data-id");
    var content = $(event.srcElement).attr("data-content");
    pobj.find("img").hide();
    $("#sub_video_div_" + id).find(".video_div").html("").append(tlp.replace(/@src/g, content));
    $("#sub_video_div_" + id).show();
}
//------视频预览结束
function Collapse(id) {
    DisPreView(event.srcElement);
}
//隐藏预览视图
function DisPreView(obj) {
    $obj = $(obj).parent().parent().parent();
    $obj.hide().siblings(".subtitle").find("img").show();
    $obj.find(".view_preimg").unbind('click');
    $obj.find(".view_nextimg").unbind('click');
}
var ViewDiag = new ZL_Dialog();
function ViewImg(id) {
    ViewDiag.width = "tie_viewImg";
    ViewDiag.title = "图片预览";
    ViewDiag.url = "/Common/PreView/PicView.aspx?ID=" + id;
    ViewDiag.maxbtn = false;
    ViewDiag.ShowModal();
}
function FocusMsg() {
    $(window).scrollTop($(document).height());
    $("MsgContent_T").focus();
}
function SinIn(e) {
    $.ajax({
        url: "/API/UserSinIn.ashx",
        data: { action: "sinin", localtion: "1" },
        type: "POST",
        success: function (d) {
            if (d != "" && d != "-1") {
                $(e).val("已签到");
                $(e).attr("disabled", "disabled");
                $("#sinDays").text("连续" + d + "天");
            } else {
                alert("您尚未登录！！");
            }

        }
    });
}
function RoteImg(id, option) {
    var angle = 0;
    if (option==1)
        angle = $('#' + id).data("angle") - 90;
    else
        angle = $('#' + id).data("angle") + 90;
    $('#'+id).data("angle", angle);
    $('#' + id).rotate(angle);
}
function GetSource() { return "<%=Request.RawUrl%>"; }
</script>
</asp:Content>