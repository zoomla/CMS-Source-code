<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuestManage.aspx.cs" Inherits="Manage_Guest_GuestManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server"><title>留言管理</title><style>.modalClose{width:30px;height:20px;}.modalClose i{ display:block;margin-top:6px;font-size:12px;}</style></asp:Content>
<asp:Content  ContentPlaceHolderID="Content" Runat="Server">
    <asp:Repeater runat="server" ID="RPT" OnItemCommand="RPT_ItemCommand">
        <HeaderTemplate>
            <table id="EGV" class="table table-bordered table-hover table-striped">
             <tr><td>ID</td><td>留言名称</td><td>留言状态</td><td>访问权限</td><td>留言总数</td><td>操作</td></tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr data-layer="1" data-id="<%#Eval("CateID") %>" data-pid="0" ondblclick="ShowCate(<%#Eval("CateID") %>,0);">
                <td><%#Eval("CateID") %></td>
                
                <td style="text-align:left;"><a href="Default.aspx?CateID=<%#Eval("CateID") %>"><%#GetIcon() %></a><%#GetCateName()%></td>
                <td><span style="color:blue;"><%#GetBarStatus(Eval("BarInfo").ToString()) %></span></td>
                <td><%#GetNeedLog(Eval("NeedLog").ToString()) %></td>
                <td><%#Eval("GCount") %></td>
                <td class='optd'>
                    <a href="javascript:;" class="option_style" onclick="ShowCate(<%#Eval("CateID") %>,0);"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton CommandName="Del" CssClass="option_style" CommandArgument='<%#Eval("CateID") %>' OnClientClick="return confirm('确实要删除吗？');" runat="server"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <script>
        function ShowCate(id, pid) {
            location.href = "BarConfig.aspx?ID=" + id + "&GType=0&PID=" + pid;
        }
    </script>
</asp:Content>