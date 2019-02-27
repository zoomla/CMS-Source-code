<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="BookList.aspx.cs" Inherits="BookList" %>

<%@ Register Src="WebUserControlLabel.ascx" TagName="WebUserControlLabel" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc3" %>
<asp:Content ContentPlaceHolderID="head" runat="Server"><title>读书</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="zone"></div>
    <div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li class="active">读书<a href="BooktableAdd.aspx">[添加书籍]</a></li>
		<div class="clearfix"></div>
    </ol></div>
    <div class="container btn_green">
        <div>
            <uc3:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc3:WebUserControlTop>
        </div>
        <table class="table table-striped table-bordered">
            <tr>
                <td width="82%" height="26" valign="top">
                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td style="padding-left: 20px" class="catebar" height="26"><span class="M"><font class="textlink">读书</font></span></td>
                        </tr>
                    </table>
                </td>
                <td width="18%" rowspan="2" align="center" valign="top">
                    <table class="border1" border="0" cellspacing="0" cellpadding="0" width="170">
                        <tr>
                            <td style="padding-bottom: 2px; padding-left: 2px; padding-right: 2px; padding-top: 2px"
                                class="lh15" valign="top" align="center">
                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                </table>
                                <table border="0" cellspacing="5" cellpadding="0" width="95%">
                                    <tr>
                                        <td valign="top">
                                            <uc1:WebUserControlLabel ID="WebUserControlLabel1" runat="server"></uc1:WebUserControlLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="secback textlink" height="22" align="center"><strong>标签汇总</strong></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table class="table table-bordered">
                        <tr>
                            <td class="text-center">搜索你要找的书籍：
                                <asp:TextBox ID="Searchtxt" CssClass="form-control" runat="server" Width="286px"></asp:TextBox>
                                <asp:Button ID="sBtn" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="sBtn_Click" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="不能为空" ControlToValidate="Searchtxt"></asp:RequiredFieldValidator></td>
                        </tr>
                    </table>
                    <asp:DataList ID="DataList1" Width="100%" DataKeyField="ID" runat="server" RepeatColumns="1">
                        <ItemTemplate>
                            <table class="table table-bordered">
                                <tr>
                                    <td valign="top">
                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                            <tr>
                                                <td height="25" valign="bottom">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong><font color="#004d00">[<a href="BookShow.aspx?bID=<%#DataBinder.Eval(Container.DataItem,"ID") %>"><%# DataBinder.Eval(Container.DataItem,"BookTitle")%></a>]</font></strong></td>
                                                <td align="right">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" Visible='<%#GetV(DataBinder.Eval(Container.DataItem,"Uid").ToString()) %>' OnClick="LinkButton1_Click" OnClientClick="return  confirm('你确定要删除这个信息吗？')">[删除]</asp:LinkButton></td>
                                            </tr>
                                            <tr>
                                                <td width="23%" class="lh13" style="padding-bottom: 3px; padding-left: 0px; padding-right: 0px; display: block; padding-top: 3px">&nbsp; &nbsp; <a href="BookShow.aspx?bID=<%#DataBinder.Eval(Container.DataItem,"ID") %>">
                                                    <asp:Image ID="Image2" runat="server" Height="120px" Width="100px" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"Bookurl")%>' /></td>
                                                <td width="77%" valign="middle" class="lh13" style="padding-bottom: 3px; padding-left: 0px; padding-right: 0px; display: block; padding-top: 3px"><font color="#6c6c6c"> <%# GetStr(DataBinder.Eval(Container.DataItem, "BookContent").ToString())%> </font></td>
                                    </td>
                                    <td width="200" valign="middle"><%# DataBinder.Eval(Container.DataItem, "BookAddtime")%></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td align="center" height="55"><asp:Label ID="AddBook" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td align="center">共
                                <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
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
                                个/页
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
