<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mb_music.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.mb_music" MasterPageFile="~/Design/Master/Edit.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>选择音乐</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<%--<div class="div_title">音乐</div> --%>
<div class="search">
    <div class="search_div">
        <asp:TextBox ID="Skey_T" runat="server" />
        <asp:Button ID="Search_B" runat="server" CssClass="hidden" OnClick="Search_B_Click" />
        <i id="icon" class="fa fa-search"></i>
        <span id="search_btn" class="text">音乐搜索</span>
    </div>
</div>
<div class="mb_music">
    <div class="scroll_wrap">
        <div class="nomusic music_item"><div class="rad" data-vpath="">●</div> <div class="name">无音乐</div></div>
        <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
            <ItemTemplate>
                <div class="music_item">
                    <div class="rad" data-vpath="<%#Eval("VPath") %>">●</div>
                    <div class="name"><%#Eval("name") %></div>
                    <div title="试听" data-vpath="<%#Eval("VPath") %>" class="play_wrap" style="text-decoration: none;">
                        <i class="fa fa-play"></i>
                        <i class="fa fa-pause"></i>
                    </div>
                    <div class="choose"><i class="fa fa-check"></i></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div class="clearfix"></div>
    </div>
</div>
<div class="page_div"><asp:Literal ID="Page_Lit" runat="server" /></div>
<%--<div class="btn_div">
    <a href="javascript:;"><i class="fa fa-times" onclick="CloseSelf();"></i></a>
    <a href="javascript:;"><i class="fa fa-check" onclick="setMusic();"></i></a>
</div>--%>
<audio id="test_audio" style="display: none;" autoplay="autoplay"></audio>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>
/*.div_title{height:75px;font-size:30px;padding-top:20px; text-align:center;position:fixed;top:0px;width:100%;background-color:#fff;border-bottom:2px solid #e3e3e3;}*/
.search{position:fixed;top:0;width:100%;background-color:#fff;}
.search_div{position:relative;height:75px;border-bottom:1px solid #e3e3e3;}
.search_div #Skey_T{height:55px;margin-left:15px; margin-top:10px;border-radius:26px;font-size:25px;padding-left:25px;width:75%;}
.search_div i{position: absolute;right:25%;top:25px; color: #999;background: #fff;font-size:25px;}
.search_div .text{font-size:25px;margin-left:10px;}
.page_div{height:70px;position:fixed;bottom:0px;text-align:center;background-color:#fff;width:100%;border-top:1px solid #e3e3e3;}
.page_div .pagination{-moz-transform:scale(2); -webkit-transform:scale(1.5); -o-transform:scale(2); }
.mb_music .scroll_wrap{margin-top:75px;margin-bottom:70px;}
.music_item{background-color:#fcfcfc;height:75px;border-bottom:1px solid #ddd;overflow:hidden;display:block;padding:10px 16px;}
.music_item .rad{color:#fcfcfc;width:50px;height:60px;font-size:25px; float:left;padding-top:10px;}
.music_item .name{font-size:25px;width:400px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap; display:inline-block;padding-top:10px;}
.music_item .play_wrap{color:#fff;float:right;width:60px;height:60px;background-color:#21ebff;text-align:center;border-radius: 100%;font-size:25px;}
.music_item .play_wrap:hover{color:#fff;}
.music_item .play_wrap i{line-height:62px;}
.music_item .play_wrap .fa-play {display:inline-block;}
.music_item .play_wrap .fa-pause {display:none;}
.music_item.active{background-color:#21ebff;}
.music_item.active .play_wrap{background-color:#00d8ff; }
.music_item.play .play_wrap .fa-play{display:none;}
.music_item.play .play_wrap .fa-pause {display:inline-block;}
.music_item .choose {display:none;color:green;font-size:50px;padding-top:10px;margin-right:10px; float:right;}
.music_item.active .choose {display:block;}
/*.btn_div{height:90px;font-size:40px;border-top:solid 2px #ccc;position:fixed;bottom:0px;width:100%;background-color:#fff;}
.btn_div i{margin:0 25px;padding-top:20px;}
.btn_div .fa-times{color:red;float:left;}
.btn_div .fa-check{color:green;float:right;}*/
</style>
<%--<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>--%>
<script>
$(function () {
        $(".pagination").css("top", "19px");
    var music = parent.page.music;
    if (music.data && music.data.src && music.data.src != "") {
        var $music = $($("span[data-vpath='" + music.data.src + "']").parent(".music_item").get(0));
        $music.addClass("active");
    } else {
        $(".nomusic").addClass("active");
    }
})
var music = {};
music.$audio = $('#test_audio');
music.play = function (obj) {
    var $obj = $(obj);
    var vpath = $obj.find(".rad").data("vpath");
    if ($obj.hasClass("active")) {
        if (!vpath || vpath == "") { return;}
        if ($obj.hasClass("play")) {
            $obj.removeClass('play');
            music.$audio[0].pause();
        } else {
            $obj.addClass('play');
            PlayMusic(vpath);
        }
    }
    else {
        $(".active").removeClass("active").removeClass('play');
        $obj.addClass("active").addClass('play');
        PlayMusic(vpath);
    }
    setMusic();
}
function PlayMusic(vpath) {
    music.$audio.attr("src", vpath);
    if (vpath && vpath != "") {
        music.$audio[0].play();
    }
}
function setMusic() {
    var vpath = $(".active").find(".rad").data("vpath");
    if (vpath && vpath != "") {
        parent.page.music.set({ src: vpath });
    } else {
        parent.page.music.clear();
    }
}
$(".music_item").click(function () {
    music.play(this);
});
$("#search_btn").click(function () {  $("#Search_B").click(); } );
$('#Skey_T').bind('input propertychange', function () { changInput() });
$('#Skey_T').change(function () { changInput(); });
function changInput() {
    if ($('#Skey_T').val().length > 0) { $("#icon").removeClass("fa-search").addClass("fa-close"); }
    else { $("#icon").removeClass("fa-close").addClass("fa-search"); }
}
$("#icon").click(function () { if ($(this).hasClass("fa-close")) { $("#Skey_T").val(""); changInput(); } });
</script>
</asp:Content>