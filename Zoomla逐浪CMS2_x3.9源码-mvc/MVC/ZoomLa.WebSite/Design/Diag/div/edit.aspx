<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.div.edit"  MasterPageFile="~/Design/Master/Edit.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>DIV布局设置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="panel panel-primary" ng-app="app">
        <div class="panel-heading"><span class="marginl5">DIV布局</span></div>
        <div class="panel-body pad0" ng-controller="appCtrl">
            <div class="div_footer">
                <div class="control-section-divider labeled">配置</div>
                <hr class="divider-long" />
                <div class="setting-row">
                    <div><label class="row-title">版权信息</label></div>
                    <textarea id="copy_t" class="form-control" style="height:80px;resize:none;"></textarea>
                </div>
                <div class="setting-row">
                    <div><label class="row-title">链接</label></div>
                    <table class="table table-bordered table-striped">
                        <tr><td>名称</td><td>链接</td><td>操作</td></tr>
                        <tr ng-repeat="item in model.dataMod.links">
                            <td>{{item.name}}</td>
                            <td><input type="text" value="{{item.href}}" /></td>
                            <td><a href="javascript:;" ng-click="dellink(item);"><i class="fa fa-remove"></i></a></td>
                        </tr>
                    </table>
                    <input type="button"  value="新增一列" ng-click="addlink(item);" />
                </div>
                <div class="setting-row">
                    <div><label class="row-title">官方博客</label></div>
                      <table class="table table-bordered table-striped">
                        <tr><td>平台</td><td>是否显示</td><td>链接</td></tr>
                        <tr ng-repeat="item in model.dataMod.blogs"><td>{{item.name}}</td><td>{{item.show}}</td><td><input type="text" value="{{item.href}}" /></td></tr>
                    </table>
                </div>
            </div>
            <hr class="divider-long" />
            <div class="control-section-divider labeled">样式设置</div>
            <hr class="divider-long" />
            <div class="setting-row">
                <div>
                    <label class="row-title">背景颜色</label>
                </div>
                <input type="text" id="bgcolor_t" class="form-control text_150">
            </div>
            <hr class="divider-long" />
            <div class="setting-row">
                <div>
                    <label class="row-title">圆角边框(px)</label>
                </div>
                <div id="radius_slider" class="slider_min"></div>
                <input type="text" id="radius_t" class="inputer min" />
            </div>
            <hr class="divider-long" />
            <div class="setting-row">
                <div>
                    <label class="row-title">边距(px)</label>
                </div>
                内边距：<input type="text" id="padding_t" style="width:150px;" class="form-control" />
                外边距：<input type="text" id="margin_t" style="width:150px;" class="form-control" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script src="/JS/Plugs/angular.min.js"></script>
    <script>
        $(function () {
            //背景色
            $("#bgcolor_t").val(editor.dom.css("background-color"));
            $("#bgcolor_t").ColorPickerSliders({
                size: 'sm', placement: 'right', swatches: false, sliders: false, hsvpanel: true, previewformat: "hex",
                onchange: function (container, color) {
                    editor.dom.css("background-color", color.tiny.toHexString());
                }
            });
            //圆角边框
            $("#radius_t").val(editor.dom.css("border-radius"));
            $("#radius_slider").slider({
                range: "min", min: 0, max: 100, value: parseInt(editor.dom.css("border-radius")),
                slide: function (event, ui) {
                    $("#radius_t").val(ui.value + "px");
                    editor.dom.css("border-radius", ui.value + "px");
                }
            });
            //边距
            $("#padding_t").val(editor.dom.css("padding")).blur(function () { if ($(this).val() != "") { editor.dom.css("padding", $(this).val()); } });
            $("#margin_t").val(editor.dom.css("margin")).blur(function () { if ($(this).val() != "") { editor.dom.css("margin", $(this).val()); } });
        });
        angular.module("app", []).controller("appCtrl", function ($scope) {
            console.log(editor.model.config.compid);
            if (editor.model.config.compid.indexOf("footer_") > -1) {
                $("#div_footer").show();
                $scope.model = editor.model;

                $scope.dellink = function (item) {
                    $scope.model.dataMod.links.forEach(function (v, i, _) {
                        if (v == item) {
                            _.splice(i, 1);
                            parent.editor.scope.$digest();
                            return;
                        }
                    });
                }//remove end;
                $scope.addlink = function () {
                    editor.model.dataMod.links.push({ orderid: 0, name: "名称", href: "#" });
                }
            }
        })
    </script>
</asp:Content>
