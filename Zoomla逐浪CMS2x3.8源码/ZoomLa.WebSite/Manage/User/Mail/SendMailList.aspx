<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMailList.aspx.cs" Inherits="manage_Qmail_SendMailList" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>邮件订阅</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting" DataKeyNames="ID">
        <Columns>
        <asp:TemplateField HeaderText="标题">
            <ItemTemplate> <a href='MailShow.aspx?id=<%#DataBinder.Eval(Container.DataItem,"ID")%>'><%#DataBinder.Eval(Container.DataItem, "MailTitle")%></a> </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="发送时间" DataField="MailSendTime" />
        <asp:TemplateField HeaderText="邮件状态">
            <ItemTemplate> <%#GetState(DataBinder.Eval(Container.DataItem, "MailState").ToString())%> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                    <a href="MailShow.aspx?idid=<%#Eval("ID") %>" class="option_style"><i class="fa fa-eye" title="预览"></i></a>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <RowStyle Height="24px" HorizontalAlign="Center"  />
</ZL:ExGridView>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style>
        .allchk_sp,#AllID_Chk{
            display:none;
        }
    </style>
</asp:Content>
