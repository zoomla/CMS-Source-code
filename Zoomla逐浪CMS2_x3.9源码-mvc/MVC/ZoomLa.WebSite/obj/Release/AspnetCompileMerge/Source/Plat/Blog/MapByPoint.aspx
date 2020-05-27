<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MapByPoint.aspx.cs" Inherits="ZoomLaCMS.Plat.Blog.MapByPoint" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>查看位置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="headdiv">
       <a href="/Plat/Default.aspx" style="color: #fff;"><i class="fa fa-chevron-left"></i>   返回</a>
    </div>
    <div id="map_div" style="width:100%;min-height:600px;min-width:400px;"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css">
         .headdiv {height: 40px; background-color: #3ABBFF;color:#fff;padding-left:10px; line-height:40px;border-bottom:#2FAEF3}
    </style>
    <script src="http://api.map.baidu.com/api?v=2.0&ak=A8nKkhhnf81lQGCFFH3k8l2A"></script>
    <script>
        $(function () {
            $("#top_nav_ul li[title='主页']").addClass("active");
        })
        var map = new BMap.Map("map_div");
        map.disableScrollWheelZoom();
        //获取传入的Point位,必须用其的再初始化,否则无效
        function GetByPoint(str) {
            point = JSON.parse(str);
            point = new BMap.Point(point.lng, point.lat);
            map.centerAndZoom(point, 18);
            var mk = new BMap.Marker(point);
            map.addOverlay(mk);
            map.panTo(point);
            //displayPOI(point);
        }
        $("#map_div").height(window.innerHeight);
        GetByPoint('<%=Point%>');
    </script>
</asp:Content>