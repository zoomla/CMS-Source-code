<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgioCommodityShow.aspx.cs" Inherits="manage_Shop_AgioCommodityShow" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>选择商品</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered">
            <tr id="tr1" runat="server">
                <td>
                    <table class="table table-striped table-bordered table-hover">
                        <tr class="title">
                            <td align="left" colspan="4">
                                <b>商品列表：</b></td>
                        </tr>
                        <tr class="tdbgleft">
                            <td width="5%" height="24" align="center">
                                <strong>ID</strong></td>
                            <td width="10%" height="24" align="center">
                                <strong>商品图片</strong></td>
                            <td width="40%" height="24" align="center">
                                <strong>商品名称</strong></td>
                            <td width="35%" height="24" align="center">
                                <strong>商品零售价</strong></td>
                        </tr>
                        <ZL:ExRepeater ID="Pagetable" runat="server" PagePre="<tr><td colspan='4' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
                            <ItemTemplate>
                                <tr class="tdbg">
                                    <td height="24" align="center">
                                        <%#Eval("ID") %>
                                    </td>
                                    <td height="24" align="center">
                                        <%#getproimg(DataBinder.Eval(Container,"DataItem.Thumbnails","{0}"))%>
                                    </td>
                                    <td height="24" align="center">
                                        <%#Eval("Proname") %>
                                        <input type="hidden" id="Pronames<%#Eval("ID") %>" value="<%#Eval("Proname") %>" />
                                    </td>
                                    <td height="24" align="center">
                                        <%#Eval("LinPrice","{0:c}")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate></FooterTemplate>
                        </ZL:ExRepeater>
                    </table>
                    </td>
            </tr>
            <tr>
                <td id="tdnode" class="tdbgleft" runat ="server">
               </td>
            </tr>
        </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>