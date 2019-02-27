<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Tools.Default" MasterPageFile="~/Common/Common.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head"><title>Supper Gavel-For ZoomlaCMS2</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="SupperGavel" runat="server" visible="false">
        <h1><i class="fa fa-gavel"></i>Supper gavel</h1>
        <ul>
            <li>
                <asp:TextBox ID="UserName_T" placeholder="管理员" runat="server" CssClass="form-control text_300" data-enter="0" /></li>
            <li>
                <asp:TextBox TextMode="Password" ID="UserPwd_T" placeholder="口令" runat="server" CssClass="form-control text_300" data-enter="1" /></li>
            <li>
                <asp:TextBox ID="VCode" runat="server" placeholder="验证码" CssClass="form-control text_300 code" data-enter="2" />
                <img id="VCode_img" title="点击刷新验证码" class="code" style="height: 34px;" />
                <asp:HiddenField runat="server" ID="VCode_hid" />
            </li>
            <li>
                <asp:Button ID="Login_Btn" runat="server" CssClass="btn btn-primary text_300" OnClick="Login_Btn_Click" Text="登录" data-enter="3" /></li>
        </ul>
    </div>
    <div id="SupperGavelCon" runat="server">
        <div class="admintop" id="admintop" runat="server" visible="false">
            <div class="container" style="margin: 0 auto;">
                <a href="<%:CustomerPageAction.customPath2 %>Default.aspx" class="btn btn-sm btn-primary"><i class="fa fa-bank"></i>返回后台</a>
                <a href="<%:CustomerPageAction.customPath2 %>SignOut.aspx" class="btn btn-sm btn-primary margin_l5"><i class="fa fa-power-off"></i>退出登录</a>
            </div>
        </div>
        <div class="container">
            <h1><i class="fa fa-gavel"></i>Supper gavel维护工具</h1>
            <ul class="nav nav-tabs">
                <li class="active"><a href="#tab0" data-toggle="tab">基本配置</a></li>
                <li><a href="#tab1" data-toggle="tab">高级配置</a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="tab0">
                    <table class="table table-striped table-bordered table-hover">
                        <tr>
                            <td class="td_l">配置文件检测:</td>
                            <td>
                                <asp:Button ID="Check_Btn" runat="server" OnClick="Check_Btn_Click" Text="开始检测" /></td>
                        </tr>
                        <tr>
                            <td>网站配置:</td>
                            <td>
                                <asp:Button ID="Update_Btn" runat="server" OnClick="Update_Btn_Click" Text="恢复初始" OnClientClick="return confirm('确定要恢复初始配置吗,后台路径,网站名称等均会初始化');" />
                                <asp:Button ID="Repair_Btn" runat="server" OnClick="Repair_Btn_Click" Text="修复配置" />

                            </td>
                        </tr>
                        <tr>
                            <td>开发调式模式:</td>
                            <td>
                                <asp:Button runat="server" ID="Develop_Btn" Text="开启调试" OnClick="Develop_Btn_Click" />
                                <asp:Button runat="server" ID="ColseDevlop_Btn" Text="关闭调试" OnClick="ColseDevlop_Btn_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>数据库脚本修复</td>
                            <td>
                                <asp:Button runat="server" ID="RepairData_Btn" OnClick="RepairData_Btn_Click" Text="修复脚本" />
                            </td>
                        </tr>
                        <tr>
                            <td class="text-center">文本加密/解密:
                   <div class="margin_t5">
                       <asp:Button runat="server" ID="Encry_Btn" Text="文本加密" OnClick="Encry_Btn_Click" />
                       <asp:Button runat="server" ID="Decry_Btn" Text="文本解密" OnClick="Decry_Btn_Click" />
                   </div>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="Before_T" TextMode="MultiLine" CssClass="form-control" Style="height: 80px;" placeholder="需要处理的字符串" />
                                <asp:TextBox runat="server" ID="After_T" TextMode="MultiLine" CssClass="form-control margin_t5" Style="height: 80px;" placeholder="字符串加密/解密结果" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tab-pane" id="tab1">
                    <table class="table table-bordered table-striped">
                        <tr>
                            <td class="td_l">关闭HTTPS重写:</td>
                            <td>
                                <asp:Button runat="server" ID="Close_Btn" Text="关闭HTTPS" OnClick="Close_Btn_Click" /></td>
                        </tr>
                        <tr>
                            <td>取消管理员动态口令:</td>
                            <td>
                                <asp:Button runat="server" ID="Close_Code_Btn" Text="取消口令" OnClick="Close_Code_Btn_Click" /></td>
                        </tr>
                    </table>
                </div>
            </div>
            <abbr>*不可逆操作请进行全站和数据备份后操作！</abbr>
            <div class="alert alert-info" role="alert" runat="server" id="FileInfo_Div" visible="false">
                <table class="table">
                    <thead>
                        <tr>
                            <td class="td_lg">文件名</td>
                            <td>是否存在</td>
                        </tr>
                    </thead>
                    <asp:Literal ID="Files_Li" runat="server" EnableViewState="false"></asp:Literal>
                </table>
            </div>
        </div>
        <div class="container SupperGavelTool">
            <ins>扩展工具:</ins>
            <ul class="bs-glyphicons-list">
                <li>
                    <a href="http://bbs.z01.com" target="_blank">
                        <i class="fa fa-bold"></i>
                        <span class="glyphicon-class">BootStarp框架</span>
                    </a>
                </li>
                <li>
                    <a href="http://app.z01.com" target="_blank">
                        <i class="fa fa-mobile"></i>
                        <span class="glyphicon-class">微首页</span>
                    </a>
                </li>
                <li>
                    <a href="http://app.z01.com/Class_1/Default.aspx" target="_blank">
                        <i class="fa fa-weixin"></i>
                        <span class="glyphicon-class">场景列表</span>
                    </a>
                </li>
                <li>
                    <a href="http://www.z01.com/tool/" target="_blank">
                        <i class="fa fa-cog"></i>
                        <span class="glyphicon-class">站长工具</span>
                    </a>
                </li>
                <li>
                    <a href="http://www.z01.com/blog/techs/2409.shtml" target="_blank">
                        <i class="fa fa-font"></i>
                        <span class="glyphicon-class">WebFont</span>
                    </a>
                </li>
                <li>
                    <a href="http://ad.z01.com/" target="_blank">
                        <i class="fa fa-picture-o"></i>
                        <span class="glyphicon-class">广告源码</span>
                    </a>
                </li>
                <li>
                    <a href="http://ad.z01.com/color.htm" target="_blank">
                        <i class="fa fa-align-justify"></i>
                        <span class="glyphicon-class">网页配色</span>
                    </a>
                </li>
                <li>
                    <a href="http://bbs.z01.com/boot/" target="_blank">
                        <i class="fa fa-laptop"></i>
                        <span class="glyphicon-class">响应式工具</span>
                    </a>
                </li>
                <li>
                    <a href="http://www.z01.com/mb/" target="_blank">
                        <i class="fa fa-briefcase"></i>
                        <span class="glyphicon-class">免费模板</span>
                    </a>
                </li>
                <li>
                    <a href="http://www.z01.com/pub/" target="_blank">
                        <i class="fa fa-download"></i>
                        <span class="glyphicon-class">下载逐浪CMS</span>
                    </a>
                </li>
                <li>
                    <a href="http://bbs.z01.com/index" target="_blank">
                        <i class="fa fa-users"></i>
                        <span class="glyphicon-class">技术社区</span>
                    </a>
                </li>
                <li>
                    <a href="http://www.z01.com/mtv/" target="_blank">
                        <i class="fa fa-video-camera"></i>
                        <span class="glyphicon-class">视频教程</span>
                    </a>
                </li>
                <li>
                    <a href="https://www.z01.com/blog/techs/2975.shtml" target="_blank">
                        <i class="fa fa-child"></i>
                        <span class="glyphicon-class">Emmet</span>
                    </a>
                </li>
                <li>
                    <a href="https://www.ziti163.com" target="_blank">
                        <i class="fa ZoomlaICO2015"></i>
                        <span class="glyphicon-class">字体网</span>
                    </a>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style>
        .admintop { height: 50px; position: fixed; top: 0px; left: 0; padding: 5px; width: 100%; text-align: right; }
        .code { display: none; }
    </style>
    <script src="/JS/Controls/Control.js"></script>
    <script src="/JS/ZL_ValidateCode.js"></script>
    <script>
        function EnableCode() {
            if ($(".code").is(":hidden")) {
                $(".code").show();
                $("#VCode_img").click();
                $("#VCode").ValidateCode();
            }
        }
        $(function () { Control.EnableEnter(); })
    </script>
</asp:Content>
