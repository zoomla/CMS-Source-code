<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Addclass.aspx.cs" Inherits="User_UserShop_Addclass" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的店铺</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="Classmanage.aspx">分类管理</a></li>
        <li class="active"><%= str %></li>
    </ol>
    <asp:HiddenField ID="hiden" runat="server" />
    <div class="us_topinfo" style="margin-top: 10px;">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td width="33%" align="right">分类名称：
                </td>
                <td width="67%">
                    <asp:TextBox ID="Classname" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="分类名称不能为空！" ControlToValidate="Classname"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="33%" align="right">排列顺序：
                </td>
                <td width="67%">
                    <asp:TextBox ID="Orderid" runat="server" CssClass="form-control" Width="100">0</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="排列顺序不能为空！" ControlToValidate="Orderid" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" Display="Dynamic" ErrorMessage="排列顺序格式不正确！" ControlToValidate="Orderid" MaximumValue="99999999" MinimumValue="0"></asp:RangeValidator>数字越大排列越前
                </td>
            </tr>
            <tr>
                <td width="33%" align="right">分类说明：
                </td>
                <td width="67%">
                    <asp:TextBox ID="Classinfo" runat="server" Height="119px" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="100%" align="center" colspan="2">
                    <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="添加 " OnClick="Button1_Click" />
                    <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="取消 " OnClick="Button2_Click" CausesValidation="False" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/calendar.js"></script>
    <script>
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
            }
        }
    </script>
</asp:Content>