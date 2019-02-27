var ZoomLa = {};

/*ajax登陆
*/
ZoomLa.ajaxlogin = function (username, password, callback) {

    var url = "/user/login?" + Math.random() + "";
    $.post(url, {
        "UserName": username,
        "Password": password,
        "postlogin": "True",
        "DropExpiration": "",
        "ajaxlogin": "True"
    },
    function (data) {
        if (data != null && data != "") {
            if (data.indexOf('错误') == -1) {
                //登录成功
                callback();
            }
            else {
                alert('登录失败，请重新登录');
            }
        }
        else {
            alert('登录失败，请重新登录');
        }
    }, "");
}

/*ajax退出
*/
ZoomLa.ajaxlogout = function (callback) {
    var url = "/User/User/Logout?" + Math.random() + "";
    $.post(url, {},
    function (data) {
        callback();
    });
}
ZoomLa.AjaxUrl = "";
function PostToCS(a, v, callback) {
    PostToCS2(ZoomLa.AjaxUrl, a, v, callback);
}
function PostToCS2(u, a, v, callback) {
    $.ajax({
        type: "Post",
        url: u,
        data: { action: a, value: v },
        success: function (data) {
            callback(data);
        },
        error: function (data) {
        }
    });
}
