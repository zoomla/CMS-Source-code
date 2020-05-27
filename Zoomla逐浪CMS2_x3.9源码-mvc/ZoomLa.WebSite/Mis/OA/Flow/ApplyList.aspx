<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyList.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Flow.ApplyList" MasterPageFile="~/User/Empty.master" %>  
<asp:Content runat="server" ContentPlaceHolderID="head"><title>申请列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href="/MIS/OA/Main.aspx">办公管理</a></li>
        <li><a href="/MIS/OA/Main.aspx">流程管理</a></li>
        <li class="active"><asp:Label runat="server" ID="CurView_L"></asp:Label>(提示：支持自定义分类，分类格式为：/Mis/OA/Flow/ApplyList.aspx?view=3&proid=[流程ID])</li>
    </ol>
    <div style="height:40px;"></div>
    <ul id="proul" class="nav nav-tabs">
        <li id="proli_0" class="active"><a href="ApplyList.aspx?View=<%:CurrentView %>">查看全部</a></li>
        <asp:Repeater runat="server" ID="ProRPT" EnableViewState="false">
            <ItemTemplate>
                <li id="proli_<%#Eval("ID") %>"><a href="ApplyList.aspx?View=<%#CurrentView+"&ProID="+Eval("ID") %>"><%#Eval("ProcedureName") %></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"
         EnableTheming="False" GridLines="None" CssClass="table table-bordered table-striped table-hover"
         EmptyDataText="当前没有信息!!" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:BoundField HeaderText="流程" DataField="Title" ItemStyle-CssClass="td_50p"/>
            <asp:BoundField HeaderText="拟稿部门" DataField="Branch" />
            <asp:BoundField HeaderText="申请人" DataField="UserID" />
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate><%# GetStatus(Eval("Status","{0}")) %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="申请时间" DataField="CreateTime" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <%#GetLinks() %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button runat="server" ID="Del_Btn" OnClick="Del_Btn_Click" style="display:none;" />
    <asp:Button runat="server" ID="WithDraw_Btn" OnClick="WithDraw_Btn_Click" style="display:none;" />
    <asp:HiddenField runat="server" ID="DelID_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css">
        #proul li {cursor:pointer;}
        .allchk_l {display:none;}
</style>
    <script>
        function SetDel(id) {
            if (!confirm("确定要删除吗?")) { return false; }
            $("#DelID_Hid").val(id);
            $("#Del_Btn").click();
            return true;
        }
        function SetWithDraw(id) {
            if (!confirm("确定要撤回吗?")) { return false; }
            $("#DelID_Hid").val(id);
            $("#WithDraw_Btn").click();
        }
        $(function () {
            var proid = "<%:ProID%>";
            if (proid != "0") {
                var $lis = $("#proul li");
                $lis.removeClass("active");
                $("#proli_" + proid).addClass("active");
            }
        })
    </script>
</asp:Content>
