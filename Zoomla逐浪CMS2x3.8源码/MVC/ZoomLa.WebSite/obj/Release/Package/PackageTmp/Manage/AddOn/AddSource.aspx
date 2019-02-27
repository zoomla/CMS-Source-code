<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master"  AutoEventWireup="true" CodeBehind="AddSource.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.AddSource" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <title>添加来源</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" Runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td colspan="4" class="spacingtitle">
                <strong><asp:Label ID="LblPTitle" runat="server" Text="添加来源信息"></asp:Label></strong>
            </td>
        </tr>
        <tr>
            <td>
                <strong>名称：</strong></td>
            <td>
                <asp:TextBox ID="TxtName" runat="server" class="form-control pull-left" Width="150"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtName" Display="Dynamic" ErrorMessage="*">用户名不能为空</asp:RequiredFieldValidator>
            </td>
            <td colspan="2">
               
                <ZL:SFileUP ID="SFileUp_File" FType="Img" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <strong>地址：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtAddress" runat="server" class="form-control pull-left" Width="150"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtAddress" Display="Dynamic" ErrorMessage="*">地址不能为空</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <strong>电话：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtTel" runat="server" class="form-control pull-left" Width="150"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtTel" Display="Dynamic" ErrorMessage="*">电话不能为空</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="Tel" runat="server" ControlToValidate="TxtTel" Display="Dynamic" ErrorMessage="*" ValidationExpression="((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)">电话格式不正确</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <strong>传真：</strong></td>
            <td>
                <asp:TextBox ID="TxtFax" runat="server" class="form-control pull-left" Width="150"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtFax" Display="Dynamic" ErrorMessage="*" ValidationExpression="((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)">传真格式不正确</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <strong>联系人：</strong></td>
            <td>
                <asp:TextBox ID="TxtContacterName" runat="server" class="form-control pull-left" Width="150"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtContacterName" Display="Dynamic" ErrorMessage="*">联系人不能为空</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <strong>主页：</strong></td>
            <td>
                <asp:TextBox ID="TxtHomePage" runat="server"  class="form-control pull-left" Width="150"/>
            </td>
        </tr>
        <tr>
            <td>
                <strong>邮编：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtZipCode" runat="server" class="form-control pull-left" Width="150" />
                <asp:RegularExpressionValidator ID="Code" runat="server" ControlToValidate="TxtZipCode" Display="Dynamic" ErrorMessage="*" ValidationExpression="[1-9]\d{5}(?!\d)">邮编格式不正确</asp:RegularExpressionValidator>
            </td>
            <td>
                <strong>邮件：</strong></td>
            <td>
                <asp:TextBox ID="TxtEmail" runat="server" class="form-control pull-left" Width="150" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtEmail" Display="Dynamic" ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">邮箱格式不正确</asp:RegularExpressionValidator>(格式为……@…….com/cn/net)
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtEmail" Display="Dynamic" ErrorMessage="*">邮箱不能为空</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <strong>通讯：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtMail" runat="server" class="form-control pull-left" Width="150"/></td>
            <td>
                <strong>IM：</strong></td>
            <td>
                <asp:TextBox ID="TxtIm" runat="server" class="form-control pull-left" Width="150"/>
            </td>
        </tr>
        <tr>
            <td>
                <strong>分类：</strong></td>
            <td colspan="3">
                <asp:TextBox ID="TxtType" runat="server" class="form-control pull-left" Width="150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <strong>属性：</strong></td>
            <td colspan="3">
                <asp:CheckBox ID="ChkElite" runat="server" />推荐
                <asp:CheckBox ID="ChkOnTop" runat="server" />置顶
            </td>
        </tr>
        <tr>
            <td>
                <strong>来源简介：</strong>
            </td>
            <td colspan="3">
                <asp:TextBox ID="TxtIntro" runat="server" Height="300px" Width="583px" TextMode="MultiLine" class="form-control pull-left" />
            </td>
        </tr>
        <tr>
            <td>
                <strong>是否启用</strong>
            </td>
            <td colspan="3">
                <asp:CheckBox ID="ChkPass" runat="server" Checked="true"  />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="EBtnModify" class="btn btn-primary" Text="修改" OnClick="EBtnModify_Click" runat="server" Visible="false" />
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="Cancel" type="button"  class="btn btn-primary"  id="Button2" value="取消" onclick="javascript: window.location.href = 'SourceManage.aspx'" />
            </td>
        </tr>
    </table>   
</asp:Content>