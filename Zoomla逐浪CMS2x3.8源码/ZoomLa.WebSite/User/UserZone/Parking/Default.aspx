<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>抢车位</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="zone"></div>
    <div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li class="active">抢车位</li>
		<div class="clearfix"></div>
    </ol></div>
    <div class="container btn_green">
    <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
    <asp:HiddenField ID="HiddenField1" runat="server" OnValueChanged="HiddenField1_ValueChanged" />
    <asp:HiddenField ID="HiddenField2" runat="server" OnValueChanged="HiddenField2_ValueChanged" />
    <asp:HiddenField ID="HiddenField3" runat="server" OnValueChanged="HiddenField3_ValueChanged" />
    <asp:HiddenField ID="HiddenField4" runat="server" OnValueChanged="HiddenField4_ValueChanged" />
    <asp:HiddenField ID="HiddenField5" runat="server" OnValueChanged="HiddenField5_ValueChanged" />
    <br />
    <div style="margin: auto;" class="us_topinfo">
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td width="85%" valign="top">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server" Height="50px" Width="100%">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left">
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/User/UserZone/Parking/CarImage/back.gif"
                                                    OnClick="ImageButton1_Click" />
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/User/UserZone/Parking/CarImage/reme.gif" OnClick="ImageButton2_Click" />
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/User/UserZone/Parking/CarImage/next.gif" OnClick="ImageButton3_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <h4>我的记录</h4>
                                                </td>
                                                <td align="right"></td>
                                            </tr>
                                        </table>
                                        <hr />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>我<%#DataBinder.Eval(Container.DataItem, "P_content")%>
                                                </td>
                                                <td width="20%" align="right">
                                                    <%#DataBinder.Eval(Container.DataItem, "P_introtime")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <asp:Panel ID="Panel2" runat="server" Width="100%">
                        <table>
                            <tr>
                                <td>我的钱包
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <h3>
                                        <asp:Label ID="lblPurse" runat="server" Text="" ForeColor="red"></asp:Label></h3>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="MagMyCarList.aspx">车市</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="javascript:showPopWin('我的车辆','ShowMyCar.aspx?Math.random()',500,300, retCar,true)">我的车辆</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="javascript:showPopWin('我的好友','MyFriendList.aspx?Math.random()',400,200, retFriend,true)">停放到好友的车位</a>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        //好友停车
        function retFriend(ret) {
            if (typeof (ret) != "undefined") {
                window.document.getElementById("<%=HiddenField1.ClientID%>").value = ret;
                $().submit();
            }
        }
        //选择车辆停放地
        function retCar(ret) {
            if (typeof (ret) != "undefined") {
                window.document.getElementById("<%=HiddenField2.ClientID%>").value = ret;
            $().submit();
        }
    }
    //停放车辆
    function SetCar(id) {
        window.document.getElementById("<%=HiddenField3.ClientID%>").value = id;
        $().submit();
    }
    function SetTT(id) {
        window.document.getElementById("<%=HiddenField4.ClientID%>").value = id;
        $().submit();
    }
    function SetJB(id) {

        window.document.getElementById("<%=HiddenField5.ClientID%>").value = id;
	    $().submit();
	}
    </script>

</asp:Content>
