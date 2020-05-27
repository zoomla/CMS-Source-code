<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductsSelect.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.ProductsSelect" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>选择商品</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="top_opbar">
        <div class="input-group" style="display:inline-block;">
            <select id="ddlnode" name="ddlnode" class="form-control text_300" onchange="UpdateNode()">
                <option value="0">全部栏目</option>
                <asp:Label runat="server" ID="NodeHtml_L"></asp:Label>
            </select>
            <asp:TextBox ID="TxtKeyWord" class="form-control text_300" runat="server" placeholder="请输入商品名称"></asp:TextBox>
            <span class="input-group-btn">
                <asp:LinkButton runat="server" ID="BtnSearch" OnClick="BtnSearch_Click" class="btn btn-info"><i class="fa fa-search"></i> 搜索商品</asp:LinkButton>
                <button type="button" class="btn btn-info" onclick="suresel();"><i class="fa fa-sign-in"></i> 确定选择</button>
            </span>
        </div>
    </div>
    <table class="table table-striped table-bordered" id="EGV">
        <tr>
            <td class="td_s">ID</td>
            <td class="td_l">商品图片</td>
            <td>商品名称</td>
            <td class="td_l">商品零售价</td>
        </tr>
        <asp:Repeater ID="RPT" runat="server">
            <ItemTemplate>
                <tr>
                    <td><label><input data-name="<%#Eval("Proname") %>" data-price="<%#Eval("LinPrice","{0:F2}") %>" name="idchk" type="checkbox" value="<%#Eval("ID") %>" /><%#Eval("ID") %></label></td>
                    <td><img src=" <%#getproimg()%>" class="img_50" /></td>
                    <td><%#Eval("Proname") %></td>
                    <td><%#Eval("LinPrice","{0:c}")%></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/SelectCheckBox.js"></script>
<script>
    $(function () { $("#ddlnode").val("<%:NodeID%>"); })
    function suresel() {
        var list = [];
        var $chks = $("#EGV input[name='idchk']:checked");
        if ($chks.length < 1) { alert("尚未选择商品"); return; }
        $chks.each(function () {
            var $this = $(this);
            list.push({ id: $this.val(), name: $this.data("name"), price: $this.data("price") });
        });
        parent.window["<%:CallBack%>"](JSON.stringify(list));
        parent.closeDiag();
    }
    function UpdateNode() {
        location = "ProductsSelect.aspx?NodeID=" + $("#ddlnode").val()+"&callback=<%:CallBack%>";
    }
</script>
</asp:Content>
