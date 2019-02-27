<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailSysTlp.aspx.cs" Inherits="Manage_User_Mail_MailSysTlp" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master"  %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>系统邮件</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" runat="server" AllowPaging="true" OnPageIndexChanging="EGV_PageIndexChanging" AutoGenerateColumns="False" 
        class="table table-striped table-bordered table-hover" OnRowDataBound="EGV_RowDataBound" EmptyDataText="<%$Resources:L,当前没有信息 %>">
        <Columns>
            <asp:TemplateField HeaderText="模板名称">
                <ItemTemplate><i class="fa fa-code"></i><a href="MailSysTlpEdit.aspx?TlpName=<%# HttpUtility.UrlEncode(Eval("Name","")) %>" class="option_style"><%# Eval("Name") %></a></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="模板类型">
                <ItemTemplate>邮件模板</ItemTemplate>
                <ItemStyle Width="8%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建日期">
                <ItemTemplate><%# Eval("CreationTime") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="修改日期">
                <ItemTemplate><%# Eval("UpdateTime") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="MailSysTlpEdit.aspx?TlpName=<%# HttpUtility.UrlEncode(Eval("Name","")) %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style>
        .allchk_sp,#AllID_Chk{
            display:none;
        }
    </style>
</asp:Content>