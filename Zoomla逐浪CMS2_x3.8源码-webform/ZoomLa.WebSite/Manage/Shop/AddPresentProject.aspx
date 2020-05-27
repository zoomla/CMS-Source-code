<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="AddPresentProject.aspx.cs" Inherits="Zoomla.Website.manage.Shop.AddPresentProject" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加促销方案</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <asp:Label ID="Label1" runat="server" Text="添加促销方案"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 24%"><strong>方案名称：</strong></td>
            <td >
                <asp:TextBox ID="Promoname" runat="server" Width="253px" class="form-control" />
                <font color="red">*
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="方案名称不能为空!" ControlToValidate="Promoname"></asp:RequiredFieldValidator></font></td>
        </tr>
        <tr class="WebPart">
            <td style="width: 24%"><strong>有效期：</strong></td>
            <td>
                <asp:TextBox ID="Promostart" class="form-control" runat="server" Width="150px" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" />&nbsp;
    &nbsp;至      
                <asp:TextBox ID="Promoend" class="form-control" runat="server" Width="150px" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" />
                不填写为不限制有效期</td>
        </tr>
        <tr>
            <td style="width: 24%"><strong>价格区间：</strong></td>
            <td>
                <ZL:TextBox ID="Pricetop" runat="server" ValidType="FloatZeroPostive" Width="79px" class="form-control">100.00</ZL:TextBox><strong>≤</strong> 购物总金额 <strong>&lt;</strong>
                <ZL:TextBox ID="Priceend" runat="server" Width="79px" ValidType="FloatZeroPostive" class="form-control">500.00</ZL:TextBox>
                <span>不同促销方案的价格区间必须不同，否则会产生冲突<span style="background-color: #effcee"> </span><font color="red">*</font></span></td>
        </tr>
        <tr>
            <td style="width: 24%"><strong>促销内容：</strong></td>
            <td>
                <table class="table table-bordered">
                    <tr>
                        <td>
                            <asp:CheckBox ID="GetPresent" Text="可以" runat="server" Checked="True" />
                            <asp:TextBox ID="Presentmoney" runat="server" Width="45px" class="form-control">10</asp:TextBox>
                            元超值换购以下礼品中任一款：</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="PromoProlist" runat="server" Height="144px" SelectionMode="Multiple" Width="323px" class="form-control"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="AddProject_B" runat="server" class="btn btn-primary" Style="width: 110px;" Text="添加促销礼品" OnClientClick="SelectProducer();return false;" />
                            <asp:Button ID="DelProject_B" class="btn btn-primary" runat="server" Text="删除" OnClientClick="Clearoption();return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <font color="red">非选中</font><font color="green">状态的礼品添加或更新后将被</font><font color="red">删除<br /><font color="red"><b>选中状态</b></font><font color="green">的商品将被更新</font> 支持<b>Ctrl</b>或<b>Shift</b>键多选</font>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="IntegralTure" Text="可以得到" runat="server" />
                            <asp:TextBox ID="Integral" runat="server" Width="55px" class="form-control" />
                            点积分</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="5" align="center">
                <asp:HiddenField ID="HiddenID" runat="server" />
                <asp:Button ID="Save_B" runat="server" class="btn btn-primary" Text="添加" OnClick="Save_B_Click" />
                <a href="PresentProject.aspx" class="btn btn-primary">返回</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        function SelectProducer() {
            window.open('Addproject.aspx', '', 'width=600,height=450,resizable=0,scrollbars=yes');
        }
        function Clearoption() {
            var hiddenidvalue = document.getElementById("PromoProlist"); //获取已经存在的ID值

            for (var i = hiddenidvalue.options.length - 1; i >= 0; i--) {
                if (hiddenidvalue[i].selected == true) {
                    hiddenidvalue[i] = null;
                }
            }

        }
    </script>
</asp:Content>
