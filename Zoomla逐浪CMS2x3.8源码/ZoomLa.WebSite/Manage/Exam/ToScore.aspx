<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToScore.aspx.cs" Inherits="manage_Exam_ToScore" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>试卷评阅</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10"
        OnPageIndexChanging="EGV_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有试卷信息">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="流水号" DataField="FlowID" ItemStyle-CssClass="td_l" />
            <asp:TemplateField HeaderText="试卷名称">
                <ItemTemplate><a href="/User/Exam/ExamDetail.aspx?ID=<%#Eval("PaperID")+"&Action=View&FlowID="+Eval("FlowID") %>" target="_blank" title="查看详情"><%#Eval("PaperName") %></a></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="用户名" DataField="UserName" />
            <asp:BoundField HeaderText="分数" DataField="TotalScore" ItemStyle-CssClass="td_m" />
            <asp:TemplateField HeaderText="阅卷教师" ItemStyle-CssClass="td_l">
                <ItemTemplate><%#Eval("TechName")==DBNull.Value?"尚未阅卷":Eval("TechName") %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="答题日期" DataField="CDate" DataFormatString="{0:yyyy年MM月dd日 HH:mm}" ItemStyle-CssClass="td_l" />
            <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="td_l">
                <ItemTemplate>
                    <a target="_blank" href="/User/Exam/ExamDetail.aspx?ID=<%#Eval("PaperID")+"&Action=View&FlowID="+Eval("FlowID") %>" class="option_style"><i class="fa  fa-pencil-square-o" title="批阅"></i>批阅试卷</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript" src="/JS/Drag.js"></script>
    <script type="text/javascript" src="/js/Common.js"></script>
</asp:Content>
    