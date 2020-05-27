<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabelExport.aspx.cs"  EnableViewStateMac="false" Inherits="ZoomLa.WebSite.Manage.Template.LabelExport"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>标签升级</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="panel panel-info">
        <div class="panel-heading"><i class="fa fa-flag"> 标签升级</i></div>
        <div class="panel-body">
            <div>升级标签：</div>
            <div>升级状态：<span style="color: Red"><asp:Label ID="Remind_L" runat="server"></asp:Label></span></div>
        </div>
        <div class="panel-footer">
            <asp:Button ID="OldVerUP_Btn" runat="server" class="btn btn-info" Text="早期标签升级" OnClick="Button1_Click" />
            <asp:Button ID="GlobalUP_Btn" runat="server" class="btn btn-primary" Text="国际版标签升级" OnClick="GlobalUP_Btn_Click" />
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
