<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Friendsearch_quick.aspx.cs" Inherits="Friendsearch_quick" %>
<%@ Register Src="../../UserZone/WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<%@ Register Src="ControlPageLink.ascx" TagName="ControlPageLink" TagPrefix="uc3" %>
<%@ Register Src="~/User/UserFriend/WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>好友搜索</title>
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
    <table  class="table table-bordered" style="margin-top:10px;">
        <tr>
            <td valign="top" width="100">
                <uc3:ControlPageLink ID="ControlPageLink1" runat="server"></uc3:ControlPageLink>
            </td>
            <td valign="top">
                <div>
                    <asp:Panel ID="quickPanel" runat="server">
                        <table class="table table-striped table-bordered table-hover">
                            <tr>
                                <td align="right">性别：</td>
                                <td width="69%" align="left">
                                    <asp:RadioButtonList ID="RadioButtonList1" name="RadioButtonList1" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem>男生</asp:ListItem>
                                        <asp:ListItem Selected="True">女生</asp:ListItem>
                                    </asp:RadioButtonList></td>
                            </tr>
                            <tr>
                                <td align="right">年龄：</td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox1" CssClass="form-control text_md" runat="server" Width="50px"></asp:TextBox>~
									<asp:TextBox ID="TextBox2" CssClass="form-control text_md" runat="server" Width="50px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">地区：</td>
                                <td align="left">
                                    <asp:DropDownList ID="DropDownList3" CssClass="form-control text_md" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:DropDownList ID="DropDownList4" CssClass="form-control text_md" runat="server" Visible="false"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trSearchFriendMarry" runat="server">
                                <td align="right">婚姻状况：</td>
                                <td align="left">
                                    <asp:DropDownList CssClass="form-control text_md" ID="marryDropDownList" runat="server"></asp:DropDownList></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" height="50px">
                                    <asp:Button ID="quick2btn" runat="server" CssClass="btn btn-primary" Text="快速搜索" OnClick="quick2btn_Click" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Label ID="Label1" runat="server" ForeColor="#ff0000" Font-Size="13" Text=""></asp:Label>
                    <asp:Panel ID="quickresultPanel" runat="server" Width="100%" Visible="false">
                        <table class="table table-bordered">
                            <tr>
                                <td>
                                    <asp:DataList ID="DataList1" runat="server" Width="100%">
                                        <ItemTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="18%">&nbsp;</td>
                                                    <td width="82%">
                                                        <a href="#">
                                                            <%#Eval("UserName") %>
                                                        </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" height="143">
                                                        <a href="../User/Usershow.aspx?userid=<%#DataBinder.Eval(Container.DataItem,"UserID") %>">
                                                            <asp:Image ID="Image1" runat="server" Height="120px" Width="120px" ImageUrl='<%#GetPic(Eval("UserID","{0}").ToString()) %>' /></a></td>
                                                    <td valign="top">&nbsp;<%#DataBinder.Eval(Container.DataItem, "UserLove")%></td>
                                                </tr>
                                                <tr>
                                                    <td height="10" align="center">&nbsp;</td>
                                                    <td align="right">
                                                        <script type="text/javascript">
                                                            function sss() {
                                                                var ajax = new AJAXRequest;
                                                                ajax.get("/user/UserZone/AddFave.aspx?Sid=1", function (obj) {
                                                                    alert("收藏成功！");
                                                                });
                                                            }
                                                        </script>
                                                        <a href="../../Message/MessageSend.aspx?ToID=<%#Eval("UserID")%>">给他留言</a>&nbsp;<a href="javascript:showPopWin('添加好友','showfriendsearch.aspx?sID=<%#DataBinder.Eval(Container.DataItem,"UserID") %>&Math.random()',400,200, refpage,true)">加为好友</a>&nbsp;<a onclick="javascript:sss()">加为收藏</a>&nbsp;</td>
                                                    <caption>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </caption>
                                                </tr>
                                                <tr>
                                                    <td height="1" colspan="2" align="center" bgcolor="#CC6600"></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
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
