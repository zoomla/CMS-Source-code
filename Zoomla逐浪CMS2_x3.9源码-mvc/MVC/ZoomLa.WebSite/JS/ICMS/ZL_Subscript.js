if (!window.jQuery) { document.write("<span>请在该页面引入jqurey，以确保功能正常使用</span"); }
else {
    $(function () {
        //订阅窗口
        var diaghtml = "<div id=\"Submail\">"
    + "<input id=\"subscript_email\" type=\"text\" placeholder=\"您的邮箱\">"
    + "<button id=\"subscript_submit\" type=\"button\">邮件订阅</button>"
    + "<div id=\"subscript_tips\"></div>"
    + "</div>";
        //    var diaghtml = "<div id=\"subscript_diag\" style=\"position:absolute;top:20%;right:15px;\">"
        //+ "<table>"
        //+ "<tr><td class=\"text-right\">订阅邮箱:</td><td><input type=\"text\" id=\"subscript_email\" placeholder=\"您的邮箱\" /></td></tr>"
        //+ "<tr><td class=\"text-center\" colspan=\"2\"><button type=\"button\" id=\"subscript_submit\">我要订阅</button></td></tr>"
        //+ "</table>"
        //+ "<div id=\"subscript_tips\"></div>"
        //+ "</div>";
        $("body").append(diaghtml);
        ////显示订阅窗口
        //$("#subscript_btn").click(function () {
        //    $("#subscript_diag").show();
        //});
        //关闭订阅窗口
        //$("#subscript_close").click(function () {
        //    $("#subscript_email").val("");
        //    ShowTips("");
        //    $("#subscript_diag").hide();
        //});
        $("#subscript_email").keydown(function (event) {
            var event = event ? event : window.event;
            if (event.keyCode == 13 && $("#subscript_submit").attr("disabled") != "disabled") {
                $("#subscript_submit").click();
            }
        })
        $("#subscript_submit").click(function () {
            ShowTips("");
            if (!checkdata()) {
                ShowTips("邮箱格式错误!", 2);
                return;
            }
            $(this).attr("disabled", "disabled");
            ShowTips("正在提交...", 1);
            $.post("/common/SubScriptCheck.aspx", { action: "addsub", email: $("#subscript_email").val() }, function (data) {
                switch (data) {
                    case "1":
                        var mailurl = "http://mail." + $("#subscript_email").val().split('@')[1];
                        ShowTips("提交成功!请前往<a href='" + mailurl + "' target='_blank'>您的邮箱</a>确认验证邮件", 3);
                        break;
                    case "-1":
                        ShowTips("该邮箱已订阅!", 2);
                        break;
                    case "-2":
                        ShowTips("邮件发送失败!",2)
                        break;
                    default:
                        ShowTips("未知错误!", 2);
                        break;
                }
                $("#subscript_submit").removeAttr("disabled");
            });
        });
        function ShowTips(val, type) {
            var tlp = "@val";
            switch (type) {
                case 1://等待提示
                    tlp = "<span style='color:gray;'><i class='fa fa-spinner fa-spin'></i> @val</span>";
                    break;
                case 2://错误提示
                    tlp = "<span style='color:red;'><i class='fa fa-info-circle'></i> @val</span>";
                    break;
                case 3://成功提示
                    tlp = "<span style='color:green'><i class='fa fa-check-circle'></i> @val</span>";
                    break;
            }
            $("#subscript_tips").html(tlp.replace("@val", val));
        }
        function checkdata() {
            var patrn = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/gi;
            return patrn.exec($("#subscript_email").val()) ? true : false;
        }


        //样式风格
        var styledef = "<style>"
    + "</style>";
        $('body').append(styledef);
    });
}

