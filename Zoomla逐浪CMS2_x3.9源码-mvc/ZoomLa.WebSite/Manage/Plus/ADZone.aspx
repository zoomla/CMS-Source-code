<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADZone.aspx.cs" Inherits="ZoomLaCMS.Manage.Plus.ADZone" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加广告版位</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <iframe width="260" height="165" id="colorPalette" src="/common/setcolor.htm" style="visibility: hidden; position: absolute; border: 1px gray solid; left: 2px; top: 1px;" frameborder="0" scrolling="no"></iframe>
    <table class="table table-bordered table-striped">
        <tr>
            <td class="spacingtitle" colspan="2" style="height: 26px; text-align: center;">
                <strong>
                    <asp:Label ID="Label1" runat="server" Text="添加广告版位"></asp:Label>
                </strong>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 300px;"><strong>版位名称：</strong></td>
            <td align="left">
                <asp:TextBox ID="TxtZoneName" class="form-control text_300" runat="server" MaxLength="100" TextMode="SingleLine"></asp:TextBox>&nbsp;*
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtZoneName" ErrorMessage="版位名称不能为空"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left" style="height: 24px; width: 200px"><strong>生成JS文件名：</strong></td>
            <td align="left" style="color: red; height: 24px;">
                <asp:TextBox ID="TxtZoneJSName" class="form-control text_300" runat="server" EnableViewState="False" MaxLength="100" TextMode="singleLine"></asp:TextBox>
                &nbsp;*
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="JS文件名不能为空" ControlToValidate="TxtZoneJSName"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtZoneJSName" Display="Dynamic" ErrorMessage="JS文件名不正确或者为空！" ValidationExpression="[0-9]{6}\/[0-9]{1,4}\.js"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 200px;"><strong>版位描述：</strong></td>
            <td align="left">
                <asp:TextBox ID="TxtZoneIntro" class="form-control text_300" runat="server" EnableViewState="False" Height="63px" MaxLength="255" TextMode="multiline" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" style="height: 46px; width: 200px"><strong>版位类型：</strong><br />
                选择放置于此版位的广告类型。</td>
            <td style="height: 46px;">
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
            <td align="left" style="width: 200px;"><strong>版位位置：</strong><br />
                对版位的详细参数进行设置。
                <br />
            </td>
            <td align="left">
                <asp:RadioButtonList ID="RBLDefaultSetting" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                    <asp:ListItem Value="1" Selected="True">默认设置</asp:ListItem>
                    <asp:ListItem Value="0">自定义设置</asp:ListItem>
                </asp:RadioButtonList>
                <div id="ZoneTypeSetting" runat="server">
                    <table id="ZoneTypeSetting1" runat="server" border="0" cellpadding="2" cellspacing="1" style="display: none">
                        <tr>
                            <td>
                                <table class="table table-striped table-bordered">
                                    <tr>
                                        <td colspan="2" align="center" style="height: 23px">此类型无版位参数设置！</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting2" runat="server" border="0" cellpadding="0" cellspacing="0" style="display: none">
                        <tr>
                            <td>
                                <table class="table table-striped table-bordered">
                                    <tr>
                                        <td>弹出方式： </td>
                                        <td>
                                            <asp:DropDownList ID="DropPopType" CssClass="form-control text_md" Width="150" runat="server">
                                                <asp:ListItem Value="1">前置窗口</asp:ListItem>
                                                <asp:ListItem Value="2">后置窗口</asp:ListItem>
                                                <asp:ListItem Value="3">网页对话框</asp:ListItem>
                                                <asp:ListItem Value="4">背投广告</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>弹出位置： </td>
                                        <td>
                                            <asp:DropDownList ID="DropPopPosition" CssClass="form-control text_md" Width="150" runat="server">
                                                <asp:ListItem Value="1" Text="左上" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="左下"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="右上"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="右下"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><span id="PopLeft">左</span>： </td>
                                        <td>
                                            <asp:TextBox ID="TxtPopLeft" CssClass="form-control text_md" runat="server" Text="100" MaxLength="4" TextMode="SingleLine" Width="80" /></td>
                                    </tr>
                                    <tr>
                                        <td><span id="PopTop">上</span>： </td>
                                        <td>
                                            <asp:TextBox ID="TxtPopTop" CssClass="form-control text_md" runat="server" Text="100" MaxLength="4" TextMode="singleLine" Width="80" /></td>
                                    </tr>
                                    <tr>
                                        <td>时间间隔： </td>
                                        <td>
                                            <asp:TextBox ID="TxtPopCookieHour" Text="0" MaxLength="2" TextMode="singleLine" Width="36px" runat="server" />
                                            小时 在时间间隔内不重复弹出，设为0时总是弹出 </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting3" runat="server" border="0" cellpadding="0" cellspacing="0" style="display: none">
                        <tr>
                            <td>
                                <table class="table table-striped table-bordered">
                                    <tr>
                                        <td>广告位置： </td>
                                        <td>
                                            <asp:DropDownList ID="DropMovePosition" CssClass="form-control text_md" Width="150" runat="server">
                                                <asp:ListItem Value="1" Text="左上" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="左下"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="右上"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="右下"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><span id="MoveLeft">左</span>： </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveLeft" CssClass="form-control text_md" MaxLength="4" Width="80" Text="15" TextMode="singleLine" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td><span id="MoveTop">上</span>： </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveTop" CssClass="form-control text_md" MaxLength="4" Width="80" Text="200" TextMode="singleLine" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td>移动平滑度： </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveDelay" CssClass="form-control text_md" MaxLength="7" Text="0.015" TextMode="singleLine" runat="server" Width="80" />
                                            （取值在0.001至1之间） 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>是否显示关闭标签： </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlMoveShowCloseAD" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Value="true">是</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                        <td>关闭标签的颜色： </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveCloseFontColor" class="form-control text_md" runat="server"></asp:TextBox>
                                            <img src="/images/<%=string.IsNullOrEmpty(TxtMoveCloseFontColor.Text) ? "rectNoColor.gif" : "rect.gif" %>"
                                                width="18" height="17" border="0" id="MoveCloseFontColorShow" alt="选取颜色" style="cursor: pointer;" title="选取颜色!" onclick="GetColor($('MoveCloseFontColorShow'),'TxtMoveCloseFontColor');" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting4" runat="server" border="0" cellpadding="0" cellspacing="0" style="display: none">
                        <tr>
                            <td>
                                <table class="table table-striped table-bordered">
                                    <tr>
                                        <td>广告位置： </td>
                                        <td>
                                            <asp:DropDownList ID="DropFixedPosition" CssClass="form-control text_md" Width="150" runat="server">
                                                <asp:ListItem Value="1" Text="左上" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="左下"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="右上"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="右下"></asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td><span id="FixedLeft">左</span>： </td>
                                        <td>
                                            <asp:TextBox ID="TxtFixedLeft" CssClass="form-control text_md" MaxLength="4" Text="100" TextMode="singleLine" runat="server" Width="80" /></td>
                                    </tr>
                                    <tr>
                                        <td><span id="FixedTop">上</span>： </td>
                                        <td>
                                            <asp:TextBox ID="TxtFixedTop" CssClass="form-control text_md" MaxLength="4" Text="100" TextMode="singleLine" Width="80" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td>是否显示关闭标签： </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlFixedShowCloseAD" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Value="true">是</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                        <td>关闭标签的颜色： </td>
                                        <td>
                                            <asp:TextBox ID="TxtFixedCloseFontColor" class="form-control text_md" runat="server"></asp:TextBox>
                                            <img src="/images/<%=string.IsNullOrEmpty(TxtMoveCloseFontColor.Text) ? "rectNoColor.gif" : "rect.gif" %>"
                                                width="18" height="17" border="0" id="FixedCloseFontColorShow" alt="选取颜色" style="cursor: pointer; background-color: #<%=TxtFixedCloseFontColor.Text %>;" title="选取颜色!" onclick="GetColor($('FixedCloseFontColorShow'),'TxtFixedCloseFontColor');" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting5" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table class="table table-striped table-bordered">
                                    <tr>
                                        <td>漂浮类型： </td>
                                        <td>
                                            <asp:DropDownList ID="DropFloatType" CssClass="form-control text_md" Width="150" runat="server">
                                                <asp:ListItem Value="1">变速漂浮</asp:ListItem>
                                                <asp:ListItem Value="2">匀速漂浮</asp:ListItem>
                                                <asp:ListItem Value="3">上下漂浮</asp:ListItem>
                                                <asp:ListItem Value="4">左右漂浮</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>开始位置： </td>
                                        <td>
                                            <asp:DropDownList ID="DropFloatPosition" CssClass="form-control text_md" Width="150" runat="server">
                                                <asp:ListItem Value="1" Text="左上" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="左下"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="右上"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="右下"></asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td><span id="FloatLeft">左</span>： </td>
                                        <td>
                                            <asp:TextBox ID="TxtFloatLeft" CssClass="form-control text_md" MaxLength="4" Text="100" TextMode="singleLine" Width="80" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td><span id="FloatTop">上</span>： </td>
                                        <td>
                                            <asp:TextBox ID="TxtFloatTop" CssClass="form-control text_md" MaxLength="4" Text="100" TextMode="singleLine" Width="80" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td>是否显示关闭标签： </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlFloatShowCloseAD" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Value="true">是</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                        <td>关闭标签的颜色： </td>
                                        <td>
                                            <asp:TextBox ID="TxtFloatCloseFontColor" class="form-control text_md" runat="server"></asp:TextBox>
                                      <img src="/images/<%=string.IsNullOrEmpty(TxtMoveCloseFontColor.Text) ? "rectNoColor.gif" : "rect.gif" %>"
                                                width="18" height="17" border="0" id="FloatCloseFontColorShow" alt="选取颜色" style="cursor: pointer; background-color: #<%=TxtFloatCloseFontColor.Text %>;" title="选取颜色!" onclick="GetColor($('FloatCloseFontColorShow'),'TxtFloatCloseFontColor');" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting6" runat="server" border="0" cellpadding="0" cellspacing="0" style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td align="center">此类型无版位参数设置！</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting7" runat="server" border="0" cellpadding="0" cellspacing="0" style="display: none">
                        <tr>
                            <td>
                                <table class="table table-striped table-bordered">
                                    <tr>
                                        <td>左右边距： </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletLeft" CssClass="form-control text_md" MaxLength="4" Width="80" Text="15" TextMode="singleLine" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td>上边距： </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletTop" CssClass="form-control text_md" MaxLength="4" Width="80" Text="200" TextMode="singleLine" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td>移动平滑度： </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletDelay" CssClass="form-control text_md" MaxLength="7" Text="0.015" TextMode="singleLine" runat="server" Width="80" />
                                            （取值在0.001至1之间） </td>
                                    </tr>
                                    <tr>
                                        <td>是否显示关闭标签： </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlCoupletShowCloseAD" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Value="true">是</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                        <td>关闭标签的颜色： </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletCloseFontColor" CssClass="form-control text_md" runat="server" Width="100"></asp:TextBox>
                                            <img src="/images/<%=string.IsNullOrEmpty(TxtMoveCloseFontColor.Text) ? "rectNoColor.gif" : "rect.gif" %>"
                                                width="18" height="17" border="0" id="CoupletCloseFontColorShow" alt="选取颜色" style="cursor: pointer; background-color: <%=TxtCoupletCloseFontColor.Text %>;" title="选取颜色!" onclick="GetColor($('CoupletCloseFontColorShow'),'TxtCoupletCloseFontColor');" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 200px;"><strong>版位尺寸：</strong><br />
                IAB：互联网广告联合会标准尺寸。<br />
                带*号的为新增加的标准广告尺寸。 </td>
            <td align="left">
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DropAdZoneSize" CssClass="form-control text_300" runat="server" AutoPostBack="false" EnableViewState="False">
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
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 226px; height: 22px;">
                            宽度：<asp:TextBox ID="TxtZoneWidth" runat="server" CssClass="form-control" MaxLength="4" Text="468" TextMode="singleLine" Width="256"></asp:TextBox><br />
                            高度：<asp:TextBox ID="TxtZoneHeight" CssClass="form-control" runat="server" MaxLength="4" Text="60" TextMode="singleLine" Width="256"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 200px;"><strong>显示方式：</strong><br />
                当版位中有多个广告时<br />
                按照此设定进行<br />
                显示（依据广告的权重）。</td>
            <td align="left">
                <table>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="RadlShowType" runat="server">
                                <asp:ListItem Selected="True" Value="1">按权重随机显示</asp:ListItem>
                                <asp:ListItem Value="2">按权重优先显示</asp:ListItem>
                                <asp:ListItem Value="3">按顺序循环显示</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td>&nbsp;</td>
                        <td style="color: blue">[注意]<br />
                            1、按权重随机显示，权重越大显示机会越大。<br />
                            2、按权重优先显示，显示权重值最大的广告。<br />
                            3、按顺序循环显示，此方式仅对矩形横幅有效。</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 200px;"><strong>版位状态：</strong><br />
                设为活动的版位才能在前台显示。</td>
            <td style="height: 49px;">
                <asp:CheckBox ID="ChkActive" runat="server" Checked="true" EnableViewState="False" />
                活动版位 </td>
        </tr>
        <tr>
            <td align="left" style="width: 200px;"><strong>是否开放用户申请：</strong></td>
            <td style="height: 49px;">
                <asp:CheckBox ID="CheckApply" runat="server" />
                是 </td>
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                <asp:Button ID="EBtnAdZone" runat="server" OnClick="EBtnAdZone_Click" Text="保存" class="btn btn-primary" />
                <input id="Cancel" name="Cancel" onclick="GoBack();" style="cursor: pointer" type="button" value="取消" class="btn btn-primary" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnZoneId" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function GoBack() {
            window.location.href = "ADZoneManage.aspx";
        }
        function ShowZoenTypePanel()
        {
            //版位类型为矩位横幅,才可选按顺序循环
            if ($("#radlZonetype_0")[0].checked == false)
            {
                $("#RadlShowType_2").attr("disabled", "disabled");
            }
            else
            {
                $("#RadlShowType_2").removeAttr("disabled");
            }
            Zone_DisableSize($("#radlZonetype_5")[0].checked == true);
            for (var j = 0; j < 7; j++)
            {
                var ot = $("#ZoneTypeSetting" + (j + 1));
                //版位类型,并且版位位置为[自定义设置],显示对应的表格
                if ($("input[type=radio][id=radlZonetype_" + j + "]")[0].checked==true &&$("input[type=radio][id=RBLDefaultSetting_1]")[0].checked==true)
                {
                    ot.css("display", "");
                }
                else
                {
                    ot.css("display", "none");
                }
            }
        }
        function Zone_DisableSize(value)
        {
            if (!value)
            {
                $("#TxtZoneWidth").removeAttr("disabled");
                $("#TxtZoneHeight").removeAttr("disabled");
                $("#DropAdZoneSize").removeAttr("disabled");
                
            }
            else
            {
                $("#TxtZoneWidth").attr("disabled", "disabled");
                $("#TxtZoneHeight").attr("disabled", "disabled");
                $("#DropAdZoneSize").attr("disabled", "disabled");
            }
        }
        function Zone_SelectSize(o) {
            size = o.options[o.selectedIndex].value;
            if (size != '0x0') {
                sarray = size.split('x');
                height = sarray.pop();
                width = sarray.pop();
                $("#TxtZoneWidth").val(width);
                $("#TxtZoneHeight").val(height);
            } else {
                $("#TxtZoneWidth").val(100);
                $("#TxtZoneHeight").val(100);
            }
        }
        function ChangePositonShow(drop) {
            var text = drop.options[drop.options.selectedIndex].text;
            var name = drop.id.replace("Drop", "");
            name = name.replace("Position", "");
            $(name + "Left").innerHTML = text.substring(0, 1);
            $(name + "Top").innerHTML = text.substring(1, 2);
        }
        ShowZoenTypePanel();
    </script>
</asp:Content>
