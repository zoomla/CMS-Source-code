<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.Text.edit" MasterPageFile="~/Design/Master/Edit.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>文本组件</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
   <div class="panel panel-primary">
       <div class="panel-heading"><i class="fa fa-font"></i><span class="marginl5">文本修改</span></div>
       <div class="panel-body" style="padding-left:0px;padding-right:0px;padding-top:0px;">
           <hr class="divider-long"/>
           <div class="control-section-divider labeled">样式设置</div>
           <hr class="divider-long"/>
           <div class="setting-row">
               <div><label class="row-title">字体大小(px)</label></div>
               <div id="font_slider" class="slider_min"></div>
               <input type="text" id="font_t" class="inputer min" />
           </div>
           <hr class="divider-long"/>
           <div class="setting-row">
               <div><label class="row-title">字体颜色</label></div>
               <input type="text" id="color_t" class="form-control text_150">
           </div>
		   <hr class="divider-long"/>
		   <div class="setting-row">
				<div><label class="row-title">文字内容</label></div>
				<textarea  id="content_t" class="form-control" style="height:100px;text-align:left;"></textarea>
		   </div>
           <hr class="divider-long"/>
           <div class="setting-row">
                <div ><label class="row-title">文本装饰</label></div>
                <div id="dec_body">
                    <label><input type="radio" name="dec_rad" value="none" />无</label>
                    <label><input type="radio" name="dec_rad" value="underline" />下划线</label>
                    <label><input type="radio" name="dec_rad" value="line-through" />中划线</label>
                    <label><input type="radio" name="dec_rad" value="overline" />上划线</label>
               </div>
           </div>
           <hr class="divider-long"/>
           <div class="setting-row">
                <div ><label class="row-title">字体加粗</label></div>
                <div id="weg_body">
                    <label><input type="radio" name="weg_rad" value="normal" />默认</label>
                    <label><input type="radio" name="weg_rad" value="bold" />粗体</label>
                    <label><input type="radio" name="weg_rad" value="bolder" />更粗</label>
                    <label><input type="radio" name="weg_rad" value="lighter" />更细</label>
               </div>
           </div>
           <hr class="divider-long"/>
           <div class="setting-row">
                <div ><label class="row-title">字体风格</label></div>
                <div id="font_body">
                    <label><input type="checkbox" id="fstyle"/>倾斜</label>
               </div>
           </div>
       </div>
   </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script>
        $(function () {
            $(".ui-buttonset").buttonset();
            //先设定值,再绑事件,避免重复触发
            $("#font_t").val(StyleHelper.ConverToInt(editor.dom.css("font-size")));
            $("#color_t").val(editor.dom.css("color"));
            {
                var val = editor.dom.css("text-decoration");
                StyleHelper.setRadVal("dec_rad", val);

                val = editor.dom.css("font-weight");
                StyleHelper.setRadVal("weg_rad", val);

                val = editor.dom.css("font-style");
                if (val == 'italic' || val == 'oblique') {
                    $("#fstyle").prop('checked',true);
                }
            }
            //文本值
            $("#content_t").text($.trim(editor.model.dataMod.text));
            //绑定事件,字体,颜色,位置,缩进
			$("#content_t").change(function () {
			    editor.model.dataMod.text = $(this).val();
			    editor.scope.$digest();
			})
            $("#font_slider").slider({
                range: "min", min: 1, max: 150, value: 15,
                slide: function (event, ui) {
                    $("#font_t").val(ui.value);
                    editor.dom.css("font-size", ui.value + "px");
                }
            });
            $("#font_t").change(function () {
                editor.dom.css("font-size", $("#font_t").val() + "px");
            });
            $("#color_t").ColorPickerSliders({
                flat: true, previewformat: 'rgb', order: {}, hsvpanel: true, grouping: false,
                onchange: function (container, color) {
                    editor.dom.css("color", color.tiny.toRgbString());
                }
            });
            $("#dec_body input[name=dec_rad]").click(function () {
                editor.dom.css("text-decoration", $(this).val())
            });
            $("#weg_body input[name=weg_rad]").click(function () {
                editor.dom.css("font-weight", $(this).val())
            });
            $("#fstyle").change(function () {
                var val = $(this).prop('checked');
                if (val) { editor.dom.css("font-style", 'italic'); }
                else { editor.dom.css("font-style", 'normal'); }
            });
        })
    </script>
</asp:Content>
