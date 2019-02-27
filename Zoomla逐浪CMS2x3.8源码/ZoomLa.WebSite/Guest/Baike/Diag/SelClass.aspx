<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelClass.aspx.cs" Inherits="Guest_Baike_Diag_SelClass" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link type="text/css" href="/App_Themes/Guest.css" rel="stylesheet"/>
<title>选择分类</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="main">
    <div class="list-group catediv" id="catediv1">
    <asp:Repeater runat="server" ID="RPT1">
        <ItemTemplate>
            <a href="javascript:;" class="list-group-item" data-id="<%#Eval("GradeID") %>"><%#Eval("GradeName") %>
                <%#Convert.ToInt32(Eval("ChildCount"))>0?"<span class='fa fa-chevron-right'></span>":"" %>
            </a>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div class="list-group catediv" id="catediv2"></div>
<div class="list-group catediv" id="catediv3"></div>
</div>
<asp:HiddenField runat="server" ID="Cate_Hid" />
<asp:HiddenField runat="server" ID="txtBtype_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
.catediv { width:200px;float:left;margin-right:10px;}
</style>
<script>
$(function () {
    $("#catediv1 a").click(function () {
        var id = $(this).data("id");
        $("#catediv2").html(""); $("#catediv3").html("");
        SelCate(id, 2);
    });
    CateBind();
})
function CateBind() {
    $(".catediv a").click(function () {
        $(".list-group-item").removeClass("active");
        $(this).closest(".list-group-item").addClass("active");
        //var id = $(this).data("id");
        //$("#txtBtype_Hid").val(id);
        //$("#txtBtype").val($(this).text());
    })
}
function SelCate(gid, layer) {
    var $obj = $("#catediv" + layer);
    var liTlp = " <a href='javascript:;' class='list-group-item' data-id='@GradeID'>@GradeName"
            + "@ChildCount</a>";
    var childTlp = "<span class='fa fa-chevron-right'</span>";
    $obj.html("");
    var gradelist = JSON.parse($("#Cate_Hid").val());
    for (var i = 0; i < gradelist.length; i++) {
        if (gradelist[i].ParentID == gid) {
            var tlp = liTlp.replace("@GradeID", gradelist[i].GradeID).replace("@GradeName", gradelist[i].GradeName);
            tlp = tlp.replace("@ChildCount", gradelist[i].ChildCount > 0 ? childTlp : "");
            $obj.append(tlp);
        }
    }
    if (layer == 2) {
        $("#catediv2 a").click(function () {
            var id = $(this).data("id");
            $("#catediv3").html("");
            SelCate(id, 3);
        });
    }
    CateBind();
}
</script>
</asp:Content>
