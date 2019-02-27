<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddWorks.aspx.cs" Inherits="Manage_Copyright_AddWorks" MasterPageFile="~/Manage/I/Default.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link href="/App_Themes/V3.css" rel="stylesheet" />
<title>添加作品</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
<div class="margin_t5">
    <div class="input-group text_500">
        <ZL:TextBox runat="server" ID="Title_T" CssClass="form-control text_500" placeholder="标题" AllowEmpty="false" />
        <span class="input-group-btn">
            <input type="button" value="选择内容" onclick="SelContent();" class="btn btn-info"  />
        </span>
    </div>
</div>
<div class="margin_t5">
    <asp:TextBox runat="server" ID="content_t" style="height:260px;" TextMode="MultiLine"></asp:TextBox>
</div>
<div class="margin_t5">
<table class="table table-condensed table-bordered">
    <tr><td rowspan="2" style="width:130px;">选择授权规则：</td>
        <td>
            <label class="chkLabel"><input type="checkbox" checked="checked" onchange="repChange(this);" />转载使用</label>
            <ZL:TextBox runat="server" ID="RepPrice_T" CssClass="form-control text_300" Text="0" ValidType="FloatZeroPostive" />
        </td>
        <td><input type="button" class="btn btn-info" value="设置价格模板" style="display:none;" /></td>
    </tr>
    <tr>
        <td>
            <label class="chkLabel"><input type="checkbox" checked="checked" onchange="matChange(this);" />素材使用</label>
            <ZL:TextBox runat="server" ID="MatPrice_T" CssClass="form-control text_300" Text="0" ValidType="FloatZeroPostive" />
        </td>
        <td><a href="http://www.baidu.com" target="_blank" class="btn btn-info" style="width:110px;display:none;">进入版权印</a></td>
    </tr>
</table>
</div>
<div class="margin_t5">
<asp:Button runat="server" ID="Add_Btn" Text="发表贴子" CommandArgument="add" CssClass="btn btn-lg btn-primary" OnClick="Add_Btn_Click" />
<%--<asp:Button runat="server" ID="Draft_Btn" Text="存为草稿" CommandArgument="draft" CssClass="btn btn-lg btn-info"  OnClick="Add_Btn_Click" style="margin-left:5px;"/>--%>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
.chkLabel{font-weight:normal;font-size:14px;}
.chkLabel input {margin-right:2px;position:relative;top:-3px;}
</style>
<script src="/Plugins/Ueditor/ueditor.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<%=Call.GetUEditor("content_t",2) %>
<script>
var repChange = function (chk) {
    if ($(chk).is(':checked')) {
        $("#RepPrice_T").attr("disabled", false);
        $("#RepPrice_T").val("0");
    }
    else {
        $("#RepPrice_T").attr("disabled", true);
        $("#RepPrice_T").val("");
    }
}
var matChange = function () {
    if ($(chk).is(':checked')) {
        $("#MatPrice_T").attr("disabled", false);
        $("#MatPrice_T").val("0");
    }
    else {
        $("#MatPrice_T").attr("disabled", true);
        $("#MatPrice_T").val("");
    }
};
function SelContent()
{
    ShowComDiag("/Common/Dialog/SelContent.aspx", "选择内容");
}
function GetContent(content, title, gid) {
    location = siteconf.path + "CopyRight/AddWorks.aspx?gid=" + gid;
}
function CloseDiag() {
    CloseComDiag();
}
</script>
</asp:Content>