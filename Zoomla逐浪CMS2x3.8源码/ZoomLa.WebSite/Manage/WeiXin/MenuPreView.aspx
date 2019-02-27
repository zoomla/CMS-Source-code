<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuPreView.aspx.cs" Inherits="Manage_WeiXin_MenuPreView" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>微信菜单预览</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="background: url(/App_Themes/User/bg_mobile.png) no-repeat 0 0; width: 327px; height: 695px;">
        <div style="height: 150px;"></div>
        <div style="height: 382px;"></div>
        <div style="height: 152px;">
            <ul class="menuul"></ul>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
    .menuul {margin-left:65px;margin-right:36px;}
    .menuul li {border-left:1px solid #ddd;list-style-type:none;text-align:center;width:33.3%;float:left;}
    .menuul li>a{color:#616161;text-decoration:none;font-size:14px;line-height:38px;display:block;}
    .menuul li>a:hover {background-color:#edeaea;}
    .cbody {display:block;background:#fff;position:absolute;width:80px;bottom:165px; box-shadow:0 4px 20px 1px rgba(0,0,0,0.2);border-radius:3px;display:none;}
    .citem {font-size:12px;color:#616161;border-bottom:1px solid #ddd;text-align:center;padding:5px 0 5px 0;}
</style>
    <script>
        var menuMod = {
            //"menu": {
            //    "button": [
            //    { "name": "菜单一", "sub_button": [{ "type": "click", "name": "子菜一", "key": "menu_0_btn_0", "sub_button": [] }] },
            //    { "name": "菜单二", "sub_button": [{ "type": "click", "name": "子菜", "key": "menu_1_btn_0", "sub_button": [] }] },
            //    { "name": "菜单三", "sub_button": [{ "type": "click", "name": "子菜三", "key": "menu_2_btn_0", "sub_button": [] }] }]
            //}
        };
        $(function () {
            menuMod = JSON.parse($(parent.document).find("#<%:Request.QueryString["Cid"]%>").val());
            CreateMenu();
            $(".item_a").click(function () {
                var $cbody = $(this).closest("li").find(".cbody");
                var flag = $cbody.is(":hidden"); $(".cbody").hide();
                if (flag) { $cbody.show(); } else { $cbody.hide(); }
            });
        })
        /*----------------------------------------------------*/
        function CreateMenu() {
            var btnlist = menuMod.menu.button;
            var itemtlp = '<li><div class="cbody"><div></div></div><a href="javascript:;" class="item_a">@name</a></li>';
            for (var i = 0; i < btnlist.length; i++) {
                var btn = btnlist[i];
                var $li = $(itemtlp.replace("@name", btn.name));
                $(".menuul").append($li);
                CreateChild($li, btn);
            }
        }
        //创建子菜单
        function CreateChild($li, btn) {
            var $cbody = $li.find(".cbody");
            var citemtlp = '<div class="citem">@name</div>';
            for (var i = 0; i < btn.sub_button.length; i++) {
                $cbody.append(citemtlp.replace("@name", btn.sub_button[i].name));
            }
        }
    </script>
</asp:Content>