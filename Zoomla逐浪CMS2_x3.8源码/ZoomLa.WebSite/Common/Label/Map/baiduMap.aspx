<%@ Page Language="C#" AutoEventWireup="true" CodeFile="baiduMap.aspx.cs" Inherits="User_UserShop_baiduMap" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>地图定位</title>
    <style type="text/css">
        body, html {width: 100%;height: 100%;margin: 0;font-family: "微软雅黑";}
        #allmap {height:500px; width: 100%;position: relative;z-index: 1;}
        #ToolBoxs_div {width:150px;}
        #ToolBoxs_div ul>li {float:left;margin-left:5px;margin-top:5px; cursor:pointer;}
        #ToolBoxs_div ul > li img {width:40px;height:40px;}
        
        .infowin {margin-top:10px;}
        .infowin .infotxt {width:100%;height:200px;}
        .infowin .info-footer {margin-top:65px;text-align:center;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="input-group" style="width: 510px;">
        <span class="input-group-addon">城市</span>
        <input type="text" id="city_t" class="form-control text_md num" placeholder="请输入城市"  style="border-right: none;">
        <span class="input-group-addon">地址</span>
        <input type="text" id="address_t" class="form-control text_md num" placeholder="请输入地址" style="border-left:none;">
        <span class="input-group-btn">
            <input type="button" value="搜索" onclick="doSearch();" class="btn btn-primary">
        </span>
    </div>
    <div id="allmap"></div>
    <div style="position:fixed;bottom:0px;width:100%;text-align:center;">
        <input type="button" value="保存定位" class="btn btn-info" onclick="saveMapData();" />
        <input type="button" value="取消定位" class="btn btn-info" onclick="parent.CloseDiag();" />
    </div>
    <div class="infowin" id="infowin" style="display:none;">
        <div>
            <textarea class="infotxt" id="wincon_t"></textarea>
        </div>
        <div class="info-footer">
            <input type="button" class="btn btn-info" value="确认" onclick="setMarkLabel();" />
            <%--      <input type="button" class="btn btn-default" value="重填" style="margin-left: 15px;" onclick="res.editor.setContent('');"/>--%>
            <input type="button" class="btn btn-danger" value="移除" style="margin-left: 15px;" onclick="res.removeMark();" />
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="http://api.map.baidu.com/api?v=2.0&ak=WRiR4XARbMRjm3NrQuP88w6P"></script>
<%--<script src="res/CityList_min.js"></script>--%>
<script src="res/MarkerTool.js"></script>
<script src="/Plugins/Ueditor/ueditor.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<script type="text/javascript">
    var field = "<%:Field%>";
    var map = new BMap.Map("allmap");
    var mkrTool = new BMapLib.MarkerTool(map, { autoClose: true, followText: "" });
    //信息窗口
    var infoWin = new BMap.InfoWindow(document.getElementById("infowin"), { offset: new BMap.Size(0, -10), width: 500, height: 330 });
    mkrTool.addEventListener("markend", function (evt) {
        var mkr = evt.marker;
        afterMarked(mkr);
    });
    //单击地图上标记
    function markClick() {
        var mkr = this;
        if (res.editor == "") { $("#infowin").show(); res.editor = UE.getEditor("wincon_t", {<%=ZoomLa.BLL.BLLCommon.ueditorMinEx%> }); }
        res.curMark = mkr;
        mkr.openInfoWindow(infoWin);
        var model = res.getMark(mkr);
        setTimeout(function () { res.editor.setContent(decodeURI(model.content)); }, 300);
    }
    //--------------
    var res = {
        curMark: null, curIconSrc: null, editor: "", saveData: [],//{mark:"",content:"",icon:""}
        getMark: function (marker, action) {
            if (!action) { action = "return"; }//return remove
            //根据坐标获取对应的marker
            var lat = marker.point.lat;
            var lng = marker.point.lng;
            for (var i = 0; i < res.saveData.length; i++) {
                var model = res.saveData[i];
                var point = model.mark.point;
                if (point.lat == lat && point.lng == lng)
                {
                    switch (action)//根据action返回,或移除元素
                    {
                        case "remove":
                            res.saveData.splice(i, 1);
                            break;
                        case "return":
                            return model;
                            break;
                    }
                }
            }
        },
        removeMark: function (marker) {
            //移除当前或指定覆盖物
            if (!marker) { marker=res.curMark; }
            res.getMark(marker, "remove");
            map.removeOverlay(marker);
        }
    };
    //添加自定义标志
    var addMark = function (lng, lat, src) {
        //根据坐标和图片资源,添加覆盖物
        var point = new BMap.Point(lng, lat);//必须仍初始一次
        var myIcon = new BMap.Icon(src, new BMap.Size(60, 60));
        var mark = new BMap.Marker(point, { icon: myIcon });
        map.addOverlay(mark);
        res.curIconSrc = src;
        return mark;
    }
    //添加完标志后执行该方法,弹出窗口
    function afterMarked(mkr) {        
        res.curMark = mkr;
        mkr.openInfoWindow(infoWin);
        if (res.editor == "") { $("#infowin").show(); res.editor = UE.getEditor("wincon_t", {<%=ZoomLa.BLL.BLLCommon.ueditorMinEx%> }); }
        res.saveData.push({ "mark": mkr, content: "", icon: res.curIconSrc });
        mkr.addEventListener("click", markClick);
    }
    //---------------
    function InitMap() {
        //读取数据,初始化标记
        var val = $(parent.document).find("#txt_" + field).val();
        if (!val || val == "" || val == "[]") { res.saveData = []; point = new BMap.Point(116.404, 39.915); map.centerAndZoom(point, 14); map.enableScrollWheelZoom(); return; }
        res.saveData = JSON.parse(val);

        for (var i = 0; i < res.saveData.length; i++) {
            var model = res.saveData[i];
            model.mark = addMark(model.mark.lng, model.mark.lat, model.icon);
            model.mark.addEventListener("click", markClick);
        }
        var point = res.saveData[0].mark.point;
        map.centerAndZoom(point, 14);
        map.enableScrollWheelZoom();
    }
    //存储定位数据
    function saveMapData() {
        //1,只保存坐标与内容
        for (var i = 0; i < res.saveData.length; i++) {
          res.saveData[i].mark = res.saveData[i].mark.point;
        }
        var val = JSON.stringify(res.saveData);
        $(parent.document).find("#txt_" + field).val(val);
        parent.CloseDiag();
    }
    //----------------
    //自定义工具箱控件
    function ToolBoxs() {
        // 默认停靠位置和偏移量
        //this.defaultAnchor = BMAP_ANCHOR_TOP_LEFT;
        this.defaultAnchor = BMAP_ANCHOR_TOP_RIGHT;
        this.defaultOffset = new BMap.Size(10, 10);
    }
    ToolBoxs.prototype = new BMap.Control();
    ToolBoxs.prototype.initialize = function (map) {
        var html = "<div id=\"ToolBoxs_div\">"
            + "<ul class=\"list-unstyled\">"
             + "<li title=\"选择标记\"><img src=\"/Common/Label/Map/Img/f1.png\" /></li>"
            + "<li title=\"选择标记\"><img src=\"/Common/Label/Map/Img/f2.png\" /></li>"
            + "<li title=\"选择标记\"><img src=\"/Common/Label/Map/Img/f3.png\" /></li>"
            + "<li title=\"选择标记\"><img src=\"/Common/Label/Map/Img/f4.png\" /></li>"
            + "<li title=\"选择标记\"><img src=\"/Common/Label/Map/Img/f5.png\" /></li>"
            + "<li title=\"选择标记\"><img src=\"/Common/Label/Map/Img/f6.png\" /></li>"
            + "<li title=\"选择标记\"><img src=\"/Common/Label/Map/Img/f7.png\" /></li>"
            + "<li title=\"选择标记\"><img src=\"/Common/Label/Map/Img/f8.png\" /></li>"
            + "<li title=\"选择标记\"><img src=\"/Common/Label/Map/Img/f9.png\" /></li>"
            + "</ul>"
            + "</div>";
        var div = $(html)[0];
        //单击后才允许放置标记
        $(div).find("li").click(function () {
            var src = $(this).find("img").attr("src");
            res.curIconSrc = src;
            var icon = new BMap.Icon(src, new BMap.Size(60, 60));
            mkrTool.open(); //打开工具 
            mkrTool.setIcon(icon);
        });
        //添加dom元素到地图中
        map.getContainer().appendChild(div);
        return div;
    }
    var toolBoxs = new ToolBoxs();
    map.addControl(toolBoxs);
    var setMarkLabel = function () {
        if (infoWin.isOpen()) {
            map.closeInfoWindow();
        }
        var model = res.getMark(res.curMark);
        model.content = encodeURI(res.editor.getContent());
    }
    //-----------
    InitMap();
    //var point = new BMap.Point(116.404, 39.915);
    //map.centerAndZoom(point, 14);

    map.enableInertialDragging();
    map.enableContinuousZoom();
    // 显示城市
    //var size = new BMap.Size(10, 20);
    //map.addControl(new BMap.CityListControl({
    //    anchor: BMAP_ANCHOR_TOP_LEFT,
    //    offset: size,
    //}));
    map.addControl(new BMap.NavigationControl());
    //城市搜索
    function doSearch() {
        if (!document.getElementById('city_t').value) { alert("请先指定城市"); return; }
        var search = new BMap.LocalSearch(document.getElementById('city_t').value, {
            onSearchComplete: function (results) {
                if (results && results.getNumPois()) {
                    var points = [];
                    for (var i = 0; i < results.getCurrentNumPois() ; i++) {
                        points.push(results.getPoi(i).point);
                    }
                    if (points.length > 1) {
                        map.setViewport(points);
                    } else {
                        map.centerAndZoom(points[0], 13);
                    }
                    point = map.getCenter();
                    //var marker = new BMap.Marker(new BMap.Point(point));
                    //marker.setPoint(point);
                    var mkr = addMark(point.lng, point.lat, "/Common/Label/Map/Img/f1.png");
                    afterMarked(mkr);
                } else {
                    alert("没有找到对应的地区");
                }
            }
        });
        search.search(document.getElementById('address_t').value || document.getElementById('city_t').value);
    }
    $("#city_t,#address_t").keyup(function (e) {
        if (e.keyCode == 13) { doSearch(); }
    });
</script>
</asp:Content>