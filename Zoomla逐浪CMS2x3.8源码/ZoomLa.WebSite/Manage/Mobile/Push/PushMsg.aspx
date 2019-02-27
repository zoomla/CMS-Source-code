<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PushMsg.aspx.cs" Inherits="Manage_Mobile_Push_PushMsg" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>推送消息</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
<tr><td class="td_m">应用名称</td><td>
    <asp:DropDownList runat="server" ID="APPList_DP" CssClass="form-control text_300" DataTextField="Alias" DataValueField="ID"></asp:DropDownList>
</td></tr>
<%--<tr><td class="td_m">接口</td><td><label><input type="radio" checked="checked" />极光推送</label></td></tr>--%>
<tr><td>类型</td><td>
<label><input type="radio" name="pushtype_rad" value="alert" checked="checked" />通知</label>
<label><input type="radio" name="pushtype_rad" value="sms" />SMS(会产生相关短信费用)</label>
</td></tr>
<tr><td>内容</td><td>
    <asp:TextBox runat="server" TextMode="MultiLine" CssClass="form-control m715-50" style="height:80px;" ID="MsgContent_T" MaxLength="72"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="MsgContent_T" ForeColor="Red" ErrorMessage="内容不能为空" />
    <div class="rd_green">可输入最多72个汉字</div>
</td></tr>
<tr><td></td><td>
   <asp:Button runat="server" ID="Push_Btn" CssClass="btn btn-primary" OnClick="Push_Btn_Click" Text="推送" />
    <a href="Default.aspx" class="btn btn-default">返回</a>
             </td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>