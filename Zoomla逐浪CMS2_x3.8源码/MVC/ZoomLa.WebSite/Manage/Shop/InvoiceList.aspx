<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceList.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.InvoiceList" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>支付明细</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无记录！">
        <Columns>
            <asp:TemplateField HeaderText="订单号码">
                <ItemTemplate>
                <a href="Orderlistinfo.aspx?id=<%#Eval("ID") %>"><%#Eval("OrderNo")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户名">
                <ItemTemplate>
                   <%#Eval("Rename", "{0}")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="交易时间">
                <ItemTemplate>
                <%#Eval("AddTime")%>
                </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField HeaderText="订单金额 + 运费 = 总额">
                <ItemTemplate>
              <%#Eval("Ordersamount","{0:c}")%> + <%#Getdeli(Eval("ID", "{0}"))%> = <%#GetMoney(Eval("ID","{0}"))%>
                </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField HeaderText="已开发票">
                <ItemTemplate> 
                   <%# GetDevelopedvotes(Eval("Developedvotes", "{0}"))%>
                </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
    </script>
</asp:Content>