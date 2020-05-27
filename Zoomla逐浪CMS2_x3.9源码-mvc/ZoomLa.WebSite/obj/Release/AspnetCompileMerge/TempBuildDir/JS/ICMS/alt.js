if (parent && parent.ShowMain) {
   var  ShowMain = parent.ShowMain;
   var showleft = parent.showleft;
}
function lock(evt) {//alt+ 快捷键
    var a;
    var res;
    if (navigator.appName == "Microsoft Internet Explorer") {
        a = event.keyCode;
        res = event.altKey;
    } else {
        a = evt.which;
        res = evt.altKey;
    }
    if (a == 13) {
        event.keyCode = 9;
    }
    if ((a == 81) && res) {
        showWindow('Lockin.aspx', 900, 460);
    }
    if ((a == 85) && res) {
        window.open("/User/");
    }
    if ((a == 74) && res) {
        modalDialog('/Common/calc.html', 'calculator', 420, 320)
    }
    if ((a == 73) && res) {
        window.open("/");
    }
    if ((a == 68) && res) {
        ShowMain('', '/Common/SelectChinaDay.html');
        //window.location.href = "?ShowCaculator=true";
    }
    if ((a == 72) && res) {
        window.open("../../help.html");
    }
    if ((a == 77) && res) {
        showleft('menu8_2', 'Content/ModelManage.aspx?ModelType=1');
    }
    if ((a == 84) && res) {
        showleft('menu8_6','Template/TemplateSet.aspx');
    }
    if ((a == 83) && res) {
        showleft('menu8_6', 'Template/CSSManage.aspx');
    }
    if ((a == 76) && res) {
        ShowMain('LabelGuide.ascx', 'Template/LabelManage.aspx');
    }
    if ((a == 67) && res) {
        showleft('menu8_6', 'Template/CloudLead.aspx');

    }
    if ((a == 78) && res) {
        ShowMain('','Main.aspx'); 

    }
	 if ((a == 65) && res) {
	     ShowMain('LabelGuide.ascx','Template/LabelCallTab.aspx'); 
	 }
	 if ((a == 70) && evt.ctrlKey && evt.shiftKey) {//翻译快捷
	     if (!$("#BaiduTrans_btn")[0]) {;$("body").append("<button type='button' id='BaiduTrans_btn' style='display:none;'></button>"); }
	     $("#BaiduTrans_btn").TransTools();
	     $("#BaiduTrans_btn").click();
	 }
}
$(document).keydown(lock);
function modalDialog(url, name, width, height)
{
    if (width == undefined) {
        width = 600;
    }
    if (height == undefined) {
        height = 500;
    }
    if (window.showModalDialog) {
        window.showModalDialog(url, name, 'dialogWidth=' + (width) + 'px; dialogHeight=' + (height + 5) + 'px; status=off');
    }
    else {
        x = (window.screen.width - width) / 2;
        y = (window.screen.height - height) / 2;
        window.open(url, name, 'height=' + height + ', width=' + width + ', left=' + x + ', top=' + y + ', toolbar=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, modal=yes');
    }
}