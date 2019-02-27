<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestView.aspx.cs" Inherits="User_Exam_QuestView" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>查看试题</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a href="/user">用户中心</a></li>
        <li>查看试题</li>
    </ol>
    <div class="panel panel-info">
        <div class="panel-heading"><asp:Label runat="server" ID="Title_L"></asp:Label></div>
        <div class="panel-body">
            <div><asp:Label runat="server" ID="Content_L"></asp:Label></div>
            <div class="divline"></div>
            <div>
                <p style="white-space: normal;"><span style="font-family: 宋体, sans-serif; font-size: 13px; font-weight: bold; letter-spacing: 1px; line-height: 25px; background-color: #FFFFFF;">【正确答案】</span></p>
                <asp:Label runat="server" ID="Shuming_L"></asp:Label>
            </div>
            <div class="divline"></div>
            <div><asp:Label runat="server" ID="Jiexi_L"></asp:Label></div>
        </div>
    </div>
</asp:Content>