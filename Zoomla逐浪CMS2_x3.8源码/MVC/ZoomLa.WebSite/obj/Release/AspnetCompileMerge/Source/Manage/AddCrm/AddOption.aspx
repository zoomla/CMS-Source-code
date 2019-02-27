<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddOption.aspx.cs" Inherits="ZoomLaCMS.Manage.AddCrm.AddOption" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加新选项</title>
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <div id="addOptionDiv">
            <table class="table table-striped table-bordered table-hover">
                <tr class="gridtitle">
                    <td colspan="2" align="center" style="height: 25px;">
                        <asp:Label ID="Tit0" runat="server" Text="添加区域"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">选项名称：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_content" runat="server" CssClass="form-control text_300" ValidationGroup="ww" MaxLength="10" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="选项值不能为空！" ControlToValidate="txt_content" ValidationGroup="ww" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="Re1" runat="server" ValidationExpression="^([a-zA-Z\u4e00-\u9fa5\@\.]([a-zA-Z0-9_\u4e00-\u9fa5\@\.]){3,10}$)" ForeColor="Red" ControlToValidate="txt_content" ValidationGroup="ww"
                            Text="不能以数字或特殊字符开头,最少需要四位中文/数字/字母组合，例:投资意向1" Display="Dynamic" />
                    </td>
                </tr>
                <tr>
                    <td align="right">默认显示：
                    </td>
                    <td align="left">
                        <input type="checkbox" runat="server" id="isDefault" class="switchChk" checked="checked" />
                    </td>
                </tr>
                
                <tr>
                    <td align="right">生成方式：
                    </td>
                    <td id="fieldtype_td">
                        <asp:RadioButtonList runat="server" ID="BuildMethod" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">下拉列表</asp:ListItem>
                            <asp:ListItem Value="2">单选框</asp:ListItem>
                            <asp:ListItem Value="3">多选框</asp:ListItem>
                            <asp:ListItem Value="4">多行文本(不支持Html)</asp:ListItem>
                            <asp:ListItem Value="5">多行文本(支持Html)</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr class="option optionset_tr">
                    <td class="text-right">选项内容：</td>
                    <td>
                        <asp:TextBox ID="Option_T" runat="server" CssClass="form-control text_300" TextMode="MultiLine" Height="100"></asp:TextBox>
                        <span style="color:green;">选项以"|"隔开</span>
                    </td>
                </tr>
                <tr class="option textopt_tr" style="display:none;">
                    <td class="text-right">文本框尺寸：</td>
                    <td>
                        <asp:TextBox ID="TxtWidth_T" runat="server" CssClass="form-control text_s" placeholder="宽度"></asp:TextBox><span>px</span>
                        <asp:TextBox ID="TxtHeight_T" runat="server" CssClass="form-control text_s" placeholder="高度"></asp:TextBox><span>px</span>
                    </td>
                </tr>
                <tr>
                    <td class="text-right">管理员权限：</td>
                    <td>
                        <asp:TextBox ID="AdminName_T" runat="server" CssClass="form-control text_300" placeholder="管理员名称"></asp:TextBox> <span style="color:green;">为空默认不限制,多个以","隔开</span>
                    </td>
                </tr>
                <tr>
                    <td align="right">是否启用：
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="txt_enable" runat="server" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="Save_Btn" runat="server" Text="添加" class="btn btn-primary" OnClick="Add2_Click" ValidationGroup="ww" />
                        <asp:Button ID="Button3" runat="server" Text="取消" class="btn btn-primary" CausesValidation="false" />
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/dist/js/bootstrap-switch.js"></script>
    <script>
        $(function () {
            $("#fieldtype_td input").click(function () {
                ChangeOption($(this).val());
            });
            ChangeOption($("#fieldtype_td input:checked").val());
        });
        //切换选项模式
        function ChangeOption(val) {
            $(".option").hide();
            switch (val) {
                case "4":
                case "5":
                    $(".textopt_tr").show();
                    break;
                default:
                    $(".optionset_tr").show();
                    break;
            }
        }
    </script>
</asp:Content>
         