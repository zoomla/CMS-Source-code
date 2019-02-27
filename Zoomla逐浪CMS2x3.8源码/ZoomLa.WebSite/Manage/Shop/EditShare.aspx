<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditShare.aspx.cs" Inherits="Manage_Shop_EditShare" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>修改商品评论</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-hover table-striped shareorder">
        <tr class="share_tr">
            <td class="text-right td_m">标题:</td>
            <td><asp:TextBox ID="Title_T" runat="server" CssClass="form-control text_300"></asp:TextBox></td>
        </tr>
        <tr class="share_tr">
            <td class="text-right">评分:</td>
            <td>
                <div id="star_div">
                <i class="staricon fa fa-star-o" data-val="1"></i>
                <i class="staricon fa fa-star-o" data-val="2"></i>
                <i class="staricon fa fa-star-o" data-val="3"></i>
                <i class="staricon fa fa-star-o" data-val="4"></i>
                <i class="staricon fa fa-star-o" data-val="5"></i>
                <asp:HiddenField runat="server" id="star_hid" value="0"></asp:HiddenField>
                </div>
            </td>
        </tr>
        <tr>
            <td class="text-right">心得:</td>
            <td><asp:TextBox ID="MsgContent_T" placeholder="商品是否给力?快分享你的购买心得吧" runat="server" CssClass="form-control text_300" MaxLength="500" TextMode="MultiLine" Height="120"></asp:TextBox></td>
        </tr>
        <tr class="share_tr">
            <td class="text-right">晒单:</td>
            <td>
                <input type="button" id="upfile_btn" class="btn btn-info" value="添加图片" />
                <div style="margin-top: 10px;" id="uploader" class="uploader">
                    <ul class="filelist"></ul>
                </div>
                <asp:HiddenField runat="server" id="Attach_Hid"></asp:HiddenField>
            </td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="Edit_Btn" runat="server" Text="保存" OnClick="Edit_Btn_Click" CssClass="btn btn-primary" /> <a class="btn btn-primary" id="back_a" runat="server">返回</a></td>
        </tr>
    </table>
    <asp:HiddenField ID="Imgs_Hid" runat="server" />
    <style>
        .shareorder .staricon{font-size:20px; color:#ccc; cursor:pointer;}
        .staricon.fa-star {color:#FBA507;}
    </style>
    <link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
    <script src="/JS/jquery.validate.min.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/Controls/ZL_Webup.js"></script>
    <script>
        $().ready(function () {
            ZL_Webup.config.json.ashx = "action=OAattach";
            ZL_Webup.config.json.accept = "img";
            $("#upfile_btn").click(ZL_Webup.ShowFileUP);
            StarInit();
            var imgs = $("#Imgs_Hid").val().split('|');
            for (var i = 0; i < imgs.length; i++) {
                if (imgs[i] != "") {
                    AddAttach({}, { _raw: imgs[i] }, {});
                }
            }
        });
        function AddAttach(file, ret, pval) { return ZL_Webup.AddAttach(file, ret, pval); }
        //评星
        function StarInit() {
            $(".staricon").hover(function () {
                //fa-star-o空心,
                $(this).removeClass("fa-star-o").addClass("fa-star");
                $(this).prevAll(".staricon").removeClass("fa-star-o").addClass("fa-star");
            }, function () {
                StarByVal($("#star_hid").val());
            }).click(function () {
                $("#star_hid").val($(this).data("val"));
                StarByVal($(this).data("val"));
            });
            $("#star_div").ready(function () {
                var val = $("#star_hid").val();
                StarByVal(val);
            });
            //移出div块,除非已click,否则清除值
            $("#star_div").mouseleave(function () {
                var val = $("#star_hid").val();
                StarByVal(val);
            });
            //根据val点亮或熄灭评星
            function StarByVal(val) {
                if (val == "" || val == 0 || val == "0") { $(".staricon").removeClass("fa-star").addClass("fa-star-o"); }
                else
                {
                    var ref = $(".staricon[data-val=" + val + "]"); ref.removeClass("fa-star-o").addClass("fa-star");
                    ref.prevAll().removeClass("fa-star-o").addClass("fa-star");
                    ref.nextAll().removeClass("fa-star").addClass("fa-star-o");
                }
            }
        }
        function GoBank() {
            history.go(-1);
        }
        function HideShareTr() {
            $(".share_tr").hide();
        }
    </script>
</asp:Content>


