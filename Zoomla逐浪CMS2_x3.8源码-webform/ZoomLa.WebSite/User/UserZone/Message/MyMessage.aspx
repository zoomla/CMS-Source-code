<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MyMessage.aspx.cs" Inherits="FreeHome.User.MyMessage" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的留言板</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="zone"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
            <li class="active">我的留言板</li>
            <div class="clearfix"></div>
        </ol>
    </div>
    <div class="container btn_green">
        <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
    <div class="us_topinfo">
        <table class="table table-bordered">
            <tr>
                <td valign="top" style="width: 100%">
                    <asp:DataList ID="DataList1" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" OnItemDataBound="DataList1_ItemDataBound" DataKeyField="ID">
                        <ItemTemplate>
                            <table class="table table-striped table-bordered table-hover">
                                <tr>
                                    <td align="center" rowspan="3">
                                        <asp:Image ID="Image1" runat="server" Height="96px" Width="96px" ImageUrl='<%# getuserpic(DataBinder.Eval(Container.DataItem, "SendID").ToString())%>' />
                                    </td>
                                    <td width="84%" style="height: 31px">
                                        <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="False" OnClick="lbtsave_Click"
                                            Visible='<%# getDelV(DataBinder.Eval(Container.DataItem, "SendID").ToString())%>'
                                            OnClientClick="return  confirm('你确定要删除这个留言吗？')">[删除]</asp:LinkButton>&nbsp;<%#getusername(DataBinder.Eval(Container.DataItem, "SendID").ToString())%>留言道:
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="white-space: normal">&nbsp;<%# DataBinder.Eval(Container.DataItem, "Mcontent")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;<a href="javascript:showPopWin('回复留言','MessageRestore.aspx?rID=<%# DataBinder.Eval(Container.DataItem, "ID")%>&Math.random()',400,200, refpage,true)">回复</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="top" style="width: 152px" class="trr"></td>
                                    <td class="trr">
                                        <asp:DataList ID="DataList2" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" DataKeyField="ID">
                                            <ItemTemplate>
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="trr">&nbsp;
												<asp:LinkButton ID="lbDelete" runat="server" CausesValidation="False" OnClick="lbDelete_Click"
                                                    OnClientClick="return  confirm('你确定要删除这个留言吗？')">[删除]</asp:LinkButton><%#getusername(DataBinder.Eval(Container.DataItem, "SendID").ToString())%>的回复<br />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "Mcontent")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td align="center">
                    共<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Toppage" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Nextpage" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Downpage" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Endpage" runat="server" Text=""></asp:Label>
                    页次：<asp:Label ID="Nowpage" runat="server" Text=""></asp:Label>
                    /<asp:Label ID="PageSize" runat="server" Text=""></asp:Label>
                    页<asp:Label ID="pagess" runat="server" Text=""></asp:Label>个/页 转到第
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>页
                </td>
            </tr>
            <tr>
                <td>
                    <table class="table table-striped table-bordered table-hover">
                        <tr>
                            <td align="center">内容:
                            </td>
                            <td>
                                <textarea cols="50" class="form-control" rows="5" id="TEXTAREA1" runat="server"></textarea>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TEXTAREA1" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="HiddenField1" runat="server" OnValueChanged="HiddenField1_ValueChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center"></td>
                            <td>
                                <asp:Button ID="savebtn" runat="server" Text="留言" CssClass="btn btn-primary" OnClick="savebtn_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function refpage(ret)
        {
            if (typeof (ret) != "undefined")
            {
                window.document.getElementById("<%=this.HiddenField1.ClientID %>").value = ret;
                $().submit();
            }
        }
    </script>

</asp:Content>
