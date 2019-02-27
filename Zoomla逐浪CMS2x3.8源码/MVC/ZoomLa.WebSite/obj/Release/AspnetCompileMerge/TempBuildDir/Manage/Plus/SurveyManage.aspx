<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Plus.SurveyManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>问卷投票管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV"  DataKeyNames="Surveyid"  CssClass="table table-striped table-bordered"
        GridLines="None" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnRowCommand="gviewSurvey_RowCommand"
         OnRowDataBound="gviewSurvey_RowDataBound" OnPageIndexChanging="EGV_PageIndexChanging">
        <EmptyDataTemplate>无相关数据</EmptyDataTemplate>
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_xs">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("SurveyID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="SurveyID" HeaderText="ID" ItemStyle-CssClass="td_s" />
            <asp:TemplateField HeaderText="问卷名称">
                <ItemTemplate>
                    <a href="/Plugins/Vote.aspx?SID=<%#Eval("SurveyID") %>" target="_blank"><%#Eval("Surveyname") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="类别" ItemStyle-CssClass="td_m">
                <ItemTemplate><%# GetSurType(Eval("SurType","{0}"))%> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="启用" ItemStyle-CssClass="td_m">
                <ItemTemplate>
                    <asp:ImageButton ID="imgbtnCanNull" runat="server" CommandArgument='<%# Eval("SurveyID", "{0}") %>' ImageUrl='<%# "~/Images/" + BoolValueToImgName(Eval("Isopen", "{0}")) +".gif" %>' OnClick="imgbtnCanNull_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作" ItemStyle-Width="30%">
                <ItemTemplate>
                    <a href="/Plugins/Vote.aspx?Sid=<%#Eval("SurveyID") %>" target="_blank" class="option_style"><i class="fa fa-eye"></i>查看</a>
                    <a href="SurveyItemList.aspx?SID=<%#Eval("SurveyID") %>" class="option_style"><i class="fa fa-list-alt"></i>问题列表</a>
                    <a href="SurveyResult.aspx?SID=<%#Eval("SurveyID") %>" class="option_style"><i class="fa fa-pie-chart" title="分析"></i>结果分析</a>
                    <a href="SurveyViewer.aspx?SID=<%#Eval("SurveyID") %>" class="option_style"><i class="fa fa-magic"></i>问卷管理</a>
                    <a href="Survey.aspx?SID=<%# Eval("SurveyID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton ID="lbtnDel" runat="server" CommandName="Del" CommandArgument='<%# Eval("SurveyID") %>' OnClientClick="return confirm('确实要删除吗？');" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <div id="divBtns" runat="server">
        <asp:Button ID="btndelete" runat="server" OnClientClick="return CanDelete();" Text="批量删除" OnClick="btndelete_Click" class="btn btn-primary" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        // 判断是否有选中的项目
        function HasChecked() {
            var flag = false;
            var chks = document.getElementById("gviewSurvey").getElementsByTagName("input");
            for (i = 0; i < chks.length; i++) {
                if (chks[i].type == 'checkbox' && chks[i].checked) {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        function CanDelete() {
            if (HasChecked()) {
                return confirm("确定要删除选中的项目吗？");
            }
            alert("请先选中要删除的项目！");
            return false;
        }
    </script>
</asp:Content>
