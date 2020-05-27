var shortPassword = function () {
    var t = function (t) {
        this.targetElement = t,
        this.pwdValue = "",
        this.typing()
    };
    t.prototype.typing = function () {
        var t = this,
        e = t.targetElement.find("[node-type=shortPassword-input]");
        e.bind("click",
        function () {
            var i = t.pwdValue.length;
            0 == i ? e[0].focus() : (i = i >= e.length ? e.length - 1 : i, e[i].focus())
        }),
        e.bind("keydown",
        function (t) {
            8 != t.keyCode && 46 != t.keyCode ? $.trim($(this).val().length) > 0 && $(this).next("[node-type=shortPassword-input]").focus() : 0 == $.trim($(this).val().length) ? $(this).prev("[node-type=shortPassword-input]").focus() : $(this).val("")
        }),
        e.bind("keyup",
        function () {
            t.copy()
        })
    },
    t.prototype.copy = function () {
        var t = "";
        this.targetElement.find("[node-type=shortPassword-input]").each(function () {
            t += $.trim($(this).val())
        }),
        this.pwdValue = t,
        this.targetElement.find("[node-type=shortPassword-value]").val(this.pwdValue);
    };
    var e = $("[node-type=shortPassword]");
    e.each(function () {
        new t($(this))
    })
};
//<div class="shortPwd_div">
//    <span class="shortPwd" node-type="shortPassword">
//        <input type="password" class="shortPwd-input" node-type="shortPassword-input" autocomplete="new-password" maxlength="1" />
//        <input type="password" class="shortPwd-input" node-type="shortPassword-input" autocomplete="new-password" maxlength="1" />
//        <input type="password" class="shortPwd-input" node-type="shortPassword-input" autocomplete="new-password" maxlength="1" />
//        <input type="password" class="shortPwd-input" node-type="shortPassword-input" autocomplete="new-password" maxlength="1" />
//        <input type="password" class="shortPwd-input" node-type="shortPassword-input" autocomplete="new-password" maxlength="1" />
//        <input type="password" class="shortPwd-input" node-type="shortPassword-input" autocomplete="new-password" maxlength="1" />
//        <input type="password" name="chk_pwd" id="chk_pwd" class="shortPwd-hidden" node-type="shortPassword-value" maxlength="6" />
//    </span>
//</div>