<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuestBookShow.aspx.cs" Inherits="ZoomLaCMS.Manage.Guest.GuestBookShow" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <title>留言内容</title>
</asp:Content>
<asp:Content  ContentPlaceHolderID="Content" Runat="Server">
    <table class="table table-striped table-bordered table-hover" border="0" cellpadding="0" cellspacing="0" width="100%" style="border: #fff solid 1px; ">
        <tr align="center">
            <td colspan="2" class="spacingtitle"><strong>查看留言内容</strong></td>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="repFileReName_ItemCommand">
            <ItemTemplate>
                <tr>
                    <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>ID</strong></td>
                    <td class="tdbg" style="width: 85%; "><%# Eval("GID")%></td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 100px; text-align: center; "><strong>留言人</strong></td>
                    <td class="tdbg" style="width: 85%; "><a href="../User/UserInfo.aspx?id=<%# Eval("UserID") %>" title="点击查看该用户详情"><%# GetUserName(Eval("UserID","{0}")) %></a></td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 100px; text-align: center; "><strong>留言标题</strong></td>
                    <td class="tdbg" style="width: 85%; "><%# Eval("Title") %></td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 100px; text-align: center; "><strong>留言内容</strong></td>
                    <td class="tdbg" style="width: 85%; "><%# Eval("TContent") %></td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 100px; text-align: center; "><strong>IP地址</strong></td>
                    <td class="tdbg" style="width: 85%; "><%# Eval("IP") %></td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 100px; text-align: center; "><strong>添加时间</strong></td>
                    <td class="tdbg" style="width: 85%; "><%# Eval("GDate") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div id="Pager1" runat="server" style="display: none;"></div>
    <asp:HiddenField ID="HdnGID" runat="server" />
    <div class="clearfix"></div>
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="GID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Lnk_Click" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="chkSel" title="" value='<%#Eval("GID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="留言标题">
                <ItemTemplate>
                    <%# Eval("Title")%>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" />
            </asp:TemplateField>
            <asp:BoundField DataField="Tcontent" HeaderText="留言内容">
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                <HeaderStyle Width="65%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="留言时间">
                <ItemTemplate>
                    <%#Eval("GDate") %>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="留言人">
                <ItemTemplate>
                    <a href="../User/UserInfo.aspx?id=<%# Eval("UserID")%>" title="点击查看用户详情">
                        <%# GetUserName(Eval("UserID","{0}")) %></a>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                <HeaderStyle Width="5%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" CommandArgument='<%# Eval("GID") %>' OnClientClick="return confirm('确实要删除吗？');">删除</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                <HeaderStyle Width="5%" />
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center"  />
		<RowStyle HorizontalAlign="Center" />
    </ZL:ExGridView>
    <table class="TableWrap" border="0" cellpadding="0" cellspacing="0" width="100%" id="sleall">
        <tr>
            <td style="height: 21px">
                <asp:Button ID="btndelete" runat="server" CssClass="btn btn-primary" OnClientClick="return confirm('确定要删除选中的项目吗？')" Text="批量删除" OnClick="btndelete_Click" />
                <asp:HiddenField ID="HdnCateID" runat="server" />
            </td>
        </tr>
    </table>
    <div style="text-align: center; margin-top: 5px;">
        <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" OnClick="Button2_Click" Text="继续回复" />
        &nbsp;<asp:Button ID="Button1" runat="server" Text="返回列表" CssClass="btn btn-primary" OnClick="Button1_Click" />
    </div>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        $().ready(function () {
            $("#Egv tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
            //$("#Egv  tr>th").css("height", "30px").css("line-height", "30px");
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "chkSel");
            });
        });
    </script>
</asp:Content>