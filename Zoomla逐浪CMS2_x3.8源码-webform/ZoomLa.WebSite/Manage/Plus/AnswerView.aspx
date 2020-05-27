<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnswerView.aspx.cs" Inherits="Manage_Plus_AnswerView" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>问卷详情</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" PageSize="20" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" Width="100%" Height="20px" AllowPaging="True" AllowSorting="True" CellPadding="2" CellSpacing="1" BackColor="White" ForeColor="Black" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" GridLines="None" EnableModelValidation="True">
        <Columns>
            <asp:BoundField HeaderText="问卷ID" DataField="Surveyid" ItemStyle-Width="100" />
            <asp:TemplateField HeaderText="问题名称">
                <ItemTemplate>
                    <a href="SurveyItem.aspx?SID=<%:Sid %>&QID=<%#Eval("Qid") %>" target="_blank" title="问题详情"><%#Eval("QTitle") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="问题类型">
                <ItemTemplate><%#GetTypeStr() %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="用户回答" DataField="AnswerContent" />
            <asp:BoundField HeaderText="得分" DataField="AnswerScore" />
        </Columns>
    </ZL:ExGridView>
    <div><span style="font-size: 16px; color: #f00; font-weight: bold;">本问卷总得分:<%=GetCountScore() %></span></div>
</asp:Content>
