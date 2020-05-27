<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserConfig.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.UserConfig" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>用户参数</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs">
        <li class="active"><a href="#Tabs0" data-toggle="tab"">基本参数</a></li>
<%--        <li><a href="#Tabs5" data-toggle="tab">扩展配置</a></li>--%>
        <li><a href="#Tabs1" data-toggle="tab">积分设置</a></li>
        <li><a href="#Tabs2" data-toggle="tab">积分兑换</a></li>
        <li><a href="#Tabs3" data-toggle="tab">点券设置</a></li>
        <li><a href="#Tabs4" data-toggle="tab">IP限制</a></li>
        <li><a href="#Tabs5" data-toggle="tab">邀请码配置</a></li>
    </ul>
    <div class="tab-content panel-body padding0">
        <div class="tab-pane active" id="Tabs0">
            <table class="table table-striped table-bordered table-hover myform-control">
                <tbody>
                    <tr>
                        <td style="width: 20%;"><strong>是否开启会员注册：</strong></td>
                        <td>
                            <input type="checkbox" runat="server" id="RadioButtonList1" class="switchChk" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>会员注册协议：</strong></td>
                        <td>
                            <asp:RadioButtonList ID="Agreement" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">云办公模式</asp:ListItem>
                                <asp:ListItem Value="3" Selected="True">倒计时式</asp:ListItem>
                                <asp:ListItem Value="2">选择勾选</asp:ListItem>
                                <asp:ListItem Value="1">常规注册</asp:ListItem>
                                <asp:ListItem Value="0">不显示</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>允许用户名注册规则：</strong></td>
                        <td>
                            <asp:CheckBoxList ID="RegRule" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">不允许纯数字</asp:ListItem>
                                <asp:ListItem Value="2">不允许纯英文</asp:ListItem>
                                <asp:ListItem Value="3">不允许带有中文</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>是否开启用户ID登录：</strong></td>
                        <td>
                            <input type="checkbox" runat="server" id="radioUserID" class="switchChk" />
                    </tr>
                    <tr>
                        <td><strong>手机校验码注册：</strong></td>
                        <td>
                            <input type="checkbox" runat="server" id="radioMobile" class="switchChk" /></td>
                    </tr>
                    <tr>
                        <td><strong>手机校验码位数：</strong></td>
                        <td>
                            <asp:TextBox runat="server" ID="MobileCodeNum_T" CssClass="form-control text_md int" MaxLength="2" /><span>个字符</span>
                            <span class="rd_green">最少必须4位</span></td>
                    </tr>
                    <tr>
                        <td><strong>手机校验码规则：</strong></td>
                        <td>
                            <label>
                                <input type="radio" value="2" name="mobilecode_rad" />纯数字</label>
                            <label>
                                <input type="radio" value="3" name="mobilecode_rad" />纯字母</label>
                            <label>
                                <input type="radio" value="0" name="mobilecode_rad" checked="checked" />混合式</label>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>会员注册默认状态：</strong></td>
                        <td>
                            <asp:RadioButtonList ID="UserValidateType_Rad" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">验证</asp:ListItem>
                                <asp:ListItem Value="1">未验证</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr runat="server" id="Tr5">
                        <td><strong>可使用站内短信功能用户组：</strong></td>
                        <td>
                            <asp:CheckBoxList ID="MessageGroup" runat="server" DataTextField="GroupName" DataValueField="GroupID"
                                RepeatColumns="5" RepeatDirection="Horizontal">
                            </asp:CheckBoxList></td>
                    </tr>
                    <tr>
                        <td><strong>是否开启注册Email验证：</strong></td>
                        <td>
                            <input type="checkbox" runat="server" id="rdoEmailCheck" class="switchChk" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>是否开启Email注册登录：</strong></td>
                        <td>
                            <input type="checkbox" runat="server" id="RadioButtonList2" class="switchChk" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>是否开启注册成功邮件提醒：</strong></td>
                        <td>
                            <input type="checkbox" runat="server" id="radioEmail" class="switchChk" />
                        </td>
                    </tr>

                    <tr>
                        <td><strong>是否开启用户注册管理员验证：</strong></td>
                        <td>
                            <input type="checkbox" runat="server" id="RadioButtonList3" class="switchChk" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>注册时是否启用验证码功能：<br />
                        </strong>可一定程度上防暴力营销软件或注册机自动注册。 </td>
                        <td>
                            <input type="checkbox" runat="server" id="RadioButtonList5" class="switchChk" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>登录是否启用验证码功能：<br />
                        </strong>可一定程度上防止会员密码被暴力破解 </td>
                        <td>
                            <input type="checkbox" runat="server" id="RadioButtonList6" class="switchChk" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>是否允许多人同时使用同一会员帐号登录：</strong></td>
                        <td>
                            <input type="checkbox" runat="server" id="RadioButtonList7" class="switchChk" />
                        </td>
                    </tr>
             <%--       <tr>
                        <td><strong>启用Discuz!NT论坛：</strong></td>
                        <td>
                            <input type="checkbox" runat="server" id="DisCuzNT" class="switchChk" />
                                         
                        </td>
                    </tr>--%>
                    <tr>
                        <td><strong>新会员注册时用户名最少字符数：</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server" class="form-control"></asp:TextBox>
                            个字符
				        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox6" Display="Dynamic" ErrorMessage="最少字符数不能为空"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBox6" ErrorMessage="最少字符数必须大于等于2" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="2" Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td><strong>新会员注册时用户名最多字符数：</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox7" runat="server" class="form-control"></asp:TextBox>
                            个字符
				        <asp:RequiredFieldValidator ID="ReqTxtUserNameMax" runat="server" Display="Dynamic" ErrorMessage="最多字符数不能为空" ControlToValidate="TextBox7" />
                            <asp:CompareValidator ID="CValTxtUserNameMax" runat="server" ControlToValidate="TextBox7" ControlToCompare="TextBox6" Type="Integer" Operator="GreaterThanEqual" ErrorMessage="最多字符数必须大于等于最小字符数" Display="Dynamic" SetFocusOnError="true" /></td>
                    </tr>
                    <tr>
                        <td><strong>禁止注册的用户名：<br />
                        </strong>每个用户名请用“|”符号分隔 </td>
                        <td>
                            <asp:TextBox ID="TextBox8" runat="server" Rows="6" TextMode="MultiLine" Width="300px" class="form-control"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td><strong>注册时的必填、选填项目：</strong><br />
                            可根据需要设定注册的必填项、选填项。<br />
                            将“可用项”中内容添加到“必填项”或者“选填项”的列表中即可。<br />
                            用户名、密码、确认密码、问题、问题答案、Email为必填项。<br />
                            <span style="color: Blue">注：若修改此项，前台正在注册的表单页面将失效</span></td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="HdnRegFields_MustFill" runat="server" />
                                        <asp:HiddenField ID="HdnRegFields_SelectFill" runat="server" />
                                        可用项：<br />
                                        <asp:ListBox ID="LitRegFields" SelectionMode="Multiple" Width="130" Height="285" runat="server" class="form-control" /></td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <input id="Button4" value=" >> " onclick="AddFields_MustFill()" title="添加所选项" type="button" />
                                                    <br />
                                                    <input id="Button2" value=" << " onclick="RemoveFields_MustFill()" title="移除所选项"
                                                        type="button" /></td>
                                                <td>必填项：<br />
                                                    <asp:ListBox ID="LitRegFields_MustFill" SelectionMode="Multiple" Width="130" Height="130" runat="server" class="form-control" /></td>
                                                <td>
                                                    <input id="Button5" value=" ︽ " onclick="UpFields_MustFill()" title="上移" type="button" />
                                                    <br />
                                                    <input id="Button6" value=" ︾ " onclick="DownFields_MustFill()" title="下移" type="button" />
                                                    <br />
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="Button3" value=" >> " onclick="AddFields_SelectFill()" title="添加所选项" type="button" />
                                                    <br />
                                                    <input id="Button7" value=" << " onclick="RemoveFields_SelectFill()" title="移除所选项"
                                                        type="button" /></td>
                                                <td>选填项：<br />
                                                    <asp:ListBox ID="LitRegFields_SelectFill" SelectionMode="Multiple" Width="130" Height="130"
                                                        class="form-control" runat="server" /></td>
                                                <td>
                                                    <input id="Button8" value=" ︽ " onclick="UpFields_SelectFill()" title="上移" type="button" />
                                                    <br />
                                                    <input id="Button9" value=" ︾ " onclick="DownFields_SelectFill()" title="下移" type="button" />
                                                    <br />
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>新会员注册时发送的手机验证短信内容：<br />
                        </strong>短信内容中可用标签如下(内容不超过70个字)：<br />
                            <span>{$CheckNum}</span>：会员名称<br />
                            <span>{$SiteName}</span>：网站名称<br />
                            <span>{$SiteUrl}</span>：网站地址</td>
                        <td>
                            <asp:TextBox ID="txtMobileRegInfo" runat="server" Rows="6" TextMode="MultiLine" Width="300px" class="form-control"></asp:TextBox></td>
                    </tr>
                </tbody>
                <tr id="#save">
                    <td class="text-center" colspan="2">
                        <asp:Button runat="server" Text="保存设置" OnClick="Save_Click" class="btn btn-primary" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tab-pane" id="Tabs1">
            <table class="table table-striped table-bordered table-hover myform-control">
                <tbody class="table" width: 100%;">
                    <tr runat="server" id="PresentExpPerLogin">
                        <td style="width: 20%;"><strong>会员每登录一次奖励的积分：</strong><br />
                            一天只计算一次 </td>
                        <td style="width: 60%;">
                            <asp:TextBox ID="TxtPresentExpPerLogin" Text="0" runat="server" Columns="5" class="form-control" />分积分 </td>
                    </tr>
                    <tr>
                        <td>每次签到奖励:</td>
                        <td>
                            <asp:TextBox runat="server" CssClass="form-control" ID="SignPurse_T" Text="0"></asp:TextBox><span>资金</span>
                        </td>
                    </tr>
                    <tr
                        runat="server" id="Tr1">
                        <td><strong>发布一条新内容奖励积分：</strong></td>
                        <td>
                            <asp:TextBox runat="server" ID="tb_InformationRule" class="form-control"></asp:TextBox>
                            分积分
	          <asp:RegularExpressionValidator runat="server" ID="rev_2" ValidationExpression="^[1-9]\d*|0$"
                  ControlToValidate="tb_InformationRule" ErrorMessage="只能输入非0整数"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr
                        runat="server" id="Tr2">
                        <td><strong>发布一条评论奖励积分：</strong></td>
                        <td>
                            <asp:TextBox runat="server" ID="tb_CommentRule" class="form-control"></asp:TextBox>
                            分积分
	          <asp:RegularExpressionValidator runat="server" ID="Rev_1" ValidationExpression="^[1-9]\d*|0$"
                  ControlToValidate="tb_CommentRule" ErrorMessage="只能输入非0整数"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr
                        runat="server" id="Tr3">
                        <td><strong>发布的内容被推荐奖励积分：</strong></td>
                        <td>
                            <asp:TextBox runat="server" ID="tb_RecommandRule" class="form-control"></asp:TextBox>
                            分积分
	          <asp:RegularExpressionValidator runat="server" ID="rev_3" ValidationExpression="^[1-9]\d*|0$"
                  ControlToValidate="tb_RecommandRule" ErrorMessage="只能输入非0整数"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr
                        runat="server" id="PresentExp">
                        <td><strong>新会员注册时赠送的积分：</strong></td>
                        <td>
                            <asp:TextBox ID="TxtPresentExp" Text="0" runat="server" Columns="7" MaxLength="7"
                                class="form-control" />
                            分积分
	          <asp:RegularExpressionValidator ID="ValgPresentExp" runat="server" ControlToValidate="TxtPresentExp"
                  ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" /></td>
                    </tr>
                    <tr
                        runat="server" id="PresentMoney">
                        <td><strong>新会员注册时赠送的金钱：</strong></td>
                        <td>
                            <asp:TextBox ID="TxtPresentMoney" Text="0" runat="server" Columns="7" MaxLength="7"
                                class="form-control" />
                            元钱
	          <asp:RegularExpressionValidator ID="ValgTxtPresentMoney" runat="server" ControlToValidate="TxtPresentMoney"
                  ErrorMessage="只能输入货币字符，并且不能为负数" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?"
                  Display="Dynamic" /></td>
                    </tr>
                    <tr
                        runat="server" id="PresentValidNum">
                        <td><strong>新会员注册时赠送的有效期：</strong></td>
                        <td>
                            <asp:TextBox ID="TxtPresentValidNum" Text="0" runat="server" Columns="5" class="form-control" />
                            <asp:DropDownList
                                ID="DropPresentValidUnit" runat="server" class="form-control">
                                <asp:ListItem Value="1">天</asp:ListItem>
                                <asp:ListItem Value="2">月</asp:ListItem>
                                <asp:ListItem Value="3">年</asp:ListItem>
                            </asp:DropDownList>
                            （－1表示无限期） </td>
                    </tr>
                    <tr>
                        <td><strong>创建群组所需要的积分：</strong></td>
                        <td>
                            <asp:TextBox ID="Txtintegral" Text="0" runat="server" Columns="5" class="form-control" /></td>
                    </tr>
                    <tr>
                        <td><strong>佣金占赠送积分百分比：</strong></td>
                        <td>
                            <asp:TextBox ID="TxtIntegralPercentage" Text="0" runat="server" Columns="5" class="form-control" />%</td>
                    </tr>
                </tbody>
                <tr id="#save">
                    <td class="text-center" colspan="2">
                        <asp:Button runat="server" Text="保存设置" OnClick="Save_Click" class="btn btn-primary" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tab-pane" id="Tabs2">
            <table class="table table-striped table-bordered myform-control">
                <tbody>
                    <tr runat="server" id="MoneyExchangePoint">
                        <td class="td_l"><strong>资金与点券的兑换比率：</strong></td>
                        <td>每
	          <asp:TextBox ID="TxtMoneyExchangePoint" Text="0" runat="server" Columns="7" MaxLength="7"
                  class="form-control" />
                            元钱可兑换 <strong>
                                <asp:TextBox ID="TxtCMoneyExchangePoint" Text="1" runat="server" Columns="7" MaxLength="7"
                                    class="form-control" />
                            </strong>点券
	          <asp:RegularExpressionValidator ID="ValeMoneyExchangePoint" runat="server" ControlToValidate="TxtMoneyExchangePoint"
                  ErrorMessage="只能输入货币字符，并且不能为负数" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?"
                  Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="ValeCMoneyExchangePoint" runat="server" ControlToValidate="TxtCMoneyExchangePoint"
                                ErrorMessage="只能输入货币字符，并且不能为负数" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?"
                                Display="Dynamic" /></td>
                    </tr>
                    <tr
                        runat="server" id="MoneyExchangeValidDay">
                        <td><strong>资金与有效期的兑换比率：</strong></td>
                        <td>每
	          <asp:TextBox ID="TxtMoneyExchangeValidDay" Text="0" runat="server" Columns="7" MaxLength="7"
                  class="form-control" />
                            元钱可兑换 <strong>
                                <asp:TextBox ID="TxtCMoneyExchangeValidDay" Text="1" runat="server" Columns="7" MaxLength="7"
                                    class="form-control" />
                            </strong>天有效期
	          <asp:RegularExpressionValidator ID="ValeMoneyExchangeValidDay" runat="server" ControlToValidate="TxtMoneyExchangeValidDay"
                  ErrorMessage="只能输入货币字符，并且不能为负数" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?"
                  Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="ValeCMoneyExchangeValidDay" runat="server" ControlToValidate="TxtCMoneyExchangeValidDay"
                                ErrorMessage="只能输入货币字符，并且不能为负数" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?"
                                Display="Dynamic" /></td>
                    </tr>
                    <tr runat="server">
                        <td><strong>资金与虚拟币的兑换比率：</strong></td>
                        <td>每
	          <asp:TextBox ID="txtMoneyExchangeDummyPurse" Text="0" runat="server" Columns="7"
                  MaxLength="7" class="form-control" />
                            元钱可兑换 <strong>
                                <asp:TextBox ID="txtCMoneyExchangeDummyPurse" Text="1" runat="server" Columns="7"
                                    MaxLength="7" class="form-control" />
                            </strong>元虚拟币
	          <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMoneyExchangeDummyPurse"
                  ErrorMessage="只能输入货币字符，并且不能为负数" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?"
                  Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtCMoneyExchangeDummyPurse"
                                ErrorMessage="只能输入货币字符，并且不能为负数" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?"
                                Display="Dynamic" /></td>
                    </tr>
                    <tr
                        runat="server" id="UserExpExchangePoint">
                        <td><strong>积分与点券的兑换比率：</strong></td>
                        <td>每
	          <asp:TextBox ID="TxtUserExpExchangePoint" Text="0" runat="server" Columns="7" MaxLength="7"
                  class="form-control" />
                            分积分可兑换 <strong>
                                <asp:TextBox ID="TxtCUserExpExchangePoint" Text="1" runat="server" Columns="7" MaxLength="7"
                                    class="form-control" />
                            </strong>点券
	          <asp:RegularExpressionValidator ID="ValgUserExpExchangePoint" runat="server" ControlToValidate="TxtUserExpExchangePoint"
                  ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="ValgCUserExpExchangePoint" runat="server" ControlToValidate="TxtCUserExpExchangePoint"
                                ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" /></td>
                    </tr>
                    <tr
                        runat="server" id="UserExpExchangeValidDay">
                        <td><strong>积分与有效期的兑换比率：</strong></td>
                        <td>每
	          <asp:TextBox ID="TxtUserExpExchangeValidDay" Text="0" runat="server" Columns="7"
                  MaxLength="7" class="form-control" />
                            分积分可兑换 <strong>
                                <asp:TextBox ID="TxtCUserExpExchangeValidDay" Text="1" runat="server" Columns="7"
                                    MaxLength="7" class="form-control" />
                            </strong>天有效期
	          <asp:RegularExpressionValidator ID="ValgUserExpExchangeValidDay" runat="server" ControlToValidate="TxtUserExpExchangeValidDay"
                  ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="ValgCUserExpExchangeValidDay" runat="server"
                                ControlToValidate="TxtCUserExpExchangeValidDay" ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$"
                                Display="Dynamic" /></td>
                    </tr>
                    <tr
                        runat="server" id="Tr4">
                        <td><strong>积分与资金的兑换比率：</strong></td>
                        <td>每<asp:TextBox ID="TxtCUserExpExchangePoints" Text="0" runat="server" Columns="7" MaxLength="7" class="form-control" />
                            分积分可兑换 <strong>
                                <asp:TextBox ID="TxtCUserExpExchangeMoney" Text="0" runat="server" Columns="7" MaxLength="7" class="form-control" /></strong>元钱
	          <asp:RegularExpressionValidator ID="REV1" runat="server" ControlToValidate="TxtCUserExpExchangePoints"
                  ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="REV2" runat="server" ControlToValidate="TxtCUserExpExchangeMoney"
                                ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" />

                        </td>
                    </tr>
                    <tr
                        runat="server" id="Tr8">
                        <td><strong>积分与银币的兑换比率：</strong></td>
                        <td>每
	          <asp:TextBox ID="TxtCUserExpExchangeExp" Text="0" runat="server" Columns="7" MaxLength="7"
                  class="form-control" />
                            分积分可兑换 <strong>
                                <asp:TextBox ID="TxtCUserExpExchangeSilverCoin" Text="0" runat="server" Columns="7" MaxLength="7"
                                    class="form-control" />
                            </strong>个银币
	          <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TxtCUserExpExchangeExp"
                  ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TxtCUserExpExchangeSilverCoin"
                                ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" /></td>
                    </tr>
                    <tr>
                        <td><strong>打卡领取奖励：</strong></td>
                        <td>
                            <select id="selPunch" runat="server" onchange="PunchCha()" class="form-control" style="width:200px;">
                                <option value="0">不奖励</option>
                                <option value="1">现金</option>
                                <option value="2">虚拟币</option>
                                <option value="3">积分</option>
                                <option value="4">点劵</option>
                                <option value="5">信誉值</option>
                            </select></td>
                    </tr>
                    <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" style="display: none"
                        onmouseout="this.className='tdbg'" id="Tr7">
                        <td><strong>打卡奖励数值：</strong></td>
                        <td>
                            <asp:TextBox ID="txtPunch" Text="" runat="server" Columns="5" MaxLength="5" class="form-control" /></td>
                    </tr>
                    <tr>
                        <td><strong>推广奖励类型：</strong></td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList10" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="RadioButtonList10_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">不奖励</asp:ListItem>
                                <asp:ListItem Value="1">现金</asp:ListItem>
                                <asp:ListItem Value="2">银币</asp:ListItem>
                                <asp:ListItem Value="3">积分</asp:ListItem>
                                <asp:ListItem Value="4">虚拟币</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" visible="false" onmouseout="this.className='tdbg'"
                        runat="server" id="Tr6">
                        <td><strong>推广奖励数值：</strong></td>
                        <td>
                            <asp:TextBox ID="txtPromotion" Text="" runat="server" Columns="5" MaxLength="5" class="form-control" /></td>
                    </tr>
                </tbody>
                <tr>
                    <td class="text-center" colspan="2">
                        <asp:Button runat="server" Text="保存设置" OnClick="Save_Click" class="btn btn-primary" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tab-pane" id="Tabs3">
            <table class="table table-striped table-bordered table-hover myform-control">
                <tbody>
                    <tr runat="server" id="PointName">
                        <td style="width: 20%;"><strong>点券的名称：</strong><br />
                            例如：点券、金币、拍点 </td>
                        <td style="height: 36px">
                            <asp:TextBox ID="TxtPointName" Text="点券" runat="server" Columns="5" MaxLength="5"
                                class="form-control" /></td>
                    </tr>
                    <tr
                        runat="server" id="PointUnit">
                        <td><strong>点券的单位：</strong><br />
                            例如：点、个 </td>
                        <td>
                            <asp:TextBox ID="TxtPointUnit" Text="点" runat="server" Columns="5" MaxLength="5"
                                class="form-control" /></td>
                    </tr>
                    <tr
                        runat="server" id="PresentPoint">
                        <td><strong>注册成功赠送点券：</strong></td>
                        <td>
                            <asp:TextBox ID="TxtPresentPoint" Text="0" runat="server" Columns="7" MaxLength="7"
                                class="form-control" />
                            点券
	          <asp:RegularExpressionValidator ID="ValgPresentPoint" runat="server" ControlToValidate="TxtPresentPoint"
                  ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" /></td>
                    </tr>
                    <tr>
                        <td>资料填写完整赠送点券 </td>
                        <td>
                            <asp:TextBox ID="txtzi" runat="server" Columns="7" MaxLength="7" class="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtzi"
                                ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" /></td>
                    </tr>
                </tbody>
                <tr id="#save">
                    <td class="text-center" colspan="2">
                        <asp:Button runat="server" Text="保存设置" OnClick="Save_Click" class="btn btn-primary" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tab-pane" id="Tabs4">
            <table class="table table-striped table-bordered table-hover myform-control">
                <tbody>
                    <tr>
                        <td style="width: 20%;" class="text-right"><strong>是否对同一IP注册进行时间间隔限制：</strong></td>
                        <td>
                            <input type="checkbox" runat="server" id="RBLDZ" class="switchChk" />
                        </td>
                    </tr>
                    <tr>
                        <td class="text-right"><strong>注册间隔限制时间：</strong></td>
                        <td>
                            <asp:TextBox ID="txtLimitTime" class="form-control" runat="server" onkeyup="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}" onafterpaste="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}" Width="150px"></asp:TextBox>
                            <strong style="color: red">&nbsp;间隔时间单位为小时 </strong></td>
                    </tr>
                    <tr>
                        <td class="text-right"><strong>是否设置限制的IP段：</strong></td>
                        <td>
                            <input type="checkbox" runat="server" id="RadioButtonList11" class="switchChk" />
                        </td>
                    </tr>
                    <div id="IPpart" runat="server">
                        <tr>
                            <td class="text-right"><strong>设置限制的IP段：</strong></td>
                            <td>
                                <asp:TextBox ID="txtBeginIP" class="form-control" runat="server" Width="150px"></asp:TextBox>
                                &nbsp; —— &nbsp;
	                            <asp:TextBox ID="txtEndIP" class="form-control" runat="server" Width="150px"></asp:TextBox>
                                <strong style="color: red">&nbsp; 不填为所有IP限制</strong></td>
                        </tr>
                    </div>
                </tbody>
                <tr id="#save">
                    <td class="text-center" colspan="2">
                        <asp:Button runat="server" Text="保存设置" OnClick="Save_Click" class="btn btn-primary" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tab-pane" id="Tabs5">
            <%--<table class="table table-striped table-bordered table-hover myform-control">
                <tbody>
                    <tr>
                        <td class="td_l"><strong>用户统计字段：</strong></td>
                        <td class="countfield_td">
                            <div class="cols col-md-2">
                                <asp:ListBox ID="UserFields_list" runat="server" Style="width: 100%;" SelectionMode="Multiple" Height="285" CssClass="form-control"></asp:ListBox>
                            </div>
                            <div class="cols col-md-1 text-center">
                                <div style="margin-top: 30px;">
                                    <button class="btn btn-default" onclick="AddFields_CountFill()" type="button">>></button>
                                </div>
                                <div class="margin_t10">
                                    <button class="btn btn-default" onclick="RemoveFields_CountFill()" type="button"><<</button>
                                </div>
                            </div>
                            <div class="cols col-md-2">
                                <asp:ListBox ID="CountFields_list" runat="server" Style="width: 100%;" SelectionMode="Multiple" Height="285" CssClass="form-control"></asp:ListBox>
                            </div>
                            <asp:HiddenField ID="CountFields_Hid" runat="server" />
                        </td>
                    </tr>
                </tbody>
                <tr id="#save">
                    <td class="text-center" colspan="2">
                        <asp:Button runat="server" Text="保存设置" OnClick="Save_Click" class="btn btn-primary" />
                    </td>
                </tr>
            </table>--%>
            <table class="table table-bordered table-striped">
                <tr>
                    <td class="td_l">会员生成邀请码的最大数值</td>
                    <td>
                        <asp:TextBox ID="InviteCode_T" runat="server" class="form-control text_300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="InviteCode_T" Display="Dynamic" ErrorMessage="最少字符数不能为空"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="InviteCode_T" ErrorMessage="最少字符数必须大于等于1" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1" Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>
                        <span class="rd_green margin_l5">为0则不允许生成</span>
                    </td>
                </tr>
                <tr>
                    <td>会员邀请码格式</td>
                    <td>
                        <ZL:TextBox runat="server" ID="InviteFormat_T" CssClass="form-control text_300" MaxLength="20" />
                        <span class="rd_green">示例:{000000AA},0表示数字占位符,A表示字母占位符</span></td>
                </tr>
                <tr><td>邀请入会员组</td><td>
                    <asp:DropDownList runat="server" ID="InviteJoinGroup_DP" class="form-control text_300" DataTextField="GroupName" DataValueField="GroupID"></asp:DropDownList>
                </td></tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" Text="保存设置" OnClick="Save_Click" class="btn btn-primary" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/SelectCheckBox.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script src="/dist/js/bootstrap-switch.js"></script>
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            ZL_Regex.B_Num(".int");
        })

        function checkIP() {
            var ip1 = document.getElementById("txtBeginIP").value;
            var ip2 = document.getElementById("txtEndIP").value;
            if (ip1 != "" || ip2 != "") {
                var re = /^((\d)|(([1-9])\d)|(1\d\d)|(2(([0-4]\d)|5([0-5]))))\.((\d)|(([1-9])\d)|(1\d\d)|(2(([0-4]\d)|5([0-5]))))\.((\d)|(([1-9])\d)|(1\d\d)|(2(([0-4]\d)|5([0-5]))))\.((\d)|(([1-9])\d)|(1\d\d)|(2(([0-4]\d)|5([0-5]))))$/;
                var message = document.getElementById("lblMessage");
                if (!re.test(ip1) || !re.test(ip2)) {
                    alert("输入IP地址格式不正确，请从新输入！")
                    return;
                }
            }
        }
        window.onload = function PunchCha() {
            var obj = document.getElementById("selPunch").value;
            if (obj == "0") {
                document.getElementById("Tr7").style.display = "none";
            } else {
                document.getElementById("Tr7").style.display = "";
            }
            InitCountFiled();
        }
        var LitRegFieldsClientID = "<%=LitRegFields.ClientID %>";
        var HdnRegFields_MustFillClientID = "<%=HdnRegFields_MustFill.ClientID %>";
        var LitRegFields_MustFillClientID = "<%=LitRegFields_MustFill.ClientID %>";
        var HdnRegFields_SelectFillClientID = "<%=HdnRegFields_SelectFill.ClientID %>";
        var LitRegFields_SelectFillClientID = "<%=LitRegFields_SelectFill.ClientID %>";
        //初始化需要统计的用户字段
        function InitCountFiled() {
            //var hids = $("#CountFields_Hid").val();
            //if (hids != "") {
            //    var harr = hids.split(',');
            //    for (var i = 0; i < harr.length; i++) {
            //        $("#UserFields_list option[value='" + harr[i] + "']")[0].selected = true;
            //    }
            //    addItem($("#UserFields_list")[0], $("#CountFields_list")[0]);
            //}
        }
        function AddFields_CountFill() {
            //addItem($("#UserFields_list")[0], $("#CountFields_list")[0]);
            //SetHdn($("#CountFields_list")[0], $("#CountFields_Hid")[0]);
        }
        function RemoveFields_CountFill() {
            addItem($("#CountFields_list")[0], $("#UserFields_list")[0]);
            SetHdn($("#CountFields_list")[0], $("#CountFields_Hid")[0]);
        }
        function AddFields_MustFill() {
            var itemList = document.getElementById(LitRegFieldsClientID);
            var target = document.getElementById(LitRegFields_MustFillClientID);
            addItem(itemList, target);
            SetHdn(target, document.getElementById(HdnRegFields_MustFillClientID));
        }

        function RemoveFields_MustFill() {
            var itemList = document.getElementById(LitRegFieldsClientID);
            var target = document.getElementById(LitRegFields_MustFillClientID);
            addItem(target, itemList);
            SetHdn(target, document.getElementById(HdnRegFields_MustFillClientID));
        }

        function AddFields_SelectFill() {
            var itemList = document.getElementById(LitRegFieldsClientID);
            var target = document.getElementById(LitRegFields_SelectFillClientID);
            addItem(itemList, target);
            SetHdn(target, document.getElementById(HdnRegFields_SelectFillClientID));
        }

        function RemoveFields_SelectFill() {
            var itemList = document.getElementById(LitRegFieldsClientID);
            var target = document.getElementById(LitRegFields_SelectFillClientID);
            addItem(target, itemList);
            SetHdn(target, document.getElementById(HdnRegFields_SelectFillClientID));
        }

        function UpFields_MustFill() {
            var target = document.getElementById(LitRegFields_MustFillClientID);
            UpOption(target);
            SetHdn(target, document.getElementById(HdnRegFields_MustFillClientID));
        }

        function DownFields_MustFill() {
            var target = document.getElementById(LitRegFields_MustFillClientID);
            DownOption(target);
            SetHdn(target, document.getElementById(HdnRegFields_MustFillClientID));
        }

        function UpFields_SelectFill() {
            var target = document.getElementById(LitRegFields_SelectFillClientID);
            UpOption(target);
            SetHdn(target, document.getElementById(HdnRegFields_SelectFillClientID));
        }

        function DownFields_SelectFill() {
            var target = document.getElementById(LitRegFields_SelectFillClientID);
            DownOption(target);
            SetHdn(target, document.getElementById(HdnRegFields_SelectFillClientID));
        }

        function UpOption(obj) {
            for (var i = 0; i < obj.length; i++) {
                var opt = obj.options[i];
                if (opt.selected) {
                    if (i >= 1) {
                        var temp = obj.options[i - 1];
                        obj.options[i] = new Option(temp.text, temp.value, 0, 0);
                        obj.options[i - 1] = new Option(opt.text, opt.value, 0, 1);
                    }
                }
            }
        }

        function DownOption(obj) {
            for (var i = obj.length - 1; i >= 0; i--) {
                var opt = obj.options[i];
                if (opt.selected) {
                    if (i <= obj.length - 2) {
                        var temp = obj.options[i + 1];
                        obj.options[i] = new Option(temp.text, temp.value, 0, 0);
                        obj.options[i + 1] = new Option(opt.text, opt.value, 0, 1);
                    }
                }
            }
        }

        function addItem(ItemList, Target) {
            for (var i = 0; i < ItemList.length; i++) {
                var opt = ItemList.options[i];
                if (opt.selected) {
                    flag = true;
                    for (var y = 0; y < Target.length; y++) {
                        var myopt = Target.options[y];
                        if (myopt.value == opt.value) {
                            flag = false;
                        }
                    }
                    if (flag) {
                        Target.options[Target.options.length] = new Option(opt.text, opt.value, 0, 0);
                    }
                }
            }

            for (var y = 0; y < Target.length; y++) {
                var myopt = Target.options[y];
                for (var i = 0; i < ItemList.length; i++) {
                    if (ItemList.options[i].value == myopt.value) {
                        ItemList.options[i] = null;
                    }
                }
            }
        }

        function SetHdn(ItemList, HdnObj) {
            var adminId = "";
            for (var i = 0; i < ItemList.length; i++) {
                if (adminId == "") {
                    adminId = ItemList.options[i].value;
                }
                else {
                    adminId += "," + ItemList.options[i].value;
                }
            }
            HdnObj.value = adminId;
        }
    </script>
    <script type="text/javascript" src="/js/Common.js"></script>
</asp:Content>
