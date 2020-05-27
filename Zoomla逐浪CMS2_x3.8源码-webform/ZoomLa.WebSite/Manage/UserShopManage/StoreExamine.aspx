<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreExamine.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="manage_UserShopManage_StoreExamine" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>商品列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("GeneralID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请人用户名">
                <ItemTemplate>
                    <%#Eval("Inputer","")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="店铺名称">
                <ItemTemplate>
                    <%#Eval("Title") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请时间">
                <ItemTemplate>
                     <%#Eval("CreateTime","{0:yyyy年MM月dd日 HH:mm}") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="店铺状态">
                <ItemTemplate>
                   <%#GetStatus() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="StoreEdit.aspx?id=<%#Eval("GeneralID")%>">查看</a>
                    <asp:LinkButton runat="server" CommandName='audit' CommandArgument='<%#Eval("GeneralID")%>' >通过审核</asp:LinkButton>
                    <asp:LinkButton runat="server" CommandName='del2' CommandArgument='<%#Eval("GeneralID") %>' OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <div>
        <asp:Button ID="BatAudit_Btn" CssClass="btn btn-primary" runat="server" CommandName="1" Text="批量审核" OnClick="BatAudit_Btn_Click" />
        <asp:Button ID="BatDel_Btn" CssClass="btn btn-primary" runat="server" Text="批量删除" CommandName="5" OnClick="BatDel_Click" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" />
    </div>
</asp:Content>
