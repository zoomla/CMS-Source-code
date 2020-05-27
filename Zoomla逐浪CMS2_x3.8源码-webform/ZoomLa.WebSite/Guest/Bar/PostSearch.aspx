<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Guest/Guest.master" CodeFile="PostSearch.aspx.cs" Inherits="Guest_Bar_PostSearch" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
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
<asp:Literal runat="server" ID="MsgPage_L" EnableViewState="false"></asp:Literal>
<div class="barcount">共有主题数<span class="card_menNum" runat="server" id="dnum_span"></span>个，共<span class="card_menNum" runat="server" id="pagenum_span1"></span>页</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script src="/JS/ZL_ValidateCode.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/js/jquery.rotate.min.js"></script>
    <script src="/JS/ICMS/ZL_Common.js"></script>
    <script>
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
            curPreImg = event.srcElement;
            $("#view_div_" + id).find(".view_preimg").click(function () { eachImg(id, 0) });
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
    <script>
        $(function () {
            //搜索关键字高亮显示
            var searkeys = "<%:Skey%>".trim().split('|');
            $(".search_title").each(function () {
                var $titleobj = $(this).next();
                for (var i = 0; i < searkeys.length; i++) {
                    if (searkeys[i] != "") {
                        var regex = new RegExp(searkeys[i], "gi");
                        var datas = $titleobj.html().match(regex);
                        if (datas) {
                            for (var j = 0; j < datas.length; j++) {
                                $titleobj.html($titleobj.text().replace(regex, "//^" + datas[j] + "//$"));
                            }
                        }
                    }
                }
                $titleobj.html($titleobj.text().replace(/\/\/\^/g, "<span style='color:red;'>").replace(/\/\/\$/g,"</span>"));
            });
        });
    </script>
</asp:Content>

