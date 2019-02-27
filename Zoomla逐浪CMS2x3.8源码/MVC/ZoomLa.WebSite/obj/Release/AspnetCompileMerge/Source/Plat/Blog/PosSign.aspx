<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PosSign.aspx.cs" Inherits="ZoomLaCMS.Plat.Blog.PosSign" MasterPageFile="~/Common/Common.master" ValidateRequest="false"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>移动签到</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="background-color: #fff;min-height:600px;min-width:400px;">
        <div class="headdiv">
            <div class="col-xs-4 col-sm-4">
                <a href="/Plat/Default.aspx" style="color: #fff;"><i class="fa fa-chevron-left"></i>取消</a>
            </div>
            <div class="col-xs-8 col-sm-8"><span style="font-size: 1.5em;">移动签到</span></div>
        </div>
        <div id="map_div"></div>
        <div class="city_div">所在城市：<span id="address_t"></span></div>
        <ul class="add_ul">
            <li style="text-align: center;">正在加载中,请等待...</li>
        </ul>
    </div>
    <asp:HiddenField runat="server" ID="Pos_Hid" />
    <asp:HiddenField runat="server" ID="Point_Hid" />
    <asp:HiddenField runat="server" ID="Location_Hid" />
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="margin-top:50%;">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
              <div><i class="fa fa-map-marker"></i><span id="add_final_sp"></span></div>
          </div>
          <div class="modal-body">
            <asp:TextBox runat="server" TextMode="MultiLine" ID="Msg_T" CssClass="form-control" placeholder="说点什么吧..." />
          </div>
          <div class="modal-footer">
            <asp:Button runat="server" class="btn btn-primary" ID="Sure_Btn" style="width:100%;" OnClick="Sure_Btn_Click" OnClientClick="return CheckSubmit(this);" Text="提交签到" />
          </div>
        </div>
      </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css">
        ul {padding-left:0px;}
        #map_div {width:100%;height:230px;}
        #Msg_T {height:100px;resize:none;}
        .headdiv {height: 40px; background-color: #3ABBFF;color:#fff;
                  line-height:40px;border-bottom:#2FAEF3}
        .city_div {background-color: #3ABBFF;color:#fff;height:30px;line-height:30px;
                      padding-left:10px;}
        .add_ul li {padding:5px;border-bottom:1px solid #ddd;padding-top:10px;padding-bottom:10px;}
        .chkdiv {border-radius:50%;background:#3ABBFF;height:20px;width:20px;color:#fff;text-align:center;line-height:20px;margin-top:8px;display:none;}
        .active .chkdiv {display:block;}
        .add_rad {display:none;}
    </style>
  <script src="/JS/Controls/ZL_Array.js"></script>
  <script src="http://api.map.baidu.com/api?v=2.0&ak=A8nKkhhnf81lQGCFFH3k8l2A"></script>
  <script>
      $(function () {
          $("#top_nav_ul li[title='主页']").addClass("active");
      })

      var isready = false;
      var mOption = {
          poiRadius: 100,           //POI半径,默认100米
          numPois: 20                //列举出POI,默认10个
      }
      var myGeo = new BMap.Geocoder();        //创建地址解析实例
      function displayPOI(point) {
          var liTlp = '<li class="addli" data-toggle="modal" data-target="#myModal"><div class="col-xs-10 col-sm-10"><div><strong>@address @title</strong></div><div style="color: gray;">标签：@Gi</div></div>'
                + '<div class="col-xs-2 col-sm-2"><div class="chkdiv"><i class="fa fa-check"></i></div>'
                + '<input type="radio" value="@address @title" name="add_rad" class="add_rad" /></div>'
                + '<div class="clearfix"></div></li>';
          map.addOverlay(new BMap.Circle(point, mOption.poiRadius));        //添加一个圆形覆盖物
          myGeo.getLocation(point,
              function mCallback(rs) {
                  var allPois = rs.surroundingPois;       //获取全部POI（该点半径为100米内有6个POI点）
                  var html = JsonHelper.FillData(liTlp, allPois);
                  $(".add_ul").html("");
                  $(".add_ul").append(html);
                  $(".addli").click(function () {
                      $(".addli").removeClass("active");
                      var $li = $(this);
                      var rad = $li.find(".add_rad")[0]; rad.checked = true;
                      var add = $("#Pos_Hid").val() + " " + rad.value;
                      $li.addClass("active");
                      $("#add_final_sp").text(add);
                  });
                  $firstli = $($(".addli")[0]);
                  $firstli.addClass("active");
                  $firstli.find(".add_rad")[0].checked = true;
                  //for (i = 0; i < allPois.length; ++i) {
                  //    //document.getElementById("panel").innerHTML += "<p style='font-size:12px;'>" + (i + 1) + "、" + allPois[i].title + ",地址:" + allPois[i].address + "</p>";
                  //    //map.addOverlay(new BMap.Marker(allPois[i].point));
                  //}
              }, mOption
          );
      }
      //---------------------------------
      var map = new BMap.Map("map_div");
      map.disableScrollWheelZoom();
      function CheckSubmit(o) {
          if (!isready) { alert("正在获取地址信息,请稍等片刻"); return false; }
          setTimeout(function () { o.disabled = true; $(o).css("color", "#808080"); }, 100);
          return true;
      }
      function Signed() {
          if (parent) {
              parent.CloseDiag();
              parent.location = parent.location;
          }
          else { location = "/Plat/Blog/Default.aspx"; }
      }
      //function ShowResult(data) {
      //    console.log(data);
      //}
      //获取当前位置
      function GetCurrent() {
          var geolocation = new BMap.Geolocation();
          geolocation.getCurrentPosition(function (r) {
              if (this.getStatus() == BMAP_STATUS_SUCCESS) {
                  map.centerAndZoom(r.point, 18);
                  var mk = new BMap.Marker(r.point);
                  map.addOverlay(mk);
                  map.panTo(r.point);
                  r.address.lng = r.point.lng;
                  r.address.lat = r.point.lat;
                  $("#address_t").text(r.address.province + " " + r.address.city + " " + r.address.district + " " + r.address.street);
                  //省市,后台获取拼接
                  $("#Pos_Hid").val(r.address.province + " " + r.address.city);
                  $("#Point_Hid").val(JSON.stringify(r.point));
                  $("#Location_Hid").val(JSON.stringify(r.address));
                  //var local = new BMap.LocalSearch(map, {
                  //    renderOptions: { map: map },
                  //    onSearchComplete: ShowResult
                  //});
                  //local.searchInBounds("银行,大楼,酒店,住宅,社区,广场,医院,大厦".split(','),map.getBounds());
                  displayPOI(r.point);
                  isready = true;
              }
              else {
                  console.log("地址位置获取失败");
              }
          }, { enableHighAccuracy: true })
      }
      GetCurrent();
  </script>
</asp:Content>