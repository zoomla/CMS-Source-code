<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="CategList.aspx.cs" Inherits="CategList" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的相册</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="zone"></div>
    <div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li class="active">我的相册</li>
		<div class="clearfix"></div>
    </ol></div>
<div class="container btn_green">
    <div>
        <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
    </div>
    <div>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td class="text-center">
                    我的相册
                </td>
            </tr>
            <tr>
                <td style="height: 50px">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 561px">可以有自己的相册来存放照片了，你可以从<a href="AddPicCateg.aspx">创建一个相册</a>开始 </td>
                            <td><a href="AddPicCateg.aspx">
                                <asp:Image ID="Image2" runat="server" ImageUrl="../Images/FoundCateg.jpg" />
                            </a></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td><%=content%>
                    <asp:DataList ID="dltCategList" runat="server" RepeatColumns="4">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td align="center"><a href="PicTureList.aspx?CategID=<%#DataBinder.Eval(Container.DataItem,"ID")%>">
                                        <asp:Image ID="Image1" runat="server" Width="139px" Height="106px" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "TitlePIcUrl")%>' />
                                    </a></td>
                                </tr>
                                <tr>
                                    <td align="center"><a href='PicTureList.aspx?CategID=<%#DataBinder.Eval(Container.DataItem,"ID")%>'><%#DataBinder.Eval(Container.DataItem, "PicCategTitle")%> </a></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList></td>
            </tr>
            <tr>
                <td align="center">
                    <span style="text-align: center">
                        共
                        <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
                        <asp:Label ID="Toppage" runat="server" Text=""></asp:Label>
                        <asp:Label ID="Nextpage" runat="server" Text=""></asp:Label>
                        <asp:Label ID="Downpage" runat="server" Text=""></asp:Label>
                        <asp:Label ID="Endpage" runat="server" Text=""></asp:Label>
                        页次：
                        <asp:Label ID="Nowpage" runat="server" Text=""></asp:Label>
                        /
                        <asp:Label ID="PageSize" runat="server" Text=""></asp:Label>
                        页
                        <asp:Label ID="pagess" runat="server" Text=""></asp:Label>
                        个/页 转到第
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                        页
                    </span>
                </td>
            </tr>
        </table>
    </div>
</div>
</asp:Content>
