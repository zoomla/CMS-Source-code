<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mobileview.aspx.cs" MasterPageFile="~/Common/Master/Empty.master" Inherits="User_Content_Note_mobileview" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>游记预览</title>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/> 
<link href="mobile_note.css" rel="stylesheet" type="text/css" />
<style>
body { background:#f0f0f0; }
.mnote_head{padding:5px;}
.mnote_head .mnote_head_back{padding-top:10px;float:none;padding-right:70px;}
.mnote_head .mnote_head_back a{color:#999}
.com_img_item img{max-width:100%;}
.com_image_img { position:absolute; bottom:0; width:100%; border-top:1px solid #f0f0f0;}
.com_image_text .form-control { border-radius:0; border:none;}
.mnote_top_b{line-height:20px;padding:10px;}
/*段落*/
.paralist_item .para_title{position:absolute;border:none;width:150px;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div ng-app="app">
        <div ng-controller="NoteCtrl">
            <div class="mnote_head">
                <div class="mnote_head_back">
                    <div class="pull-left">
                    <a href="mobilelist.aspx"><i class="fa fa-chevron-left"></i></a>
                    </div>
                    <div class="pull-right">
                    <a href="mobilenote.aspx?id=<%=Mid %>"><i class="fa fa-edit"></i></a>
                    </div>
                </div>
                    <div class="mimenue_div">
                        <button class="mimenue_btn btn btn-default" type="button">
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                         <div class="mnote_menue_right">
                             <div class="menue_title">步骤列表</div>
                             <ul class="list-unstyled">
                                 <li data-id="{{item.id}}" ng-repeat="item in comMod.comlist |filter:{type:'para'} |orderBy: 'orderID'">
                                    <div class="step_head">{{item.title}}</div>
                                    <div class="step_body">{{item.text}}</div>
                                 </li>
                             </ul>
                        </div>
                    </div>
            </div>
            <div class="mnote_top">
                <div class="mnote_top_c">
                    <div class="mnote_top_t" ng-style="{'background':'url({{comMod.topimg}}) center no-repeat','background-size':'cover'}"><div  ng-if="comMod.topimg==''" class="mnote_top_ti"><i class="fa fa-image"></i><br />背景图片</div></div>
                    <div class="mnote_top_b" ng-bind="comMod.title"></div>
                </div>
            </div>
            <div class="mnote_content">
                <div class="con_item" id="{{item.id}}" ng-repeat="item in comMod.comlist|orderBy: 'orderID'" ng-switch="item.type">
                     <div ng-switch-when="text" class="com com_text">
                        <div ng-bind-html="item.content|html"></div>
                     </div>
                    <div ng-switch-when="image" class="com com-image">
                    <div class="com_img_item"><img ng-src="{{item.content}}" class="com-img_img"></div>
                    </div>
                    <div ng-switch-when="video" ng-switch="item.videoType" class="con-item com com-video">
                    <div ng-switch-when="video" ng-bind-html="note.Video.getvideo(item)|html"></div>
                    <div ng-switch-when="online" ng-bind-html="note.Video.getonline(item)|html"></div>
                    </div>
                    <div ng-switch-when="para" class="con-item com com-para">
                        <div id="{{item.id}}_div" class="paralist_item {{item.content}}">
                            <img src="/user/content/note/res/{{item.content}}.gif" />
                            <div class="para_title" ng-bind="item.title"></div>
                        </div>
                    </div>
                </div>
            </div>
           
        </div>
    </div>
    <asp:HiddenField ID="Save_Hid" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
<script src="/JS/Plugs/angular.min.js"></script>
<script src="note.js"></script>
<script>
    var scope = null;
    angular.module("app", []).controller("NoteCtrl", function ($scope) {
        scope = $scope;
        $scope.note = note;
        $scope.readysave = false;
        $scope.comMod = { id: 0, topimg: "", pic: "", title: "", mp3: "", comlist: [] };
        if ($("#Save_Hid").val() != "") {
            $scope.comMod = JSON.parse($("#Save_Hid").val());
        }
        console.log($scope.comMod);
        //note.Text.add();
        //note.Img.add("");
        //note.preMobilecom("text");
    }).filter("html", ["$sce", function ($sce) {
        return function (text) { return $sce.trustAsHtml(text); }
    }])
    $(function () {
        //右侧栏选中效果
        $('.mnote_menue_right li')
        .on('touchstart', function () { $(this).addClass("li_active"); })
        .on('touchend', function () { $(this).removeClass("li_active"); });
        $(".mimenue_btn").click(function () {
            if ($(".mnote_menue_right").css("right") != "0px") {
                $(".mnote_menue_right").css({ "right": "0" });
            } else {
                $(".mnote_menue_right").css({ "right": "-180px" });
            }
        });

    })
</script>
</asp:Content>


