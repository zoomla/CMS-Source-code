<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addsite.aspx.cs" Inherits="Design_mobile_addsite" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title><asp:Literal runat="server" ID="Title_L"></asp:Literal></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="page_list" class="page page-current">
    <header class="bar bar-nav">
        <a href="/design/user/mbsite/default.aspx" class="icon icon-left pull-left external"></a>
        <h1 class="title"><asp:Label runat="server" ID="Head_L"></asp:Label></h1>
    </header>
    <div class="content">
        <div class="list-block" style="margin:0;">
            <ul>
                <li>
                    <div class="item-content">
                        <div class="item-media"><i class="fa fa-book"></i></div>
                        <div class="item-inner">
                            <div class="item-input">
                                <asp:TextBox  runat="server" ID="SiteName_T" placeholder="站点名"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="item-content">
                        <div class="item-media"><i class="fa fa-list"></i></div>
                        <div class="item-inner">
                            <div class="item-input">
                                <asp:DropDownList runat="server" ID="Tlp_DP" DataTextField="TlpName" DataValueField="ID"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="item-content">
                        <div class="item-media"><i class="fa fa-picture-o"></i></div>
                        <div class="item-inner">
                            <div class="item-input" onclick="picup.sel();" style="padding:5px 0px;">
                                <img src="/Plugins/Ueditor/dialogs/image/images/image.png" runat="server" id="siteimg" style="max-width:80%;" />
                                <asp:HiddenField runat="server" ID="SiteImg_Hid"/>
                            </div>
                        </div>
                    </div>
                </li>
            <li>
                    <div class="item-content" style="padding-top:5px;">
                        <ul class="bgcolor list-unstyled">
                            <li class="active" style="background-color: #fff;"></li>
                            <li style="background-color: #00e0ff;"></li>
                            <li style="background-color: #ff0000;"></li>
                            <li style="background-color: #fff200;"></li>
                            <li style="background-color: #e500ff;"></li>
                            <li style="background-color: #00ff0d;"></li>
                            <li style="background-color: #00f;"></li>
                            <li style="background-color: #000;"></li>
                            <li style="background-color: #28c3fc;"></li>
                            <li style="background-color: #f0f0f0;"></li>
                            <div class="clearfix"></div>
                        </ul>
                        <asp:HiddenField runat="server" ID="Color_Hid" Value="#fff" />
                    </div>
                </li>
            </ul>
        </div>
        <div class="content-block">
            <div class="row">
                <asp:Button runat="server" ID="Save_Btn" Text="提交" OnClick="Save_Btn_Click" class="button button-big button-fill button-success back"/>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<link  href="/design/JS/sui/css/sm.min.css" rel="stylesheet" />
<style type="text/css">
.bgcolor li { display:inline-block; margin-right:13px; padding:1px; width:40px;height:40px; background:#000; border-radius:100%; border:solid 3px #ccc;}
.bgcolor li:hover{border:solid 3px #28c3fc;}
.bgcolor li.active{border:solid 3px #28c3fc;}
</style>
<script src="/design/h5/js/zepto.min.js"></script>
<script src="/design/JS/sui/js/sm.min.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<script src="/JS/Mobile/ResizeImg/lrz.js"></script>
<script src="/design/JS/Plugs/Helper/StyleHelper.js"></script>
<script>
picup.up_before = function () { Zepto.showPreloader(); };
picup.zip.enable = true;
picup.up_after = function (data) {
    $("#siteimg").attr("src", data);
    $("#SiteImg_Hid").val(data);
    Zepto.hidePreloader();
}
$(function () {
    $(".bgcolor li").click(function () {
        $(".bgcolor li").removeClass("active");
        $(this).addClass("active");
        $("#Color_Hid").val(StyleHelper.RGBTo16($(this).css("background-color")));
        console.log($("#Color_Hid").val());
    });
})
function NoMoreSite(msg) {
    Zepto.alert(msg, function () { location = "/design/user/mbsite/"; });
}
</script>
</asp:Content>
