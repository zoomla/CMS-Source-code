<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControlMy.ascx.cs"
    Inherits="WebUserControlMy" %>
<%@ Register Src="WebUserControlLabel.ascx" TagName="WebUserControlLabel" TagPrefix="uc1" %>   
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td align="center" style="height: 19px">
            &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton></td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td id="td1" runat="server" valign="top">
        </td>
        <td rowspan="2" valign="top">
            <uc1:WebUserControlLabel id="WebUserControlLabel1" runat="server">
            </uc1:WebUserControlLabel>
        
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;<asp:DataList ID="DataList1" runat="server" DataKeyField="ID">
                <ItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center">
                                <asp:Image ID="Image2" runat="server" Height="79px" Width="79px" ImageUrl='<%# geturl(DataBinder.Eval(Container.DataItem, "CbyID").ToString())%>' /></td>
                        </tr>
                        <tr>
                            <td align="center">
                                [<asp:LinkButton ID="DelCommant" runat="server" OnClick="DelCommant_Click">删除</asp:LinkButton>]
                                &nbsp;<%# fotmatString(DataBinder.Eval(Container.DataItem, "CbyID").ToString(), DataBinder.Eval(Container.DataItem, "CbyType").ToString())%></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList></td>
    </tr>
    <tr>
        <td align="center" style="height: 19px">
            共<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>&nbsp;
            <asp:Label ID="Toppage" runat="server" Text=""></asp:Label>
            <asp:Label ID="Nextpage" runat="server" Text=""></asp:Label>
            <asp:Label ID="Downpage" runat="server" Text=""></asp:Label>
            <asp:Label ID="Endpage" runat="server" Text=""></asp:Label>
            页次：<asp:Label ID="Nowpage" runat="server" Text=""></asp:Label>/<asp:Label ID="PageSize"
                runat="server" Text=""></asp:Label>页
            <asp:Label ID="pagess" runat="server" Text=""></asp:Label>个/页
        </td>
        <td rowspan="2" valign="top">
        </td>
    </tr>
</table>
