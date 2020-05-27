<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="BaikeContribution.aspx.cs" Inherits="User_Guest_BaikeContribution" ClientIDMode="Static"  %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>我的词条贡献</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="baike"></div>
<div class="container margin_t10">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">我的词条贡献</li>
        <div class="clearfix"></div>
    </ol>
</div>
<div class="container">
<div class="input-group" style="width:440px;">
    <span class="input-group-addon">状态</span>
    <asp:DropDownList runat="server" ID="Filter_DP" CssClass="form-control text_150" AutoPostBack="true" OnSelectedIndexChanged="Filter_DP_SelectedIndexChanged">
        <asp:ListItem Value="1">已通过</asp:ListItem>
        <asp:ListItem Value="0">待审核</asp:ListItem>
        <asp:ListItem Value="-2">未通过</asp:ListItem>
        <asp:ListItem Value="-100" Selected="True">全部</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox runat="server" ID="Skey_T" CssClass="form-control text_md" placeholder="关键词"></asp:TextBox>
    <span class="input-group-btn">
        <asp:Button runat="server" ID="Skey_Btn" Text="搜索" OnClick="Skey_Btn_Click"  CssClass="btn btn-info"/>
    </span>
</div>
    <table class="table table-striped table-bordered  margin_t5">
        <tr>
            <td class="td_l">词条信息</td>
            <td class="td_m">状态</td>
            <td class="td_l">处理记录</td>
            <td class="td_m">操作</td>
        </tr>
        <ZL:ExRepeater ID="RPT" runat="server" PageSize="10" PagePre="<tr><td colspan='4' class='text-center'>" PageEnd="</td></tr>" OnItemCommand="RPT_ItemCommand">
            <ItemTemplate>
                <tr>
                    <td>
                        <a href='/Guest/Baike/Details.aspx?EditID=<%#Eval("ID") %>' target="_blank"><%# Eval("Tittle")%></a>
                        <span>(<%#Convert.ToDateTime(Eval("AddTime")).ToString("yyyy-MM-dd")%>)</span>
                        <span class="btn btn-default btn-sm">(版本号:<%#Eval("VerStr") %>)</span>
                    </td>
                    <td><%#GetStatus() %></td>
                    <td class="td_l"><%#Eval("AdminRemind") %></td>
                    <td>
                        <asp:LinkButton runat="server" ID="del_btn" Visible='<%#Eval("Status","").Equals("0") %>' CommandArgument='<%#Eval("ID") %>' CommandName="del" OnClientClick="return confirm('确定要删除吗?');">删除</asp:LinkButton>
                        <%#Eval("Status","").Equals("1")?"":"<a href='/Baike/BKEditor.aspx?EditID="+Eval("ID")+"'>修改</a>" %>
                        <a href="/Baike/Details.aspx?EditID=<%#Eval("ID") %>">浏览</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
