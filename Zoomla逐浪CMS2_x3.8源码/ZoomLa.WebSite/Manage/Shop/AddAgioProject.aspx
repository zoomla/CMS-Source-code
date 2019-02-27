<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddAgioProject.aspx.cs" Inherits="manage_Shop_AddAgioProject" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加促销方案</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="EGV" class="table table-striped table-bordered table-hover">
                <tr>
                    <td colspan="2" class="spacingtitle"><%=type %>打折方案</td>
                </tr>
                <tr>
                    <td style="width: 24%;" class="text-right"><strong>方案名称：</strong></td>
                    <td style="text-align: left; width: 76%;">
                        <asp:TextBox ID="txtName" class="form-control" runat="server" Width="253px" />
                        <font color="red">*</font></td>
                </tr>
                <tr>
                    <td class="text-right"><strong>有效期：</strong></td>
                    <td class="text-left">
                        <asp:TextBox ID="txtStartTime" class="form-control" runat="server" Width="150px" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" />止
              <asp:TextBox ID="txtEndTime" runat="server" class="form-control" Width="150px" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" />不填写为不限制有效期
                    </td>
                </tr>
                <tr>
                    <td class="text-right"><strong>方案类型：</strong></td>
                    <td class="text-left">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                            <asp:ListItem Value="1" Selected="True">按商品打折</asp:ListItem>
                            <asp:ListItem Value="2">按商品类型打折</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="text-right"><strong>商品列表：</strong></td>
                    <td class="text-left">
                        <table class="table table-striped table-bordered table-hover">
                            <tr>
                                <td class="text-left">
                                    <select id="PromoProlist" name="PromoProlist" style="height: 144px;" multiple="multiple" class="form-control text_500"></select>
                                    <br />
                                    <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="添加打折商品" Style="width: 110px;" OnClientClick="SelectProducer();return false;" />
                                    <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="删除" OnClientClick="Clearoption();return false;" />
                                    <br />
                                    <font color="red">非选中</font>
                                    <font color="green">状态的商品添加或更新后将被</font>
                                    <font color="red">删除<br /><font color="red"><b>选中状态</b></font><font color="green">的商品将被更新</font> 支持<b>Ctrl</b>或<b>Shift</b>键多选 </font>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="text-center">
        <asp:HiddenField ID="HiddenID" runat="server" />
        <asp:Button ID="Submit_B" class="btn btn-primary" runat="server" Text="提交" OnClick="Submit_B_Click" />
        <input id="Button5" class="btn btn-primary" type="button" value="返回" onclick="javascript: window.history.go(-1);" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $().ready(function () {
            //$("#PromoProlist").append("<option value='n+1'>第N+1项</option>");
        })
        function SelectProducer() {
            if (document.getElementById("RadioButtonList1_0").checked)
                window.open('AddAgioCommodity.aspx?type=1', '', 'width=600,height=450,resizable=0,scrollbars=yes');
            else
                window.open('AddAgioCommodity.aspx?type=2', '', 'width=600,height=450,resizable=0,scrollbars=yes');
        }
        function Clearoption() {
            var hiddenidvalue = document.getElementById("PromoProlist"); //获取已经存在的ID值

            for (var i = hiddenidvalue.options.length - 1; i >= 0; i--) {
                if (hiddenidvalue[i].selected == true) {
                    hiddenidvalue[i] = null;
                }
            }
        }
        function SetPro(name, id) {
            $("#PromoProlist").append("<option value='" + id + "'>" + name + "</option>");
        }
        function selall() {
            $("#PromoProlist").find("option").attr("selected", true);
        }
        function clearAll() {
            $("#PromoProlist").find("option").remove();
        }
    </script>
</asp:Content>
