<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="test_test" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>在线WebFont</title>
    <link href="/App_Themes/V3.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="background-color: #F6F6F6;">
       <div class="container" style="padding:10px;">
           <asp:TextBox runat="server" TextMode="MultiLine" ID="T1" CssClass="form-control" Style="height: 119px; resize: none;" Text="结婚就是给自由穿件棉衣,活动起来不方便,但会很温暖" MaxLength="500" />
           <div style="margin-top: 5px;">
               <asp:DropDownList runat="server" ID="Font_DP" CssClass="form-control text_md pull-left">
                   <asp:ListItem Value="z1" Selected="True">逐浪创艺粗黑体</asp:ListItem>
                   <asp:ListItem Value="z2">逐浪创意含羞体</asp:ListItem>
                   <asp:ListItem Value="z3">逐浪创意沁竹体</asp:ListItem>
                   <asp:ListItem Value="z4">逐浪粗宋简体</asp:ListItem>
                   <asp:ListItem Value="z5">逐浪经典粗黑体</asp:ListItem>
                   <asp:ListItem Value="z6">逐浪时尚钢笔体</asp:ListItem>
                   <asp:ListItem Value="z7">逐浪细阁体</asp:ListItem>
                   <asp:ListItem Value="z8">逐浪硬行体</asp:ListItem>
                   <asp:ListItem Value="z9">逐浪创意粗行体</asp:ListItem>
                   <asp:ListItem Value="z10">逐浪创意流珠体</asp:ListItem>
                   <asp:ListItem Value="z11">逐浪大雪钢笔体</asp:ListItem>
                   <asp:ListItem Value="z12">逐浪雅宋体</asp:ListItem>
                   <asp:ListItem Value="z13">逐浪小雪钢笔体</asp:ListItem>
               </asp:DropDownList>
               <asp:Button runat="server" ID="CreateFont_Btn" class="btn btn-primary pull-right" Text="生成字体" OnClientClick="return checkFont();" OnClick="CreateFont_Btn_Click" />
           </div>
           <div class="clearfix"></div>
       </div>
    </div>
    <div class="container" style="margin-top: 30px; background-color: #F6F6F6; padding: 10px;" runat="server" id="result_div" visible="false">
        <div class="fontitem" style="font-family: demofont;" runat="server" id="result_t_div"></div>
        <asp:TextBox runat="server" TextMode="MultiLine" ID="r_quote_t" CssClass="form-control" Style="height: 150px;margin-top:5px;" />
    </div>
    <div id="fontlist" class="container margin_t10"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
#fontlist .item {background-color: #F6F6F6; height: 170px;border-radius:5px;padding:15px; margin-bottom:15px;}
#fontlist .item .body {min-height:110px;}
#fontlist .item .footer {margin-top:10px;}
</style>
<style type="text/css" runat="server" id="font_css"></style>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script>
function checkFont() {
    if (ZL_Regex.isEmpty($("#T1").val())) { alert("内容不能为空"); return false; }
    else
    {
        disBtn("CreateFont_Btn");
        $("#CreateFont_Btn").val("正在生成中,请等待...");
    }
}
$(function () {
    var $dp = $("#Font_DP");
    var list = [];
    $dp.find("option").each(function () {
        var $this = $(this);
        list.push({ txt: $("#T1").val(), family: $this.text() });
    })
    var stlp = "<div class=\"item\">"
                + "<div class=\"body\">"
                + "<img src=\"/Common/Label/FontToImg.aspx?txt=@txt&family=@family&size=30&bkcolor=ffffff\" />"
                + "</div>"
                + "<div class=\"footer\"><i class=\"fa fa-text-width fa-2x\"></i> <span style=\"font-weight:bold;\">@family</span></div>"
                + "</div>";
    var $items = JsonHelper.FillItem(stlp, list);
    $("#fontlist").append($items);
})
</script>
</asp:Content>
