<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditNode.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.EditNode" MasterPageFile="~/Manage/I/Default.master"%>
<%@ Register Src="~/Manage/I/ASCX/TlpDP.ascx" TagPrefix="ZL" TagName="TlpDP" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link type="text/css" href="/dist/css/bootstrap-switch.min.css"  rel="stylesheet"/>
<style>
th {text-align: center;}
.tips {line-height: 32px;margin-left: 5px;}
.tdl{width:288px;}
table input[type=checkbox]{margin-top:-2px;}
</style>
<script src="/dist/js/bootstrap-switch.js"></script>
<script src="/JS/SelectCheckBox.js"></script>
<title><asp:Literal runat="server" ID="Title_T"></asp:Literal> </title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ul class="nav nav-tabs">
    <li class="active"><a href="#Tabs0" data-toggle="tab">基本信息</a></li>
    <li><a href="#Tabs1" data-toggle="tab">栏目选项</a></li>
    <li><a href="#Tabs2" data-toggle="tab">收费设置</a></li>
    <li><a href="#Tabs3" data-toggle="tab">模板选项</a></li>
    <li><a href="#Tabs4" data-toggle="tab">生成选项</a></li>
    <li><a href="#Tabs5" data-toggle="tab">自设内容</a></li>
    <li><a href="#Tabs6" data-toggle="tab">浏览权限</a></li>
    <li><a href="#Tabs7" data-toggle="tab">操作权限</a></li>
</ul>
<div class="tab-content">
    <div class="tab-pane active" id="Tabs0">
        <table class="table table-striped table-bordered table-hover">
            <%-- 基本信息--%>
            <tbody>
                <tr>
                    <td class="tdl"><strong>所属栏目：</strong></td>
                    <td>
                        <asp:Label ID="LblNodeName" runat="server"></asp:Label>
                        <asp:HiddenField ID="HdnKeyName" Value="0" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>栏目名称：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtNodeName" CssClass="form-control text_300" runat="server"/>
                        <span id="TxtNodeName_sp" class="rd_red">*</span>
                        <asp:RequiredFieldValidator Visible="false" ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="TxtNodeName" ErrorMessage="RequiredFieldValidator">栏目名不能为空</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>栏目标识名：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtNodeDir" CssClass="form-control text_300" MaxLength="20" runat="server"></asp:TextBox>
                        <span id="TxtNodeDir_sp" class="rd_red">*</span>
                        <span style="color: Blue">注意：目录名只能是字母、数字、下划线组成，首字符不能是数字
                        <asp:RegularExpressionValidator Visible="false" ID="RegTxtNodeDir" runat="server" ControlToValidate="TxtNodeDir" Display="Dynamic" ValidationExpression="[_a-zA-Z][_a-zA-Z0-9]*" ErrorMessage="目录名格式错误" SetFocusOnError="True" /></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>栏目图片地址：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtNodePicUrl" class="form-control text_300" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <strong>栏目提示：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtTips" Style="max-width: 400px;" class="form-control" runat="server" Height="60px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>栏目说明：</strong>
                    </td>
                    <td><asp:TextBox ID="TxtDescription" Style="max-width: 400px;" class="form-control" runat="server" Height="58px" Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>栏目META关键词：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtMetaKeywords" Style="max-width: 400px;" class="form-control" runat="server" Rows="4" TextMode="MultiLine" Height="61px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>栏目META网页描述：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtMetaDescription" Style="max-width: 400px;" class="form-control" runat="server" Rows="4" TextMode="MultiLine" Height="60px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td><strong>创建人：</strong></td>
                <td><asp:TextBox ID="CUser_T" runat="server" CssClass="form-control text_300"></asp:TextBox></td>
            </tr>
            <tr>
                <td><strong>创建时间：</strong></td>
                <td><asp:TextBox ID="CDate_T" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm' })" CssClass="form-control text_300"></asp:TextBox></td>
            </tr>
            </tbody>
        </table>
    </div>
    <div class="tab-pane" id="Tabs1">
        <table class="table table-striped table-bordered table-hover">
            <%--栏目选项--%>
            <tbody>
                <tr>
                    <td class="tdl">
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
                <tr>
                    <td><strong>内容打开方式：</strong></td>
                    <td>
                        <asp:RadioButtonList ID="RBLItemOpenType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="_self">原窗口打开</asp:ListItem>
                            <asp:ListItem Value="_blank">新窗口打开</asp:ListItem>
                            <asp:ListItem Value="_parent">父窗口打开</asp:ListItem>
                            <asp:ListItem Value="_top">全局载入</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>栏目权限：</strong>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RBLPurviewType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">开放</asp:ListItem>
                            <asp:ListItem Value="1">认证</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td><strong>URL访问格式：</strong></td>
                    <td>
                        <label><input type="radio" name="UrlFormat_Rad" value="en" checked="checked"/>经典(/Item/Gid)</label>
                        <label><input type="radio" name="UrlFormat_Rad" value="cn" />中文(Item/标题)</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>会员投稿状态：</strong>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="SiteContentAudit_Rad" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">待审核</asp:ListItem>
                            <asp:ListItem Value="99">已审核</asp:ListItem>
                            <asp:ListItem Value="-1">回收站</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>点击统计时间：</strong>
                    </td>
                    <td>
                        <asp:DropDownList ID="ClickTimeout" CssClass="form-control text_md" runat="server">
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
                        <%--<asp:TextBox ID="ClickTimeout" class="form-control" runat="server" Columns="30">0</asp:TextBox>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>热点的点击数：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtHitsOfHot" class="form-control text_md" runat="server">0</asp:TextBox>
                        <asp:RangeValidator Visible="false" ID="ValgHitsOfHot" runat="server" Display="Dynamic" ControlToValidate="TxtHitsOfHot" ErrorMessage="请输入整数" MinimumValue="0" MaximumValue="2147483647" Type="Integer" SetFocusOnError="True"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>工作流：</strong>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control text_md"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>内容防护：</strong>
                    </td>
                    <td>
                            <input runat="server" type="checkbox" id="SafeGuard" class="switchChk"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>简洁模式：</strong>
                    </td>
                    <td>
                        <input type="checkbox" runat="server" id="isSimple_CH" class="switchChk" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>绑定互动管理：</strong><br />
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" CssClass="form-control text_md" runat="server"></asp:DropDownList>
                        <span class="tips" style='color: #1e860b;'>只能绑定节点类别的互动模块</span>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="tab-pane" id="Tabs2">
        <table class="table table-striped table-bordered table-hover">
            <%--收费设置--%>
            <tbody>
                <tr>
                    <td class="tdl">
                        <strong>默认消费点券数：</strong><br />
                        注：会员在此栏目下阅读内容时，该内容默认的收费点券数
                    </td>
                    <td>
                        <asp:TextBox ID="TxtConsumePoint" Style="max-width: 150px;" class="form-control pull-left" runat="server">0</asp:TextBox><span class="tips">点</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>重复收费方式：</strong><br />
                        注：会员在此栏目下阅读内容时，该内容的重复收费方式
                    </td>
                    <td>
                        <asp:HiddenField ID="ConsumeType_Hid" runat="server" />
                        <input id="Radio1" name="ConsumeType" value="0" type="radio" />不重复收费<div class="clearfix"></div>
                        <span class="pull-left" style="line-height: 32px; margin-right: 5px;">
                            <input id="Radio2" name="ConsumeType" value="1" type="radio" />距离上次收费时间</span>
                        <asp:TextBox ID="TxtConsumeTime" Style="max-width: 80px;" class="form-control pull-left" runat="server">0</asp:TextBox><span class="tips">小时后重新收费</span><div class="clearfix"></div>
                        <span class="pull-left" style="line-height: 32px; margin-right: 5px;">
                            <input id="Radio3" name="ConsumeType" value="2" type="radio" />会员重复阅读此文章</span>
                        <asp:TextBox ID="TxtConsumeCount" Style="max-width: 80px;" class="form-control pull-left" runat="server">0</asp:TextBox><span class="tips">次后重新收费</span><div class="clearfix"></div>
                        <input id="Radio4" name="ConsumeType" value="3" type="radio" />上述两者都满足时重新收费<div class="clearfix"></div>
                        <input id="Radio5" name="ConsumeType" value="4" type="radio" />上述两者任一个满足时就重新收费<div class="clearfix"></div>
                        <input id="Radio6" name="ConsumeType" value="5" type="radio" />每阅读一次就重复收费一次（建议不要使用）<div class="clearfix"></div>
                    </td>
                </tr>
                <tr runat="server" visible="false">
                    <td>
                        <strong>可浏览篇数：</strong><br />
                        注：会员在此栏目下查看内容时,该节点可浏览篇数
                    </td>
                    <td id="BGroups">
                        <asp:Repeater ID="BGroup" runat="server">
                            <ItemTemplate>
                                <span class="tips" style="width: 70px; display: block; float: left"><%# Eval("GroupName")%> </span>
                                <input type="hidden" value='<%# Eval("GroupID") %>' />
                                <asp:TextBox ID="CNode1" class="form-control pull-left" Style="max-width: 80px;" runat="server" Text='<%# GetViewVl(Eval("GroupID").ToString()) %>'></asp:TextBox><span class="tips">篇</span><div class="clearfix"></div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>分成比例：</strong><br />
                        注：会员在此栏目下添加内容时，该内容默认的分成比例
                    </td>
                    <td>
                        <asp:TextBox ID="TxtShares" Style="max-width: 80px;" class="form-control pull-left" runat="server">0</asp:TextBox><span class="tips">%</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>发布内容收费：</strong><br />
                        注：会员在此栏目下发布内容时，每发布一篇内容收费
                    </td>
                    <td>
                        <asp:TextBox ID="TxtAddMoney" Style="max-width: 80px; margin-bottom: 5px;" class="form-control pull-left" runat="server">0</asp:TextBox>
                        <span class="tips">元</span><div class="clearfix"></div>
                        <asp:TextBox ID="TxtAddPoint" Style="max-width: 80px; margin-bottom: 5px;" class="form-control pull-left" runat="server">0</asp:TextBox>
                        <span class="tips">点券</span><div class="clearfix"></div>
                        <asp:TextBox ID="txtAddExp" Style="max-width: 80px; margin-bottom: 5px;" class="form-control pull-left" runat="server">0</asp:TextBox>
                        <span class="tips">积分</span><div class="clearfix"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>下载内容收费：</strong><br />
                        注：会员在此栏目下载内容时，每下载一次所需费用
                    </td>
                    <td>
                        <asp:TextBox ID="txtDeducExp" Style="max-width: 80px;" class="form-control pull-left" runat="server">0</asp:TextBox>
                        <span class="tips">积分</span>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="tab-pane" id="Tabs3">
        <table class="table table-striped table-bordered table-hover">
            <%--模板选项--%>
            <tbody>
                <tr>
                    <td class="tdl">
                        <strong>栏目首页模板：</strong>
                    </td>
                    <td>
                            <%=PageCommon.GetTlpDP("IndexTemplate",!string.IsNullOrEmpty(IndexTemplate_hid.Value),"/Class_"+NodeID+"/Default.aspx") %>
                            <asp:HiddenField ID="IndexTemplate_hid" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>栏目列表页模板：</strong>
                    </td>
                    <td>
                            <%=PageCommon.GetTlpDP("TxtTemplate",!string.IsNullOrEmpty(TxtTemplate_hid.Value),"/Class_"+NodeID+"/NodePage.aspx") %>
                            <asp:HiddenField ID="TxtTemplate_hid" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>最新信息模板：</strong>
                    </td>
                    <td>
                            <%=PageCommon.GetTlpDP("LastinfoTemplate",!string.IsNullOrEmpty(LastinfoTemplate_hid.Value),"/Class_"+NodeID+"/NodeNews.aspx") %>
                            <asp:HiddenField ID="LastinfoTemplate_hid" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>热门信息模板：</strong>
                    </td>
                    <td>
                            <%=PageCommon.GetTlpDP("HotinfoTemplate",!string.IsNullOrEmpty(HotinfoTemplate_hid.Value),"/Class_"+NodeID+"/NodeHot.aspx") %>
                            <asp:HiddenField ID="HotinfoTemplate_hid" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>推荐信息模板：</strong>
                    </td>
                    <td>
                            <%=PageCommon.GetTlpDP("ProposeTemplate",!string.IsNullOrEmpty(ProposeTemplate_hid.Value),"/Class_"+NodeID+"/NodeElite.aspx") %>
                            <asp:HiddenField ID="ProposeTemplate_hid" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>选择内容模型：</strong>
                    </td>
                    <td>
                        <asp:HiddenField ID="HdnModeID" runat="server" />
                        <table class="table table-striped table-bordered table-hover">
                            <asp:Repeater ID="Model_RPT" runat="server">
                                <ItemTemplate>
                                    <tr class="tdbg">
                                        <td style="width: 150px">
                                            <label><%# GetChk(Eval("ModelID","{0}")) %>
                                            <%#GetModelIcon() %><span><%# Eval("ModelName") %></span>
                                            </label>
                                        </td>
                                        <td>
                                                <%#PageCommon.GetTlpDP("ModelTemplate_"+Eval("ModelID")) %>
                                                <input type="hidden" id="ModelTemplate_<%# Eval("ModelID") %>_hid" name="ModelTemplate_<%# Eval("ModelID") %>" value="<%# GetTemplate(Eval("ModelID","{0}")) %>" />
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
    <div class="tab-pane" id="Tabs4">
        <table class="table table-striped table-bordered table-hover">
            <%--生成选项--%>
            <tbody>
                <tr>
                    <td class="tdl">
                        <strong>列表首页扩展名：</strong>
                    </td>
                    <td>
                        <div style="display:inline-block;">
                            <asp:RadioButtonList ID="ListPageHtmlEx_Rad" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">.html</asp:ListItem>
                                <asp:ListItem Value="1">.htm</asp:ListItem>
                                <asp:ListItem Value="2">.shtml</asp:ListItem>
                                <asp:ListItem Value="3" Selected="True">.aspx(不生成静态)</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <input runat="server" type="button" onclick="createHtml();" id="Release_Btn" style="position:relative;margin-top:-10px;margin-left:10px;" visible="false" class="btn btn-info" value="发布该节点" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>栏目列表页面扩展名：</strong>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="ListPageEx_Rad" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">.html</asp:ListItem>
                            <asp:ListItem Value="1">.htm</asp:ListItem>
                            <asp:ListItem Value="2">.shtml</asp:ListItem>
                            <asp:ListItem Value="3" Selected="True">.aspx(不生成静态)</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>最新信息页扩展名：</strong>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="LastinfoPageEx_Rad" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">.html</asp:ListItem>
                            <asp:ListItem Value="1">.htm</asp:ListItem>
                            <asp:ListItem Value="2">.shtml</asp:ListItem>
                            <asp:ListItem Value="3" Selected="True">.aspx(不生成静态)</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>热门信息页扩展名：</strong>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="HotinfoPageEx" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">.html</asp:ListItem>
                            <asp:ListItem Value="1">.htm</asp:ListItem>
                            <asp:ListItem Value="2">.shtml</asp:ListItem>
                            <asp:ListItem Value="3" Selected="True">.aspx(不生成静态)</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>推荐信息扩展名：</strong>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="ProposePageEx" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">.html</asp:ListItem>
                            <asp:ListItem Value="1">.htm</asp:ListItem>
                            <asp:ListItem Value="2">.shtml</asp:ListItem>
                            <asp:ListItem Value="3" Selected="True">.aspx(不生成静态)</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr> 
                    <td>
                        <strong>内容页扩展名：</strong>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="ContentFileEx_Rad" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">.html</asp:ListItem>
                            <asp:ListItem Value="1">.htm</asp:ListItem>
                            <asp:ListItem Value="2">.shtml</asp:ListItem>
                            <asp:ListItem Value="3" Selected="True">.aspx(不生成静态)</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>节点目录生成位置：</strong>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RBLPosition" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">继承父节点目录</asp:ListItem>
                            <asp:ListItem Value="0">根目录下</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                    
                <tr>
                    <td>
                        <strong>内容页文件名规则：</strong>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLContentRule" CssClass="form-control" Style="max-width: 200px;" runat="server">
                            <asp:ListItem Selected="True" Value="2">栏目目录/InfoID</asp:ListItem>
                            <asp:ListItem Value="0">栏目目录/年/月/日/infoid</asp:ListItem>
                            <asp:ListItem Value="1">栏目目录/年-月/InfoID</asp:ListItem>
                            <asp:ListItem Value="3">栏目目录/年月日/标题</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="tab-pane" id="Tabs5">
        <table class="table table-striped table-bordered table-hover">
            <tbody>
                <tr>
                    <td class="tdl">
                        <strong>自设内容项目数：</strong>
                    </td>
                    <td>
                        <asp:DropDownList ID="SelCount" CssClass="form-control" Style="max-width: 80px;" runat="server" onchange="showCustom(this.value);selectnum();" onmouseup="selectnum()" onmousedown="selectnum()" onmouseout="selectnum()">
                            <asp:ListItem Value="1"></asp:ListItem>
                            <asp:ListItem Value="2"></asp:ListItem>
                            <asp:ListItem Value="3"></asp:ListItem>
                            <asp:ListItem Value="4"></asp:ListItem>
                            <asp:ListItem Value="5"></asp:ListItem>
                            <asp:ListItem Value="6"></asp:ListItem>
                            <asp:ListItem Value="7"></asp:ListItem>
                            <asp:ListItem Value="8"></asp:ListItem>
                            <asp:ListItem Value="9"></asp:ListItem>
                            <asp:ListItem Value="10"></asp:ListItem>
                            <asp:ListItem Value="11"></asp:ListItem>
                            <asp:ListItem Value="12"></asp:ListItem>
                            <asp:ListItem Value="13"></asp:ListItem>
                            <asp:ListItem Value="14"></asp:ListItem>
                            <asp:ListItem Value="15"></asp:ListItem>
                            <asp:ListItem Value="16"></asp:ListItem>
                            <asp:ListItem Value="17"></asp:ListItem>
                            <asp:ListItem Value="18"></asp:ListItem>
                            <asp:ListItem Value="19"></asp:ListItem>
                            <asp:ListItem Value="20"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom1" name="Custom1" runat="server" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容1：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="1" /}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom2" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容2：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="2" /}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom3" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容3：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="3" /}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom4" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容4：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="4" /}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox4" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom5" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容5：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="5" /}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox5" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom6" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容6：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="6" /}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox6" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom7" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容7：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="7" /}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox7" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom8" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容8：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="8" /}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox8" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom9" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容9：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="9" /}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox9" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom10" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容10：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="10" /}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox10" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom11" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容11：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="11"/}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox11" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom12" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容12：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="12"/}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox12" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom13" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容13：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="13"/}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox13" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom14" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容14：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="14"/}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox14" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom15" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容15：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="15"/}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox15" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom16" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容16：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="16"/}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox16" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom17" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容17：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="17"/}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox17" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom18" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容18：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="18"/}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox18" class="form-control" Style="max-width: 500px;" Height="85px" runat="server" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom19" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容19：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="19"/}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox19" class="form-control" Style="max-width: 500px;" runat="server" Height="85px" TextMode="MultiLine" Rows="5" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg" id="Custom20" runat="server" style="display: none" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        <strong>自设内容20：</strong><br />
                        在列表模板页面插入{PH.Label id="自设节点内容" nodeid="@Request_id" num="20"/}
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox20" class="form-control" Style="max-width: 500px;" runat="server" TextMode="MultiLine" Rows="5" Width="99%" Height="85px"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="tab-pane" id="Tabs7">
        <table class="table table-striped table-bordered table-hover">
            <%--后台权限--%>
            <tbody>
                <tr>
                    <td class="tdl">
                        <strong>后台权限：</strong>
                    </td>
                    <td>
                        <ZL:ExGridView ID="AdminRole_EGV" runat="server" CssClass="table table-striped table-bordered table-hover" 
                            AutoGenerateColumns="False" DataKeyNames="RoleID">
                            <Columns>
                                <asp:TemplateField HeaderText="角色名">
                                    <HeaderTemplate>
                                        <label><input type="checkbox" onclick="ChkAll('admin');" />角色名</label>
                                    </HeaderTemplate>
                                    <ItemStyle Width="35%" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <label><%#Eval("RoleName") %><input type="checkbox" id="checkAll" onclick="CheckAll(this,'checkAll','0');" /></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label><input type="checkbox"  onclick="ColChk('look1');" />查看</label>
                                    </HeaderTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="HiddenRID" runat="server" Value='<%# Eval("RoleID") %>' />
                                        <asp:CheckBox ID="chkSel1" data-auth="look1" runat="server" Checked='<%# getbool(Eval("look")) %>'  />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="录入">
                                    <HeaderTemplate>
                                        <label><input type="checkbox"  onclick="ColChk('addto1');" />录入</label>
                                    </HeaderTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel2" data-auth="addto1" runat="server" Checked='<%# getbool(Eval("addTo")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label><input type="checkbox"  onclick="ColChk('state1');" />审核</label>
                                    </HeaderTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel3" data-auth="state1" runat="server" Checked='<%# getbool(Eval("State")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label><input type="checkbox"  onclick="ColChk('modify1');" />信息管理</label>
                                    </HeaderTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel4" data-auth="modify1" runat="server" Checked='<%# getbool(Eval("Modify")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label><input type="checkbox"  onclick="ColChk('col1');" />仅当前节点</label>
                                    </HeaderTemplate>
                                    <HeaderStyle Width="15%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel5" data-auth="col1" runat="server" Checked='<%# getbool(Eval("Columns")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label><input type="checkbox" onclick="ColChk('com1');" />评论管理</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel6" data-auth="com1" runat="server" Checked='<%# getbool(Eval("Comments")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            </ZL:ExGridView>
                    </td>
                </tr>
                <tr>
                    <td><strong>用户权限：</strong></td>
                    <td>
                        <ZL:ExGridView runat="server" ID="GroupAuth_EGV" AutoGenerateColumns="false" EnableTheming="False" CssClass="table table-striped table-bordered table-hover" DataKeyNames="GroupID">
                            <Columns>
                                <asp:TemplateField HeaderText="组名称">
                                    <HeaderTemplate>
                                        <label><input type="checkbox" onclick="ChkAll('group');" />组名称</label>
                                    </HeaderTemplate>
                                    <ItemStyle Width="30%" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <label><%# Eval("GroupName") %><input type="checkbox" id="checkAll" onclick="CheckAll(this, 'checkAll', '0');" /></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="查看">
                                    <HeaderTemplate>
                                        <label><input type="checkbox" onclick="ColChk('look2');" />查看</label>
                                    </HeaderTemplate>
                                    <HeaderStyle Width="7%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel1" data-auth="look2" runat="server" Checked='<%#GroupAuth("look") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="下载">
                                    <HeaderTemplate>
                                        <label><input type="checkbox" onclick="ColChk('down');" />下载</label>
                                    </HeaderTemplate>
                                    <HeaderStyle Width="7%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel11" data-auth="down" runat="server" Checked='<%#GroupAuth("down") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label><input type="checkbox" onclick="ColChk('quote');" />引用</label>
                                    </HeaderTemplate>
                                    <HeaderStyle Width="7%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel12" data-auth="quote" runat="server" Checked='<%#GroupAuth("quote") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label><input type="checkbox" onclick="ColChk('addto2');" />录入</label>
                                    </HeaderTemplate>
                                    <HeaderStyle Width="7%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel2" data-auth="addto2" runat="server" Checked='<%#GroupAuth("addTo") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label><input type="checkbox" onclick="ColChk('modify2');" />修改</label>
                                    </HeaderTemplate>
                                    <HeaderStyle Width="7%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel3" data-auth="modify2" runat="server" Checked='<%#GroupAuth("Modify") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label><input type="checkbox" onclick="ColChk('del');" />删除</label>
                                    </HeaderTemplate>
                                    <HeaderStyle Width="7%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel4" data-auth="del" runat="server" Checked='<%#GroupAuth("Deleted") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label><input type="checkbox" onclick="ColChk('col2');" />仅当前节点</label>
                                    </HeaderTemplate>
                                    <HeaderStyle Width="15%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel5" data-auth="col2" runat="server" Checked='<%#GroupAuth("Columns") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label><input type="checkbox" onclick="ColChk('com2');" />评论管理</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel6" data-auth="com2" runat="server" Checked='<%#GroupAuth("Comments") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </ZL:ExGridView>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="tab-pane" id="Tabs6">
        <table class="table table-striped table-bordered table-hover">
            <!--前台权限-->
            <tbody>
                <tr>
                    <td class="tdl">
                        <strong>浏览/查看权限：</strong><br />
                        栏目权限为继承关系。例如：当父栏目设为“认证栏目”时，子栏目的权限设置将继承父栏目设置，即使子栏目设为“开放栏目”也无效。相反，如果父栏目设为“开放栏目”，子栏目可以设为“半开放栏目”或“认证栏目”。
                    </td>
                    <td>
                        <asp:RadioButtonList ID="SelCheck_RadioList" runat="server">
                            <asp:ListItem Text="开放栏目 任何人（包括游客）可以浏览和查看此栏目下的信息。" Selected="True" Value="allUser"></asp:ListItem>
                            <asp:ListItem Text="半开放栏目 任何人（包括游客）都可以浏览。游客不可查看，其他会员根据会员组的栏目权限设置决定是否可以查看。" Value="moreUser"></asp:ListItem>
                            <asp:ListItem Text="认证栏目 游客不能浏览和查看，其他会员根据会员组的栏目权限设置决定是否可以浏览和查看。" Value="onlyUser"></asp:ListItem>
                        </asp:RadioButtonList>
                            
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>允许浏览此栏目的会员组：</strong><br />
                        如果栏目是“半开放栏目”或“认证栏目”，请在此设置允许浏览此栏目的会员组
                    </td>
                    <td>
                        <asp:CheckBoxList ID="ViewGroup_Chk" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" Width="100%">
                            <asp:ListItem Value="-2">待验证会员</asp:ListItem>
                            <asp:ListItem Value="-1">已验证会员</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr repeatcolumns="5">
                    <td>
                        <strong>允许查看此栏目下信息的会员组：</strong><br />
                        如果栏目是“认证栏目”，请在此设置允许查看此栏目下信息的会员组 如果在信息中设置了查看权限，则以信息中的权限设置优先
                    </td>
                    <td>
                        <asp:CheckBoxList ID="ViewGroup2_Chk" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" Width="100%">
                            <asp:ListItem Value="-2">待验证会员</asp:ListItem>
                            <asp:ListItem Value="-1">已验证会员</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>发表权限：</strong><br />
                        请在此设置允许在此栏目发表信息的会员组。 游客没有发表信息权限。
                    </td>
                    <td>
                        <asp:CheckBoxList ID="input_Chk" runat="server" RepeatDirection="Horizontal" RepeatColumns="5"
                            Width="100%">
                            <asp:ListItem Value="-2">待验证会员</asp:ListItem>
                            <asp:ListItem Value="-1">已验证会员</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>评论权限：</strong>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RBLCommentType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">关闭评论</asp:ListItem>
                            <asp:ListItem Value="1">允许游客</asp:ListItem>
                            <asp:ListItem Value="2">必须登录</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:CheckBoxList ID="forum_Chk" runat="server">
                            <asp:ListItem Value="1">允许在此栏目发表评论</asp:ListItem>
                            <asp:ListItem Value="2">评论需要审核</asp:ListItem>
                            <asp:ListItem Value="3">一篇文章只能发表一次评论</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="panel-footer text-center" style="border-bottom:1px solid #ddd;">
        <asp:Button ID="EBtnSubmit" CssClass="btn btn-primary" Text="保存信息" OnClick="EBtnSubmit_Click" runat="server" UseSubmitBehavior="false" />
        <asp:Button ID="Cancel_B" CssClass="btn btn-primary" Text="返回列表" OnClick="Cancel_B_Click" runat="server" />
    </div>
</div>
<ZL:TlpDP runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/chinese.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script>
    function setSaveWidth(){
        //$("#save").width($($(this).attr("href")).width());
    }
    $().ready(function () {
        $("#GridView1 tr>th:eq(0)").append("<input type=checkbox id='chkThisAll1'/>");
        $("#Egv tr>th:eq(0)").append("<input type=checkbox id='chkThisAll'/>");
        $("#chkThisAll").click(function () {
            CheckAll(this,"chkThisAll",1);
        });
        $("#chkThisAll1").click(function () {
            CheckAll(this,"chkThisAll1",1);
        });
        Tlp_initTemp();
        //初始化收费类型
        if ($("#ConsumeType_Hid").val() != "") {
            $("[name='ConsumeType'][value='" + $("#ConsumeType_Hid").val() + "']")[0].checked = true;
        }
    })
    var diag=new ZL_Dialog();
    function ShowTList(url){
        diag.title="选择模板";
        diag.url=url;
        diag.maxbtn=false;
        diag.ShowModal();
    }
    function CloseDialog(){
        diag.CloseModal();
    }
    function showCustom(value) {
        var index = parseInt(value);
        for (i = 2; i <= 20; i++) {
            if (i <= index)
                document.getElementById("Custom" + i).style.display = "";
            else
                document.getElementById("Custom" + i).style.display = "none";
        }
    }

    function selectnum() {
        //parent.document.getElementById("main_right").height = document.body.offsetHeight + 50;
    }
    function CheckAll(obj,id,n){
        var chkArr;
        if(n==0)
            chkArr = $(obj).parent().parent().parent().find("input[type=checkbox][id!=" + id + "]");
        if(n==1)
            chkArr = $(obj).parent().parent().parent().parent().find("input[type=checkbox][id!=" + id + "]");
        for (var i = 0; i < chkArr.length; i++) {
            chkArr[i].checked = obj.checked;
               
        }
    }
    function ColChk(name) {
        obj = event.srcElement;
        console.log(obj, $('span[data-auth=' + name + '] :checkbox'));
        $('span[data-auth=' + name + '] :checkbox').each(function () { this.checked = obj.checked; });
    }
    function ChkAll(type) {
        obj = event.srcElement;
        if (type == 'admin') { $("#AdminRole_EGV input[type=checkbox]").each(function () { this.checked = obj.checked; }); }
        if (type == 'group') { $("#GroupAuth_EGV input[type=checkbox]").each(function () { this.checked = obj.checked; }); }
    }
    /*-----------栏目名称与目录-----------*/
    var nameFlag = true, dirFlag = true;
    $(function () {
        $("#TxtNodeName").blur(function () {
            if (ZL_Regex.isEmpty(this.value)) { $("#TxtNodeName_sp").text("节点名称不能为空"); nameFlag = false; CheckBtn(); return; }
            $.post("<%:Request.RawUrl%>", { action: "nodename", value: $("#TxtNodeName").val() }, function (data) {
                if (data != M_APIResult.Success) {
                    nameFlag = false;
                    $("#TxtNodeName_sp").text("节点名称重复,请修改节点名称");
                }
                else { $("#TxtNodeName_sp").text("*"); nameFlag = true; }
                CheckBtn();
            });
        });
        $("#TxtNodeDir").blur(function () {
            if (ZL_Regex.isEmpty(this.value)) { $("#TxtNodeDir_sp").text("栏目名称不能为空"); dirFlag = false; CheckBtn(); return; }
            $.post("<%:Request.RawUrl%>", { action: "nodedir", value: $("#TxtNodeDir").val() }, function (data) {
                if (data != M_APIResult.Success) {
                    dirFlag = false;
                    $("#TxtNodeDir_sp").text("栏目标识重复,请修改栏目标识");
                }
                else { $("#TxtNodeDir_sp").text("*"); dirFlag = true; }
                CheckBtn();
            });
        });
        function CheckBtn()
        {
            if (nameFlag == true && dirFlag == true) { $("#EBtnSubmit").removeAttr("disabled"); }
            else { disBtn("EBtnSubmit"); }
        }
    })
    function BindPY() {
        $("#TxtNodeName").keyup(function () {
            Getpy("TxtNodeName", "TxtNodeDir");
        });
    }
    function createHtml() {
        var path = "<%=customPath2%>";
        var url = path + "Content/ReleaseByNode.aspx?NodeID=<%=NodeID%>";
        comdiag.reload = true;
        ShowComDiag(url, "生成发布");
    }
</script>
</asp:Content>
