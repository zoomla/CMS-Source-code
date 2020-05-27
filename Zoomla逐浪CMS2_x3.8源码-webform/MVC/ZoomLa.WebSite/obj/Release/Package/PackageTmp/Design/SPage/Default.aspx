<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Design.SPage.Default" MasterPageFile="~/Common/Master/Empty2.Master" EnableViewState="false" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>布局管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<nav class="navbar navbar-default">
      <div class="container">
          <div class="pull-right" style="line-height:50px;font-weight:bolder;font-size:16px;"><i class="fa fa-flag"></i> <%=pageMod.PageName %></div>
        <div class="navbar-header">
          <a class="navbar-brand" href="javascript:;" onclick="scope.showDiag('<%=CustomerPageAction.customPath2+"Template/SPage/AddPage.aspx" %>','添加新页');"><i class="fa fa-file"></i> 添加新页</a>
        </div>
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
          <ul class="nav navbar-nav">
            <li><a href="javascript:;" onclick="scope.save('PageDesign.aspx?ID=<%:pageMod.ID %>');"><i class="fa fa-pencil"></i> 页面设计</a></li>
            <li><a href="javascript:;" onclick="scope.save('<%:string.IsNullOrEmpty(pageMod.ViewUrl)?"PreView.aspx?ID="+pageMod.ID:pageMod.ViewUrl %>');"><i class="fa fa-globe"></i> 预览页面</a></li>
            <li><a href="javascript:;" onclick="scope.save();"><i class="fa fa-save"></i> 保存布局</a></li>
            <%--         <li class="dropdown">
                          <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Dropdown <span class="caret"></span></a>
                          <ul class="dropdown-menu" role="menu">
                            <li><a href="#">Action</a></li>
                            <li class="divider"></li>
                            <li><a href="#">Separated link</a></li>
                          </ul>
                        </li>--%>
          </ul>

        </div>
      </div>
</nav>
<div id="editor" ng-app="APP" ng-controller="ZLCtrl" class="container">
  <div class="text_600 text-center r_gray" style="margin:0 auto;color:#999;" ng-if="Layout.list.length<1">
        <i class="fa fa-bank margin_t10" style="font-size:120px;"></i>
        <div class="margin_t10" style="font-size:18px;">尚未定义布局,请添加</div>
    </div>
   <div class="layout_list">
       <div ng-repeat="layout in Layout.list|orderBy:'order' track by $index">
            <div class="layout row clearfix" ng-switch="layout.type">
                <div class="btnwrap">
                    <button type="button" class="btn btn-xs btn-info" title="删除" ng-click="Layout.suredel(layout);"><i class="fa fa-trash-o"></i></button>
                    <button type="button" class="btn btn-xs btn-info" title="上移" ng-disabled="!Layout.canmove(layout,'pre')" ng-click="Layout.move(layout,'pre')"><i class="fa fa-chevron-up"></i></button>
                    <button type="button" class="btn btn-xs btn-info" title="下移" ng-disabled="!Layout.canmove(layout,'next')" ng-click="Layout.move(layout,'next')"><i class="fa fa-chevron-down"></i></button>
                    <span ng-if="layout.type=='fill'"><i class="fa fa-desktop"></i> 全屏</span>
                </div>
                <div ng-switch-when="fill" class="col-md-12 column">
                    <div class="comp" ng-repeat="comp in layout.middle|orderBy:'order' track by $index">
                        <input type="text" ng-model="comp.name" class="form-control text_md" />
                        <div class="pull-right" ng-init="list=layout.middle">
                            <compbtns></compbtns>
                        </div>
                    </div>
                    <div ng-if="layout.middle.length<1" class="comp empty" ng-click="showAddComp(layout.middle);">
                        <span>点击添加模块</span>
                    </div>
                </div>
                <div ng-switch-when="12" class="col-md-12 column">
                    <div class="comp" ng-repeat="comp in layout.middle|orderBy:'order'  track by $index">
                       <input type="text" ng-model="comp.name" class="form-control text_md" />
                        <div class="pull-right" ng-init="list=layout.middle">
                            <compbtns></compbtns>
                        </div>
                    </div>
                    <div ng-if="layout.middle.length<1" class="comp empty" ng-click="showAddComp(layout.middle);">
                        <span>点击添加模块</span>
                    </div>
                </div>
                <div ng-switch-when="48">
                    <div class="col-md-4 column">
                        <div class="comp" ng-repeat="comp in layout.left|orderBy:'order'  track by $index">
                           <input type="text" ng-model="comp.name" class="form-control text_md" />
                            <div class="pull-right" ng-init="list=layout.left">
                                <compbtns></compbtns>
                            </div>
                        </div>
                        <div ng-if="layout.left.length<1" class="comp empty" ng-click="showAddComp(layout.left);">
                            <span>点击添加模块</span>
                        </div>
                    </div>
                    <div class="col-md-8 column">
                        <div class="comp" ng-repeat="comp in layout.right|orderBy:'order'  track by $index">
                            <input type="text" ng-model="comp.name" class="form-control text_md" />
                            <div class="pull-right" ng-init="list=layout.right">
                                <compbtns></compbtns>
                            </div>
                        </div>
                        <div ng-if="layout.right.length<1" class="comp empty" ng-click="showAddComp(layout.right);">
                            <span>点击添加模块</span>
                        </div>
                    </div>
                </div>
                <div ng-switch-when="84">
                    <div class="col-md-8 column">
                        <div class="comp" ng-repeat="comp in layout.left|orderBy:'order'  track by $index">
                            <input type="text" ng-model="comp.name" class="form-control text_md" />
                            <div class="pull-right" ng-init="list=layout.left">
                                <compbtns></compbtns>
                            </div>
                        </div>
                        <div ng-if="layout.left.length<1" class="comp empty" ng-click="showAddComp(layout.left);">
                            <span>点击添加模块</span>
                        </div>
                    </div>
                    <div class="col-md-4 column">
                            <div class="comp" ng-repeat="comp in layout.right|orderBy:'order'  track by $index">
                          <input type="text" ng-model="comp.name" class="form-control text_md" />
                                <div class="pull-right" ng-init="list=layout.right">
                                    <compbtns></compbtns>
                                </div>
                        </div>
                        <div ng-if="layout.right.length<1" class="comp empty" ng-click="showAddComp(layout.right);">
                            <span>点击添加模块</span>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
       </div>
    </div>
   <div class="text-center">
        <button type="button" class="addLayout btn btn-lg btn-info" ng-click="showAddLayout();"><i class="fa fa-plus"></i> 添加新布局</button>
   </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
#editor {font-size:12px;font-family:'Microsoft YaHei';}
#editor .middle {width:700px;margin:0 auto;}
#editor .layout_list {min-height:500px;margin-bottom:10px;margin-top:10px;}
#editor .layout {border:1px solid #ddd;background:none;padding:10px;margin-top:35px;position:relative;}
#editor .layout .btnwrap {position: absolute; left: -1px; top: -30px; height: 30px; padding: 3px; border: 1px solid #ddd; border-bottom: none;border-top-left-radius:3px;border-top-right-radius:3px; }
#editor .layout .column{line-height:35px;}
#editor .layout .compbtns .btn{margin-right:5px;}
#editor .layout .comp {background:#ffebcc;padding:7px 10px;border:1px solid #ffd599;overflow:visible;margin-bottom:10px;border-radius:3px;}
#editor .layout .comp.empty {background-color:#ececec !important;border:1px solid #d4d4d4 !important;cursor:pointer;}
.text_md{max-width:150px;display:inline-block;}
</style>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Modal/APIResult.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script src="/JS/Plugs/base64.js"></script>
<script>
    var diags = [];
    var waitDiag=new ZL_Dialog();
    var scope = null;
    angular.module("APP", []).controller("ZLCtrl", function ($scope) {
        //sort:值越小越前,rowList:该layout下分为几列,根据选项自动生成
        //var layout = { id: "", type: "", sort: 0, colList: [] }
        //var column = { id: "", sort: 0, compList: [] };
        //var comp = { id: "", type: "", data: { value: null }, config: {} };
        //layout--column(可多列)--comp
        //类名:首字母大写,方法与变量首字母小写
        scope = $scope;
        $scope.Layout = {
        list:<%=pageMod.Layouts%>,
        cur:null,
        newMod: function (type) {
            var ref = this;
            var max = ref.list.GetByMax("order");
            return { id: GetRanID(), "type": type, order: max ? max.order + 1 : 1, left: [], middle: [], right: [] };
        },
        add: function (type) {
            //left-middle,left-left
            var ref = this;
            var model = ref.newMod(type);
            switch (type) {
                case "fill":
                    break;
                case "12":
                    break;
                case "48":
                    break;
                case "84":
                    break;
            }
            ref.list.push(model);
        },
        del: function (layout) {
            var ref = this;
            for (var i = 0; i < ref.list.length; i++) {
                if (ref.list[i] == layout) { ref.list.splice(i,1); return; }
            }
        },
        suredel: function (layout)
        {
            var ref = this;
            if (confirm("确定要移除吗")) { ref.del(layout); }
        },
        move: function (item, dir) {
            var ref = this;
            var tar = ArrCOM.GetNear(ref.list, { "dir": dir, order: item.order, field: "order" });
            var temp = tar.order;
            tar.order = item.order;
            item.order = temp;
        },
        canmove: function (item, dir) {
            var ref = this;
            return ArrCOM.GetNear(ref.list, { "dir": dir, order: item.order, field: "order" }) ? true : false;
        }
    };
        $scope.Comp = {
            list: null,
            newMod: function (type) {
                var ref = this;
                var max = ref.list.GetByMax("order");
                var model=zlhelp.deepCopy(ref.datas[type]);
                model.order=max ? max.order + 1 : 1;
                model.id=GetRanID();
                model.type=type;
                return model;
            },
            copy:function(list,comp){
                //if(!confirm("要复制该元素吗")){return false;}
                var ref=this;
                var model=zlhelp.deepCopy(comp);
                var max = list.GetByMax("order");
                model.order=max ? max.order + 1 : 1;
                model.id=GetRanID();
                list.push(model);
            },
            add: function (type) {
                var ref = this;
                var comp = ref.newMod(type);
                ref.list.push(comp);
            },
            del: function (list, comp) {
                if(!confirm("确定要删除吗")){return;}
                for (var i = 0; i < list.length; i++) {
                    if (list[i].id == comp.id) { list.splice(i, 1); return;}
                }
            },
            getName: function (type) {
                var ref=this;
                if(!ref.datas[type]){return "未知类型[" + type + "]";}
                else{return ref.datas[type].name;}
            },
            move:function(item,dir,list){
                var tar = ArrCOM.GetNear(list, { "dir": dir, order: item.order, field: "order" });
                var temp = tar.order;
                tar.order = item.order;
                item.order = temp;
            },
            canmove:function(item,dir,list){
                return ArrCOM.GetNear(list, { "dir": dir, order: item.order, field: "order" }) ? true : false;
            },
            datas:[]
        };
        //初始数据,data为数据,config为属性,data.value为值 {name:"",data:{},config:{}}
        $scope.Comp.datas["carousel"]={name:"轮播图",data:{title:"",value:[
          {src:"/design/spage/image/1.png",url:"#",order:1},
          {src:"/design/spage/image/2.png",url:"#",order:2},
          {src:"/design/spage/image/3.png",url:"#",order:3}
        ]},config:{style:"",width:"" }};
        $scope.Comp.datas["content"]={name:"自定义内容",data:{value:"PGRpdj7or7fngrnlh7vovpPlhaXlhoXlrrk8L2Rpdj4="},config:{style:"",width:"container" }};
        $scope.Comp.datas["nav"]={name:"导航菜单",data:{value:[
        {url:"#",text:"菜单1"},
        {url:"#",text:"菜单2"},
        {url:"#",text:"菜单3"},
        {url:"#",text:"菜单4"},
        {url:"#",text:"菜单5"},
        ]},config:{style:"" }};
        $scope.Comp.datas["label"]={name:"标签",data:{value:""},config:{style:"",width:""}};
        //--------------------------------
        $scope.save=function(url){
            var json=angular.toJson($scope.Layout.list);
            waitDiag.ShowMask("正在保存中");
            $.post("",{"layouts":json,"action":"save"},function(data){
                var model=APIResult.getModel(data);
                if(!APIResult.isok(model)){alert("save failed:"+model.retmsg);}
                waitDiag.CloseModal();
                if(url){location=url;}
            })
        }
        $scope.showAddLayout = function () {
            $scope.showDiag("AddLayout.html", "添加布局");
        }
        $scope.showAddComp = function (col) {
            $scope.showDiag("AddComp.aspx", "添加组件");
            $scope.Comp.list = col;
        }
        //--------------dialog
        $scope.showDiag = function (url, title, reload) {
            url = url.toLowerCase();
            var diag = diags[url];
            if (!diag) {
                diag = new ZL_Dialog();
                diag.url = url;
                diag.title = title;
                diag.backdrop = true;
                diag.maxbtn = false;
                diags[url] = diag;//非数组,为json类型
            }
            if (reload === true) { diag.reload = true; }
            diag.ShowModal();
        }
        //$scope.Layout.add("12");
        //$scope.Layout.add("48");
        //$scope.Layout.add("84");
    }).directive("compbtns",function(){
        var html="<button type=\"button\" class=\"btn btn-xs btn-info\" ng-click=\"showAddComp(list)\" ng-if='$index==0' title='添加'><i class=\"fa fa-plus\"></i></button>"
    +"<button type=\"button\" class=\"btn btn-xs btn-info\" ng-click=\"Comp.copy(list,comp);\" title='复制'><i class=\"fa fa-copy\"></i></button>"
    +"<button type=\"button\" class=\"btn btn-xs btn-info\" ng-click=\"Comp.del(list,comp);\" title='删除'><i class=\"fa fa-trash-o\"></i></button>"
    +"<button type=\"button\" class=\"btn btn-xs btn-info\" ng-disabled=\"!Comp.canmove(comp,'pre',list)\"  title='上移' ng-click=\"Comp.move(comp,'pre',layout.middle);\"><i class=\"fa fa-chevron-up\"></i></button>"
    +"<button type=\"button\" class=\"btn btn-xs btn-info\" ng-disabled=\"!Comp.canmove(comp,'next',list)\" title='下移' ng-click=\"Comp.move(comp,'next',layout.middle);\"><i class=\"fa fa-chevron-down\"></i></button>";
        return {
            restrict: 'E',
            replace: true,
            template:"<div class=\"compbtns\">"+html+"</div>",

        }


    })
function closeDiag() {
    for (var key in diags) {
        if (diags[key] && diags[key].CloseModal) { diags[key].CloseModal(); }
    }
    waitDiag.CloseModal();
    scope.$digest();
}
</script>
</asp:Content>