<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.Button.edit" MasterPageFile="~/Design/Master/Edit.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head"><title>按钮设置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="panel panel-primary">
        <div class="panel-heading"><i class="fa fa-image"></i><span class="marginl5">按钮设置</span></div>
        <div class="panel-body" style="padding-left: 0px; padding-right: 0px; padding-top: 0px;">
            <hr class="divider-long" />
            <div class="control-section-divider labeled">按钮样式</div>
            <hr class="divider-long" />
            <div class="setting-row">
                <div>
                    <label class="row-title">按钮文本</label></div>
                <input type="text" id="value_t" class="form-control" />
            </div>
        </div>
        <div class="setting-row">
            <div>
                <label class="row-title">字体大小(px)</label>
            </div>
            <div id="font_slider" class="slider_min"></div>
            <input type="text" id="font_t" class="inputer min" />
        </div>
        <div class="setting-row" data-group="indent">
            <div>
                <label class="row-title">背景颜色</label>
            </div>
            <input type="text" id="bg_color_t" class="form-control text_150">
        </div>
        <div class="setting-row" data-group="indent">
            <div>
                <label class="row-title">文本颜色</label>
            </div>
            <input type="text" id="color_t" class="form-control text_150">
        </div>
        <div id="pub_button_div" style="display:none;">
            <div class="setting-row" data-group="indent">
                <div>
                    <label class="row-title">表单名称</label>
                </div>
                <input type="text" id="fname_t" class="form-control text_200">
            </div>
            <div class="setting-row" data-group="indent">
                <div>
                    <label class="row-title">提交表单后提示语</label>
                </div>
                <input type="text" id="prompt_t" class="form-control text_200">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script>
        $(function () {
            var $btn = editor.model.instance.find("button");
            $("#value_t").val(editor.model.dataMod.value);
            $("#font_t").val(StyleHelper.ConverToInt($btn.css("font-size")));
            $("#bg_color_t").val($btn.css("background-color"));
            $("#color_t").val($btn.css("color"));

            $("#value_t").blur(function () {
                editor.model.dataMod.value = this.value;
                editor.scope.$digest();
            });
            $("#font_slider").slider({
                range: "min", min: 1, max: 150, value: StyleHelper.ConverToInt($btn.css("font-size")),
                slide: function (event, ui) {
                    $("#font_t").val(ui.value);
                    $btn.css("font-size", ui.value + "px");
                }
            });
            $("#bg_color_t").ColorPickerSliders({
                size: 'sm', placement: 'right', swatches: false, sliders: false, hsvpanel: true, previewformat: "hex",
                onchange: function (container, color) {
                    $btn.css("background-color", color.tiny.toHexString());
                }
            });
            $("#color_t").ColorPickerSliders({
                size: 'sm', placement: 'right', swatches: false, sliders: false, hsvpanel: true, previewformat: "hex",
                onchange: function (container, color) {
                    $btn.css("color", color.tiny.toHexString());
                }
            });
            if (editor.model.config.type == "pub_button") {
                $("#pub_button_div").show();
                $("#fname_t").val(editor.model.config.fname);
                $("#prompt_t").val(editor.model.dataMod.click.prompt);
                $("#fname_t").blur(function () { editor.model.config.fname = this.value; editor.scope.$digest(); });
                $("#prompt_t").blur(function () { editor.model.dataMod.click.prompt = this.value; editor.scope.$digest(); });
            }
        });
    </script>
</asp:Content>
