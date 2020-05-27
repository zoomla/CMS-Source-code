<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="User.AddUser" Title="添加会员" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
    <script" src="/dist/js/bootstrap-switch.js"></script>
    <title>添加会员</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="4" class="text-center">添加会员 </td>
            </tr>
            <tr>
                <td class="text-right td_l" >用户名： </td>
                <td>
                    <ZL:TextBox runat="server" ID="tbUserName" TabIndex="1" onkeydown="return GetEnterCode('focus','WorkNum_T');" CssClass="form-control text_md" AllowEmpty="false" ValidType="String"></ZL:TextBox>
                    <span style="color: red;">*</span>
                    <div id="check" runat="server"></div></td>
                 <td class="text-right td_l">工号： </td>
                <td>
                    <asp:TextBox ID="WorkNum_T" Cssclass="form-control text_md" TabIndex="2"  onkeydown="return GetEnterCode('focus','txtpassword');" runat="server"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td class="text-right">用户密码： </td>
                <td colspan="3">
                    <ZL:TextBox runat="server" ID="txtpassword" TabIndex="1" onkeydown="return GetEnterCode('focus','WorkNum_T');" CssClass="form-control text_md" AllowEmpty="false" ValidType="Custom" ValidExpressionCustom="[\s\S]{6,}" ValidError="密码不能少于6位"></ZL:TextBox>
                    <span style="color: red;">*</span>
                    <input type="button" value="随机密码" onclick="SetPassWord('txtpassword');" class="btn btn-primary" />
                </td>
            </tr>
           
            <tr>
                <td class="text-right">E-mail： </td>
                <td class="style2" style="width: 35%;">
                    <ZL:TextBox runat="server" ID="tbEmail" TabIndex="4" onkeydown="return GetEnterCode('focus','txtHoneyName');" CssClass="form-control text_md" AllowEmpty="false" ValidType="Mail"></ZL:TextBox>
                    <span class="red">*</span>
                    <span class="red existemail" style="display:none;" >邮箱已被注册!</span>
                </td>
                <td class="text-right">呢称： </td>
                <td>
                    <asp:TextBox ID="txtHoneyName" runat="server" TabIndex="5" onkeydown="return GetEnterCode('focus','tbQuestion');" class="form-control text_md"></asp:TextBox>
            </tr>
            <tr>
                <td class="text-right">提示问题： </td>
                <td style="width: 514px;">
                    <ZL:TextBox runat="server" ID="tbQuestion" TabIndex="6" onkeydown="return GetEnterCode('focus','tbAnswer');" CssClass="form-control text_md" AllowEmpty="false"></ZL:TextBox>
                    <span style="color: red">*</span>
                </td>
                <td width="224" class="text-right">VIP类型： </td>
                <td width="515" style="width: 35%;">
                    <input type="checkbox" runat="server" id="VIPUser" class="switchChk"  />
            </tr>
            <tr>
                <td class="text-right">提示问题答案： </td>
                <td style="width: 35%;">
                    <ZL:TextBox ID="tbAnswer" runat="server"  TabIndex="7" onkeydown="return GetEnterCode('focus','DDLGroup');" class="form-control text_md" AllowEmpty="false"></ZL:TextBox>
                    <span class="red">*</span>

                </td>
                <td class="text-right">所属会员组： </td>
                <td style="width: 35%;">
                    <asp:DropDownList ID="DDLGroup" runat="server"  TabIndex="8" onkeydown="return GetEnterCode('focus','tbTrueName');" class="btn btn-default dropdown-toggle"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="text-right">真实姓名： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbTrueName" runat="server" class="form-control text_md" TabIndex="9" onkeydown="return GetEnterCode('focus','tbUserSex');"></asp:TextBox></td>
                <td class="text-right">性别： </td>
                <td style="width: 35%;">
                    <asp:RadioButtonList ID="tbUserSex" runat="server"  TabIndex="10" onkeydown="return GetEnterCode('focus','tbBirthday');" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                        <asp:ListItem Value="0">女</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="text-right">出生日期： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbBirthday" runat="server" onblur="setday(this);" TabIndex="11" onkeydown="return GetEnterCode('focus','tbOfficePhone');" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' });" class="form-control text_md"></asp:TextBox></td>
                <td class="text-right">办公电话： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbOfficePhone" runat="server" class="form-control text_md" TabIndex="12" onkeydown="return GetEnterCode('focus','tbHomePhone');"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">家庭电话： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbHomePhone" runat="server" class="form-control text_md"  TabIndex="13" onkeydown="return GetEnterCode('focus','tbFax');"></asp:TextBox></td>
                <td style="width:15%;" class="text-right">推荐人：</td>
                <td style="width:35%;">
                    <asp:TextBox ID="ParentUser_T" runat="server" Cssclass="form-control text_md" TabIndex="14"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right">手机号码： </td>
                <td style="width: 35%;">
                    <ZL:TextBox ID="tbMobile" runat="server" class="form-control text_md" TabIndex="15" onkeydown="return GetEnterCode('focus','tbPHS');" ValidType="MobileNumber" ></ZL:TextBox>
                </td>
                <td class="text-right">小灵通： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbPHS" runat="server" class="form-control text_md"  TabIndex="16" onkeydown="return GetEnterCode('focus','tbAddress');"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">联系地址： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbAddress" runat="server" Columns="40" class="form-control text_md"  TabIndex="17" onkeydown="return GetEnterCode('focus','tbZipCode');" ></asp:TextBox></td>
                <td class="text-right">邮政编码： </td>
                <td style="width: 35%;">
                    <ZL:TextBox ID="tbZipCode" runat="server" class="form-control text_md"  TabIndex="18" onkeydown="return GetEnterCode('focus','tbIDCard');" ValidType="PostalCode" ></ZL:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">身份证号码： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbIDCard" runat="server" Columns="40" class="form-control text_md"  TabIndex="19" onkeydown="return GetEnterCode('focus','tbHomepage');"></asp:TextBox></td>
                <td class="text-right">个人主页： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbHomepage" runat="server" Columns="40" class="form-control text_md" TabIndex="20" onkeydown="return GetEnterCode('focus','tbQQ');">http://</asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">QQ号码： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbQQ" runat="server" class="form-control text_md" TabIndex="21" onkeydown="return GetEnterCode('focus','tbFax');"></asp:TextBox></td>
                <td class="text-right">传真： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbFax" runat="server" class="form-control text_md" TabIndex="14" onkeydown="return GetEnterCode('focus','tbMobile');"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">UC帐号： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbUC" runat="server" class="form-control text_md" TabIndex="25" onkeydown="return GetEnterCode('focus','tbUserFace');" Style="max-width:200px;"></asp:TextBox></td>
                <td style="width: 15%; height: 26px;" class="text-right">头像地址： </td>
                <td style="width: 35%; height: 26px;">
                    <asp:TextBox ID="tbUserFace" runat="server" TabIndex="26" onkeydown="return GetEnterCode('focus','tbFaceWidth');" class="form-control text_md"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">头像宽度： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbFaceWidth" runat="server" TabIndex="27" onkeydown="return GetEnterCode('focus','tbFaceHeight');" class="form-control text_md">16</asp:TextBox></td>
                <td class="text-right">头像高度： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbFaceHeight" runat="server"  TabIndex="28" onkeydown="return GetEnterCode('focus','tbSign');" class="form-control text_md">16</asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 15%;; line-height: 70px;" class="text-right">签名档： </td>
                <td style="width: 35%;">
                    <asp:TextBox ID="tbSign" runat="server" class="form-control text_md" TabIndex="29" onkeydown="return GetEnterCode('focus','btnSave');" TextMode="MultiLine" Width="300" Height="60"></asp:TextBox></td>
                <td style="width: 15%;; line-height: 70px;" class="text-right">隐私设定： </td>
                <td style="width: 35%;">
                    <asp:RadioButtonList ID="tbPrivacy" TabIndex="30" onkeydown="return GetEnterCode('focus','lblHtml');" runat="server">
                        <asp:ListItem Selected="True" Value="0">公开信息</asp:ListItem>
                        <asp:ListItem Value="1">只对好友公开</asp:ListItem>
                        <asp:ListItem Value="2">完全保密</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <asp:Literal ID="lblHtml" runat="server"></asp:Literal>
            <tr>
                <td style="height: 21px" colspan="4" class="text-center">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" TabIndex="31"  Text="保存会员信息" OnClick="btnSave_Click"/>
                    <asp:Button ID="btnCancel" CssClass="btn btn-primary" runat="server" TabIndex="32" Text="取消" OnClientClick="location.href='UserManage.aspx';return false;" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/Modal/APIResult.js"></script>
    <script>
        function SetPassWord(id) {
            $("#" + id).val(GetRanPass(6));
        }
        $(function () {
            SetPassWord('txtpassword');
            //existemail
            $("#tbEmail").change(function () {
                $.post("/API/UserCheck.ashx", { action: "ExistEmail", email: $(this).val() }, function (data) {
                    var model = APIResult.getModel(data);
                    if (APIResult.isok(model)) {
                        $(".existemail").hide();
                        $("#btnSave").removeAttr("disabled");
                    }
                    else {
                        $(".existemail").show();
                        $("#btnSave").attr("disabled", "disabled");
                    }
                })
            });
        })
    </script>
</asp:Content>
