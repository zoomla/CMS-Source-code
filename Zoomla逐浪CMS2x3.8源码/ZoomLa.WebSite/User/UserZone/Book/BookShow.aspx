<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="BookShow.aspx.cs" Inherits="BookAdd" %>
<%@ Register Src="WebUserControlCollect.ascx" TagName="WebUserControlCollect" TagPrefix="uc2" %>
<%@ Register Src="WebUserControlComment.ascx" TagName="WebUserControlComment" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc3" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>查看读书</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li><a href="BookList.aspx">读书</a></li>
        <li class="active">查看书籍</li>
    </ol>
    <uc3:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc3:WebUserControlTop>
    <br />
    <table class="table table-bordered table-striped" style="margin-top:10px;">
        <tr>
            <td align="center" class="catebar">读 书 </td>
        </tr>
        <tr>
            <td>
                <table class="table table-bordered">
                    <tr>
                        <td valign="middle" style="padding-right: 70px">
                            <table>
                                <tbody>
                                    <tr>
                                        <td valign="middle">
                                            <asp:Image ID="Image1" runat="server" Height="188px" Width="250px" /></td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="center" valign="middle" class="paybutton">加入我的收藏</td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td valign="top">   
                            <table class="table table-striped table-bordered table-hover">
                                <tbody>
                                    <tr>
                                        <td class="text-center" colspan="2"><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th width="139" align="right" style="height: 23px">作者：</th>
                                        <td width="361" colspan="3" style="height: 23px">
                                            <asp:Label ID="Label3" runat="server" Text="Label" Width="107px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th align="right" style="height: 21px">副标题：</th>
                                        <td class="memberprice" colspan="3" style="height: 21px">
                                            <asp:Label ID="Label4" runat="server" Text="Label" Width="107px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th align="right" style="height: 22px">ISBN：</th>
                                        <td colspan="3" style="height: 22px">
                                            <asp:Label ID="Label5" runat="server" Text="Label" Width="107px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th align="right" style="height: 23px">定 价：</th>
                                        <td colspan="3" style="height: 23px">
                                            <asp:Label ID="Label6" runat="server" Text="Label" Width="107px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th align="right" style="height: 24px">出版社：</th>
                                        <td colspan="3" style="height: 24px">
                                            <asp:Label ID="Label7" runat="server" Text="Label" Width="107px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th align="right" style="height: 29px">出版年份：</th>
                                        <td colspan="3" style="height: 29px">
                                            <asp:Label ID="Label8" runat="server" Text="Label" Width="107px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="text-center"><uc2:WebUserControlCollect ID="WebUserControlCollect1" runat="server" /></td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
                <div class="clear"></div>
                <h4>详细介绍：</h4>
                <div class="GoodsDetailsWarp">
                    <p style="text-align: left; line-height: 15pt; margin: 0cm 0cm 0pt; background: white; mso-pagination: widow-orphan" class="MsoNormal" align="left">
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                        <span class="MsoNormal" style="text-align: left; line-height: 15pt; margin: 0cm 0cm 0pt; background: white; mso-pagination: widow-orphan"></span>
                    </p>
                </div>
                <h4>在线评论：</h4>
                <div class="GoodsDetailsWarp">
                    <p style="text-align: left; line-height: 15pt; margin: 0cm 0cm 0pt; background: white; mso-pagination: widow-orphan" class="MsoNormal" align="left">
                        <uc1:WebUserControlComment ID="WebUserControlComment1" runat="server" />
                        <span class="MsoNormal" style="text-align: left; line-height: 15pt; margin: 0cm 0cm 0pt; background: white; mso-pagination: widow-orphan"></span>
                    </p>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
