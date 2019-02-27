<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ADZone.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Plus.ADZone" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加广告版位</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <script src="../JS/Common.js" type="text/javascript"></script>     
</head>
<body>
    <form id="form1" runat="server">
        <iframe width="260" height="165" id="colorPalette" src="/common/setcolor.htm"
        style="visibility: hidden; position: absolute; border: 1px gray solid; left: 2px;
        top: 1px;" frameborder="0" scrolling="no"></iframe>
       <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>附件管理</span> &gt;&gt;<a href="ADZoneManage.aspx" title="广告版位管理">广告版位管理</a>&gt;&gt; <span>添加版位</span></div> 
        <div class="clearbox"></div>   

        <table border="0" cellpadding="2" cellspacing="1" class="border" width="100%">
            <tr>
                <td class="spacingtitle" colspan="2" style="height: 26px">
                    <strong><asp:Label ID="Label1" runat="server" Text="添加广告版位"></asp:Label></strong>
                </td>
            </tr>
            <tr>
                <td align="left" class="tdbgleft" style="width: 16%; height: 24px;">
                    <strong>版位名称：</strong></td>
                <td align="left"  class="tdbg" style="width: 650px; color: red; height: 24px;">
                    <asp:TextBox ID="TxtZoneName" runat="server" EnableViewState="False" MaxLength="100"
                        TextMode="SingleLine"></asp:TextBox>
                    &nbsp;*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtZoneName"
                        ErrorMessage="版位名称不能为空"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="left" class="tdbgleft" style="height: 24px">
                    <strong>生成JS文件名：</strong></td>
                <td align="left" class="tdbg"  style="color: red; width: 650px; height: 24px;">
                    <asp:TextBox ID="TxtZoneJSName" runat="server" EnableViewState="False" MaxLength="100"
                        TextMode="singleLine" ></asp:TextBox>
                    &nbsp;*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="JS文件名不能为空" ControlToValidate="TxtZoneJSName"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtZoneJSName"
                        Display="Dynamic" ErrorMessage="JS文件名不正确或者为空！" ValidationExpression="[0-9]{6}\/[0-9]{1,2}\.js"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td align="left" class="tdbgleft">
                    <strong>版位描述：</strong></td>
                <td align="left" class="tdbg"  style="width: 650px">
                    <asp:TextBox ID="TxtZoneIntro" runat="server" EnableViewState="False" Height="63px"
                        MaxLength="255" TextMode="multiline" Width="280px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="left" class="tdbgleft" style="height: 46px">
                    <strong>版位类型：</strong><br />
                    选择放置于此版位的广告类型。</td>
                <td  class="tdbg" style="height: 46px; width: 650px;">
                    <asp:RadioButtonList ID="radlZonetype" runat="server" AutoPostBack="false" EnableViewState="true" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="0" Selected="True">矩形横幅</asp:ListItem>
                        <asp:ListItem Value="1">弹出窗口</asp:ListItem>
                        <asp:ListItem Value="2">随屏移动</asp:ListItem>
                        <asp:ListItem Value="3">固定位置</asp:ListItem>
                        <asp:ListItem Value="4">漂浮移动</asp:ListItem>
                        <asp:ListItem Value="5">文字代码</asp:ListItem>
                        <asp:ListItem Value="6">对联广告</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td align="left" class="tdbgleft" >
                    <strong>版位位置：</strong><br />
                    对版位的详细参数进行设置。 <br />
               
                </td>
               <td align="left" class="tdbg"  style="width: 650px;">
                   <asp:RadioButtonList ID="RBLDefaultSetting" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                       <asp:ListItem Value="1" Selected="True">默认设置</asp:ListItem>
                       <asp:ListItem Value="0">自定义设置</asp:ListItem>
                   </asp:RadioButtonList>
                   <div id="ZoneTypeSetting" runat="server">
                    <table id="ZoneTypeSetting1" runat="server" border="0" cellpadding="2" cellspacing="1"
                        style="display: none">
                        <tr>
                            <td style="width: 144px">
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td colspan="2" align="center" style="height: 23px">
                                            此类型无版位参数设置！</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting2" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td>
                                            弹出方式：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropPopType" runat="server">
                                                <asp:ListItem Value="1">前置窗口</asp:ListItem>
                                                <asp:ListItem Value="2">后置窗口</asp:ListItem>
                                                <asp:ListItem Value="3">网页对话框</asp:ListItem>
                                                <asp:ListItem Value="4">背投广告</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            弹出位置：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropPopPosition" runat="server">
                                                <asp:ListItem Value="1" Text="左上" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="左下"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="右上"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="右下"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="PopLeft">左</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPopLeft" runat="server" Text="100" MaxLength="4" TextMode="SingleLine"
                                                Width="36px" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="PopTop">上</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPopTop" runat="server" Text="100" MaxLength="4" TextMode="singleLine"
                                                Width="36px" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            时间间隔：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPopCookieHour" Text="0" MaxLength="2" TextMode="singleLine" Width="36px"
                                                runat="server" />
                                            小时 在时间间隔内不重复弹出，设为0时总是弹出
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting3" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td>
                                            广告位置：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropMovePosition" runat="server">
                                                <asp:ListItem Value="1" Text="左上" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="左下"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="右上"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="右下"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="MoveLeft">左</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveLeft" MaxLength="4" Width="36px" Text="15" TextMode="singleLine"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="MoveTop">上</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveTop" MaxLength="4" Width="36px" Text="200" TextMode="singleLine"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            移动平滑度：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveDelay" MaxLength="7" Text="0.015" TextMode="singleLine" runat="server"
                                                Width="36px" />
                                            （取值在0.001至1之间）
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            是否显示关闭标签：
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlMoveShowCloseAD" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="true">是</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            关闭标签的颜色：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveCloseFontColor" runat="server"></asp:TextBox>
                                            <img src="../../images/<%if(TxtMoveCloseFontColor.Text=="") {%>rectNoColor.gif<%}else{%>rect.gif<%} %>" width="18" height="17" border="0" id="MoveCloseFontColorShow" alt="选取颜色" style="cursor: pointer;background-color: #<%=TxtMoveCloseFontColor.Text %>;" title="选取颜色!" onclick="GetColor($('MoveCloseFontColorShow'),'TxtMoveCloseFontColor');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting4" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td>
                                            广告位置：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropFixedPosition" runat="server">
                                                <asp:ListItem Value="1" Text="左上" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="左下"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="右上"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="右下"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="FixedLeft">左</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFixedLeft" MaxLength="4" Text="100" TextMode="singleLine" runat="server"
                                                Width="36px" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="FixedTop">上</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFixedTop" MaxLength="4" Text="100" TextMode="singleLine" Width="36px"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            是否显示关闭标签：
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlFixedShowCloseAD" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="true">是</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            关闭标签的颜色：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFixedCloseFontColor" runat="server"></asp:TextBox>
                                            <img src="../../images/<%if(TxtFixedCloseFontColor.Text=="") {%>rectNoColor.gif<%}else{%>rect.gif<%} %>" width="18" height="17" border="0" id="FixedCloseFontColorShow" alt="选取颜色" style="cursor: pointer;background-color: #<%=TxtFixedCloseFontColor.Text %>;" title="选取颜色!" onclick="GetColor($('FixedCloseFontColorShow'),'TxtFixedCloseFontColor');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting5" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td>
                                            漂浮类型：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropFloatType" runat="server">
                                                <asp:ListItem Value="1">变速漂浮</asp:ListItem>
                                                <asp:ListItem Value="2">匀速漂浮</asp:ListItem>
                                                <asp:ListItem Value="3">上下漂浮</asp:ListItem>
                                                <asp:ListItem Value="4">左右漂浮</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            开始位置：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropFloatPosition" runat="server">
                                                <asp:ListItem Value="1" Text="左上" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="左下"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="右上"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="右下"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="FloatLeft">左</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFloatLeft" MaxLength="4" Text="100" TextMode="singleLine" Width="36px"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="FloatTop">上</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFloatTop" MaxLength="4" Text="100" TextMode="singleLine" Width="36px"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            是否显示关闭标签：
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlFloatShowCloseAD" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="true">是</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            关闭标签的颜色：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFloatCloseFontColor" runat="server"></asp:TextBox>
                                            <img src="../../images/<%if(TxtFloatCloseFontColor.Text=="") {%>rectNoColor.gif<%}else{%>rect.gif<%} %>" width="18" height="17" border="0" id="FloatCloseFontColorShow" alt="选取颜色" style="cursor: pointer;background-color: #<%=TxtFloatCloseFontColor.Text %>;" title="选取颜色!" onclick="GetColor($('FloatCloseFontColorShow'),'TxtFloatCloseFontColor');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting6" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td align="center">
                                            此类型无版位参数设置！</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting7" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td>
                                            左右边距：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletLeft" MaxLength="4" Width="36px" Text="15" TextMode="singleLine"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            上边距：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletTop" MaxLength="4" Width="36px" Text="200" TextMode="singleLine"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            移动平滑度：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletDelay" MaxLength="7" Text="0.015" TextMode="singleLine"
                                                runat="server" Width="36px" />
                                            （取值在0.001至1之间）
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            是否显示关闭标签：
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlCoupletShowCloseAD" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="true">是</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            关闭标签的颜色：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletCloseFontColor" runat="server"></asp:TextBox>
                                            <img src="../../images/<%if(TxtFloatCloseFontColor.Text=="") {%>rectNoColor.gif<%}else{%>rect.gif<%} %>" width="18" height="17" border="0" id="CoupletCloseFontColorShow" alt="选取颜色" style="cursor: pointer;background-color: <%=TxtCoupletCloseFontColor.Text %>;" title="选取颜色!" onclick="GetColor($('CoupletCloseFontColorShow'),'TxtCoupletCloseFontColor');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                   
                     
               </td>
           </tr>
            <tr>
                <td align="left" class="tdbgleft">
                    <strong>版位尺寸：</strong><br />
                    IAB：互联网广告联合会标准尺寸。<br />
                    带*号的为新增加的标准广告尺寸。
                </td>
                <td align="left" class="tdbg"  style="width: 650px">
                    <table class="tdbg">
                        <tr>
                            <td  class="tdbg" style="width: 226px">
                                <asp:DropDownList ID="DropAdZoneSize" runat="server" AutoPostBack="false" EnableViewState="False">
                                    <asp:ListItem Value="468x60">IAB - 468 x 60 IMU (横幅广告)</asp:ListItem>
                                    <asp:ListItem Value="234x60">IAB - 234 x 60 IMU (半幅广告)</asp:ListItem>
                                    <asp:ListItem Value="88x31">IAB - 88 x 31 IMU (小按钮)</asp:ListItem>
                                    <asp:ListItem Value="120x90">IAB - 120 x 90 IMU (按钮一)</asp:ListItem>
                                    <asp:ListItem Value="120x60">IAB - 120 x 60 IMU (按钮二)</asp:ListItem>
                                    <asp:ListItem Value="728x90">IAB - 728 x 90 IMU (通栏广告) *</asp:ListItem>
                                    <asp:ListItem Value="120x240">IAB - 120 x 240 IMU (竖幅广告)</asp:ListItem>
                                    <asp:ListItem Value="125x125">IAB - 125 x 125 IMU (方形按钮)</asp:ListItem>
                                    <asp:ListItem Value="180x150">IAB - 180 x 150 IMU (长方形) *</asp:ListItem>
                                    <asp:ListItem Value="300x250">IAB - 300 x 250 IMU (中长方形) *</asp:ListItem>
                                    <asp:ListItem Value="336x280">IAB - 336 x 280 IMU (大长方形)</asp:ListItem>
                                    <asp:ListItem Value="240x400">IAB - 240 x 400 IMU (竖长方形)</asp:ListItem>
                                    <asp:ListItem Value="250x250">IAB - 250 x 250 IMU (正方形弹出)</asp:ListItem>
                                    <asp:ListItem Value="120x600">IAB - 120 x 600 IMU (摩天大楼)</asp:ListItem>
                                    <asp:ListItem Value="160x600">IAB - 160 x 600 IMU (宽摩天大楼) *</asp:ListItem>
                                    <asp:ListItem Value="300x600">IAB - 300 x 600 IMU (半页广告) *</asp:ListItem>
                                    <asp:ListItem Value="100x100">自定义大小</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 226px; height: 22px;" class="tdbg">
                                宽度：
                                <asp:TextBox ID="TxtZoneWidth" runat="server" MaxLength="4" Text="468" TextMode="singleLine"
                                    Width="24px"></asp:TextBox>
                                &nbsp; &nbsp; 高度：
                                <asp:TextBox ID="TxtZoneHeight" runat="server" MaxLength="4" Text="60" TextMode="singleLine"
                                    Width="24px"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="tdbgleft">
                    <strong>显示方式：</strong><br />
                    当版位中有多个广告时<br />
                    按照此设定进行<br />
                    显示（依据广告的权重）。</td>
                <td align="left" class="tdbg"  style="width: 650px">
                    <table class="tdbg">
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="RadlShowType" runat="server">
                                    <asp:ListItem Selected="True" Value="1">按权重随机显示</asp:ListItem>
                                    <asp:ListItem Value="2">按权重优先显示</asp:ListItem>
                                    <asp:ListItem Value="3">按顺序循环显示</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td>
                                &nbsp;</td>
                            <td style="color: blue">
                                [注意]<br />
                                1、按权重随机显示，权重越大显示机会越大。<br />
                                2、按权重优先显示，显示权重值最大的广告。<br />
                                3、按顺序循环显示，此方式仅对矩形横幅有效。</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="tdbgleft">
                    <strong>版位状态：</strong><br />
                    设为活动的版位才能在前台显示。</td>
                <td class="tdbg" style="height: 49px; width: 650px;">
                    <asp:CheckBox ID="ChkActive" runat="server" Checked="true" EnableViewState="False" />
                    活动版位
                </td>
            </tr>
        </table>
        <asp:Button ID="EBtnAdZone" runat="server" OnClick="EBtnAdZone_Click" Text="保存" />
        &nbsp;&nbsp; &nbsp;
        <input id="Cancel" class="inputbutton" name="Cancel" onclick="GoBack();" style="cursor: hand"
            type="button" value="取消" />        
        <asp:HiddenField ID="HdnZoneId" runat="server" />        
    </form>
    <script language="javascript" type="text/javascript">
    function GoBack()
    {
       window.location.href="ADZoneManage.aspx";
    }    
    function ShowZoenTypePanel()
    {
        if($('<%=radlZonetype.ClientID %>_0').checked==false)
        {
            $('<%=RadlShowType.ClientID %>_2').disabled = true; 
        }
        else
        {
            $('<%=RadlShowType.ClientID %>_2').disabled=false;
        }
        
        Zone_DisableSize($('<%=radlZonetype.ClientID %>_5').checked);
        for (var j=0;j<7;j++)
        {
            var ot = eval($('<%=ZoneTypeSetting.ClientID %>' + (j + 1)))
            if($('<%=radlZonetype.ClientID %>_'+j).checked && $('<%=RBLDefaultSetting.ClientID %>_1').checked)
            {
                ot.style.display = '';
                
            }else{ 
                ot.style.display = 'none';
            }
        } 
         
     }
     
     function Zone_DisableSize(value)
     {
        $('<%=TxtZoneWidth.ClientID %>').disabled=value;
        $('<%=TxtZoneHeight.ClientID %>').disabled=value;
        $('<%=DropAdZoneSize.ClientID %>').disabled=value;
     }
   
    function Zone_SelectSize(o)
    {
        size = o.options[o.selectedIndex].value;
        if (size != '0x0')
        {
            sarray = size.split('x');
            height = sarray.pop();
            width  = sarray.pop();
            $('<%=TxtZoneWidth.ClientID %>').value=width;
            $('<%=TxtZoneHeight.ClientID %>').value=height;
        }else{
            $('<%=TxtZoneHeight.ClientID %>').value=100;
            $('<%=TxtZoneWidth.ClientID %>').value=100;
        }
     }
    
    function ChangePositonShow(drop)
    {
        var text=drop.options[drop.options.selectedIndex].text;
        var name=drop.id.replace("Drop","");
        name=name.replace("Position","");
        $(name+"Left").innerHTML=text.substring(0,1);
        $(name+"Top").innerHTML=text.substring(1,2);
    }
    ShowZoenTypePanel();
    </script>
</body>

</html>
