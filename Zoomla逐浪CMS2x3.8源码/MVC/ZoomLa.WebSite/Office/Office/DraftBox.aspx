<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DraftBox.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Office.DraftBox" MasterPageFile="~/Common/Master/UserEmpty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>草稿箱</title>
<script>
    function delCon() {
        return confirm('确定要删除公文到回收站!!!');
    }
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="draftnav"><a href="/Office/Main.aspx">行政公文</a>/<a href="Default.aspx">公文管理</a></div>
    <div id="site_main" style="margin:10px;">
        <div style="margin-bottom:10px;">
            <ul class="nav nav-tabs">
                <li><a href="Default.aspx">公文</a></li>
                <li><a href="DocManage.aspx">文档</a></li>
                <li class="active"><a href="DraftBox.aspx">草稿箱</a></li>
                <li><a href="Garbagebox.aspx">回收站</a></li>
            </ul><div class="clearfix"></div>
        </div><div class="clearfix"></div>
        <div class="input-group" style=" width:400px;float:left;margin-bottom:10px;">
            <asp:TextBox runat="server" ID="searchText" placeholder="请输入需要查询的信息" CssClass="form-control"/>
            <span class="input-group-btn">
                <asp:Button runat="server" CssClass="btn btn-primary" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click" />
            </span>
        </div>
        <div class="tab3">
            <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"  EnableTheming="False"  GridLines="None" CellPadding="2" CellSpacing="1"  Width="100%" CssClass="table table-striped table-bordered table-hover" EmptyDataText="你当前尚未发出任何公文!!" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound" >
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="ID" />
                    <asp:TemplateField HeaderText="发文类型">
                        <ItemTemplate>
                            <%#GetType(Eval("Type", "{0}")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="拟稿部门" DataField="Branch" />
                    <asp:BoundField HeaderText="标题" DataField="Title" />
                    <asp:TemplateField HeaderText="密级">
                        <ItemTemplate>
                            <%#GetSecret( Eval("Secret","{0}")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="紧急程度">
                        <ItemTemplate>
                            <%# GetUrgency(Eval("Urgency","{0}")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="重要程度">
                        <ItemTemplate>
                            <%# GetImport(Eval("Importance","{0}")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                            <%# GetStatus(Eval("Status","{0}")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="创建时间" DataField="CreateTime" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="edit" runat="server" ToolTip="修改" CommandArgument='<%#Eval("ID") %>' CommandName="Edit1">修改</asp:LinkButton>
                            <asp:LinkButton ID="del" CommandArgument='<%#Eval("ID") %>' CommandName="del2" runat="server" ToolTip="删除" OnClientClick="return delCon();">删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center"/>
                <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
        </div>
    </div>
</asp:Content>

