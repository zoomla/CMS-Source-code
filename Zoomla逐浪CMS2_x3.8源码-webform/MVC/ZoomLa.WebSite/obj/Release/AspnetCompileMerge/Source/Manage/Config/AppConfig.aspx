<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppConfig.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.AppConfig" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <script type="text/javascript" src="/JS/jquery.validate.min.js"></script>
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
    <title>维护中心</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField ID="HiddenText" runat="server" />
    <ul class="nav nav-tabs" role="tablist">
        <li role="presentation" class="active"><a href="#NormalConfig" aria-controls="NormalConfig" role="tab" data-toggle="tab">通用配置</a></li>
        <li role="presentation"><a href="#ErroCfg" aria-controls="ErroCfg" role="tab" data-toggle="tab">错误页配置</a></li>
      </ul>
    <table class="table table-striped table-bordered table-hover tab-content"">
        <tbody role="tabpanel" id="NormalConfig" class="tab-pane active">
        <tr>
            <td class="tdleft"><strong><%:lang.LF("是否重新安装") %>：</strong></td>
            <td>
                <input type="checkbox" runat="server" id="Installed_Chk" class="switchChk" /><span style="color:green;"> 点击启动重系统请慎重!</span>
            </td>
        </tr>
        <tr>
            <td class="text-right"><strong>管理员申请模式：</strong></td>
            <td>
                <input type="checkbox" runat="server" id="IsManageReg_Chk" class="switchChk" />
            </td>
        </tr>
         <%--<tr>
            <td class="text-right"><strong>启用默认文档：</strong></td>
             <td>
                 <input type="checkbox" runat="server" id="IsIndex_Chk" class="switchChk" />
             </td>
        </tr>--%>
        <tr>
            <td class="text-right"><strong>默认文档配置：</strong></td>
            <td ng-app="app">
                <ul ng-controller="appController" class="default_div">
                    <li>
                        <div class="input-group margin_b2px text_300">
                            <span class="input-group-addon">+</span>
                            <input type="text" id="indexname_T" value="home.html" class="form-control" />
                            <span class="input-group-btn">
                                <button type="button" class="btn btn-default" ng-click="add(list.length)">添加新项</button>
                            </span>
                        </div>
                    </li>
                    <li ng-repeat="item in list">
                        <div class="input-group margin_b2px text_300">
                            <span class="input-group-addon">{{$index+1}}</span>
                            <input type="text" class="form-control" disabled="disabled" value="{{item.name}}" />
                            <span class="input-group-btn">
                                <button type="button" class="btn btn-default" ng-click="remove($index)"><span class="fa fa-trash-o"></span></button>
                                <button type="button" class="btn btn-default" ng-click="moveup($index)"><span class="fa fa-arrow-up"></span></button>
                            </span>
                        </div>
                    </li>
                </ul>
                <asp:HiddenField ID="Default_Hid" runat="server" />
            </td>
        </tr>
        <tr>
            <td><strong><%:lang.LF("防盗链后缀名") %>：</strong></td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" class="form-control text_300" /></td>
        </tr>
        </tbody>
        <tbody role="tabpanel" id="ErroCfg" class="tab-pane">
        <tr>
            <td class="text-right"><strong>启用错误页：</strong></td>
            <td>
                <input type="checkbox" runat="server" id="CustomError_Chk" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td><strong><%:lang.LF("错误页配置") %>：</strong>
                <ul>
                    <li><span>401-访问被拒绝</span></li>
                    <li><span>403-禁止访问</span></li>
                    <li><span>403-文件不存在</span></li>
                    <li><span>405-访问方式不被允许</span></li>
                    <li><span>406-浏览器不接受所请求页面的MIME类型</span></li>
                    <li><span>412-前提条件失败</span></li>
                    <li><span>500-服务器错误</span></li>
                    <li><span>501-指定了未实现的配置</span></li>
                    <li><span>502-服务器收到了无效响应</span></li>
                </ul>
            </td>
            <td>
                <ul>
                    <asp:Repeater runat="server" ID="ErrRPT">
                        <ItemTemplate>
                            <li><div class="input-group margin_b2px text_300">
                                <span class="input-group-addon"><%#Eval("ErrCode") %></span>
                                <asp:HiddenField runat="server" ID="ErrCode_Hid" Value='<%#Eval("ErrCode") %>' />
                                <asp:TextBox runat="server" ID="ErrUrl_T" class="form-control required serverurl" Text='<%#Eval("Url") %>' /></div></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </td>
        </tr>
        </tbody>
        
  </table>
    </div>
  <asp:Button ID="Button1" runat="server" Text="保存设置" OnClientClick="return UrlChk();" OnClick="Button1_Click" class="btn btn-primary" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style>
        .default_div .input-group-addon{width:40px;}
    </style>
    <script src="/JS/Plugs/angular.min.js"></script>
    <script src="/JS/Controls/ZL_Array.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script type="text/javascript">
        var curScope = null;
        var app = angular.module("app", []).controller("appController", function ($scope, $compile) {
            curScope = $scope;
            $scope.list = JSON.parse($("#Default_Hid").val());
            $scope.remove = function (index) {
                $scope.list.splice(index, 1);
            }
            $scope.add = function (index) {
                if ($("#indexname_T").val() == "") { alert("默认文档不能为空!"); return;}
                $scope.list.unshiftNoDup({ name: $("#indexname_T").val() }, "name");
                $("#indexname_T").val("");
            }
            $scope.moveup = function (index) {//上移
                var predata = $scope.list[index - 1];
                if (predata) {
                    var curdata = {name: $scope.list[index].name };
                    $scope.list[index] = { name: predata.name };
                    $scope.list[index - 1] = curdata;
                }
            }
        })
        $(function () {
            jQuery.validator.addMethod("serverurl", function (value) {
                return ZL_Regex.isVirtualPath(value)
            }, "请输入正确的URL");
            curScope.$apply(function ($compile) {//加载默认数据
                if (curScope.list.length <= 0) {
                    curScope.list = [{ "name": "Default.htm" }, { "name": "Default.asp" }, { "name": "index.htm" }, { "name": "index.html" }, { "name": "iisstart.htm" }, { "name": "default.aspx" }];
                }
            });
            console.log($("#Default_Hid").val());
        });
        function UrlChk() {
            var vaild = $("form").validate({ meta: "validate" });
            $("#Default_Hid").val(angular.toJson(curScope.list));
            return vaild.form();
        }
    </script>
</asp:Content>