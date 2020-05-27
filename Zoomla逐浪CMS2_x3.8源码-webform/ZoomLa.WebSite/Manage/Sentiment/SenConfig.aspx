<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SenConfig.aspx.cs" Inherits="Manage_Sentiment_SenConfig" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>监测维度</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-striped">
        <tr><td>签名标记</td>
            <td>
                <asp:TextBox runat="server" ID="Sign_T" CssClass="form-control text_md"></asp:TextBox>
            </td></tr>
        <tr><td class="td_m">报告频率</td>
            <td>
                <label><input name="fre_rad" type="radio" value="day" checked="checked"/>每天</label>
                <label><input name="fre_rad" type="radio" value="week" />每周</label>
                <label><input name="fre_rad" type="radio" value="month" />按月</label>
                <label><input name="fre_rad" type="radio" value="year" />按年</label>
            </td></tr> 
        <tr><td>存档格式</td>
            <td>
                <label><input name="save_rad" type="radio" value="word" checked="checked" />Word</label>
                <label><input name="save_rad" type="radio" value="text" disabled="disabled" />文本</label> 
                <label><input name="save_rad" type="radio" value="excel" disabled="disabled"/>Excel</label>
            </td></tr>
        <tr><td></td><td><asp:Button runat="server" ID="Save_Btn" Text="保存" OnClick="Save_Btn_Click" CssClass="btn btn-primary" /></td></tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>