<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreEdit.aspx.cs" Inherits="ZoomLaCMS.Manage.UserShopMannger.StoreEdit" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>商品列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td style="width: 156px; height: 30px;">店铺名称：
            </td>
            <td style="height: 30px">
                <asp:Label ID="Namelabel" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 156px; height: 30px;">申请人用户名：
            </td>
            <td>
                <asp:Label ID="Label1" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 15%; height: 30px;">店铺类型：
            </td>
            <td>
                <asp:Label ID="Label6" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DetailsView ID="DetailsView1" runat="server" CellPadding="4" GridLines="None" Font-Size="12px" Style="margin-bottom: 3px; margin-top: 2px;" CssClass="table table-striped table-bordered table-hover">
                    <Fields></Fields>
                    <FooterStyle Font-Bold="True" BackColor="#FFFFFF" />
                    <CommandRowStyle Font-Bold="True" CssClass="tdbgleft" />
                    <FieldHeaderStyle Font-Bold="True" />
                    <PagerStyle HorizontalAlign="Left" />
                    <HeaderStyle Font-Bold="True" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                </asp:DetailsView>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <input id="Button1" type="button" class="btn btn-primary" value="返  回" onclick="javascript: history.go(-1)" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
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

