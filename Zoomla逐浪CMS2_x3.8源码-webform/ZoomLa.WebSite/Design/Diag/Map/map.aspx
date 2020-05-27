<%@ Page Language="C#" AutoEventWireup="true" CodeFile="map.aspx.cs" Inherits="Design_Diag_Map_map" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>地图管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="mapbody"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css">
        #mapbody {width:100%; }
    </style>
    <script src="http://api.map.baidu.com/api?v=2.0&ak=A8nKkhhnf81lQGCFFH3k8l2A"></script>
    <script>
        $("#mapbody").height($(window).height());
        var map = new BMap.Map("mapbody");
        //map.disableScrollWheelZoom();
        var maphelper = {};
        maphelper.GetCurPos = function (callback) {
            //获取当前所在的坐标与省市县
            //r.point,r.address
            var geolocation = new BMap.Geolocation();
            geolocation.getCurrentPosition(function (r) {
                if (this.getStatus() == BMAP_STATUS_SUCCESS) {
                    if (callback) { callback(r); }
                    return r;
                }
                else {
                    return null;
                    console.log("地址位置获取失败");
                }
            }, { enableHighAccuracy: true })
        }
        maphelper.GetCurPos(function (r) {
            map.centerAndZoom(r.point, 18);
            var mk = new BMap.Marker(r.point);
            map.addOverlay(mk);//将标注添加至地图
            map.panTo(r.point);
            //弹出信息窗口的事件了
            //var infoWindow = new BMap.InfoWindow("<a target='_blank' href='www.z01.com'>详情</a>");  // 创建信息窗口对象
            //mk.addEventListener("click", function () {//给标注添加点击事件
            //    this.openInfoWindow(infoWindow);
            //});
        });
    </script>
</asp:Content>