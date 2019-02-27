<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductsSelect.aspx.cs" Inherits="manage_Shop_ProductsSelect" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>选择商品</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="top_opbar">
        <asp:TextBox ID="TxtKeyWord" class="form-control text_300" style="display:inline-block;" runat="server"></asp:TextBox>
        <asp:Button class="btn btn-primary" ID="BtnSearch" runat="server" Text="查找" OnClick="BtnSearch_Click" />
        <input type="button" class="btn btn-primary" value="确定" onclick="GetCheckvalue();" />
        <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="取消" OnClientClick="window.close();return false;" />
    </div>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="td_s">ID</td>
            <td class="td_l">商品图片</td>
            <td>商品名称</td>
            <td class="td_l">商品零售价</td>
        </tr>
        <asp:Repeater ID="RPT" runat="server">
            <ItemTemplate>
                <tr>
                    <td align="center"><label><input data-name="<%#Eval("Proname") %>" name="idrad" type="radio" value="<%#Eval("ID") %>" /><%#Eval("ID") %></label></td>
                    <td align="center"><img src=" <%#getproimg()%>" class="img_50" /></td>
                    <td align="left"><%#Eval("Proname") %></td>
                    <td align="center"><%#Eval("LinPrice","{0:c}")%></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function GetCheckvalue(itemid) {
            var $rad = $("[name=idrad]:checked");
            if ($rad.length < 1) { alert("请先选定值"); }
            if (opener) {
                opener.document.getElementById("Productsname<%=Request.QueryString["act"]%>").value = $rad.data("name");
                opener.document.getElementById("HiddenField<%=Request.QueryString["act"]%>").value = $rad.val();
            }
            window.close();
        }
    </script>
</asp:Content>
