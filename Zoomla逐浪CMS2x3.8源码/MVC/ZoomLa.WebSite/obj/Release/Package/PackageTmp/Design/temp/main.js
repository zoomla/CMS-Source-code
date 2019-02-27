var buser = new B_User();
$(function () {
    setTimeout(function () {
        $(".kfleft").click(function () {
            var i = $("#Plat_show").css("left");
            if (i == '0px') {
                $('#Plat_show').animate({ left: -80 }, 200);
            } else {
                $('#Plat_show').animate({ left: 0 }, 200);
            }
        });
    }, 1000);
    //通栏广告脚本---------------------------
    var now = 0;
    var max = $(".e-bx_banner_img").length - 1;
    for (var i = 0; i <= max; i++) {
        $("#s" + i).show();
    }
});
$(function () {
    $("#pagemain2").find(".text1").hide();
    //鼠标在图片上的变幻
    $("#pagemain2").find(".tou").mouseover(function () {
        //$(this).next(".text1").show("2000");	
        //$(this).next(".text1").fadeIn("2000");		
        $(this).next(".text1").slideDown("2000");
        $("#pagemain2").find(".page_main_icon").css("z-index", "1");
        $(this).parent().css("z-index", 99);
    });
    //移除鼠标
    $("#pagemain2").find(".tou").mouseout(function () {
        //$(this).next(".text1").hide();
        //$(this).next(".text1").fadeOut();		
        $(this).next(".text1").slideUp(100);
    });
});
$(function () {
    $(document).snowfall('clear');
    $(document).snowfall({ round: true, minSize: 4, maxSize: 35, text: '<i class="fa fa-leaf"></i>', flakeColor: '', flakeCount: 50 });
    buser.IsLogged(function (data, flag) {
        if (flag) {
            data = JSON.parse(data);
            $(".nav_user>a").html(data.UserName + "的会员中心");
            $(".nav_user .dropdown-menu li:nth-child(1) a").html("会员中心");
            $(".nav_user .dropdown-menu li:nth-child(2)").remove();
        }
    });
});
function change() {
    $($(".e-bx_banner_img")[now]).css({
        "display": "block"
    });
    //$($(".e-bx_banner_img")[now]).fadeIn("slow");
    var spid = document.getElementById("s" + now);
    spid.className = "zsdot";
    for (var i = 0; i <= max; i++) {
        if (i != now) {
            $($(".e-bx_banner_img")[i]).css({
                "display": "none"
            });
            var spid = document.getElementById("s" + i);
            spid.className = "hsdot";
        }
    }
    if (now == max) {
        now = 0;
    } else {
        now += 1;
    }
    timerID = setTimeout("change()", 3000);
}
function over(num) {
    now = num;
    $($(".e-bx_banner_img")[now]).css({
        "display": "block"
    });
    //$($(".e-bx_banner_img")[now]).fadeIn("slow");
    var spid = document.getElementById("s" + now);
    spid.className = "zsdot";

    for (var i = 0; i <= max; i++) {
        if (i != now) {
            $($(".e-bx_banner_img")[i]).css({
                "display": "none"
            });
            var spid = document.getElementById("s" + i);
            spid.className = "hsdot";
        }
    }
    //clearTimeout(timerID);
}
function out(num) {
    now = num;
    change();
}
function show(ele) {
    $(ele.children[0]).hide();
    $(ele.children[1]).show();
}
function hide(ele) {
    $(ele.children[0]).show();
    $(ele.children[1]).hide();
}
function advshow1() {
    document.getElementById("pagemain1").style.display = 'block';
    document.getElementById("pagemain2").style.display = 'none';
    $("#imagediv").empty();
    $("#imagediv").append('<div class="as"><img src="{$CssDir/}images/asbg.jpg"/></div><div onclick="advshow3()"  style="cursor:pointer;">&nbsp;</div>');
}
function advshow2() {
    document.getElementById("pagemain1").style.display = 'none';
    document.getElementById("pagemain2").style.display = 'block';
    $("#imagediv").empty();
    $("#imagediv").append('<div class="as1"><img src="{$CssDir/}images/asbg.jpg"/></div><div onclick="advshow3()"  style="cursor:pointer;">&nbsp;</div>');
}
function advshow3() {
    if (document.getElementById("pagemain1").style.display == 'none') {
        document.getElementById("pagemain1").style.display = 'block';
        document.getElementById("pagemain2").style.display = 'none';
        $("#imagediv").empty();
        $("#imagediv").append('<div class="as"><img src="{$CssDir/}images/asbg.jpg"/></div><div onclick="advshow3()"  style="cursor:pointer;">&nbsp;</div>');
    } else {
        document.getElementById("pagemain1").style.display = 'none';
        document.getElementById("pagemain2").style.display = 'block';
        $("#imagediv").empty();
        $("#imagediv").append('<div class="as1"><img src="{$CssDir/}images/asbg.jpg"/></div><div onclick="advshow3()"  style="cursor:pointer;">&nbsp;</div>');
    }
}
function LogoutFun()
{
    buser.Logout(function(){location=location;});
}