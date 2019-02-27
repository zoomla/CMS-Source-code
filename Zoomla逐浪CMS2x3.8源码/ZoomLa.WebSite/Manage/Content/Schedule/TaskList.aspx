<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskList.aspx.cs" Inherits="Manage_Content_Schedule_TaskList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>任务队列</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" ItemStyle-CssClass="td_s"/>
            <asp:BoundField HeaderText="任务名称" DataField="TaskName" />
            <asp:TemplateField HeaderText="运行状态">
                <ItemTemplate>
                    <%#Eval("IsRun") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="执行计划">
                <ItemTemplate>
                    <%#GetExecuteType() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="执行时间">
                <ItemTemplate>
                      <%#GetExecuteTime() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="最近一次执行">
                <ItemTemplate>
                    <%#GetLastTime() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="创建时间" DataField="CDate" DataFormatString="{0:yyyy年MM月dd日}"  />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a class="option_style" href="AddSche.aspx?id=<%#Eval("ID") %>"><i class="fa fa-pencil" title="修改"></i></a>
                    <a class="option_style" href="ScheLogList.aspx?TaskID=<%#Eval("ID") %>"><i class="fa fa-file"></i>查看日志</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
<a href="Default.aspx" class="btn btn-success">返回列表</a>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>