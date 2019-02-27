<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubscribeTypeList.aspx.cs" Inherits="manage_Qmail_SubscribeTypeList" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>订阅管理</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
        <div class="clearbox">
        </div>
        <table class="table table-striped table-bordered table-hover">
                <tr class="tdbg">
                    <td align="center" class="spacingtitle">
                        订阅管理</td>
                </tr>
                <tr class="tdbg">
        <td>
        添加订阅类别：<asp:TextBox ID="txtTepy" runat="server" class="form-control" style="max-width:200px; display:inline;"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="Button1"
                        runat="server" Text="添  加" OnClick="Button1_Click" class="btn btn-primary" />
        </td>
        </tr>
        <tr class="tdbg">
        <td>
            <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" DataKeyNames="ID">
                <Columns>
                    <asp:TemplateField HeaderText="编号">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="订阅类别名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SubscribeName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="160px" />
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("SubscribeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="调用代码">
                        <ItemTemplate>
                        <%#GetCode( DataBinder.Eval(Container.DataItem,"ID").ToString()) %>                 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="邮址列表" HeaderStyle-Width="55px" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <a href="MailListManage.aspx?typeid=<%#DataBinder.Eval(Container.DataItem,"ID") %>" title='<%#DataBinder.Eval(Container.DataItem,"SubscribeName")+"的邮址列表" %>'>邮址列表</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" ItemStyle-HorizontalAlign="Center" >
                        <HeaderStyle Width="60px" />
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ItemStyle-HorizontalAlign="Center" >
                        <HeaderStyle Width="40px" />
                    </asp:CommandField>
                </Columns>
            </ZL:ExGridView>
        </td>
        </tr>
       
        </table>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
