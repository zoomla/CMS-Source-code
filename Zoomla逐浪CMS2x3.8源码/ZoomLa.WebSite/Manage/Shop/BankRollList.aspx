<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankRollList.aspx.cs" Inherits="Zoomla.Website.manage.Shop.BankRollList" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>资金明细</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无记录！">
        <Columns>
            <asp:TemplateField HeaderText="交易时间">
                <ItemTemplate>
    <%#DataBinder.Eval(Container.DataItem, "PayTime", "{0:yyyy-MM-dd hh:mm:ss}")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户名">
                <ItemTemplate>
          <%#getusername(Eval("UserID","{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="交易方式">
                <ItemTemplate>
          <%#GetContent(Eval("PayPlatID", "{0}"))%>
                </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField HeaderText="支付序号">
                <ItemTemplate>
               <%#Eval("PaymentNum") %>
                </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField HeaderText="金额">
                <ItemTemplate>
            <%#Eval("MoneyPay","{0:f}") %>
                </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField HeaderText="实际金额">
                <ItemTemplate>
                 <%#Eval("MoneyTrue","{0:f}") %>
                </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField HeaderText="  交易状态">
                <ItemTemplate>
                 <%# GetStatus(Eval("Status","{0}"))%>
                </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField HeaderText="  处理状态">
                <ItemTemplate>
               <%# GetCStatus(Eval("CStatus","{0}"))%>
                </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
      <asp:HiddenField ID="AllMonkey_HF" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript">
        HideColumn("0,1");
        $().ready(function () {
            var i = $("#Egv tr").length - 1;
            var all = $("#AllMonkey_HF").val();
            var str = "<tr><td colspan=\"7\" style='text-align:right;'>总计金额：</td><td>" + all + "</td></tr>";
            $("#Egv tr:nth-child(" + i + ")").after(str);
        })
        function getinfo(Orderlistid) {
            location.href = 'Orderlistinfo.aspx?id=' + Orderlistid;
        }
    </script>
</asp:Content>