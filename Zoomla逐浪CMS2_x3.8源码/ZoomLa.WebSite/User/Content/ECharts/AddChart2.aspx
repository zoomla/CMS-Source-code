<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddChart2.aspx.cs" Inherits="test_echart" MasterPageFile="~/User/Default.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>智慧图表</title>
<link href="/Plugins/Third/ystep/css/ystep.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="office" data-ban="chart"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a href='Default.aspx'>智慧图表</a></li>
        <li><a href='AddChart.aspx'>创建图表</a></li><li class='active'><a href='<%=Request.RawUrl %>'>图表配置</a>[<asp:Label ID="ImgType_L" runat="server"></asp:Label>]</li>
    </ol>
    </div>
    <div class="container">
        <div class="row">
            <div class="container-fluid" >
                <div class="ystep1"></div>
                <ul class="nav nav-tabs" role="tablist" id="mytab">
                    <li role="presentation" class="active"><a href="#base_tb" aria-controls="base_tb" role="tab" data-toggle="tab">基本配置</a></li>
                    <li role="presentation"><a href="#data_tb" aria-controls="data_tb" role="tab" data-toggle="tab">数据编辑</a></li>
                    <li role="presentation"><a href="#code_tb" aria-controls="code_tb" role="tab" data-toggle="tab">查看代码</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" role="tabpanel"  id="base_tb">
                        <table class="table table-bordered table-striped">
                           <tr><td class="td_l">主标题:</td><td><asp:TextBox runat="server" CssClass="form-control text_300" ID="Title_T" placeholder="请输入标题" /></td></tr>
                           <tr><td>主标题超链接:</td><td><asp:TextBox runat="server" CssClass="form-control text_300" ID="Link_T" Text="http://demo.zoomla.com/" /></td></tr>
                           <tr><td>注释说明:</td><td><asp:TextBox runat="server" CssClass="form-control text_300" TextMode="MultiLine" Height="100" ID="SubTitle_T" placeholder="注释说明" /></td></tr>
                           <tr><td>标题位置水平</td><td>
                               <label><input type="radio" name="titlex_rad"  value="left" />左</label>
                               <label><input type="radio" name="titlex_rad" checked="checked" value="center"/>中</label>
                               <label><input type="radio" name="titlex_rad" value="right"/>右</label></td></tr>
                           <tr><td>标题位置垂直</td><td>
                              <label><input type="radio" name="titley_rad" checked="checked" value="top" />上</label>
                              <label><input type="radio" name="titley_rad" value="center" />中</label>
                              <label><input type="radio" name="titley_rad" value="bottom" />下</label></td></tr>
                           <tr><td>工具栏:</td><td>
                              <ul>
                                  <li><label><input type="checkbox" id="tb_calculable" checked="checked" /> 是否开启拖动</label></li>
                                  <li><label><input type="checkbox" id="tb_show" checked="checked" /> 是否显示工具箱</label></li>
                                  <li><label><input type="checkbox" id="tb_mark" checked="checked"/> 绘制辅助线功能按钮</label></li>
                                  <li><label><input type="checkbox" id="tb_dataZoom" /> 选区缩放功能按钮</label></li>
                                  <li><label><input type="checkbox" id="tb_dataView" checked="checked"/> 切换至数据视图按钮</label></li>
                                  <li><label><input type="checkbox" id="tb_magicType"/> 折柱切换按钮</label></li>
                                  <li><label><input type="checkbox" id="tb_restore"/> 还原按钮</label></li>
                                  <li><label><input type="checkbox" id="tb_saveAsImage" checked="checked"/> 保存为图片按钮</label></li>
                              </ul>
                           </td></tr>
                  </table>
                        <div class="text-center"><button type="button" onclick="ShowTag('data_tb')" class="btn btn-primary">下一步</button></div>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="data_tb">
                        <div style="width:100%; height:500px;">

                        </div>
                        <div class="text-center margin_t5"><button type="button" onclick="ShowTag('base_tb')" class="btn btn-primary">上一步</button> <asp:Button runat="server" ID="Button1" OnClientClick="PreSave();" OnClick="Save_Btn_Click1" CssClass="btn btn-primary" Text="保存图表"/>
                            <input type="button" value="预览图表" onclick="ReCreateImg()" class="btn btn-success" /></div>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="code_tb">
                        <asp:TextBox runat="server" ID="code" TextMode="MultiLine" CssClass="form-control" Style="height: 500px;width:100%;max-width:100%;"></asp:TextBox>
                        
                    </div>
                </div>
            </div>
               <div style="width:100%; height:500px; position:absolute; top:-9999px; left:20px; overflow:hidden;" id="excel1"></div>
            <div class="container-fluid margin_t5" id="PreImg" style="display:none;">
                <div class="panel panel-default">
                  <div class="panel-heading">图表预览</div>
                  <div class="panel-body">
                    <iframe id="echarts_ifr" src="" style="width:100%;height:510px;border:none;"></iframe>
                  </div>
                </div>
        </div>
        </div>
   </div>
    <asp:HiddenField runat="server" ID="ChartTitle_Hid" />
    <asp:HiddenField runat="server" ID="ToolBox_Hid" />
    <asp:HiddenField runat="server" ID="Package_Hid" />
    <asp:HiddenField runat="server" ID="Cdate_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <link href="/Plugins/Third/excel/handsontable.full.css" rel="stylesheet" />
    <script src="/Plugins/Third/excel/handsontable.full.js"></script>
    <script src="/Plugins/Third/ystep/js/ystep.js"></script>
    <script>
        function PreSave() {
            GetConfig();
            return true;
        }
        function ShowTag(id) {
            $("[aria-controls='" + id + "']").tab('show');
        }
        $().ready(function () {
            //绑定滑动门事件
            $("[aria-controls='data_tb']").on('show.bs.tab', function (e) {
                if ($("#Title_T").val().trim() == "") {
                    alert("请输入标题信息!");
                    return false;
                }
                console.log(e);
                $("#excel1").css("top", "165px");
                ShowStep(3)
                ReCreateImg();
            });
            $("[aria-controls='data_tb']").on('hide.bs.tab', function (e) {
                $("#excel1").css("top", "-9999px");
            });
            $("#Title_T").focus();
            //步骤插件
            $(".ystep1").loadStep({
                size: "large",
                color: "green",
                steps: [{
                    title: "选择",
                    content: "选择图表"
                }, {
                    title: "配置",
                    content: "设置基本参数"
                }, {
                    title: "编辑",
                    content: "对表格进行编辑"
                }, {
                    title: "完成",
                    content: "点击保存完成操作"
                }]
            });
            ShowStep(2);
            setTimeout(function () {  }, 500);
        });
        function ShowStep(num) {
            $(".ystep1").setStep(num);
        }
    </script>
    <script>
        //-----------------------------------------默认数据
        //折线,柱图
        var lineDef = {//xAis
            rowdata: ["周一", "周二", "周三", "周四", "周五", "周六", "周日"],
            series: [
       {
           name: "最高气温",
           type: "line",
           data_doub: [11, 11, 15, 13, 12, 13, 10]
       },
       {
           name: "最低气温",
           type: "line",
           data_doub: [1, -2, 2, 5, 3, 2, 15]
       }]
        };
        //纵向折线图
        var ylineDef = {//yAis
            rowdata: ["0", "10", "20", "30", "40", "50", "60", "70", "80"],
            series: [
                {
                    name: "高度(km)与气温(°C)变化关系",
                    data_doub: [15, -50, -56.5, -46.5, -22.1, -2.5, -27.7, -55.7, -76.5]
                }
            ]
        }
        //柱状图
        var barDef = {
            rowdata: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
            series: [
        {
            name: "蒸发量",
            type: "bar",
            data_doub: [2, 4.9, 7, 23.2, 25.6, 76.7, 135.6, 162.2, 32.6, 20, 6.4, 3.3]
        },
       {
           name: "降水量",
           type: "bar",
           data_doub: [2.6, 5.9, 9, 26.4, 28.7, 70.7, 175.6, 182.2, 48.7, 18.8, 6, 2.3]
       }
       ]
        };
        //饼图
        var pieDef = {//legend
            rowdata: ["直接访问", "邮件营销", "联盟广告", "视频广告", "搜索引擎"],
            series: [{
           name: "访问来源",
           data_mod: [
               { value: 335, name: "直接访问" }, { value: 310, name: "邮件营销" }, { value: 234, name: "联盟广告" },
               { value: 135, name: "视频广告" }, { value: 1548, name: "搜索引擎" }]
            }]
        };
        //嵌套饼图
        var inpieDef = {
            rowdata: ["直达", "营销广告", "搜索引擎", "邮件营销", "联盟广告", "视频广告", "百度", "谷歌", "必应", "其他"],
            series: [{
                name: "访问来源",
                data_mod: [{value: 335,name: "直达"},{value: 679,name: "营销广告"},{value: 1548,name: "搜索引擎"}]
            }, {
                name: "访问来源", data_mod: [{ value: 335, name: "直达" }, { value: 310, name: "邮件营销" }, { value: 234, name: "联盟广告" }, { value: 135, name: "视频广告" }, { value: 1048, name: "百度" }, { value: 251, name: "谷歌" },
                    {value: 147,name: "必应"},{value: 102,name: "其他"}]
            }]
        }
        //标准南丁玫瑰图
        var nanpieDef = {
            rowdata: ["rose1", "rose2", "rose3", "rose4", "rose5", "rose6", "rose7", "rose8"],
            series: [{
                name: "半径模式",
                data_mod: [{ value: 10, name: "rose1" }, { value: 5, name: "rose2" }, { value: 15, name: "rose3" }, { value: 25, name: "rose4" }, { value: 20, name: "rose5" }, { value: 35, name: "rose6" }, { value: 30, name: "rose7" }, { value: 40, name: "rose8" }]
            }]
            
            }
        //漏斗
        var funnelDef = {
            rowdata: ["直接访问", "邮件营销", "联盟广告", "视频广告", "搜索引擎"],
            series: [{
                name: "访问来源", data_mod: [
            { value: 80, name: "直接访问" }, { value: 60, name: "邮件营销" }, { value: 40, name: "联盟广告" },
            { value: 20, name: "视频广告" }, { value: 100, name: "搜索引擎" }]
            }]
        };
        //仪表盘
        var dashDef = {
            rowdata: ["业务指标"],
            series: [{ name: "业务指标", data_mod: [{ name: "完成率", value: 50 }] }]
        }
        //-------------------------
        //散点
        var scatterDef = {
            rowdata: ["女性", "男性"],
            series: [{ name: "女性", data_doub_arr: [[161.2, 51.6], [172.9, 62.5], [153.4, 42], [160, 50], [147.2, 49.8], [168.2, 49.2], [175, 73.2], [157, 47.8], [167.6, 68.8], [159.5, 50.6], [175, 82.5], [166.8, 57.2], [176.5, 87.8], [170.2, 72.8], [174, 54.5], [173, 59.8], [179.9, 67.3], [170.5, 67.8], [162.6, 61.4]] }
                , { name: "男性", data_doub_arr: [[174, 65.6], [164.1, 55.2], [163, 57], [171.5, 61.4], [184.2, 76.8], [174, 86.8], [182, 72], [167, 64.6], [177.8, 74.8], [180.3, 93.2], [180.3, 82.7], [177.8, 58], [177.8, 79.5], [177.8, 78.6], [177.8, 71.8], [177.8, 72], [177.8, 81.8], [180.3, 83.2]] }
            ]
        };

        //气泡
        var circleDef = {
            rowdata: ["scatter1", "scatter2"],
            series: [{ name: "scatter1", data_doub_arr: [[-69, 58, 70], [10, 74, 29], [-11, 68, 99], [10, 76, 53], [-79, -9, 93], [-81, -67, 56], [-95, -71, 50], [-7, 22, 5], [-57, -67, 73], [16, 58, 73], [-65, 30, 56], [4, -75, 3], [-65, 92, 98], [30, -65, 29], [-11, -37, 59], [48, -31, 89], [-39, -51, 23], [-21, -37, 23], [24, -11, 38], [90, -19, 79], [-61, -11, 98], [-23, -83, 75], [-39, 58, 25], [-93, -65, 24], [60, -17, 22], [54, 88, 27], [-29, 76, 76], [-45, 32, 89], [-69, 58, 63], [90, 34, 33], [-41, -5, 91], [58, -33, 77], [-79, -83, 69], [-99, -43, 80], [44, 8, 0], [-81, -19, 74], [-13, 40, 84], [60, -67, 82], [16, 14, 59], [-37, 36, 93], [0, 54, 23], [-61, 44, 26], [32, 60, 10], [90, 20, 21], [20, -91, 53], [32, -87, 73], [-85, 90, 74], [72, -1, 95], [-67, -59, 87], [-21, -29, 60]] }
                , { name: "scatter2", data_doub_arr: [[76, 72, 50], [-67, 70, 52], [26, 20, 71], [-57, -63, 93], [-19, -49, 78], [46, 28, 71], [-87, -97, 7], [-81, -59, 71], [32, 28, 49], [68, 88, 73], [20, -47, 46], [-61, 18, 89], [-89, -3, 92], [-19, -27, 43], [-55, -67, 18], [-79, 52, 92], [14, 16, 61], [-19, -1, 97], [-93, -67, 81], [-63, 14, 4], [-87, -37, 92], [24, 96, 82]] }
            ]
        }
        //地图
        var mapDef = {
            rowdata: ['iphone3', 'iphone4', 'iphone5'],
            series: [
                    {
                        name: 'iphone3',
                        type: 'map',
                        mapType: 'china',
                        roam: false,
                        itemStyle: {
                            normal: { label: { show: true } },
                            emphasis: { label: { show: true } }
                        },
                        data_mod: [
                            { name: '北京', value: 1800 },
                            { name: '天津', value: 600 },
                            { name: '上海', value: 540 },
                            { name: '重庆', value: 200 },
                            { name: '河北', value: 156 },
                            { name: '河南', value: 2600 },
                            { name: '云南', value: 5000 },
                            { name: '辽宁', value: 3200 },
                            { name: '黑龙江', value: 1600 },
                            { name: '湖南', value: 15 },
                            { name: '安徽', value: 21 },
                            { name: '山东', value: 66 },
                            { name: '新疆', value: 889 },
                            { name: '江苏', value: 2300 },
                            { name: '浙江', value: 1200 },
                            { name: '江西', value: 800 },
                            { name: '湖北', value: 600 },
                            { name: '广西', value: 500 },
                            { name: '甘肃', value: 1300 },
                            { name: '山西', value: 3000 },
                            { name: '内蒙古', value: 1200 },
                            { name: '陕西', value: 2000 },
                            { name: '吉林', value: 2600 },
                            { name: '福建', value: 3500 },
                            { name: '贵州', value: 2100 },
                            { name: '广东', value: 332 },
                            { name: '青海', value: 233 },
                            { name: '西藏', value: 156 },
                            { name: '四川', value: 62 },
                            { name: '宁夏', value: 158 },
                            { name: '海南', value: 198 },
                            { name: '台湾', value: 600 },
                            { name: '香港', value: 888 },
                            { name: '澳门', value: 200 }
                        ]
                    },
                    {
                        name: 'iphone4',
                        type: 'map',
                        mapType: 'china',
                        itemStyle: {
                            normal: { label: { show: true } },
                            emphasis: { label: { show: true } }
                        },
                        data_mod: [
                            { name: '北京', value: 1986 },
                            { name: '天津', value: 1083 },
                            { name: '上海', value: 1024 },
                            { name: '重庆', value: 300 },
                            { name: '河北', value: 200 },
                            { name: '安徽', value: 600 },
                            { name: '新疆', value: 2200 },
                            { name: '浙江', value: 523 },
                            { name: '江西', value: 882 },
                            { name: '山西', value: 106 },
                            { name: '内蒙古', value: 820 },
                            { name: '吉林', value: 326 },
                            { name: '福建', value: 568 },
                            { name: '广东', value: 1726 },
                            { name: '西藏', value: 1850 },
                            { name: '四川', value: 500 },
                            { name: '宁夏', value: 900 },
                            { name: '香港', value: 1300 },
                            { name: '澳门', value: 1900 }
                        ]
                    },
                    {
                        name: 'iphone5',
                        type: 'map',
                        mapType: 'china',
                        itemStyle: {
                            normal: { label: { show: true } },
                            emphasis: { label: { show: true } }
                        },
                        data_mod: [
                            { name: '北京', value: 2200 },
                            { name: '天津', value: 1080 },
                            { name: '上海', value: 266 },
                            { name: '广东', value: 54 },
                            { name: '台湾', value: 682 },
                            { name: '香港', value: 777 },
                            { name: '澳门', value: 941 }
                        ]
                    }
            ]
        }
        //标题,工具箱
        var titleMod = { text: "", subtext: "", link: "", sublink: "", x: "left", y: "top" };
        var toolboxMod = {
            show: true,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: true },
                dataZoom: { show: false },
                magicType: { show: false, type: ["line", "bar"] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        };
        function GetSeriesData(seriesArr)
        {
            for (var i = 0; i < seriesArr.length; i++) {
                seriesArr[i].data = DealData(seriesArr[i]);
            }
        }
        function DealData(series) {
            if (series.data_int && series.data_int != null) { return series.data_int; }
            if (series.data_doub_arr) { return series.data_doub_arr; }//散点等
            if (series.data_doub && series.data_doub != null) { return series.data_doub; }
            if (series.data_str && series.data_str != null) { return series.data_str; }
            if (series.data_mod) { return series.data_mod; }
        }
    </script>
    <script>
        var type = "<%=Type%>", tag = "<%=Tag%>";
        var hot1, container1 = document.getElementById('excel1');
        function InitExcel()
        {
            hot1 = new Handsontable(container1, {
                //startRows: 20,//装载data后此项丢失,如有需要,使用minRows等
                //startCols: 20,
                minRows: 20,
                minCols: 31,
                //data: xlsdata,
                rowHeaders: true,
                colHeaders: true,
                contextMenu: true,
                manualColumnResize: true,
               // outsideClickDeselects: true,
                minSpareRows: 1,
                minSpareCols: 1,
            });
        }
        //新建,修改,保存时调用
        function LoadNewConfig() {
            //注意顺序,先初始化Excel,再显示图像,再导入数据
            InitExcel();
            switch (type) {
                case "line":
                    LoadLineData()
                    break;
                case "pie":
                    LoadPieData();
                    break;
                case "funnel":
                    CreateImg(funnelDef);
                    hot1.loadData(DataToXls(funnelDef.rowdata, funnelDef.series));
                    break;
                case "scatter":
                    CreateImg(scatterDef);
                    hot1.loadData(DataToXls(scatterDef.rowdata, scatterDef.series));
                    break;
                case "circle":
                    CreateImg(circleDef);
                    hot1.loadData(DataToXls(circleDef.rowdata, circleDef.series));
                    break;
                case "bar":
                    CreateImg(barDef);
                    hot1.loadData(DataToXls(barDef.rowdata, barDef.series));
                    break;
                case "dash":
                    CreateImg(dashDef);
                    hot1.loadData(DataToXls(dashDef.rowdata, dashDef.series));
                    break;
                case "map":
                    CreateImg(mapDef);
                    hot1.loadData(DataToXls(mapDef.rowdata, mapDef.series));
                    break;
            }
        }
        function LoadEditConfig() {
            titleMod = JSON.parse($("#ChartTitle_Hid").val());
            $("#Title_T").val(titleMod.text);
            $("#Link_T").val(titleMod.link);
            $("#SubTitle_T").val(titleMod.subtext.split('\n').length>1?titleMod.subtext.split('\n')[0]:"");//去除时间部分
            $("#SubLink_T").val(titleMod.sublink);
            $("input[name=titlex_rad]:checked").val(titleMod.x);
            $("input[name=titley_rad]:checked").val(titleMod.y);
            //------------
            toolboxMod = JSON.parse($("#ToolBox_Hid").val());
            document.getElementById("tb_show").checked = toolboxMod.show;
            document.getElementById("tb_mark").checked = toolboxMod.feature.mark.show;
            document.getElementById("tb_dataZoom").checked = toolboxMod.feature.dataZoom.show;
            document.getElementById("tb_dataView").checked = toolboxMod.feature.dataView.show;
            document.getElementById("tb_magicType").checked = toolboxMod.feature.magicType.show;
            document.getElementById("tb_restore").checked = toolboxMod.feature.restore.show;
            document.getElementById("tb_saveAsImage").checked = toolboxMod.feature.saveAsImage.show;
            (new Function(document.getElementById("code").value))();
            document.getElementById("tb_calculable").checked = option.calculable;
            //-----
            var packMod = JSON.parse($("#Package_Hid").val());
            InitExcel();
            hot1.loadData(DataToXls(packMod.rowdata, packMod.series));
            $("#echarts_ifr").attr("src", "/Plugins/ECharts/ZLEcharts.aspx");//或调用方法重新生成
        }
        function LoadPieData() {
            var data = null;
            switch (tag) {
                case "inside":
                    data = inpieDef;
                    break;
                case "doublearea":
                case "areanan":
                case "nanpie":
                    data = nanpieDef;
                    break;
                default:
                    data = pieDef;
                    break;
            }
            InitExcel();
            CreateImg(data);
            hot1.loadData(DataToXls(data.rowdata, data.series));
        }
        function LoadLineData() {
            var data = null;
            switch (tag) {
                case "yline":
                    data = ylineDef;
                    break;
                default:
                    data = lineDef;
                    break;
            }
            InitExcel();
            CreateImg(data);
            hot1.loadData(DataToXls(data.rowdata, data.series));
        }
        function ReCreateImg()
        {
            $("#PreImg").show();
            var datas = XlsToData();
            CreateImg(datas);
        }
        //收集参数,传给后台和获取参数,更新配置
        function CreateImg(datas) {
            GetConfig();
            for (var i = 0; i < datas.series.length; i++) {
                delete  datas.series[i].data;
            }
            $.ajax({
                type: "post",
                url: "?type=" + type+ "&tag=" + tag,
                data: { action: "createimg", title: JSON.stringify(titleMod), toolbox: JSON.stringify(toolboxMod), packmod: JSON.stringify(datas), calculable: document.getElementById("tb_calculable").checked },
                success: function (data) {
                    $("#code").val(data);
                    $("#echarts_ifr").attr("src", "/Plugins/ECharts/ZLEcharts.aspx");//或调用方法重新生成 
                },
                error: function () {alert("数据格式不正确,请确保行与列的值相对应"); }
                });
        }
        function GetConfig() {
            //----普通配置
            titleMod.text = $("#Title_T").val();
            titleMod.link = $("#Link_T").val().indexOf("http://") < 0 ? "http://" + $("#Link_T").val() : $("#Link_T").val();
            titleMod.subtext = $("#SubTitle_T").val().trim() == "" ? $("#Cdate_Hid").val() : $("#SubTitle_T").val() + "\n" + $("#Cdate_Hid").val();
            titleMod.sublink = $("#SubLink_T").val();
            titleMod.x = $("input[name=titlex_rad]:checked").val();
            titleMod.y = $("input[name=titley_rad]:checked").val();
            //----工具箱配置
            toolboxMod.show = document.getElementById("tb_show").checked;
            toolboxMod.feature.mark.show = document.getElementById("tb_mark").checked;
            toolboxMod.feature.dataZoom.show = document.getElementById("tb_dataZoom").checked;
            toolboxMod.feature.dataView.show = document.getElementById("tb_dataView").checked;
            toolboxMod.feature.magicType.show = document.getElementById("tb_magicType").checked;
            toolboxMod.feature.restore.show = document.getElementById("tb_restore").checked;
            toolboxMod.feature.saveAsImage.show = document.getElementById("tb_saveAsImage").checked;
            //----获取数据
            
            $("#ChartTitle_Hid").val(JSON.stringify(titleMod));
            $("#ToolBox_Hid").val(JSON.stringify(toolboxMod));
            $("#Package_Hid").val(JSON.stringify(XlsToData()));
        }
        //-----------------------------------------Excel
        function DataToXls(rowdata, seriesArr) {
            GetSeriesData(seriesArr);//索引一份至data
            var colArr = [];
            if (rowdata[0] != "") { rowdata.unshift(""); }
            colArr.push(rowdata);
            for (var i = 0; i < seriesArr.length; i++) {//加上行头与数据
                seriesArr[i].data.unshift(seriesArr[i].name);
                switch (type) {
                    case "line":
                        colArr.push(seriesArr[i].data);
                        break;
                    default:
                        arr = ConverVal(seriesArr[i].data);
                        colArr.push(arr);
                        break;
                }

            }
            return colArr;
        } 
        //将数组转化为图表所需的JSON数据格式
        function XlsToData() {
            var datas = { rowdata: [], series: [] };
            var arr = deepcopy(hot1.getData());//避免修改原数据
            datas.rowdata = arr.shift(); if (datas.rowdata[0] == "") { datas.rowdata.shift(); }
            ClearNullArr(datas.rowdata);
            for (var i = 0; i < arr.length; i++) {//row行
                if (!arr[i][0] || arr[i][0] == null) { continue; }
                ClearNullArr(arr[i]);
                switch (type)
                {
                    case "line":
                        datas.series.push({ name: arr[i].shift(), data_doub: arr[i], type: "line" });
                        break;
                    case "bar":
                        datas.series.push({name:arr[i].shift(),data_doub:arr[i],type:"bar"});
                        break;
                    case "pie":
                        //将字符串重新转化为JSON,否则后台无法使用,第一列为行头,不需要转
                        ConverToJson(arr[i]);
                        datas.series.push({ name: arr[i].shift(), data_mod: arr[i] });
                        break;
                    case "funnel":
                        ConverToJson(arr[i]);
                        datas.series.push({ name: arr[i].shift(), data_mod: arr[i] });
                        break;
                    case "scatter":
                        ConverToJson(arr[i]);
                        datas.series.push({ name: arr[i].shift(), data_doub_arr: arr[i] });
                        break;
                    case "circle":
                        ConverToJson(arr[i]);
                        datas.series.push({name:arr[i].shift(),data_doub_arr:arr[i]});
                        break;
                    case "dash":
                        ConverToJson(arr[i]);
                        datas.series.push({ name: arr[i].shift(), data_mod: arr[i] });
                        break;
                    case "map":
                        ConverToJson(arr[i]);
                        datas.series.push({ name: arr[i].shift(), data_mod: arr[i] });
                        break;
                }
            }
            return datas;
        }
        //值转换,非json对象不转
        function ConverVal(arr) {
            for (var i = 1; i < arr.length; i++) {
                if (typeof (arr[i]) == "object") {
                    arr[i] = JSON.stringify(arr[i]);
                }
            }
            return arr;
        }
        //将值从字符串转回Json用于回发服务端之前
        function ConverToJson(arr)
        {
            for (var i = 1; i < arr.length; i++) {
                arr[i] = JSON.parse(arr[i]);
            }
        }
        //将值转回数组,散点|气泡
        function ConverToArr(arr) {
            for (var i = 1; i < arr.length; i++) {
                arr[i] =eval(arr[i]);
            }
        }
        //移除空值
        function ClearNullArr(arr) {
            for (var i = arr.length; i >= 0; i--) {
                if (arr[i] == null||arr[i] =="") { arr.splice(i, 1); }
            }
            return arr;
        }
        //深度拷贝
        function deepcopy(obj) {
            var out = [], i = 0, len = obj.length;
            for (; i < len; i++) {
                if (obj[i] instanceof Array) {
                    out[i] = deepcopy(obj[i]);
                }
                else out[i] = obj[i];
            }
            return out;
        }
        //是否Json或对象数据
        function IsObject(obj)
        {
            return(typeof (obj) == "object" && Object.prototype.toString.call(obj).toLowerCase() == "[object object]" && !obj.length)
        }
    </script>
</asp:Content>