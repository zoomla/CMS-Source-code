function resizeTableColumn(tableId) {
    var tb = document.getElementById(tableId);
    if (!tb) { return; }
    var ths = tb.getElementsByTagName("th");
    if (!ths) { return; }
    for (var i = 0; i < ths.length; i++) {
        var th = ths[i];
        th.style.cursor = 'col-resize';
        th.innerHTML = '<div style="width:99%;line-height:24px;cursor:default;">' + ths[i].innerHTML + '</div>';

        th.onmousedown = function(e) {
            var d = document;
            e = e || window.event;
            var x = e.clientX;//鼠标按下时的位置
            var bLeft = d.body.scrollLeft || 0;
            th = getResizeTh(tb, this, e);
            if (!th) return;
            var thWidth = th.clientWidth;
            var nextTH = th.nextSibling;
            var nextThWidth = nextTH.clientWidth;
            //var tbWidth = tb.clientWidth;
            //设置捕获范围
            if (th.setCapture) {
                th.setCapture();
            } else if (window.captureEvents) {
                window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP);
            }

            var l = d.getElementById(tableId + "_line");
            if (!l) {
                l = d.createElement("div");
                l.className = 'aline';
                l.id = tableId + "_line";
                with (l.style) {
                    position = "absolute";
                    width = "1px";
                    height = "100px";
                    //border="1 solid #000000";   
                    backgroundColor = "#000000";
                    zIndex = "1000";
                }
                d.body.appendChild(l);
            } else {
                l.style.display = "";
            }
            l.style.height = tb.offsetHeight + "px";
            l.style.left = (bLeft + e.clientX) + "px";
            l.style.top = tb.offsetTop + "px";

            d.onmousemove = function(e) {
                e = e || window.event;
                l.style.left = (bLeft + e.clientX) + "px";
                var width = e.clientX - x;
                if (th && Math.abs(width) > 2 && width + x > 0 && nextThWidth - width > 2) {
                    //tb.style.width = (width + tbWidth) + "px";
                    th.style.width = (width + thWidth) + "px";
                    nextTH.style.width = (nextThWidth - width) + "px";
                }
            };

            d.onmouseup = function() {
                //取消捕获范围
                if (th.releaseCapture) {
                    th.releaseCapture();
                } else if (window.captureEvents) {
                    window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP);
                }

                l.style.display = "none";
                //清除事件
                d.onmousemove = null;
                d.onmouseup = null;
            };
        };
    }
}
function getResizeTh(tb, th, e) {
    var tLeft = tb.offsetLeft + th.offsetLeft;
    if (e.clientX - tLeft < 2) {//如果按到的是下一个th
        for (var i = 0; i < tb.rows[0].cells.length; i++) {
            if (tb.rows[0].cells[i] == th) {
                //如果为第一个则跳出
                if (i == 0) return null;
                return tb.rows[0].cells[i - 1];
            }
        }
    } else {
        if (tLeft + th.clientWidth - e.clientX < 2) {
            //如果是最后一个则跳出
            if (th == tb.rows[0].cells[tb.rows[0].cells.length - 1]) return null;
            return th;
        } else {
            return null;
        }
    }
}