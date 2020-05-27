<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="PicTureList.aspx.cs" Inherits="ZoomLa.WebSite.User.UserZone.Pic.PicTureList" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的相册</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li><a href="CategList.aspx">我的相册</a></li><li class="active"><%= CategName %></li>
		<div class="clearfix"></div>
    </ol>
    <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
    <table class="table table-bordered table-striped" style="margin-top:10px;">
        <tr>
            <td class="text-center">
                <%=UserName %>的相册<%=CategName %>
            </td>
        </tr>
        <tr>
            <td align="right">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <span class="pull-left"><%=type %></span>
                            <asp:Panel ID="Panel1" CssClass="pull-left" runat="server">
                                >
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CausesValidation="false">删除相册</asp:LinkButton>
                                >
                                <a href='EditCateg.aspx?CategID=<%=categid%>'> 修改相册信息</a> &nbsp;&nbsp;> <a href='UpPic.aspx?CategID=<%=categid %>'>添加照片</a> 
                            </asp:Panel>
                        </td>
                        <td style="width: 130px;">&gt; <a href='CategList.aspx?intervieweeID=<%=UserID%>'>返回<%=UserName %>的相册首页</a></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataList ID="dltPicList" CssClass="table table-striped table-bordered table-hover" runat="server" RepeatColumns="5" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td align="center">
                                    <a href='ShowPic.aspx?picID=<%#DataBinder.Eval(Container.DataItem,"ID") %>&where=2'>
                                        <asp:Image ID="Image1" runat="server" Height="100px" Width="100px" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"PicUrl")%>' />
                                </a></td>
                            </tr>
                            <tr>
                                <td align="center"><a href='ShowPic.aspx?picID=<%#DataBinder.Eval(Container.DataItem,"ID") %>&where=2'><%# DataBinder.Eval(Container.DataItem,"PicName")%> </a></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList></td>
        </tr>
        <tr>
            <td align="center">
                <li style="height: 30px; text-align: center">共
          <asp:Label ID="Allnum" runat="server"
              Text=""></asp:Label>
                    &nbsp;
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
                    页</li>
            </td>
        </tr>
    </table>
</asp:Content>
