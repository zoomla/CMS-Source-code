var _speedMark = new Date();
function init_viewport() {
    if (/Android (\d+\.\d+)/.test(navigator.userAgent)) {
        var version = parseFloat(RegExp.$1);

        if (version > 2.3) {
            var width = window.outerWidth == 0 ? window.screen.width : window.outerWidth;
            var phoneScale = parseInt(width) / 500;
            // if(phoneScale < 2)
            // {
            document.write('<meta name="viewport" content="width=500, minimum-scale = ' + phoneScale + ', maximum-scale = ' + phoneScale + ', target-densitydpi=device-dpi">');

            // }
            // else
            // {
            //     document.write('<meta name="viewport" content="target-densitydpi=320, width=500, min-height:850px, user-scalable=no"/>');
            // }
        }
        else {
            document.write('<meta name="viewport" content="width=500, target-densitydpi=device-dpi">');
        }
    }
    else if (navigator.userAgent.indexOf('iPhone') != -1) {
        var phoneScale = parseInt(window.screen.width) / 500;
        document.write('<meta name="viewport" content="width=500; min-height=750;initial-scale=' + phoneScale + '; user-scalable=no;" /> ');         //0.75   0.82
    }
    else {
        document.write('<meta name="viewport" content="width=500, height=750, initial-scale=0.64" /> ');         //0.75   0.82

    }
}

init_viewport();





var module_inits = [];

var upload_permission = true;
var atkname = 'xinqing';

wx.config({
    debug: false,
    appId: 'wx4dd4bc6712e52cda',
    timestamp: 10000,
    nonceStr: 'helloxinqing',
    signature: '2ccfd24e55bdb13ab232451ab2f2b2258e799768',
    jsApiList: [
        'onMenuShareTimeline', 'onMenuShareAppMessage', 'onMenuShareQQ', 'onMenuShareQZone', 'closeWindow', 'hideAllNonBaseMenuItem', 'showMenuItems', 'showAllNonBaseMenuItem', 'chooseImage', 'uploadImage']
});

function load_init_modules() {
    for (var i = 0; i < module_inits.length; i++) {
        module_inits[i]();
    }
}

function call_me(fun) {
    module_inits.push(fun);
}

var ua = navigator.userAgent.toLowerCase();

if (ua.match(/MicroMessenger/i) == 'micromessenger') {
    wx.ready(load_init_modules);
}
else {
    onload = load_init_modules;
}

var date = 20160930;
var zan_num = 0;
var e_bookid = 'dD2nhG888UxWZ9qbz1_LXjIs7hV9LjVhPVmwpJ5UxaM';
var e_openid = null;
var e_scene = 'qiqiu';
var editmode = false;
var wxid = 'liti';
var shareid = '';
var guanzhu_url = '';
var words = { "li_746734895270": "\u5bb4\u4f1a\u65f6\u95f49\u670822\u201523\u53f7", "li_746757341727": "\u7c73\u5154\u513f\u7ae5\u6444\u5f71\u4e0a\u95e8\u62cd\u6444" };
var app = '';
var e_music_title = '';
var tbssaveoff = false;
var isguangchang = '';


if (typeof (objid) === "undefined") {
    var objid = function (id) {
        return document.getElementById(id);
    }
}

function random(min, max) {
    return Math.floor(min + Math.random() * (max - min));
}


function on_wx_music_init() {
    if (e_desc != "") {
        document.title = e_desc.replace("<br>", "\n").replace("<br/>", "\n");;
    }
    else {
        document.title = "音乐相册";
    }
    loadingdiv_out();
    create_music();

    var locationurl = window.location.href;
    check_cookie();


}

//创建存储cookie的函数
function set_cookie(value) {
    // value = "";
    // if(wxid == 'youyi')
    // 	document.cookie = "books="+value+';domain=.kawabody.cn';
    // else
    document.cookie = "books=" + value + ';domain=.kagirl.cn';
    // alert('write:'+document.cookie);
}

function get_cookie(c_name) {
    // alert(document.cookie);
    if (document.cookie.length > 0) {
        var c_start = document.cookie.indexOf(c_name + '=');
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            var c_end = document.cookie.indexOf(';', c_start);
            if (c_end == -1)
                c_end = document.cookie.length;
            return unescape(document.cookie.substring(c_start, c_end));
        }

    }
    return '';
}


function check_cookie() {


}



call_me(on_wx_music_init);

//音乐播放

var music_header = 'http://kawaweika.qiniudn.com/sound/';
var e_music_player = new Audio();

function play_music() {
    if (e_music_url == '') {
        return;
    }

    if (e_music_url.indexOf('qqmusic') != -1) {
        mymusic = 'https://qnphoto.kagirl.cn/music/688042.m4a';
    }
    else {
        mymusic = e_music_url;
    }

    e_music_player.src = mymusic;
    e_music_player.loop = 'loop';

    //console.log(e_music_player.src);
    e_music_player.onerror = function () {
        if (e_music_url.indexOf("http://sound.kagirl.net") != -1) {
            var url = e_music_url.replace("http://sound.kagirl.net", "https://music.kagirl.cn");
        }
        else {
            var url = e_music_url.replace("https://music.kagirl.cn", "http://sound.kagirl.net");
        }

        e_music_player.src = url;
        e_music_player.play();
        e_music_player.onerror = null;
    }

    e_music_player.play();

    //检查音乐是否缓冲成功
    setTimeout(function () {
        var timeRanges = e_music_player.buffered;
        var timeBuffered = timeRanges.end(timeRanges.length - 1);
        var bufferPercent = timeBuffered / e_music_player.duration;
        //console.log('music:'+bufferPercent);

        if (isNaN(bufferPercent) || bufferPercent == 0) {
            if (e_music_url.indexOf("http://sound.kagirl.net") != -1) {
                var url = e_music_url.replace("http://sound.kagirl.net", "https://music.kagirl.cn");
            }
            else {
                var url = e_music_url.replace("https://music.kagirl.cn", "http://sound.kagirl.net");
            }
            e_music_player.src = url;
            e_music_player.play();
        }
    }, 5000);


    if (objid('sound_image')) {
        objid('sound_image').style.webkitAnimation = "zhuan 1s linear infinite";
    }

    bplay = 1;
}

function create_music() {
    if (e_music_url == '') {
        return;
    }

    play_music();

    sound_div = document.createElement("div");
    sound_div.setAttribute("ID", "cardsound");
    sound_div.style.cssText = "position:fixed;right:20px;top:25px;z-index:50;visibility:visible;";
    sound_div.onclick = switchsound;

    bg_htm = "<img id='sound_image' src='/design/album/tlps/3/res/music_note_big.png' style='-webkit-animation:zhuan 1s linear infinite'>";
    txt_htm = "<div id='music_txt' style='color:white;position:absolute;left:-40px;top:30px;width:60px'></div>"

    sound_div.innerHTML = bg_htm + txt_htm;

    document.body.appendChild(sound_div);
}

var bplay = 0;

function switchsound() {
    au = e_music_player;
    ai = objid('sound_image');
    if (au.paused) {
        bplay = 1;
        au.play();
        ai.style.webkitAnimation = "zhuan 1s linear infinite";
        objid("music_txt").innerHTML = "打开";
        objid("music_txt").style.visibility = "visible";
        setTimeout(function () { objid("music_txt").style.visibility = "hidden" }, 2500);
    }
    else {
        bplay = 0;
        au.pause();
        ai.style.webkitAnimation = "";
        objid("music_txt").innerHTML = "关闭";
        objid("music_txt").style.visibility = "visible";
        setTimeout(function () { objid("music_txt").style.visibility = "hidden" }, 2500);
    }
}



function write_localstorage() {
    //console.log('write');
    objid('like_div').onclick = function () { };
}

function read_localstorage() {

}

function visit_guangchang() {
   
}
function loadingdiv_out() {
    objid('loadingdiv').style.webkitAnimation = 'fadeout 0.3s linear both';
    if (navigator.userAgent.indexOf('Windows Phone') != 1) {
        objid('loadingdiv').style.display = 'none';
    }

    setTimeout(function () {
        objid('loadingdiv').style.display = 'none';
    }, 400)
}