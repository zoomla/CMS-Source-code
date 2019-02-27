<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuestBookShow.aspx.cs" Inherits="ZoomLaCMS.Manage.Guest.GuestBookShow" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <title>留言内容</title>
    <script charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
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
            <asp:TemplateField HeaderStyle-Width="2%" >
                <ItemTemplate>
                    <input type="checkbox" name="idchk" title="" value='<%#Eval("GID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GID" HeaderText="ID" HeaderStyle-Width="3%" />
            <asp:TemplateField HeaderText="留言标题">
                <ItemTemplate>
                    <%# Eval("Title")%>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="留言内容">
                <ItemTemplate>
                    <%#Eval("TContent","") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="留言时间">
                <ItemTemplate>
                    <%#Eval("GDate") %>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="留言人">
                <ItemTemplate>
                    <a href="../User/UserInfo.aspx?id=<%# Eval("UserID")%>" title="点击查看用户详情">
                        <%# GetUserName(Eval("UserID","{0}")) %></a>
                </ItemTemplate>
                <HeaderStyle Width="5%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="10%" >
                <ItemTemplate>
                    <a href='ReplyGuest.aspx?ID=<%#Eval("GID") %>&CateID=<%=Request.QueryString["CateID"] %>&&GID=<%=Request.QueryString["GID"] %>' class="option_style"><i class="fa fa-pencil"></i>修改</a>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" CommandArgument='<%# Eval("GID") %>' CssClass="option_style" OnClientClick="return confirm('确实要删除吗？');"><i class=" fa fa-trash-o"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
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
</asp:Content>