<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyExamResult.aspx.cs" Inherits="User_Questions_MyExamResult" ValidateRequest="false" EnableViewStateMac="false" MasterPageFile="~/User/Default.master"%>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
<title>考试结果</title>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="Content">
    <ol class="container breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx" target="_parent">会员中心</a></li>
        <li class="active"><a href="SelectAllStudent.aspx">我的成绩</a></li>
		<div class="clearfix"></div>
    </ol>
    <div id="pageflag" data-nav="edu" data-ban="ke"></div>
    <div class="container">
     <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" 
        OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" OnRowCommand="Egv_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="流水号" DataField="FlowID" ItemStyle-CssClass="td_l"/>
            <asp:TemplateField HeaderText="试卷名称">
                <ItemTemplate><a href="ExamDetail.aspx?ID=<%#Eval("PaperID")+"&Action=View&FlowID="+Eval("FlowID") %>" title="查看详情"><%#Eval("PaperName") %></a></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="分数" DataField="TotalScore" ItemStyle-CssClass="td_m"/>
            <asp:TemplateField HeaderText="阅卷教师" ItemStyle-CssClass="td_l">
                <ItemTemplate><%#Eval("TechName")==DBNull.Value?"尚未阅卷":Eval("TechName") %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="答题日期" DataField="CDate" DataFormatString="{0:yyyy年MM月dd日 HH:mm}" ItemStyle-CssClass="td_l" />
            <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="td_l">
                <ItemTemplate>
                    <a href="ExamDetail.aspx?ID=<%#Eval("PaperID")+"&Action=View&FlowID="+Eval("FlowID") %>">查看详情</a>
                    <a href="ExamDetail.aspx?ID=<%#Eval("PaperID") %>">再次考试</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
  </div>
</asp:Content>
