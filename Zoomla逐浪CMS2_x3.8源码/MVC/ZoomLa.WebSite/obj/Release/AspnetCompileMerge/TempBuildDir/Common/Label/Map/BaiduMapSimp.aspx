<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaiduMapSimp.aspx.cs" Inherits="ZoomLaCMS.Common.Label.Map.BaiduMapSimp" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>地图定位</title>
    <style type="text/css">
        body, html {width: 100%;height: 100%;margin: 0;font-family: "微软雅黑";}
        #allmap { height: 500px;width: 100%;position: relative;z-index: 1;}
    </style>
    <script src="/test/MarkerTool.js"></script>
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
    <div id="r-result"><input type="hidden" id="lat"><input type="hidden" id="lng"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=WRiR4XARbMRjm3NrQuP88w6P"></script>
    <script type="text/javascript">
        var map = new BMap.Map("allmap");
        var fieldname = '<%:Field%>';
        var pointval = '<%:Point %>';
        var point = new BMap.Point(<%:Point %>);
        var marker = new BMap.Marker(point);
        map.addOverlay(marker);
        map.centerAndZoom(point, 14);
        map.enableScrollWheelZoom();
        map.enableInertialDragging();
        map.enableContinuousZoom();
        // 显示城市
        //var size = new BMap.Size(10, 20);
        //map.addControl(new BMap.CityListControl({
        //    anchor: BMAP_ANCHOR_TOP_LEFT,
        //    offset: size,
        //}));
        //点击添加标注事件
        map.addEventListener("dblclick", showInfo);
        function showInfo(e) {
            var pointstr = e.point.lng + "," + e.point.lat;
            $(parent.document).find("#txt_" + fieldname).val(pointstr);
            $(parent.document).find("#" + fieldname + "_ifr").attr("src", "/Common/Label/Map/BaiduMapView.aspx?Field=" + fieldname + "&Point=" + pointstr + "&Type=simp&IsPre=1");
            parent.CloseDiag();
        }
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
                        var mkr = new BMap.Marker(point);
                        map.addOverlay(mkr);
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