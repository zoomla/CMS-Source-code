<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateTable.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.CreateTable" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/SPwd.ascx" TagPrefix="uc1" TagName="SPwd" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>快速建表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <uc1:SPwd runat="server" ID="SPwd" Visible="false" />
    <div ng-app="app" id="maindiv" runat="server" visible="false">
        <div ng-controller="ZLCtrl">
            <div class="top_opbar">
                <div class="input-group text_500">
                    <span class="input-group-addon">
                    ZL_My_
                    </span>
                    <asp:TextBox ID="txtTabName" runat="server" CssClass="form-control text_300" placeholder="请输入表名,只允许英文和数字" MaxLength="20"/>
                    <span class="input-group-btn">
                        <input type="button" class="btn btn-info" value="添加字段" ng-click="addrow();" />
                        <input type="button" value="创建数据表" class="btn btn-primary" ng-click="preSave()" />
                    </span>
                </div>
                <asp:Button ID="CreateBtn" Style="display: none;" runat="server" OnClick="CreateBtn_Click" />
                <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="txtTabName" Display="Dynamic" ForeColor="Red" ErrorMessage="表名不能为空" />
                <asp:RegularExpressionValidator runat="server" ID="REV2" ControlToValidate="txtTabName" Display="Dynamic" ForeColor="Red" ErrorMessage="仅允许英文与数字" ValidationExpression="^[A-Za-z0-9]*$" />
            </div>
            <table class="table table-striped table-bordered">
                <tr>
                    <td>序号</td>
                    <td class="td_m">字段名</td>
                    <td>类型</td>
          <%--          <td>长度</td>--%>
                    <td>默认值</td>
                    <td>主键</td>
                    <td>可否为空</td>
                    <td class="td_l">操作</td>
                </tr>
                <tr ng-repeat="item in list">
                    <td class="td_s">{{$index+1}}</td>
                    <td>
                        <input type="text" class="form-control" ng-model="item.fieldName" placeholder="字段名" /></td>
                    <td class="td_l">
                        <select class="form-control" ng-option="item.fieldType" ng-model="item.fieldType">
                            <option value="int">int(整型)</option>
                            <option value="float">float(浮点型)</option>
                            <option value="decimal">decimal(精确数值)</option>
                            <option value="money">money(货币)</option>
                            <option value="nvarchar">nvarchar(Unincode字符串)</option>
                            <option value="ntext">ntext(Unincode字符串)</option>
                            <option value="datetime">datetime(日期时间)</option>
                            <option value="varbinary">varbinary(二进制)</option>
                            <option value="image">image(二进制,最大2G)</option>
                            <option value="Uniqueidentifier">Uniqueidentifier(全局标识)</option>
                            <option value="timestamp">timestamp(时间戳)</option>
                        </select></td>
<%--                    <td class="td_m">
                        <input type="text" class="form-control" ng-model="item.fieldLen" /></td>--%>
                    <td class="td_m">
                        <input type="text" class="form-control" ng-model="item.defval" /></td>
                    <td class="td_m">
                        <input type="radio" ng-click="setpk(item);" name="ispk_rad"/>
                    </td>
                    <td class="td_m">
                        <input type="checkbox" ng-checked="item.isnull" ng-model="item.isnull" /></td>
                    <td class="td_m">
                        <a href="javascript:;" ng-show="$index>0" ng-click="delrow($index)" class="btn btn-info"><i class="fa fa-minus"></i></a></td>
                </tr>
            </table>
        </div>
    </div>
    <div runat="server" id="remind_div" visible="false" class="alert alert-info"></div>
    <asp:HiddenField runat="server" ID="Data_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Plugs/angular.min.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script>
    var app = angular.module('app', []).controller('ZLCtrl', function ($scope) {
        $scope.addrow = function () {
            $scope.list.push($scope.getModel());
        }
        $scope.delrow = function (item) {
            $scope.list.splice(item, 1);
        }
        $scope.getModel = function () { return { fieldName: "", fieldType: "int", fieldLen: "4", defval: "", ispk: false, isnull: true }; }
        $scope.list = [];
        var data = $("#Data_Hid").val();
        if (!ZL_Regex.isEmpty(data)) { $scope.list = JSON.parse(data); }
        else { $scope.list.push($scope.getModel()); }
        $scope.preSave = function () {
            if (!confirm("确定要创建数据表吗")) { return false; }
            var result = true;
            var array = new Array();
            for (var i = 0; i < $scope.list.length; i++) {
                var data = $scope.list[i];
                if (ZL_Regex.isEmpty(data["fieldName"])) {
                    alert("字段名不能为空!"); return false;
                    return;
                }
                if (array.indexOf(data["fieldName"]) > -1) {
                    alert(data["fieldName"] + "字段名不能重复!");
                    return false;
                    return;
                } else { array.push(data["fieldName"]) }
            }
            $("#Data_Hid").val(angular.toJson($scope.list));
            $("#CreateBtn").click();
        }
        $scope.setpk = function (item) {
            angular.forEach($scope.list, function (item) {
                item.ispk = false;
            })
            item.ispk = true;
        }
    });
</script>
</asp:Content>
