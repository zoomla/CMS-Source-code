<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Search_Work.aspx.cs" Inherits="Search_Work" EnableViewStateMac="false" %>
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
        <li><a href="User/UserZone/Default.aspx">我的空间</a></li>
        <li class="active">搜索好友</li>
    </ol>
</div>
<div class="container btn_green">
    <uc2:WebUserControlTop ID="WebUserControlTop2" runat="server" />
    <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" />
</div>
<div class="container btn_green">
    <table class="table table-bordered" style="margin-top:10px;">
        <tr>
            <td valign="top" width="100px">
                <uc3:ControlPageLink ID="ControlPageLink1" runat="server"></uc3:ControlPageLink>
            </td>
            <td valign="top">
                <div>
                    <asp:Panel ID="quickPanel" runat="server">
                        <table class="table table-striped table-bordered table-hover">
                            <tr>
                                <td width="20%" align="right">对方性别：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="男生">男生</asp:ListItem>
                                        <asp:ListItem Value="女生" Selected="true">女生</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">年龄：
                                </td>
                                <td align="left"><asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Width="50"></asp:TextBox>~
									<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Width="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">对方学历：
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="cblBachelor" Width="100%" runat="server" RepeatColumns="3"
                                        RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">职 业：
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="cblWork" runat="server" Width="100%" RepeatColumns="3" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">月收入额：
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="cblMonthIn" runat="server" Width="100%" RepeatColumns="3" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">居住地区：
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList3" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DropDownList4" CssClass="form-control" runat="server" Visible="false">
                                    </asp:DropDownList>
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
                    <asp:Panel ID="quickresultPanel" runat="server" Width="100%">
                        <asp:DataList ID="DataList1" runat="server" Width="100%">
                            <ItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" height="191" width="100%">
                                    <tr>
                                        <td width="18%">
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
                                        <td valign="top"><%#DataBinder.Eval(Container.DataItem, "UserLove")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="32" align="center">
                                        </td>
                                        <td align="right">
                                            <a href="#">给他留言</a><a href="javascript:showPopWin('添加好友','showfriendsearch.aspx?sID=<%#DataBinder.Eval(Container.DataItem,"UserID") %>&Math.random()',400,200, refpage,true)">加为好友</a><a
                                                href="#">同居邀请</a>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td height="1" colspan="2" align="center" bgcolor="#CC6600"></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </asp:Panel>
                </div>
            </td>
        </tr>
    </table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function refpage(ret) {
            if (typeof (ret) != "undefined") {
                window.location.href = "Friendsearch_quick.aspx";
            }
        }
    </script>
</asp:Content>
