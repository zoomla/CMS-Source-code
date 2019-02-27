<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProducerManage.aspx.cs" Inherits="Zoomla.Website.manage.Shop.ProducerManage" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>厂商管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Egv_RowCommand" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无厂商信息！！">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="chkSel" value='<%#Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="厂商名称" DataField="Producername" />
            <asp:BoundField HeaderText="厂商缩写" DataField="Smallname" />
            <asp:BoundField HeaderText="厂商分类" DataField="CoClass" />
            <asp:TemplateField HeaderText="已启用">
                <ItemTemplate>
                    <%#showstop2( Eval("Disable", "{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="属性">
                <ItemTemplate>
                    <%#showtop2( Eval("istop", "{0}"))%>
                    <%#showjian2( Eval("Isbest", "{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="Producer.aspx?menu=edit&id=<%#Eval("id") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton ID="LinkButton4" CommandName="Del" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" runat="server" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1" CommandName="Stop" CommandArgument='<%#Eval("ID") %>' runat="server" CssClass="option_style" ><%#showstop( Eval("Disable", "{0}"))%></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" CommandName="SetTop" CommandArgument='<%#Eval("ID") %>' runat="server" CssClass="option_style"><%#showtop( Eval("istop", "{0}"))%></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" CommandName="Tui" CommandArgument='<%#Eval("ID") %>' runat="server" CssClass="option_style"><%#showjian(Eval("Isbest", "{0}"))%></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button ID="Button1" class="btn btn-primary" Text="删除选中厂商" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" OnClick="Button1_Click" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        $().ready(function () {
            $("#Egv tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "chkSel");
            });
        })
        function IsSelectedId() {
            var checkArr = $("input[type=checkbox][name=chkSel]:checked");
            if (checkArr.length > 0)
                return true
            else
                return false;
        }
    </script>
</asp:Content>