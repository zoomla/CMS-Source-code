<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Camera.aspx.cs" MasterPageFile="~/Common/Common.master" Inherits="Plugins_Camera" EnableViewStatemac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<title>在线拍照（仅支持Chrome等webkit浏览器）</title>
<style>
    .camera_div .leftphoto{padding:0 30px;}
    .camera_div .leftphoto li{margin-top:10px;}
    .camera_div .leftphoto img{width:100%;}
    .camera_btn input{font-size:20px;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

<div class="container">
    <div class="row margin_t10 camera_div">
        <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                <div class="panel panel-default">
                  <div class="panel-heading"><h3>在线拍照</h3></div>
                  <div class="panel-body">
                       <div id="spec-n1">
                        <div class="o-img"><!--左主窗口-->
                        <video id="video" autoplay style="width:100%;padding:1px;text-align:center"></video>
                        </div>
                        <div class="switch">
                        </div>													
                        </div>				
                  </div>
                </div>
            </div>
            <div class="col-lg-3  col-md-3  col-sm-12 col-xs-12" id="spec-n5">
            <div id="spec-top" class="control disabled"></div>
            <div id="spec-bottom" class="control"></div>
            <div id="spec-list" class="list-unstyled">
                <div class="panel panel-default">
                  <div class="panel-heading"><h3>照片列表</h3></div>
                  <div class="panel-body">
                      <ul id="RightPreView" class="leftphoto">
                            <li class='curr' id="need" style="display:none;"><img width='128' height='96' src="#"/></li>	
                        </ul>
                  </div>
                </div>
                
            </div>
        </div>
    </div>
    <div class="row camera_btn">
        <input type="button" value="拍照" onclick="shoot()" class="btn btn-primary"  style="width:20%;height:80px; margin-left:150px;"/>
        <input type="button" value="上传" onclick="PicUpload()" class="btn btn-primary" style="width:20%;height:80px;margin-left:150px;"/>
    </div>
</div>
		
<!--spec-n5 end-->
    
<div id="intro"></div><!--底部文字-->

											
<div class="thickdiv" style="display:none;"></div>
<div class="thickbox" style="width:272px;height:90px;display:none;">
<div style="width:250px;" class="thicktitle">
    <span>提示</span>
</div>
<div style="width:250px;height:40px;" id="" class="thickcon">已经到最后一张了！</div>
<a class="thickclose" href="#">×</a>
</div>											
											
<script type="text/javascript">
    function PicUpload() {
        //发送信息给页面，保存，保存方法内检测是否登录，
        $(".curr").each(function (i, v) {
            var base64String = $(v).find('img').attr('src').substr(22); //取得base64字串
            if (base64String == "") return;
            $.post("Camera.aspx", { "type": "imageS", "data": base64String }, function (reData) {
                $(v).find('img').attr('src', reData);
                $(v).removeClass("curr");
                if (!$(".curr")[1])//判断是否还有未保存的图片
                    alert('上传成功!');
            });
        });
        //$.post("Camera.aspx", { "type": "imageS" }, function () { });
    }
    //清除Session
    function clearSession() {
        $.post("Camera.aspx", { "type": "clearSession" }, function () { });

    }

    var imgArray = new Array();//本用于存图片，暂不用
    var imgNum = 0;
    var video = document.getElementById("video");
    navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia;
    if (navigator.getUserMedia) //  
    {
        if (navigator.webkitURL)//  
        {
            navigator.getUserMedia({ video: true }, function (stream) {
                video.src = window.webkitURL.createObjectURL(stream);
            }, function (error) { });
        }
        else //  
        {
            navigator.getUserMedia({ video: true }, function (stream) {
                video.src = window.webkitURL.createObjectURL(stream);
            }, function (error) { });
        }
    }

    //執行拍照
    function shoot() {
        var video = $("#video")[0];
        var canvas = capture(video);
        //$("#result").empty();
        //呈現圖像(拍照結果)
        var imgData = canvas.toDataURL("image/jpg");
        //var base64String = imgData.substr(22); //取得base64字串
        if (imgNum > 7) { alert("最多只能同时上传八张照片"); reutrn; }
        imgNum++;
        var img = "<li class='curr'><img width='128' height='96' src='";
        img += imgData;
        img += "'/></li>";
        $(img).insertBefore("#need");
        //上傳，儲存圖片
        //$.ajax({
        //    url: "Camera.aspx",
        //    type: "post",
        //    data: { data: base64String, type: "image" },
        //    async: true,
        //    success: function (reData) {//htmlVal
        //        $("#result").append(canvas);
        //        var img = "<li class='curr'><img width='128' height='96' src='";
        //        img += reData;
        //        img += "'/></li>";
        //        $(".curr").removeClass("curr");
        //        $(img).insertBefore("#need");

        //    }, error: function (e) {
        //        alert("无法拍摄，请确保摄像头打开，并且网络连接正常"); //alert錯誤訊息
        //    }

        //});
    }

    //从video元素抓取圖像到canvas
    function capture(video) {
        var canvas = document.createElement('canvas'); //建立canvas js DOM元素
        canvas.width = video.videoWidth;
        canvas.height = video.videoHeight;
        var ctx = canvas.getContext('2d');
        ctx.drawImage(video, 0, 0);
        return canvas;
    }

    function closebox() {
        $(".thickdiv,.thickbox").hide();
    }
    $(".thickclose").bind("click", function () {
        $(".thickdiv,.thickbox").hide();
    });

    (function (a) { a.fn.jdMarquee = function (h, b) { if (typeof h == "function") { b = h; h = {}; } var j = a.extend({ deriction: "up", speed: 10, auto: false, width: null, height: null, step: 1, control: false, _front: null, _back: null, _stop: null, _continue: null, wrapstyle: "", stay: 5000, delay: 20, dom: "div>ul>li".split(">"), mainTimer: null, subTimer: null, tag: false, convert: false, btn: null, disabled: "disabled", pos: { ojbect: null, clone: null } }, h || {}); var u = this.find(j.dom[1]); var e = this.find(j.dom[2]); var r; if (j.deriction == "up" || j.deriction == "down") { var l = u.eq(0).outerHeight(); var c = j.step * e.eq(0).outerHeight(); u.css({ width: j.width + "px", overflow: "hidden" }); } if (j.deriction == "left" || j.deriction == "right") { var n = e.length * e.eq(0).outerWidth(); u.css({ width: n + "px", overflow: "hidden" }); var c = j.step * e.eq(0).outerWidth(); } var o = function () { var s = "<div style='position:relative;overflow:hidden;z-index:1;width:" + j.width + "px;height:" + j.height + "px;" + j.wrapstyle + "'></div>"; u.css({ position: "absolute", left: 0, top: 0 }).wrap(s); j.pos.object = 0; r = u.clone(); u.after(r); switch (j.deriction) { default: case "up": u.css({ marginLeft: 0, marginTop: 0 }); r.css({ marginLeft: 0, marginTop: l + "px" }); j.pos.clone = l; break; case "down": u.css({ marginLeft: 0, marginTop: 0 }); r.css({ marginLeft: 0, marginTop: -l + "px" }); j.pos.clone = -l; break; case "left": u.css({ marginTop: 0, marginLeft: 0 }); r.css({ marginTop: 0, marginLeft: n + "px" }); j.pos.clone = n; break; case "right": u.css({ marginTop: 0, marginLeft: 0 }); r.css({ marginTop: 0, marginLeft: -n + "px" }); j.pos.clone = -n; break; } if (j.auto) { k(); u.hover(function () { m(j.mainTimer); }, function () { k(); }); r.hover(function () { m(j.mainTimer); }, function () { k(); }); } if (b) { b(); } if (j.control) { g(); } }; var k = function (s) { m(j.mainTimer); j.stay = s ? s : j.stay; j.mainTimer = setInterval(function () { t(); }, j.stay); }; var t = function () { m(j.subTimer); j.subTimer = setInterval(function () { q(); }, j.delay); }; var m = function (s) { if (s != null) { clearInterval(s); } }; var p = function (s) { if (s) { a(j._front).unbind("click"); a(j._back).unbind("click"); a(j._stop).unbind("click"); a(j._continue).unbind("click"); } else { g(); } }; var g = function () { if (j._front != null) { a(j._front).click(function () { a(j._front).addClass(j.disabled); p(true); m(j.mainTimer); j.convert = true; j.btn = "front"; t(); if (!j.auto) { j.tag = true; } f(); }); } if (j._back != null) { a(j._back).click(function () { a(j._back).addClass(j.disabled); p(true); m(j.mainTimer); j.convert = true; j.btn = "back"; t(); if (!j.auto) { j.tag = true; } f(); }); } if (j._stop != null) { a(j._stop).click(function () { m(j.mainTimer); }); } if (j._continue != null) { a(j._continue).click(function () { k(); }); } }; var f = function () { if (j.tag && j.convert) { j.convert = false; if (j.btn == "front") { if (j.deriction == "down") { j.deriction = "up"; } if (j.deriction == "right") { j.deriction = "left"; } } if (j.btn == "back") { if (j.deriction == "up") { j.deriction = "down"; } if (j.deriction == "left") { j.deriction = "right"; } } if (j.auto) { k(); } else { k(4 * j.delay); } } }; var d = function (w, v, s) { if (s) { m(j.subTimer); j.pos.object = w; j.pos.clone = v; j.tag = true; } else { j.tag = false; } if (j.tag) { if (j.convert) { f(); } else { if (!j.auto) { m(j.mainTimer); } } } if (j.deriction == "up" || j.deriction == "down") { u.css({ marginTop: w + "px" }); r.css({ marginTop: v + "px" }); } if (j.deriction == "left" || j.deriction == "right") { u.css({ marginLeft: w + "px" }); r.css({ marginLeft: v + "px" }); } }; var q = function () { var v = (j.deriction == "up" || j.deriction == "down") ? parseInt(u.get(0).style.marginTop) : parseInt(u.get(0).style.marginLeft); var w = (j.deriction == "up" || j.deriction == "down") ? parseInt(r.get(0).style.marginTop) : parseInt(r.get(0).style.marginLeft); var x = Math.max(Math.abs(v - j.pos.object), Math.abs(w - j.pos.clone)); var s = Math.ceil((c - x) / j.speed); switch (j.deriction) { case "up": if (x == c) { d(v, w, true); a(j._front).removeClass(j.disabled); p(false); } else { if (v <= -l) { v = w + l; j.pos.object = v; } if (w <= -l) { w = v + l; j.pos.clone = w; } d((v - s), (w - s)); } break; case "down": if (x == c) { d(v, w, true); a(j._back).removeClass(j.disabled); p(false); } else { if (v >= l) { v = w - l; j.pos.object = v; } if (w >= l) { w = v - l; j.pos.clone = w; } d((v + s), (w + s)); } break; case "left": if (x == c) { d(v, w, true); a(j._front).removeClass(j.disabled); p(false); } else { if (v <= -n) { v = w + n; j.pos.object = v; } if (w <= -n) { w = v + n; j.pos.clone = w; } d((v - s), (w - s)); } break; case "right": if (x == c) { d(v, w, true); a(j._back).removeClass(j.disabled); p(false); } else { if (v >= n) { v = w - n; j.pos.object = v; } if (w >= n) { w = v - n; j.pos.clone = w; } d((v + s), (w + s)); } break; } }; if (j.deriction == "up" || j.deriction == "down") { if (l >= j.height && l >= j.step) { o(); } } if (j.deriction == "left" || j.deriction == "right") { if (n >= j.width && n >= j.step) { o(); } } }; })(jQuery);

    (function () {
        clearSession();
        var a = {
            obj: $("#spec-list"),
            subobj: $("#spec-n1 img"),
            width: 720,
            height: 490,
            subheight: 540,
            posi: function () {
                var h = a.subobj.attr("height");
                if (h < a.subheight && h > 0) {
                    a.subobj.css({ "margin-top": (a.subheight - h) / 2 })
                } else {
                    a.subobj.css({ "margin-top": 0 });
                }
            },

            images: function () {
                a.obj.find("img").bind("click", function () {
                    var src = $(this).attr("src");
                    var cont = $(this).attr("title");
                    $("#intro").html(cont);
                    $("#spec-n1 img").attr("src", src.replace("s128x96", "s720x540"));
                    a.posi();

                    if ($("#spec-list li").hasClass("curr")) {
                        $("#spec-list .curr").removeClass("curr")
                    };
                    $(this).parent().addClass("curr");
                    var m = a.calculate.swith();

                    if (m[1] == 0) {
                        $("#foward").addClass("disabled");
                    } else {
                        if (m[1] + 1 == m[0]) {
                            $("#next").addClass("disabled");
                        } else {
                            $("#foward").removeClass("disabled");
                            $("#next").removeClass("disabled");
                        }
                    }
                })
            },

            alpha: function () {
                var img = new Image();
                img = $("#spec-n1").find("img").eq(0).get(0);
                var appname = navigator.appName.toLowerCase();
                if (appname.indexOf("netscape") == -1) {
                    if (img.readyState == "complete") {
                        a.posi();
                    }
                } else {
                    img.onload = function () {
                        if (img.complete == true) {
                            a.posi();
                        }
                    }
                }
            },

            calculate: {
                swith: function () {
                    var p = a.obj.find("li");
                    var m = [];
                    m[0] = p.length;
                    m[1] = p.index($(".curr"));
                    return m;
                },
                roll: function () {
                    var p = a.obj.find("ul");
                    var m = [];
                    m[0] = parseInt(p.css("margin-top"));
                    m[1] = p.height();
                    return m;
                }
            },

            swith: function () {
                $("#foward").bind("click", function () {
                    var m = a.calculate.swith();
                    var s = a.calculate.roll();
                    if (m[1] > 0) {
                        $("#next").removeClass("disabled");
                        $("#spec-list .curr").removeClass("curr");
                        var ob = a.obj.find("li").eq(m[1] - 1).find("img");
                        var src = ob.attr("name");
                        var cont = ob.attr("title");
                        $("#intro").html(cont);
                        $("#spec-n1").find("img").attr("src", src.replace("s128x96", "s720x540"));
                        a.posi();
                        a.obj.find("li").eq(m[1] - 1).addClass("curr");
                    };
                    if (m[1] == 1) {
                        $("#foward").addClass("disabled");
                    };
                    if (m[1] == 0) {
                        $(".thickdiv,.thickbox").show();
                        setTimeout(function () { closebox(); }, 1200)
                    }
                    if (m[1] > 2 && s[0] < 0) {
                        a.roll.next(1);
                    }
                });

                $("#next").bind("click", function () {
                    var m = a.calculate.swith();
                    var s = a.calculate.roll();
                    if (m[1] < m[0] - 1) {
                        $("#foward").removeClass("disabled");
                        $("#spec-list .curr").removeClass("curr");
                        var ob = a.obj.find("li").eq(m[1] + 1).find("img");
                        var src = ob.attr("name");
                        var cont = ob.attr("title");
                        $("#intro").html(cont);
                        $("#spec-n1").find("img").attr("src", src.replace("s128x96", "s720x540"));
                        a.posi();
                        a.obj.find("li").eq(m[1] + 1).addClass("curr");
                    };
                    if (m[1] + 2 == m[0]) {
                        $("#next").addClass("disabled")
                    };
                    if (m[1] + 1 == m[0]) {
                        $(".thickdiv,.thickbox").show();
                        setTimeout(function () { closebox(); }, 1200)
                    };
                    if (m[1] >= 2 && s[1] + s[0] > 490) {
                        a.roll.foward(1);
                    }
                })
            },

            list: function () {
                $("#spec-top").bind("click", function () {
                    var m = a.calculate.roll();
                    if (m[0] < 0) {
                        a.roll.next(2);
                    }
                });

                $("#spec-bottom").bind("click", function () {
                    var m = a.calculate.roll();
                    if (m[1] + m[0] > 490) {
                        a.roll.foward(2);
                    }
                });
            },

            roll: {
                foward: function (step) {
                    var m = a.calculate.roll();
                    a.obj.find("ul").animate({ "marginTop": m[0] - 125 * step }, 100);
                },
                next: function (step) {
                    var m = a.calculate.roll();
                    a.obj.find("ul").animate({ "marginTop": m[0] + 125 * step }, 100)
                }
            },

            init: function () {
                a.obj.jdMarquee({
                    deriction: "up",
                    width: auto,
                    height: 490,
                    step: 1,
                    speed: 4,
                    delay: 10,
                    control: false
                });

                a.obj.find("ul").eq(1).remove();
                //var h=a.obj.find("li").length;
                var h = 8;
                a.obj.find("ul").css({ "height": h * 125 });
                a.images();
                a.swith();
                a.list();
                a.alpha();

            }
        };

        a.init();

    })(jQuery)
    //每次刷新页面等ClearSession,当页面文档完全加载，并且图像呈现时会执行该事件. 
</script>
<form id="form1" runat="server" visible="false">
<div data-role="page" >
<div data-role="header"  data-position="fixed">
    <h1 runat="server" id="topWord"></h1>
</div>
<!-- /header -->

  <div id="mainPicture" >  
    <video id="video" autoplay="autoplay" style="width:75%; float:left;margin-left:50px;"></video>
        
  <div id="result" style="float:right;">
        
  </div> 
    
       <div>
    <input type="button" value="拍照" onclick="shoot()"  style="width:20%;height:150px;margin-left:150px;"/>
    <input type="button" value="上传"  style="width:20%;height:150px;margin-left:150px;"/>
       </div>
  
    <script type="text/javascript">


        var imgArray = new Array();
        var video = document.getElementById("video");
        navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia;
        if (navigator.getUserMedia) //  
        {
            if (navigator.webkitURL)//  
            {
                navigator.getUserMedia({ video: true }, function (stream) {
                    video.src = window.webkitURL.createObjectURL(stream);
                }, function (error) { alert(error); });
            }
            else //  
            {
                navigator.getUserMedia({ video: true }, function (stream) {
                    video.src = window.webkitURL.createObjectURL(stream);
                }, function (error) { alert(error); });
            }
        }

        //執行拍照
        function shoot() {
            var video = $("#video")[0];
            var canvas = capture(video);
            //$("#result").empty();
            //呈現圖像(拍照結果)
            var imgData = canvas.toDataURL("image/jpg");
            var base64String = imgData.substr(22); //取得base64字串

            //上傳，儲存圖片
            $.ajax({
                url: "Camera.aspx",
                type: "post",
                data: { data: base64String, type: "image" },
                async: true,
                success: function (htmlVal) {//htmlVal
                    //$("#result").append(canvas);
                    imgArray[imgArray.length] = canvas;
                    var img = "<img style='width:150px;height:150px;' src='"
                    img += htmlVal;
                    img += "' /><br/>";
                    $("#result").append(img);
                }, error: function (e) {
                    alert(e.responseText); //alert錯誤訊息
                }

            });
        }

        //從video元素抓取圖像到canvas
        function capture(video) {
            var canvas = document.createElement('canvas'); //建立canvas js DOM元素
            canvas.width = video.videoWidth;
            canvas.height = video.videoHeight;
            var ctx = canvas.getContext('2d');
            ctx.drawImage(video, 0, 0);
            return canvas;
        }

        //function preView()
        //{
        //    for (i = 0; i < imgArray.length; i++)
        //    {
        //        //$("#result").append("<div id='pic1' style='width:150px;height:150px;'></div>");
        //        // $("#pic1").append(imgArray[i]);

        //        $("#result2").append(imgArray[i]);
        //    }
        //}
</script>
      

<div id="Complete" style="display:none;">
       <div style="margin-top:50px;"></div>
<!--添加完成后显示该页，看继续添加，还是查看内容-->
       <div style="font-size:large"><label>添加成功，你是要继续添加，还是查看刚添加的内容</label></div>
       <input type="button" value="继续添加"  onclick="DivTab()"/>
           <br />
       <asp:Button ID="readContent" runat="server" text="查看内容" />
           <br />
       <input type="button" value="返回首页"  onclick="location.href = 'default.aspx'"/>
           </div>
       <input type="hidden" value="" id="Info" runat="server" />
<!-- /content -->
<div data-role="footer"  data-position="fixed">
    <h6>&copy Shanghai Zoomla!CMS Software technology Co., LTD all rights reserved</h6>
</div>
<!-- /footer -->
</div>
<!-- /page -->
   
</form>
</asp:Content>
