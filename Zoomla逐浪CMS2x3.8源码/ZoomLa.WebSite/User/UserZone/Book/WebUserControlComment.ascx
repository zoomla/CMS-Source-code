<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControlComment.ascx.cs"
    Inherits="WebUserControlComment" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>
            <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnRowCommand="Egv_RowCommand" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
                <Columns>
                    <asp:TemplateField HeaderText="用户名">
                        <ItemTemplate>
                            <%# getusername(DataBinder.Eval(Container.DataItem, "UserID").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="标题" DataField="Ctitle" />
                    <asp:BoundField HeaderText="内容" DataField="Ccontent" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("ID") %>' CommandName="del1" runat="server" OnClientClick="return  confirm('你确定要删除这个评论吗？')" >删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" />
                <RowStyle HorizontalAlign="Center" />
            </ZL:ExGridView>
        </td>
    </tr>
    <tr>
        <td>
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td style="width: 100px;">标题:</td>
                    <td>
                        <asp:TextBox ID="titletxt" CssClass="form-control" runat="server" Width="260px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>内容</td>
                    <td>
                        <textarea id="TextArea1" class="form-control" style="width: 360px; height: 112px" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="commentBtn" CssClass="btn btn-primary" runat="server" OnClick="commentBtn_Click" Text="发表评论" /></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
