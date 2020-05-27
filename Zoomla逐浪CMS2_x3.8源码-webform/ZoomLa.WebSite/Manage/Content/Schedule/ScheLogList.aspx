<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScheLogList.aspx.cs" Inherits="Manage_Content_Schedule_ScheLogList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>任务日志</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
        <Columns>
            <%--<asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:BoundField HeaderText="任务名称" DataField="TaskName" />
            <asp:TemplateField HeaderText="任务计划">
                <ItemTemplate>
                    <%#GetResult() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="执行结果">
                <ItemTemplate>
                   <%#GetExecuteType() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CDate" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" HeaderText="执行时间" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style>
.allchk_l{display:none;}
</style>
</asp:Content>