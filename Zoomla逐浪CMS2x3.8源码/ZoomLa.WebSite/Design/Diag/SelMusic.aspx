<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelMusic.aspx.cs" Inherits="Design_Diag_SelMusic" MasterPageFile="~/Design/Master/Edit.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>选择音乐</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="sel_box">
        <div class="input-group pull-left">
            <asp:TextBox ID="Skey_T" placeholder="请输入音乐名称" runat="server" CssClass="form-control text_md" />
            <span class="input-group-btn">
                <asp:Button ID="Search_B" runat="server" Text="<%$Resources:L,搜索 %>" class="btn btn-info" OnClick="Search_B_Click" />
            </span>
        </div>
    </div>
    <ZL:ExGridView ID="EGV" runat="server" PageSize="20" ShowHeader="false" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="EGV_PageIndexChanging" CssClass="table table-bordered table-striped">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="text-center">
                <ItemTemplate><input type="radio" class="sel_rad" name="music_rad" value="<%#Eval("VPath") %>"/></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <a href="javascript:;" title="试听" data-vpath="<%#Eval("VPath") %>" class="play_wrap" onclick="music.play(this);" style="text-decoration:none;">
                        <i class="fa fa-play"></i>
                        <i class="fa fa-pause"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="name" />
        </Columns>
    </ZL:ExGridView>
    <div class="btn_div">
        <span><input type="button" value="确定选择" class="btn btn-info" onclick="setMusic();" />
        <input type="button" value="清除音乐" class="btn btn-danger" onclick="clearMusic();" />
        <input type="button" value="关闭窗口" class="btn btn-default" onclick="CloseSelf();" /></span>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
#sel_box{border: 1px solid #ddd;background: #f5f5f5;padding: 5px;border-radius: 4px;position: fixed;top: 0px;width: 100%;margin:5px auto;}
#sel_box .input-group{width:50%;}
#EGV{margin-top:55px;}
.play_wrap .fa-play {display:inline-block;}
.play_wrap .fa-pause {display:none;}
.play_wrap.active .fa-play{display:none;}
.play_wrap.active .fa-pause {display:inline-block;}
.btn_div{text-align:center;}
</style>
<script>
var music = {};
music.$audio = $('<audio src="" loop="" style="display: none; width: 0; height: 0;"></audio>');
music.play = function (obj) {
    var $obj = $(obj);
    if ($obj.hasClass("active")) {
        $obj.removeClass("active");
        music.$audio.attr("src", "");
    }
    else {
        $(".play_wrap").removeClass("active");
        $obj.addClass("active");
        music.$audio.attr("src", $obj.data("vpath"));
        music.$audio[0].play();
    }
}
function setMusic() {
    parent.page.music.set({ src: $("[name=music_rad]:checked").val() });
    CloseSelf();
}
function clearMusic() { parent.page.music.clear(); CloseSelf(); }
$(function () {
    var $first = $("input[name=music_rad]:first");
    if ($first.length > 0) {$first[0].checked=true; }
})
$("#EGV tr").click(function () {
    $(this).find(".sel_rad").prop("checked",true);
});
</script>
</asp:Content>