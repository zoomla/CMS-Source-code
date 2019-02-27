<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnsList.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.Ask.AnsList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>答题列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
    <li><a href='Default.aspx'>动力模块</a></li>
    <li><a href='AskList.aspx'>问卷调查</a></li>
    <li class='active'><asp:Label ID="AskTitle_L" runat="server" /></li>
    <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn" onclick="selbox.toggle();"><i class="fa fa-search"></i></a></div>
    <div id="sel_box" runat="server" class="padding5">
        <div style="display: inline-block; width: 230px;">
            <div class="input-group" style="position: relative; margin-bottom: -12px;">
                <asp:TextBox ID="Skey_T" placeholder="答题ID或用户名" runat="server" CssClass="form-control text_md" />
                <span class="input-group-btn">
                    <asp:Button ID="Search_B" runat="server" Text="<%$Resources:L,搜索 %>" class="btn btn-primary" OnClick="Search_B_Click" />
                </span>
            </div>
        </div>
    </div>
</ol>
<div class="template margin_t5" id="template" runat="server">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" OnRowDataBound="EGV_RowDataBound" PageSize="20" AllowPaging="true" CssClass="table table-striped table-bordered table-hover" OnRowCommand="EGV_RowCommand" EmptyDataText="没有数据">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:BoundField HeaderText="问卷标题" DataField="Title" />
            <asp:TemplateField HeaderText="答题用户"><ItemTemplate><%#GetCUser() %></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="提交IP"><ItemTemplate><%#GetIP() %></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="来源平台"><ItemTemplate>微信</ItemTemplate></asp:TemplateField>
            <asp:BoundField HeaderText="答题时间" DataField="CDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href='AnsDetailList.aspx?ansid=<%#Eval("ID") %>'><i class="fa fa-list"></i> 详情</a>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del" OnClientClick="return confirm('确认删除？')" CssClass="option_style"><i class="fa fa-trash-o" title="<%=Resources.L.删除 %>"></i><%=Resources.L.删除 %></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style>
.contr{width:60%;}
</style>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    function showuser(id) { ShowComDiag("../../User/Userinfo.aspx?id=" + id, "查看用户"); }
</script>
</asp:Content>