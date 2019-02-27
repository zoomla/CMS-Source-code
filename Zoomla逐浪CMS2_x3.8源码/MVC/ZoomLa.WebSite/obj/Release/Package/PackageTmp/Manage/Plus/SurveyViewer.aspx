<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyViewer.aspx.cs" Inherits="ZoomLaCMS.Manage.Plus.SurveyViewer" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>问卷来源分析</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" DataKeyNames="Rid" CellPadding="2" CellSpacing="1" CssClass="table table-striped table-bordered table-hover" GridLines="None" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" Width="100%" OnRowDataBound="gviewSurSorcer_RowDataBound" OnPageIndexChanging="gviewSurSorcer_PageIndexChanging" OnRowCommand="gviewSurSorcer_RowCommand">
        <EmptyDataTemplate>无相关数据</EmptyDataTemplate>
        <EmptyDataRowStyle BackColor="#e8f4ff" Height="45px" BorderColor="#4197e2" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="chkSel" title="" value='<%#Eval("Rid") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编号">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="5%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="问卷ID">
                <ItemTemplate>
                    <%=Request.QueryString["SID"] %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Submitip" HeaderText="提交IP" />
            <asp:TemplateField HeaderText="提交者">
                <ItemTemplate>
                    <asp:HyperLink ID="hlnkUser" Target="_self" NavigateUrl='<%# customPath2+"Plus/UserVote.aspx?SID="+Eval("Sid")+"&UID="+Eval("Userid")+"&PTime="+Server.UrlEncode(Eval("Submitdate","{0}")) %>' ToolTip="查看我的答卷" runat="server"><%# GetUserName(Eval("Userid", "{0}"))%></asp:HyperLink>
                    <a href='<%# customPath2+"UserVote.aspx?SID="+Eval("Sid")+"&UID="+Eval("Userid")+"&PTime="+Server.UrlEncode(Eval("Submitdate","{0}")) %>'></a>
                    <itemstyle horizontalalign="Center" width="17%" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Submitdate" HeaderText="提交日期" DataFormatString="{0:yyyy-MM-dd  HH:mm}" HeaderStyle-Width="16%" />
            <asp:TemplateField HeaderText="总分">
                <ItemTemplate>
                    <%#GetScoreAll(Eval("UserID","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <%--        <asp:TemplateField HeaderText="浏览器类型">
            <ItemTemplate>
                <%# Request.Browser.Type %> &nbsp; 版本: <%# Request.Browser.Version %>
                <ItemStyle HorizontalAlign="Center" Width="15%" />
            </ItemTemplate>
        </asp:TemplateField>--%>
            <%--        <asp:BoundField DataField="Submitip" HeaderText="来源IP" HeaderStyle-Width="14%" />
        <asp:TemplateField HeaderText="所在地">
            <ItemTemplate><%# GetCitybyIP(Eval("Submitip", "{0}")) %></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle Width="20%" />
        </asp:TemplateField>--%>

            <%--        <asp:TemplateField HeaderText="状态">
            <ItemTemplate>
                <%#  GetStatus(Eval("Status","{0}")) %>
                <ItemStyle HorizontalAlign="Center" Width="15%" />
            </ItemTemplate>
        </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="AnswerView.aspx?sid=<%=Request.QueryString["SID"] %>&UID=<%# Eval("UserID") %>">查看</a> |
                <asp:LinkButton ID="lbtStatus" runat="server" ToolTip="审核" CommandName="Del1" CommandArgument='<%# Eval("Rid") %>'>删除</asp:LinkButton>
                    <%--<asp:LinkButton ID="lbtnExport" runat="server" ToolTip="导出为Word文档" CommandName="Export" CommandArgument='<%# Eval("Rid") %>'></asp:LinkButton> --%>

                    <%--                <asp:LinkButton ID="lbtConcle" runat="server" ToolTip="取消审核" CommandName="Concle" CommandArgument='<%# Eval("Rid") %>'>取消审核</asp:LinkButton> |
                <asp:LinkButton ID="lbtSelected" runat="server" ToolTip="通过" CommandName="Selected" CommandArgument='<%# Eval("Rid") %>'>通过</asp:LinkButton> |  
                <asp:LinkButton ID="lbtNLock" runat="server" ToolTip="解除锁定" CommandName="NLock" CommandArgument='<%# Eval("Rid") %>'>解除锁定</asp:LinkButton>     --%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <PagerSettings Visible="false" />
    </ZL:ExGridView>
    <div id="divPager" runat="server">
    </div>
    <asp:Button runat="server" ID="DelAll" Text="批量删除" CssClass="btn btn-primary" OnClick="DelAll_Click" />
    <div id="sendMsg" runat="server" visible="false" style="position: absolute; top: 20px; left: 20px; background: #fff; padding: 5px;">
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        $().ready(function () {
            $("#EGV tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "chkSel");
            });
        });
        function hid(obj) {
            document.getElementById(obj).innerHTML = '';
            document.getElementById(obj).style.display = 'none';
        }
    </script>
    <style>
        th,td{ text-align:center;}
    </style>
</asp:Content>
