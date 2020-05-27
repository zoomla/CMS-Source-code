var ZLForm = {
    Inputer: function (opts) {
        this.id = opts.id;
        this.ctype = "input_txt";
        this.sortnum = 1;
        this.title = opts.title ? opts.title : "单行文本";
        this.intro = opts.intro;
        this.required = opts.required;
        this.value = opts.value;
        this.txt_size = opts.txt_size;
        this.txt_type = opts.txt_type;
    },
    Select: function (opts) {
        this.id = opts.id;
        this.ctype = "select";
        this.sortnum = 1;
        this.title = opts.title ? opts.title:"单选项" ;
        this.intro = opts.intro;
        this.value = opts.value;
        this.required = opts.required;
        this.sel_option = opts.sel_option;
        this.sel_type = opts.sel_type;
    },
    Img: function (opts) {
        this.id = opts.id;
        this.ctype = "img";
        this.sortnum = 1;
        this.required = opts.required;
        this.title = opts.title ? opts.title:"图片" ;
        this.intro = opts.intro;
        this.src = opts.src;
        this.align = opts.align;
    },
    Str: function (opts) {
        this.id = opts.id;
        this.ctype = "str";
        this.sortnum = 1;
        this.title = opts.title;
        this.intro = opts.intro;
        this.align = opts.align;
    },
    //通用事件绑定
    EventBind: function ()
    {
        $("#title_edit_txt,#intro_edit_txt").keyup(function () {
            var $obj = $(event.srcElement);
            var type = $obj.data("sync");
            switch (type) {
                case "title"://标题
                    $cur.find(".title").text($obj.val());
                    $cur.instance.title = $obj.val();
                    break;
                case "intro"://简述
                    var v = $obj.val();
                    if (v == "") { $cur.find(".intro").hide(); }
                    else $cur.find(".intro").show();
                    $cur.find(".intro").text(v);
                    $cur.instance.intro = v;
                    break;
            }
        });
        //必填
        $("#required_chk").click(function () {
            var $obj = $(event.srcElement);
            var requiredTlp = "<span class='com_required'>*</span>";
            if ($obj[0].checked) {
                $cur.find(".title_field").after(requiredTlp);
            }
            else { $cur.find(".com_required").remove(); }
            $cur.instance.required = $obj[0].checked;
        });
        $("#txtalign_div :radio").click(function () {
            var $obj = $(event.srcElement);
            $cur.instance.align = $obj.val();
            $cur.find(".intro").css("text-align", $cur.instance.align);
        });
        new ZLForm.Inputer({}).EventBind();
        new ZLForm.Select({}).EventBind();
        new ZLForm.Img({}).EventBind();
    },
    AddFormli: function (mod) {
        switch (mod.ctype) {
            case "input_txt":
                var mtlp = ZLForm.TlpReplace($("#input_txtTlp").html(), mod);
                switch (mod.txt_type) {
                    case "input":
                        mtlp = mtlp.replace("@tlp", "<input type='text' class='input " + mod.txt_size + "'>")
                        break;
                    default:
                        mtlp = mtlp.replace("@tlp", "<textarea class='textarea " + mod.txt_size + "'></textarea>")
                        break;
                }
                $("#formul").append(mtlp);
                break;
            case "select":
                var mtlp = ZLForm.TlpReplace($("#selectTlp").html(), mod);
                var tlp = ""; var arr = mod.sel_option;
                if (mod.sel_type == "radio") {
                    tlp = "<ul>";
                    var liTlp = "<li data-sort='@sort'><input type='radio' name='@id_radio' value='@value'><label>@value</label></li>";
                    for (var j = 0; j < arr.length; j++) {
                        tlp += liTlp.replace("@sort", arr[j].id).replace(/@value/g, arr[j].value).replace("@id", mod.id);
                    }
                    tlp += "</ul>";
                }
                else if (mod.sel_type == "checkbox") {
                    tlp = "<ul>";
                    var liTlp = "<li data-sort='@sort'><input type='checkbox' name='@id_checkbox' value='@value'><label>@value</label></li>";
                    for (var j = 0; j < arr.length; j++) {
                        tlp += liTlp.replace("@sort", arr[j].id).replace(/@value/g, arr[j].value).replace("@id", mod.id);
                    }
                    tlp += "</ul>";
                }
                else {
                    var tlp = "<select id='@id_select'>".replace("@id", mod.id);
                    var opTlp = "<option value='@value' data-sort='@sort'>@value</option>";
                    for (var j = 0; j < arr.length; j++) {
                        tlp += opTlp.replace("@sort", arr[j].id).replace(/@value/g, arr[j].value);
                    }
                    tlp += "</select>";
                }
                $("#formul").append(mtlp.replace("@tlp", tlp));
                break;
            case "img":
                var mtlp = ZLForm.TlpReplace($("#imgTlp").html(), mod);
                var li = $("#formul").append(mtlp);
                $("#" + mod.id).find(".intro").css("text-align", mod.align);
                break;
            case "str":
                var mtlp = ZLForm.TlpReplace($("#strTlp").html(), mod);
                $("#formul").append(mtlp);
                $("#" + mod.id).find(".intro").css("text-align", mod.align);
                break;
        }//switch end;
    },
    TlpReplace: function (tlp, mod) {
        tlp = tlp.replace(/@id/g, mod.id).replace(/@title/g, mod.title).replace("@intro", mod.intro);
        if (tlp.indexOf("@src") > -1) tlp = tlp.replace("@src", mod.src);
        return tlp;
    },
    GetValue: function (mod) {
        var $cur = $("#"+mod.id);
        switch (mod.ctype)//img不处理
        {
            case "input_txt":
                switch (mod.txt_type)
                {
                    case "input":
                        mod.value = $cur.find(".input")[0].value;
                        break;
                    default:
                        mod.value = $cur.find(".textarea")[0].value;
                        break;
                }
                if (mod.value == "") return false;
                break;
            case "select":
                switch (mod.sel_type)
                {
                    case "radio":
                        mod.value = $cur.find("[name=" + mod.id + "_radio" + "]:checked")[0].value;
                        break;
                    case "checkbox":
                        mod.value = $cur.find("[name=" + mod.id + "_checkbox" + "]:checked").val();
                        break;
                    default://下拉
                        mod.value = $cur.find("[name=" + mod.id + "_select" + "]").val();
                        break;
                }
                if (mod.value == "") return false;
                break;
            case "img":
                mod.value = "";
                break;
            case "str":
                mod.value = "";
                break;
        }
        return true;
    }
}
//----------Input
ZLForm.Inputer.prototype = {
    inputTlp: "<input type='text' class='input large'/>",
    textTlp: "<textarea class='textarea large'></textarea>",
    htmlTlp: "<input type='text' class='input large'/>",
    UpdateFront: function () {
        $cur = GetCurobj();
        $cur.find(".content>input:text,textarea").remove();
        switch ($cur.instance.txt_type) {
            case "textarea":
                $cur.find(".content").append(this.textTlp);
                $cur.find("textarea").removeClass().addClass("textarea " + $cur.instance.txt_size);
                break;
            default:
                $cur.find(".content").append(this.inputTlp);
                $cur.find("input:text").removeClass().addClass("input " + $cur.instance.txt_size);
                break;
        }
    },
    Render: function () {
        var $obj = $(event.srcElement);
        var $cur = GetCurobj();//当前选中
        var type = $obj.data("sync");
        switch (type) {//这里的this为控件
            case "txt_size":
                $cur.instance.txt_size = $obj.val();
                break;
            case "txt_type":
                $cur.instance.txt_type = $obj.val();
                break;
        }
        $cur.instance.UpdateFront();
    },
    //将指定元素中的数据赋值到编辑框中,传入需要更改的obj或当前obj
    BeginEdit: function ($obj) {
        $editdiv = $("#EditDiv");
        $obj.instance.title = $obj.find(".title").text();
        $obj.instance.intro = $obj.find(".intro").text();
        $editdiv.find("#title_edit_txt").val($obj.instance.title)
        $editdiv.find("#intro_edit_txt").val($obj.instance.intro);
        $editdiv.find("#required_chk")[0].checked = $obj.instance.required;
        //----独有
        $obj.instance.value = $obj.find("input:text,textarea").val();
        $obj.instance.txt_size = $obj.find("input:text,textarea").attr("class").split(' ')[1];
        $obj.instance.txt_type = $obj.find("input:text").length > 0 ? "input" : "textarea";

        $editdiv.find("input:radio[value=" + $obj.instance.txt_size + "]")[0].checked = true;
        $editdiv.find("input:radio[value=" + $obj.instance.txt_type + "]")[0].checked = true;
    },
    EventBind: function () {
        $("#EditDiv input[data-sync]").click(this.Render);
    },
    //描述需要显示哪些块
    AttrSett: "title,intro,required,txt_size,txt_type".split(',')
}
//----------Select
ZLForm.Select.prototype = {
    addTlp: "<span class='fa fa-plus opbtn'>",
    radTlp: "<li data-sort='@sort'><input type='radio' name='@id_rad'/><input type='text' value='@value' /><span class='fa fa-minus opbtn'></span></li>",
    chkTlp: "<li data-sort='@sort'><input type='checkbox' name='@id_chk'/><input type='text' value='@value' /><span class='fa fa-minus opbtn'></span></li>",
    //前后台独有更新
    GetMaxSort: function () {
        var max = 1;
        $("#sel_option_ul li").each(function () { var num = $(this).data("sort"); max = parseInt(num) > parseInt(max) ? num : max; });
        return max + 1;
    },
    AddOption: function () {
        $cur = GetCurobj();
        var max = $cur.instance.GetMaxSort();
        $cur.instance.sel_option.push({ id: max, value: "选项" + max });
        $("#sel_option_ul .fa-plus").remove(); 
        //添加与移除后,左边栏根据Json数据重建
        $cur.instance.UpdateBack($cur.instance.sel_type, $cur.instance.sel_option);
        $cur.instance.UpdateFront("radio", $cur.instance.sel_option);
    },
    RemoveOption: function () {
        var id = $(event.srcElement).closest("li").data("sort");
        $cur = GetCurobj();
        if ($("#sel_option_ul li").length == 1) {//只剩一个则清空值，而不是删除
            ArrCommon.GetModelByID($cur.instance.sel_option, id).value = "";
        }
        else {
            ArrCommon.DelByID($cur.instance.sel_option, id);
        }
        $cur.instance.UpdateBack($cur.instance.sel_type, $cur.instance.sel_option);
        $cur.instance.UpdateFront($cur.instance.sel_type, $cur.instance.sel_option);
    },
    UpdateFront: function (type, arr) {//radio,checkbox,select
        $cur = GetCurobj();
        var tlp = "";
        switch (type) {
            case "radio":
                tlp = "<ul>";
                var liTlp = "<li data-sort='@sort'><input type='radio' name='@id_radio' value='@value'><label>@value</label></li>";
                for (var i = 0; i < arr.length; i++) {
                    tlp += liTlp.replace("@sort", arr[i].id).replace(/@value/g, arr[i].value).replace("@id", $cur.instance.id);
                }
                tlp += "</ul>";
                break;
            case "checkbox":
                tlp = "<ul>";
                var liTlp = "<li data-sort='@sort'><input type='checkbox' name='@id_checkbox' value='@value'><label>@value</label></li>";
                for (var i = 0; i < arr.length; i++) {
                    tlp += liTlp.replace("@sort", arr[i].id).replace(/@value/g, arr[i].value).replace("@id", $cur.instance.id);
                }
                tlp += "</ul>";
                break;
            case "select":
                var tlp = "<select id='@id_select'>".replace("@id", $cur.instance.id);
                var opTlp = "<option value='@value' data-sort='@sort'>@value</option>";
                for (var i = 0; i < arr.length; i++) {
                    tlp += opTlp.replace("@sort", arr[i].id).replace(/@value/g, arr[i].value);
                }
                tlp += "</select>";
                break;
        }
        $cur.find(".content").html(tlp);
    },
    UpdateBack: function (type, arr) {
        var tlp = "";
        switch (type) {
            case "radio":
            case "select":
                for (var i = 0; i < arr.length; i++) {
                    tlp += this.radTlp.replace("@sort", arr[i].id).replace("@value", arr[i].value);
                }
                break;
            case "checkbox":
                for (var i = 0; i < arr.length; i++) {
                    tlp += this.chkTlp.replace("@sort", arr[i].id).replace("@value", arr[i].value);
                }
                break;
        }
        $("#sel_option_ul").html(tlp); $("#sel_option_ul li:last").append(this.addTlp);
        this.EventFunc();
    },
    BeginEdit: function ($cur) {
        $editdiv = $("#EditDiv");
        $cur.instance.title = $cur.find(".title").text();
        $cur.instance.intro = $cur.find(".intro").text();
        $editdiv.find("#title_edit_txt").val($obj.instance.title)
        $editdiv.find("#intro_edit_txt").val($obj.instance.intro);
        $editdiv.find("#required_chk")[0].checked = $obj.instance.required;
        //----独有
        $cur.instance.value = $cur.find(":checked").val();//选中值
        $cur.instance.UpdateBack($cur.instance.sel_type, $cur.instance.sel_option);//更后后台Radio选项
        $editdiv.find("input:radio[value=" + $cur.instance.sel_type + "]")[0].checked = true;
    },
    AttrSett: "title,intro,required,sel_option,sel_type,radio_view".split(','),
    EventBind: function () {
        $("#sel_type_div :radio").click(function () {
            var $obj = $(event.srcElement);
            var $cur = GetCurobj();//当前选中
            $cur.instance.sel_type = $obj.val();
            $cur.instance.UpdateBack($cur.instance.sel_type, $cur.instance.sel_option);
            $cur.instance.UpdateFront($cur.instance.sel_type, $cur.instance.sel_option);
        });
    },
    //-----独有事件绑定,特定条件后需要重绑
    EventFunc: function () {
        $cur = GetCurobj();
        $("#sel_option_ul li input:radio,input:checkbox").click(function () {
            //获取编辑区的选中,更新页面效果
            var id = $(this).closest("li").data("sort");
            var flag = this.checked;
            $cur.find("li[data-sort=" + id + "]").find(":radio,:checkbox").each(function () { this.checked = flag; });
            $cur.find("select").val($cur.find("option[data-sort=" + id + "]").val());
        });
        $("#sel_option_ul li input:text").keyup(function () {
            var id = $(this).closest("li").data("sort");
            var mod = ArrCommon.GetModelByID($cur.instance.sel_option, id);
            mod.value = $(this).val();
            if ($cur.instance.sel_type != "select")
                $cur.find("[data-sort=" + id + "] label").text(mod.value);
            else {
                $cur.find("option[data-sort=" + id + "]").val(mod.value).text(mod.value);
            }
        });
        //添加删除选项
        $("#sel_option_ul .fa-minus").click($cur.instance.RemoveOption);
        $("#sel_option_ul .fa-plus").click($cur.instance.AddOption);
    },
}
//----------Img
ZLForm.Img.prototype = {
    AttrSett: "intro,imgsrc,txtalign".split(','),
    BeginEdit: function ($cur) {
        $editdiv = $("#EditDiv");
        $cur.instance.intro = $cur.find(".intro").text();
        $editdiv.find("#intro_edit_txt").val($obj.instance.intro);
        //----独有
        $("[name=txtalign_radio][value=" + $cur.instance.align + "]")[0].checked = true;
        $("#imgsrc_txt").val($cur.instance.src);
    },
    EventBind: function () {
        $("#imgsrc_txt").blur(function () {
            $cur = GetCurobj();
            $cur.find("img").attr("src", $(this).val());
        });
    },
}
ZLForm.Str.prototype = {
    AttrSett: "title,intro,txtalign".split(','),
    BeginEdit: function () {
        $editdiv = $("#EditDiv");
        $cur.instance.title = $cur.find(".title").text();
        $cur.instance.intro = $cur.find(".intro").text();
        $editdiv.find("#title_edit_txt").val($obj.instance.title);
        $editdiv.find("#intro_edit_txt").val($obj.instance.intro);
        $("[name=txtalign_radio][value=" + $cur.instance.align + "]")[0].checked = true;
        $("#imgsrc_txt").val($cur.instance.src);
    },
}

//GetValue: function (mod) {
//    switch (mod.ctype)//img不处理
//    {
//        case "input_txt":
//            switch (mod.txt_type)
//            {
//                case "input":
//                    break;
//                default:
//                    break;
//            }
//            break;
//        case "select":
//            break;
//        case "img":
//            break;
//        case "str":
//            break;
//    }
//}