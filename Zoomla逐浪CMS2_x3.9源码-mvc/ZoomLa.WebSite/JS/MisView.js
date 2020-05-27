 
//显示div
function PopupDiv(div_id, txt_sorce, input) {
	//document.getElementById("ifm1").src = '/Mis/SelUser.aspx?OpenerText=' + input;
	var div_obj = $("#" + div_id); 
    var h = (document.documentElement.offsetHeight - 200)/2;
    var w = (document.documentElement.offsetWidth - 500)/2;
    div_obj.animate({ opacity: "show", left: w, top: h, width: div_obj.width, height: div_obj.height }, 300); 
	div_obj.focus();
}

    //显示div
 function PopupDiv2(div_id,txt_sorce) {
        var div_obj = $("#" + div_id);
        div_obj.animate({opacity: "show", left: 300, top: 200, width: div_obj.width, height: div_obj.height }, 300);
        div_obj.focus();
    }

//隐藏div
function HideDiv(div_id) {
$("#" + div_id).animate({ opacity: "hide" }, 300);
}

//隐藏两个DIV
function HideDivs(div_id,div_ids) {
	$("#" + div_id).animate({ opacity: "hide" }, 300);
	$("#" + div_ids).animate({ opacity: "hide" }, 300);
}
function ShowDiv(obj)
{
	var divs= document.getElementById(obj);
	if (divs.style.display == "none")
		divs.style.display = "block";
	else
		divs.style.display = "none";
}
function ShowHidDiv(obj, obj2, objval) {
	var divs = document.getElementById(obj);
	var divs2 = document.getElementById(obj2);
	var inp = document.getElementById(objval);
	if (divs2.style.display == "none") {
		divs.style.display = "none";
		divs2.style.display = "block";
		inp.value = "";
	}
	else {
		divs.style.display = "block";
		divs2.style.display = "none";
		inp.value = "";
	}
}

 function GetProcedure(div_id,proHid,proText) {
        document.getElementById("HidPro").value = proHid;
        document.getElementById("lblPro").innerText = proText;
        $("#" + div_id).animate({ opacity: "hide" }, 300);
    }
//备忘
//弹出div
   function PopupDivs(div_id,txt_sorce, type) {
	 SetOptions(txt_sorce,type);
	   var div_obj = $("#" + div_id);       
    var h = (document.documentElement.offsetHeight - 200)/2;
    var w = (document.documentElement.offsetWidth - 500)/2;
	   div_obj.animate({opacity: "show", left:w, top: h, width: div_obj.width, height: div_obj.height }, 300);
	   div_obj.focus();
   }
   //显示div并取出级别的ID
function PopupDivs2(div_id, txt_sorce, id) {
    document.getElementById("HidPid").value = id;
    var div_obj = $("#" + div_id);
    div_obj.animate({ opacity: "show", left: 300, top: 200, width: div_obj.width, height: div_obj.height }, 300);
    div_obj.focus();
}
function CheckNull()
    {
        if ($("#txtMTitle").val().length == 0)
        {
            $("#errMsgTitle").css("display", "block");
            return false;
        }
        //if ($("#txtContent").val().length == 0)
        //{
        //    $("#errMsgContent").css("display", "block");
        //    return false;
        //}
        return true;
    } 
    // 填充选择项
    function SetOptions(txt_id, type)
    {
        if ($("#" + txt_id).val().length == 0)
            return;
        switch (type) {
            case "warn":
                SetWarn(txt_id);
                break;
            case "share":
                SetShare(txt_id);
                break;
        }
    }
    //显示选择项
    function GetOptions(div_id, txt_id, type)
    {
        var options = "";
        switch (type) {
            case "warn":
                options = GetWarn(div_id);
                break;
            case "share":
                options = GetShare(div_id);
                break;
        }
        $("#" + txt_id).val(options);
        HideDiv(div_id);
    }
    //获取 提醒消息：
    function GetWarn(div_id)
    {
        var warn = "";
        warn = warn + $("#w_fqRepeat").find("option:selected").text() +"|";
        warn += $("#w_date").val() + "|";
        warn += $("#w_time").val() + "|";
        $(":checkbox[name=chkpWarn]:checked").each(function () {
            warn += $(this).val() + ",";
        });
        warn = warn.Trim(",");
        warn += "|";
        warn += $("#w_method").val();
        return warn;
    }
    //获取共享信息
    function GetShare(div_id)
    {
        var share = "";
        $(":checkbox[name=chkUser]:checked").each(function () {
            share += $(this).val() + ",";
        });
        return share.Trim(",");
    }

    //设置 提醒消息
    function SetWarn(txt)
    {
        var arr = $("#" + txt).val().split("|");
        $("#w_fqRepeat").val(arr[0]);
        $("#w_date").val(arr[1]);
        $("#w_time").val(arr[2]);
        $(":checkbox[name=chkpWarn]").each(function () {
            if (arr[3].indexOf($(this).val()) >= 0)
                $(this).attr("checked", true);
            else
                $(this).attr("checked", false);
        });
        $("w_method").val(arr[4]);
    }

    //设置 共享信息
    function SetShare(txt)
    {
    }
 

    //加载时间
    function show2() {
        var divs = document.getElementById("DateDiv");
        var Digital = new Date();
        var year = Digital.getFullYear();
        var months = Digital.getMonth() + 1;
        var Days = Digital.getDate();
        var hours = Digital.getHours();
        var minutes = Digital.getMinutes();
        var seconds = Digital.getSeconds();
        var dn = "AM"
        if (hours >= 12) {
            dn = "PM";
            hours = hours - 12;
        }
        if (hours == 0)
            hours = 12;
        if (minutes <= 9)
            minutes = "0" + minutes;
        if (seconds <= 9)
            seconds = "0" + seconds;
        var ctime = year + "年" + months + "月" + Days + "日 " + hours + ":" + minutes + ":" + seconds + " " + dn;
        divs.innerHTML = ctime;
        setTimeout("show2()", 1000);
    }
