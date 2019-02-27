<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelUserRole.aspx.cs" Inherits="ZoomLaCMS.Common.Dialog.SelUserRole" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>选择用户角色</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
      <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"   EmptyDataText="无数据"
        EnableTheming="False" RowStyle-CssClass="tdbg"  GridLines="None" CellPadding="2" CellSpacing="1"  Width="98%" class="table table-striped table-bordered table-hover" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" AllowUserToOrder="true" DataKeyNames="ID">
        <Columns>
            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="td_s">
               <ItemTemplate>
                   <input type="checkbox" name="idchk" data-name="<%#Eval("RoleName") %>" value="<%#Eval("ID") %>" />
               </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="角色名称" >
                <ItemTemplate>
                    <%#Eval("RoleName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="角色说明" DataField="info" />
            <asp:BoundField HeaderText="优先级别" DataField="Precedence" />
        </Columns>
        <PagerStyle HorizontalAlign="Center"/>
        <RowStyle Height="24px" HorizontalAlign="Center" />
    </ZL:ExGridView>
    <asp:HiddenField runat="server" ID="RoleIDS_Hid" Value="[]" />
    <input type="button" value="确定" onclick="SureFunc();" class="btn btn-primary"/>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css">
        #AllID_Chk {display:none;}
    </style>
    <script src="/JS/Controls/ZL_Array.js"></script>
    <script src="/JS/ICMS/ZL_Common.js"></script>
   <script>
       $(function () {
           $("[name=idchk]").click(function () {
               var id = $(this).val();
               var name = $(this).data("name");
               var model = { "id": id, "name": name };
               var list = JSON.parse($("#RoleIDS_Hid").val());
               //list=[];
               if (this.checked) {
                   list.push(model);
               }
               else {
                   list.RemoveByID(model.id);
               }
               $("#RoleIDS_Hid").val(JSON.stringify(list));
               console.log( $("#RoleIDS_Hid").val());
           });
           //$("#AllID_Chk").unbind("click");
       })
       function SureFunc() {
           var select = getParam2();
           var list = JSON.parse($("#RoleIDS_Hid").val());
           parent.Def_RoleFunc(list, select);
       }
   </script>
</asp:Content>
