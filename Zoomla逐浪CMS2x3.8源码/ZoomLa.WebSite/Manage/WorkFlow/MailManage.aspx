<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailManage.aspx.cs" Inherits="Manage_WorkFlow_MailMange" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>模型管理</title>
    <style>
        #AllID_Chk{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <asp:DropDownList runat="server" ID="SizeStatus_Dp" CssClass="form-control" Width="200"  OnSelectedIndexChanged="SizeStatus_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Value="All">全部用户</asp:ListItem>
            <asp:ListItem Value="NoSize">无容量用户</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox runat="server" ID="searchText" placeholder="请输入用户名或工号" Width="200"  CssClass="form-control" />
        <asp:Button runat="server" CssClass="btn btn-primary" Text="查询"  ID="searchBtn" OnClick="searchBtn_Click" />
    </div>
    <div>
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" EnableTheming="False" GridLines="None"
            CellPadding="2" CellSpacing="1" Width="100%" CssClass=" table table-bordered table-hover table-striped" OnPageIndexChanging="EGV_PageIndexChanging" DataKeyNames="UserID"
            OnRowCommand="EGV_RowCommand" RowStyle-CssClass="tdbg" AllowUserToOrder="true" BackColor="White" EmptyDataText="当前没有类型!!">
            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="ID" SortExpression="UserID">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="会员名">
                    <ItemTemplate>
                        <a href="../User/UserInfo.aspx?id=<%# Eval("UserID") %>"><%# Eval("UserName","{0}") %></a>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="WorkNum" HeaderText="工号" SortExpression="WorkNum">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="会员组">
                    <ItemTemplate>
                        <a href="UserManage.aspx?GroupID=<%#Eval("GroupID","{0}") %>">
                            <%# GetGroupName(Eval("GroupID","{0}")) %></a>
                    </ItemTemplate>
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="邮箱使用状况">
                    <ItemTemplate>
                        <%#GetMailRemind() %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Center" />
        </ZL:ExGridView>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
