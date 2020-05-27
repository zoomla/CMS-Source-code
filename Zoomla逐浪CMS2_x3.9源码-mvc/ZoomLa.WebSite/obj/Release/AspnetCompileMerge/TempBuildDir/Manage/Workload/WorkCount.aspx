<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkCount.aspx.cs" Inherits="ZoomLaCMS.Manage.Workload.WorkCount"MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register TagPrefix="uc"TagName="NodeList"Src="~/Manage/I/ASCX/NodeTreeJs.ascx"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>统计</title>
    <style>
        .padding_td5 {padding-top: 5px;padding-bottom: 5px;}
        .dashed { border-bottom: 1px dashed #ccc;}
        iframe{border:none; width:100%; height:310px;}
        table tr th,td{text-align:center;}
        .input-group-btn{width:0%;}
        .container-fluid{padding-left:0px;padding-right:0px;}
        .down_ico{margin-right:5px; cursor:pointer;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container-fluid">
        <div class="col-lg-2 col-md-2 col-sm-2">
            <div class="panel panel-default">
                <div class="panel-heading">栏目列表</div>
                <div class="panel-body">
                    <uc:NodeList ID="Lists" runat="server" />
                </div>
            </div>
        </div>
        <div id="countlist_div" class="col-lg-10 col-md-10 col-sm-10">
            <div class="container-fluid padding_td5 dashed">
                <strong>年份：</strong>
                <div class="btn-group" id="years_div" data-toggle="buttons">
                    <asp:Literal ID="Years_Li" EnableViewState="false" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="container-fluid padding_td5 dashed">
                <strong>月份：</strong>
                <div class="btn-group" data-toggle="buttons">
                    <asp:Literal ID="Months_Li" EnableViewState="false" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="container-fluid padding_td5">
                <strong>模型：</strong>
                <div class="btn-group" data-toggle="buttons">
                    <asp:Literal ID="Models_Li" runat="server" EnableViewState="false"></asp:Literal>
                </div>
            </div>
            <div class="alert alert-warning" role="alert">统计概括：发稿量(<span id="pcount_s">0</span>) 评论量(<span id="comcount_s">0</span>) 点击数(<span id="hits_s">0</span>)</div>
            <div class="container-fluid padding_td5">
                <label><input type="radio" name="DrawData" value="1" checked />发稿量</label> 
                <label><input type="radio" value="2" name="DrawData" />评论数</label>
                <label><input type="radio" name="DrawData" value="3" />点击数</label>
            </div>
            <div class="container-fluid">
                <iframe id="draw_ifr" style="display:none;"></iframe>
            </div>
            <div class="container-fluid">
                <asp:Button ID="Export_B" runat="server" Text="导出Excel" CssClass="btn btn-primary" OnClick="Export_B_Click" />
                <table id="DataTable" class="table table-striped table-hover table-bordered" style="display:none;">
                    <thead>
                        <tr>
                        <th>日期</th><th>发稿量</th><th>评论数</th><th>访问量</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <input type="hidden" value="" id="NodeID_hid" />
            <input type="hidden" value="" id="curNid_hid" />
        </div>
    </div>
    <input type="hidden" id="worddatas_hid" />
    <script>
        $().ready(function () {
            
            $("[name='DrawData']").click(function () {
                if (this.checked)
                    InitDrawTool();
            });
            $(".btn-group label").click(function () {
                InitData();
            });
            InitData();
        });
        function ExNode(obj, nodid) {
            $(obj).parent().parent().find("ul").hide(500);
            $(obj).next("ul").show(500);
            InitData(nodid);
        }
        function ShowData(obj, nodeid) {
            InitData(nodeid);
        }
        function ShowMain(url, target, obj) {
            $(obj).parent().parent().find("ul").hide(500);
            $(obj).next("ul").show(500);
            $("#NodeID_hid").val(target);
            InitData();
        }
        var DataObj = {};//返回数据对象
        function InitData(nodeid) {
            $("#draw_ifr").fadeOut(200);
            $("#DataTable").fadeOut(200, function () {
                $.ajax({
                    type: "POST",
                    url: $("#NodeID_hid").val(),
                    data: { action: "groupcount", year: $("[name='years']:checked").val(), month: $("[name='months']:checked").val(), mid: $("[name='model']:checked").val(),NodeID:nodeid?nodeid:'' },
                    success: function (data) {
                        var countobj = JSON.parse(data);
                        $("#curNid_hid").val(countobj.nodeid);
                        DataObj.DataCounts = countobj.attr;
                        DataObj.CurYear = $("[name='years']:checked").val();
                        DataObj.CurMonth = $("[name='months']:checked").val();
                        InitDrawTool();
                    }
                });
            });
            
        }
       
        //加载图表数据
        function InitDrawTool() {
            var pcount = 0; var comcount = 0; var hits = 0; //统计数量
            var days = "";//统计表天数
            var value = "";//统计值
            var trtemp = "<tr><td>@date</td><td>@pcount</td><td>@comcount</td><td>@hits</td></tr>";//表格数据模板
            $("#DataTable tbody").html("");//清空数据表
            for (var i = 0; i < DataObj.DataCounts.length; i++) {
                //days += DataObj.DataCounts[i].Days;
                switch ($("[name='DrawData']:checked").val()) {
                    case "1":
                        value += DataObj.DataCounts[i].PCount;
                        break;
                    case "2":
                        value += DataObj.DataCounts[i].ComCount;
                        break;
                    case "3":
                        value += DataObj.DataCounts[i].Hits;
                        break;
                }
                days += DataObj.DataCounts[i].Years + "-" + DataObj.DataCounts[i].Months + "-" + DataObj.DataCounts[i].Days;
                if (DataObj.DataCounts.length - 1 != i) {
                    value += ","; days += ",";
                }
                pcount += parseInt(DataObj.DataCounts[i].PCount); comcount += parseInt(DataObj.DataCounts[i].ComCount); hits += parseInt(DataObj.DataCounts[i].Hits);
                $("#DataTable tbody").append(trtemp.replace(/@date/g, DataObj.DataCounts[i].Years + "-" + DataObj.DataCounts[i].Months + "-" + DataObj.DataCounts[i].Days).replace(/@pcount/g, DataObj.DataCounts[i].PCount)
                           .replace(/@comcount/g, DataObj.DataCounts[i].ComCount).replace(/@hits/g, DataObj.DataCounts[i].Hits));
            }
            //显示统计数量
            $("#pcount_s").text(pcount); $("#comcount_s").text(comcount); $("#hits_s").text(hits);
            //控制图表头部信息显示内容
            var month = DataObj.CurMonth != "-1" ? DataObj.CurMonth+"月" : "";//月份信息
            var year = DataObj.CurYear != "-1" ? DataObj.CurYear + "年" : "";//年份信息
            $("#worddatas_hid").val("{" + (DataObj.CurYear!="-1"?DataObj.CurYear+"年":"全部") + "|" + value + "|" + days + "}");
            $("#draw_ifr").attr("src", "/Plugins/Chart/line.aspx?Bases=1200|300|" + year + month + "统计报表|无&hid=worddatas_hid");
            $("#draw_ifr").load(function () { $("#draw_ifr").fadeIn(200); })
            $("#DataTable").fadeIn(200);
        }
    </script>
</asp:Content>
