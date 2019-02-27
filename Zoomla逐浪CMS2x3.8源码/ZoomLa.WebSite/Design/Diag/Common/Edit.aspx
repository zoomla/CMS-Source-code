<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Design_Diag_Common_Edit" MasterPageFile="~/Design/Master/Edit.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>属性设置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs" role="tablist">
        <li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">基本</a></li>
        <li role="presentation"><a href="#option" aria-controls="option" role="tab" data-toggle="tab">高级</a></li>
    </ul>
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="home">
            <div class="control-section-divider labeled">字体设置</div>
            <hr class="divider-long" />
            <div class="setting-row">
                <div>
                    <label class="row-title">字体大小(px)</label></div>
                <div id="font_slider" class="slider_min"></div>
                <input type="text" id="font_t" class="inputer min" />
            </div>
            <hr class="divider-long" />
            <div class="setting-row">
                <div>
                    <label class="row-title">字体选择</label></div>
                <div class="btn-group" id="fontstyle_dp">
                    <button type="button" class="btn btn-default dropdown-toggle text_200 text-left" data-toggle="dropdown" aria-expanded="false">
                        <span class="font_text">请选择字体!</span><span class="pull-right" style="height: 20px; padding-top: 8px;"><span class="caret"></span></span>
                    </button>
                    <ul class="dropdown-menu" style="width: 200px; max-height: 200px; overflow: auto;" role="menu">
                        <li><a href="javascript:;" style="font-family: Arial">Arial</a></li>
                        <li><a href="javascript:;" style="font-family: 'Microsoft YaHei'">微软雅黑</a></li>
                        <li><a href="javascript:;" style="font-family: 'Microsoft JhengHei'">微软正黑体</a></li>
                        <li><a href="javascript:;" style="font-family: 'KaiTi'">楷体</a></li>
                        <li><a href="javascript:;" style="font-family: FangSong">仿宋</a></li>
                        <li><a href="javascript:;" style="font-family: NSimSun">新宋体</a></li>
                        <li><a href="javascript:;" style="font-family: SimSun">宋体</a></li>
                        <li><a href="javascript:;" style="font-family: SimHei">黑体</a></li>
                        <li><a href="javascript:;" style="font-family: DFKai-SB">标楷体</a></li>
                        <li><a href="javascript:;" style="font-family: MingLiU">细明体</a></li>
                        <li><a href="javascript:;" style="font-family: PMingLiU">新细明体</a></li>
                        <li><a href="javascript:;" style="font-family: 'Comic Sans MS'">Comic Sans MS</a></li>
                        <li><a href="javascript:;" style="font-family: Impact">Impact</a></li>
                        <li><a href="javascript:;" style="font-family: 'Lucida Sans Unicode'">Lucida Sans Unicode</a></li>
                        <li><a href="javascript:;" style="font-family: 'Trebuchet MS'">Trebuchet MS</a></li>
                        <li><a href="javascript:;" style="font-family: Verdana">Verdana</a></li>
                        <li><a href="javascript:;" style="font-family: Georgia">Georgia</a></li>
                    </ul>
                </div>
            </div>
            <hr class="divider-long" />
            <div class="setting-row">
                <div><label class="row-title">字体颜色</label></div>
                <input type="text" id="color_t" class="form-control text_150">
            </div>
            <hr class="divider-long" />
            <div class="setting-row">
                <div>
                    <label class="row-title">文本对齐</label></div>
                <div id="align_body">
                    <label><input type="radio" name="align_rad" value="left" />左对齐</label>
                    <label><input type="radio" name="align_rad" value="center" />居中</label>
                    <label><input type="radio" name="align_rad" value="right" />右对齐</label>
                </div>
            </div>
            <hr class="divider-long" />
            <div class="setting-row" data-group="indent">
                <div>
                    <label class="row-title">是否缩进</label></div>
                <div id="indent_body">
                     <label><input type="radio" name="indent_rad" value="no" />不缩进</label>
                     <label><input type="radio" name="indent_rad" value="yes" />缩进</label>
                </div>
            </div>
            <hr class="divider-long" />
            <div class="control-section-divider labeled">样式设置</div>
            <hr class="divider-long" />
            <div class="setting-row" data-group="indent">
                <div>
                    <label class="row-title">背景色</label></div>
                <input type="text" id="bg_color_t" class="form-control text_150">
            </div>
            <div class="setting-row">
                <div>
                    <label class="row-title">透明度</label></div>
                <div id="opacity_slider" class="slider_min"></div>
                <input type="text" id="opacity_t" class="inputer min" />
            </div>
            <div class="setting-row" data-group="indent">
                <div>
                    <label class="row-title">背景图片</label></div>
                <div class="diy-control image">
                    <div id="bg_div" class="needupimg"></div>
                </div>
            </div>
            <hr class="divider-long" />
            <div class="setting-row">
                <div>
                    <label class="row-title">布局</label></div>
                <div id="bodyid_div">
                    <label><input type="radio" name="body_rad" value="mainBody" />全屏</label>
                    <label><input type="radio" name="body_rad" value="midBody" />居中</label>
                </div>
            </div>
            <div class="setting-row" data-group="indent">
                <div><label class="row-title">定位</label></div>
                <div class="pos_div">
                    <div class="preset pull-left">
                        <a class="left-top" data-pos="left top"><i class="fa fa-arrow-up"></i></a>
                        <a class="top" data-pos="top"><i class="fa fa-arrow-up"></i></a>
                        <a class="right-top" data-pos="right top"><i class="fa fa-arrow-up"></i></a>
                        <a class="left" data-pos="left"><i class="fa fa-arrow-left"></i></a>
                        <a class="center" data-pos="center"><i class="fa fa-arrows-alt"></i></a>
                        <a class="right" data-pos="right"><i class="fa fa-arrow-right"></i></a>
                        <a class="left-bottom" data-pos="left bottom"><i class="fa fa-arrow-up"></i></a>
                        <a class="bottom" data-pos="bottom"><i class="fa fa-arrow-down"></i></a>
                        <a class="right-bottom" data-pos="right bottom"><i class="fa fa-arrow-up"></i></a>
                    </div>
                    <div class="posset pull-left">
                        <div><span>横向:</span>
                            <input type="text" id="posX_t" class="form-control text_50 int" /></div>
                        <div><span>纵向:</span>
                            <input type="text" id="posY_t" class="form-control text_50 int" /></div>
                    </div>
                </div>
                <div class="pos_repeat_div">
                    <div class="inline_div">
                        <span>重复:</span>
                        <div class="btn-group" data-toggle="buttons">
                            <label class="btn btn-default active" data-bind="background-repeat" data-param="repeat">
                                <input type="radio" name="options" autocomplete="off" checked><span class="fa fa-th"></span>
                            </label>
                            <label class="btn btn-default" data-bind="background-repeat" data-param="repeat-x">
                                <input type="radio" name="options" autocomplete="off"><span class="fa fa-ellipsis-h"></span>
                            </label>
                            <label class="btn btn-default" data-bind="background-repeat" data-param="repeat-y">
                                <input type="radio" name="options" autocomplete="off"><span class="fa fa-ellipsis-v"></span>
                            </label>
                            <label class="btn btn-default" data-bind="background-repeat" data-param="no-repeat">
                                <input type="radio" name="options" autocomplete="off"><span class="fa fa-remove"></span>
                            </label>
                        </div>
                    </div>
                    <div class="inline_div">
                        <span>固定:</span>
                        <div class="btn-group" data-toggle="buttons">
                            <label class="btn btn-default active" data-bind="background-attachment" data-param="fixed">
                                <input type="radio" name="attachment" autocomplete="off" /><span class="fa fa-unlock-alt"></span>
                            </label>
                            <label class="btn btn-default" data-bind="background-attachment" data-param="scroll">
                                <input type="radio" name="attachment" autocomplete="off" /><span class="fa fa-lock"></span>
                            </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="setting-row" data--group="indent">
                <div>
                    <label class="row-title">边框</label></div>
                <div class="border_div">
                    <div class="borderset_div pull-left">
                        <div>
                            <span>宽度:</span>
                            <input type="text" id="border_width" class="form-control text_80 int" />
                        </div>
                        <div class="margin_t5">
                            <span>颜色:</span>
                            <input type="text" id="border_color" class="text_80" />
                        </div>
                        <div class="margin_t5">
                            <span>样式:</span>
                            <div class="btn-group border_style" data-toggle="buttons">
                                <label class="btn btn-default" data-param="solid">
                                    <input type="radio" name="border" autocomplete="off" /><span style="margin-top: 7px; border: 1px solid #000; width: 20px; display: inline-block;"></span>
                                </label>
                                <label class="btn btn-default" data-param="dashed">
                                    <input type="radio" name="border" autocomplete="off" /><span style="margin-top: 7px; border: 1px dashed #000; width: 20px; display: inline-block;"></span>
                                </label>
                                <label class="btn btn-default" data-param="dotted">
                                    <input type="radio" name="border" autocomplete="off" /><span style="margin-top: 7px; border: 1px dotted #000; width: 20px; display: inline-block;"></span>
                                </label>
                                <label class="btn btn-default" data-param="none">
                                    <input type="radio" name="border" autocomplete="off" /><span class="fa fa-remove"></span>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--<div class="setting-row" data-group="indent">
                <div>
                    <label class="row-title">边框</label></div>
                <div class="border_div">
                    <div class="boderpos_div pull-left" data-toggle="buttons">
                        <label class="btn btn-default border_left" data-param="left">
                            <input type="radio" name="border" autocomplete="off" />
                        </label>
                        <label class="btn btn-default border_top" data-param="top">
                            <input type="radio" name="border" autocomplete="off" />
                        </label>
                        <label class="btn btn-default border-right" data-param="right">
                            <input type="radio" name="border" autocomplete="off" />
                        </label>
                        <label class="btn btn-default border_bottom" data-param="bottom">
                            <input type="radio" name="border" autocomplete="off" />
                        </label>
                        <label class="btn btn-default border_center" data-param="all">
                            <input type="radio" name="border" autocomplete="off" />
                            All 
                        </label>
                    </div>
                    <div class="borderset_div pull-left">
                        <div>
                            <span>宽度:</span>
                            <input type="text" id="border_width" class="form-control text_80 int" />
                        </div>
                        <div class="margin_t5">
                            <span>颜色:</span>
                            <input type="text" id="border_color" class="text_80" />
                        </div>
                        <div class="margin_t5">
                            <span>样式:</span>
                            <div class="btn-group border_style" data-toggle="buttons">
                                <label class="btn btn-default" data-param="solid">
                                    <input type="radio" name="border" autocomplete="off" /><span style="margin-top: 7px; border: 1px solid #000; width: 20px; display: inline-block;"></span>
                                </label>
                                <label class="btn btn-default" data-param="dashed">
                                    <input type="radio" name="border" autocomplete="off" /><span style="margin-top: 7px; border: 1px dashed #000; width: 20px; display: inline-block;"></span>
                                </label>
                                <label class="btn btn-default" data-param="dotted">
                                    <input type="radio" name="border" autocomplete="off" /><span style="margin-top: 7px; border: 1px dotted #000; width: 20px; display: inline-block;"></span>
                                </label>
                                <label class="btn btn-default" data-param="none">
                                    <input type="radio" name="border" autocomplete="off" /><span class="fa fa-remove"></span>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
            <hr class="divider-long" />
        </div>
        <div role="tabpanel" class="tab-pane" id="option">
            <div class="control-section-divider labeled">自定义设置</div>
            <hr class="divider-long" />
            <div class="setting-row">
                <div>
                    <label class="row-title">Class属性设置</label></div>
                <input id="classattr_t" type="text" class="form-control " />
            </div>
            <hr class="divider-long" />
            <div class="setting-row">
                <div>
                    <label class="row-title">Style属性设置</label></div>
                <textarea id="styleattr_t" class="form-control" style="height: 150px;"></textarea>
            </div>
        </div>
    </div>

    <input type="file" id="upimg_file" style="display: none;" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript" src="/JS/ZL_Regex.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Webup.js"></script>
    <style type="text/css">
        .setting-row .diy-control.image {width:150px; height:90px;overflow:hidden;border:solid 1px #ccc;cursor:pointer;
            background-image:url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAgAAAAICAMAAADz0U65AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAAZQTFRFsbGx1dXVfSyRngAAABdJREFUeNpiYAACRiBgwMMgJA8EAAEGAATIACGnjpkGAAAAAElFTkSuQmCC);
        }
        .setting-row .needupimg {width:150px; height:90px;
                                background:url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADkAAAAuCAYAAACSy7GmAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyNpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNS1jMDE0IDc5LjE1MTQ4MSwgMjAxMy8wMy8xMy0xMjowOToxNSAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIChNYWNpbnRvc2gpIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjBEQUEwM0ZENjQ2NDExRTM4RDIzOTlFMzJCNERBN0E3IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjBEQUEwM0ZFNjQ2NDExRTM4RDIzOTlFMzJCNERBN0E3Ij4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6MERBQTAzRkI2NDY0MTFFMzhEMjM5OUUzMkI0REE3QTciIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6MERBQTAzRkM2NDY0MTFFMzhEMjM5OUUzMkI0REE3QTciLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz74xDmeAAACTUlEQVR42mL08PD4zzDMARPDCACjnhz15KgnRz1Jd8BCQF4PiJ8MAX/IAPElcj35EYjfDwFP8lISk6QARiDWBGJJIH4KxDeGW56MBuL7QHwViPcA8XUgvgXEgcPFk7VAvASI5dHEVYF4LRDnDHVPmgFxI4Ek3A9NxuQCw4H2ZBbUI4TyfRoF7gOlEq+B9KQRkerMyTTfF4i1gLiLkkKSUk8y0jg7VUBpbSBOGihPXiBS3VkyzLYDYgskfiOh+pBWnpxJhJp/QDyLDLPL0fgSQFw6EJ48AsQ9BNTUAfFlEs0FNSc9sYgXA7H0QNSToNDNAOLXaOKgVk8sELeSaSa2/M4FxM0kFxwExnhAFfwjEhr7JkAsBsTPgfgcEP8lw4MgO+/gKU3/QUv1i0hickD8kB5t1z9AfIIK5hQTcBcTNIu4DtX+pAgQpxChzgWt5B1SnswFYk4i1QoPVk+C8lsfDo/wAHH2UB/+iIEWFoVAvB6I2dHkU0iJncHmSUEgXgHEi4GYHyrmDsTrgJgNymeFep5hKHrSGRp74VjkQD2LNVCPRkKrAZoAFhqZyw5tBBQSCEhQL2M5EGvQMqQp9SSo+ZUKbVc+AOKVQPwDiJdC5YgBQbTOL5R40gmItyPlKxAogbZymAdTvURunlSBFiZsWOQGlQfJ9SSohNwExKIMQwSQ6klQLK1ioGxgatB7cgIQuzEMMcBCYjIFdX7TB4nbL9PCkx/JHMYYsqXrsEqu/NC252AH/JR48tJwiMnR6fRRT456ctSTo56kBQAIMAC9dkxafAcmugAAAABJRU5ErkJggg==) center center no-repeat
        }
        .margin_t5{margin-top:5px;}
        .pos_div{height:85px;}
        .preset{width:90px;}
        .preset a{display:block;float:left;padding:5px;border:1px solid #ddd;margin:1px;width:25px;}
        .preset .left-top i{-webkit-transform:rotate(-45deg);transform:rotate(-45deg)}
        .preset .right-top i{-webkit-transform:rotate(45deg);transform:rotate(45deg)}
        .preset .left-bottom i{-webkit-transform:rotate(-135deg);transform:rotate(-135deg)}
        .preset .right-bottom i{-webkit-transform:rotate(135deg);transform:rotate(135deg)}
        .posset div{margin-top:5px;}
        .pos_repeat_div{margin-top:10px;}
        .pos_repeat_div .btn{width:38px;}
        .border_div{height:100px;}
        .boderpos_div{width:84px; position:relative;height:85px;}
        .boderpos_div .btn{padding:0px;}
        .boderpos_div .border_left{position:absolute;top:12px;left:0;height:60px;width:12px;}
        .boderpos_div .border-right{position:absolute;right:0px; height:60px;top:12px;width:12px;}
        .boderpos_div .border_top{position:absolute;left:12px;width:60px; top:0px;height:12px;}
        .boderpos_div .border_bottom{position:absolute;left:12px;width:60px;bottom:0px; height:12px;}
        .boderpos_div .border_center{position:absolute;left:28px;top:25px;padding:5px;border-radius:0;}
        /*.border_div .borderset_div{margin-left:30px;}*/
        .border_style .btn{width:50px; height:30px;}
        .inline_div{display:inline-block;margin-right:10px;}
        .divider-long{width:100%;}
    </style>
    <script>
        //边框暂缓,margin,padding需要置入,背景图片
        $(function () {
            ZL_Regex.B_Num('.int');
            $(".ui-buttonset").buttonset();
            //先设定值,再绑事件,避免重复触发
            $("#font_t").val(StyleHelper.ConverToInt(editor.dom.css("font-size")));
            $("#color_t").val(editor.dom.css("color"));
            if(editor.dom.find(".pub").length>0){  $("#bg_color_t").val(editor.dom.find(".pub").css("background-color"));}
            else
            {
                $("#bg_color_t").val(editor.dom.css("background-color"));
            }
          
            $("#bg_img").attr("src", editor.dom.attr("background-image"));
            //$("#border_color_t").val(editor.dom.css("border-color"));
            {
                var val = editor.dom.css("text-align"); if (val == "start") { val = "left"; }
                StyleHelper.setRadVal("align_rad", val);
                val = StyleHelper.ConverToInt(editor.dom.css("text-indent")) == 0 ? "no" : "yes";
                StyleHelper.setRadVal("indent_rad", val);
                StyleHelper.setRadVal("body_rad", editor.model.config.bodyid)
            }
            //字体,颜色,位置,缩进
            $("#font_slider").slider({
                range: "min", min: 1, max: 150, value: 15,
                slide: function (event, ui) {
                    $("#font_t").val(ui.value);
                    editor.dom.css("font-size", ui.value + "px");
                }
            });
            $("#color_t").ColorPickerSliders({
                flat:true, previewformat:'rgb',order:{},hsvpanel:true,grouping:false,
                onchange: function (container, color) {
                    editor.dom.css("color", color.tiny.toRgbString());
                }
            });
            $("#bg_color_t").ColorPickerSliders({
                flat:true, previewformat:'rgb',order:{},hsvpanel:true,grouping:false,
                onchange: function (container, color) {
                    editor.dom.css("background-color", color.tiny.toRgbString());
                    editor.dom.find(".comp_contain").children().not('.comp_mask').css("background-color", color.tiny.toRgbString());
                }
            });
            //透明度
            var opacity=editor.dom.css("opacity");
            if(editor.model.config.type=="image"){opacity=editor.dom.find(".imgcomp").css("opacity");}
            opacity=(1 - opacity) * 100;
            $("#opacity_t").val(opacity+"%");
            $("#opacity_slider").slider({
                range: "min", min: 1, max: 100, value:opacity,
                slide: function (event, ui) {
                    $("#opacity_t").val(ui.value + "%");
                    switch(editor.model.config.type)
                    {
                        case "image":
                            editor.dom.find(".imgcomp").css("opacity", 1 - ui.value / 100);
                            break;
                        default:
                            editor.dom.css("opacity", 1 - ui.value / 100);
                            break;
                    }
                }
            });
            //----------------------
            $("#align_body input[name=align_rad]").click(function () {
                editor.model.instance.css("text-align", this.value);
            });
            $("#indent_body input[name=indent_rad]").click(function () {
                var val = "0px";
                if (this.value == "yes") { val = "2em"; }
                editor.model.instance.css("text-indent", val);
            });
            $("#bodyid_div input[name=body_rad]").click(function(){
                var bodyid=this.value;
                if(editor.model.config.bodyid==val){return;}
                top.IfrHelper.find("#"+bodyid).append(editor.model.instance);
                editor.model.config.bodyid=bodyid;
                console.log(editor.model.config.bodyid);
                NotifyUpdate();
            });
            $("#fontstyle_dp .font_text").text(editor.dom.css('font-family'));
            $("#fontstyle_dp a").click(function(){
                $("#fontstyle_dp .font_text").text($(this).css('font-family'));
                editor.dom.css('font-family',$(this).css('font-family'));
            });
            SetBackGround();
            SetBorderStyle();
            //自定义属性
            $("#classattr_t").val(editor.model.config.css);
            $("#styleattr_t").val(editor.model.config.style);
            $("#classattr_t").change(function(){
                editor.model.config.css=$(this).val();
                editor.dom.attr('class',$(this).val());
            });
            $("#styleattr_t").change(function(){
                editor.model.config.style=$(this).val();
                editor.dom.attr('style',$(this).val());
            });
        });

        //边框样式设置
        function SetBorderStyle(){
            var $pubs = editor.dom.find(".comp_contain").children('.pub');
            if($pubs.length>0){
                $(".border_style .btn[data-param='"+$pubs.css("border-style")+"']").click();
                $("#border_width").val($pubs.css('border-width'));
                $("#border_color").val($pubs.css('border-color'));
            }else{
                $(".border_style .btn[data-param='"+editor.dom.css("border-style")+"']").click();
                $("#border_width").val(editor.dom.css('border-width'));
                $("#border_color").val(editor.dom.css('border-color'));
            }

            $("#border_width").change(function(){
                if($pubs.length>0){ $pubs.css("border-width",$(this).val()+"px"); }
                else { editor.dom.css("border-width",$(this).val()+"px"); }
            });
            $("#border_color").ColorPickerSliders({
                size: 'sm', placement: 'right', swatches: false, sliders: false, hsvpanel: true,previewformat: "hex",
                onchange: function (container, color) {
                    if($pubs.length>0){ $pubs.css("border-color",color.tiny.toHexString()); }
                    else { editor.dom.css("border-color",color.tiny.toHexString()); }
                }
            });
            $(".border_style .btn").click(function(){
                if($pubs.length>0){ $pubs.css("border-style",$(this).data("param")); }
                else { editor.dom.css("border-style",$(this).data("param")); }
            });

            //var bordercss="";
            //$(".boderpos_div .btn").click(function(){
            //    bordercss=$(this).data("param")=="all"?"border":"border-"+$(this).data("param");
            //    $("#border_width").val(editor.dom.css(bordercss+"-width"));
            //    $(".border_style .btn[data-param='"+editor.dom.css(bordercss+"-style")+"']").click();
            //});
            //$("#border_width").change(function(){
            //    editor.dom.css(bordercss+"-width",$(this).val()+"px");
            //    editor.dom.find(".comp_contain").children().css(bordercss+"-width",$(this).val()+"px");
            //});
            //$(".border_style .btn").click(function(){
            //    editor.dom.css(bordercss+"-style",$(this).data("param"));
            //});
            //$("#border_color").ColorPickerSliders({
            //    size: 'sm', placement: 'right', swatches: false, sliders: false, hsvpanel: true,previewformat: "hex",
            //    onchange: function (container, color) {
            //        editor.dom.css(bordercss+"-color",color.tiny.toHexString());
            //    }
            //});
            //$(".boderpos_div .border_center").click();
        }

        //背景图片样式设置
        function SetBackGround(){
            $(".needupimg").click(function () {//自动base64上传,并回发路径
                $("#upimg_file").click();
            });
            $("#upimg_file").change(function () {
                //ajax上传文件
                SFileUP.AjaxUpFile("upimg_file",function(data){
                    $(".needupimg").css("background-image", "url(" + data + ")");
                    editor.dom.css("background-image", "url(" + data + ")");
                    InitBackGround();
                });
            });
            $("#posX_t").change(function(){editor.dom.css("background-position-x",$(this).val()+"px");$(".needupimg").css("background-position-x", $(this).val()+"px");});
            $("#posY_t").change(function(){editor.dom.css("background-position-y",$(this).val()+"px");$(".needupimg").css("background-position-y", $(this).val()+"px");});
            //背景图片定位设置
            $(".preset a").click(function () {
                var curX = parseInt(editor.dom.css("background-position-x"));
                var curY = parseInt(editor.dom.css("background-position-y"));
                var posarr = $(this).data('pos').split(' ');
                for (var i = 0; i < posarr.length; i++) {
                    switch (posarr[i]) {
                        case "left":
                            curX = curX - 1;
                            break;
                        case "top":
                            curY = curY - 1;
                            break;
                        case "right":
                            curX = curX + 1;
                            break;
                        case "bottom":
                            curY = curY + 1;
                            break;
                        case "center":
                            break;
                    }
                }
                $(".needupimg").css("background-position-x", curX);
                $(".needupimg").css("background-position-y", curY);
                editor.dom.css("background-position-x", curX);
                editor.dom.css("background-position-y", curY);
                $("#posX_t").val(curX);
                $("#posY_t").val(curY);
            });
            $(".pos_repeat_div .btn").click(function(){
                editor.dom.css($(this).data('bind'),$(this).data('param'));
                $(".needupimg").css($(this).data('bind'), $(this).data('param'));
            });
            //加载背景
            function InitBackGround(){
                if (editor.dom.css("background-image") != "none") {
                    $(".needupimg").css("background-image", editor.dom.css("background-image"))
                    $(".needupimg").css("background-position", editor.dom.css("background-position"));
                    $("#posX_t").val($(".needupimg").css("background-position-x"));
                    $("#posY_t").val($(".needupimg").css("background-position-y"));
                    $(".pos_repeat_div .btn[data-bind='background-repeat'][data-param='"+editor.dom.css("background-repeat")+"']").click();
                    $(".pos_repeat_div .btn[data-bind='background-attachment'][data-param='"+editor.dom.css("background-attachment")+"']").click();
                }
            }
            InitBackGround();
        }
    </script>
</asp:Content>
