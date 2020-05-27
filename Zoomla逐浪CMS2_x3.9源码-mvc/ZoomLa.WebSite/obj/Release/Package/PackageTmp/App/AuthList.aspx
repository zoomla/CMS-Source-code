<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthList.aspx.cs" Inherits="ZoomLaCMS.App.AuthList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>授权审核</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs">
        <li id="navli_0"><a href="AuthList.aspx">全部</a></li>
        <li id="navli_1"><a href="AuthList.aspx?Filter=1">未授权</a></li>
        <li id="navli_2"><a href="AuthList.aspx?Filter=2">已授权</a></li>
    </ul>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有申请数据">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="网址">
                <ItemTemplate>
                    <a href="<%#Eval("SiteUrl") %>" target="_blank"><%#Eval("SiteUrl") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="联系人" DataField="Contact" />
            <asp:BoundField HeaderText="联系电话" DataField="MPhone" />
            <asp:BoundField HeaderText="申请时间" DataField="CDate" />
    <%--        <asp:BoundField HeaderText="状态" DataField="" />--%>
            <asp:BoundField HeaderText="授权码" DataField="AuthKey" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="audit_btn" CommandName="audit" CommandArgument='<%#Eval("ID") %>'>审核</asp:LinkButton>
                    <asp:LinkButton runat="server" ID="unaudit_btn" CommandName="unaudit" CommandArgument='<%#Eval("ID") %>'>取消审核</asp:LinkButton>
                    <asp:LinkButton runat="server" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button runat="server" ID="BatAudit_Btn" Text="审核并发送邮件" OnClick="BatAudit_Btn_Click" class="subcheck btn btn-primary"/>
    <asp:Button runat="server" ID="BatAudit2_Btn" Text="审核不发送邮件" OnClick="BatAudit2_Btn_Click" class="subcheck btn btn-primary"/>
    <asp:Button runat="server" ID="BatUnAudit_Btn" Text="取消审核" OnClick="BatUnAudit_Btn_Click" class="subcheck btn btn-primary"/>
    <asp:Button runat="server" ID="BatDel_Btn" Text="批量删除" OnClick="BatDel_Btn_Click" class="subcheck btn btn-primary"/>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/ICMS/ZL_Common.js"></script>
    <script>
        $(function () {
            var filter = "<%:Filter%>";
            $("#navli_" + filter).addClass("active");
            $(".subcheck").click(function () {
                var len = $("[name=idchk]:checked").length;
                if (len < 1) { alert("请先选定需要操作的数据!"); return false; }
                if (!confirm("确定要执行操作吗?")) { return false; }
                disBtn(this);
                return true;
            });
        })
    </script>
</asp:Content>