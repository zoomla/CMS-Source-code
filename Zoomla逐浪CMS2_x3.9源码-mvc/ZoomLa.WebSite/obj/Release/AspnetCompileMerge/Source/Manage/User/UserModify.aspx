<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserModify.aspx.cs" Inherits="ZoomLaCMS.Manage.User.UserModify"  MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
<title>修改用户信息</title>
<script src="/JS/ICMS/area.js"></script>
<script  src="/JS/Controls/ZL_PCC.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs">
        <li class="active"><a href="#Tabs0" onclick="tab1()" data-toggle="tab">基本信息</a></li>
        <li><a href="#Tabs1" data-toggle="tab" onclick="tab2()">联系信息</a></li>
        <li><a href="#Tabs2" data-toggle="tab" onclick="tab3()">其他信息</a></li>
        <li><a id="platInfo_A" href="#Tabs3" data-toggle="tab" visible="false" runat="server">能力中心信息</a></li>
        <li><a href="#Tabs4" data-toggle="tab">权限角色</a></li>
    </ul>
    <!-- Tab panes -->
    <div class="tab-content">
        <div class="tab-pane active" id="Tabs0">
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td class="text-right td_l">用户名： </td>
                    <td class="text-left text_405">
                        <asp:Label ID="lblUser" runat="server" Text="Label"></asp:Label></td>
                    <td class="text-right td_l">所属会员组： </td>
                    <td class="text-left text_405">
                        <asp:DropDownList ID="DDLGroup" CssClass="form-control" onkeydown="return GetEnterCode('focus','DDLGroup');" Width="150" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="text-right">新密码： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbNewPwd" runat="server" onkeydown="return GetEnterCode('focus','tbConPwd');" class="form-control" Style="max-width: 200px;"></asp:TextBox>
                        <input type="button" value="随机密码" onclick="SetPassWord('tbNewPwd');" class="btn btn-primary" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server" ErrorMessage="密码不能少于6位" ControlToValidate="tbNewPwd" ValidationExpression="[\s\S]{6,}" ForeColor="Red" />
                        <div>如果不修改密码，请不要输入新密码和确认密码</div>
                    </td>
                    <td class="text-right">用户昵称： </td>
                    <td colspan="3" class="text-left">
                        <ZL:TextBox ID="txtHoneyName" runat="server" onkeydown="return GetEnterCode('focus','WorkNum_T');" CssClass="form-control" AllowEmpty="false" Style="max-width: 200px;" />
                    </td>

                </tr>
                <tr>
                    <td class="text-right">新密码确认： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbConPwd" runat="server" TextMode="Password" onkeydown="return GetEnterCode('focus','tbEmail');" class="form-control" Style="max-width: 200px;"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="新密码和确认密码必须一致" ControlToValidate="tbConPwd" ControlToCompare="tbNewPwd" CssClass="red"></asp:CompareValidator>
                    </td>
                    <td class="text-right">VIP类型： </td>
                    <td class="text-left">
                        <asp:DropDownList ID="VIPUser" onkeydown="return GetEnterCode('focus','DDLGroup');" CssClass="form-control" Width="80" runat="server">
                            <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                            <asp:ListItem Value="1">是</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="text-right">Email： </td>
                    <td class="text-left">
                        <ZL:TextBox ID="tbEmail" runat="server" onkeydown="return GetEnterCode('focus','tbQuestion');" CssClass="form-control" AllowEmpty="false" ValidType="Mail" Style="max-width: 200px;" />
                        <asp:HiddenField ID="OldEmail_Hid" runat="server" />
                        <span style="color: red">*</span>
                        <span class="red existemail" style="display: none;">邮箱已被注册!</span>
                    </td>
                    <td class="text-right">工号： </td>
                    <td>
                        <asp:TextBox ID="WorkNum_T" runat="server" onkeydown="return GetEnterCode('focus','tbNewPwd');" class="form-control text_md"></asp:TextBox>

                    </td>
                </tr>
                <tr class="tdbg">
                    <td class="text-right">剩余资金： </td>
                    <td class="text-left">
                        <asp:TextBox ID="txtMoney" runat="server" class="form-control" Style="max-width: 200px;"></asp:TextBox>
                    </td>
                    <td class="text-right">用户信誉值</td>
                    <td>
                        <asp:TextBox ID="txtUserCrite" runat="server" class="form-control" Style="max-width: 200px;"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="text-right">用户银币：</td>
                    <td class="text-left">
                        <asp:TextBox ID="txtSilverCoin" runat="server" class="form-control" Style="max-width: 200px;"></asp:TextBox>
                    </td>
                    <td class="text-right">用户积分： </td>
                    <td class="text-left">
                        <asp:TextBox ID="txtPoint" runat="server" class="form-control" Style="max-width: 200px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="text-right">点券： </td>
                    <td class="text-left">
                        <asp:TextBox ID="txtUserPoint" runat="server" class="form-control" Style="max-width: 200px;"></asp:TextBox>
                    </td>
                    <td class="text-right">虚拟币： </td>
                    <td class="text-left">
                        <asp:TextBox ID="txtPurm" runat="server" class="form-control" Style="max-width: 200px;"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="text-right">提示问题： </td>
                    <td class="text-left">
                        <ZL:TextBox ID="tbQuestion" runat="server" onkeydown="return GetEnterCode('focus','tbAnswer');"  CssClass="form-control text_md" AllowEmpty="false" /><span style="color: red">*</span>
                    </td>
                    <td class="text-right">问题答案： </td>
                    <td class="text-left">
                        <ZL:TextBox ID="tbAnswer" runat="server" onkeydown="return GetEnterCode('focus','BtnSubmit');" CssClass="form-control text_md" AllowEmpty="false" />
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="text-right">消费积分： </td>
                    <td class="text-left">
                        <asp:TextBox ID="txtConsumeExp" runat="server" onkeydown="return GetEnterCode('focus','tbSign');" class="form-control" Style="max-width: 200px;"></asp:TextBox>
                    </td>
                    <td class="text-right">卖家积分： </td>
                    <td class="text-left">
                        <asp:TextBox ID="txtboffExp" runat="server" onkeydown="return GetEnterCode('focus','txtConsumeExp');" class="form-control" Style="max-width: 200px;"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="tab-pane" id="Tabs1">
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td class="text-right">真实姓名： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbTrueName" runat="server" onkeydown="return GetEnterCode('focus','tbBirthday');" class="form-control" Style="max-width: 200px;"></asp:TextBox></td>
                    <td class="text-right">性别： </td>
                    <td class="text-left">
                        <asp:RadioButtonList ID="tbUserSex" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                            <asp:ListItem Value="0">女</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td class="text-right">出生日期： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbBirthday" runat="server" onkeydown="return GetEnterCode('focus','tbOfficePhone');" onblur="setday(this);" onclick="WdatePicker()"
                            class="form-control text_md"></asp:TextBox></td>
                    <td class="text-right">办公电话： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbOfficePhone" runat="server" onkeydown="return GetEnterCode('focus','tbProvince');" class="form-control" Style="max-width: 200px;"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="text-right">家庭电话： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbHomePhone" runat="server" onkeydown="return GetEnterCode('focus','tbFax');" class="form-control" Style="max-width: 200px;"></asp:TextBox></td>
                    <td class="text-right">传真： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbFax" runat="server" class="form-control" onkeydown="return GetEnterCode('focus','tbMobile');" Style="max-width: 200px;"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="text-right">手机号码： </td>
                    <td class="text-left">
                        <ZL:TextBox ID="tbMobile" runat="server" onkeydown="return GetEnterCode('focus','tbPHS');" MaxLength="13" class="form-control" ValidType="MobileNumber" Style="max-width: 200px;" />
                    </td>
                    <td class="text-right">小灵通： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbPHS" runat="server" onkeydown="return GetEnterCode('focus','tbAddress');" class="form-control" Style="max-width: 200px;"></asp:TextBox></td>
                </tr>
                <tr class="tdbg">
                    <td class="text-right">省市县：  </td>
                    <td class="text-left">
                        <select id="selprovince" class="form-control text_s" name="selprovince">
                        </select>
                        <select id="selcity" class="form-control text_s" name="selcity">
                        </select>
                        <select id="selcoutry" class="form-control text_s" name="selcoutry">
                        </select><asp:HiddenField ID="Adress_Hid" runat="server" />
                    </td>
                    <td class="text-right">推荐人(ID)： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbParentUserID" runat="server" onkeydown="return GetEnterCode('focus','tbHomePhone');" class="form-control text_md"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="text-right">联系地址： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbAddress" runat="server" Columns="40" onkeydown="return GetEnterCode('focus','tbZipCode');" class="form-control" Style="max-width: 200px;"></asp:TextBox></td>
                    <td class="text-right">邮政编码： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbZipCode" runat="server" class="form-control" onkeydown="return GetEnterCode('focus','tbIDCard');" Style="max-width: 200px;"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="text-right">身份证号码： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbIDCard" runat="server" Columns="40" onkeydown="return GetEnterCode('focus','tbHomepage');" class="form-control" Style="max-width: 200px;"></asp:TextBox></td>
                    <td class="text-right">个人主页： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbHomepage" runat="server" onkeydown="return GetEnterCode('focus','tbQQ');" Columns="40" class="form-control" Style="max-width: 200px;">http://</asp:TextBox></td>
                </tr>
                <tr>
                    <td class="text-right">QQ号码： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbQQ" runat="server" onkeydown="return GetEnterCode('focus','tbICQ');" class="form-control" Style="max-width: 200px;"></asp:TextBox></td>
                    <td class="text-right">ICQ号码： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbICQ" runat="server" class="form-control" onkeydown="return GetEnterCode('focus','tbMSN');" Style="max-width: 200px;"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="text-right">MSN帐号： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbMSN" runat="server" class="form-control" onkeydown="return GetEnterCode('focus','tbYahoo');" Style="max-width: 200px;"></asp:TextBox></td>
                    <td class="text-right">雅虎通帐号： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbYahoo" runat="server" class="form-control" onkeydown="return GetEnterCode('focus','tbUC');" Style="max-width: 200px;"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="text-right">UC帐号： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbUC" runat="server" class="form-control" onkeydown="return GetEnterCode('focus','tbUserFace');" Style="max-width: 200px;"></asp:TextBox></td>
                    <td style="width: 15%; height: 26px;" class="text-right">头像地址： </td>
                    <td style="width: 35%; height: 26px;" class="text-left">
                        <asp:TextBox ID="tbUserFace" runat="server" class="form-control" onkeydown="return GetEnterCode('focus','tbFaceWidth');" Style="max-width: 200px;"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="text-right">头像宽度： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbFaceWidth" runat="server" class="form-control" onkeydown="return GetEnterCode('focus','tbFaceHeight');" Style="max-width: 200px;">16</asp:TextBox></td>
                    <td class="text-right">头像高度： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbFaceHeight" runat="server" class="form-control" onkeydown="return GetEnterCode('focus','CompanyNameT');" Style="max-width: 200px;">16</asp:TextBox></td>
                </tr>
                <tr>
                    <td class="text-right">公司名称： </td>
                    <td class="text-left">
                        <asp:TextBox ID="CompanyNameT" runat="server" class="form-control" onkeydown="return GetEnterCode('focus','CompanyDescribeT');" Style="max-width: 200px;" Columns="40"></asp:TextBox>
                    </td>
                    <td class="text-right">公司简介： </td>
                    <td class="text-left">
                        <asp:TextBox ID="CompanyDescribeT" runat="server" TextMode="MultiLine" onkeydown="return GetEnterCode('focus','BtnSubmit');" Rows="4" Columns="28"></asp:TextBox></td>
                </tr>
            </table>
        </div>
        <div class="tab-pane" id="Tabs2">
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td class="text-right">商铺认证有效期截止： </td>
                    <td class="text-left">
                        <asp:TextBox ID="txtCerificateDeadLine" onkeydown="return GetEnterCode('focus','txtDeadLine');" runat="server" class="form-control" Style="max-width: 200px;" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' })"></asp:TextBox></td>
                    <td class="text-right">有效期截止时间： </td>
                    <td class="text-left">
                        <asp:TextBox ID="txtDeadLine" runat="server" onkeydown="return GetEnterCode('focus','txtboffExp');" class="form-control" Style="max-width: 200px;" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' })"></asp:TextBox></td>
                </tr>

                <tr>
                    <td class="text-right">签名档： </td>
                    <td class="text-left">
                        <asp:TextBox ID="tbSign" runat="server" onkeydown="return GetEnterCode('focus','BtnSubmit');" class="form-control" Style="max-width: 200px;" TextMode="MultiLine" Width="253px"
                            Height="60"></asp:TextBox></td>
                    <td class="text-right">隐私设定： </td>
                    <td class="text-left">
                        <asp:RadioButtonList ID="tbPrivacy" runat="server">
                            <asp:ListItem Selected="True" Value="0">公开信息</asp:ListItem>
                            <asp:ListItem Value="1">只对好友公开</asp:ListItem>
                            <asp:ListItem Value="2">完全保密</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td colspan="4" style="padding-left: 0;">
                        <table width="100%" border="0" cellpadding="0">
                            <asp:Literal ID="lblHtml" runat="server"></asp:Literal>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="tab-pane" id="Tabs3">
            <table class="table table-striped table-bordered table-hover padding0">
                <tr>
                    <td style="width: 15%;" class="text-right">真实姓名：</td>
                    <td style="width: 35%;">
                        <asp:TextBox ID="tbTrueName_T" CssClass="form-control text_md" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 15%;" class="text-right">所属公司：</td>
                    <td style="width: 35%;">
                        <asp:DropDownList ID="tbCompName_D" CssClass="form-control text_md" runat="server" DataTextField="CompName" DataValueField="ID"></asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td class="text-right">公司部门：</td>
                    <td><asp:Label runat="server" ID="tbPost_T"></asp:Label></td>
                    <td class="text-right"></td>
                    <td></td>
                </tr>
            </table>
        </div>
        <div class="tab-pane" id="Tabs4">
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td style="width: 200px;"><strong>管理员角色设置：</strong></td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cblRoleList" runat="server" /></td>
                </tr>
            </table>
        </div>
    </div>
    <asp:Literal ID="ltlTab" runat="server"></asp:Literal>
    <asp:Literal ID="models" runat="server"></asp:Literal>
    <div class="text-center" style="border: 1px solid #ddd; padding: 5px; margin-bottom: 10px;">
        <asp:Button ID="BtnSubmit" runat="server" Text="确认修改" OnClick="BtnSubmit_Click" class="btn btn-primary" />
        <asp:Button ID="Button1" runat="server" Text="返回上页" class="btn btn-primary" OnClick="Button1_Click" CausesValidation="False" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" class="btn btn-primary" />
    </div>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/Modal/APIResult.js"></script>
    <script>
        $().ready(function () {
            $("#txtMoney,#txtUserCrite,#txtSilverCoin,#txtPoint,#txtUserPoint,#txtPurm").attr("disabled",true);
            var pcc = new ZL_PCC("selprovince", "selcity", "selcoutry");
            var defstrs = $("#Adress_Hid").val().split(',');
            if (defstrs.length > 0)
                pcc.SetDef(defstrs[0], defstrs[1], defstrs[2]);
            pcc.ProvinceInit();
            pcc.$province.change(area);
            pcc.$city.change(area);
            pcc.$county.change(area);
            //检测用户邮箱
            $("#tbEmail").change(function () {
                if ($("#OldEmail_Hid").val() == $(this).val()) {
                    $(".existemail").hide();
                    $("#BtnSubmit").removeAttr("disabled");
                    return;
                }
                $.post("/API/UserCheck.ashx", { action: "ExistEmail", email: $(this).val() }, function (data) {
                    var model = APIResult.getModel(data);
                    if (APIResult.isok(model)) {
                        $(".existemail").hide();
                        $("#BtnSubmit").removeAttr("disabled");
                    }
                    else {
                        $(".existemail").show();
                        $("#BtnSubmit").attr("disabled", "disabled");
                    }
                })
            });
            $("[href='#<%=Request.QueryString["tabs"] %>']").tab('show');
        });
        function SetPassWord(id) {
            $("#" + id).val(GetRanPass(6));
        }
        function area() {
            $("#tbProvince").val($("#selprovince").val());
            $("#tbCity").val($("#selcity").val());
            $("#tbCoutry").val($("#selcoutry").val());
        }
        function tab2() {
            $("#tbTrueName").focus();
        }
        function tab1() {
            $("#txtHoneyName").focus();
        }
        function tab3() {
            $("#txtMoney").focus();
        }
    </script>
</asp:Content>

