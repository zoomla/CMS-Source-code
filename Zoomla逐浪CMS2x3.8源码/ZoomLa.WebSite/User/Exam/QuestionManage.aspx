<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionManage.aspx.cs" Inherits="manage_Question_QuestionManage" EnableViewStateMac="false" MasterPageFile="~/Common/Common.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>试题篮</title>
<script type="text/javascript" src="/JS/OAKeyWord.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container-fluid" style="padding-top:20px;">
<ul class="nav nav-tabs">
    <li class="active"><a href="#treePanel" data-toggle="tab">分类</a></li>
    <li><a href="#keyPanel" data-toggle="tab">知识点</a></li>
</ul>
<div class="tab-content col-lg-2 col-md-2 col-xs-2 col-sm-2 padding0">
    <div class="tab-pane active treediv" id="treePanel">
        <ul>
            <li><span class='fa fa-list-alt treeicon'></span><a class='filter_class' data-val='0' href='javascript:;'>全部</a></li>
            <asp:Literal ID="QuestType_Lit" runat="server" EnableViewState="false"></asp:Literal>
        </ul>
    </div>
    <div class="tab-pane treediv" id="keyPanel">
        <div class="input-group text_md" style="margin-left:10px;">
            <input type="text" id="knowname_t" onkeydown="EnterSearch()" class="form-control" placeholder="知识点">
            <span class="input-group-btn">
            <button class="btn btn-default" type="button" onclick="SearchKnows()"><span class="fa fa-search"></span></button>
            </span>
        </div>
        <ul id="knows_ul" class="margin_t5">
            <li id="knows_li"><span class="fa fa-list-alt treeicon"></span><a data-val='0' href='javascript:;'>全部</a></li>
        </ul>
    </div>
</div>
<div class="col-lg-8 col-md-8 col-xs-8 col-sm-8">
        <table class="table table-bordered table-hover table-striped">
        <tr><td class="td_md">教材</td>
            <td>
                <ul class="sel_ul filter" data-type="version">
                    <li data-val="0" class="active"><a href="javascript:;">全部</a></li>
                    <asp:Repeater runat="server" ID="VersionRPT" EnableViewState="false">
                        <ItemTemplate>
                            <li data-val="<%#Eval("ID") %>"><a href="javascript:;"><%#Eval("VersionName") %></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </td>
        </tr>
        <tr><td class="td_md">题型</td>
            <td><ul class="sel_ul filter" data-type="type">
                    <li data-val="-1" class="active"><a href="javascript:;">全部</a></li>
                    <li data-val="0"><a href="javascript:;">单选</a></li>
                    <li data-val="1"><a href="javascript:;">多选</a></li>
                    <li data-val="2"><a href="javascript:;">填空</a></li>
                    <li data-val="3"><a href="javascript:;">解答</a></li>
                    <li data-val="4"><a href="javascript:;">完形填空</a></li>
                    <li data-val="10"><a href="javascript:;">大题</a></li>
                </ul></td></tr>
        <tr><td>难度</td><td>
            <ul class="sel_ul filter" data-type="diffcult">
                <li data-val="0" class="active"><a href="javascript:;">不限</a></li>
                <li data-val="0.91-1.00"><a href="javascript:;">基础(0.91-1.00)</a></li>
                <li data-val="0.81-0.90"><a href="javascript:;">容易(0.81-0.90)</a></li>
                <li data-val="0.51-0.80"><a href="javascript:;">中等(0.51-0.80)</a></li>
                <li data-val="0.31-0.50"><a href="javascript:;">偏难(0.31-0.50)</a></li>
                <li data-val="0.01-0.30"><a href="javascript:;">极难(0.01-0.30)</a></li>
            </ul>
            <div class="clearfix"></div>
            <div class="input-group margin_t5" style="width: 225px;margin-left:10px;">
                <span class="input-group-addon">自定义</span>
                <input type="text" value="0.5" id="sdiff_t" class="form-control text_xs float" style="border-right:none;" placeholder="起始" />
                <input type="text" value="0.8" id="ediff_t" class="form-control text_xs float" style="border-right:none;" placeholder="结束" />
                <span class="input-group-btn">
                    <input type="button" class="btn btn-default" value="筛选" onclick="CustomDiffSearch();" />
                </span>
            </div>
            </td></tr>
        <tr><td>年级</td><td>
            <ul class="sel_ul filter" data-type="grade">
                <li data-val="0" class="active"><a href="javascript:;">不限</a></li>
                <asp:Repeater runat="server" ID="GradeRPT" EnableViewState="false">
                    <ItemTemplate>
                        <li data-val="<%#Eval("GradeID") %>"><a href="javascript:;"><%#Eval("GradeName") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
                </ul>
            </td></tr>
        <tr>
            <td>知识点<br /><button type="button" class="btn btn-primary btn-xs" onclick="LoadList(GetFilter())">筛选试题</button></td>
            <td>
                <div id="knowsfiter"></div>
                <input id="fiterknows_hid" type="hidden"  />
            </td>
        </tr>
</table>
<asp:HiddenField runat="server" ID="SearchMod_Hid" />
<div id="rpt_div" class="margin_t10">
<div id="wait_div" style="position: absolute; top: 40%; left: 40%;">
    <i class="fa fa-spinner fa-spin" style="font-size: 40px;"></i>
</div>
</div>
</div>
<div style="position:absolute;top:40%;right:0px;" id="icon-cart"><div id="tip"></div></div>
<div class="col-lg-2 col-md-2 col-xs-2 col-sm-2">
    <div id="basket">
        <div class="hanger-con">
            <dl>
                <dt>共计 <span id="questcount">(0)</span> 道题</dt>
                <dd id="qtnone">
                    <p style="text-align: center; margin: 40px auto;font-size:12px;">开始添加试题组卷吧！</p>
                </dd>
                <dd id="qtlist" style="display:none;">
                    <ul id="QTypeul" style="text-align: right;"></ul>
                </dd>
            </dl>
            <a href="javascript:ShowCenter();" class="btn btn-primary">组卷中心</a>
            <a href="javascript:;" class="btn btn-danger margin_t5 clearQid">全部清空</a>
            <a href="QuestionManage2.aspx" class="btn btn-info margin_t5" style="margin-bottom:5px;">专 业 版</a>
        </div>
    </div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
    * {font-family:'Microsoft YaHei';font-size:12px;}
    .sel_ul li{float:left;margin-left:10px;padding:5px 10px 5px 10px;cursor:pointer;}
    .sel_ul li > a {text-decoration:none;color:#666;}
    .sel_ul li:hover {background:#78c8fa;color:#fff;}
    .sel_ul li:hover>a {color:#fff;text-decoration:none;}
    .sel_ul .active {background:#78c8fa;color:#fff;}
    .sel_ul .active>a {color:#fff;text-decoration:none;}
    #basket{position:fixed;top:200px;width:180px;background:#02a4de url(/App_Themes/User/basketbg.jpg) no-repeat;padding-left:37px;right:0px;z-index:999;overflow:hidden;min-height:244px;}
    .hanger-con{border-top:6px solid #029cde;border-right:6px solid #029cde;border-bottom:6px solid #029cde;background:#fff;min-height:232px;text-align:center;}
    .hanger-con dt{height:20px;line-height:20px;background:#b8e3f6;text-align:center;margin:15px auto;}
    .hanger-con dt span{color:#ff1e1e;font-weight:bold;font-size:14px;}
    #QTypeul li {border-bottom:1px dashed #ddd;margin-bottom:2px;padding-bottom:3px;padding-right:40px;}
    .flyer-img {width:50px;height:50px;border-radius:50%;}
        
    .list-group {padding-top:5px;}
    .list-group .list-group-item {cursor:pointer;overflow:hidden;text-overflow:ellipsis;display:inline-block;width:90px;}
    .list-group .list-group-item.active {background-color:#78c8fa;color:#fff;border:#fff;}
    .list-group .list-group-item:hover {background-color:#78c8fa;color:#fff;border:#fff;}
    .tab-content .tab-pane {border-top:none;}
    .treediv{ border:1px solid #ddd;padding:10px 0px;border-top:none;}
    .treediv ul,li{list-style:none;min-height:30px;}
    .treediv ul{margin-left:20px;}
    .treediv ul .lastchild {padding-left:16px;background-position: 0 0; min-height:20px;}
    .treediv ul ul{display:none; margin-top:5px;}
    .treediv li{background: url(/Images/TreeLineImages/treedash.gif) 0 0 no-repeat; background-position: 0 -176px;}
    .treediv li:last-child{background-position:-9999px -9999px;}
    .treediv .treeicon{font-size:16px; cursor:pointer; color:#999;}
    .tabinput{ border:none; padding-left:5px; height:30px; line-height:30px;display:none;}
    .width1100{width:1300px;}
</style>
<script src="/JS/SelectCheckBox.js"></script>
<script src="/JS/Plugs/fly.js"></script>
<script src="/JS/Plugs/requestAnimationFrame.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script>
    var waitdiv = "", countList = [];
    $(function () {
        $(".filter>li").click(function () {
            //更新filter
            $(this).addClass("active").siblings("li").removeClass("active");
            /*------logic-------*/
            var ul = $(this).closest("ul");
            var filter = GetFilter();
            var val = $(this).data("val");
            switch (ul.data("type")) {
                case "type":
                    filter.type = val;
                    break;
                case "version":
                    filter.version = val;
                    break;
                case "diffcult":
                    filter.diffculty = val;
                    break;
                case "nodeid"://(disuse)
                    filter.nodeid = val;
                    break;
                case "grade":
                    filter.grade = val;
                    break;
                default:
                    break;
            }
            $("#SearchMod_Hid").val(JSON.stringify(filter));
            LoadList(filter);
        });
        //分类与关键词
        $(".filter_class").click(function () {
            var filter = GetFilter();
            filter.keyword = "";
            filter.nodeid = $(this).data("val");
            $("#SearchMod_Hid").val(JSON.stringify(filter));
            LoadList(filter);
            var $pnode = $(".treediv li a[data-val='" + filter.nodeid + "']").closest('li[data-pid=0]');
            curnid = $pnode.data('id');
            if (curnid) {
                //清空知识点
                $("#knowsfiter").html('');
                $("#fiterknows_hid").val('');
                LoadKnows(curnid, $pnode.data('type'));
            }
        });
        LoadList(GetFilter());
        InitTreeEvent();
        $(".clearQid").click(function () {
            if (!confirm("确定要清空试题篮吗?")) { return false; }
            countList = [];
            RenderQList();
            AddToCart("cart_clear");
            $(".addQid").show();
            $(".removeQid").hide();
        });
        ZL_Regex.B_Float(".float");
    });
    function EnterSearch(event) {
        var event = event ? event : window.event;
        if (event.keyCode == 13) {
            SearchKnows();
            event.preventDefault();
        }
    }
    function CustomDiffSearch() {
        var filter = GetFilter();
        var sdiff = $("#sdiff_t").val();
        var ediff = $("#ediff_t").val();
        if (ZL_Regex.isEmpty(sdiff)) { alert("起始难度不能为空"); return; }
        if (ZL_Regex.isEmpty(ediff)) { alert("结束难度不能为空"); return; }
        filter.diffculty = sdiff + "-" + ediff;
        LoadList(filter);
    }
    function GetFilter() {
        var filter = { type: 0, diffculty: 0, nodeid: 0, grade: 0, keyword: "", version: 0 };
        var val = $("#SearchMod_Hid").val();
        if (val != "") { filter = JSON.parse(val); }
        return filter;
    }
    function LoadList(filter) {
        ShowWait();
        var query = "type=" + filter.type + "&diffcult=" + filter.diffculty + "&NodeID=" + filter.nodeid + "&grade=" + filter.grade;
        query += "&keyword=" + filter.keyword + "&version=" + filter.version;
        var url = "/User/Exam/QuestRPT.aspx?" + query;
        console.log(url);
        $("#rpt_div").load(url + " start", {}, function () { BindEvent(); });
            
    }
    //用于Ajax分页
    function LoadByAjax(query, page) {
        ShowWait();
        var url = "/User/Exam/QuestRPT.aspx" + query + "page=" + page;
        $("#rpt_div").load(url + " start", {}, function () { BindEvent(); });
    }
    function ShowWait() {
        if (waitdiv == "")
        { waitdiv = $("#wait_div")[0].outerHTML; }
        else { $("#rpt_div").html(""); $("#rpt_div").append(waitdiv); }
    }
    /*-------知识点--------*/
    var curnid = 0;//当前选择的科目节点
    //初始化科目
    function InitTreeEvent() {
        $(".treeicon").unbind('click');
        $(".treeicon").click(function () {
            var $ul = $(this).closest('li');
            if ($ul.hasClass("showchild")) {
                $ul.removeClass("showchild");
                $ul.find("ul:first").hide();
                $(this).removeClass("fa fa-minus-circle");
                $(this).addClass("fa fa-plus-circle");
            } else {
                $ul.addClass("showchild");
                $ul.find("ul:first").show();
                $(this).removeClass("fa fa-plus-circle");
                $(this).addClass("fa fa-minus-circle");
            }
        });
    }
    //知识点筛选
    function InitKnows(value) {
        $("#knowsfiter").html('');
        if ($("#knowsfiter").length > 0) {
            $("#knowsfiter").tabControl({
                tabW: 80,
                onRemoveTab: function (removeval) {
                    var fiter = GetFilter();
                    var knows = $("#fiterknows_hid").val().split(',');//知识点
                    var knowids = (fiter.keyword + "").split(',');
                    for (var i = 0; i < knows.length; i++) {
                        if (knows[i] == removeval) {
                            knows.splice(i, 1);
                            knowids.splice(i, 1);
                            break;
                        }
                    }
                    $("#fiterknows_hid").val(knows.join(','));
                    fiter.keyword = knowids.join(',');
                    $("#SearchMod_Hid").val(JSON.stringify(fiter));
                }
            }, value);
        }
    }
    //加载知识点
    function LoadKnows(nodeid, knowname) {
        $.post('QuestionManage.aspx', { action: 'getknows', nodeid: nodeid, knowname: knowname ? knowname : "" }, function (data) {
            $("#knows_li").nextAll().remove();
            if (data != "") {
                //knows_li
                $("#knows_ul").append(data);
                InitTreeEvent();
                $(".filter_class.knows").click(function () {
                    var filter = GetFilter();
                    filter.nodeid = 0;
                    var oldstr = $("#fiterknows_hid").val();
                    if ((',' + filter.keyword + ',').indexOf(',' + $(this).data('val') + ',') < 0) {
                        $("#fiterknows_hid").val((oldstr == "") ? $(this).text() : oldstr + ',' + $(this).text());
                        filter.keyword = filter.keyword == "" ? $(this).data('val') : filter.keyword + "," + $(this).data('val');
                    }
                    InitKnows($("#fiterknows_hid").val());
                    $("#SearchMod_Hid").val(JSON.stringify(filter));
                });
            }
        })
    }
    //搜索知识点
    function SearchKnows() {
        if (curnid > 0) {
            LoadKnows(curnid, $("#knowname_t").val());
        }
    }
    /*-------试题篮--------*/
    function RenderQList(list) {
        if (list && list != "") { countList = list; }
        if (IsAllEmpty()) {
            $("#qtnone").show(); $("#qtlist").hide();
        }
        else {
            $("#qtnone").hide(); $("#qtlist").show();
        }
        //---------------------
        var $ul = $("#QTypeul"); $ul.html("");
        var litlp = "<li>@typestr：<span class='r_green'>@count</span>道</li>";
        var allcount = 0;
        for (var i = 0; i < countList.length; i++) {
            if (countList[i].count < 1) { continue; }
            var tlp = litlp.replace("@typestr", GetTypeStr(countList[i].type)).replace("@count", countList[i].count);
            allcount += countList[i].count;
            $ul.append(tlp);
        }
        $("#questcount").text("(" + allcount + ")");
    }
    function ChangeQCount(type, num) {//更试数据后,重刷新
        var item = null;
        for (var i = 0; i < countList.length; i++) {
            if (countList[i].type == type) { item = countList[i]; break; }
        }
        if (item == null) { item = { "type": type, count: 1, "typestr": GetTypeStr(type) }; countList.push(item); }
        else
        {
            item.count = item.count + num;
        }
        RenderQList();
    }
    //true为全部是空
    function IsAllEmpty() {
        var list = countList;
        var flag = true;
        if (list == null || list.length < 1) { return flag; }
        for (var i = 0; i < list.length; i++) {
            if (list[i].count > 0) { flag = false; break; }
        }
        return flag;
    }
    function GetTypeStr(type)
    {
        switch (type)
        {
            case 0:
                return "单选题";
            case 1:
                return "多选题";
            case 2:
                return "填空题";
            case 3:
                return "选择题";
            case 10:
                return "大题";
            default:
                return "单选题";
        }
    }

    function BindEvent()
    {
        $(".addQid").click(function () {
            var qid = $(this).data("qid");
            var type=$(this).data("type");
            $(this).hide().siblings(".cart_op").show();
            ChangeQCount(type, 1);
            AddToCart("cart_add", qid);
            //----------------
            var offset = $("#icon-cart").offset();
            var flyer = $('<img class="flyer-img" src="/Images/Copy.png">'); // //抛物体对象
            flyer.fly({
                start: {
                    left: event.pageX,
                    top: (event.pageY - $(window).scrollTop())//y需要减去减动条
                },
                end: {
                    left: offset.left + 10,
                    top: offset.top + 10,
                    width: 0, //结束时宽度 
                    height: 0 //结束时高度 
                },
                onEnd: function () {
                    //$("#tip").show().animate({ width: '200px' }, 300).fadeOut(500);//成功加入购物车动画效果 
                    //this.destory();
                    flyer.remove(); 
                }
            });
        });
        $(".removeQid").click(function () {
            var qid = $(this).data("qid");
            var type=$(this).data("type");
            AddToCart("cart_remove", qid);
            ChangeQCount(type, -1);
            $(this).hide().siblings(".cart_op").show();
        })
    }
    function inputQMinfo() {
        $("#Excel_ifr").attr("src", "ExportExcel.aspx");
    }
    function AddToCart(action,qid)
    {
        //如果未登录,则弹窗提示
        $.post("QuestAPI.aspx?action=" + action, { "qid": qid }, function (data) {
            if (data == -2)
            {
                alert("请先登录");
            }
        });
    }
    function ShowCenter() {
        var filter = GetFilter();
        comdiag.title = "组卷中心";
        comdiag.url = "PaperCenter.aspx?pclass=" + filter.pclass;
        comdiag.maxbtn = false;
        comdiag.reload = true;
        comdiag.ShowModal();
    }
</script>
</asp:Content>