//翻译工具
(function ($) {
    $.fn.TransTools = function (option) {
        var htmltlp = "<div id=\"@id_trans_div\" style=\" position:fixed;left:@leftpx;bottom:@bottompx;right:@rightpx;top:@toppx;width:500px;height:400px;display:none;\">"
+ "        <div class=\"panel panel-info\">"
+ "            <div class=\"panel-heading\">翻译工具 <span id=\"@id_close\" class=\"pull-right\" style=\"cursor:pointer;\"><i class=\"fa fa-remove\"></i></span></div>"
+ "          <div class=\"panel-body\">"
+ "              <div class=\"container-fluid\">"
+ "                    <div><textarea id=\"@id_text_t\" placeholder=\"请输入要翻译的内容!\" class=\"form-control\" style=\"height:150px;\"></textarea></div> "
+ "                    <div class=\"row margin_t5 option\">"
+ "                        <div class=\"col-md-1 col-lg-1\"></div>"
+ "                        <div class=\"col-md-4 col-lg-3 text-center\">"
+ "                            <select id=\"@id_source_sel\" class=\"form-control\">"
+ "                                <option value=\"en\">英文</option>"
+ "                                <option value=\"zh\">中文</option>"
+ "                                <option value=\"auto\">自动检测</option>"
+ "                                <option value=\"jp\">日语</option>"
+ "                                <option value=\"kor\">韩语</option>"
+ "                                <option value=\"fra\">法语</option>"
+ "                                <option value=\"th\">泰语</option>"
+ "                                <option value=\"ara\">阿拉伯语</option>"
+ "                                <option value=\"ru\">俄语</option>"
+ "                                <option value=\"yue\">粤语</option>"
+ "                                <option value=\"wyw\">文言文</option>"
+ "                                <option value=\"de\">德语</option>"
+ "                                <option value=\"it\">意大利语</option>"
+ "                                <option value=\"nl\">荷兰语</option>"
+ "                                <option value=\"el\">希腊语</option>"
+ "                            </select>"
+ "                        </div> "
+ "                        <div class=\"col-md-1 col-lg-2 text-center\">"
+ "                            <span style=\"font-size:2.5em;line-height:30px;\" class=\"fa fa-arrows-h\"></span>"
+ "                        </div>"
+ "                        <div class=\"col-md-4 col-lg-3 text-center\">"
+ "                            <select id=\"@id_target_sel\" class=\"form-control\">"
+ "                                <option value=\"en\">英文</option>"
+ "                                <option value=\"zh\">中文</option>"
+ "                                <option value=\"auto\">自动检测</option>"
+ "                                <option value=\"jp\">日语</option>"
+ "                                <option value=\"kor\">韩语</option>"
+ "                                <option value=\"fra\">法语</option>"
+ "                                <option value=\"th\">泰语</option>"
+ "                                <option value=\"ara\">阿拉伯语</option>"
+ "                                <option value=\"ru\">俄语</option>"
+ "                                <option value=\"yue\">粤语</option>"
+ "                                <option value=\"wyw\">文言文</option>"
+ "                                <option value=\"de\">德语</option>"
+ "                                <option value=\"it\">意大利语</option>"
+ "                                <option value=\"nl\">荷兰语</option>"
+ "                                <option value=\"el\">希腊语</option>"
+ "                            </select>"
+ "                        </div>"
+ "                        <div class=\"col-md-2 col-lg-2 text-center\">"
+ "                            <button type=\"button\" id=\"@id_trans_btn\" class=\"btn btn-info\">翻译</button>"
+ "                        </div>"
+ "                        <div class=\"col-md-1 col-lg-1\" style=\"line-height:30px;color:#999;\"><i class=\"fa fa-refresh fa-spin\" style=\"display:none;\"></i></div>"
+ "                    </div>"
+ "                    <div class=\"margin_t5\">"
+ "                        <textarea id=\"@id_tagtext_t\" class=\"form-control\" style=\"height:150px;\"></textarea>"
+ "                    </div>"
+ "                  <div class=\"text-right\">"
+ "                      <button type=\"button\" id=\"@id_cleardata_btn\" class=\"btn btn-info\">清空内容</button>"
+ "                      <a href=\"javascript:;\" id=\"@id_copy_btn\" class=\"btn btn-info\">复制内容</a>"
+ "                  </div>"
+ "                </div>"
+ "          </div>"
+ "        </div>"
+ "    </div>";
        var _ref = this, pre = "#" + _ref.attr('id') + "_";//前缀符
        var defconfig = {
            from: 'auto',//来源语种
            to: 'auto',//翻译语种
            top: 100,
            right: 30,
            left: '',
            bottom:''
        }
        var opts = $.extend(defconfig, option);
        //单例处理
        if ($(pre + "trans_div")[0]) { return; }
        $('body').append(htmltlp.replace(/@id/g, _ref.attr('id')).replace(/@top/g,opts.top).replace(/@right/g,opts.right).replace(/@left/g,opts.left).replace(/@bottom/g,opts.bottom));
        var diagobj = $(pre + "trans_div");//浮动窗口对象
        diagobj.find('.option div').css({ padding: 0 });//样式调整
        diagobj.find('.fa-spin').css({ 'animation-duration': '0.5s' });
        //根据配置选则默认语种
        $(pre + "source_sel option[value='" + opts.from + "']")[0].selected = true;
        $(pre + "target_sel option[value='" + opts.to + "']")[0].selected = true;
        InitEvent();
        function InitEvent() {//初始化事件
            _ref.click(function () {
                diagobj.toggle();
                if ($.fn.zclip) {//是否引用复制插件
                    $(pre + "copy_btn").unbind();
                    $(pre + "copy_btn").zclip({
                        path: '/JS/ZeroClipboard.swf',
                        copy: function () {
                            return $(pre + "tagtext_t").val();
                        },
                        afterCopy: function () { alert("复制完成"); }
                    });
                }
                else {
                    $(pre + "copy_btn").hide();
                }
            });
            $(pre + "cleardata_btn").click(function () {//清空数据
                $(pre + "text_t").val('');
                $(pre + "tagtext_t").val('');
            });
            $(pre + "text_t").keydown(function (evt) {//绑定回车事件
                var events = evt ? evt : window.event;
                if (events.keyCode == 13) {
                    $(pre + "trans_btn").click();
                    $(this).blur();
                }
            });
            $(pre + "close").click(function () { diagobj.hide(); });//显示隐藏窗口事件
            $(pre + "trans_btn").click(function () {//翻译操作
                diagobj.find('.fa-spin').show();
                $.post('/Common/API/Translation.ashx', { action: 'bdtrans', text: $(pre + "text_t").val(), from: $(pre + "source_sel option:checked").val(), to: $(pre + "target_sel option:checked").val() }, function (data) {
                    diagobj.find('.fa-spin').hide();
                    var result = JSON.parse(data);
                    console.log(result);
                    if (result.error_code) { alert(result.error_msg); return; }
                    $(pre + "tagtext_t").val(decodeURIComponent(result.trans_result[0].dst));
                });
            });
        }
    }
})(jQuery);