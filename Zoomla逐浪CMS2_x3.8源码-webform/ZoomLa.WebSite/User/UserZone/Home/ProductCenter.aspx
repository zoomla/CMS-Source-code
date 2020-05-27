<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="ProductCenter.aspx.cs" Inherits="User_UserZone_Home_ProductCenter" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的小屋</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="zone"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
            <li class="active">商品中心</li>
            <div class="clearfix"></div>
        </ol>
    </div>
    <div class="container btn_green">
        <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" />
        <div class="us_topinfo" style="margin-top: 10px;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <div class="btn-group">
                            <a href="homeset.aspx" class="btn btn-primary" target="_blank">布置我的小屋</a>
                            <a href="ProductCenter.aspx" class="btn btn-primary">商品中心</a>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataList ID="DataList2" runat="server" Width="100%" RepeatColumns="7" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" height="129" width="100%">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%#getpic(DataBinder.Eval(Container.DataItem,"ProductPic").ToString()) %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <%#DataBinder.Eval(Container.DataItem,"ProductName") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="HomeImage/vip_icon1.gif" />
                                        </td>
                                        <td align="center">
                                            <font color="#ff6600">
												<%#DataBinder.Eval(Container.DataItem,"VipPrice") %><%=uname %>币</font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">价格:
                                        </td>
                                        <td align="center">
                                            <%#DataBinder.Eval(Container.DataItem,"Price") %><%=uname %>币
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <a href="javascript:showPopWin('购买商品','ProductBuy.aspx?pID=<%#DataBinder.Eval(Container.DataItem,"ID") %>&Math.random()',400,200, refpage,true)">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="HomeImage/button_buy.gif" /></a>
                                        </td>
                                        <td align="center">
                                            <a href="javascript:showPopWin('赠送商品','ProductGive.aspx?pID=<%#DataBinder.Eval(Container.DataItem,"ID") %>&Math.random()',400,400, refpage,true)">
                                                <asp:Image ID="Image4" runat="server" ImageUrl="HomeImage/button_give.gif" /></a>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </table>
        </div>
    </div>
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
        function refpage(ret) {
            if (typeof (ret) != "undefined") {

            }
        }
    </script>

</asp:Content>
