<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserModelManage.aspx.cs" Inherits="Zoomla.WebSite.Manage.Page.UserModelManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
      <title>申请设置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table  class="table table-striped table-bordered table-hover">
    <tbody>
        <tr class="gridtitle" align="center" style="height:25px; text-align:center;">
            <td width="5%" height="24">
                <strong>ID</strong></td>                
            <td width="25%">
                <strong>模型名称</strong></td>
            <td width="25%">
                <strong>表名</strong></td>  
            <td width="25%">
                <strong>模型描述</strong></td>                      
            <td width="20%">
                <strong>操作</strong></td>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
            <ItemTemplate>
                <tr class="tdbg" align="center"  id="<%# Eval("ModelID") %>" ondblclick="getinfo(this.id)">                        
                    <td align="center" height="24">
                        <%# Eval("ModelID") %></td>                        
                    <td align="center">
                        <%# Eval("ModelName")%></td>                                             
                    <td align="center">
                        <%# Eval("TableName")%></td> 
                    <td align="center">
                        <%# Eval("Description")%></td>                         
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" class="option_style" CommandName="Edit" CommandArgument='<%# Eval("ModelID") %>'><span class="fa fa-pencil" title="修改"></span></asp:LinkButton>                           
                        <%#showdelbotton(DataBinder.Eval(Container,"DataItem.ModelID","{0}"))%>
                        <asp:LinkButton ID="LinkButton3" class="option_style" runat="server" CommandName="Field" ToolTip="字段列表" CommandArgument='<%# Eval("ModelID") %>'><i class="fa fa-th-list" title="字段列表"></i>字段列表</asp:LinkButton> 
                        </td>
                </tr>
            </ItemTemplate>
         </asp:Repeater>                        
    </tbody>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function getinfo(id) {
            location.href = "UserModel.aspx?ModelID=" + id + "";
        }
</script>
</asp:Content>