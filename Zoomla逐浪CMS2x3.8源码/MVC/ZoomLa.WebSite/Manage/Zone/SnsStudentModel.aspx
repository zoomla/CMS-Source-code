<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SnsStudentModel.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.SnsStudentModel" MasterPageFile="~/Manage/I/Default.master"  %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>学校会员模型管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<div class="clearbox"></div>
    <table class="table table-striped table-bordered table-hover">
        <tbody>
            <tr class="gridtitle" align="center" style="height:25px;">
                <td width="5%" height="20">
                    <strong>ID</strong></td>
                <td width="5%">
                    <strong>图标</strong></td>
                <td width="10%">
                    <strong>模型名称</strong></td>
                <td width="20%">
                    <strong>模型描述</strong></td>
                <td width="10%">
                    <strong>项目名称</strong></td>
                <td width="20%">
                    <strong>表名</strong></td>                        
                <td width="30%">
                    <strong>操作</strong></td>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                <ItemTemplate>
                    <tr class="tdbg" align="center" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'" style="height:25px;">                        
                        <td>
                            <strong><%# Eval("ModelID") %></strong></td>
                        <td>
                            <img src="<%# GetIcon(DataBinder.Eval(Container, "DataItem.ItemIcon", "{0}"))%>" style="border-width:0px;" /></td>
                        <td>
                            <strong><%# Eval("ModelName")%></strong></td>
                        <td align="left">
                            <strong><%# Eval("Description")%></strong></td>
                        <td>
                            <strong><%# Eval("ItemName")%></strong></td>
                        <td align="left">
                            <strong><%# Eval("TableName")%></strong></td>                        
                        <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ModelID") %>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton> 
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" CommandArgument='<%# Eval("ModelID") %>' OnClientClick="return confirm('确实要删除此模型吗？');" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton> 
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Field" CommandArgument='<%# Eval("ModelID") %>' CssClass="option_style"><i class="fa fa-list-alt" title="列表"></i>字段列表</asp:LinkButton>                            
                         </td>
                    </tr>
                </ItemTemplate>
             </asp:Repeater>                        
        </tbody>
    </table>
</asp:Content>
