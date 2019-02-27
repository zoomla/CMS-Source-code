<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnsDetailList.aspx.cs" Inherits="Manage_Design_Ask_AnsDetailList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>答题详情</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="问题标题">
                <ItemTemplate>
                    <span class="rd_green">[<%#GetQType() %>]</span><%#Eval("QTitle") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户回答">
                <ItemTemplate>
                    <%#GetAnswer() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="是否必填"><ItemTemplate><%#GetRequired() %></ItemTemplate></asp:TemplateField>
            <asp:BoundField HeaderText="日期" DataField="CDate" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del2" OnClientClick="return confirm('确定要删除吗?');"><i class="fa fa-trash"></i> 删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    
</asp:Content>