function ShowMap(uid, adds, cmid) {
    $("#hmid").val(cmid);
    var doctxt;
    if (GBrowserIsCompatible()) {
        $.get('/manage/Template/GoogleMap.ashx?mt=' + new Date().getTime() + "&v2=select&mid=" + cmid, function (data) {
            var mcoctxt;
            var mlngx;
            var mlngy;
            var mlngz;
            var lng = data;
            var map = new GMap2(document.getElementById("map"));
            var blueIcon = new GIcon(G_DEFAULT_ICON);
            blueIcon.image = "/User/Develop/images/markerTransparent.png";
            markerOptions = { icon: blueIcon };
            if (adds.length == 0) {
                if (data.replace(",,0,", "").replace("0", "").length > 0) {
                    var m = lng.split(',')[2];
                    mcoctxt = lng.split(',')[3];
                    $("#hlngx").val(lng.split(',')[1]);
                    $("#hlngy").val(lng.split(',')[0]);
                    $("#hlngz").val(lng.split(',')[2]);
                    map.setCenter(new GLatLng(lng.split(',')[0], lng.split(',')[1]), (m - 0));
                    var marker = new GMarker(new GLatLng(lng.split(',')[0], lng.split(',')[1]), markerOptions)
                    marker.openInfoWindowHtml(lng.split(',')[3]);
                    doctxt = lng.split(',')[3]
                    map.addOverlay(marker);
                } else {
                    var geocoder = new GClientGeocoder();
                    geocoder.getLatLng(IPData[3], function (point) {
                        if (!point) {
                            alert("无法解析:" + address);
                        } else {
                            map.setCenter(point, 13);
                            var marker = new GMarker(point, markerOptions);
                            if ((uid - 0) > 0) {
                                marker.openInfoWindowHtml("<input type='text' id='addtxt' onchange='javascript:$(\"#haddtxt\").val($(this).val())' onfocus=\"if(value=='您的位置'||value=='您的位置') {value=''};\" value=\"您的位置\" />");
                            } else {
                                marker.openInfoWindowHtml("<div style='text-align:center; line-height:20px'>您当前IP为：" + IPData[0] + "，<br/>" + IPData[2] + "-" + IPData[3] + "[不存在或未标注状态]</div>");
                            }
                            map.addOverlay(marker);
                            $("#hlngx").val(point.x);
                            $("#hlngy").val(point.y);
                            $("#hlngz").val(map.getZoom());
                            $("#Bbtn").click(function () {
                                var t;
                                var f;
                                if (($("#hlngx").val() - 0) > 100) {
                                    t = $("#hlngx").val();
                                    f = $("#hlngy").val();
                                } else {
                                    t = $("#hlngx").val();
                                    f = $("#hlngy").val();
                                }
                                update(t, f, uid, $("#hlngz").val(), $("#addtxt").val());
                            });
                        }
                    });
                }
            } else {
                var geocoder = new GClientGeocoder();
                var ipare = adds;
                var pz = 7;
                if (adds.length > adds.replace("市", "").length) {
                    pz = 10;
                }
                geocoder.getLatLng(adds, function (point1) {
                    if (!point1) {
                        alert("无法解析:" + address);
                    } else {
                        map.setCenter(point1, (pz - 0));
                        var marker = new GMarker(point1, markerOptions);
                        if ((uid - 0) > 0) {
                            marker.openInfoWindowHtml("<input type='text' id='addtxt' onchange='javascript:$(\"#haddtxt\").val($(this).val())' onfocus=\"if(value=='您的位置'||value=='您的位置') {value=''};\" value=\"您的位置\" />");
                        }
                        else {
                            marker.openInfoWindowHtml("您的位置");
                        }
                        map.addOverlay(marker);
                        $("#hlngx").val(point1.x);
                        $("#hlngy").val(point1.y);
                        $("#hlngz").val(map.getZoom());

                    }
                });
            }
            $("#Bbtn").click(function () {
                var t;
                var f;
                if (($("#hlngx").val() - 0) > 100) {
                    t = $("#hlngx").val();
                    f = $("#hlngy").val();
                } else {
                    t = $("#hlngx").val();
                    f = $("#hlngy").val();
                }
                update(t, f, uid, $("#hlngz").val(), $("#addtxt").val(), $("#hmid").val());
            });

            map.removeMapType(G_HYBRID_MAP);
            var mapControl = new GMapTypeControl();
            map.addControl(mapControl);
            map.addControl(new GLargeMapControl());
            map.enableScrollWheelZoom();
            map.enableGoogleBar();
            $("#maxmap").click(function () {
                map.zoomIn();
            });
            $("#minmap").click(function () {
                map.zoomOut();
            });
            GEvent.addListener(map, 'click', function (overlay, point2) {
                if (point2) {
                    map.clearOverlays();
                    map.setCenter(point2, point2.z);
                    var marker = new GMarker(point2, markerOptions);
                    map.addOverlay(marker);
                    map.removeMapType(G_HYBRID_MAP);
                    if ((uid - 0) > 0) {
                        marker.openInfoWindowHtml("<input type='text' id='addtxt' onchange='javascript:$(\"#haddtxt\").val($(this).val())' onfocus=\"if(value=='您的位置'||value=='您的位置') {value=''};\" value=\"您的位置\" />");
                        $("#addtxt").val($("#haddtxt").val());
                    } else {
                        marker.openInfoWindowHtml("您的位置");
                    }
                    $("#hlngx").val(point2.x);
                    $("#hlngy").val(point2.y);
                    $("#hlngz").val(map.getZoom());
                    mcoctxt = $("#addtxt").val();
                    var mapControl = new GMapTypeControl();
                    map.addControl(mapControl);
                    map.addControl(new GLargeMapControl());
                    map.enableGoogleBar();
                    GEvent.addListener(marker, 'click', function ()
                    { marker.openInfoWindowHtml(doctxt); });
                }
            });
            GEvent.addListener(marker, 'click', function () {
                marker.openInfoWindowHtml(doctxt);
            });

        });
    }
}

function update(lx, ly, uid, lz, txt, dmid) {
    $.get('/manage/Template/GoogleMap.ashx?mt=' + new Date().getTime() + "&v2=updat&uid=" + uid + "&lx=" + lx + "&ly=" + ly + "&lz=" + lz + "&mid=" + $("#hmid").val() + "&type=" + escape(txt), function (data) {
        alert(data.split(',')[1]);
        opener.document.getElementById("hmap").value = data.split(',')[0];
        window.close();
    });
}