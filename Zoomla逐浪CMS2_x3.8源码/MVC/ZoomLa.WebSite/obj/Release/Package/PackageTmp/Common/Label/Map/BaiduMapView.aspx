<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaiduMapView.aspx.cs" Inherits="ZoomLaCMS.Common.Label.Map.BaiduMapView" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>地图浏览</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="allmap" style="width:100%;height:100%;"></div>
    <div id="conver_div" title="编辑地图" style="width:100%;height:100%;position:absolute;z-index:9999;top:0;cursor:pointer;display:none;"></div>
    <asp:HiddenField runat="server" ID="MapData_Hid" />
    <div id="infowin_div"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=WRiR4XARbMRjm3NrQuP88w6P"></script>
<script type="text/javascript">
    var map = new BMap.Map("allmap");
    var infoWin = new BMap.InfoWindow(document.getElementById("infowin_div"), { offset: new BMap.Size(0, -10)});
    var fieldname = '<%:Field%>';
    var type="<%:Type%>";
    //map.enableScrollWheelZoom();
    //map.enableInertialDragging();
    //map.enableContinuousZoom();
    function preMode() {
        $("#conver_div").height(window.height);
        $("#conver_div").show();
    }
    $(function () {
        switch (type) {
            case "simp":
                $("#conver_div").click(function () {
                    parent.ShowDiag("/Common/Label/Map/BaiduMapSimp.aspx?Field=<%=Field%>&Point=<%:Point %>", "地图定位(双击地图选择)");
                });
                var point = new BMap.Point(<%:Point %>);
                var marker = new BMap.Marker(point);
                map.addOverlay(marker);
                map.centerAndZoom(point, 14);
                break;
            case "full":
                $("#conver_div").click(function () {
                    parent.ShowDiag("/Common/Label/Map/BaiduMap.aspx?Field=<%=Field%>", "地图定位");
                });
                MyBind();
                break;
        }
    });
    function MyBind() {
        var point = {};
        var val = $("#MapData_Hid").val();
        if (val == "") { val = $(parent.document).find("#txt_" + fieldname).val(); }
        if (!val || val == "" || val == "[]") { point = new BMap.Point(116.404, 39.915); map.centerAndZoom(point, 14); return; }
        var datas = JSON.parse(val);
        var firstdata = datas[0];
        point = new BMap.Point(firstdata.mark.lng, firstdata.mark.lat);
        var addMark = function (lng, lat, src) {
            var point = new BMap.Point(lng, lat);
            var myIcon = new BMap.Icon(src, new BMap.Size(60, 60));
            var mark = new BMap.Marker(point, { icon: myIcon });
            map.addOverlay(mark);
            return mark;
        }
        for (var i = 0; i < datas.length; i++) {
            var model= datas[i];
            var mark = addMark(model.mark.lng, model.mark.lat, model.icon);
            mark.content = model.content;
            mark.addEventListener("click", function () {
                var mark = this;
                infoWin.content = decodeURIComponent(mark.content);
                mark.openInfoWindow(infoWin);
            })
        }
        map.centerAndZoom(point, 14);
    }
</script>
</asp:Content>

