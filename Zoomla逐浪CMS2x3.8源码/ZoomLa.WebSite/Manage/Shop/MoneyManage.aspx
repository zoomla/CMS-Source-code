<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoneyManage.aspx.cs" Inherits="Zoomla.Website.manage.Shop.MoneyManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>货币管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="Flow" PageSize="10" OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Egv_RowCommand" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据！！">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value='<%#Eval("Flow") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="默认" HeaderStyle-CssClass="td_s">
                <ItemTemplate>
                    <%#(Eval("Is_flag").ToString()=="1")?"<span class='rd_green'>是</span>":"否"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderStyle-Width="15%" HeaderText="货币名称" DataField="Money_descp" />
            <asp:BoundField HeaderStyle-Width="15%" HeaderText="货币代码" DataField="Money_code" />
            <asp:BoundField HeaderStyle-Width="15%" HeaderText="货币符号" DataField="Money_sign" />
            <asp:TemplateField HeaderText="当前汇率">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <%#Eval("Money_rate","{0:0.00}")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="AddMoney.aspx?id=<%#Eval("Flow") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton ID="LinkButton1" CommandName="del1" CommandArgument='<%# Eval("Flow") %>' OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" runat="server" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button ID="Button1" class="btn btn-primary " Text="删除选中货币" runat="server" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('不可恢复性删除数据,你确定将该数据删除吗？')}" OnClick="Button1_Click" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        function IsSelectedId() {
            var checkArr = $("[name=idchk]:checked");
            if (checkArr.length > 0)
                return true
            else
                return false;
        }
    </script>
</asp:Content>
