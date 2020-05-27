<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReplyGuest.aspx.cs" Inherits="ZoomLaCMS.Manage.Guest.ReplyGuest" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <title>回复留言</title>
    <script charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
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
    <div style="margin: 20px auto">
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
                    <textarea id="tx_Content" style="width: 100%; height: 150px;" name="TxtTContent" runat="server"></textarea>
                    <%=Call.GetUEditor("tx_Content") %>
                </td>
            </tr>
            <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
            <tr class="tdbgbottom border">
                <td></td>
                <td>
                    <asp:Button ID="EBtnSubmit" Text="保存回复" CssClass="btn btn-primary" OnClick="EBtnSubmit_Click" runat="server" />
                    <a href="javascript:;" onclick="history.go(-1);" class="btn btn-primary">返回</a>
                    <br />
                    <asp:HiddenField ID="HdnModel" runat="server" />
                    <asp:HiddenField ID="HiddenParentid" runat="server" />
                    <asp:HiddenField ID="HdnPubid" runat="server" />
                    <asp:HiddenField ID="HdnID" runat="server" />
                    <asp:HiddenField ID="HdnType" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>