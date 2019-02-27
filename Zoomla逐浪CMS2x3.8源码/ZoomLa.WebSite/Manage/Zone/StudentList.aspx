<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentList.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="Manage_Zone_StudentList" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>班级成员</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs" role="tablist">
        <li role="presentation" class="active" data-tabid="-1"><a href="StudentList.aspx?cid=<%=ClassID %>&status=-1&stutype=<%=StuType %>">全部</a></li>
        <li role="presentation" data-tabid="1"><a href="StudentList.aspx?cid=<%=ClassID %>&status=1&stutype=<%=StuType %>">已审核</a></li>
        <li role="presentation" data-tabid="0"><a href="StudentList.aspx?cid=<%=ClassID %>&status=0&stutype=<%=StuType %>" >未审核</a></li>
      </ul>
    <ZL:ExGridView ID="EGV" runat="server" EmptyDataText="暂无数据" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanging="EGV_PageIndexChanging" 
        CssClass="table table-striped table-bordered table-hover margin_t5" PageSize="10" >
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("Noteid") %>" />
                </ItemTemplate>
                <ItemStyle CssClass="td_s" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="成员名称">
                <ItemTemplate>
                    <%#Eval("UserName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="成员角色">
                <ItemTemplate>
                    <%#GetStuType() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="AddTime" HeaderText="申请时间" />
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%#GetStatus() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请备注">
                <ItemTemplate>
                    <%#Eval("AuditingContext") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </ZL:ExGridView>
    <div id="Option_Div">
            <asp:Button ID="Auit_Btn" runat="server" CssClass="btn btn-primary" Text="批量审核" OnClick="Auit_Btn_Click" />
            <asp:Button ID="UnAuit_Btn" runat="server" CssClass="btn btn-primary" Text="批量拒绝" OnClick="UnAuit_Btn_Click" />
            <asp:Button ID="Del_Btn" runat="server" CssClass="btn btn-primary" OnClientClick="return confirm('是否删除!')" Text="批量删除" OnClick="Del_Btn_Click" />
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function InitTab(id) {
            $("[data-tabid]").removeClass("active");
            $("[data-tabid='" + id + "']").addClass("active");
        }
    </script>
</asp:Content>



