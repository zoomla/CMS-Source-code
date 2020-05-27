<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Manage.WorkFlow.Default" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>流程管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <ol id="BreadNav" class="breadcrumb marginbot10" style="line-height:30px;">
                <li><a href="/Admin/I/Main.aspx">工作台</a></li>
                <li><a href="<%=Request.RawUrl %>">流程管理</a></li>
                <li class="active">流程列表<a href="WorkFlow.aspx">[添加流程]</a>
                    <div class="input-group pull-right" style="width: 300px; margin-left: 10px;">
                        <asp:TextBox runat="server" ID="searchText" Width="250" placeholder="请输入需要查询的信息" CssClass="form-control" />
                        <span class="input-group-btn">
                            <asp:Button runat="server" CssClass="btn btn-primary" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click" />
                        </span>
        </div></li></ol>
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" EnableTheming="False" GridLines="None" CellPadding="2" CellSpacing="1" Width="100%" CssClass="table table-striped table-bordered table-hover" BackColor="White" EmptyDataText="当前没有信息!!" OnRowDataBound="EGV_RowDataBound" DataKeyNames="ID" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" HeaderStyle-CssClass="td_s" />
            <asp:BoundField HeaderText="流程名" DataField="ProcedureName" />
            <asp:TemplateField HeaderText="分类">
                <ItemTemplate>
                    <%#GetClassID(Eval("ClassID","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="允许附件">
                <ItemTemplate>
                    <%#GetAllowAttach(Eval("AllowAttach","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate><span class="rd_green">启用</span></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="备注" DataField="Remind" ItemStyle-CssClass="td_l" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="WorkFlow.aspx?proID=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="编辑"></i></a>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('你确定要删除吗!');" ToolTip="删除" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <a href="AddFlow.aspx?proID=<%#Eval("ID") %>" class="option_style"><i class="fa fa-eye" title="查看"></i>查看步骤</a>
                    <a href="javascript:;" onclick="ShowImgDiag(<%#Eval("ID") %>)" class="option_style"><i class="fa fa-eye" title="查看"></i>查看流程图</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .allchk_l{display:none;}
    </style>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript">
        $().ready(function () {
            $(":button").addClass("btn btn-primary");
            $(":submit").addClass("btn btn-primary");
            $("#EGV tr:last >td>:text").css("line-height", "normal");
            $("#EGV tr:first >th").css("text-align", "center");
        });
        var diag = new ZL_Dialog();
        function ShowImgDiag(proid) {
            diag.title = "查看流程图";
            diag.url = "/Office/flow/ImgWorkFlow.aspx?proid=" + proid;
            diag.maxbtn = false;
            diag.ShowModal();
        }
    </script>
</asp:Content>
