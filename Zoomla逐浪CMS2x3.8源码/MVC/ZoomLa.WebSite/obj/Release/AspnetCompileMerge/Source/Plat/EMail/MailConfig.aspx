<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailConfig.aspx.cs" Inherits="ZoomLaCMS.Plat.EMail.MailConfig" MasterPageFile="~/Plat/Main.master" %>
<%@ Register Src="~/Plat/Common/EmailNav.ascx" TagPrefix="uc1" TagName="EmailNav" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>邮箱配置</title><style>.Messge_nav { margin-bottom: 10px; margin-top: 10px; }</style></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
    <uc1:EmailNav runat="server" ID="EmailNav" /><a href="javascript:;" class="btn btn-primary pull-right margin_t5" onclick="ShowAdd();">添加邮箱</a>
    <div class="clearfix"></div>
    <ZL:ExGridView runat="server" ID="EGV" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" 
        AllowPaging="true" PageSize="10" EnableTheming="False" GridLines="None" EmptyDataText="您尚未配置邮箱!!" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <%#Eval("ID") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="名称">
                <ItemTemplate>
                    <%#Eval("Alias") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="POP" DataField="POP" />
            <asp:BoundField HeaderText="SMTP" DataField="SMTP" />
            <asp:TemplateField HeaderText="邮箱名">
                <ItemTemplate>
                    <%#Eval("ACount") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建时间">
                <ItemTemplate>
                    <%#Eval("CDate") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                 <ItemTemplate>
        <%--             <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="receive"><i class="fa fa-envelope-o"></i> 接收邮件</asp:LinkButton>--%>
                     <a href="javascript:;" onclick="ShowEdit(<%#Eval("ID") %>);"><i class="fa fa-pencil"></i> 修改</a>
                     <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del2"><i class="fa fa-trash-o"></i> 删除</asp:LinkButton>
                 </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button ID="Dels" runat="server" Text="批量删除" OnClick="Dels_Click" CssClass="btn btn-info" OnClientClick="return confirm('确定要删除吗?');" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    $(function () { setactive("邮件"); })
    function ShowAdd() {
        ShowComDiag("AddMailConfig.aspx", "添加邮箱");
    }
    function ShowEdit(id) {
        ShowComDiag("AddMailConfig.aspx?id="+id, "修改邮箱");
    }
</script>
</asp:Content>