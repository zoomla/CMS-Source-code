<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="SelectiServer.aspx.cs" Inherits="manage_iServer_SelectiServer" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>有问必答</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="cnt"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="FiServer.aspx">有问必答</a></li>
        <li><a href="SelectiServer.aspx">问题列表</a></li>
        <li class="active"><%= retuenMapNav() %></li> 
    </ol>
</div>
<div class="container btn_green">
    <table class="table table-striped table-bordered table-hover">
        <tr class="tdbg">
            <td>查找：<asp:TextBox ID="txtTitle" runat="server"  placeholder="请输入标题" class="form-control text_md"></asp:TextBox>
                <asp:Button ID="btnSeach" runat="server" CssClass="btn btn-primary" UseSubmitBehavior="false" Text="搜索" OnClick="btnSeach_Click" />
                <div id="typeList" class="btn-group" style="margin-left:30px;">
                    <asp:Repeater ID="repSeachBtn" runat="server">
                        <HeaderTemplate>
                            <a href="SelectiServer.aspx" target="_self">All</a>&nbsp;&nbsp;
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a name="type" href='SelectiServer.aspx?type=<%# returnType(Eval("type")) %>' target="_self"><%# Eval("type") %></a>&nbsp;&nbsp;
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="divbox" id="nocontent" runat="server" style="display: none">
                    暂无问题
                </div>
                <asp:Repeater ID="resultsRepeater_w" runat="server">
                    <HeaderTemplate>
                        <br />
                        <table class="table table-striped table-bordered table-hover">
                            <tr align="center" class="title" style="height: 25px">
                                <td>编号 </td>
                                <td>标题</td>
                                <td>用户名</td>
                                <td>用户类型</td>
                                <td>优先级</td>
                                <td>问题类型</td>
                                <td>已读次数</td>
                                <td>提交时间</td>
                                <td>状态</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr align="center">
                            <td>
                                <%# Eval("QuestionId")%>
                            </td>
                            <td>
                                <a href="FiServerInfo.aspx?QuestionId=<%#Eval("QuestionId")%>"><%# ZoomLa.Common.BaseClass.CheckInjection(Eval("Title", "{0}"))%></a>
                            </td>
                            <td><%# GetUserName(Eval("UserId","{0}"))%></td>
                            <td><%# GetGroupName()%></td>
                            <td><%# Eval("Priority")%></td>
                            <td><%# Eval("Type")%></td>
                            <td><%# Eval("ReadCount")%></td>
                            <td><%# Eval("SubTime")%></td>
                            <td>
                                <asp:Label ID="lblState" runat="server" ForeColor="Red" Text='<%# Eval("State")%>'></asp:Label></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td class="text-center">
                <span>共<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>条数据
                    <asp:Label ID="Toppage" runat="server" Text="" />
                    <asp:Label ID="Nextpage" runat="server" Text="" />
                    <asp:Label ID="Downpage" runat="server" Text="" />
                    <asp:Label ID="Endpage" runat="server" Text="" />页次：
                    <asp:Label ID="Nowpage" runat="server" Text="" />/
                    <asp:Label ID="PageSize" runat="server" Text="" />页
                    <asp:TextBox ID="txtPage" runat="server" AutoPostBack="true" class="l_input" Width="22px"
                    Height="22px" OnTextChanged="txtPage_TextChanged"></asp:TextBox>
                    条数据/页 转到第
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    </asp:DropDownList>
                    页<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPage"
                        ErrorMessage="只能输入数字" Type="Integer" MaximumValue="100000" MinimumValue="0"></asp:RangeValidator>
                </span>
            </td>
        </tr>
        </table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        $(function () {
            var num = $("#typeList").find("a[name='type']").length;
            if (parseInt(num) == 0)
                $("#typeList").hide();
        });
    </script>
</asp:Content>
