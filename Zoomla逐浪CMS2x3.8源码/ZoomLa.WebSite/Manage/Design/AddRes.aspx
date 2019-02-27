<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddRes.aspx.cs" Inherits="Manage_Design_AddRes" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>资源管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
    <tr>
        <td class="td_m">资源名称</td>
        <td>
            <ZL:TextBox runat="server" ID="Name_T" CssClass="form-control text_300" AllowEmpty="false"></ZL:TextBox>
        </td>
    </tr>
    <tr><td>资源类别</td><td>
        <asp:DropDownList CssClass="form-control text_300" runat="server" ID="Type_DP">
            <asp:ListItem Text="图片" Value="image"></asp:ListItem>
            <asp:ListItem Text="音乐" Value="music"></asp:ListItem>
            <asp:ListItem Text="图标" Value="icon"></asp:ListItem>
            <asp:ListItem Text="形状" Value="shape"></asp:ListItem>
            <asp:ListItem Text="文字" Value="text"></asp:ListItem>
        </asp:DropDownList>
    </td></tr>
    <tr><td>使用场景</td><td>
        <asp:DropDownList CssClass="form-control text_300" runat="server" ID="Useage_Dp" >
            <asp:ListItem Text="动力模块" Value="bk_pc"></asp:ListItem>
            <asp:ListItem Text="H5场景" Value="bk_h5"></asp:ListItem>
        </asp:DropDownList>
    </td></tr>
    <tr><td>资源文件</td><td><ZL:SFileUp runat="server" ID="Res_UP" FType="All"/></td></tr>
    <tr><td>缩略图</td><td><asp:TextBox runat="server" ID="PreviewImg_T" CssClass="form-control text_300"></asp:TextBox></td></tr>
    <tr><td>用途</td><td><div id="use_div"></div></td></tr>
    <tr><td>功能</td><td><div id="fun_div"></div></td></tr>
    <tr><td>风格</td><td><div id="style_div"></div></td></tr>
    <tr>
        <td>状态</td>
        <td>
        <label><input type="radio" value="0" name="zstatus_rad" checked="checked" />正常</label>
        <label><input type="radio" value="1" name="zstatus_rad" />推荐</label>
        <label><input type="radio" value="-1" name="zstatus_rad" />停用</label>
    </td></tr>
    <tr>
        <td></td>
        <td>
            <asp:LinkButton runat="server" ID="Save_Btn" CssClass="btn btn-primary" OnClick="Save_Btn_Click">保存</asp:LinkButton>
            <a href="ResList.aspx" class="btn btn-default">取消</a>
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
.chkwrap {margin-right:5px;}
.chkwrap .chkitem{position:relative;top:-3px;}
</style>
<script src="/JS/SelectCheckBox.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script>
var usearr = "<%=resMod.UseArr%>".split(',');
var funarr = "<%=resMod.FunArr%>".split(',');
var stylearr = "<%=resMod.StyleArr%>".split(',');
var textarr = "<%=resMod.TextStyle%>".split(',');
var shapearr = "<%=resMod.ShapeStyle%>".split(',');
var iconarr = "<%=resMod.IconStyle%>".split(',');
function addchks(name, arr) {
    $("#" + name + "_div").html("");
    var list = [];
    var stlp = '<label class="chkwrap"><input type="checkbox" name="' + name + '_chk" value="@name" class="chkitem" />@name</label>';
    for (var i = 0; i < arr.length; i++) {
        list.push({ name: arr[i] });
    }
    $items = JsonHelper.FillItem(stlp, list, null);
    $("#" + name + "_div").append($items);
}
addchks("use", usearr);
addchks("fun", funarr);
addchks("style", stylearr);

$("#Type_DP").change(function () {
    var type = $(this).val();
    switch (type) {
        case "image":
        case "music":
            addchks("style",stylearr);
            break;
        case "icon":
            addchks("style", iconarr);
            break;
        case "shape":
            addchks("style", shapearr);
            break;
        case "text":
            addchks("style", textarr);
            break;
    }
});
</script>
</asp:Content>
