<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Office.Default" MasterPageFile="~/User/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>公文处理</title>
<script type="text/javascript">
    function delCon() {
        return confirm('确定该公文移入回收站吗!!!');
    }
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="draftnav"><a href="/MIS/OA/Main.aspx">行政公文</a>/<a href="Default.aspx">发文管理</a><a href="javascript:;" style="margin-left:5px;">提示：已开始审批的文件无法修改删除</a></div>
    <div id="site_main" style="margin:5px;">
        <div style="margin-bottom:5px;">
            <ul class="nav nav-tabs">
                <li class="active"><a href="Default.aspx">公文</a></li>
                <li><a href="DocManage.aspx">文档</a></li>
                <li><a href="DraftBox.aspx">草稿箱</a></li>
                <li><a href="Garbagebox.aspx">回收站</a></li>
            </ul>
            <div class="clearfix"></div>
        </div>
        <div class="input-group" style=" width:400px;float:left;margin-bottom:5px;">
            <asp:TextBox runat="server" ID="searchText" placeholder="请输入需要查询的信息" CssClass="form-control"/>
            <span class="input-group-btn">
                <asp:Button runat="server" CssClass="btn btn-primary" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click"/>
            </span>
        </div>
        <div class="tab3">
            <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"  EnableTheming="False"  GridLines="None" CellPadding="2" CellSpacing="1"  Width="100%" CssClass="table table-striped table-bordered table-hover" EmptyDataText="你当前尚未发出任何公文!!" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound" >
                <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="发文类型">
                <ItemTemplate>
                    <%#GetType(Eval("DocType", "{0}")) %>
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
                    <asp:LinkButton ID="view" CommandArgument='<%#Eval("ID") %>' CommandName="View" runat="server">查看审批详情</asp:LinkButton>
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