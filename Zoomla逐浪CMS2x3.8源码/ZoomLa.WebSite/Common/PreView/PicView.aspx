<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PicView.aspx.cs" Inherits="Common_PreView_PicView" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>图片预览</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div onmousemove="showButton()" onmouseout="hideButton()" style="width: 1000px; height: 500px;">
            <div onclick="reimg()" style="position:absolute; display:none; width:50px;height:400px;"><a class="rebutton" href="javascript:;"></a></div>
            <div onclick="nextimg()" style="position:absolute; display:none; left:950px; width:50px;height:400px;">
                <div class="imageCount"><span class="curImage">1</span><span>/</span><span class="SumImageCount" runat="server" id="piccount_span">0</span></div>
                <a class="nextbutton" href="javascript:;"></a>
            </div>
            <div style="width:100%;height:80%; background-color:black; text-align:center;">
                <img id="main_img" runat="server"  data-select_index="0" src="" onerror="shownopic(this);" style="max-width:900px; max-height:300px;" />
            </div>
            <div id="sections" class="container" style="width:100%">
                <div id="horizontal">
                    <div class="slyWrap example1">
                        <a href="javascript:;" class="arrowL" id="prevPageBtn"><img src="/Plugins/JqueryUI/Sly/img/ArrowL.png" /></a>
                        <a href="javascript:;" class="arrowR" id="nextPageBtn"><img src="/Plugins/JqueryUI/Sly/img/ArrowR.png" /></a>
                        <div class="sly" data-options='{ "horizontal": 1, "itemNav": "basic", "dragContent": 1, "startAt": 3, "scrollBy": 1 }'>
                            <ul>
                                <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
                                    <ItemTemplate>
                                        <li class="active"><a class="thumbnail" title="图片预览">
                                            <img onclick="checkImg(this)" data-index="<%#Eval("Index") %>" style="width: 120px; height: 185px;cursor:pointer;" class="preview_img" src="<%#Eval("Src") %>" /></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div></div></div></div></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <link type="text/css" href="/Plugins/JqueryUI/Sly/css/style.css" rel="stylesheet" />
    <style>
        .arrowL {float: left;height: 195px;}
        .arrowL img {margin-top: 80px;margin-right: 20px;}
        .arrowR {float: right;height: 195px;}
        .arrowR img {margin-top: 80px;margin-left: 20px;}
        .rebutton{display:block;width:50px;height:100%;background-image:url("/App_Themes/Guest/images/Bar/slider-arrow-left.gif");background-repeat:no-repeat;background-position:center center;opacity:.6;background-color:#fff;}
        .nextbutton{display:block;width:50px;height:100%;background-image:url("/App_Themes/Guest/images/Bar/slider-arrow-right.gif");background-repeat:no-repeat;background-position:center center; opacity:.6;background-color:#fff;}
        .imageCount{display:block;position:absolute;top:5px;right:5px;padding:5px;color:#fff;background-color:black;line-height:normal; }
        #main_img{cursor:pointer;}
    </style>
    <script type="text/javascript" src="/Plugins/JqueryUI/Sly/jquery.sly.min.js"></script>
    <script type="text/javascript" src="/Plugins/JqueryUI/Sly/js/main.js"></script>
    <script src="/JS/ICMS/ZL_Common.js"></script>
    <script type="text/javascript">
        var divheight = $("#main_img").parent().height();
        var divwidth = $("#main_img").parent().width();
        $("#main_img").css("margin-top", ($("#main_img").parent().height() / 2) - ($("#main_img").height() / 2));//图片上下居中
        $("[class=nextbutton]").parent().height(divheight);//div自适应高度
        $("[class=rebutton]").parent().height(divheight);//div自适应宽度
        $("[class=nextbutton]").parent().css("left", divwidth - $("[class=nextbutton]").parent().width());//div自适应左边距
        function showButton() {
            $("[class=nextbutton]").parent().show();
            $("[class=rebutton]").parent().show();
        }
        function hideButton() {
            $("[class=nextbutton]").parent().hide();
            $("[class=rebutton]").parent().hide();
        }
        function reimg() {
            var currentIndex = (parseInt($("#main_img").attr("data-select_index")) - 1);
            if (currentIndex >= 0) {
                $("#main_img").attr("src", $("[data-index=" + currentIndex + "]").attr("src"));
                $("#main_img").attr("data-select_index", currentIndex);
                $("[class=curImage]").text(currentIndex + 1);
                checkImg($("[data-index=" + currentIndex + "]")[0]);
                $("#main_img").css("margin-top", ($("#main_img").parent().height() / 2) - ($("#main_img").height() / 2));//图片上下居中
            }
        }
        function nextimg() {
            var currentIndex = (parseInt($("#main_img").attr("data-select_index")) + 1);
            if ($("[data-index=" + currentIndex + "]").attr("src") != undefined) {
                $("#main_img").attr("src", $("[data-index=" + currentIndex + "]").attr("src"));
                $("#main_img").attr("data-select_index", currentIndex);
                $("[class=curImage]").text(currentIndex + 1);
                checkImg($("[data-index=" + currentIndex + "]")[0]);
                $("#main_img").css("margin-top", ($("#main_img").parent().height() / 2) - ($("#main_img").height() / 2));//图片上下居中
            }

        }
        function checkImg(data) {
            $("li").children(0).css("border-color", "#ddd");
            $("#main_img").attr("src", $(data).attr("src"));
            $("#main_img").attr("data-select_index", $(data).attr("data-index"));
            $("[class=curImage]").text(parseInt($(data).attr("data-index")) + 1);
            $(data).parent().css("border-color", "#428bca");
            $("#main_img").css("margin-top", ($("#main_img").parent().height() / 2) - ($("#main_img").height() / 2));//图片上下居中
        }
        $().ready(function () {
            $("#main_img").click(function () {
                window.open($(this).attr('src'));
            });
        });
    </script>
</asp:Content>
