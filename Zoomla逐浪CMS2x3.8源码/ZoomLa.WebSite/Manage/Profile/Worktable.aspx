<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Worktable.aspx.cs" Inherits="ZoomLa_WebSite_Manages_Worktable" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link href="/App_Themes/V3.css" rel="stylesheet" />
<title><%=Resources.L.我的工作台 %></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb navbar-fixed-top hidden-sm hidden-xs" runat="server" id="BreadNav">
<li><a href="../Main.aspx" style="color: #428bca;"><%=Resources.L.工作台 %></a></li>
<li class="active"><%=Resources.L.版本信息 %>&nbsp;&nbsp; <a title="打开欢迎界面" href="javascript:;" onclick="$(parent.document).find('#ShowAD_Btn').click();"><%=Resources.L.欢迎 %></a><strong>
	<asp:Literal runat="server" ID="litUserName"></asp:Literal>
</strong>&nbsp;<%=Resources.L.今天是 %>:
<asp:Literal runat="server" ID="litDate"></asp:Literal>
	<span id="times" name="times"></span><a href="../../Common/SelectChinaDay.html">[<%=Resources.L.万年历 %>]</a> </li>
<%= Call.GetHelp(1) %>
</ol>
<div style="height: 40px;" class=" hidden-sm hidden-xs"></div>
<div class="panel panel-info Worktable_well">
    <div class="panel-heading">
        <%=Resources.L.欢迎进入 %> <%:Call.SiteName.Trim() %> <%=Resources.L.后台管理系统 %>，
        <asp:Label ID="Version" runat="server"></asp:Label>。
    </div>
    <div class="panel-body">
        <ul class="link_menu" style="list-style-type: none;">
            <li class="litd"><a href="../Config/SiteInfo.aspx"><i class="fa fa-cogs"></i></a><br /><a href="../Config/SiteInfo.aspx" target="_self"><%=Resources.L.网站配置 %></a></li>
            <li class="litd"><a href="../Content/ModelManage.aspx?ModelType=1"><i class="fa fa-edit"></i></a><br /><a href="../Content/ModelManage.aspx?ModelType=1" target="_self"><%=Resources.L.内容模型 %></a></li>
            <li class="litd"><a href="../Content/NodeManage.aspx"><i class="fa fa-university"></i></a><br /><a href="../Content/NodeManage.aspx" target="_self"><%=Resources.L.节点设置 %></a></li>
            <li class="litd"><a href="../Template/LabelManage.aspx"><i class="fa fa-tags"></i></a><br /><a href="../Template/LabelManage.aspx" target="_self"><%=Resources.L.标签设置 %></a></li>
            <li class="litd"><a href="../Template/TemplateSet.aspx"><i class="fa fa-cubes"></i></a><br /><a href="../Template/TemplateSet.aspx" target="_self"><%=Resources.L.模板设置 %></a></li>
            <li class="litd"><a href="../User/AdminManage.aspx"><i class="fa fa-lock"></i></a><br /><a href="../User/AdminManage.aspx" target="_self"><%=Resources.L.安全设定 %></a></li>
            <li class="litd"><a href="../Content/ContentManage.aspx"><i class="fa fa-file-text-o"></i></a><br /><a href="../Content/ContentManage.aspx" target="_self"><%=Resources.L.内容管理 %></a></li>
        </ul>
    </div>
</div>
<div class="container-fulid" style="overflow-x:hidden;">
<div class="row padding_l_r10px">
    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 padding5">
        <div class="panel panel-default Worktable_well">
            <div class="panel-heading"><i class="fa fa-file"></i><span> <%=Resources.L.内容管理 %></span><span class="badge pull-right"><a href="javascript:;" title="<%=Resources.L.内容管理 %>" onclick="ShowMain('NodeTree.ascx','Content/ContentManage.aspx');">More</a></span></div>
            <div class="panel-body WorktableHeight"><div id="chart1" class="zlchart"></div></div>
        </div>
    </div>
    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 padding5">
        <div class="panel panel-default Worktable_well shop_panel">
            <div class="panel-heading">
                <a href="javascript:;" class="btn btn-default" onclick="shoptabs('goods');"><i class="fa fa-shopping-bag"></i> 商品</a>
                <a href="javascript:;" class="btn btn-default" onclick="shoptabs('order');"><i class="fa fa-shopping-cart"></i> 订单</a>
                <a href="javascript:;" class="btn btn-default" onclick="shoptabs('pay');"><i class="fa fa-dollar"></i> 流水</a>
                <%--<i class="fa  fa-shopping-cart"></i><span> <%=Resources.L.商城管理 %></span><span class="badge pull-right">--%>
                <span class="badge pull-right"><a href="javascript:;" title="<%=Resources.L.商城管理 %>" id="shop_more" class="sgoods" onclick="showmore(this);">More</a></span>
            </div>
            <div class="panel-body goods"><div id="chart2" class="zlchart"></div></div>
            <div hidden class="panel-body order" style="padding:0px;height:240px;">
                <table class="table table-bordered table-striped">
                    <tr><td>ID</td><td>用户名</td><td>下单时间</td><td>订单状态</td><td>付款状态</td></tr>
                    <asp:Repeater runat="server" ID="Order_RPT" EnableViewState="false">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("ID") %></td>
                                <td><%#Eval("ReName") %></td>
                                <td><%#Eval("AddTime","{0:yyyy-MM-dd HH:mm:ss}") %></td>
                                <td><%#formatzt(Eval("OrderStatus", "{0}"),"0")%> <input type="hidden" class="returnmsg_hid" value="<%#Eval("Guojia") %>" /></td>
                                <td><%#formatzt(Eval("Paymentstatus", "{0}"),"1")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div hidden class="panel-body pay" style="padding:0px;height:240px;">
                <table class="table table-bordered table-striped">
                    <tr><td>ID</td><td>用户名</td><td>支付平台</td><td>交易时间</td><td>金额</td></tr>
                    <asp:Repeater runat="server" ID="Pay_RPT">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("PaymentID") %></td>
                                <td><%#getusername(Eval("UserID","{0}"))%></td>
                                <td><%#getPayPlat(Eval("PayPlatID","{0}"))%></td>
                                <td><%#Eval("PayTime")%></td>
                                <td><%#Eval("MoneyPay","{0:f}") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 padding5">
        <div class="panel panel-default Worktable_well">
            <div class="panel-heading"><i class="fa  fa-user"></i><span> <%=Resources.L.会员管理 %></span><span class="badge pull-right"><a href="javascript:;" title="<%=Resources.L.会员管理 %>" onclick="ShowMain('UserGuide.ascx','User/UserManage.aspx');">More</a></span></div>
            <div class="panel-body WorktableHeight"><div id="chart3" class="zlchart"></div></div>
        </div>
    </div>
    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 padding5">
        <div class="panel panel-default">
            <div class="panel-heading"><span><%=Resources.L.浏览信息 %></span></div>
            <div class="panel-body WorktableHeight"><iframe id="showiframe" src="../BrowserCheck.aspx" marginwidth="1" marginheight="1" scrolling="no" frameborder="0" style="border-style: none; border-color: inherit; border-width: medium; width: 100%; height: 250px;"></iframe></div>
            <div class="panel-footer">
                <a href="http://www.z01.com/help/web/1414.shtml" class="margin-15 hidden-xs" title="<%=Resources.L.设置Cookie %>" target="_blank"><%=Resources.L.设置Cookie %></a>
                <a href="http://www.z01.com/help/web/1413.shtml" class="margin-15 hidden-xs hidden-sm" title="<%=Resources.L.设置网页脚本 %>" target="_blank"><%=Resources.L.设置网页脚本 %></a>
                <a href="<%:customPath2+"Common/SystemFinger.aspx" %>" title="<%=Resources.L.服务器信息总览 %>"><span style="margin-right: 2px;"><%=Resources.L.服务器信息总览 %></span><i class="fa fa-forward"></i></a>
            </div>
        </div>
    </div>
    <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12 padding5">
        <div class="panel panel-default">
            <div class="panel-heading">
                <a href="javascript:;" class="btn btn-default" onclick="showPanel('sche');"><i class="fa fa-clock-o"></i> 任务计划</a>
                <a href="javascript:;" class="btn btn-default" onclick="showPanel('note');"><i class="fa fa-book"></i> <%=Resources.L.备忘录 %></a>
            </div>
            <div class="panel-body sche" style="padding:0px;height:240px;">
                <table class="table table-bordered table-striped">
                    <tr><td>任务ID</td><td>任务名称</td><td>创建时间</td><td>执行计划</td><td>结果</td></tr>
                    <asp:Repeater runat="server" ID="Sche_RPT" EnableViewState="false">
                        <ItemTemplate>
                            <tr><td><%#Eval("ID") %></td>
                                <td><%#Eval("TaskName") %></td>
                                <td><%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %></td>
                                <td><%#GetExecuteType() %></td>
                                <td><%#GetResult() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="panel-footer sche">
                <a href="../Content/Schedule/Default.aspx" class="btn btn-info">任务中心</a>
                <a href="../Content/Schedule/AddSche.aspx" class="btn btn-info margin_l5">添加任务</a>
                <a href="../Content/Schedule/ScheLogList.aspx" class="btn btn-info margin_l5">执行记录</a>
            </div>
            <div style="display:none;" class="panel-body WorktableHeight note"><asp:TextBox ID="NoteBook" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 100%; resize: none;" /></div>
            <div style="display:none;" class="panel-footer note"><%=Resources.L.提示 %>：<%=Resources.L.记录随Cookies删除而消失 %></div>
        </div>
    </div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
.zlchart {width:100%;height:260px;}
.shop_panel .panel-body{height:252px;}
.shop_panel .goods{padding-top:0px;}
</style>
<script src="/JS/jquery.zclip.min.js"></script>
<script src="/JS/Plugs/transtool.js"></script>
<script src="/JS/ICMS/alt.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/Plugins/ECharts/build/source/echarts.js"></script>
<script>
$(function () {
        $("#NoteBook").bind("blur paste cut", function () {//keyup paste cut
            setTimeout(function () { window.localStorage.NoteBook = $("#NoteBook").val(); }, 100);
        });
        if (window.localStorage.NoteBook) { $("#NoteBook").val(window.localStorage.NoteBook); }
    })
function showPanel(panel){
    $(".sche,.note").hide();
    $("."+panel).show();
}
function shoptabs(panel){
    $(".goods,.order,.pay").hide();
    $("."+panel).show();
    $("#shop_more").removeClass("sgoods").removeClass("sorder").removeClass("spay").addClass("s"+panel);
}
function showmore(more){
    var $this = $(more);
    console.log("this",$this);
    if($this.hasClass("sgoods")){ 
        ShowMain('ShopNodeTree.ascx','Shop/ProductManage.aspx');
    }else if($this.hasClass("sorder")){
        showleft('menu3_3','Shop/OrderList.aspx');
    }else if($this.hasClass("spay")){
        showleft('menu3_4','Shop/PayList.aspx');
    }
}
</script>
<script>
    var myChart1 = echarts.init(document.getElementById('chart1'));
    var myChart2 = echarts.init(document.getElementById('chart2'));
    var myChart3 = echarts.init(document.getElementById('chart3'));
    //内容,商品,会员
    var option1 = {
        title: {
            left :"left",
            text: '内容统计',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            show:false,
            orient: 'vertical',
            left: 'left',
            data: []
        },
        series: [
            {
                name: '数据量',
                type: 'pie',
                radius: '55%',
                center: ['50%', '50%'],
                data: [],
                itemStyle: {
                    emphasis: {
                        shadowBlur: 10,
                        shadowOffsetX: 0,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    }
                }
            }
        ]
    };
    /*----------------------------------------------------*/
    var option2 = {
        title : {
            text: '商品统计',
        },
        tooltip : {
            trigger: 'axis'
        },
        legend: {
            data:['商品数', '销量']
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis : [
            {
                type : 'value',
                boundaryGap : [0, 0.01]
            }
        ],
        yAxis : [
            {
                type : 'category',
                data : []
            }
        ],
        series : [
            {
                name:'商品数',
                type:'bar',
                data:[]
            },
            {
                name:'销量',
                type:'bar',
                data:[]
            }
        ]
    };
    /*----------------------------------------------------*/
    var option3 = {
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b}: {c} ({d}%)"
        },
        legend: {
            orient: 'vertical',
            x: 'left',
            data: []
        },
        series: [
            {
                name: '用户来源',
                type: 'pie',
                selectedMode: 'single',
                radius: [0, '30%'],

                label: {
                    normal: {
                        position: 'inner'
                    }
                },
                labelLine: {
                    normal: {
                        show: false
                    }
                },
                data: []
            },
            {
                name: '用户分组',
                type: 'pie',
                radius: ['40%', '55%'],
                data: []
            }
        ]
    };
    $(function () {
        //内容
        var data1 = <%=data1%>;
        option1.legend.data = [];
        option1.series[0].data = [];
        for (var i = 0; i < data1.length; i++) {
            option1.legend.data.push(data1[i].NodeName);
            option1.series[0].data.push({ value: data1[i].ItemCount, name: data1[i].NodeName });
        }
        myChart1.setOption(option1);
        <%-- //商城(环状)
        var data2 = <%=data2%>;
        option2.legend.data = [];
        option2.series[0].data = [];
        for (var i = 0; i < data2.length; i++) {
            option2.legend.data.push(data2[i].NodeName);
            option2.series[0].data.push({ value: data2[i].ICount, name: data2[i].NodeName });
        }
        myChart2.setOption(option2);--%>
        <%--  var data2 = <%=data2%>;//(竖线)
        option2.xAxis.data = [];
        option2.series[0].data = [];
        for (var i = 0; i < data2.length; i++) {
            option2.xAxis.data.push(data2[i].NodeName);
            option2.series[0].data.push(data2[i].ICount);
        }--%>
        var data2 = <%=data2%>;//(横状)
        option2.yAxis[0].data = [];
        option2.series[0].data = [];
        option2.series[1].data = [];
        for (var i = 0; i < data2.length; i++) {
            option2.yAxis[0].data.push(data2[i].NodeName);
            option2.series[0].data.push(data2[i].ProCount);
            option2.series[1].data.push(data2[i].SellCount);
        }
        myChart2.setOption(option2);
        //会员(来源与会员组)
        var data3_1=<%=data3_1%>;
        var data3_2=<%=data3_2%>;

        option3.legend.data = [];
        option3.series[0].data = [];
        option3.series[1].data = [];

        option3.legend.data.push("直接注册","用户推荐");
        option3.series[0].data.push({value: data3_1[0].count1, name: '注册' });
        option3.series[0].data.push({value: data3_1[0].count2, name: '推荐'});

        for (var i = 0; i < data3_2.length; i++) {
            option3.legend.data.push(data3_2[i].GroupName);
            option3.series[1].data.push({ value: data3_2[i].ICount, name: data3_2[i].GroupName });
        }
        myChart3.setOption(option3);
    })
</script>
</asp:Content>