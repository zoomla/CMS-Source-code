var myChart; 
var domMain = document.getElementById('main');
var iconResize = document.getElementById('icon-resize');
var needRefresh = false;
var curTheme;
function requireCallback(ec, defaultTheme) {
    curTheme = {};
    echarts = ec;
    refresh();
    window.onresize = myChart.resize;
}

function refresh(isBtnRefresh) {
    //if (isBtnRefresh) {
    //    needRefresh = true;
    //    return;
    //}
    needRefresh = false;
    if (myChart && myChart.dispose) {
        myChart.dispose();
    }
    myChart = echarts.init(domMain);
    window.onresize = myChart.resize;
    (new Function(document.getElementById("code").value))();
    myChart.setOption(option, true)
}

function needMap() {
    var href = location.href;
    return href.indexOf('map') != -1
           || href.indexOf('mix3') != -1
           || href.indexOf('mix5') != -1
           || href.indexOf('dataRange') != -1;

}

var echarts;

