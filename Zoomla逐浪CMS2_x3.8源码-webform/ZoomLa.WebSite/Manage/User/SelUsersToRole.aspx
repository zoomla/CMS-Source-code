<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="SelUsersToRole.aspx.cs" Inherits="Manage_User_SelUsersToRoleUsers" %>
<asp:Content runat="server" ContentPlaceHolderID="head"> 
    <title>批量设置用户</title> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="users_edit" runat="server"> 
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有会员!!" OnPageIndexChanging="EGV_PageIndexChanging" >
            <Columns> 
                <asp:TemplateField ItemStyle-CssClass="td_m" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" value="<%#Eval("UserID") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserID" HeaderText="用户ID" />
                <asp:BoundField DataField="UserName" HeaderText="用户名" /> 
                <asp:BoundField DataField="HoneyName" HeaderText="真实姓名" />  
                <asp:TemplateField HeaderText="邮箱">
                    <ItemTemplate>
                        <%#Eval("Email") %>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:BoundField DataField="RegTime" HeaderText="注册时间" /> 
            </Columns> 
        </ZL:ExGridView>                     
        <asp:HiddenField runat="server" ID="RoleUsers_Hid" />
        <asp:Button runat="server" ID="SaveBtn" CssClass="btn btn-primary" OnClick="SaveBtn_Click" style="display:none;" />
        <asp:LinkButton runat="server" ID="DelBtn" CssClass="btn btn-primary" OnClick="BtnDelAll_Click" OnClientClick="return confirm('确定删除吗?');">批量删除</asp:LinkButton>             
    </div> 
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script> 
        $("#selUsers").click(function (e) {
            ShowComDiag("/Common/Dialog/SelStructure.aspx?Type=AllInfo#RoleUsers", "选择用户");
        })
        function UserFunc(json,select) {
            Def_UserFunc(json, select);
            $("#SaveBtn").trigger("click");
        } 
    </script> 
</asp:Content> 