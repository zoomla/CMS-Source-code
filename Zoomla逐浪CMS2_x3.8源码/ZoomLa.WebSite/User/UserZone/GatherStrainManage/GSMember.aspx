<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="GSMember.aspx.cs" Inherits="ZoomLa.GatherStrainManage.GSMember" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<%@ Register Src="WebUserControlGztherLetf.ascx" TagName="WebUserControlGztherLetf" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>群族成员</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="GSManage.aspx">我的群族</a></li>
        <li><a href="GSBuid.aspx?GSID=<%=GSID %>&where=5"><asp:Label ID="labGSName" runat="server"></asp:Label></a></li>
        <li class="active">群族成员</li>
    </ol>
    <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" /><br />
    <uc2:WebUserControlGztherLetf ID="WebUserControlGztherLetf1" runat="server" />
    <div>
        <a href="GSBuid.aspx?GSID=<%=GSID %>&where=5"><asp:Image ID="imgGSICQ" runat="server" Width="150px" Height="100px" /></a>
    </div>
    <table width="100%%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <div class="btn-group">
                    <a href='GSBuid.aspx?GSID=<%=GSID %>' class="btn btn-primary">群族首页</a>
                    <a href='CreatHuaTee.aspx?GSID=<%=GSID %>' class="btn btn-primary">话题</a>
                    <a href='FileShareManage.aspx?GSID=<%=GSID %>' class="btn btn-primary">文件共享</a>
                    <a href='GSMember.aspx?GSID=<%=GSID %>' class="btn btn-primary">成员</a>
                </div>
            </td>
            <td style="width: 2px">&nbsp;
            </td>
            <td></td>
        </tr>
    </table>
    <table class="table table-striped table-bordered">
        <tr>
            <td style="width: 70%">&nbsp;
                <asp:LinkButton ID="lbtnAll" runat="server" OnClick="lbtnAll_Click">所有成员</asp:LinkButton>
                |
				<asp:LinkButton ID="lbtnNewP" runat="server" OnClick="lbtnNewP_Click">最新成员</asp:LinkButton>
                |&nbsp;
				<asp:LinkButton ID="lbtnCome" runat="server" OnClick="lbtnCome_Click">最近来访成员</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataList ID="dlMember" runat="server" DataKeyField="ID" RepeatColumns="4" ShowFooter="False" ShowHeader="False" OnItemDataBound="dlMember_ItemDataBound">
                    <ItemTemplate>
                        <table class="table table-bordered">
                            <tr>
                                <td align="center">
                                    <img src='<%#DataBinder.Eval(Container.DataItem,"Userpic")%>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 20px">
                                    <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IsMy") %>' BackColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <a href="#">
                                        <%#DataBinder.Eval(Container.DataItem, "UserName")%>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <%#DataBinder.Eval(Container.DataItem, "LastCallTime")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
</asp:Content>
