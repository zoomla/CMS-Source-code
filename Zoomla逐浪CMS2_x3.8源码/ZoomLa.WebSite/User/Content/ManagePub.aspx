<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="ManagePub.aspx.cs" Inherits="User_ManagePub" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server"><title>互动管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="pub"></div>
<div class="container margin_t10">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">我的互动</li>
    </ol>
    <div class="margin_t10">
        <ul class="nav nav-tabs">
            <asp:Repeater ID="Node_RPT" runat="server" EnableViewState="false">
                <ItemTemplate>
                    <li class="<%#Eval("PubID","").Equals(PubID.ToString())?"active":"" %>"><a href="?pubID=<%#Eval("pubID","{0}")%>"><%#Eval("PubName","{0}")%></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <table class="table table-striped table-bordered">
            <tr>
                <td class="td_s">ID</td>
                <td class="td_l">互动标题</td>
                <td>互动内容</td>
                <td class="td_l">发表时间</td>
                <td class="td_md">操作</td>
            </tr>
            <ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='6' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>" OnItemCommand="Repeater2_ItemCommand">
                <ItemTemplate>
                    <tr id="<%#Eval("ID") %>" ondblclick="ShowTabs(this.id)">
                        <td><%#Eval("ID")%></td>
                        <td><%#ZoomLa.Common.BaseClass.Left(Eval("PubTitle","{0}"),45)%></td>
                        <td><%#ZoomLa.Common.BaseClass.Left(Eval("PubContent","{0}"),45)%></td>
                        <td><%#Eval("PubAddTime")%></td>
                        <td>
                            <a href="Reply.aspx?pubid=<%#Eval("Pubupid")+"&id="+Eval("ID") %>">回复</a>|
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID")+":"+Eval("Pubupid") %>' CommandName="Del" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">删除</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
        </table>
    </div>
</div>

</asp:Content>
