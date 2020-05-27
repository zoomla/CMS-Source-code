<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CarRuleManage.aspx.cs" Inherits="manage_Zone_CarRuleManage" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>抢车位规则管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
         <ZL:ExGridView runat="server" ID="EGV" DataKeyNames="ID" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="暂无信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" > 
            <Columns>
                <asp:TemplateField HeaderText="规则名称">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("CText") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="规则数值">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Cvalue") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Cvalue") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" HeaderText="操作" />
            </Columns>
         <PagerStyle HorizontalAlign="Center" />
        <RowStyle Height="24px" HorizontalAlign="Center"  />
</ZL:ExGridView>
    </div>
</asp:Content>
