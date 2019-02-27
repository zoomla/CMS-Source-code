<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotifyList.aspx.cs" Inherits="ZoomLaCMS.Plat.Addon.NotifyList"  MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>通知列表</title><style>.container .text_245{width:245px;}</style></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
    <div class="child_head">
        <span class="child_head_span1"></span><span class="child_head_span2">提示列表</span>
        <div class="input-group pull-right text_300" >
            <asp:TextBox ID="Skey_T" placeholder="提示标题" runat="server" CssClass="form-control text_245" />
            <span class="input-group-btn">
                <asp:Button ID="Search_B" runat="server" Text="<%$Resources:L,搜索 %>" class="btn btn-primary" OnClick="Search_B_Click" />
            </span>
        </div>
    </div>
    <table class="table table-bordered table-striped">
        <tr>
            <td>标题</td>
            <td>内容</td>
            <td>提示人</td>
            <td>时间</td>
            <td>操作</td>
        </tr>
        <ZL:ExRepeater runat="server" ID="RPT" PageSize="10" PagePre="<tr><td colspan='5' class='text-center'>" PageEnd="</td></tr>" OnItemCommand="RPT_ItemCommand">
            <ItemTemplate>
                <tr ondblclick="location='SetPrompt.aspx?ID=<%#Eval("ID") %>'">
                    <td><%#Eval("Title") %></td>
                    <td><%#Eval("Content") %></td>
                    <td><%#GetUser() %></td>
                    <td><%#Eval("BeginDate","{0:yyyy年MM月dd日 HH:mm}") %></td>
                    <%--  <td><%#GetStatus() %></td>--%>
                    <td>
                        <a href="SetPrompt.aspx?ID=<%#Eval("ID") %>"><i class="fa fa-pencil"></i></a>
                        <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del" OnClientClick="return confirm('确定要删除吗?');"><i class="fa fa-trash-o"></i></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
</div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
