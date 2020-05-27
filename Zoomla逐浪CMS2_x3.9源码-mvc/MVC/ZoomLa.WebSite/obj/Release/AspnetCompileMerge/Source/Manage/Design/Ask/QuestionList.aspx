<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionList.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.Ask.QuestionList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>问题列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" OnRowDataBound="EGV_RowDataBound" PageSize="20" AllowPaging="true" CssClass="table table-striped table-bordered table-hover" OnRowCommand="EGV_RowCommand" EmptyDataText="没有数据">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemTemplate><input type="checkbox" value="<%#Eval("ID") %>" /></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>
                    <a href="javascript:;" onclick=""><%#Eval("QTitle") %></a>
                </ItemTemplate>
            </asp:TemplateField>
<%--            <asp:TemplateField HeaderText="创建用户" >
                <ItemTemplate>
                    <a href="javascript:;" onclick="showuser(<%#Eval("CUser") %>)"><%#GetUserName() %></a>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="问题选项">
                <ItemTemplate>
                    <%#GetOption() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="问题类型">
                <ItemTemplate><%#GetQType() %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="必填">
                <ItemTemplate>
                    <%#GetRequired() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="创建时间" DataField="CDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="javascript:;" ><i class="fa fa-pencil"></i>编辑</a> 
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del" OnClientClick="return confirm('确认删除？')" CssClass="option_style"><i class="fa fa-trash-o" title="<%=Resources.L.删除 %>"></i><%=Resources.L.删除 %></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    function showuser(id) {ShowComDiag("../../User/Userinfo.aspx?id=" + id,"查看用户")}
</script>
</asp:Content>
