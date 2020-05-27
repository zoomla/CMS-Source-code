<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Survey.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Plus.Survey" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css"  rel="stylesheet"/>
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <title>编辑问卷投票</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <asp:Literal ID="LTitle" runat="server" Text="添加问卷投票"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="tdleft td_l"><strong>问卷投票名称：</strong></td>
            <td>
                <asp:TextBox ID="TxtSurveyName" class="form-control" runat="server" Width="156" MaxLength="80" ToolTip="标题最大长度为80个字符" />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TxtSurveyName">问卷投票名称不能为空</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvTitleLength" runat="server" ErrorMessage="标题最大长度为80个字符" ControlToValidate="TxtSurveyName" Display="Dynamic" ClientValidationFunction="CheckTitleLen"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>简介：</strong></td>
            <td>
                <asp:TextBox ID="TxtDescription" class="form-control" runat="server" TextMode="MultiLine" Width="365px" Height="43px" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>类型：</strong></td>
            <td>
                <asp:RadioButtonList ID="RblSurType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">投票</asp:ListItem>
                    <asp:ListItem Value="2">问卷</asp:ListItem>
                    <asp:ListItem Value="3" Selected="True">报名系统</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>起止时间：</strong></td>
            <td>
                <asp:TextBox ID="txtStartTime" class="form-control" Width="200" runat="server" onfocus="WdatePicker({ dateFmt: 'yyyy-MM-dd'});"></asp:TextBox>
                -- 
                <asp:TextBox ID="txtEndTime" class="form-control" Width="200" runat="server" onfocus="WdatePicker({ dateFmt: 'yyyy-MM-dd'});"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>同IP重复次数：</strong></td>
            <td><asp:TextBox ID="txtIPRepeat" class="form-control" Width="150" runat="server" Text="1"></asp:TextBox>0为无限制</td>
        </tr>
        <tr>
            <td class="tdleft"><strong>是否需登录：</strong></td>
            <td>
                <input runat="server" type="checkbox" id="ChkNeedLogin" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>是否启用：</strong></td>
            <td>
                 <input runat="server" type="checkbox" id="ChkIsOpen" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>是否需要审核：</strong></td>
            <td>
                <input runat="server" type="checkbox" id="ChkIsStatus" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>是否启用验证码：</strong></td>
            <td>
                <input runat="server" type="checkbox" id="ChkIsCheck" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>是否启用结果前台预览：</strong></td>
            <td>
                <input runat="server" type="checkbox" id="ChkIsShow" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class=" text-center">
                <asp:HiddenField ID="HdnSurveyID" runat="server" />
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" class="btn btn-primary" />
                <input type="reset" id="Reset" class="btn btn-primary" value="重置" />
                <input name="Cancel" type="button" class="btn btn-primary" id="Cancel" value="返回" onclick="window.location.href = 'SurveyManage.aspx';" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/JS/Verify.js"></script>
    <script>
        function CheckTitleLen(source, args) {
            var str = args.Value;
            if (str.length > 80) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

    </script>
</asp:Content>
