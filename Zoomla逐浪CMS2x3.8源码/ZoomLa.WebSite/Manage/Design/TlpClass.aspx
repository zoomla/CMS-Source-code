<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TlpClass.aspx.cs" Inherits="Manage_Design_TlpClass" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>模板类别</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="5000" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="名称">
                <ItemTemplate>
                  <a href="AddTlpClass.aspx?ID=<%#Eval("ID") %>"><%#Eval("Name") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Remind" HeaderText="备注" />
            <asp:BoundField DataField="CDate" DataFormatString="{0:yyyy年MM月dd日}" HeaderText="创建时间" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a class="option_style" href="AddTlpClass.aspx?id=<%#Eval("ID") %>"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
<asp:Button runat="server" ID="BatDel_Btn" OnClick="BatDel_Btn_Click" OnClientClick="return confirm('确定要删除吗');" CssClass="btn btn-primary" Text="批量删除" />

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>