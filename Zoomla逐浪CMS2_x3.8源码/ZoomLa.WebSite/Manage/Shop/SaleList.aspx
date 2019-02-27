<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaleList.aspx.cs" Inherits="Zoomla.Website.manage.Shop.SaleList" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>销售明细</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td>
                <span class="pull-left">
                    快速查找：
                    <asp:DropDownList ID="quicksouch" CssClass="form-control" Width="150" runat="server" AutoPostBack="True" OnSelectedIndexChanged="souchok_Click">
                        <asp:ListItem Selected="True" Value="1">所有销售记录</asp:ListItem>
                        <asp:ListItem Value="2">今天销售明细</asp:ListItem>
                        <asp:ListItem Value="3">本周销售明细</asp:ListItem>
                        <asp:ListItem Value="4">本月销售明细</asp:ListItem>
                    </asp:DropDownList>
                </span>
                <span class="pull-left" style="margin-left:5px;">
                    高级查询：
                    <asp:DropDownList ID="souchtable" CssClass="form-control" Width="150" runat="server">
                        <asp:ListItem Selected="True" Value="Reuser">客户名称</asp:ListItem>
                        <asp:ListItem Value="Username">用户名称</asp:ListItem>
                        <asp:ListItem Value="Proname">商品名称</asp:ListItem>
                        <asp:ListItem Value="AddTime">下单时间</asp:ListItem>
                        <asp:ListItem Value="OrderNo">订单编号</asp:ListItem>
                    </asp:DropDownList>
                </span>
                <div class="input-group pull-left" style="width:300px;">
                    <asp:TextBox runat="server" ID="souchkey" CssClass="form-control" onkeydown="return GetEnterCode('click','HidSearch');" placeholder="请输入需要搜索的内容" />
                    <span class="input-group-btn">
                        <asp:LinkButton runat="server" CssClass="btn btn-default" ID="souchok" OnClick="souchok_Click"><span class="fa fa-search"></span></asp:LinkButton>
                    </span>
                </div>
            </td>
        </tr>
    </table>
    <asp:Button ID="HidSearch" runat="server" style="display:none;" OnClick="souchok_Click" />
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="Orderlistid" PageSize="10" OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无销售记录！">
        <Columns>
            <asp:TemplateField HeaderText="下单时间">
                <ItemTemplate>
                    <%#getordertime(Eval("Orderlistid", "{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="订单编号">
                <ItemTemplate>
                    <a href="Orderlistinfo.aspx?id=<%#Eval("Orderlistid","{0}")%>"><%#GetOrder(Eval("Orderlistid","{0}"))%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="客户名称">
                <ItemTemplate>
                    <%#GetOrderiuser(Eval("Orderlistid","{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="用户名称" DataField="Username" />
            <asp:BoundField HeaderText="商品名称" DataField="proname" />
            <asp:BoundField HeaderText="单位" DataField="Danwei" />
            <asp:BoundField HeaderText="数量" DataField="Pronum" />
            <asp:TemplateField HeaderText="市场价">
                <ItemTemplate>
                    <%#string.Format("{0:c}", getpromoney(Eval("ProID","{0}")))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="指定价">
                <ItemTemplate>
                    <%#Eval("Shijia","{0:c}")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="金额">
                <ItemTemplate>
                    <%#Eval("AllMoney","{0:c}") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:HiddenField ID="AllMonkey_HF" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        $().ready(function () {
            var i = $("#Egv tr").length - 1;
            var all = $("#AllMonkey_HF").val();
            var str = "<tr><td colspan=\"9\" style='text-align:right;'>总计金额：</td><td>" + all + "</td></tr>";
            $("#Egv tr:nth-child(" + i + ")").after(str);
        })
        function getinfo(Orderlistid) {
            location.href = 'Orderlistinfo.aspx?id=' + Orderlistid;
        }
    </script>
</asp:Content>
