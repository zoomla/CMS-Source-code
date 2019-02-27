<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IDC_Ren.aspx.cs" Inherits="Manage_Shop_OtherOrder_IDC_Ren" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>IDC续费</title><style>.opion{font-size:0.5em; cursor:pointer;}</style></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-striped">
        <tr><td class="text-right" style="width: 20%;">订单编号:</td><td><asp:Label ID="OrderNo_L" runat="server"></asp:Label></td></tr>
        <tr><td class="text-right">用户操作:</td><td><label><input type="radio" name="usertype" value="1" checked="checked" />用户身份 <i class="fa fa-external-link
"></i></label></td></tr>
        <tr><td class="text-right">管理操作:</td>
            <td>
                <label><input type="radio" name="usertype" value="0" />管理员身份</label>
              <%--  <span class="rd_green">(以管理员身份,将直接完成订单,不需要支付操作)</span>--%>
            </td>
        </tr>
        <tr><td class="text-right">生效时间:</td><td><asp:Label runat="server" ID="STime_L"></asp:Label></td></tr>
        <tr><td class="text-right">到期时间:</td><td><asp:Label runat="server" ID="ETime_L"></asp:Label></td></tr>
        <tr class="zltab" id="tab0">
            <td class="text-right">新到期日:</td>
            <td>
                <asp:TextBox ID="ETime_T" runat="server" CssClass="form-control text_300" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' });" />
                <asp:Button runat="server" ID="UpdateETime_Btn" CssClass="btn btn-info" Text="确认操作" OnClick="UpdateETime_Btn_Click" />
            </td>
        </tr>
        <tr class="zltab active" id="tab1">
            <td class="text-right">商品信息:</td>
            <td>
                <table class="table table-bordered table-striped">
                    <tr>
                        <td>商品名称</td>
                        <td>订购时限</td>
                        <td>操作</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="ProName_L" runat="server"></asp:Label></td>
                        <td>
                            <asp:DropDownList runat="server" ID="IDCTime_DP" DataTextField="name" DataValueField="time" CssClass="form-control text_md"></asp:DropDownList></td>
                        <td>
                            <asp:Button ID="EBtnSubmit" Text="确认续费" class="btn btn-info" OnClick="EBtnSubmit_Click" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/jquery.validate.min.js"></script>
<script src="/JS/DatePicker/WdatePicker.js"></script>
<style type="text/css">
.zltab {display:none;}
.zltab.active {display:table-row;}
</style>
<script>
    $(function () {
        $("input[name=usertype]").click(function () {
            $(".zltab").removeClass("active");
            $("#tab" + $(this).val()).addClass("active");
        });
    })
</script>
</asp:Content>