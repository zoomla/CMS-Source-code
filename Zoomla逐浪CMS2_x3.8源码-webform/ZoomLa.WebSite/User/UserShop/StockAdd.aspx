<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="StockAdd.aspx.cs" Inherits="User_UserShop_StockAdd" EnableViewStateMac="false" %>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>我的店铺</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="shop" data-ban="store"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li><a href="ProductList.aspx">我的店铺</a></li>
            <li><a href="StockList.aspx">库存管理</a></li>
            <li class="active"><%= str %></li>
        </ol>
    </div>
    <div class="container btn_green">
        <uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
    </div>
    <div class="container margin_t10 btn_green">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2">出入库管理</td>
            </tr>
            <tr>
                <td class="td_l"><strong>选择单据类型：</strong></td>
                <td>
                    <label>
                        <input type="radio" value="0" name="stocktype_rad" checked="checked" />入库</label>
                    <label>
                        <input type="radio" value="1" name="stocktype_rad" />出库</label>
                </td>
            </tr>
            <tr>
                <td><strong>单据编号：</strong></td>
                <td>
                    <asp:Label ID="danjuhao_T" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><strong>商品：</strong></td>
                <td>
                    <asp:Label runat="server" ID="ProName_L"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><strong>商品数量：</strong></td>
                <td>
                    <asp:TextBox ID="Pronum" CssClass="form-control text_300" runat="server" Text="0" />
                    <span class="r_red">*</span>
                    <asp:RangeValidator ID="RV1" runat="server" ControlToValidate="Pronum" ErrorMessage="商品数量必须是整数!" Type="Integer" /></td>
            </tr>
            <tr>
                <td><strong>备  注：</strong></td>
                <td>
                    <asp:TextBox ID="stockcontent" CssClass="form-control" TextMode="MultiLine" runat="server" Height="91px" Style="max-width: 480px;" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="Save_Btn" CssClass="btn btn-primary" runat="server" Text="添加" OnClick="Save_Btn_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script>
        function ttname(value) {
            document.getElementById("danjuhaos").value = value + document.getElementById("danjuhaobak").value;
            document.getElementById("danjuhao").innerHTML = document.getElementById("danjuhaos").value
        }
    </script>
</asp:Content>