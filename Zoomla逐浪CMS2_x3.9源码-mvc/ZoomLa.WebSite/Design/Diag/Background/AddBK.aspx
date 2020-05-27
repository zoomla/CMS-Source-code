<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBK.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.Background.AddBK"  MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>设置背景</title>
    <style type="text/css">
        .bg-panel-top {}
        .bg-panel-body {padding-left:10px;}
        .preset-list-section-list{list-style:none;margin:0;padding:0;}
        .preset-list-section-list .preset-list-item {display:inline-block;width:49%;cursor:pointer;margin-bottom:18px;}
        .preset-list-item .thumbnail{padding:2px;}
        .section-wrapper {display:none;}
        .h5Img li{width:20%;float:left;}
        .h5Img li img{margin:5px 0px;border: 1px solid #ddd; padding:2px;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="panel panel-default">
       <div class="panel-body" style="padding:0px;"><div class="bg-panel-top">
       <div class="text-center" style="padding-bottom:20px;">
          <div class="ui-buttonset" style="padding-top:10px;">
            <input type="radio" id="radio1" name="bk_rad" value="color" class="ui-helper-hidden-accessible"><label for="radio1" class="ui-button ui-widget ui-state-default ui-button-text-only ui-corner-left" role="button" aria-disabled="false" aria-pressed="false"><span class="ui-button-text">小图背景</span></label>
            <span id="imageRad" runat="server"><input type="radio" id="radio2" name="bk_rad" value="image" checked="checked" class="ui-helper-hidden-accessible"><label for="radio2" class="ui-button ui-widget ui-state-default ui-button-text-only" role="button" aria-disabled="false" aria-pressed="false"><span class="ui-button-text">背景图片</span></label></span>
            <span id="h5Rad" runat="server" visible="false"><input type="radio" id="radio6" name="bk_rad" value="h5" checked="checked" class="ui-helper-hidden-accessible"><label for="radio6" class="ui-button ui-widget ui-state-default ui-button-text-only" role="button" aria-disabled="false" aria-pressed="false"><span class="ui-button-text">H5场景</span></label></span>
            <input type="radio" id="radio3" name="bk_rad" value="video" class="ui-helper-hidden-accessible"><label for="radio3" class="ui-button ui-widget ui-state-default ui-button-text-only ui-corner-left" role="button" aria-disabled="false" aria-pressed="false"><span class="ui-button-text">背景视频</span></label>
            <input type="radio" id="radio4" name="bk_rad" onclick="clearBK();" class="ui-helper-hidden-accessible"><label for="radio4" class="ui-button ui-widget ui-state-default ui-button-text-only ui-corner-left" role="button" aria-disabled="false" aria-pressed="false"><span class="ui-button-text">清空背景</span></label>
            <input type="radio" id="radio5" name="bk_rad" value="upload" class="ui-helper-hidden-accessible"><label for="radio5" class="ui-button ui-widget ui-state-default ui-button-text-only ui-corner-left" role="button" aria-disabled="false" aria-pressed="false"><span class="ui-button-text">上传背景</span></label>
        </div>
      </div>
  </div>
  <div class="bg-panel-body">
      <div class="sections tab-content">
          <div id="colorTab" class="section-wrapper live-comps tab-pane">
              <ul class="preset-list-section-list">
                  <asp:Repeater runat="server" ID="RPTMinImg" EnableViewState="false">
                      <ItemTemplate>
                          <li class="preset-list-item" data-type="minimg" data-url="<%#Eval("Path") %>">
                              <div class="thumbnail">
                                  <img src="<%#Eval("Path") %>"/></div>
                          </li>
                      </ItemTemplate>
                  </asp:Repeater>
              </ul>
             <div class="clearfix"></div>
          </div>
          <div id="imageTab" runat="server" class="section-wrapper live-comps tab-pane active">
              <div class="section-header">
                  <ul class="preset-list-section-list">
                        <asp:Repeater runat="server" ID="RPTImg" EnableViewState="false">
                            <ItemTemplate>
                                <li class="preset-list-item" data-type="image" data-url="<%#Eval("vpath") %>"><div class="thumbnail"><img src="<%#Eval("previewimg") %>" style="width:420px;height:280px;" /></div></li>
                            </ItemTemplate>
                        </asp:Repeater>
                 </ul>
              <div class="clearfix"></div>
              </div>
          </div>
          <div id="videoTab" class="section-wrapper live-comps tab-pane">
               <ul class="preset-list-section-list">
                    <li class="preset-list-item" data-type="video" data-post="/Design/res/bkvideo/dreamscapes.jpg" data-url="/Design/res/bkvideo/dreamscapes.mp4"><div class="thumbnail"><img src="/Design/res/bkvideo/dreamscapes.jpg" /></div></li>
                   <li class="preset-list-item" data-type="video"  data-post="/Design/res/bkvideo/file.jpg" data-url="/Design/res/bkvideo/file.mp4"><div class="thumbnail"><img src="/Design/res/bkvideo/file.jpg" /></div></li>
               </ul>
          </div>
          <div id="uploadTab" class="section-wrapper live-comps tab-pane">
               <div class="section-header">
                  <div class="title-line">
                      <div class="title">上传或指定背景</div>
                  </div>
              </div>
               <div class="input-group" style="width:380px;">
                   <input type="text" class="form-control text_300" id="url_t" placeholder="请输入图片路径" />
                   <span class="input-group-btn"><input type="button" value="确定路径" class="btn btn-info" onclick="setBK();"/></span>
               </div>
               <div class="input-group" style="margin-top:10px;margin-bottom:10px;width:380px;">
                   <input type="text" class="form-control text_300" placeholder="请选择文件" />
                   <span class="input-group-btn"><input type="button" value="选择文件" class="btn btn-info" onclick="pic.sel();"/></span>
               </div>
          </div>
          <div id="h5Tab" runat="server" visible="false" class="section-wrapper live-comps tab-pane active">
              <ul class="h5Img">
                  <asp:Repeater runat="server" ID="h5RPT" EnableViewState="false">
                      <ItemTemplate>
                          <li class="preset-list-item" data-type="image" data-url="<%#Eval("vpath") %>" title="设定背景">
                              <img src="<%#Eval("previewimg") %>" style="width: 95%; height: 300px;" /></li>
                      </ItemTemplate>
                  </asp:Repeater>
              </ul>
          </div>
          <div></div>
      </div>
  </div>
        </div>
    </div>
<input type="file" id="pic_up" style="display:none;" accept="image/*" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/Design/JS/Plugs/jqueryUI/jquery-ui-1.9.2.custom.min.js"></script>
<link href="/Design/JS/Plugs/jqueryUI/css/custom-theme/jquery-ui-1.10.0.custom.css" rel="stylesheet" />
<link href="/Design/res/css/edit/common.css" rel="stylesheet" />
<script src="/JS/Controls/ZL_Webup.js"></script>
<script>
    var data = { type: "image", url: "", post: "", pageid: "<%:Request["pageid"]%>" };
    $(function () {
        $(".ui-buttonset").buttonset();
        $("input[name=bk_rad]").click(function () {
            $(".tab-pane").removeClass("active");
            $("#" + this.value + "Tab").addClass("active");
        });
        $(".preset-list-item").click(function () {
            var $li = $(this);
            data.type = $li.data("type");
            data.url = $li.data("url");
            data.post = $li.data("post");
            top.page.bk.set(data);
            top.CloseDiag();
        });
    })
    function clearBK() {
        data.type = "clear";
        top.page.bk.clear(data);
        top.CloseDiag();
    }
    function setBK() {
        data.url = $("#url_t").val();
        top.page.bk.set(data);
        top.CloseDiag();
    }
    var pic = { id: "pic_up", txtid: null };
    pic.sel = function (id) { $("#" + pic.id).val(""); $("#" + pic.id).click(); }
    pic.upload = function () {
        var fname = $("#" + pic.id).val();
        if (!SFileUP.isWebImg(fname)) { alert("请选择图片文件"); return false; }
        SFileUP.AjaxUpFile(pic.id, function (url) {
            data.type = "image";
            data.url = url;
            top.page.bk.set(data);
            top.CloseDiag();
        });
    }
    $("#" + pic.id).change(function (e) {
        if (e.target.files.length < 1) { return; }
        pic.upload();
    });
</script>
</asp:Content>
