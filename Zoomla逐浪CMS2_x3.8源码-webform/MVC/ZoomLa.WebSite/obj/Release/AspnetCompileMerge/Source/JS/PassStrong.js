
function showPwRank(obj, pwRank) {
    var apass1 = document.getElementById("apass1");
    var apass2 = document.getElementById("apass2");
    var apass3 = document.getElementById("apass3");
    switch (pwRank) {
        case 0:
        case 1:
            apass1.className = "a_man";
            apass2.className = "a_bor";
            apass3.className = "a_bor";
            obj.innerText = " 密码强度：低";

            break;
        case 2:
            apass1.className = "a_man";
            apass2.className = "a_man";
            apass3.className = "a_bor";
            obj.innerText = " 密码强度：中";

            break;
        case 3:
            apass1.className = "a_man";
            apass2.className = "a_man";
            apass3.className = "a_man";
            obj.innerText = " 密码强度：高";

            break;
    }
}
function f_CalcPwdRank(l_Content) {
    var ls = 0;
    if (l_Content.match(/[a-z]/g)) { ls++; }
    if (l_Content.match(/[A-Z]/g)) { ls++; }
    if (l_Content.match(/[0-9]/g)) { ls++; }
    if (l_Content.match(/[^a-zA-Z0-9]/g)) { ls++; }
    if (l_Content.length < 8 && ls > 1) {
        ls = 1;
    }
    if (ls > 3) {
        ls = 3;
    };
    return ls;
};

function f_checkrank() //符合强度后返回true
{
    document.getElementById("valPass").style.display = "block";
    var obj = document.getElementById("idshow");
    var l_Content = document.getElementById("TxtPassword").value;
    if (l_Content.length < 6 || /^[0-9]{1,8}$/.test(l_Content)) { showPwRank(obj, 0); return 0; }
    var ls = f_CalcPwdRank(l_Content);

    switch (ls) {
        case 0:    //不显示
        case 1:    //弱
        case 2:    //中
        case 3:    //强 
            showPwRank(obj, ls);
            break;
        default:
            showPwRank(obj, 3);
    }
    return ls;
};

function focusinput() {
    var l_Content = document.getElementById("TxtPassword").value;
    if (l_Content.length > 0) {
        document.getElementById("valPass").style.display = "block";
    }
}
function PwdCheck(source, args) {
    var ls = 0;
    var l_Content = document.getElementById("TxtPassword").value;
    if (l_Content.length < 6 || /^[0-9]{1,8}$/.test(l_Content)) { ls = 0; }
    else { ls = f_CalcPwdRank(l_Content); }
    args.IsValid = (ls > 1);
}

