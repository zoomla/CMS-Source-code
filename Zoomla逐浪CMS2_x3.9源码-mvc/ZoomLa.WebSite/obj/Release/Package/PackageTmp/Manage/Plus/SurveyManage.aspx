<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Plus.SurveyManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>问卷投票管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="float: left;">问卷投票名：</div>
    <table runat="server" visible="false">
        <tr>
            <td>
                <asp:TextBox ID="txtSurKey" BorderColor="#fff" CssClass="l_inpnon" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click" class="C_sch" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnKey" runat="server" />
    <ZL:ExGridView ID="EGV" RowStyle-HorizontalAlign="Center" DataKeyNames="Surveyid" CellPadding="2" CellSpacing="1" BackColor="White" CssClass="table table-striped table-bordered table-hover"
        GridLines="None" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" Width="100%" OnRowCommand="gviewSurvey_RowCommand" OnRowDataBound="gviewSurvey_RowDataBound" OnPageIndexChanging="EGV_PageIndexChanging">
        <EmptyDataTemplate>无相关数据</EmptyDataTemplate>
        <EmptyDataRowStyle BackColor="#e8f4ff" Height="45px" BorderColor="#4197e2" />
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("SurveyID") %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:BoundField DataField="SurveyID" HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" />
            <asp:HyperLinkField DataTextField="Surveyname" HeaderText="问卷投票名" Target="_blank" DataNavigateUrlFields="Surveyid" DataNavigateUrlFormatString="~/Plugins/Vote.aspx?SID={0}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30%" />
            <asp:TemplateField HeaderText="类别">
                <ItemTemplate><%# GetSurType(Eval("SurType","{0}"))%> </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="10%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="启用">
                <ItemTemplate>
                    <asp:ImageButton ID="imgbtnCanNull" runat="server" CommandArgument='<%# Eval("SurveyID", "{0}") %>' ImageUrl='<%# "~/Images/" + BoolValueToImgName(Eval("Isopen", "{0}")) +".gif" %>' OnClick="imgbtnCanNull_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="10%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("SurveyID") %>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                    <asp:LinkButton ID="lbtnDel" runat="server" CommandName="Del" CommandArgument='<%# Eval("SurveyID") %>' OnClientClick="return confirm('确实要删除吗？');" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <asp:LinkButton ID="lbtnList" runat="server" CommandName="QList" CommandArgument='<%# Eval("SurveyID") %>' CssClass="option_style"><i class="fa fa-list-alt"></i>问题列表</asp:LinkButton>
                    <asp:LinkButton ID="LbtnAnalyze" runat="server" CommandName="Result" CommandArgument='<%# Eval("SurveyID") %>' CssClass="option_style"><i class="fa fa-pie-chart" title="分析"></i>结果分析</asp:LinkButton>
                    <asp:LinkButton ID="lbtnManage" runat="server" CommandName="Manage" CommandArgument='<%# Eval("SurveyID") %>' CssClass="option_style"><i class="fa fa-magic" title="管理"></i>问卷管理</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
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
