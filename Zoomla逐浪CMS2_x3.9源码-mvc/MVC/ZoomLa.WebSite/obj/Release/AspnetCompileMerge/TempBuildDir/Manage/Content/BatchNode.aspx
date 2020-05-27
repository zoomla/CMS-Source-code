<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchNode.aspx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.BatchNode" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/TlpDP.ascx" TagPrefix="ZL" TagName="TlpDown" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
    <script src="/dist/js/bootstrap-switch.js"></script>
    <title>节点批量设置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="foo" style="position: fixed; top: 50%; left: 30%; display: block;"></div>
    <table class="table table-striped table-bordered">
        <tr style="text-align: center;">
            <td><span style="color: green; font-family: 'Microsoft YaHei';" id="curNode_span">当前节点:尚未选定</span></td>
            <td>批量设置节点</td>
        </tr>
        <tr>
            <td valign="top" align="center" style="width: 250px">
                <table>
                    <tr>
                        <td align="left">
                            <span style="color: Red">提示：</span>可以按住“Shift”<br />
                            或“Ctrl”键进行多个节点的选择
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:ListBox ID="LstNodes" runat="server" DataTextField="NodeName" CssClass="form-control" DataValueField="NodeId" SelectionMode="Multiple" Height="500px" Width="100%"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <input id="BtnSelectAll" onclick="SelectAll()" type="button" class="btn btn-primary" value="选定所有节点" />
                            <input id="BtnCancelAll" onclick="UnSelectAll()" type="button" class="btn btn-primary" value="取消所有节点" />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <ul class="nav nav-tabs">
                    <li class="active"><a href="#Tabs0" data-toggle="tab">栏目选项</a></li>
                    <li><a href="#Tabs1" data-toggle="tab">模板选项</a></li>
                    <li><a href="#Tabs2 " data-toggle="tab">收费设置</a></li>
                    <li><a href="#Tabs3 " data-toggle="tab">生成选项</a></li>
                    <li><a href="#Tabs4 " data-toggle="tab">浏览权限</a></li>
                    <li><a href="#Tabs5 " data-toggle="tab">操作权限</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="Tabs0">
                        <table class="table table-striped table-bordered table-hover">
                            <tbody>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkOpenType" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>打开方式：</strong>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RBLOpenType" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="_self">原窗口打开</asp:ListItem>
                                            <asp:ListItem Value="_blank">新窗口打开</asp:ListItem>
                                            <asp:ListItem Value="_parent">父窗口打开</asp:ListItem>
                                            <asp:ListItem Value="_top">全局载入</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkItemOpen" runat="server" />
                                    </td>
                                    <td style="text-align: right" width="20%">
                                        <strong>内容打开方式：</strong>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RBLItemOpenType" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="_self">原窗口打开</asp:ListItem>
                                            <asp:ListItem Value="_blank">新窗口打开</asp:ListItem>
                                            <asp:ListItem Value="_parent">父窗口打开</asp:ListItem>
                                            <asp:ListItem Value="_top">全局载入</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkPurview" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>栏目权限：</strong>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RBLPurviewType" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">开放</asp:ListItem>
                                            <asp:ListItem Value="1">认证</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkComment" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>评论权限：</strong>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RBLCommentType" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">关闭评论</asp:ListItem>
                                            <asp:ListItem Value="1">允许评论</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkClickTimeout" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>点击统计时间：</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ClickTimeout" CssClass="form-control" style="max-width:80px;" runat="server">
                                            <asp:ListItem Text="1秒" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="30秒" Value="30"></asp:ListItem>
                                            <asp:ListItem Text="1分钟" Value="60"></asp:ListItem>
                                            <asp:ListItem Text="5分钟" Value="300"></asp:ListItem>
                                            <asp:ListItem Text="10分钟" Value="600"></asp:ListItem>
                                            <asp:ListItem Text="半小时" Value="1800"></asp:ListItem>
                                            <asp:ListItem Text="1小时" Value="3600"></asp:ListItem>
                                            <asp:ListItem Text="8小时" Value="28800"></asp:ListItem>
                                            <asp:ListItem Text="24小时" Value="86400"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-center">
                                        <asp:CheckBox ID="ChkHits" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>本栏目热点的点击数最小值：</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtHitsOfHot" class="form-control pull-left" style="max-width:80px;" runat="server" Columns="5">0</asp:TextBox>
                                        <asp:RangeValidator ID="ValgHitsOfHot" Display="Dynamic" CssClass="tips" runat="server" ControlToValidate="TxtHitsOfHot" ErrorMessage="请输入整数" MinimumValue="0" MaximumValue="2147483647" Type="Integer" SetFocusOnError="True"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-center"><asp:CheckBox ID="Bat_SafeGuard_Chk" runat="server" /></td>
                                    <td class="text-right"><strong>安全防护：</strong></td>
                                    <td><input runat="server" type="checkbox" id="SafeGuard" class="switchChk" /></td>
                                </tr>
                                <tr>
                                    <td class="text-center"><asp:CheckBox ID="Bat_isSimple_Chk" runat="server" /></td>
                                    <td class="text-right"><strong>简洁模式：</strong></td>
                                    <td><input type="checkbox" runat="server" id="isSimple_CH" class="switchChk" /></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane" id="Tabs1">
                        <table class="table table-striped table-bordered table-hover">
                            <%--模板选项--%>
                            <tbody>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkITemp" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>栏目首页模板：</strong>
                                    </td>
                                    <td>
                                        <%=PageCommon.GetTlpDP("TxtIndexTemplate") %>
                                        <asp:HiddenField ID="TxtIndexTemplate_hid" runat="server" />
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkTemp" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>栏目列表页模板：</strong>
                                    </td>
                                    <td>
                                        <%=PageCommon.GetTlpDP("TxtTemplate") %>
                                        <asp:HiddenField ID="TxtTemplate_hid" runat="server" />
                                    </td>
                                </tr>

                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="Chk1Temp" runat="server" />
                                    </td>

                                    <td style="text-align: right">
                                        <strong>最新信息模板：</strong>
                                    </td>
                                    <td>
                                        <%=PageCommon.GetTlpDP("LastinfoTemplate") %>
                                        <asp:HiddenField ID="LastinfoTemplate_hid" runat="server" />
                                        
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="Chk2Temp" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>热门信息模板：</strong>
                                    </td>
                                    <td>
                                        <%=PageCommon.GetTlpDP("HotinfoTemplate") %>
                                        <asp:HiddenField ID="HotinfoTemplate_hid" runat="server" />
                                        <%--<asp:TextBox ID="HotinfoTemplate" class="form-control" MaxLength="255" runat="server" Columns="50"></asp:TextBox>--%>
                                        
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="Chk3Temp" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>推荐信息模板：</strong>
                                    </td>
                                    <td>
                                        <%=PageCommon.GetTlpDP("ProposeTemplate") %>
                                        <asp:HiddenField ID="ProposeTemplate_hid" runat="server" />
                                        
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkModelID" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>选择内容模型:</strong>
                                    </td>
                                    <td id="model_td">
                                        <asp:HiddenField ID="HdnModeID" runat="server" />
                                        <table class="table table-bordered table-striped">
                                            <asp:Repeater ID="Model_RPT" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width: 150px">
                                                            <label>
                                                                <input type="checkbox" id="ChkModel" name="ChkModel" value="<%# Eval("ModelID") %>" />
                                                                <i class="<%#Eval("ItemIcon") %>"></i><span><%# Eval("ModelName") %></span>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <%#PageCommon.GetTlpDP("TxtModelTemplate_"+Eval("ModelID")) %>
                                                            <input type="hidden" id="TxtModelTemplate_<%# Eval("ModelID") %>_hid" name="TxtModelTemplate_<%# Eval("ModelID") %>" value="<%# GetTemplate(Eval("ModelID","{0}")) %>" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane" id="Tabs2">
                        <table class="table table-striped table-bordered table-hover">
                            <%--收费设置--%>
                            <tbody>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkConsumePoint" runat="server" />
                                    </td>
                                    <td>
                                        <strong>默认消费点券数：</strong><br />
                                        注：会员在此栏目下阅读内容时，该内容默认的收费点券数
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtConsumePoint" class="form-control pull-left" style="max-width:80px;" runat="server" Columns="5">0</asp:TextBox>
                                        <span class="tips">点</span>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChConsumeType" runat="server" />
                                    </td>
                                    <td>
                                        <strong>重复收费方式：</strong><br />
                                        注：会员在此栏目下阅读内容时，该内容的重复收费方式
                                    </td>
                                    <td>
                                        <input id="Radio1" name="ConsumeType" runat="server" value="0" type="radio" />不重复收费<div class="clearfix"></div>
                                        <span class="tips pull-left" style="margin-left:0;"><input id="Radio2" name="ConsumeType" runat="server" value="1" type="radio" />距离上次收费时间</span>
                                        <asp:TextBox ID="TxtConsumeTime" class="form-control pull-left" style="max-width:60px;" runat="server" Columns="5">0</asp:TextBox><span class="tips">小时后重新收费</span><div class="clearfix"></div>
                                        <span class="tips pull-left" style="margin-left:0;"><input id="Radio3" name="ConsumeType" runat="server" value="2" type="radio" />会员重复阅读此文章</span>
                                        <asp:TextBox ID="TxtConsumeCount" class="form-control pull-left" style="max-width:60px;" runat="server" Columns="5">0</asp:TextBox><span class="tips">次后重新收费</span><div class="clearfix"></div>
                                        <input id="Radio4" name="ConsumeType" runat="server" value="3" type="radio" />上述两者都满足时重新收费<div class="clearfix"></div>
                                        <input id="Radio5" name="ConsumeType" runat="server" value="4" type="radio" />上述两者任一个满足时就重新收费<div class="clearfix"></div>
                                        <input id="Radio6" name="ConsumeType" runat="server" value="5" type="radio" />每阅读一次就重复收费一次（建议不要使用）<div class="clearfix"></div>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkBGroup" runat="server" />
                                    </td>
                                    <td>
                                        <strong>可浏览篇数：</strong><br />
                                        注：会员在此栏目下查看内容时,该节点可浏览篇数
                                    </td>
                                    <td id="BGroups">
                                        <asp:Repeater ID="BGroup" runat="server">
                                            <ItemTemplate>
                                                <span style="width: 70px; display: block; float: left" class="tips"><%# Eval("GroupName")%> </span>
                                                <input type="hidden" value='<%# Eval("GroupID") %>' />
                                                <asp:TextBox ID="CNode1" class="form-control pull-left" style="max-width:60px;" runat="server" Text='<%# GetViewVl(Eval("GroupID").ToString()) %>' Columns="5"></asp:TextBox><span class="tips">篇</span><div class="clearfix"></div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkShares" runat="server" />
                                    </td>
                                    <td>
                                        <strong>分成比例：</strong><br />
                                        注：会员在此栏目下添加内容时，该内容默认的分成比例
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtShares" class="form-control pull-left" style="max-width:60px;" runat="server" Columns="5">0</asp:TextBox>
                                        <span class="tips">%</span>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkPay" runat="server" />
                                    </td>
                                    <td style="width: 288px">
                                        <strong>发布内容收费：</strong><br />
                                        注：会员在此栏目下发布内容时，每发布一篇内容收费
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtAddMoney" class="form-control pull-left" style="max-width:60px;" runat="server" Columns="8">0</asp:TextBox>
                                        <span class="tips">元</span><div class="clearfix"></div>
                                        <asp:TextBox ID="TxtAddPoint" class="form-control pull-left" style="max-width:60px;" runat="server" Columns="8">0</asp:TextBox>
                                        <span class="tips">点券</span><div class="clearfix"></div>
                                        <asp:TextBox ID="txtAddExp" class="form-control pull-left" style="max-width:60px;" runat="server" Columns="8">0</asp:TextBox>
                                        <span class="tips">积分</span><div class="clearfix"></div>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkDeducExp" runat="server" />
                                    </td>
                                    <td style="width: 288px">
                                        <strong>下载内容收费：</strong><br />
                                        注：会员在此栏目下载内容时，每下载一次所需费用
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDeducExp" class="form-control pull-left" style="max-width:60px;" runat="server" Columns="8">0</asp:TextBox>
                                        <span class="tips">积分</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane" id="Tabs3">
                        <table class="table table-striped table-bordered table-hover">
                            <%--生成选项--%>
                            <tbody>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkListEx" runat="server" />
                                    </td>
                                    <td style="text-align: right" width="20%">
                                        <strong>列表首页扩展名：</strong>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RBLListEx" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">.html</asp:ListItem>
                                            <asp:ListItem Value="1">.htm</asp:ListItem>
                                            <asp:ListItem Value="2">.shtml</asp:ListItem>
                                            <asp:ListItem Value="3">.aspx</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkListPageEx" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>栏目列表页面扩展名：</strong>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="ListPageEx" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">.html</asp:ListItem>
                                            <asp:ListItem Value="1">.htm</asp:ListItem>
                                            <asp:ListItem Value="2">.shtml</asp:ListItem>
                                            <asp:ListItem Value="3">.aspx</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkLastinfoPageEx" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>最新信息页扩展名：</strong>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="LastinfoPageEx" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">.html</asp:ListItem>
                                            <asp:ListItem Value="1">.htm</asp:ListItem>
                                            <asp:ListItem Value="2">.shtml</asp:ListItem>
                                            <asp:ListItem Value="3">.aspx</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkHotinfoPageEx" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>热门信息页扩展名：</strong>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="HotinfoPageEx" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">.html</asp:ListItem>
                                            <asp:ListItem Value="1">.htm</asp:ListItem>
                                            <asp:ListItem Value="2">.shtml</asp:ListItem>
                                            <asp:ListItem Value="3">.aspx</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkProposePageEx" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>推荐信息扩展名：</strong>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="ProposePageEx" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">.html</asp:ListItem>
                                            <asp:ListItem Value="1">.htm</asp:ListItem>
                                            <asp:ListItem Value="2">.shtml</asp:ListItem>
                                            <asp:ListItem Value="3">.aspx</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkContentEx" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>内容页扩展名：</strong>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RBLContentEx" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">.html</asp:ListItem>
                                            <asp:ListItem Value="1">.htm</asp:ListItem>
                                            <asp:ListItem Value="2">.shtml</asp:ListItem>
                                            <asp:ListItem Value="3">.aspx</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkPosition" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>节点目录生成位置：</strong>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RBLPosition" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">根目录下</asp:ListItem>
                                            <asp:ListItem Value="1">继承父节点目录</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="ChkContentRule" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>内容页文件名规则：</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDLContentRule" CssClass=" form-control" runat="server" Style="margin: -2px; max-width: 150px;">
                                            <asp:ListItem Selected="True" Value="2">栏目目录/InfoID</asp:ListItem>
                                            <asp:ListItem Value="1">栏目目录/年-月/InfoID</asp:ListItem>
                                            <asp:ListItem Value="0">栏目目录/年/月/日/infoid</asp:ListItem>
                                            <asp:ListItem Value="3">栏目目录/年月日/标题</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </tbody>
                            
                        </table>
                    </div>
                    <div class="tab-pane" id="Tabs4">
                        <table class="table table-striped table-bordered table-hover">
                            <%--浏览权限--%>
                            <tbody>
                                <tr >
                                    <td style="width: 50px; text-align: center" rowspan="5">
                                        <asp:CheckBox ID="ChkUserView" runat="server" />
                                    </td>
                                    <td style="width: 288px">
                                        <strong>浏览/查看权限：</strong><br />
                                        栏目权限为继承关系。例如：当父栏目设为“认证栏目”时，子栏目的权限设置将继承父栏目设置，即使子栏目设为“开放栏目”也无效。相反，如果父栏目设为“开放栏目”，子栏目可以设为“半开放栏目”或“认证栏目”。
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="UserAuit_RadioList" runat="server">
                                            <asp:ListItem Text="开放栏目 任何人（包括游客）可以浏览和查看此栏目下的信息。" Value="allUser" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="半开放栏目 任何人（包括游客）都可以浏览。游客不可查看，其他会员根据会员组的栏目权限设置决定是否可以查看。" Value="moreUser"></asp:ListItem>
                                            <asp:ListItem Text="认证栏目 游客不能浏览和查看，其他会员根据会员组的栏目权限设置决定是否可以浏览和查看。" Value="onlyUser"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr >

                                    <td style="width: 288px">
                                        <strong>允许浏览此栏目的会员组：</strong><br />
                                        如果栏目是“半开放栏目”或“认证栏目”，请在此设置允许浏览此栏目的会员组
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="ViewGroup" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" Width="100%">
                                            <asp:ListItem Value="-2">待验证会员</asp:ListItem>
                                            <asp:ListItem Value="-1">已验证会员</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 288px">
                                        <strong>允许查看此栏目下信息的会员组：</strong><br />
                                        如果栏目是“认证栏目”，请在此设置允许查看此栏目下信息的会员组如果在信息中设置了查看权限，则以信息中的权限设置优先
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="ViewGroup2" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" Width="100%">
                                            <asp:ListItem Value="-2">待验证会员</asp:ListItem>
                                            <asp:ListItem Value="-1">已验证会员</asp:ListItem>
                                        </asp:CheckBoxList>
                                </tr>
                                <tr >
                                    <td style="width: 288px">
                                        <strong>发表权限：</strong><br />
                                        请在此设置允许在此栏目发表信息的会员组。游客没有发表信息权限。
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="ViewGroup3" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" Width="100%">
                                            <asp:ListItem Value="-2">待验证会员</asp:ListItem>
                                            <asp:ListItem Value="-1">已验证会员</asp:ListItem>
                                        </asp:CheckBoxList>
                                </tr>
                                <tr >
                                    <td style="width: 288px">
                                        <strong>评论权限：</strong><br />
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="forum" runat="server">
                                            <asp:ListItem Selected="True" Value="1">允许在此栏目发表评论</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="2">评论需要审核</asp:ListItem>
                                        </asp:CheckBoxList>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane" id="Tabs5">
                        <table class="table table-striped table-bordered table-hover">
                            <%--权限设置--%>
                            <tbody>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="NodeRole" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>栏目权限设置：</strong><br />
                                        <strong>(请勾上左边单选框,确认更改)</strong>
                                    </td>
                                    <td>
                                        <ZL:ExGridView ID="Egv" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False" DataKeyNames="RoleID" Width="100%">
                                            <Columns>
                                              <asp:TemplateField HeaderText="角色名">
                                                  <ItemTemplate>
                                                      <%#Eval("RoleName") %>
                                                      <input type="checkbox" onclick="CheckAll();" />
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="查看">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSel1" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="录入">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSel2" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="审核">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSel3" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="信息管理">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSel4" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="当前栏目管理">
                                                    <HeaderStyle Width="15%" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSel5" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="评论管理">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSel6" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </ZL:ExGridView>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="width: 50px; text-align: center">
                                        <asp:CheckBox ID="NodeURole" runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <strong>用户权限：<br />(会员中心)</strong>
                                    </td>
                                    <td>
                                        <ZL:ExGridView ID="GridView1" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False" DataKeyNames="GroupID" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="会员组">
                                                    <ItemTemplate>
                                                        <input type="checkbox" onclick="CheckAll()" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="GroupName" HeaderText="组名称">
                                                    <HeaderStyle Width="30%" />
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="查看">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="HiddenGroupID" runat="server" Value='<%#GetGrouplook(Eval("GroupID","{0}")) %>' />
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#GetGrouplook(Eval("GroupID","{0}")) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="下载">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="HiddenGroupID1" runat="server" Value='<%#GetGroupDown(Eval("GroupID","{0}")) %>' />
                                                        <asp:CheckBox ID="chkSel11" runat="server" Checked='<%#GetGroupDown(Eval("GroupID","{0}")) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="引用">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="HiddenGroupID2" runat="server" Value='<%#GetGroupquote(Eval("GroupID","{0}")) %>' />
                                                        <asp:CheckBox ID="chkSel12" runat="server" Checked='<%#GetGroupquote(Eval("GroupID","{0}")) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="录入">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%#GetGroupAddto(Eval("GroupID","{0}")) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="修改">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox3" runat="server" Checked='<%#GetGroupModify(Eval("GroupID","{0}")) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="删除">
                                                    <HeaderStyle Width="7%" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox4" runat="server" Checked='<%#GetGroupModify(Eval("GroupID","{0}")) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="仅当前节点">
                                                    <HeaderStyle Width="15%" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox5" runat="server" Checked='<%#GetGroupColumns(Eval("GroupID","{0}")) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="评论管理">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox6" runat="server" Checked='<%#GetGroupComments(Eval("GroupID","{0}")) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </ZL:ExGridView>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <table class="table table-striped table-bordered table-hover">
                    <tr >
                        <td colspan="3">
                            <font color="blue">说明：</font>
                            <br />
                            1、若要批量修改某个属性的值，请先选中其左侧的复选框，然后再设定属性值。<br />
                            2、这里显示的属性值都是系统默认值，与所选节点的已有属性无关
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="text-center">
            <td colspan="2">
                <asp:Button ID="EBtnBacthSet" class="btn btn-primary" Text="执行批处理" OnClick="EBtnBacthSet_Click" runat="server" />
                <a href="NodeManage.aspx" class="btn btn-primary">取消</a>
            </td>
        </tr>
    </table>
    <ZL:TlpDown runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        var tID = 0;
        var arrTabTitle = new Array("TabTitle0", "TabTitle1", "TabTitle2", "TabTitle3", "TabTitle4", "TabTitle5");
        var arrTabs = new Array("Tabs0", "Tabs1", "Tabs2", "Tabs3", "Tabs4","Tabs5");
        $().ready(function(){
            Tlp_initTemp();
        });
        function ShowTabs(ID) {
            if (ID != tID) {
                document.getElementById(arrTabTitle[tID].toString()).className = "tabtitle";
                document.getElementById(arrTabTitle[ID].toString()).className = "titlemouseover";
                document.getElementById(arrTabs[tID].toString()).style.display = "none";
                document.getElementById(arrTabs[ID].toString()).style.display = "";
                tID = ID;          
            }
        }
        function SelectAll() {
            for (var i = 0; i < document.getElementById('<%=LstNodes.ClientID%>').length; i++) {
                document.getElementById('<%=LstNodes.ClientID%>').options[i].selected = true;
            }
        }
        function UnSelectAll() {
            for (var i = 0; i < document.getElementById('<%=LstNodes.ClientID%>').length; i++) {
                document.getElementById('<%=LstNodes.ClientID%>').options[i].selected = false;
            }
        }
        var diag=new ZL_Dialog();
        function WinOpenDialog(url,w,h)
        {
            diag.title="选择模板";;
            diag.url=url;
            diag.maxbtn=false;
            diag.ShowModal();
        }
        function CloseDialog(){
            diag.CloseModal();
        }
        function CheckAll()
        {
            $obj=$(event.srcElement);
            $obj.parent().parent().find("input:checkbox").each(function(){this.checked=$obj.get(0).checked});
        }
    </script>
</asp:Content>