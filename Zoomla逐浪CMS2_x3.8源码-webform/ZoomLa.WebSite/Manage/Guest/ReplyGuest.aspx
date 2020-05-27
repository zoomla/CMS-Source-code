<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="ReplyGuest.aspx.cs" Inherits="Manage_I_Guest_ReplyGuest" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <title>回复留言</title>
</asp:Content>
<asp:Content  ContentPlaceHolderID="Content" Runat="Server">
    <table class="table table-striped table-bordered table-hover" border="0" cellpadding="0" cellspacing="0" width="100%">
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="repFileReName_ItemCommand">
            <ItemTemplate>
                <tr>
                    <td class="tdbgleft" style="width: 100px; text-align: center; "><strong>ID</strong></td>
                    <td class="tdbg" style="width: 85%; "><%# Eval("GID")%></td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 100px; text-align: center; "><strong>留言人</strong></td>
                    <td class="tdbg" style="width: 85%; "><a href="../User/UserInfo.aspx?id=<%# Eval("UserID") %>" title="点击查看该用户详情"><%# GetUserName(Eval("UserID","{0}")) %></a></td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>留言标题</strong></td>
                    <td class="tdbg" style="width: 85%;"><%# Eval("Title") %></td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>留言内容</strong></td>
                    <td class="tdbg" style="width: 85%;"><%# Eval("TContent") %></td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>IP地址</strong></td>
                    <td class="tdbg" style="width: 85%;"><%# Eval("IP") %></td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>添加时间</strong></td>
                    <td class="tdbg" style="width: 85%;"><%# Eval("GDate") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div id="Pager1" runat="server" style="display:none;"></div>
    <asp:HiddenField ID="HdnGID" runat="server" />
    <div style="margin-top: 20px;">
        <table style="width: 100%; margin: 0 auto; margin-top: 5px;" cellpadding="0" cellspacing="0" class="table table-striped table-bordered table-hover">
            <tr class="tdbg">
                <td colspan="2" class="title" style="text-align: center">
                    <asp:Label ID="LblModelName" runat="server" Text="回复留言"></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                <td align="right" class="tdbgleft">标题：
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" CssClass="form-control pull-left" runat="server" Width="365px" /><span style="color:#1e860b;margin-left:5px;">*非必填项</span>
                </td>
            </tr>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                <td align="right" class="tdbgleft">内容：
                </td>
                <td>
                    <asp:TextBox ID="tx_Content" runat="server" CssClass="form-control" Height="107px" TextMode="MultiLine" Width="427px"></asp:TextBox>
                </td>
            </tr>
            <asp:Literal ID="ModelHtml" runat="server"></asp:Literal><tr class="tdbgbottom border">
            <td colspan="2" class="text-center">
                <asp:HiddenField ID="HdnModel" runat="server" />
                <asp:HiddenField ID="HiddenParentid" runat="server" />
                <asp:HiddenField ID="HdnPubid" runat="server" />
                <asp:HiddenField ID="HdnID" runat="server" />
                <asp:HiddenField ID="HdnType" runat="server" />
                <div class="clearbox"></div><div class="clearbox"></div><div class="clearbox"></div>
                <asp:TextBox ID="FilePicPath" runat="server" Text="fbangd" Style="display: none"></asp:TextBox>
                <asp:Button ID="EBtnSubmit" Text="保存回复" CssClass="btn btn-primary" OnClick="EBtnSubmit_Click" runat="server" />
                <asp:Button ID="Button1" Text="返回列表" class="btn btn-primary" runat="server" OnClick="Button1_Click" />
                <br />
            </td>
            </tr>
        </table>
    </div>
</asp:Content>