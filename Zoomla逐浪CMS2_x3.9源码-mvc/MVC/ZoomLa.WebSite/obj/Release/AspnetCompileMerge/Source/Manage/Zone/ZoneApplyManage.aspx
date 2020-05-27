<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZoneApplyManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.ZoneApplyManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>查看申请信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td width="3%" class="text-center">
                </td>
            <td width="12%" class="text-center"><span>申请人用户名</span></td>
            <td width="20%" class="text-center"><span>空间名称</span></td>
            <td width="20%" class="text-center"><span>申请时间</span></td>
            <td width="20%" class="text-center"><span>空间状态</span></td>
            <td width="8%" class="text-center"><span>操作</span></td>
        </tr>
        <asp:Repeater ID="Productlist" runat="server">
            <ItemTemplate>
                <tr>
                    <td class="text-center">
                        <input name="Item" type="checkbox" value='<%# Eval("ID")%>' /></td>
                    <td class="text-center"><%#GetUsername(DataBinder.Eval(Container.DataItem, "UserID").ToString())%></td>
                    <td class="text-center"><%# Eval("BlogName")%></td>
                    <td class="text-center"><%# Eval("Addtime").ToString()%></td>
                    <td class="text-center">待审核
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName='<%#Eval("ID")%>' OnClick="Button3_Click">审核通过</asp:LinkButton></td>
                    <td class="text-center">
                        <a href="ZoneEdit.aspx?id=<%#Eval("UserID")%>" class="option_style"><i class="fa fa-eye" title="查看"></i></a>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName='<%#Eval("ID")%>' OnClick="Button2_Click" OnClientClick="return confirm('不可恢复性删除数据,确定将该数据删除?');"
                            CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td style="text-align:center"><input type="checkbox" id="Checkall" onclick="javascript: CheckAll(this);" /></td>
            <td colspan="5" class="text-center">共
                <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
                <asp:Label ID="Toppage" runat="server" Text="" />
                <asp:Label ID="Nextpage" runat="server" Text="" />
                <asp:Label ID="Downpage" runat="server" Text="" />
                <asp:Label ID="Endpage" runat="server" Text="" />
                页次：
                    <asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server" Text="" />
                页 
                    <asp:Label ID="pagess" runat="server" Text="" />个/页
                     转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>页
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="批量审核" CommandName="0" OnClick="Button1_Click" />
                <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="批量删除" CommandName="5" OnClick="Button1_Click" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
    </script>
</asp:Content>
