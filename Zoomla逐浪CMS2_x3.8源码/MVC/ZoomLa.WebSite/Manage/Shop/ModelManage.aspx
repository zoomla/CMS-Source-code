<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.ModelManage" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>商城模型管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="r_navigation">后台管理&gt;&gt;商城管理&gt;&gt; <a href="ModelManage.aspx">商品模型管理</a> 　<span class="red"><a href="Model.aspx">[添加商品模型]</a></span></div>
  <table style="width:100%" border="0" cellpadding="0" cellspacing="1" class="border" align="center">
    <tbody>
      <tr class="gridtitle text-center">
        <td style="width:5%"><strong>ID</strong></td>
        <td style="width:5%"><strong>图标</strong></td>
        <td style="width:10%"><strong>模型名称</strong></td>
        <td style="width:25%"><strong>模型描述</strong></td>
        <td style="width:10%"><strong>项目名称</strong></td>
        <td style="width:15%"><strong>表名</strong></td>
        <td style="width:30%"><strong>操作</strong></td>
      </tr>
      <ZL:ExRepeater ID="Model_RPT" runat="server" OnItemCommand="Model_RPT_ItemCommand" PagePre="<tr><td colspan='7' class='text-center'>" PageEnd="</td></tr>">
        <ItemTemplate>
          <tr class="tdbg text-center" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'" style="height:25px;" id="<%# Eval("ModelID") %>" ondblclick="getinfo(id)">
            <td><strong><%# Eval("ModelID") %></strong></td>
            <td><img src="<%# GetIcon(DataBinder.Eval(Container, "DataItem.ItemIcon", "{0}"))%>" style="border-width:0px;" /></td>
            <td><strong><%# Eval("ModelName")%></strong></td>
            <td class="text-left"><strong><%# Eval("Description")%></strong></td>
            <td><strong><%# Eval("ItemName")%></strong></td>
            <td class="text-left"><strong><%# Eval("TableName")%></strong></td>
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ModelID") %>'>修改</asp:LinkButton> | 
                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" CommandArgument='<%# Eval("ModelID") %>' OnClientClick="return confirm('确实要删除此模型吗？');">删除</asp:LinkButton> | 
                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Field" CommandArgument='<%# Eval("ModelID") %>'>字段列表</asp:LinkButton></td>
          </tr>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
      </ZL:ExRepeater>
    </tbody>
  </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function getinfo(id) {
            location.href = "Model.aspx?ModelID=" + id + "";
        }
    </script>
</asp:Content>