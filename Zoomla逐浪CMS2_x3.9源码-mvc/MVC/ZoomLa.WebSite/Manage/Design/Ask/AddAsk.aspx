<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAsk.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.Ask.AddAsk" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>编辑问卷</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="td_m">问卷名称</td>
            <td><asp:TextBox ID="Title_T" runat="server" CssClass="form-control text_300" /></td>
        </tr>
        <tr>
            <td>创建用户</td>
            <td>
                <asp:Literal ID="CUser_L" runat="server" />
            </td>
        </tr>
<%--        <tr>
            <td>问卷类型</td>
            <td><asp:Label ID="ZType_L" runat="server" /></td>
        </tr>
        <tr>
            <td>问卷状态</td>
            <td><asp:Label ID="Status_L" runat="server" /></td>
        </tr>--%>
        <tr>
            <td>问卷封面</td>
            <td>
                <ZL:SFileUp ID="SFile_Up" runat="server" FType="Img" />
            </td>
        </tr>
        <tr>
           <td>创建时间</td> 
           <td>
               <asp:TextBox ID="CDate_T" runat="server" Enabled="false" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" CssClass="form-control text_300" />
           </td>
        </tr>
        <tr>
            <td>到期时间</td>
            <td>
                <asp:TextBox ID="EndDate_L" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" CssClass="form-control  text_300" />
            </td>
        </tr>
        <tr>
            <td>问卷说明</td>
            <td><asp:TextBox ID="Remind_T" runat="server" TextMode="MultiLine" CssClass="form-control linetext" /></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="Save_B" runat="server" CssClass="btn btn-info" OnClick="Save_B_Click" Text="保存" />
                <a href="AskManage.aspx" class="btn btn-info">返回</a>
            </td>
        </tr>
    </table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style>
.linetext{width:500px;height:80px;}
</style>
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
function showuser(id) { ShowComDiag("../../User/Userinfo.aspx?id=" + id, "查看用户") }
</script>
</asp:Content>
