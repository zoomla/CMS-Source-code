/*
*关键字插件
*/

$.fn.extend({
    tabControl: function (options, tags) {
        var defOpt = {
            initTabCount: 1,
            maxTabCount: 10,
            tabMaxLen: 60,
            tabW: 90,
            tabH: 30,
            tipTOffset: 5,
            tipLOffset: 0,
        };
        var opts = $.extend(defOpt, options);
        var _tags = [];
        if (tags) {
            tags = tags.replace(/[^A-Za-z0-9_,\u4E00-\u9FA5]+/gi, "").replace(/^,+|,+$/gi, "");//将非中英文、数字、下划丝、逗号的其他字符都去掉，且不能以逗号开头与结束
            _tags = tags.split(',');
        }
        _tags = _tags.length > opts.maxTabCount ? _tags.slice(0, opts.maxTabCount - 1) : _tags;
        opts.initTabCount = opts.maxTabCount <= _tags.length ? _tags.length : _tags.length + (opts.maxTabCount - _tags.length > opts.initTabCount ? opts.initTabCount : opts.maxTabCount - _tags.length);
        var checkReg = /[^A-Za-z0-9_\u4E00-\u9FA5]+/gi;//匹配非法字符
        var initTab = function (obj, index) {//初始化标签输入
            var textHtml = "<input class='tabinput' id='tabinput' name='tabinput' style='width:" + opts.tabW + "px;height:30px; line-height:30px;' type='text'/>";
            obj.append(textHtml);
            if (_tags[index]) {
                var __inputobj = $("input[type='text'][name='tabinput']", obj).eq(index);
                __inputobj.val(_tags[index].substr(0, opts.tabMaxLen)).css("display", "none");
                compTab(obj, __inputobj, _tags[index].substr(0, opts.tabMaxLen));
            }
            $("input[type='text'][name='tabinput']:last", obj).bind("keydown blur click",
            function (event) {
                if (event.type == "click") {//这个事件处理函数会接收到一个事件对象(event)，可以通过它来阻止（浏览器）默认的行为。如果既想取消默认的行为「event.preventDefault()」，又想阻止事件起泡「event.stopPropagation()」，这个事件处理函数必须返回false。
                    return false;
                }
                if (event.keyCode == 13 || event.keyCode == 9 || event.keyCode == 32 || event.keyCode == 188 || event.type == "blur") {
                    event.preventDefault();//主要是為了tab鍵，不要讓當前元素失去焦點（即阻止（浏览器）默认的行为）
                    event.stopPropagation();
                    var inputObj = $(this);
                    var value = $(this).val().replace(/\s+/gi, "");
                    if ((event.keyCode == 13 || event.keyCode == 9 || event.keyCode == 32 || event.keyCode == 188) && value != "")//主要是處理IE
                        inputObj.data("isIEKeyDown", true);
                    if (event.type == "blur" && inputObj.data("isIEKeyDown")) {
                        inputObj.removeData("isIEKeyDown");
                        return;
                    }
                    if (value != "") {
                        if (obj.getStrLen(value)> opts.tabMaxLen) {
                            showMes($(this), "请输入1到" + opts.tabMaxLen + "个字符长度的标签(每个中文占两个字符)");
                            return;
                        }
                        var _match = value.match(checkReg);
                        if (!_match) {//重复检测
                            var valArr = obj.getTabVals();
                            for (var i = 0; i < valArr.length; i++) {
                                if (valArr[i].toLowerCase() == value.toLowerCase()) { inputObj.val(""); return; }
                            }
                        }
                        if (!_match) {
                            compTab(obj, inputObj, value);
                            if ($("input[type='text'][name='tabinput']", obj).length < opts.maxTabCount) {
                                if (!inputObj.data("isModify"))
                                    initTab(obj);
                                else if (!$("input[type='text'][name='tabinput']", obj).is(":hidden")) {
                                    initTab(obj);
                                }
                            }
                            $("input[type='text']:last", obj).focus();
                            hideErr();
                        }
                        else {
                            showMes(inputObj, "內容不能包含非法字符「{0}」！".replace("{0}", _match.join(" ")));
                        }
                    }
                    else {
                        if (event.type != "blur")
                            showMes(inputObj, "");
                    }
                }
            }).bind("focus",
            function () {
                hideErr();
            });
        }
        //完成标签编写
        var compTab = function (obj, inputObj, value) {
            inputObj.next("span").remove();//删除紧跟input元素後的span
            var _span = "<span name='tab' class='radius'>" + value + "<a class='deltab'>×</a></span>";
            inputObj.after(_span).hide();
            inputObj.next("span").find("a").click(function () {
                inputObj.next("span").remove();
                if (opts.onRemoveTab) { opts.onRemoveTab(inputObj.val()) }//删除事件
                inputObj.remove();
                if ($("span[name='tab']", obj).length == opts.maxTabCount - 1)
                    initTab(obj);
            });
            inputObj.next("span").dblclick(function () {
                inputObj.data("isModify", true).next("span").remove();
                inputObj.show().focus();
            });
            if (opts.onAddTab) { opts.onAddTab(value) }//添加事件
        }
        return this.each(function () {
            var jqObj = $(this);
            for (var i = 0; i < opts.initTabCount; i++) {
                initTab(jqObj, i);
            }
            jqObj.data("isInit", true);
            jqObj.click(function () {
                $("input[type='text'][name='tabinput']", jqObj).each(function () {
                    if ($(this).val() == "") {
                        $(this).focus();
                        return false;
                    }
                });
            });
        });
        function showMes(inputObj, mes) {
            var _offset = inputObj.offset();
            var _mesHtml = "<div id='errormes' class='radius_shadow alert alert-danger' style='position:absolute; left:" + (_offset.left + opts.tipLOffset) + "px;top:" + (_offset.top + opts.tabH + opts.tipTOffset) + "px;'>" + mes + "</div>";
            $("#errormes").remove();
            $("body").append(_mesHtml);
        }
        function hideErr() {
            $("#errormes").hide();
        }
        function showErr() {
            $("#errormes").show();
        }
    },
    getTabVals: function () {//获取当前容器所生成的tab值，结果是一维数组
        var obj = $(this);
        var values = [];
        obj.children("span[name=\"tab\"][class^=\"radius\"]").text(function (index, text) {
            var checkReg = /[^A-Za-z0-9_\u4E00-\u9FA5]+/gi;
            values.push(text.replace(checkReg, ""));
        });
        return values;
    },
    getStrLen: function (s) {
        var i = 0, len = 0;
        for (i; i < s.length; i++) {
            if (s.charAt(i).match(/[^\x00-\xff]/g) != null) {
                len += 2;
            } else {
                len++;
            }
        }
        return len;
    },
    ReFresh: function () {

    }
});