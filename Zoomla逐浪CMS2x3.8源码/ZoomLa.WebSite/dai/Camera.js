
var Camera = {
    res: { stream: null, video: null, canvas: null },
    //初始化
    init: function (videoID) {
        if (videoID) { Camera.res.video = document.getElementById(videoID); }
        Camera.res.canvas = document.createElement("canvas");
        Camera.res.canvas.width = this.res.video.width;
        Camera.res.canvas.height = this.res.video.height;
    },
    //打开摄像头
    open: function () {
        navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia;
        if (navigator.getUserMedia) {
            navigator.getUserMedia({ video: true }, Camera.open_succed, Camera.open_fail);
        }
        else { console.log("浏览器不支持"); return false; }
    },
    open_succed: function (stream) {
        window.URL = window.URL || window.webkitURL || window.msURL || window.oURL;
        Camera.res.stream = stream;
        Camera.res.video.src = (window.URL && window.URL.createObjectURL) ? window.URL.createObjectURL(Camera.res.stream) : Camera.res.stream;
        console.log("打开成功",Camera.res.video.src);
    },
    open_fail: function (error_details) { console.log("打开失败", error_details); },
    //关闭摄像头
    stop: function () {
        if (Camera.res.stream) {
            Camera.res.stream.stop(); Camera.res.stream = null;
        }
        Camera.res.video.src = "";
        console.log("已停止");
    },
    //拍照,并返回字符串
    shot: function () {
        var ctx = Camera.res.canvas.getContext("2d");
        ctx.drawImage(Camera.res.video, 0, 0);
        var imgData = Camera.res.canvas.toDataURL();
        var base64Data = imgData.substr(22);
        //Camera.save(base64Data, function (url) { $("#test_img").attr("src", url); });
        return base64Data;
    },
    //传往服务器
    save: function (base64, callback) {
        SFileUP.AjaxUpBase64(base64, function (url) { callback(url); });
    }
};
//Camera.init("cam_vdo");
//Camera.open();