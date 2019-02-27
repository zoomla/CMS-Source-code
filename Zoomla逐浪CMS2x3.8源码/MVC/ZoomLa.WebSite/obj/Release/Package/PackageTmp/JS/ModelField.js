function SelectModelType() {
    var TypeCount = $("#Type_Rad").find(":input[type=radio]");
    for (var i = 0; i < TypeCount.length; i++) {
        var tid = document.getElementById("Type_Rad_" + i).value;
        var s = i;

        var DivType = document.getElementById("Div" + tid);
        if (DivType) {
            if (document.getElementById("Type_Rad_" + i).checked) {
                DivType.style.display = "";
                if (tid == "MobileSMSType" || tid == "PicType" || tid == "SqlFile" || tid == "MultiPicType" || tid == "FileType" || tid == "SuperLinkType" || tid == "ColorType") {
                    document.getElementById("DivTextType").style.display = "none";
                }
                else {
                    if (tid == "TextType") {
                        document.getElementById("DivTextType").style.display = "";
                    }
                }
            }
            else {
                DivType.style.display = "none";
            }
        }
        if (tid == "DoubleDateType" && document.getElementById("Type_Rad_" + i).checked) {
            document.getElementById("DivDateType").style.display = "none";
        }
    }
}
function AutoSelectModelType() {
    //var TypeCount = document.getElementsByName("ctl00$Content$Type_Rad");

    //for (var i = 1; i < TypeCount.length; i++) {
    //    if (TypeCount[i].checked) {
    //        return TypeCount[i].value;
    //    }
    //}
    return "";
}

function isok() {
    var txt = $("#Name_T").val();
    var patt1 = /^[0-9]+$/;
    if (txt.replace(" ", "").length == 0) {
        alert('字段名称不能够为空！');
        $("#Name_T").focus();
        return false;
    }
    var patt = /^[a-zA-Z0-9_\-]+$/
    if (!patt.test(txt)) {
        alert('字段名称只能由字母、下划线或数字组成！');
        $("#Name_T").focus();
        return false;
    }

    if ($("Alias_T").value == "") {
        alert('字段别名不能够为空！');
        $("Alias").focus();
        return false;
    }

    if (AutoSelectModelType() == "TextType") {
        txt = $('TitleSize').value;
        if ($("TitleSize").value == "") {
            alert("文本框长度不能够为空！")
            $("TitleSize").focus();
            return false;
        }

        if (!patt1.test(txt)) {
            alert("文本框长度只能够由数字组成！")
            $("TitleSize").focus();
            return false;
        }
    }

    if (AutoSelectModelType() == "MultipleTextType") {
        txt = $('MultipleTextType_Width').value;
        if ($("MultipleTextType_Width").value == "") {
            alert("显示的宽度不能够为空！")
            $("MultipleTextType_Width").focus();
            return false;
        }

        if (!patt1.test(txt)) {
            alert("显示的宽度只能够由数字组成！")
            $("MultipleTextType_Width").focus();
            return false;
        }

        txt = $('MultipleTextType_Height').value;
        if ($("MultipleTextType_Height").value == "") {
            alert("显示的高度不能够为空！")
            $("MultipleTextType_Height").focus();
            return false;
        }

        if (!patt1.test(txt)) {
            alert("显示的高度只能够由数字组成！")
            $("MultipleTextType_Height").focus();
            return false;
        }
    }

    if (AutoSelectModelType() == "MultipleHtmlType") {
        txt = $('MultipleHtmlType_Width').value;
        if ($("MultipleHtmlType_Width").value == "") {
            alert("显示的宽度不能够为空！")
            $("MultipleHtmlType_Width").focus();
            return false;
        }

        if (!patt1.test(txt)) {
            alert("显示的宽度只能够由数字组成！")
            $("MultipleHtmlType_Width").focus();
            return false;
        }

        txt = $('MultipleHtmlType_Height').value;
        if ($("MultipleHtmlType_Height").value == "") {
            alert("显示的高度不能够为空！")
            $("MultipleHtmlType_Height").focus();
            return false;
        }

        if (!patt1.test(txt)) {
            alert("显示的高度只能够由数字组成！")
            $("MultipleHtmlType_Height").focus();
            return false;
        }
    }

    if (AutoSelectModelType() == "RadioType") {
        if ($("RadioType_Content").value == "") {
            alert("分行键入每个选项不能够为空！")
            $("RadioType_Content").focus();
            return false;
        }
    }

    if (AutoSelectModelType() == "ListBoxType") {
        if ($("ListBoxType_Content").value == "") {
            alert("分行键入每个选项不能够为空！")
            $("ListBoxType_Content").focus();
            return false;
        }
    }

    return true;
}

function SelectDictionary(val) {
    window.open('../AddOn/SelectDictionary.aspx?ControlId=' + val + '', 'newWin', 'modal=yes,width=600,height=500,resizable=no,scrollbars=yes')
}

function SubPage(txtid) {
    var a;
    a = prompt('你在下面输入字段名和值，用|隔开。例如“字段名|值”', "");
    var x = document.getElementById(txtid).value;
    if (x == "" || x == "null") {
        if (a != null) {
            document.getElementById(txtid).value = a;
        }
    }
    else {
        if (a == null) {
            document.getElementById(txtid).value = x + "\n";
        }
        else {
            document.getElementById(txtid).value = x + "\n" + a;
        }
    }
}

function SelectColor(t, clientId) {
    var url = "../Common/SelectColor.aspx?d=f&t=6";
    var old_color = (document.getElementById(clientId).value.indexOf('#') == 0) ? '&' + document.getElementById(clientId).value.substr(1) : '&' + document.getElementById(clientId).value;
    if (document.all) {
        var color = showModalDialog(url + old_color, "", "dialogWidth:18.5em; dialogHeight:16.0em; status:0");
        if (color != null) {
            document.getElementById(clientId).value = color;
        } else {
            document.getElementById(clientId).focus();
        }
    } else {
        var color = window.open(url + '&' + clientId, "hbcmsPop", "top=200,left=200,scrollbars=yes,dialog=yes,modal=no,width=300,height=260,resizable=yes");
    }
}