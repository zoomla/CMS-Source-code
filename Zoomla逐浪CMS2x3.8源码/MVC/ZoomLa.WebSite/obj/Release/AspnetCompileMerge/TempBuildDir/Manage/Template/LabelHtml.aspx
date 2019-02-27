<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LabelHtml.aspx.cs" Inherits="ZoomLaCMS.Manage.Template.LabelHtml" MasterPageFile="~/Manage/I/Default.master"  ValidateRequest="false"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
<link rel="stylesheet" href="/Plugins/CodeMirror/lib/codemirror.css">
<link rel="stylesheet" href="/Plugins/CodeMirror/theme/eclipse.css">
<script src="/Plugins/CodeMirror/lib/codemirror.js"></script>
<script src="/Plugins/CodeMirror/mode/xml.js"></script>
<script src="/Plugins/CodeMirror/mode/javascript/javascript.js"></script>
<script src="/Plugins/CodeMirror/addon/selection/active-line.js"></script>
<script src="/Plugins/CodeMirror/addon/edit/matchbrackets.js"></script>
<%--<script src="/Plugins/CodeMirror/mode/htmlmixed.js"></script>--%>
<title>静态标签</title>
<style>
.maxwidth{width: 100%;} 
.modal-sm{width: 450px;} 
.label_name{width: 120px;}
.label_class{width: 150px;}                                                       
.label_explain{height: 60px; width: 317px;} 
.label_decide_name{vertical-align: middle; text-align: center; width: 6%;} 
.label_iframe{height: 30px; width: 530px; margin-left: 142px;}
.label_num{width: 100px;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb navbar-fixed-top">
        <li><a href="LabelManage.aspx">标签管理</a></li>
        <li class="active">
            <a href="<%=Request.RawUrl %>">标签编辑</a>
            <asp:Label ID="LabelName_L" runat="server" />
            <a href="../Config/CreateTable.aspx" id="a1">添加新表</a>
            <%--<a href="javascript:;" onclick="OneMode()">一步式编辑</a></li>--%>
    </ol>

    <ul class="nav nav-tabs">
        <li class="active"><a href="#Info0" data-toggle="tab">基本信息</a></li>
        <li><a href="#Info1" data-toggle="tab">标签内容</a></li>
    </ul>
    <div class="tab-content panel-body padding0">
        <div id="Info0" class="tab-pane active">
            <table class="table table-striped table-bordered tab-content">
                <tbody>
                    <tr>
                        <td class="text-right label_name"><strong>标签名称：&nbsp;</strong></td>
                        <td class="text-left" data-auto="maxwidth">
                            <asp:TextBox ID="TxtLabelName" CssClass="form-control text_md" runat="server" />
                            <asp:RequiredFieldValidator runat="server" ID="NReq" ControlToValidate="TxtLabelName" Display="Dynamic" ErrorMessage="请输入标签名称">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="TxtLabelName" ErrorMessage="标签名称重复" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator></td>
                    </tr>
                    <tr>
                        <td class="text-right"><strong>标签分类：&nbsp;</strong></td>
                        <td class="text-left">
                            <asp:TextBox ID="TxtLabelType" CssClass="form-control text_md" runat="server"></asp:TextBox>
                            <asp:DropDownList ID="DropLabelType" CssClass=" form-control text_md label_class" runat="server" OnSelectedIndexChanged="SelectCate" AutoPostBack="true"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="text-right"><strong>标签说明：&nbsp;</strong></td>
                        <td class="text-left">
                            <asp:TextBox ID="TxtLabelIntro" CssClass="form-control text_md label_explain" runat="server" TextMode="MultiLine" Columns="50" Rows="3"></asp:TextBox></td>
                    </tr>
                </tbody>

            </table>
        </div>
        <div id="Info1" class="tab-pane">
            <table class="table table-striped table-bordered tab-content">
                <tbody>
                    <tr>
                        <td class="label_decide_name">判断模式： </td>
                        <td data-auto="maxwidth">
                            <asp:CheckBox ID="boolmodel" runat="server" Text="开启判断模式" AutoPostBack="True" />
                            <font color="red">[开启后将根据判断条件输出内容]</font>
                            <iframe id="I1" frameborder="0" name="I1" scrolling="no" src="/manage/Template/label.htm" class="label_iframe"></iframe>
                        </td>
                    </tr>
                    <tr id="isbool" runat="server" visible="false">
                        <td class="text-center">模式设置： </td>
                        <td>
                            <asp:DropDownList ID="Modeltypeinfo" CssClass="form-control text_md label_num" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="计数判断">计数判断</asp:ListItem>
                                <asp:ListItem Value="用户登录判断">用户登录判断</asp:ListItem>
                                <asp:ListItem Value="参数判断">参数判断</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="Valueroot" runat="server" Width="179px" onmousedown="inputtxt(this)" onfocus="inputtxt(this)" value="这里放入标签"></asp:TextBox>
                            <asp:DropDownList ID="addroot" CssClass="form-control text_md label_num" runat="server" Visible="false" AutoPostBack="True">
                                <asp:ListItem Value="循环计算">循环计算</asp:ListItem>
                                <asp:ListItem Value="一直累加">一直累加</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="setroot" CssClass="form-control text_md label_num" runat="server">
                                <asp:ListItem Value="大于">大于</asp:ListItem>
                                <asp:ListItem Value="等于" Selected="True">等于</asp:ListItem>
                                <asp:ListItem Value="小于">小于</asp:ListItem>
                                <asp:ListItem Value="不等于">不等于</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="Modelvalue" runat="server" CssClass="form-control text_md" Style="max-width: 200px;"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" ForeColor="#339933" Visible="False" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="text-right" style="width: 100%;" colspan="2">
                            <table style="width: 100%">
                                <tr>
                                    <td style="min-width: 276px; width: 276px; vertical-align: top;" id="frmTitle">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tbody id="ss">
                                                <tr align="center">
                                                    <td>
                                                        <div id="Tab1_header">
                                                            <ul class="nav nav-tabs">
                                                                <li id="TabTitle0" class="active"><a href="#Tabs0" data-toggle="tab">自定标签</a></li>
                                                                <li id="TabTitle1"><a href="#Tabs1" data-toggle="tab">系统标签</a></li>
                                                                <li id="TabTitle2"><a href="#Tabs2" data-toggle="tab">扩展函数</a></li>
                                                            </ul>
                                                            <div class="tab-content">
                                                                <div class="tab-pane active" id="Tabs0" runat="server">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table table-striped table-bordered">
                                                                        <tbody>
                                                                            <tr align="center">
                                                                                <td>
                                                                                    <asp:DropDownList ID="DDLCate" runat="server" OnSelectedIndexChanged="ChangeCate"
                                                                                        AutoPostBack="true">
                                                                                    </asp:DropDownList></td>
                                                                            </tr>
                                                                            <tr align="center">
                                                                                <td>
                                                                                    <div style="overflow: auto; height: 370px">
                                                                                        <asp:Label ID="LblLabel" runat="server"></asp:Label>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                                <div class="tab-pane" id="Tabs1" runat="server">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table table-striped table-bordered">
                                                                        <tbody>
                                                                            <tr align="center">
                                                                                <td>
                                                                                    <div style="overflow: auto; height: 410px">
                                                                                        <asp:Label ID="LblSysLabel" runat="server"></asp:Label>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                                <div class="tab-pane" id="Tabs2" runat="server">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table table-striped table-bordered">
                                                                        <tbody>
                                                                            <tr align="center">
                                                                                <td>
                                                                                    <div style="overflow: auto; height: 410px">
                                                                                        <asp:Label ID="LblFunLabel" runat="server"></asp:Label>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td align="left">
                                        <div id="Textarea">
                                            <asp:TextBox TextMode="MultiLine" runat="server" ID="textContent"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="s1" runat="server" visible="false">
                        <td style="width: 100%; text-align: left;" colspan="2"><strong>标签内容:(<font color="red">不满足</font>判断)</strong></td>
                    </tr>
                    <tr id="s2" runat="server" visible="false" onmouseover="this.className='tdbgmouseover'"
                        onmouseout="this.className='tdbg'">
                        <td style="width: 100%; text-align: right;" colspan="2">
                            <table style="width: 100%">
                                <tr>
                                    <td align="left">
                                        <asp:TextBox TextMode="MultiLine" runat="server" CssClass="form-control text_md" ID="falsecontent" Style="max-width: 100%; height: 231px;"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <table class="table table-striped table-bordered tab-content">
                <tr>
                    <td colspan="2">
                        <select id="selstatus" class="form-control text_md" style="width: 120px" onchange="selPage(this)">
                            <option value="0">基本信息</option>
                            <option value="1">标签内容</option>
                        </select>
                        <a id="rel" href="#Info0" onclick="changeIndex(0,'rel')" class="btn btn-primary" style="display: none;" data-toggle="tab">上一步</a> <a id="next" href="#Info1" onclick="changeIndex(1,'next')" class="btn btn-primary" data-toggle="tab">下一步</a>
                        <asp:Button ID="Button1" runat="server" Text="保存标签" CssClass="btn btn-primary" OnClick="BtnSave_Click" Style="cursor: pointer; cursor: pointer;" />
                        <input id="BtnCancel" type="button" class="btn btn-primary" value="取　消" onclick="window.location.href = 'LabelManage.aspx'" style="cursor: pointer; cursor: pointer;" />
                </tr>
        </table>
    </div>
    <asp:HiddenField runat="server" ID="indexStatu_Hid" />
    <asp:HiddenField runat="server" ID="editor_hid" Value="" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/Plugins/CodeMirror/LabelCall.js"></script>
    <script type="text/javascript" src="/JS/Common.js"></script>
<script type="text/javascript">
        $("body").ready(function () {
            $("[data-auto=maxwidth]").width($("#content").width());
            if ($("#indexStatu_Hid").val()=="1") {
                $("#next").click();
                $("#selstatus").val("1");
            }
        });
        function selPage(data) {
            if ($(data).val() == 1) {
                $("#next").click();
            } else {
                $("#rel").click();
            }
        }
        function changeIndex(index, staue) {
            if (staue == "next") {
                checkData();
                $("[data-index=" + (index - 1) + "]").removeClass("active");
                $("#next").hide();
                $("#rel").show();
                $("#selstatus").val("1");
            } else {
                $("[data-index=" + (index + 1) + "]").removeClass("active");
                $("#rel").hide();
                $("#next").show();
                $("#selstatus").val("0");
            }
            $("[data-index=" + index + "]").addClass("active");
            if ($("#editor_hid").val() == "")
            {
                setTimeout(function () { editor.refresh(); }, 500); $("#editor_hid").val("1");
            }
        }
        function changeStaus(staue) {
            if (staue == "next") {
                $("#rel").hide();
                $("#next").show();
                $("#selstatus").val("0");
            } else {
                checkData();
                $("#next").hide();
                $("#rel").show();
                $("#selstatus").val("1");
            }
        }
        function checkData() {
            if ($("#TxtLabelName").val()=="") {
                alert("标签名不能为空");
                setTimeout(function () {$("#rel").click()},100);
            }
        }
        var diag = new ZL_Dialog();
        function opentitle(url, title) {
            diag.url = url;
            diag.title = title;
            diag.ShowModal();
        }
        function closeCuModal() {
            diagLabel.CloseModal();
        }
        function inputtxt(cc) {
            if (cc.value == "这里放入标签") {
                cc.value = '';
            }
        }
    </script> 
</asp:Content>
