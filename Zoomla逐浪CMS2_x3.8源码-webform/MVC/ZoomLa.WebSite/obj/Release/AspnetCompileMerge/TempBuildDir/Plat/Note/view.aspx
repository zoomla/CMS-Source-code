<%@ Page Language="C#" MasterPageFile="~/Common/Master/Empty.master" AutoEventWireup="true" CodeBehind="view.aspx.cs" Inherits="ZoomLaCMS.Plat.Note.view" ValidateRequest="false" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
<title>
<asp:Literal ID="Title_L" runat="server" /></title>
<style>
body { position: relative; }
.note { position: static !important;}
.user_mimenu{ position:relative; z-index:100010;}
.user_mimenu .navbar-fixed-top{ text-align:left;}
.user_mimenu #mimenu_btn{ position:relative; margin-top:10px; margin-left:10px; padding-bottom:10px; background:none; z-index:10003;} 
.user_mimenu #mimenu_btn span{ display:block; width:20px; height:6px; border-bottom:1px solid #eee;} 
.user_home_mimenu #mimenu_btn{ background:rgba(8,201,12,1); }
.user_home_mimenu .navbar{ width:150px;}
.user_mimenu_left{ position:fixed; left:0; top:0; bottom:0; padding-top:50px; width:0; background:rgba(0,0,0,0.7); box-shadow:1px 0 3px rgba(0,0,0,0.9); overflow:hidden; z-index:10002;}  
.user_mimenu_left ul{ display:none; padding-top:20px; border-top:1px solid #666;} 
.user_mimenu_left li a{ display:block; padding-left:30px; height:3em; line-height:3em; font-family:"微软雅黑"; font-size:1.2em; z-index:999; color:#fff;} 
.user_mimenu_left li a:hover,.user_mimenu_left li a:focus{ background:rgba(255,255,255,0.3); outline:none; text-decoration:none;}
.user_mimenu_right{ position:fixed; top:10px; right:20px;}
.user_mimenu_right li a{ color:#666; font-family:"微软雅黑"; font-size:1.2em;}
</style>
<script src="/Plugins/Ueditor/ueditor.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<script src="/JS/scrolltopcontrol.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="user_mimenu">
<div class="navbar navbar-fixed-top" style="margin-right:50px;" role="navigation">
<button type="button" class="btn btn-default" id="mimenu_btn">
<span class="icon-bar"></span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
</button>
<div class="user_mimenu_left">
<ul class="list-unstyled">
<li><a href="/">首页</a></li>
<li><a href="/Home">能力</a></li>
<li><a href="/Index">社区</a></li>
<li><a href="/Ask">问答</a></li>
<li><a href="/Guest">留言</a></li>
<li><a href="/Baike">百科</a></li>
<li><a href="/Office">OA</a></li>
</ul>
</div>
<div class="navbar-header">
<button class="navbar-toggle in" type="button" data-toggle="collapse" data-target=".navbar-collapse">
<span class="sr-only">移动下拉</span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
</button>
</div>
</div>
</div>

    <div ng-app="app">
        <div class="note_menu">
            <div class="pull-left"><a href="#"><span id="note_logo"><span class="logo1">凵</span><span class="logo2">刂</span></span>基于Zoomla!逐浪CMS创建</a></div>
            <button class="navbar-toggle collapsed" type="button">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
        </div>
        <div ng-controller="NoteCtrl">
            <div class="note-topimg" id="note-topimg" ng-style="{'background':'url({{comMod.topimg==''?'res/page_bg.jpg':comMod.topimg}}) center no-repeat','background-size':'cover'}">
                <%--   <img ng-src="{{comMod.topimg}}" class="note-topimg_img" />--%>
                <%--      <div style="position:absolute;bottom:-20px;width:300px;left:10%;">
<img src="" style="width:140px;height:180px;" onerror="this.src='/images/nopic.gif'"/>
</div>--%>
                <div class="set_title text-center" id="test">{{comMod.title}}</div>
            </div>
            <div class="col-lg-2 col-md-2"></div>
            <div class="note col-lg-8 col-md-8 col-sm-10 col-xs-12">
                <div class="note-content" id="note-content">
                    <div class="con-item" id="{{item.id}}" ng-repeat="item in comMod.comlist|orderBy: 'orderID'" ng-switch="item.type">
                        <div ng-bind-html="item.text|html"></div>
                        <div ng-switch-when="image" class="com com-image">
                            <div class="com-img_item">
                                <img ng-src="{{item.content}}" class="com-img_img"></div>
                        </div>
                        <div ng-switch-when="video" ng-switch="item.videoType" class="con-item com com-video">
                            <div ng-switch-when="video" ng-bind-html="note.Video.getvideo(item)|html"></div>
                            <div ng-switch-when="online" ng-bind-html="note.Video.getonline(item)|html"></div>
                        </div>
                        <div ng-switch-when="para" class="con-item com com-para">
                            <h2 class="{{'para '+item.content}}">
                                <span ng-bind="item.title" class="pull-left para_title"></span>
                            </h2>
                        </div>
                    </div>
                </div>
                <div class="note_music container" style="display: none;">
                    <asp:HiddenField runat="server" ID="Save_Hid" />
                    <asp:HiddenField runat="server" ID="Partic_Hid" />
                    <audio id="mp3_audio" ng-src="{{comMod.mp3}}" autoplay></audio>
                </div>
                <div class="note_msglist">
                    <div class="msg_item" ng-repeat="item in msg.list|orderBy:'-ID'">
                        <div class="avatar"><a href="javascript:;">
                            <img src="{{item.UserFace}}" onerror="shownoface(this);" /></a></div>
                        <div class="content">
                            <div class="content_head">
                                <a href="#" ng-bind="item.UserName"></a><span><i class="fa fa-home margin-l" style="font-size: 1.3em;"></i>{{item.GroupName}}</span>
                                <span class="pull-right">
                                    <a href="javascript:;" ng-click="msg.reply(item);" class="margin-r">回复</a>
                                    <a href="javascript:;" ng-click="msg.quote(item);" class="margin-r">引用</a>
                                </span>
                                <div style="color: #b0b0b0;">
                                    {{item.CDate|date:'yyyy-MM-dd HH:mm' }} 
                                    <span ng-show="item.ReplyMsgID!=0">回复 {{item.RCUName}}：</span>
                                </div>
                            </div>
                            <div class="content_body" ng-bind-html="item.Content|html"></div>
                        </div>
                    </div>
                    <!--msglist end;-->
                    <div>
                        <tm-pagination conf="pageConf"></tm-pagination>
                        <div class="clearfix"></div>
                    </div>
                    <div class="note_rpy margin-top">
                        <textarea id="content_t" style="height: 240px;"></textarea>
                        <%=Call.GetUEditor("content_t",4) %>
                        <div class="margin-top">
                            <input type="button" class="btn btn-info" value="发表回复" ng-click="msg.add();" /></div>
                    </div>
                </div>
            </div>
            <div class="note_steps" id="note_steps">
                <div class="note_steps_c">
                    <div class="steps_title">
                        <h2>步骤列表</h2>
                        <div class="margin-top"><span><a class="note_steps_a" id="note_steps_a" href="javascript:;"><i class="fa fa-tasks"></i>步骤大纲</a></span></div>
                    </div>
                    <div class="steps_list">
                        <ul class="list-unstyled steps_ul">
                            <li data-id="{{item.id}}" ng-repeat="item in comMod.comlist |filter:{type:'para'} |orderBy: 'orderID'">
                                <div class="step_head">{{item.title}}</div>
                                <div class="step_body">{{item.text}}</div>
                            </li>
                        </ul>
                    </div>
                    <div class="steps_title">
                        <h2>参与人</h2>
                    </div>
                    <div class="steps_list">
                        <ul class="list-unstyled">
                            <li class="partic_li" ng-repeat="item in partic |orderBy: 'UserID'"><div class="step_head">{{item.UserName}}</div></li>
                        </ul>
                    </div>
                    <div class="text-center">
                        <span id="Edit_Span" visible="false" runat="server"><a href="Note.aspx?id=<%=Mid %>"><i class="fa fa-pencil"></i>返回编辑</a></span>
                    </div>
                </div>
            </div>
            <div class="note_sbody"></div>
            <div class="note_steps_info" id="note_steps_info">
                <div class="step_info_close"><i class="fa fa-close"></i></div>
                <ul class="list-unstyled step_info_list">
                    <li ng-repeat="item in comMod.comlist|filter:{type:'para'} |orderBy: 'orderID'">
                        <div class="step_list_title">{{item.title}}</div>
                        <div class="step_list_content">{{item.text}}</div>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<link href="/Plugins/Ueditor/third-party/video-js/video-js.min.css" rel="stylesheet" />
<link href="note.css" rel="stylesheet" />
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script src="/JS/Modal/APIResult.js"></script>
<script src="/Plugins/Ueditor/third-party/video-js/video.js"></script>
<script src="tm.pagination.js"></script>
<script src="/JS/Controls/Control.js"></script>
<script>
    $(function () {
        InitScoll();
        //右侧窗口弹出
        $("#note_steps_a").click(function () {
            $("#note_steps_info").css({ "left": "0" });
        });
        $(".step_info_close").click(function () {
            $("#note_steps_info").css({ "left": "100%" });
        });
    })
    //右侧栏滚动监听事件
    function InitScoll() {
        var stepscoll = $("#note_steps")[0].offsetTop;
        var step_items = [];//滚动内容项
        $(window.document).scroll(function () {
            $("#note_steps .steps_ul li").each(function () {
                console.log($(this));
                var itemid = $(this).data('id');
                step_items.push({ id: itemid, top: $("#" + itemid)[0].offsetTop });
            });
            //滚动固定右侧栏
            if ($(this).scrollTop() >= stepscoll) {
                $("#note_steps").css({ "position": "fixed", "top": "0" });
            } else {
                $("#note_steps").css({ "position": "absolute", "top": "500px" });
            }
            //滚动内容通知
            var curitem = step_items[0].id;
            for (var i = 0; i < step_items.length; i++) {
                if ($(this).scrollTop() >= step_items[i].top) { curitem = step_items[i].id; };
            }
            $("#note_steps .steps_ul li").removeClass('active');
            if (curitem != "") { $("#note_steps .steps_ul [data-id='" + curitem + "']").addClass('active'); }
        });
    }
    var scope = null, note = {
        Video: {
            getvideo: function (model) {
                if (!model.content || model.content == "") { return ""; }
                return '<video width="600" height="400" src="' + model.content + '" poster="/Template/V3/style/Images/l_logo.jpg" class="edui-upload-video vjs-default-skin video-js" preload="none" controls=""><source src="' + model.content + '" type="video/mp4"></video>';
            },
            getonline: function (model) {
                //直接写html会出错
                return '<embed type="application/x-shockwave-flash" src="' + model.content + '" quality="high" align="middle" allowscriptaccess="false" allowfullscreen="true" wmode="transparent" width="600" height="400">';
            }
        }
    };
    var ue = UE.getEditor("content_t");
    angular.module("app", ['tm.pagination'])
    .controller("NoteCtrl", function ($scope, $compile, $location, $anchorScroll) {
        scope = $scope;
        var conf = { api: "common.aspx", id: "<%:Mid%>", rid: 0 }
		scope.note = note;
		$scope.comMod = { topimg: "", title: "", mp3: "", comlist: [] };
		$scope.comMod = JSON.parse($("#Save_Hid").val());
		$scope.partic = JSON.parse($("#Partic_Hid").val());
		console.log($scope.comMod);
		var emotion = {
			SmilmgName: { tab0: ['j_00', 84], tab1: ['t_00', 40], tab2: ['w_00', 52], tab3: ['B_00', 63], tab4: ['C_00', 20], tab5: ['i_f', 50], tab6: ['y_00', 40] }, //图片前缀名
			imageFolders: { tab0: 'jx2/', tab1: 'tsj/', tab2: 'ldw/', tab3: 'bobo/', tab4: 'babycat/', tab5: 'face/', tab6: 'youa/' }, //图片对应文件夹路径
			SmileyInfor: {
				tab0: ['Kiss', 'Love', 'Yeah', '啊！', '背扭', '顶', '抖胸', '88', '汗', '瞌睡', '鲁拉', '拍砖', '揉脸', '生日快乐', '大笑', '瀑布汗~', '惊讶', '臭美', '傻笑', '抛媚眼', '发怒', '打酱油', '俯卧撑', '气愤', '?', '吻', '怒', '胜利', 'HI', 'KISS', '不说', '不要', '扯花', '大心', '顶', '大惊', '飞吻', '鬼脸', '害羞', '口水', '狂哭', '来', '发财了', '吃西瓜', '套牢', '害羞', '庆祝', '我来了', '敲打', '晕了', '胜利', '臭美', '被打了', '贪吃', '迎接', '酷', '微笑', '亲吻', '调皮', '惊恐', '耍酷', '发火', '害羞', '汗水', '大哭', '', '加油', '困', '你NB', '晕倒', '开心', '偷笑', '大哭', '滴汗', '叹气', '超赞', '??', '飞吻', '天使', '撒花', '生气', '被砸', '吓傻', '随意吐'],
				tab1: ['Kiss', 'Love', 'Yeah', '啊！', '背扭', '顶', '抖胸', '88', '汗', '瞌睡', '鲁拉', '拍砖', '揉脸', '生日快乐', '摊手', '睡觉', '瘫坐', '无聊', '星星闪', '旋转', '也不行', '郁闷', '正Music', '抓墙', '撞墙至死', '歪头', '戳眼', '飘过', '互相拍砖', '砍死你', '扔桌子', '少林寺', '什么？', '转头', '我爱牛奶', '我踢', '摇晃', '晕厥', '在笼子里', '震荡'],
				tab2: ['大笑', '瀑布汗~', '惊讶', '臭美', '傻笑', '抛媚眼', '发怒', '我错了', 'money', '气愤', '挑逗', '吻', '怒', '胜利', '委屈', '受伤', '说啥呢？', '闭嘴', '不', '逗你玩儿', '飞吻', '眩晕', '魔法', '我来了', '睡了', '我打', '闭嘴', '打', '打晕了', '刷牙', '爆揍', '炸弹', '倒立', '刮胡子', '邪恶的笑', '不要不要', '爱恋中', '放大仔细看', '偷窥', '超高兴', '晕', '松口气', '我跑', '享受', '修养', '哭', '汗', '啊~', '热烈欢迎', '打酱油', '俯卧撑', '?'],
				tab3: ['HI', 'KISS', '不说', '不要', '扯花', '大心', '顶', '大惊', '飞吻', '鬼脸', '害羞', '口水', '狂哭', '来', '泪眼', '流泪', '生气', '吐舌', '喜欢', '旋转', '再见', '抓狂', '汗', '鄙视', '拜', '吐血', '嘘', '打人', '蹦跳', '变脸', '扯肉', '吃To', '吃花', '吹泡泡糖', '大变身', '飞天舞', '回眸', '可怜', '猛抽', '泡泡', '苹果', '亲', '', '骚舞', '烧香', '睡', '套娃娃', '捅捅', '舞倒', '西红柿', '爱慕', '摇', '摇摆', '杂耍', '招财', '被殴', '被球闷', '大惊', '理想', '欧打', '呕吐', '碎', '吐痰'],
				tab4: ['发财了', '吃西瓜', '套牢', '害羞', '庆祝', '我来了', '敲打', '晕了', '胜利', '臭美', '被打了', '贪吃', '迎接', '酷', '顶', '幸运', '爱心', '躲', '送花', '选择'],
				tab5: ['微笑', '亲吻', '调皮', '惊讶', '耍酷', '发火', '害羞', '汗水', '大哭', '得意', '鄙视', '困', '夸奖', '晕倒', '疑问', '媒婆', '狂吐', '青蛙', '发愁', '亲吻', '', '爱心', '心碎', '玫瑰', '礼物', '哭', '奸笑', '可爱', '得意', '呲牙', '暴汗', '楚楚可怜', '困', '哭', '生气', '惊讶', '口水', '彩虹', '夜空', '太阳', '钱钱', '灯泡', '咖啡', '蛋糕', '音乐', '爱', '胜利', '赞', '鄙视', 'OK'],
				tab6: ['男兜', '女兜', '开心', '乖乖', '偷笑', '大笑', '抽泣', '大哭', '无奈', '滴汗', '叹气', '狂晕', '委屈', '超赞', '??', '疑问', '飞吻', '天使', '撒花', '生气', '被砸', '口水', '泪奔', '吓傻', '吐舌头', '点头', '随意吐', '旋转', '困困', '鄙视', '狂顶', '篮球', '再见', '欢迎光临', '恭喜发财', '稍等', '我在线', '恕不议价', '库房有货', '货在路上']
			}
		};
		var strToEmotion = function (str) {
			if (!str || str == "" || str.indexOf("]") < 0) { return str; }
			var baseurl = "/Plugins/Ueditor/dialogs/emotion/images/";//jx2/j_0001.gif";
			for (var i = 0; i < 7; i++) {
				if (str.indexOf("]") < 0) { return str; }
				var tab = emotion.SmileyInfor["tab" + i];
				var url = emotion.imageFolders["tab" + i] + emotion.SmilmgName["tab" + i][0];
				for (var j = 0; j < tab.length; j++) {
					var index = j + 1;
					var imgurl = url + (index < 10 ? ("0" + index) : index);
					if (str.indexOf("[" + tab[j] + "]") < 0) continue;
					else {
						str = str.replace(new RegExp("\\[" + tab[j] + "\\]", "g"), '<img src="' + baseurl + imgurl + '.gif" />');
					}
				}
			}
	
			return str;
		}
		for (var i = 0; i < $scope.comMod.comlist.length; i++) {
			var com = $scope.comMod.comlist[i];
			com.text = com.text.replace(/\n/g, "<br/>").replace(/ /g, "&nbsp;");
			com.text = strToEmotion(com.text);
		}
		//----------------
		$scope.pageConf = {
			currentPage: 1,
			totalItems: 0,
			itemsPerPage: 10,
			pagesLength: 10,
			perPageOptions: [10, 20, 30, 40, 50],
			rememberPerPage: 'perPageItems',
			onChange: function () {
				$scope.msg.getlist();
			}
		};
		$scope.msg = { list: [] };
		$scope.msg.reply = function (item) {
			conf.rid = item.ID;
			ue.focus();
			Control.Scroll.ToBottom();
		}
		$scope.msg.quote = function (item) {
			conf.rid = item.ID;
			ue.focus();
			Control.Scroll.ToBottom();
		}
		$scope.msg.add = function () {
			var content = ue.getContent();
			var model = { id: conf.id, rid: conf.rid, "content": content };
			ue.setContent(""); conf.rid = 0;//回复只对一次有效
	
			$.post(conf.api + "?action=msg_add", model, function (data) {
				//返回新添加的数据
				var retmod = APIResult.getModel(data);
				$scope.msg.list.push(retmod.result[0]);
				$scope.$digest();
			})
		}
		$scope.msg.getlist = function () {
			$.post(conf.api + "?action=msg_list&page=" + $scope.pageConf.currentPage, {
				id: conf.id,
				psize: $scope.pageConf.itemsPerPage
			}, function (data) {
				var model = APIResult.getModel(data);
				$scope.msg.list = model.result;
				$scope.pageConf.totalItems = model.addon;
				$scope.$digest();
			})
		}
		$scope.msg.getlist();
		//----------------
		$scope.goto = function (id) {
			$location.hash(id);
			$anchorScroll();
		}
	
	})
	.filter("html", ["$sce", function ($sce) {
		return function (text) { return $sce.trustAsHtml(text); }
	}])
	.directive("errSrc", function () {
		return {
			link: function (scope, element, attrs) {
				element.bind("error", function () {
					if (attrs.src != attrs.errSrc) {
						attrs.$set("src", attrs.errSrc);
					}
				});
			}//link end;
		}
	});
	$(".note_menu button").click(function () {
		$(".note_steps").fadeToggle();
		$(".note_sbody").fadeToggle();
	})
	$(".note_sbody").click(function () {
		$(".note_steps").fadeToggle();
		$(".note_sbody").fadeToggle();
	})
	$("#mimenu_btn").click(function (e) {
		if ($(".user_mimenu_left").width() > 0) {
			$(".user_mimenu_left ul").fadeOut(100);
			$(".user_mimenu_left").animate({ width: 0 }, 200);
		}
		else {
			$(".user_mimenu_left").animate({ width: 150 }, 300);
			$(".user_mimenu_left ul").fadeIn();
		}
	});
</script>
</asp:Content>