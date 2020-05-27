<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewList.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.ViewList" MasterPageFile="~/Manage/I/Default.master"%> 
<%@ Register Src="~/Manage/I/ASCX/SPwd.ascx" TagPrefix="uc1" TagName="SPwd" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>视图列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <uc1:SPwd runat="server" ID="SPwd" Visible="false" />
    <div id="maindiv" runat="server" visible="false">
<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ViewName") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="视图名" DataField="ViewName" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="ViewInfo.aspx?ViewName=<%#Eval("ViewName") %>" class="option_style"><i class="fa fa-eye" title="查看"></i></a>
                    <a href="CreateView.aspx?ViewName=<%#Eval("ViewName") %>" class="option_style"><i class="fa fa-pencil" title="编辑"></i></a>
                    <asp:LinkButton runat="server" CommandName="del2" CommandArgument='<%#Eval("ViewName") %>' OnClientClick="return confirm('你确认要删除该视图吗？')" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
     <%--   <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('此操作将删除现有站点数据，确认？');" Text="批量删除" OnClick="Button3_Click" />--%>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function CheckAll(spanChk) {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++) if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                if (elm[i].checked != xState)
                    elm[i].click();
            }
        }
</script>
</asp:Content>