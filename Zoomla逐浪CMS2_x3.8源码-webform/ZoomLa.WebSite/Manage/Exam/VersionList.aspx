<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VersionList.aspx.cs" Inherits="Manage_Exam_VersionList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>教材版本</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate><input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="版本名称">
                <ItemTemplate>
                    <a href="AddVersion.aspx?id=<%#Eval("ID") %>"><%#Eval("VersionName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="版本时间" DataField="VersionTime" />
            <asp:BoundField HeaderText="年级" DataField="GradeName" />
            <asp:BoundField HeaderText="科目" DataField="NodeName" />
            <asp:BoundField HeaderText="册序" DataField="Volume" />
            <asp:BoundField HeaderText="节名称" DataField="SectionName" />
            <asp:BoundField HeaderText="课名称" DataField="CourseName" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a class="option_style" href="AddVersion.aspx?id=<%#Eval("ID") %>"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>