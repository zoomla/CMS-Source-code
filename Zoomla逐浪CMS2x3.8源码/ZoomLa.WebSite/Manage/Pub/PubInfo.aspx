<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="PubInfo.aspx.cs" Inherits="Manage_I_Pub_PubInfo" %>
<%@ Register Src="~/Manage/I/ASCX/TlpDP.ascx" TagPrefix="ZL" TagName="TlpDown" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/JS/chinese.js"></script>
    <title>互动模块配置</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <table id="table" class="table table-bordered">
        <tr>
            <td class="title text-center" colspan="2">
                <asp:Label ID="Lbtitle" runat="server" Text="添加互动模块"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="PubNames">
            <td class="td_l">
                <strong>互动模块名称：</strong>
            </td>
            <td>
                <asp:TextBox ID="PubName" runat="server" CssClass="form-control text_300" onchange="checkmodelname(this)"></asp:TextBox>
                &nbsp;<font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="PubName" ErrorMessage="互动模块名称不能为空!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr runat="server" id="Tr1">
            <td>
                <strong>模型表名：</strong>
            </td>
            <td>&nbsp;<span >ZL_Pub_</span><asp:TextBox ID="PubTableName" onchange="checkmodelname(this)" runat="server" Width="101px" CssClass="form-control" />
                &nbsp;<asp:DropDownList ID="ModelList_DP" DataTextField="PubTableName" DataValueField="PubTableName" onchange="setmodelname(this)" runat="server" CssClass="form-control text_md"></asp:DropDownList>
                &nbsp;<font color="red">*</font>
                <span id="modelname_span"></span>
                <asp:RequiredFieldValidator ID="R1" runat="server" ControlToValidate="PubTableName" ErrorMessage="模型名称不能为空!" />
            </td>
        </tr>
        <tr runat="server" id="PubClasss">
            <td>
                <strong>信息类别：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="PubClass" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="6">首页</asp:ListItem>
                    <asp:ListItem Value="5">节点</asp:ListItem>
                    <asp:ListItem Selected="True" Value="0">内容</asp:ListItem>
                    <asp:ListItem Value="1">商城</asp:ListItem>
                    <asp:ListItem Value="2">黄页</asp:ListItem>
                    <asp:ListItem Value="3">店铺</asp:ListItem>
                    <asp:ListItem Value="4">会员</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr id="PubTypes" runat="server">
            <td >
                <strong>互动类型：</strong><span style="color:red;">(不可修改)</span></td>
            <td>
                <asp:RadioButtonList ID="PubType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">评论</asp:ListItem>
                    <asp:ListItem Value="1">投票</asp:ListItem>
                    <asp:ListItem Value="2">活动</asp:ListItem>
                    <asp:ListItem Value="3">留言</asp:ListItem>
                    <asp:ListItem Value="4">问券调查</asp:ListItem>
                    <asp:ListItem Value="5">统计</asp:ListItem>
                    <asp:ListItem Value="6">竞标</asp:ListItem>
                    <asp:ListItem Value="7">评星</asp:ListItem>
                    <asp:ListItem Value="8">表单</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr id="starOPTr" runat="server" style="display:none;">
            <td>
                <strong>选项：</strong>
            </td>
            <td>
                <asp:TextBox runat="server" ID="starOPT" Text="包装满意度,送货满意度,配送满意度" Width="600" />
            </td>
        </tr>
        <tr id="Tr2" runat="server">
            <td >
                <strong>前台权限：</strong>
            </td>
            <td>
                <label><asp:CheckBox ID="CheckBox1" runat="server" onclick="checkAll(this)" /> 可查看</label>
                <label><asp:CheckBox ID="CheckBox2" runat="server" onclick="check(this)" /> 可修改</label>
                <label><asp:CheckBox ID="CheckBox3" runat="server" onclick="check(this)" /> 可删除</label>
                <label><asp:CheckBox ID="CheckBox4" runat="server" onclick="check(this)" /> 可审核</label>
            </td>
        </tr>
        <tr runat="server" id="PubOneOrMores">
            <td>
                <strong>用户重复提交次数：</strong>
            </td>
            <td>
                <span style="width: auto; float: left">
                    <asp:RadioButtonList ID="PubOneOrMore" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">无限制</asp:ListItem>
                        <asp:ListItem Value="1">每人限一次</asp:ListItem>
                        <asp:ListItem Value="3">每篇文章限一次</asp:ListItem>
                        <asp:ListItem Value="2">自定义</asp:ListItem>
                    </asp:RadioButtonList>
                </span>
                <span style="width: auto; float: left; margin-left: 10px">
                    <asp:TextBox ID="PubOneOrMorenum" style="display:none;" runat="server" Width="69px" CssClass="form-control pull-left">0</asp:TextBox>
                </span>
            </td>
        </tr>
        <tr runat="server" id="PubIPOneOrMores">
            <td>
                <strong>重复IP提交次数：</strong>
            </td>
            <td>
                <span style="width: auto; float: left">
                    <asp:RadioButtonList ID="PubIPOneOrMore" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">无限制</asp:ListItem>
                        <asp:ListItem Value="1">每IP只限一次</asp:ListItem>
                        <asp:ListItem Value="2">自定义</asp:ListItem>
                    </asp:RadioButtonList>
                </span>
                <span style="width: auto; float: left; margin-left: 10px">
                    <asp:TextBox ID="PubIPOneOrMorenum" runat="server" Visible="False" Width="69px" CssClass="form-control pull-left">0</asp:TextBox>
                </span>
            </td>
        </tr>
        <tr>
            <td>
                <strong>cookie验证：</strong>
            </td>
            <td>
                <input type="checkbox" runat="server" id="pubflag" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td><strong>隔多久可重复提交：</strong></td>
            <td>
                <asp:TextBox runat="server" CssClass="form-control text_s" ID="Interval_T" Text="0"/>
                <span class="rd_green">单位：小时,为0则不启用该项</span></td>
        </tr>
        <tr runat="server" id="DivPuberrmsg">
            <td >
                <strong>达到提交次数提示：</strong>
            </td>
            <td>
                <span style="width: auto; float: left">
                    <asp:TextBox ID="Puberrmsg" runat="server" Width="349px" CssClass="form-control pull-left">该用户提交信息的数量已经达到上限!</asp:TextBox></span></td>
        </tr>
        <tr runat="server" id="Tr4">
            <td>
                <strong>提交完毕跳转地址：</strong>
            </td>
            <td>
                <span style="width: auto; float: left">
                    <asp:TextBox ID="PubGourl" runat="server" CssClass="form-control text_300"></asp:TextBox><span class="rd_green">为空则默认跳转</span></span></td>
        </tr>
        <tr runat="server" id="Tr3">
            <td align="left" ><strong>提交时间间隔：</strong></td>
            <td align="left">
                <asp:TextBox ID="PubTimeSlot" runat="server" Text="10" CssClass="form-control text_s" />
                <span class="rd_green">单位：秒</span>
            </td>
        </tr>
        <tr runat="server" id="Tr5">
            <td>
                <strong>保留天数信息：</strong>
            </td>
            <td>
                <asp:TextBox ID="Pubkeep" runat="server" Width="100px" Text="99999999" CssClass="form-control text_s" />
                <span class="rd_green">单位：天</span>
            </td>
        </tr>
        <tr runat="server" id="PubTemplates">
            <td>
                <strong>互动模板地址：</strong>
            </td>
            <td>
                <%=PageCommon.GetTlpDP("PubTemplate") %>
                <asp:HiddenField ID="PubTemplate_hid" runat="server" />
                <span class="rd_green">添加时如果为空则自动创建</span>
            </td>
        </tr>
        <tr id="PubLoadstrs" runat="server">
            <td>
                <strong>自定义调用互动标签：</strong>
            </td>
            <td>
                <span>{Pub.Load_</span>
                <asp:TextBox ID="PubLoadstr" CssClass="form-control text_md" runat="server" AutoPostBack="True" OnTextChanged="PubLoadstr_TextChanged"></asp:TextBox>
                <span>/}</span>&nbsp;
                <span class="rd_red">*</span><span>
                    <span class="rd_green">设置标签或模板中调用的标签</span>
                    <asp:Label ID="Label4" runat="server" CssClass="rd_red"></asp:Label>
                </span>
            </td>
        </tr>
        <tr runat="server" id="PubIsTrues">
            <td>
                <strong>是否需要审核：</strong>
            </td>
            <td>
                <input type="checkbox" runat="server" id="PubIsTrue" class="switchChk" />
            </td>
        </tr>
        <tr runat="server" id="PubCodes">
            <td>
                <strong>是否需要验证码：</strong>
            </td>
            <td>
                <input type="checkbox" runat="server" id="PubCode" class="switchChk" />
            </td>
        </tr>
        <tr runat="server" id="PubLogins">
            <td>
                <strong>是否需要登录：</strong>
            </td>
            <td>
                <input type="checkbox" runat="server" id="PubLogin" class="switchChk" onchange="$('#PubLogin_Btn').click();" />
                <asp:Button runat="server" ID="PubLogin_Btn" Style="display: none;" OnClick="PubLogin_SelectedIndexChanged" />
            </td>
        </tr>
        <tr runat="server" id="Tr6">
            <td>
                <strong>是否公开：</strong>
            </td>
            <td>
                <input type="checkbox" runat="server" id="RaPublic" class="switchChk" checked="checked" />
            </td>
        </tr>
        <tr runat="server" id="PubLoginUrls">
            <td>
                <strong>跳转登录地址(http://)：</strong>
            </td>
            <td>
                <asp:TextBox ID="PubLoginUrl" runat="server" CssClass="form-control pull-left text_300"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server" id="PubOpenComments">
            <td class="style1">
                <strong>是否开启前台管理：</strong>
            </td>
            <td class="style1">
                <input type="checkbox" runat="server" id="PubOpenComment" class="switchChk" />
            </td>
        </tr>
        <tr id="PubShowTypes" style="display: none">
            <td >
                <strong>生成投票显示：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="PubShowType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">无</asp:ListItem>
                    <asp:ListItem Value="1">柱状</asp:ListItem>
                    <asp:ListItem Value="2">圆形</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr runat="server" id="PubEndTimes">
            <td>
                <strong>结束时间：</strong>
            </td>
            <td>
                <asp:TextBox ID="PubEndTime" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });" CssClass="form-control text_300"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="PubEndTime" ErrorMessage="结束时间必须是日期!" ValidationExpression="^(\d{4}[\-]?\d{1,2}[\-]?\d{1,2})?([ ]?)(\d{1,2}[:]?\d{1,2}[:]?\d{1,2})?$" SetFocusOnError="True" Display="Dynamic" />
                <span class="rd_green">不限制请不要填写</span>
            </td>
        </tr>
        <tr id="PubInputTMs" runat="server">
            <td>
                <strong>自定义信息提交模板：</strong>
            </td>
            <td>
                <%=PageCommon.GetTlpDP("PubInputTM") %>
                <asp:HiddenField ID="PubInputTM_hid" runat="server" />
                <span class="rd_green">添加时如果为空则自动创建</span>
            </td>
        </tr>
        <tr id="PubInputLoadStrs" runat="server">
            <td>
                <strong>调用信息提交标签：</strong>
            </td>
            <td>
                <span>{Pub.</span><asp:TextBox ID="PubInputLoadStr" CssClass="form-control text_md" runat="server" AutoPostBack="True" OnTextChanged="PubInputLoadStr_TextChanged"></asp:TextBox>
                <span>/}</span><span class="rd_red">*</span><span class="rd_green">设置标签或模板中调用的标签</span><asp:Label ID="Label3" runat="server" class="rd_red"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="Pubinfos">
            <td>
                <strong>互动描述：</strong>
            </td>
            <td>
                <asp:TextBox ID="Pubinfo" runat="server" CssClass="form-control text_300" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-center" colspan="2">
                <div id="save">
                    <div>
                        <asp:Button ID="Submit_B" runat="server" Text="提交" OnClick="Submit_B_Click" CssClass="btn btn-primary" />
                        <a href="pubmanage.aspx" class="btn btn-primary">返回</a>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" class="C_input" />
                    </div>
                </div>
            </td>
        </tr>
    </table> 
    <ZL:TlpDown runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">    
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        $().ready(function () {
            Tlp_initTemp();
            EventTypeChange();
            initPubType($("#PubType input:checked").val());
            initPubOne($("#PubOneOrMore input:checked").val());
        });
        //显示自定义重复提交次数
        function initPubOne(value) {
            if (value == "2")
                $("#PubOneOrMorenum").show();
            else
                $("#PubOneOrMorenum").hide();
        }
        function initPubType(value) {
            switch (value) {
                case "1":
                    $("#PubShowTypes").show();
                    break;
                case "5":
                    $("#PubOpenComments").hide();
                    $("#PubOneOrMores").show();
                    $("#PubIsTrues").show();
                    $("#PubCodes").hide();
                    $("#PubLogins").hide();
                    break;
                case "7":
                    $("#starOPTr").show();
                    break;
                default:
                    $("#PubShowTypes").hide();
                    $("#PubOpenComments").show();
                    $("#PubOneOrMores").show();
                    $("#PubIsTrues").show();
                    $("#PubCodes").show();
                    $("#PubLogins").show();
                    $("#starOPTr").hide();
                    break;
            }
        }
        //绑定互动类型选择事件
        function EventTypeChange() {
            $("#PubType input").change(function () {
                var value = $(this).val();
                initPubType(value);
            });
            $("#PubOneOrMore input").change(function () {
                initPubOne($(this).val());
            });
        }
        function checkmodelname(obj) {
            //modelname_span
            //PubName
            if ($(obj).val() == "")
                return;
            $.post("PubInfo.aspx", { action: 'modelname', value: $(obj).val() }, function (result) {
                $("#modelname_span").html(result);
            })
        }
        function setmodelname(obj) {
            //PubTableName
            $("#PubTableName").val($(obj).find("option:checked").val().replace("ZL_Pub_",""));
        }
        function check(obj) {
            chkArr1 = $(obj).parent().find(":checkbox[name=CheckBox1]");
            chkArr2 = $(obj).parent().find(":checkbox[name=CheckBox2]");
            chkArr3 = $(obj).parent().find(":checkbox[name=CheckBox3]");
            chkArr4 = $(obj).parent().find(":checkbox[name=CheckBox4]");
            if (!chkArr2[0].checked && !chkArr3[0].checked && !chkArr4[0].checked) {
                chkArr1[0].checked = false;
            }
            else {
                chkArr1[0].checked = true;
            }
        }
        function checkAll(obj) {
            chkArr2 = $(obj).parent().find(":checkbox[name=CheckBox2]");
            chkArr3 = $(obj).parent().find(":checkbox[name=CheckBox3]");
            chkArr4 = $(obj).parent().find(":checkbox[name=CheckBox4]");
            if (!obj.checked) {
                chkArr2[0].checked = false;
                chkArr3[0].checked = false;
                chkArr4[0].checked = false;
            }
        }
        function ShowDiag(url, title) {
            ShowComDiag(url, title);
        }
        function CloseDialog() {
            CloseComDiag();
        }
    </script>
</asp:Content>
