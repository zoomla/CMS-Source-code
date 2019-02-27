<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZoneManage.aspx.cs" Inherits="manage_ZoneManage_ProductManage" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>会员空间配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td width="3%" class="text-center">
                
            <td width="12%" class="text-center"><span>申请人用户名</span></td>
            <td width="20%" class="text-center"><span>空间名称</span></td>
            <td width="20%" class="text-center"><span>申请时间</span></td>
            <td width="20%" class="text-center"><span>空间状态</span></td>
            <td width="8%" class="text-center"><span>操作</span></td>
        </tr>
        <asp:Repeater ID="Productlist" runat="server">
            <ItemTemplate>
                <tr ondblclick="View(<%# Eval("UserID")%>)">
                    <td class="text-center"><input name="Item" type="checkbox" value='<%# Eval("ID")%>' /></td>
                    <td class="text-center"><%#GetUsername(DataBinder.Eval(Container.DataItem, "UserID").ToString())%></td>
                    <td class="text-center"><%# Eval("BlogName")%></td>
                    <td class="text-center"><%# Eval("Addtime").ToString()%></td>
                    <td class="text-center"><%#GetState(DataBinder.Eval(Container.DataItem, "CommendState").ToString())%></td>
                    <td class="text-center">
                        <a href="ZoneEdit.aspx?id=<%#Eval("UserID")%>" class="option_style"><i class="fa fa-eye" title="查看"></i></a>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName='<%#Eval("ID")%>' OnClick="Button2_Click" OnClientClick="return confirm('不可恢复性删除数据,确定将该数据删除?');"
                             CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td style="text-align:center"><input id="Checkall" type="checkbox" onclick="javascript: CheckAll(this);" /></td>
            <td colspan="5" class="text-center">
                共
                <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
                条
                <asp:Label ID="Toppage" runat="server" Text="" />
                <asp:Label ID="Nextpage" runat="server" Text="" />
                <asp:Label ID="Downpage" runat="server" Text="" />
                <asp:Label ID="Endpage" runat="server" Text="" />
                页次：
                <asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server" Text="" />
                页
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                条/页 转到第
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                页
            </td>
        </tr>
        <tr>
            <td colspan="6" class="text-center">
                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="设为推荐" CommandName="1" OnClick="Button1_Click" />
                <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="设为关闭" CommandName="2" OnClick="Button1_Click" />
                <asp:Button ID="Button6" class="btn btn-primary" runat="server" Text="取消推荐" CommandName="0" OnClick="Button1_Click" />
                <asp:Button ID="Button5" class="btn btn-primary" runat="server" Text="取消关闭" CommandName="0" OnClick="Button1_Click" />
                <asp:Button ID="Button3" class="btn btn-primary" runat="server" Text="批量删除" CommandName="5" OnClick="Button1_Click" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        function View(UserID) {
            location.href = "ZoneEdit.aspx?id=" + UserID;
        }
    </script>
</asp:Content>
