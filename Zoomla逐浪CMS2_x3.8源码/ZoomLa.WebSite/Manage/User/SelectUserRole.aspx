<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectUserRole.aspx.cs" Inherits="Manage_User_SelectUserRole"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>选择权限</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
<div>
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False" 
        RowStyle-CssClass="tdbg"  GridLines="None" CellPadding="2" CellSpacing="1"  Width="98%" class="table table-striped table-bordered table-hover" 
        OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" AllowUserToOrder="true" DataKeyNames="UserID">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" id="Btchk" name="idchk" runat="server" value='<%#Eval("UserID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="UserID" />
            <asp:BoundField HeaderText="用户名" DataField="UserName" />
            <asp:TemplateField>
                <ItemTemplate>
                    <%# GetGroupName(Eval("GroupID","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RegTime" HeaderText="注册时间" SortExpression="RegTime" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate> <%# GetStatus(Eval("Status","{0}")) %> </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center"/>
        <RowStyle Height="24px" HorizontalAlign="Center" />
    </ZL:ExGridView>
    <asp:Button ID="Button1" OnClick="SaveBtn_Click" class="btn btn-primary" runat="server" Text="选择会员" />
    <input type="button" class="btn btn-primary" onclick=" window.location.href = 'PermissionInfo.aspx'" value="返回列表" />
</div>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="../../JS/SelectCheckBox.js"></script>
    <script>

        $().ready(function () {
            var str = '<%= GetUserRole()%>';
    var str1 = str.split(',');
    for (var i = 0; i < str1.length; i++) {
        var checkArr = $("input[type=checkbox][value=" + str1[i] + "]");
        checkArr[0].checked = true;
    }
})
</script>
</asp:Content>
