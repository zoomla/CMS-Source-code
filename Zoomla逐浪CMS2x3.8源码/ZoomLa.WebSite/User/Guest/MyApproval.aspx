<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MyApproval.aspx.cs" Inherits="User_Guest_MyApproval" ClientIDMode="Static"  %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>我的赞同</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="ask"></div>
    <div class="container margin_t10">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">我的赞同</li> 
    </ol>
        </div>
    <div class="container u_cnt">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td>问题</td>
                <td>答案</td>
                <td>评论时间</td>
            </tr>
            <ZL:ExRepeater ID="Rep_comment" runat="server" PagePre="<tr><td colspan='3' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>" OnItemDataBound="Rep_comment_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td style="width: 40%; padding-left: 10px; line-height: 22px;">
                            <label runat="server" id="lbAsk"></label>
                        </td>
                        <td style="width: 40%; padding-left: 10px;">
                            <label runat="server" id="lbAsw"></label>
                        </td>
                        <td><%#Eval("AddTime") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
        </table>
    </div>
    <style>
        .table td{ text-align:center;}
    </style>
</asp:Content>
