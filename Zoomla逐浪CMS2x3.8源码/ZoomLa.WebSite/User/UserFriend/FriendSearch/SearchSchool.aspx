<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="SearchSchool.aspx.cs" Inherits="SearchSchool" %>
<%@ Register Src="../../UserZone/WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<%@ Register Src="ControlPageLink.ascx" TagName="ControlPageLink" TagPrefix="uc3" %>
<%@ Register Src="~/User/UserFriend/WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>搜索好友</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="zone"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/UserZone/Default.aspx">我的空间</a></li>
        <li class="active">搜索好友</li>
    </ol>
</div>
<div class="container btn_green">
    <uc2:WebUserControlTop ID="WebUserControlTop2" runat="server" />
    <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" />
</div>
<div class="container btn_green">
    <table class="table table-bordered" style="margin-top: 10px;">
        <tr>
            <td valign="top" width="100px">
                <uc3:ControlPageLink ID="ControlPageLink2" runat="server"></uc3:ControlPageLink>
            </td>
            <td valign="top">
                <asp:Panel ID="quickPanel" runat="server" Width="100%">
                    <table class="table table-striped table-bordered table-hover">
                        <tr>
                            <td width="30%" align="right">对方性别：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">男生</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="true">女生</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">居住地区：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList3" CssClass=" form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList4" CssClass="form-control" runat="server" Visible="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">学校：</td>
                            <td>
                                <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="Button1" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Label ID="Label1" runat="server" ForeColor="#ff0000" Font-Size="13" Text=""></asp:Label>
                <asp:Panel ID="quickresultPanel" runat="server" Width="100%" Visible="false">
                    <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover" runat="server"  Width="100%">
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" height="191" width="100%">
                                <tr>
                                    <td width="18%">&nbsp;
                                    </td>
                                    <td width="82%">
                                        <a href="#">
                                            <%#DataBinder.Eval(Container.DataItem,"UserName") %>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" height="163">
                                        <a href="../User/Usershow.aspx?userid=<%#DataBinder.Eval(Container.DataItem,"UserID") %>">
                                            <asp:Image ID="Image1" runat="server" Height="120px" Width="120px" ImageUrl='<%#DataBinder.Eval(Container.DataItem,"UserPic") %>' /></a>
                                    </td>
                                    <td valign="top">&nbsp;<%#DataBinder.Eval(Container.DataItem, "UserLove")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="32" align="center">&nbsp;
                                    </td>
                                    <td align="right">
                                        <a href="#">给他留言</a>&nbsp;<a href="javascript:showPopWin('添加好友','showfriendsearch.aspx?sID=<%#DataBinder.Eval(Container.DataItem,"UserID") %>&Math.random()',400,200, refpage,true)">加为好友</a>&nbsp;<a href="#">同居邀请</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="1" colspan="2" align="center" bgcolor="#CC6600"></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                    <span class="center-block text-center">
                        共
                        <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
						<asp:Label ID="Toppage" runat="server" Text=""></asp:Label>
                        <asp:Label ID="Nextpage" runat="server" Text=""></asp:Label>
                        <asp:Label ID="Downpage" runat="server" Text=""></asp:Label>
                        <asp:Label ID="Endpage" runat="server" Text=""></asp:Label>
                        页次：
                        <asp:Label ID="Nowpage" runat="server" Text=""></asp:Label>/<asp:Label ID="PageSize" runat="server" Text=""></asp:Label>页
						<asp:Label ID="pagess" runat="server" Text=""></asp:Label>个/页 转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                        页
                    </span>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function refpage(ret) {
            if (typeof (ret) != "undefined") {
                window.location.href = "SearchSchool.aspx";
            }
        }
    </script>
</asp:Content>
