
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DnsManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Site.DnsManage"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>DNS管理</title>
    <script type="text/javascript">
        $().ready(function () {
            $(":button").addClass("btn btn-primary");
            $(":submit").addClass("btn btn-primary");
            $("#EGV tr:last >td>:text").css("line-height", "normal");
            $("#EGV tr:first >th").css("text-align", "center");
        });
        function disAlert() {
            if (confirm('确定要将当前数据生成TXT文档吗?'))
                return true;
            else
                return false;
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content"> 
    <div id="tab3" runat="server">
            <div class="input-group" style="width: 400px; float: left; margin-bottom: 10px;">
                <asp:TextBox runat="server" ID="searchText" placeholder="请输入要查询的IP或域名信息" CssClass="form-control" />
                <span class="input-group-btn">
                    <asp:Button runat="server" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click" />
                    <input type="button" id="addBtn" value="添加" onclick="location.href = 'AddDns.aspx';" />
                    <asp:Button runat="server" ID="outputBtn" Text="输出" OnClick="outputBtn_Click" OnClientClick="return disAlert();" />
                </span>
            </div>
            <div runat="server" id="remindDiv" visible="false" class="alert alert-success" style="margin-left: 25%; text-align: left; height: 34px; line-height: 34px; padding: 0px;"></div>
            <ZL:ExGridView runat="server" ID="EGV" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" RowStyle-CssClass="tdbg"
                EnableTheming="False" GridLines="None" CellPadding="2" CellSpacing="1"  
                EmptyDataText="当前没有DNS信息!!" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="ID" />
                    <asp:BoundField HeaderText="域名" DataField="Domain" />
                    <asp:BoundField HeaderText="IP" DataField="IP" />
                    <asp:BoundField HeaderText="Mail" DataField="MX" />
                    <asp:BoundField HeaderText="用户ID" DataField="User_ID" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="AddDns.aspx?ID=<%#Eval("ID") %>" title="查看详情" class="option_style">
                                <i class="fa fa-eye"></i></a>
                            <asp:LinkButton runat="server" CommandName="Del2" CommandArgument='<%#Eval("ID") %>'
                                OnClientClick="return confirm('你确定要删除吗!');" ToolTip="删除"  class="option_style"><i class="fa fa-trash"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" />
                <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
        </div>
</asp:Content>
