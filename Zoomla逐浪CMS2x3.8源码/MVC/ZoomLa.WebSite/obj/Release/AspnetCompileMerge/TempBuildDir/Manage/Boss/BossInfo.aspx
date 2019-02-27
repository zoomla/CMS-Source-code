<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BossInfo.aspx.cs" Inherits="ZoomLaCMS.Manage.Boss.BossInfo" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>加盟商详情</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="display: none">
        <asp:HiddenField ID="HiddenNode" runat="server" />
        <asp:HiddenField ID="HiddenPnode" runat="server" />
    </div>
    <table class="table table-striped table-bordered table-hover">
        <tr class="tdbg">
            <td class="text-center" colspan="2">你的上级代理商信息  </td>

        </tr>
        <tr class="tdbg">
            <td class="text-right" style="width: 200px;">代理商名称:</td>
            <td class="text-left">&nbsp;<asp:Label ID="Label7" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="text-right">联系电话：</td>
            <td class="text-left">&nbsp;<asp:Label ID="Label8" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="text-center" colspan="2">代理商基本信息  </td>

        </tr>
        <tr class="tdbg">
            <td class="text-right">代理商名称:</td>
            <td class="text-left">&nbsp;<asp:Label ID="tx_cname" runat="server" Text=""></asp:Label></td>
        </tr>

        <tr class="tdbg">
            <td class="text-center" colspan="2">总 业 绩  ：</td>
        </tr>
        <tr class="tdbg">
            <td class="text-right">收益金额：</td>
            <td class="text-left">&nbsp;<asp:Label ID="tx_money" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="text-right">定单金额：</td>
            <td class="text-left">&nbsp;<asp:Label ID="tx_zong" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="text-right">定单数量：</td>
            <td class="text-left">&nbsp;<asp:Label ID="tx_num" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="text-center" colspan="2">招  商  费 ：</td>
        </tr>
        <tr class="tdbg">
            <td class="text-right">收益金额：</td>
            <td class="text-left" style="height: 24px">&nbsp;<asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="text-right">直接招商金额：</td>
            <td class="text-left">&nbsp;<asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="text-right">间接招商金额：</td>
            <td class="text-left">&nbsp;<asp:Label ID="Label3" runat="server" Text=""></asp:Label></td>
        </tr>

        <tr class="tdbg">
            <td class="text-center" colspan="2">下 级 数 量 </td>
        </tr>

        <tr class="tdbg">
            <td class="text-right">服务中心：</td>
            <td class="text-left" style="height: 24px">&nbsp;<asp:Label ID="fhwunum" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="text-right">E店: </td>
            <td class="text-left" style="height: 24px">&nbsp;<asp:Label ID="Enum" runat="server" Text=""></asp:Label></td>
        </tr>

    </table>
    <table class="table table-striped table-bordered table-hover">
        <tr class="text-center">
            <td style="width:10%" class="title">
                <asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" /></td>
            <td style="width:15%" class="title">卡号</td>
            <td style="width:20%" class="title">发放用户</td>
            <td style="width:10%" class="title">使用用户</td>
            <td style="width:20%" class="title">卡片状态</td>
            <td style="width:10%" class="title">操作</td>
        </tr>
        <ZL:ExRepeater ID="gvCard" PageSize="10" runat="server" PagePre="<tr id='page_tr'><td><input type='checkbox' id='chkAll'/></td><td colspan='5' id='page_td'>" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr>
                    <td class="text-center">
                        <input name="idchk" type="checkbox" value='<%# Eval("Card_ID")%>' /></td>
                    <td class="text-center"><%# Eval("CardNum")%></td>
                    <td class="text-left"><%#GetUserName(DataBinder.Eval(Container.DataItem ,"PutUserID").ToString()) %> </td>
                    <td class="text-center"><%#GetUserName(DataBinder.Eval(Container.DataItem ,"AssociateUserID").ToString()) %></td>
                    <td class="text-center"><%#GetState(DataBinder.Eval(Container.DataItem, "CardState").ToString())%></td>
                    <td class="text-center"><a href="CardView.aspx?id=<%#Eval("Card_ID") %>">查看</a></td>
                </tr>
            </ItemTemplate>
             <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>