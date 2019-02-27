<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Design_Diag_Shape_edit" MasterPageFile="~/Design/Master/Edit.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>图形设置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="control-section-divider labeled">样式设置</div>
<hr class="divider-long" />
<div class="setting-row" data-group="indent">
    <div>
        <label class="row-title">颜色</label></div>
        <input type="text" id="color_t" class="form-control text_150">
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
$(function () {
    console.log(editor.dom);
    $("#color_t").ColorPickerSliders({
        size: 'sm', placement: 'right', swatches: false, sliders: false, hsvpanel: true, previewformat: "hex",
        onchange: function (container, color) {
            editor.config.color = color.tiny.toHexString();
            //editor.dom.css("color", color.tiny.toHexString());
        }
    });
})
</script>
</asp:Content>

