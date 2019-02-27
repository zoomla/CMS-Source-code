<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModelManage.aspx.cs" Inherits="Zoomla.WebSite.Manage.shop.ModelManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>商城模型管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="r_navigation">后台管理&gt;&gt;商城管理&gt;&gt; <a href="ModelManage.aspx">商品模型管理</a> 　<span class="red"><a href="Model.aspx">[添加商品模型]</a></span></div>
  <table width="100%" border="0" cellpadding="0" cellspacing="1" class="border" align="center">
    <tbody>
      <tr class="gridtitle" align="center" style="height:25px;">
        <td width="5%" height="20"><strong>ID</strong></td>
        <td width="5%"><strong>图标</strong></td>
        <td width="10%"><strong>模型名称</strong></td>
        <td width="25%"><strong>模型描述</strong></td>
        <td width="10%"><strong>项目名称</strong></td>
        <td width="15%"><strong>表名</strong></td>
        <td width="30%"><strong>操作</strong></td>
      </tr>
      <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
        <ItemTemplate>
          <tr class="tdbg" align="center" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'" style="height:25px;" id="<%# Eval("ModelID") %>" ondblclick="getinfo(id)">
            <td><strong><%# Eval("ModelID") %></strong></td>
            <td><img src="<%# GetIcon(DataBinder.Eval(Container, "DataItem.ItemIcon", "{0}"))%>" style="border-width:0px;" /></td>
            <td><strong><%# Eval("ModelName")%></strong></td>
            <td align="left"><strong><%# Eval("Description")%></strong></td>
            <td><strong><%# Eval("ItemName")%></strong></td>
            <td align="left"><strong><%# Eval("TableName")%></strong></td>
            <td><asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ModelID") %>'>修改</asp:LinkButton>
              |
              <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" CommandArgument='<%# Eval("ModelID") %>' OnClientClick="return confirm('确实要删除此模型吗？');">删除</asp:LinkButton>
              |
              <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Field" CommandArgument='<%# Eval("ModelID") %>'>字段列表</asp:LinkButton></td>
          </tr>
        </ItemTemplate>
      </asp:Repeater>
      <tr class="tdbg">
        <td height="22" colspan="10" align="center" class="tdbgleft"><span style="text-align: center"> 共
          <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
          条数据
          <asp:Label ID="Toppage" runat="server" Text="" />
          <asp:Label ID="Nextpage" runat="server" Text="" />
          <asp:Label ID="Downpage" runat="server" Text="" />
          <asp:Label ID="Endpage" runat="server" Text="" />
          页次：
          <asp:Label ID="Nowpage" runat="server" Text="" />
          /
          <asp:Label ID="PageSize" runat="server" Text="" />
          页
          <asp:TextBox ID="txtPage" runat="server" AutoPostBack="true" 
            class="l_input" Width="16px"
                    Height="16px" ontextchanged="txtPage_TextChanged"></asp:TextBox>
          条数据/页 转到第
          <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"></asp:DropDownList>
          页
          <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPage"
                    ErrorMessage="只能输入数字" Type="Integer" MaximumValue="100000" MinimumValue="0"></asp:RangeValidator>
          </span></td>
      </tr>
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