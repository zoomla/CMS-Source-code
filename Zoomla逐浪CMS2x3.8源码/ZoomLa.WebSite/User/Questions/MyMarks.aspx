<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyMarks.aspx.cs" Inherits="User_Questions_MyMarks" EnableViewStateMac="false" MasterPageFile="~/User/Default.master" %>

<%@ Register Src="~/Manage/I/ASCX/TreeView.ascx" TagPrefix="ZL" TagName="TreeView" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <title>系统试题管理</title>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript">
        function OpenSelect2(obj) {
            window.open('/User/Questions/ExamViewLX.aspx?id=' + obj, '', 'width=600,height=450,resizable=1,scrollbars=yes');
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="Content">
    <div class="container margin_t5">
        <div id="pageflag" data-nav="edu" data-ban="ke"></div>
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx" target="_parent">会员中心</a></li>
            <li class="active"><a href="MyMarks.aspx">我的考试</a></li>
            <div class="clearfix"></div>
        </ol>
        <div class="container-fluid">
            <div class="col-lg-2 col-md-2 col-xs-2 col-sm-2">
                <ZL:TreeView ID="MyTree" NodePid="C_Classid" NodeID="C_id" NodeName="C_ClassName" runat="server" />

            </div>
            <div class="col-lg-10 col-md-10 col-xs-10 col-sm-10">
                <div class="divbox" id="nocontent" runat="server" style="display: none; float: right; width: 78%">暂无试卷信息</div>
                <div style="float: right; width: 100%">
                    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10"
                        OnPageIndexChanging="EGV_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True"
                        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有试卷信息">
                        <Columns>
                            <asp:TemplateField HeaderText="试卷名称" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate><a href="ExamDetail.aspx?ID=<%#Eval("ID") %>" title="参加考试"><%# Eval("p_name")%></a></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="考试时间" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate><%# Eval("p_UseTime","").Equals("0")?"不限时间": Eval("p_UseTime")+"分钟"%> </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="有效时间" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate><%#DataBinder.Eval(Container.DataItem, "p_BeginTime", "{0:yyyy-MM-dd}")%> 至 <%#DataBinder.Eval(Container.DataItem, "p_endTime", "{0:yyyy-MM-dd}")%> </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="参加考试" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <a href="ExamDetail.aspx?ID=<%#Eval("ID") %>" target="_blank">参加考试</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </ZL:ExGridView>
                </div>
            </div>
        </div>
    </div>
    <script>
    </script>
</asp:Content>
