<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LargessMoney.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.LargessMoney" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>赠送游戏币</title>
    <style>
        #AllID_Chk{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" OnRowEditing="Egv_RowEditing" OnRowUpdating="Egv_RowUpdating" OnRowCancelingEdit="Egv_RowCancelingEdit" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有信息！！">
            <Columns>
                <asp:TemplateField HeaderText="用户名">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="250px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="拥有游戏币">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("DummyPurse") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" HeaderText="赠送" UpdateText="赠送" EditText="赠送">
                    <HeaderStyle Width="150px" />
                </asp:CommandField>
            </Columns>
        </ZL:ExGridView>
    </div>
</asp:Content>
