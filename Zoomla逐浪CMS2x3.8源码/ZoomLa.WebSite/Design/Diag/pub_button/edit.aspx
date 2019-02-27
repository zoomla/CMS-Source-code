<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Design_Diag_pub_button_edit" MasterPageFile="~/Design/Master/Edit.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>提交按钮</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
    <div class="panel panel-primary">
        <div class="panel-heading"><i class="fa fa-font"></i><span class="marginl5">按钮修改</span></div>
        <div class="panel-body" style="padding-left:0px;padding-right:0px;padding-top:0px;">
           <hr class="divider-long"/>
           <div class="control-section-divider labeled">样式设置</div>
           <hr class="divider-long"/>
            <div class="setting-row" data-group="indent">
                <div>
                    <label class="row-title">按钮文本</label></div>
                    <input type="text" id="text_t" class="form-control text_150">
            </div>
            <hr class="divider-long" />
<%--            <div class="setting-row">
                <div>
                    <label class="row-title">边框弧度</label>
                </div>
                <div id="rad_slider" class="slider_min"></div>
                <input type="text" id="rad_t" class="inputer min" />
            </div>--%>
            <hr class="divider-long"/>
            <div class="setting-row" data-group="indent">
                <div>
                    <label class="row-title">背景色</label></div>
                    <input type="text" id="bg_color_t" class="form-control text_150">
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
    var $btn = editor.model.instance.find("button");
    console.log(editor.model);
    //初始化
    $('#bg_color_t').val($btn.css('background-color'));
    $('#text_t').val(editor.model.dataMod.value);
    //文本
    $('#text_t').bind('input propertychange', function () {
        editor.model.dataMod.value = $(this).val();
        $btn.html($(this).val());
    });
    //边框弧度
    $("#rad_slider").slider({
        range: "min", min: 1, max: 150, value: 15,
        slide: function (event, ui) {
            $("#rad_t").val(ui.value);
            $btn.css("border-", ui.value + "px");
        }
    });
    //背景色
    $("#bg_color_t").ColorPickerSliders({
        flat: true, previewformat: 'rgb', order: {},
        onchange: function (container, color) {
            $btn.css("background-color", color.tiny.toRgbString());
        }
    });
</script>
</asp:Content>
