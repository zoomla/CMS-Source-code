<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FiledList.aspx.cs" Inherits="MIS_OA_Doc_FiledList" MasterPageFile="~/MIS/OA.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>已归档公文</title>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/ICMS/ZL_Common.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href="/MIS/OA/Main.aspx">办公管理</a></li>
        <li><a href="/MIS/OA/Main.aspx">流程管理</a></li>
        <li class="active">归档公文</li>
    </ol>
    <div style="height: 40px;"></div>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10"
        OnPageIndexChanging="EGV_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有文档">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="档号" DataField="No" />
    <%--        <asp:BoundField HeaderText="保管期限" DataField="bgqx" />--%>
            <asp:BoundField HeaderText="发起人" DataField="UserName" />
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate><!--点击进入审核界面,用于浏览-->
                    <a href="/Mis/OA/FreeFlow/FlowView.aspx?AppID=<%#Eval("ID") %>&Action=filed"><%#Eval("Title") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="日期">
                <ItemTemplate><%#Eval("SendDate","{0:yyyy年MM月dd日}") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate><a href="/Mis/OA/FreeFlow/FlowView.aspx?AppID=<%#Eval("ID") %>&Action=filed">查看</a></ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <input type="button" value="文档借阅" onclick="ShowBorrow();" class="btn btn-primary" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script src="/JS/SelectCheckBox.js"></script>
    <script>
        function ShowBorrow() {
            var ids = GetValByName("idchk");
            if (ids == "") { alert("请先选择需要代阅的文件!"); return; }
            location = "AddBorrow.aspx?ids=" + ids;
            //ShowComDiag("BorrowEdit.aspx?ids=" + ids,"文档借阅");
        }
    </script>
</asp:Content>