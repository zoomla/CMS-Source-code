<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAgio.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.AddAgio" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>促销方案管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" class="text-center"><%=type%>打折信息 </td>
        </tr>
        <tr>
            <td  class="text-right" style="width: 24%"><strong>打折方案名称：</strong></td>
            <td style="width: 76%;"><%=proName %></td>
        </tr>
        <tr>
            <td  class="text-right" style="width: 24%"><strong>数量区限：</strong></td>
            <td valign="middle">
                <asp:TextBox ID="txtStartNum" CssClass="form-control" runat="server" Width="90px" />
                <span class="fa fa-arrow-right"></span>
                <asp:TextBox ID="txtEndNum" class="form-control" runat="server" Width="90px" />
                购物数量
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtEndNum" Display="Dynamic" ControlToValidate="txtStartNum" ErrorMessage="请输入正确的数量区限" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="txtEndNum" ErrorMessage="请输入购物数量的下限"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ControlToValidate="txtStartNum" ErrorMessage="请输入购物数量的上限"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 24%"  class="text-right" valign="top"><strong>折扣：</strong></td>
            <td valign="middle">
                <asp:TextBox ID="txtAgio" class="form-control" runat="server" Width="50px" MaxLength="3" />%
                <asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic"  ControlToValidate="txtAgio" ErrorMessage="*请输入正确的折扣信息" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server" Display="Dynamic" ControlToValidate="txtAgio" ErrorMessage="*折扣不能小于1或大于100" MaximumValue="100" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ControlToValidate="txtAgio" ErrorMessage="*请输入正确的折扣信息"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="Submit_B" class="btn btn-primary" runat="server" Text="提交" OnClick="Submit_B_Click" />
                <input id="Return_B" type="button" class="btn btn-primary" value="返回" onclick="javascript: window.history.go(-1);" />
            </td>
        </tr>
    </table>
</asp:Content>
