<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemUserModel.aspx.cs" Inherits="manage_User_SystemUserModel" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title><%=Resources.L.会员模型字段列表 %></title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
<table id="FieldList" class="table table-striped table-bordered table-hover">
    <tr class="gridtitle" style="height: 25px;text-align:center;">
      <td style="width: 10%; height: 20px;"><strong><%=Resources.L.字段名 %></strong></td>
      <td style="width: 10%;"><strong><%=Resources.L.字段别名 %></strong></td>
      <td style="width: 10%;"><strong><%=Resources.L.字段类型 %></strong></td>
      <td style="width: 10%;"><strong><%=Resources.L.字段级别 %></strong></td>
      <td style="width: 10%;"><strong><%=Resources.L.是否必填 %></strong></td>
      <td style="width: 20%;"><strong><%=Resources.L.排序 %></strong></td>
      <td style="width: 30%;"><strong><%=Resources.L.操作 %></strong></td>
    </tr>
    <asp:Repeater ID="RepSystemModel" runat="server">
      <ItemTemplate>
        <tr style="text-align:center;">
          <td><%#Eval("FieldName")%></td>
          <td align="center"><%#Eval("FieldAlias")%></td>
          <td align="center"><%# Eval("FieldType", "{0}")%></td>
          <td align="center"><span style="color: #339900"><%=Resources.L.系统 %></span></td>
          <td align="center"><%# GetStyleTrue(Eval("IsNotNull", "{0}"))%></td>
          <td></td>
          <td align="center"></td>
        </tr>
      </ItemTemplate>
    </asp:Repeater>
    <asp:Repeater ID="RepModelField" runat="server" OnItemCommand="Repeater1_ItemCommand">
      <ItemTemplate>
          <tr style="text-align: center;">
              <td><%#Eval("FieldName")%></td>
              <td align="center"><%#Eval("FieldAlias")%></td>
              <td align="center"><%# GetFieldType(Eval("FieldType", "{0}").ToString ())%></td>
              <td align="center"><%=Resources.L.自定义 %> </td>
              <td align="center"><%# GetStyleTrue(Eval("IsNotNull", "{0}"))%></td>
              <td align="center">
                  <asp:LinkButton ID="LinkButton2" runat="server" CommandName="UpMove" CommandArgument='<%# Eval("FieldID") %>' CssClass="option_style"><i class="fa fa-arrow-up" title="<%=Resources.L.上移 %>"></i><%=Resources.L.上移 %></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton3" runat="server" CommandName="DownMove" CommandArgument='<%# Eval("FieldID") %>' CssClass="option_style"><%=Resources.L.下移 %><i class="fa fa-arrow-down" title="<%=Resources.L.下移 %>"></i></asp:LinkButton></td>
              <td align="center">
                  <a href="../Content/AddModelField.aspx?ModelID=-1&ModelType=9&FieldID=<%# Eval("FieldID") %>" class="option_style"><i class="fa fa-pencil" title="<%=Resources.L.修改 %>"></i></a>
                  <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" CommandArgument='<%# Eval("FieldID") %>' OnClientClick="return confirm('确定删除此字段吗?')" CssClass="option_style"><i class="fa fa-trash-o" title="<%=Resources.L.删除 %>"></i><%=Resources.L.删除 %></asp:LinkButton></td>
          </tr>
      </ItemTemplate>
    </asp:Repeater>
  </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/Common.js"></script>
<script>
    window.onload = function () {
        var FiledListTable = document.getElementById("FieldList");
        for (var i = 0; i < FiledListTable.rows.length; i++) {
            var FiledLevel = FiledListTable.rows[i].cells[3];
            if (FiledLevel.innerText.Trim() == "<%=Resources.L.系统 %>") {
                FiledListTable.rows[i].style.display = "none";
            }
            else {
                FiledListTable.rows[i].style.display = "";
            }
        }
    }
    function ShowList() {
        var FiledListTable = document.getElementById("FieldList");
        for (var i = 0; i < FiledListTable.rows.length; i++) {
            var FiledLevel = FiledListTable.rows[i].cells[3];
            if (FiledLevel.innerHTML.indexOf("<%=Resources.L.系统 %>") != -1 && FiledListTable.rows[i].style.display == "none") {
                FiledListTable.rows[i].style.display = "";
                document.getElementById("ShowLink").innerText = "[<%=Resources.L.隐藏系统字段 %>]";
            }
            else if (FiledLevel.innerHTML.indexOf("<%=Resources.L.系统 %>") != -1 && FiledListTable.rows[i].style.display == "") {
                FiledListTable.rows[i].style.display = "none";
                document.getElementById("ShowLink").innerText = "[<%=Resources.L.显示所有字段 %>]";
            }
        }
    }
</script>
</asp:Content>
