<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="StructMenber.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.StructMenber" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>部门成员列表</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <asp:HiddenField runat="server" ID="HiddenUser" />
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href="/Admin/I/Main.aspx">工作台</a></li>
        <li><a href="StructList.aspx">组织结构</a></li>
        <li class="active"><asp:Label runat="server" ID="curStr_L" />
            <a href='javascript:showdiv("div_share",1);'>[选择成员]</a>
            <a href="AddStruct.aspx?pid=<%=Request.QueryString["ID"] %>">[添加组织结构]</a>
        </li>
    </ol>
    <div style="height:45px;"></div>
    <asp:Button runat="server" ID="Sure_Btn" OnClick="Sure_Btn_Click" style="display:none;"/>
    <ZL:ExGridView ID="EGV" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" DataKeyNames="UserID" IsHoldState="false"
        OnPageIndexChanging="Egv_PageIndexChanging" EmptyDataText="无相关信息！！" OnRowCommand="Lnk_Click">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("UserID") %>"/>
                </ItemTemplate>
                <ItemStyle Width="50" />
            </asp:TemplateField>
            <asp:BoundField DataField="UserID" HeaderText="ID">
                <ItemStyle Width="50"/>
            </asp:BoundField>
            <asp:TemplateField HeaderText="用户名" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="../User/UserInfo.aspx?id=<%#Eval("UserID") %>"><%#Eval("UserName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="邮箱" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                   <%#Eval("Email") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%#GetStruct(Eval("StructureID").ToString()) %> </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="真实名称" DataField="TrueName" />
            <asp:BoundField HeaderText="工号" DataField="WorkNum" />
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%#GetIsManager() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="Remove" OnClientClick="if(!this.disabled) return confirm('确实要从本部门移除吗？');" CommandArgument='<%# Eval("UserID")%>'>移除</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button runat="server" ID="Button2" class="btn btn-primary" Text="批量移除" OnClick="Button2_Click" OnClientClick="return confirm('确定要删除吗?');" />
    <asp:Button runat="server" ID="SetM_Btn" CssClass="btn btn-primary" Text="设为管理员" OnClick="SetM_Btn_Click" />
    <asp:Button runat="server" ID="UnSetM_Btn" class="btn btn-primary" Text="取消管理员" OnClick="UnSetM_Btn_Click" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript">
        var diag = new ZL_Dialog();
        function showdiv(div_id, n) {
            diag.title = "选择成员";
            diag.url = "/Office/Mail/SelGroup.aspx?Type=AllInfo&Fid=<%:UserIDS %>";
            diag.maxbtn = false;
            diag.ShowModal();
        }
        function UserFunc(json, select) {
            if (json.length < 1) { return; }
            var uname = "";
            var uid = "";
            for (var i = 0; i < json.length; i++) {
                uname += json[i].UserName + ",";
                uid += json[i].UserID + ",";
            }
            if (uid) uid = uid.substring(0, uid.length - 1);
            $("#HiddenUser").val(uid);
            $("#Sure_Btn").trigger("click");
        }
    </script>
</asp:Content>