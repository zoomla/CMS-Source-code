<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step2.aspx.cs" Inherits="ZoomLaCMS.App.Step2" MasterPageFile="~/Common/Master/Commenu.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>定制效果</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div id="stepbar" style="padding-left: 140px; margin-bottom: 10px;">
                <ul class="step_bar">
                    <li class="step g_step1"><a class="g_a_step1" href="javascript:;"><i class="fa fa-desktop"></i>指定链接</a></li>
                    <li class="green_line"></li>
                    <li class="step g_step2"><a class="g_a_step2" href="javascript:;"><i class="fa fa-paint-brush active"></i>定制效果</a></li>
                    <li class="green_line"></li>
                    <li class="step step3"><a class="a_step3" href="javascript:;"><i class="fa fa-android"></i>生成APP</a></li>
                    <li>
                        <a href="APPList.aspx" class="btn btn-info" style="margin-top:8px;" >我的APP</a>
                    </li>
                </ul>
                <div style="clear: both;"></div>
            </div>
        </div>
        <ul class="nav nav-tabs hidden-xs hidden-sm">
            <li class="active"><a onclick="javascript:;" href="#tab0" data-toggle="tab">基本配置</a></li>
            <li><a onclick="javascript:;" href="#tab1" data-toggle="tab">模板选择</a></li>
        </ul>
        <div class="panel-body tab-content" style="padding:0px;">
            <div class="col-lg-6 col-md-6 mobile">
                <iframe style="border: none; width: 275px; height: 463px; background-color: #fff; overflow-x: hidden" id="url_ifr" runat="server"></iframe>
            </div>
                        <div id="tab0" data-step="1" class="stepitem tab-pane active col-lg-6 col-md-6">
                <table class="table table-bordered table-striped" runat="server" id="hasauth_div">
                        <tr><td>APP名称：</td><td>
                            <asp:TextBox runat="server" ID="APPName_T" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="APPName_T" ForeColor="Red" Display="Dynamic" ErrorMessage="APP名称不能为空" /></td></tr>
                        <tr><td class="td_md">图标：</td><td>
                            <ZL:FileUpload runat="server" data-img="appicon_img" ID="APPIcon_F"/>
                            <%--<asp:FileUpload runat="server" data-img="appicon_img" ID="APPIcon_F"/>--%>
                            <img src="" class="img50" id="appicon_img" />
                            <span class="rd_green">宽高45px-80px之间的png图片.<br />请勿上传违法、违规、未获得授权或与推广无关内容</span>
                        </td></tr>
                        <%--<tr><td>目标平台：</td><td>
                            <label class="font14"><input type="checkbox" value="android" checked="checked" /><i class="fa fa-android"></i> Android</label>
                            <label class="font14"><input type="checkbox" value="apple" disabled="disabled" /><i class="fa fa-apple"></i> ios</label>
                            <span class="r_green">(如需生成ios应用,请先将ios证书提交至平台)</span></td></tr>--%>
                        <tr><td colspan="2" class="text-center">高级选项</td></tr>
                        <tr><td class="td_md">启动图片：</td><td>
                            <ZL:FileUpload runat="server" data-img="splash_img" ID="Splash_F" />
                            <%--<asp:FileUpload runat="server" data-img="splash_img" ID="Splash_F"/>--%>
                            <img src="" class="img50" id="splash_img" /></td></tr>
                        <tr><td>作者：</td><td><asp:TextBox runat="server" ID="Author_T" CssClass="form-control"></asp:TextBox></td></tr>
                        <tr><td>APP描述：</td><td><asp:TextBox runat="server" ID="Description_T" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox></td></tr>
                        <tr><td>模板选择：</td></tr>
                      <%--  <tr><td>扩展插件：</td><td>
                            <label><input type="checkbox" value="camera" />摄像头</label>
                            <label><input type="checkbox" value="cache" />本地缓存</label>
                            <label><input type="checkbox" value="network" />网络状况</label>
                            <label><input type="checkbox" value="battery" />电池访问</label>
                            <label><input type="checkbox" value="contact" />访问通讯录</label>
                                            </td></tr>--%>
                        <tr><td></td><td>
                            <input type="button" value="上一步" onclick="ZL_Step.Pre();" class="btn btn-primary" />
                                <asp:Button runat="server" ID="Create_Btn" OnClick="Create_Btn_Click" OnClientClick="return CheckSubmit();" CssClass="btn btn-primary" Text="生成APP" />
                                        </td></tr>
                </table>
                <div runat="server" id="noauth_div" visible="false" class="margin_t10">
                    <ul class="list-unstyled" id="optionul">
                        <li>
                        <div class="opmenu" style="background: rgb(153, 153, 255);" title="开始学习">
                                <a target="_blank" href="http://www.z01.com/PhoneGap/">
                                <i class="fa fa-copyright"></i>
                                Zoomla!逐浪CMS基于全球领先的移动跨平台解决方案，并提供丰富的线上移动开发文档库【开始学习】</a><span class="clearfix"></span>
                        </div></li>
                        <li><div class="opmenu" style="background: rgb(102, 153, 51);" title="点击访问">
                            <a target="_blank" href="http://www.z01.com/server">
                                <i class="fa fa-tasks"></i>
                                购买商业授权进行企业级部署【立即购买】</a><span class="clearfix"></span>
                        </div></li>
                    </ul>
                    <div class="clearfix"></div>
                    <div class="alert alert-danger" style="display:none;">站点未授权,无法本地生成APP,你可以申请授权或在线生成APP,如果你已有授权码<a href="<%=CustomerPageAction.customPath2+"Config/SiteOption.aspx?Tab=2" %>">[点此配置]</a></div>
                </div>
            </div>
            <div id="tab1" class="tab-pane col-lg-6 col-md-6">
                <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" CssClass="table table-bordered table-striped table-hover" EnableTheming="False"
                    OnPageIndexChanging="EGV_PageIndexChanging" EmptyDataText='<div class="text-center">您还没有自己的模板,请点击上方"在线模板"按钮添加模板!</div>'>
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <input type="radio" name="idrad" value="<%#Eval("ID") %>" />
                            </ItemTemplate>
                            <ItemStyle CssClass="td_s" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="名称">
                            <ItemTemplate>
                                <a href="/App/AppTlp/Design.aspx?id=<%#Eval("ID") %>&vpath=<%#Eval("TlpUrl") %>" target="_blank"><%#Eval("Alias") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="创建时间" DataField="CDate" />
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <a href="/APP/AppTlp/Design.aspx?id=<%#Eval("ID") %>&vpath=<%#Eval("TlpUrl") %>" target="_blank">修改</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </ZL:ExGridView>
            </div>
        </div>
    </div>
    <div class="alert alert-danger" runat="server" id="remind_sp">
        如需在服务器布署APP生成功能,请先布署好Android与PhoneGap环境,你也可以使用<a href="http://app.z01.com/APP/AddAPP.aspx" target="_blank">[线上版本]</a>生成APP
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <link href="/App_Themes/V3.css" rel="stylesheet" />
    <style type="text/css">
        #optionul {padding-top:20px;}
        #optionul li{margin-bottom:10px;}
        #optionul .opmenu {padding:10px;border-radius:5px;height:70px;}
        #optionul .opmenu a {color:#fff}
        #optionul .opmenu a i {font-size:50px;float:left;padding-right:10px;}
        .remindDiv{font-size:16px;color:#fff;margin-bottom:5px;}
        .font14 {font-size:14px;font-weight:normal;}
        .img50 {width:50px;height:50px;display:none;}
        .step_bar {list-style-type:none;margin:0;list-style:none;}
        .step_bar .fa {font-size:25px;padding-right:5px;}
        .step_bar .fa.active {color:green;}
        .step_bar li {float:left;}
        .step_bar .step {width:90px;padding-top:10px;}
        .green_line {background:url(/App_Themes/Admin/Mobile/green_line.png) no-repeat 0 23px;width:44px;height:24px;}
        .mobile {background: url(/App_Themes/User/bg_mobile.png) no-repeat; width: 327px; height: 674px; padding-left: 25px; padding-top: 120px;}

        .step1 {background: url(/App_Themes/Admin/Mobile/banner_11.png) no-repeat; width: 100%; height: 420px; padding-top: 180px; padding-left: 650px;}
        .stepitem {display:none;}
        .stepitem.active {display:block;}
        .remind div {margin-bottom:3px;}
    </style>
    <script src="/JS/ZL_Regex.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/ICMS/ZL_Common.js"></script>
    <script>
        //提交前检测
        function CheckSubmit() {
            if (ZL_Regex.isEmpty($("#APPIcon_F").val())) {
                alert("未指定图标"); return false;
            }
            ShowWait();
            return true;
        }
        function ShowWait() {
            var wait = new ZL_Dialog();
            wait.closebtn = false;
            wait.maxbtn = false;
            wait.ShowMask("正在提交申请,请等待提交完成");
        }
        //-----步骤方法,后期整合,暂定页面只有一个控件
        //从1开始
        var ZL_Step = {};
        ZL_Step.Next = function (callback) {
            var $step = $(".stepitem.active");
            var num = parseInt($step.data("step")) + 1;
            ZL_Step.ShowByNum(num, callback);
        }
        ZL_Step.Pre = function (callback) {
            var $step = $(".stepitem.active");
            var num = parseInt($step.data("step")) - 1;
            if (num < 1) { stepnum = 1; }
            ZL_Step.ShowByNum(num, callback);
        }
        //显示指定步骤
        ZL_Step.ShowByNum = function (num, callback) {
            var $step = $(".stepitem[data-step=" + num + "]");
            $(".stepitem").removeClass("active");
            $(".stepitem").hide();
            $step.addClass("active");
            $step.show();
            if (callback) { callback(); }
        }
        //----------------显示图像
        $(function () {
            var isPng = function (fname) {
                if (!fname || fname == "" || fname.indexOf(".") < 0) { return false; }
                fname = fname.toLowerCase();
                var start = fname.lastIndexOf(".");
                var ext = fname.substring((start + 1), fname.length);//jpg|png|gif
                return (ext == "png");
            }
            var ShowImg = function (imgid, base64) {
                $("#" + imgid).show(); $("#" + imgid).attr('src', base64);
            }
            $("input[type=file][data-img]").change(function (e) {
                var fname = $(this).val();
                if (!isPng(fname)) { $(this).val(""); alert("请使用png图片"); return false; }
                var imgid = $(this).data("img");
                var file = e.target.files[0];
                var reader = new FileReader();
                reader.onload = function (e) {
                    ShowImg(imgid, e.target.result);
                }
                reader.readAsDataURL(file);
            });
            $(".allchk_l").html('<input type="radio" value="0" name="idrad" checked="checked">不使用模板');
        });
    </script>
</asp:Content>
