<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegularList.aspx.cs" Inherits="test_RegularList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>规则列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="充值金额" DataField="Min" DataFormatString="{0:f2}" />
            <asp:BoundField HeaderText="金额" DataField="Purse" DataFormatString="{0:f2}" />
            <asp:BoundField HeaderText="银币" DataField="SIcon" DataFormatString="{0:f2}" />
            <asp:BoundField HeaderText="积分" DataField="Point" DataFormatString="{0:f2}" />
            <asp:BoundField HeaderText="备注(用户可见)" DataField="UserRemind" />
            <asp:BoundField HeaderText="备注" DataField="AdminRemind"/>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a class="option_style" href="AddRegular.aspx?id=<%#Eval("ID") %>"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>