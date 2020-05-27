<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelGroup.aspx.cs" Inherits="ZoomLaCMS.Common.Dialog.SelGroup" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>选择部门</title>
<style>
    * {font-size:12px;font-family:'Microsoft YaHei';}
    .search_div{position:relative;}
    .selgroup_body .nav-tabs{height:40px;border-bottom:3px solid #ddd;}
    .selgroup_body .nav-tabs>li.active>a{border:none;border-bottom:3px solid #1e88e5;color:#1e88e5}
    .selgroup_body .nav-tabs>li>a{color:#999;}
    .selgroup_body .nav-tabs>li>a:hover{background:none;}
    .selgroup_body .nav-tabs>li>a:focus{background:none;}
    .selgroup_body .filter_list li{padding:0;}
    .selgroup_body .filter_list li a{color:#1e88e5;}
    .selgroup_body .userlist_div{height:280px;margin-left:-5px;border-right:2px solid #ddd;overflow:auto;}
    .userlist_div .userlist_item{height:30px;line-height:30px;padding-left:5px;cursor:pointer;}
    .userlist_div .userlist_item:hover{background-color:rgba(0, 180, 248,.3)}
    .userlist_div .userlist_item:hover .item_add{display:block;}
    .adduser_div{position:relative;}
    .adduser_div .center_div{position:absolute;left:-20px;top:150px;width:20px;color:#999;text-align:center;font-size:1.8em;}
    .empty_div{height:30px;line-height:30px;display:none;}
    .userlist_item .item_face{width:40px;}
    .userlist_item .item_face img{width:24px;height:24px;border-radius:50px;}
    .userlist_item .item_add{color:#1e88e5;width:40px;display:none;}
    .selgroup_foot{margin-top:10px;line-height:30px;height:30px;}
    .selgroup_foot a{color:#555;}
    .selgroup_foot .fa-user-plus{color:#1e88e5;}
    .group_item .item_face{width:auto;margin-right:5px;}
    .group_item .item_face img{width:auto;height:auto;}
    .group_item .item_add{width:80px;}
    #wait_div{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
      <div class="container-fluid search_div">
            <div class="input-group pull-left">
                <input type="text" id="search_t" class="form-control" placeholder="请输入用户名称,按回车搜索" />
                <span class="input-group-btn">
                    <a id="search_b" class="btn btn-default" href="javascript:;"><i class="fa fa-search"></i></a>
                </span>
            </div>
        </div>
        <div class="container-fluid selgroup_body">
            <div class="row">
                <div class="col-md-7 col-lg-7 col-xs-7 col-sm-7">
                    <ul class="nav nav-tabs">
                      <li role="presentation" id="user_tab" class="tabs active"><a href="javascript:;" onclick="B_Users.SearchByList({});">所有人</a></li>
                      <li role="presentation" id="group_tab" visible="false" runat="server" class="tabs"><a href="javascript:;" onclick="B_Groups.LoadGroups();">按会员组筛选</a></li>
                      <li role="presentation" id="platgroup_tab" visible="false" runat="server" class="tabs"><a href="javascript:;" onclick="B_Groups.LoadGroups();">按部门筛选</a></li>
                      <li role="presentation" id="structgroup_tab" visible="false" runat="server" class="tabs"><a href="javascript:;" onclick="B_Groups.LoadGroups();">组织结构筛选</a></li>
                    </ul>
                    <div class="selgroup_content padding5">
                        <ul class="margin_t5 filter_list list-unstyled list-inline">
                            <li><a href="javascript:;">全部</a></li>
                            <li><a href="javascript:;">A</a></li>
                            <li><a href="javascript:;">B</a></li>
                            <li><a href="javascript:;">C</a></li>
                            <li><a href="javascript:;">D</a></li>
                            <li><a href="javascript:;">E</a></li>
                            <li><a href="javascript:;">F</a></li>
                            <li><a href="javascript:;">G</a></li>
                            <li><a href="javascript:;">H</a></li>
                            <li><a href="javascript:;">I</a></li>
                            <li><a href="javascript:;">J</a></li>
                            <li><a href="javascript:;">K</a></li>
                            <li><a href="javascript:;">L</a></li>
                            <li><a href="javascript:;">M</a></li>
                            <li><a href="javascript:;">N</a></li>
                            <li><a href="javascript:;">O</a></li>
                            <li><a href="javascript:;">P</a></li>
                            <li><a href="javascript:;">Q</a></li>
                            <li><a href="javascript:;">R</a></li>
                            <li><a href="javascript:;">S</a></li>
                            <li><a href="javascript:;">T</a></li>
                            <li><a href="javascript:;">U</a></li>
                            <li><a href="javascript:;">V</a></li>
                            <li><a href="javascript:;">W</a></li>
                            <li><a href="javascript:;">X</a></li>
                            <li><a href="javascript:;">Y</a></li>
                            <li><a href="javascript:;">Z</a></li>
                        </ul>
                        <div class="userlist_div margin_t10">
                            <div class="text-right" id="backgroup_div" style="padding:5px;display:none;">
                                <a href="javascript:;" onclick="B_Groups.Show()"><i class="fa fa-users"></i> 返回用户组</a>
                            </div>
                            <div id="userbody_div"></div>
                            <div id="groupbody_div" style="display:none;"></div>
                            <div class="margin_t10">
                                <div class="empty_div text-center"></div>
                                <div id="load_div" class="text-center"><a href="javascript:;" onclick="B_Users.LoadMore();">加载更多</a></div>
                                <div id="wait_div" class="text-center"><i class="fa fa-spinner fa-spin" style="font-size: 40px;"></i></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-5 col-lg-5 col-xs-5 col-sm-5 adduser_div">
                    <ul class="nav nav-tabs">
                      <li role="presentation"><a href="javascript:;">已选择用户</a></li>
                      <li class="pull-right" role="presentation"><a style="color:#1e88e5" href="javascript:;" onclick="B_Users.RemoveAll();">全部清空</a></li>
                    </ul>
                    <div id="seluser_div" class="userlist_div margin_t10">
                    </div>
                    <div class="center_div">
                        <span class="fa fa-angle-right"></span>
                    </div>
                </div>
            </div>
            <div class="row selgroup_foot">
                <div class="pull-right"><button type="button" onclick="SelUser()" class="btn btn-sm btn-primary">确定</button></div>
            </div>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script src="/JS/Controls/ZL_Array.js"></script>
    <script src="/JS/ICMS/ZL_Common.js"></script>
    <script>
        var B_Users = {
            conf: { page: 1, psize: 8, skey: "", groupid: 0, char: "" },
            sellist:[],//已选择用户数组
            ResetConf:function(){//重置conf
                var ref = this;
                ref.conf.page = 1;
                ref.conf.psize = 8;
                ref.conf.skey = "";
                ref.conf.groupid = 0;
                ref.conf.char = "";
                //还原选项卡
                $(".tabs").removeClass('active');
                $("#user_tab").addClass('active');
                $("#backgroup_div").hide();//隐藏返回用户组操作
            },
            ShowWait:function(){//等待效果
                var ref = this;
                $(".empty_div").hide();
                $("#load_div").hide();
                $("#wait_div").show();
            },
            CloseWait: function () {
                var ref = this;
                $("#wait_div").hide();
                $("#load_div").show();
            },
            LoadSelList:function(){//加载已选择用户
                var ref = this;
                $("#seluser_div").html('');
                var html = "<div class=\"userlist_item\" data-uid='@UserID'>"
                        + "<div class=\"item_face pull-left\"><img src=\"@salt\" onerror=\"this.src='/images/userface/noface.png';\"/></div>"
                        + "<div class=\"pull-left item_name\">@UserName</div>"
                        + "<div class=\"pull-right item_add\">移除</div>"
                        + "</div>";
                $("#seluser_div").append(JsonHelper.FillData(html, ref.sellist));
                //绑定事件
                $("#seluser_div .userlist_item").click(function () {//移除事件
                    ref.RemoveUser($(this));
                });
            },
            SelUser:function($obj){//选择用户操作
                var ref = this;
                var obj = ref.sellist.GetByID($obj.data('uid')+"", "UserID");
                if (obj) {return;}
                ref.sellist.push({ UserID: $obj.data('uid'), salt: $obj.find('.item_face img').attr('src'), UserName: $obj.find('.item_name').text() });
                ref.LoadSelList();
            },
            RemoveUser:function($obj){//移除选择用户操作
                var ref = this;
                ref.sellist.RemoveByID($obj.data('uid')+"", "UserID");
                ref.LoadSelList();
            },
            RemoveAll: function () {//移除全部已选择用户
                var ref = this;
                ref.sellist = [];
                ref.LoadSelList();
            },
            SearchByList: function (option) {//搜索操作
                var ref = this;
                $("#userbody_div").html('');
                ref.ResetConf();
                if (option.groupid) {//用户组查询
                    //改变选项卡
                    B_Groups.activeTabs();
                    $("#backgroup_div").show();//返回用户组按钮
                    ref.conf.groupid = option.groupid;
                }
                if (option.skey) {//按用户名搜索
                    ref.conf.skey = option.skey;
                }
                if (option.char) {
                    ref.conf.char = option.char;
                }
                ref.LoadMore();
            },
            //加载数据
            LoadMore: function () {
                var ref = this;
                $("#groupbody_div").hide();//隐藏用户组列表
                $("#userbody_div").show();//显示用户列表
                var $body = $("#userbody_div");
                $div = $("<div>");
                ref.ShowWait();
                var url = "UserBody.aspx?";
                url += "source=<%=Source %>&config=<%:Config%>&skey=" + encodeURI(ref.conf.skey) + "&psize=" + ref.conf.psize + "&PIndex=" + ref.conf.page + "&groupid=" + ref.conf.groupid + "&char=" + ref.conf.char;
                $div.load(url, {}, function () {
                    ref.CloseWait();
                    if ($div.find(".userlist_item").length < ref.conf.psize) {//没有更多的数据
                        $(".empty_div").html("<span style='color:#999;'>没有更多用户了!</span>");
                        $(".empty_div").show();
                        $("#load_div").hide();
                    }
                    //事件绑定
                    $div.find('.userlist_item').click(function () {
                        ref.SelUser($(this));
                    });
                    $body.append($div.children());
                    if ($body.find(".userlist_item").length <= 0) {//没有数据
                        $(".empty_div").html("<span style='color:#999;'>暂无数据!</span>");
                        $(".empty_div").show();
                        $("#load_div").hide();
                    }
                    ref.conf.page++;//翻页
                });
            }
        }
        var B_Groups = {
            Source:"<%=Source %>",
            Show: function () {//显示会员组列表
                var $body = $("#groupbody_div");
                $("#userbody_div").hide();//隐藏用户列表
                $body.show();//显示用户组列表
                $("#backgroup_div").hide();//隐藏返回用户组链接
                $("#load_div").hide();//隐藏加载更多
                $(".empty_div").hide();
            },
            activeTabs:function(){//选中tab操作
                var ref=this;
                //激活选项卡
                $(".tabs").removeClass('active');
                if (ref.Source == "user") { $("#group_tab").addClass('active'); }
                if (ref.Source == "plat") { $("#platgroup_tab").addClass('active'); }
                if (ref.Source == "oa")   { $("#structgroup_tab").addClass('active'); }
            },
            LoadGroups: function () {
                var ref = this;
                var $body = $("#groupbody_div");
                var $div = $("<div>");
                var url = "GroupBody.aspx?source="+ref.Source;
                ref.Show();
                ref.activeTabs();
                $body.html("");
                B_Users.ShowWait();
                $div.load(url, {}, function () {
                    B_Users.CloseWait();
                    $("#load_div").hide();
                    //事件处理
                    $div.find(".group_item").dblclick(function () {
                        $("[data-pid='" + $(this).data('gid') + "']").toggle();
                    });
                    $div.find(".item_add").click(function () {
                        B_Users.SearchByList({ groupid: $(this).closest('.group_item').data('gid') });
                    });
                    $body.append($div.children());
                    if ($body.find(".group_item").length <= 0) {//没有数据
                        $(".empty_div").html("<span style='color:#999;'>暂无数据!</span>");
                        $(".empty_div").show();
                    }
                })
            }
        };
        $(function () {
            $("form").submit(function () { return false; });//禁用form提交
            B_Users.LoadMore();
            $("#search_t").keydown(function (e) {//回车绑定
                if (e.keyCode == 13) {
                    B_Users.SearchByList({skey:$(this).val()});
                }
            });
            $("#search_b").click(function () {
                B_Users.SearchByList({ skey: $("#search_t").val() });
            });
            //字母筛选
            $(".filter_list li a").click(function () {
                if ($(this).text() == "全部") { B_Users.SearchByList({}); return;}
                B_Users.SearchByList({ char: $(this).text() });
            });
        });
        //确定选择
        function SelUser() {
            if (parent.UserFunc) { parent.UserFunc(B_Users.sellist, getParam2()); }
            else { parent.user.deal(B_Users.sellist, getParam2()); }//新版
            if ("<%:Source%>" == "plat") {//plat支持部门At
                var chks = $("input[name=plat_group_chk]:checked");
                var json = [];
                for (var i = 0; i < chks.length; i++) {
                    var model = { gid: "", gname: "" };
                    model.gid = $(chks[i]).data("gid");
                    model.gname = $(chks[i]).data("gname");
                    json.push(model);
                }
                parent.GroupAt_Add(json);
            }
        }
    </script>
</asp:Content>