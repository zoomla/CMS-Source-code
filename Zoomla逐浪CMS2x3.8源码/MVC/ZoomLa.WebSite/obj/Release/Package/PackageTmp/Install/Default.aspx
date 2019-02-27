<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Install.Default" ClientIDMode="Static" ValidateRequest="false" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title><%=Resources.L.安装界面Title %></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="renderer" content="webkit" />
    <link href="/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
    <!--[if lt IE 9]>
	<script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
	<script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
<![endif]-->
    <link href="/dist/css/font-awesome.min.css" rel="stylesheet" />
    <link href="/dist/css/animate.min.css" rel="stylesheet" />
    <link href="/dist/css/animate.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="/App_Themes/User.css" />
    <script src="/JS/jquery-1.11.1.min.js"></script>
    <script src="/dist/js/bootstrap.min.js"></script>
    <script src="/dist/js/bootstrap-switch.js"></script>
    <style>
        .instbox {
            background-color: #999;
            background-position: center;
            left: 0;
            top: 0;
            right: 0;
            bottom: 0;
            position: absolute;
            background-repeat: no-repeat;
            background-size: cover;
        }

            .instbox .instcon {
                margin: auto;
                margin-top: 10vh;
                width: 90%;
                height: 680px;
                background: rgba(0,118,222,0.8);
                border-radius: 20px;
                text-align: center;
                padding-left: 10px;
                padding-right: 10px;
            }

                .instbox .instcon h1 {
                    padding-top: 2em;
                    color: #fff;
                    font-family: "STHeiti","Microsoft YaHei UI light","Microsoft YaHei","宋体","arial";
                    font-size: 1.8em;
                    text-align: center;
                }

        .instside {
            margin-top: 1em;
        }
        /*five points below the page*/
        /*.instside .carousel-indicators{ bottom:auto;}*/
        /*.instside .carousel-indicators li {width:20px; height:20px;background-color: #09f;border:#09f;}
.instside .carousel-indicators .active{width:20px; height:20px;background-color: #ccc;border:#ccc;}*/
        #carousel_step {
            margin: 0 auto;
            width: 500px;
        }

            #carousel_step li {
                float: left;
                margin-left: 10px;
                color: #fff;
                width: 70px;
                text-align: center;
            }

            #carousel_step .step_circle {
                background-color: #09f;
                border: 1px solid #008aff;
                border-radius: 50%;
                width: 40px;
                height: 40px;
                margin: 0 auto;
            }

                #carousel_step .step_circle.active {
                    background-color: #ccc;
                    border: 1px solid #ddd;
                }
        /*licence*/
        .lic_btn {
            margin: auto;
            border-radius: 15px;
            line-height: 28px;
            height: 30px;
            border: 0px solid #333;
            background-color: #fff;
            background-size: 15px 15px;
            background-repeat: no-REPEAT;
            background-position: 5px center;
            color: #333;
            font-size: 14PX;
            width: 280px;
            cursor: pointer;
        }

        .lic_content {
            position: absolute;
            z-index: 1;
            top: 0;
            left: 0;
            color: #fff;
            background-color: #369;
            padding: 12px;
            opacity: 1;
        }

        .lic_head {
            margin-top: 5em;
            border-radius: 15px;
            height: 28px;
            width: 100%;
            background-color: #BBB;
            color: #282828;
            text-align: center;
        }
        /*steps*/
        .modal {
            top: -100px;
        }

        .bottom_btns {
            text-align: center;
            margin-top: 50px;
        }

        .content_body {
            margin-top: 35px;
        }

        .div300 {
            display: inline-block;
            width: 300px;
            max-width: 300px;
        }

        .control-label {
            color: #fff;
            line-height: 34px;
            min-width: 90px;
            display: inline-block;
        }

        .insbox_txt {
            color: #fff;
        }

        .small_txt {
            color: #fff;
            margin-left: 5px;
        }

        .check_item_div {
            margin-bottom: 3px;
        }

            .check_item_div .fa-check {
                color: #A5E360;
                margin-right: 10px;
            }

            .check_item_div .fa-remove {
                color: red;
                margin-right: 10px;
            }

        .name_tips {
            color: #fff;
            font-size: 18px;
        }

        .insok {
            padding-top: 8em;
        }

            .insok h2 {
                font-size: 2em;
            }

                .insok h2 a {
                    color: #fff;
                }

            .insok i {
                font-size: 1.8em;
            }

            .insok span {
                font-size: 0.4em;
                font-style: oblique;
            }

        .user_mimenu_left li a {
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
            padding-right: 3px;
        }

        #carousel_step div {
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="instbox" class="instbox">
            <div class="user_mimenu user_home_mimenu hidden-xs">
                <div class="navbar navbar-fixed-top" role="navigation">
                    <button type="button" class="btn btn-default" id="mimenu_btn">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <div class="user_mimenu_left">
                        <ul class="list-unstyled">
                            <li><a href="/help.html" target="_blank"><%=Resources.L.快速帮助 %></a></li>
                            <li><a href="http://help.z01.com" target="_blank"><%=Resources.L.使用手册 %></a></li>
                            <li><a href="http://www.z01.com/mtv" target="_blank"><%=Resources.L.视频教程 %></a></li>
                            <li><a href="http://help.z01.com/Database" target="_blank"><%=Resources.L.数据字典 %></a></li>
                            <li><a href="http://www.z01.com/shop" target="_blank"><%=Resources.L.商业授权 %></a></li>
                            <li><a href="http://www.z01.com/corp" target="_blank"><%=Resources.L.关于逐浪 %></a></li>
                            <li><a href="http://www.z01.com/y10" target="_blank"><%=Resources.L.十年辉煌 %></a></li>
                        </ul>
                    </div>
                    <div class="navbar-header">
                        <button class="navbar-toggle in" type="button" data-toggle="collapse" data-target=".navbar-collapse">
                            <span class="sr-only"><%=Resources.L.移动下拉 %></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                    </div>
                </div>
            </div>
            <asp:Wizard runat="server" ID="Install_Wzd" ActiveStepIndex="0" DisplaySideBar="False" Width="100%" DisplayCancelButton="false">
                <WizardSteps>
                    <asp:WizardStep runat="server" ID="WizardStep1" StepType="Start">
                        <div class="container-fulid instcon">
                            <h1><%=Resources.L.安装1Title %></h1>
                            <div class="insbox_txt" style="margin-top: 120px;"><%=Resources.L.欢迎安装ZoomlaCMS %></div>
                            <div class="content_body">
                                <div class="lic_btn" onclick="showlic();"><%=Resources.L.安装显示许可 %></div>
                                <div class="lic_content" id="lic_content" style="display: none;" ondblclick="hidelic();">
                                    <div class="lic_head" onclick="hidelic();"><%=Resources.L.安装点击此次 %> </div>
                                    <div class="text-left">
                                        <asp:Literal runat="server" ID="Licence_Lit"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="insbox_txt bottom_btns">
                                <label>
                                    <input type="checkbox" id="hasRead_chk" onclick="sureLicense();" />
                                    <%=Resources.L.我已阅读并同意协议 %></label>
                                <asp:Button ID="Stpe1_Next_Btn" runat="server" disabled="disabled" CssClass="btn btn-primary" CommandName="MoveNext" Text="<%$Resources:L,下一步 %>" OnClick="Stpe1_Next_Btn_Click" />
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" ID="WizardStep2" Title="">
                        <div class="container-fulid instcon">
                            <h1><%=Resources.L.运行环境检测 %></h1>
                            <table class="container content_body" style="max-width: 900px;">
                                <tr>
                                    <td style="text-align: left;">
                                        <asp:Label runat="server" ID="Check_Basic_L" EnableViewState="false" CssClass="insbox_txt"></asp:Label></td>
                                    <td style="text-align: left;">
                                        <asp:Label runat="server" ID="Check_File_L" EnableViewState="false" CssClass="insbox_txt"></asp:Label></td>
                                    <td style="text-align: left;">
                                        <asp:Label runat="server" ID="Check_File2_L" EnableViewState="false" CssClass="insbox_txt"></asp:Label></td>
                                </tr>
                            </table>
                            <div class="bottom_btns">
                                <asp:Button ID="Step2_Pre_Btn" runat="server" OnClick="Step2_Pre_Btn_Click" Text="<%$Resources:L,上一步 %>" CssClass="btn btn-primary" UseSubmitBehavior="false" CausesValidation="false" />
                                <asp:Button ID="Step2_Next_Btn" runat="server" CommandName="MoveNext" Text="<%$Resources:L,下一步 %>" CssClass="btn btn-primary" OnClick="Step2_Next_Btn_Click" />
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" ID="WizardStep3" Title="">
                        <div class="container-fulid instcon">
                            <h1><%=Resources.L.配置数据库连接 %></h1>
                            <div class="container content_body text-left" style="max-width: 700px;">
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.数据库版本 %></span>
                                    <asp:DropDownList ID="SqlVersion_DP" onchange="sqlselect()" runat="server" class="form-control text_300">
                                        <asp:ListItem Selected="True" Value="MSSql" Text="<%$Resources:L,SqlServer2005及更高版本 %>"></asp:ListItem>
                                        <asp:ListItem Value="Local" Text="<%$Resources:L,本地数据库 %>"></asp:ListItem>
                                        <asp:ListItem Value="Oracle">Oracle</asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="margin_l20">
                                        <a href="http://www.z01.com/Item/2977.aspx" target="_blank" class="btn btn-info"><i class="fa fa-question-circle"></i><%=Resources.L.未安装数据库 %></a>
                                    </span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.数据源地址 %></span>
                                    <asp:TextBox ID="TxtDataSource" runat="server" class="form-control text_300" data-enter="0" Text="(local)" />
                                    <span class="control-label margin_l20">
                                        <asp:RequiredFieldValidator ID="ValrDataSource" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="TxtDataSource" ErrorMessage="<%$Resources:L,数据源不能为空 %>" />
                                        <%=Resources.L.如local或远程主机名 %>
                                    </span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.数据库名称 %></span>
                                    <asp:TextBox ID="TxtDataBase" runat="server" Text="ZoomlaCMS" data-enter="1" class="form-control text_300" autofocus="autofocus" />
                                    <span class="control-label margin_l20">
                                        <asp:RequiredFieldValidator ID="ValrDataBase" runat="server" ForeColor="Red" ControlToValidate="TxtDataBase" ErrorMessage="<%$Resources:L,数据库名称不能为空 %>" />
                                    </span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.数据库用户 %></span>
                                    <asp:TextBox ID="TxtUserID" runat="server" data-enter="2" class="form-control text_300" Text="ZoomlaCMS" />
                                    <span class="control-label margin_l20">
                                        <asp:RequiredFieldValidator ID="ValrUserID" runat="server" ForeColor="Red" ControlToValidate="TxtUserID" ErrorMessage="<%$Resources:L,用户名不能为空 %>" Display="Dynamic" />
                                        <%=Resources.L.有权限访问该数据库的用户名 %>!
                                    </span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.数据库口令 %></span>
                                    <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" data-enter="3" CssClass="form-control text_300" />
                                    <asp:HiddenField runat="server" ID="TxtPassword_Hid" />
                                    <span class="control-label margin_l20">
                                        <asp:RequiredFieldValidator ID="ValrPassword" runat="server" ForeColor="Red" ControlToValidate="TxtPassWord" ErrorMessage="<%$Resources:L,用户口令不能为空%>" />
                                    </span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"></span>
                                    <span class="control-label text-center">
                                        <label>
                                            <input type="checkbox" runat="server" id="ignoreSql_chk" />
                                            <%=Resources.L.重置安装 %></label>
                                    </span>
                                    <span class="control-label margin_l20"></span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"></span>
                                    <div class="div300" style="margin-top: 15px;">
                                        <asp:Button ID="Step3_Pre_Btn" runat="server" OnClick="Step3_Pre_Btn_Click" Text="<%$Resources:L,上一步 %>" CssClass="btn btn-primary" UseSubmitBehavior="false" CausesValidation="false" />
                                        <asp:Button ID="Step3_Next_Btn" runat="server" data-enter="4" Text="<%$Resources:L,下一步 %>" CssClass="btn btn-primary" OnClick="Step3_Next_Btn_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" ID="WizardStep4" Title="">
                        <div class="container-fulid instcon">
                            <h1><%=Resources.L.配置网站信息 %></h1>
                            <div class="container text-left content_body" style="max-width: 740px; padding-left: 160px;">
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.网站名称 %></span>
                                    <asp:TextBox ID="TxtSiteName" runat="server" Text="<%$Resources:L,逐浪CMS %>" data-enter="0" class="form-control text_300" />
                                    <span class="control-label ">
                                        <asp:RequiredFieldValidator runat="server" ID="RS1" Display="Dynamic" ForeColor="Red" ControlToValidate="TxtSiteName" ErrorMessage="<%$Resources:L,网站名称不能为空 %>" />
                                    </span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.网站标题 %></span>
                                    <asp:TextBox ID="TxtSiteTitle" runat="server" Text="<%$Resources:L,逐浪CMS %>" data-enter="1" class="form-control text_300" />
                                    <span class="control-label ">
                                        <asp:RequiredFieldValidator runat="server" ID="RS2" Display="Dynamic" ForeColor="Red" ControlToValidate="TxtSiteTitle" ErrorMessage="<%$Resources:L,网站标题不能为空 %>" />
                                    </span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.网站地址 %></span>
                                    <asp:TextBox ID="TxtSiteUrl" runat="server" data-enter="2" class="form-control text_300" />
                                    <span class="control-label ">
                                        <asp:RequiredFieldValidator runat="server" ID="RS3" Display="Dynamic" ForeColor="Red" ControlToValidate="TxtSiteUrl" ErrorMessage="<%$Resources:L,网站地址不能为空 %>" />
                                    </span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.电子邮箱 %></span>
                                    <asp:TextBox ID="TxtEmail" runat="server" data-enter="3" class="form-control text_300" Text="web@z01.com" />
                                    <span class="control-label ">
                                        <asp:RequiredFieldValidator ID="RS4" runat="server" ForeColor="Red" ControlToValidate="TxtEmail" ErrorMessage="<%$Resources:L,Email不能为空 %>" Display="Dynamic" />
                                        <asp:RegularExpressionValidator ID="RS5" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:L,Email格式不正确 %>" ControlToValidate="TxtEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                    </span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.管理员名称 %></span>
                                    <asp:TextBox CssClass="form-control text_300" ID="TxtAdminName" runat="server" Enabled="False">admin</asp:TextBox>
                                    <a class="name_tips" href="javascript:;" data-toggle="popover" data-content="<%=Resources.L.如库中已有此管理员 %>."><span class="fa fa-info-circle"></span></a>
                                    <span class="control-label"></span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.管理员密码 %></span>
                                    <ZL:TextBox ID="TxtAdminPassword" runat="server" TextMode="Password" data-enter="4" class="form-control text_300" autofocus="autofocus" />
                                    <a class="name_tips" href="javascript:;" data-toggle="popover" data-content="<%=Resources.L.不少于6位字母数字组合 %>" tabindex="-1"><span class="fa fa-info-circle"></span></a>
                                    <span class="control-label">
                                        <asp:RegularExpressionValidator runat="server" ID="RS6" ForeColor="Red" ControlToValidate="TxtAdminPassword" ValidationExpression="^[^\s]{6,20}$" Display="Dynamic" ErrorMessage="密码少于六位" />
                                        <asp:RequiredFieldValidator runat="server" ID="ValrAdminPassword" ForeColor="Red" ControlToValidate="TxtAdminPassword" Display="Dynamic" ErrorMessage="<%$Resources:L,密码不能为空 %>" />
                                    </span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.确认密码 %></span>
                                    <asp:TextBox ID="TxtAdminPasswordAgain" runat="server" data-enter="5" TextMode="Password" class="form-control text_300" />
                                    <span class="control-label">
                                        <asp:RequiredFieldValidator runat="server" ID="RS7" ForeColor="Red" ErrorMessage="<%$Resources:L,确认密码不能为空 %>" ControlToValidate="TxtAdminPasswordAgain" Display="Dynamic" />
                                        <asp:CompareValidator runat="server" ID="RS8" Display="Dynamic" ForeColor="Red" ErrorMessage="<%$Resources:L,两次密码不相同 %>" ControlToCompare="TxtAdminPassword" ControlToValidate="TxtAdminPasswordAgain" />
                                    </span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.后台路径 %></span>
                                    <asp:TextBox ID="TxtCustomPath" runat="server" data-enter="6" class="form-control text_300" Text="Admin" MaxLength="10" onkeyup="value=value.replace(/[^\w]/ig,'');check(this);" Title="<%$Resources:L,请输入最少三到十位英文数字组合 %>" />
                                    <a class="name_tips" href="javascript:;" tabindex="-1" data-toggle="popover" data-content="<%=Resources.L.安装完成后可在后台进行配置 %>"><span class="fa fa-info-circle"></span></a>
                                    <span class="control-label">
                                        <asp:RequiredFieldValidator runat="server" ID="RS9" ForeColor="Red" ControlToValidate="TxtCustomPath" Display="None" ErrorMessage="<%$Resources:L,后台路径不能为空 %>!" />
                                        <asp:RegularExpressionValidator runat="server" ID="RS10" ForeColor="Red" ControlToValidate="TxtCustomPath" ValidationExpression="^[a-zA-Z0-9_\u4e00-\u9fa5\@\.]{3,10}$" Display="Dynamic" ErrorMessage="<%$Resources:L,后台路径不能少于三位 %>" />
                                    </span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"><%=Resources.L.管理认证码 %></span>
                                    <asp:TextBox ID="TxtSiteManageCode" runat="server" Text="8888" data-enter="7" class="form-control text_300" onkeyup="check2(this)" />
                                    <a class="name_tips" href="javascript:;" tabindex="-1" data-toggle="popover" data-content="<%=Resources.L.初始为不开通管理认证码功能 %>"><span class="fa fa-info-circle"></span></a>
                                    <span class="control-label">
                                        <asp:RequiredFieldValidator ID="ValrSiteManageCode" runat="server" ControlToValidate="TxtSiteManageCode" Display="None" ErrorMessage="<%$Resources:L,后台管理认证码不能为空 %>" />
                                    </span>
                                </div>
                                <div class="form-group">
                                    <span class="control-label"></span>
                                    <div class="div300">
                                        <asp:Button ID="Step4_Pre_Btn" runat="server" OnClick="Step4_Pre_Btn_Click" Text="<%$Resources:L,上一步 %>" CssClass="btn btn-primary" UseSubmitBehavior="false" CausesValidation="false" />
                                        <asp:Button ID="Step4_Next_Btn" data-enter="8" runat="server" Text="<%$Resources:L,下一步 %>" CssClass="btn btn-primary" OnClick="Step4_Next_Btn_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" ID="WizardStep5">
                        <div class="container-fulid instcon">
                            <h1><%=Resources.L.完成安装 %></h1>
                            <div class="container content_body">
                                <div class="control-label insok">
                                    <h2><a class="btn btn-primary btn-lg" href="/Default.aspx"><i class="fa fa-check"></i><%=Resources.L.已经成功安装点此进入首页 %></a><br />
                                        <span><%=Resources.L.重装请新定义配置文件 %></span>
                                    </h2>
                                </div>
                            </div>
                        </div>
                    </asp:WizardStep>
                </WizardSteps>
                <StepNavigationTemplate></StepNavigationTemplate>
                <StartNavigationTemplate></StartNavigationTemplate>
                <FinishNavigationTemplate></FinishNavigationTemplate>
            </asp:Wizard>
            <!--蓝色框结束-->
            <div class="container instside">
                <ul id="carousel_step" class="list-unstyled">
                    <li data-slide-to="1">
                        <div class="step_circle"></div>
                        <div><%=Resources.L.安装协议 %></div>
                    </li>
                    <li data-slide-to="2">
                        <div class="step_circle"></div>
                        <div><%=Resources.L.数据连接 %></div>
                    </li>
                    <li data-slide-to="3">
                        <div class="step_circle"></div>
                        <div><%=Resources.L.环境检测 %></div>
                    </li>
                    <li data-slide-to="4">
                        <div class="step_circle"></div>
                        <div><%=Resources.L.配置信息 %></div>
                    </li>
                    <li data-slide-to="5">
                        <div class="step_circle"></div>
                        <div><%=Resources.L.完成安装 %></div>
                    </li>
                </ul>
            </div>
        </div>
        <div style="display: none;">
            <asp:Button runat="server" ID="InstallDB_Btn" OnClick="InstallDB_Btn_Click" />
            <asp:HiddenField runat="server" ID="CurStep_Hid" Value="1" />
            <img id="bkpic_img" onerror="updateDefImg();" style="display: none;" />
        </div>
        <script src="/JS/ICMS/ZL_Common.js"></script>
        <script src="/JS/Controls/ZL_Dialog.js"></script>
        <script src="/JS/Controls/Control.js"></script>
        <script>
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
            //会员菜单更多显示/隐藏
            $("#more_btn").click(function (e) {
                if ($(".user_menu_sub").css("display") == "none") {
                    $(".user_menu_sub").slideDown();
                    $(this).find("i").removeClass("fa-angle-double-down");
                    $(this).find("i").addClass("fa-angle-double-up");
                }
                else {
                    $(".user_menu_sub").slideUp(200);
                    $(this).find("i").removeClass("fa-angle-double-up");
                    $(this).find("i").addClass("fa-angle-double-down");
                }
            });

            //脚本执行
            $(function () {
                updateStepFlag();
                Control.EnableEnter();
                $('[data-toggle="popover"]').popover();
            })
            function sureLicense() {
                var chk = document.getElementById("hasRead_chk");
                if (chk.checked) {  $("#Stpe1_Next_Btn").removeAttr('disabled'); }
                else { $("#Stpe1_Next_Btn").attr("disabled", "disabled"); }
            }
            function showlic() {
                var lic = document.getElementById("lic_content");
                lic.style.display = '';
                lic.focus();
            }
            function hidelic() { var lic = document.getElementById("lic_content"); lic.style.display = "none"; }
            //-----step2
            function installDB() {
                var waitDiag = new ZL_Dialog();
                waitDiag.closebtn = false;
                waitDiag.ShowMask("<%=Resources.L.正在执行SQL脚本请等待 %>");
        $("#InstallDB_Btn").click();
    }
    //-----step3
    function sqlselect() {
        var sel = $("#SqlVersion_DP").val().toLowerCase();
        switch (sel) {
            case "oracle":
                alert('<%=Resources.L.该版本仅对商业用户开放 %>');
                window.open('http://www.z01.com/corp/about/83.shtml', '', '');
                break;
            case "local":
                $("#TxtDataSource").val("(localdb)\\v11.0");
                $("#TxtDataBase").val("localdb");
                $("#TxtUserID").val("localdb");
                $("#TxtPassword").val("localdb");
                break;
            default:
                $("#TxtDataSource").val("(local)");
                $("#TxtDataBase").val("ZoomlaCMS");
                $("#TxtUserID").val("ZoomlaCMS");
                $("#TxtPassword").val("");
                break;
        }
    }
    //-----step4
    function check(obj) {
        if (obj.value.length > 9 || obj.value.length < 4) {
            Remind1.style.color = "red";
            Remind1.innerText = "<%=Resources.L.三到十位英文数字组合 %>";
        }
        else {
            Remind1.style.color = "#BFDFFF"
            Remind1.innerText = "*<%=Resources.L.安装完成后可在后台进行配置 %>";
        }
    }
    function check2(obj) {
        if (obj.value.length < 1) {
            Remind2.style.color = "red";
            Remind2.innerText = "<%=Resources.L.后台管理认证码不能为空 %>";
        }
        else {
            Remind2.style.color = "#BFDFFF"
            Remind2.innerText = "*<%=Resources.L.初始为不开通管理认证码功能 %>";
        }
    }
    //-----common
    function updateStepFlag() {
        var step = ConverToInt($("#CurStep_Hid").val(), 1);
        $("#carousel_step li[data-slide-to=" + step + "]").find(".step_circle").addClass("active");
        //更换背景图
        var imgurl = "https://code.z01.com/img/2016instbg_0" + step + ".jpg";
        $("#instbox").css("background-image", "url(" + imgurl + ")")
        $("#bkpic_img").attr("src", imgurl);
    }
    function updateDefImg() {
        var defurl = "/UploadFiles/demo/ad2.jpg";
        $("#instbox").css("background-image", "url(" + defurl + ")");
    }
    function showAlert(str) { alert(decodeURI(str)); }
        </script>
    </form>
</body>
</html>
