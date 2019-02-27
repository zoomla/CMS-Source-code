<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="ViewSmallPub.aspx.cs" Inherits="User_Pages_ViewSmallPub" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>互动模块管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="Default.aspx">企业黄页</a></li>
        <li class="active">互动模块管理</li>
    </ol>
    <asp:HiddenField ID="HiddenPubRole" runat="server" />
    <asp:HiddenField ID="HdnModelID" runat="server" />
    <asp:HiddenField ID="HiddenType" runat="server" />
    <asp:HiddenField ID="HiddenPubid" runat="server" />
    <asp:HiddenField ID="HdnID" runat="server" />
    <span>
        <asp:LinkButton ID="LbtnAllPub" runat="server" OnClick="LbtnAllPub_Click">所有信息</asp:LinkButton>
        |
        <asp:LinkButton ID="LbtnUNAuditedPub" runat="server" OnClick="LbtnUNAuditedPub_Click">待审核信息</asp:LinkButton>
        |
        <asp:LinkButton ID="LbtnuditedPub" runat="server" OnClick="LbtnuditedPub_Click">已审核信息</asp:LinkButton>
    </span>
    <table class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td width="5%">
                <asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" /></td>
            <td width="5%">ID</td>
            <td width="40%">
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>标题</td>
            <td width="10%">参与人数</td>
            <td width="18%">添加时间</td>
            <td width="32%">操作</td>
        </tr>
        <asp:Repeater ID="gvCard" runat="server">
            <ItemTemplate>
                <tr >
                    <td align="center">
                        <input name="Item" type="checkbox" value='<%# Eval("ID")%>' /></td>
                    <td align="center"><%# Eval("ID")%></td>
                    <td align="left">
                        <a href="javascript:void();" onclick="javascript: window.open('ViewSmallPub.aspx?Pubid=<%#this.HiddenPubid.Value%>&ID=<%#Eval("ID") %>', 'newwindow', 'height=500, width=800, top=200, left=150, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no'); ">
                            <%#returnlen(Eval("PubTitle"))%>
                        </a>
                    </td>
                    <td align="center"><%#Eval("Pubnum")%>
                    <td align="center"><%#Eval("PubAddTime")%>
                    <td align="center">
                        <a href="PubView.aspx?Pubid=<%#this.HiddenPubid.Value%>&ID=<%#Eval("ID") %>&topid=<%=Request.QueryString["ID"]%>&small=small">查看</a>
                         <%#showuse(DataBinder.Eval(Container.DataItem, "Pubstart").ToString(), Eval("ID").ToString())%>
                        &nbsp;<%#showedit( Eval("ID"),1)%>
                        &nbsp;<%#showedit( Eval("ID"),2)%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="7" align="center">
                共
                <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
                个商品
                <asp:Label ID="Toppage" runat="server" Text="" />
                <asp:Label ID="Nextpage" runat="server" Text="" />
                <asp:Label ID="Downpage" runat="server" Text="" />
                <asp:Label ID="Endpage" runat="server" Text="" />
                页次：<asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server" Text="" />
                页
                <asp:Label ID="pagess" runat="server" Text="" />个信息/页 转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                页
            </td>
        </tr>
    </table>
    <asp:Button ID="Button3" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" CssClass="btn btn-primary" Text="批量删除" OnClick="Button3_Click" Visible="false" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Common.js" type="text/javascript"></script>
    <script>
        function OpenLink(lefturl, righturl) {
            if (lefturl != "") {
                parent.frames["left"].location = lefturl;
            }
            parent.frames["main_right"].location = righturl;
        }
    </script>
</asp:Content>
