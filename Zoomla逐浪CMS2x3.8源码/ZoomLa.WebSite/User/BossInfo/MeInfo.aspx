<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MeInfo.aspx.cs" Inherits="User_BossInfo_MeInfo" ClientIDMode="Static" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>上级代理商信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">上级代理商信息 </li>
    </ol>
    <div>
    <table class="table table-striped table-bordered table-hover">
            <tr>
            <td align="center" colspan="2"> 你的上级代理商信息  </td>
        </tr>
        <tr>
            <td align="right">代理商名称:</td>
            <td width="72%" align="left">&nbsp;<asp:Label ID="Label7" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="right">联系电话：</td>
            <td align="left">&nbsp;<asp:Label ID="Label8" runat="server" Text=""></asp:Label></td>
        </tr>
    <tr>
            <td align="center" colspan="2"> 代理商基本信息  </td>
                 
        </tr>
        <tr>
            <td align="right">地区:</td>
            <td width="72%" align="left">&nbsp;<asp:Label ID="tx_cname" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="right">代理商级别:</td>
            <td width="72%" align="left">&nbsp;<asp:Label ID="Label4" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">代理商名称:</td>
            <td width="72%" align="left">&nbsp;<asp:Label ID="Label5" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">代理商地址:</td>
            <td width="72%" align="left">&nbsp;<asp:Label ID="Label6" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">法定代理人:</td>
            <td width="72%" align="left">&nbsp;<asp:Label ID="Label9" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">营业执照:</td>
            <td width="72%" align="left">&nbsp;<asp:Label ID="Label10" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">代理商电话:</td>
            <td width="72%" align="left">&nbsp;<asp:Label ID="Label11" runat="server" Text=""></asp:Label></td>
        </tr>
                
            <tr>
            <td align="right">公司介绍:</td>
            <td width="72%" align="left">&nbsp;<asp:Label ID="Label12" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="center" colspan="2">银行帐号信息  ：</td>
        </tr>
        <tr>
            <td align="right">合同协议号：</td>
            <td align="left">&nbsp;<asp:Label ID="tx_money" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="right">开户人：</td>
            <td align="left">&nbsp;<asp:Label ID="tx_zong" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="right">开户银行1：</td>
            <td align="left">&nbsp;<asp:Label ID="tx_num" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">银行账号1：</td>
            <td align="left">&nbsp;<asp:Label ID="Label13" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">开户人2：</td>
            <td align="left">&nbsp;<asp:Label ID="Label14" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">开户银行2：</td>
            <td align="left">&nbsp;<asp:Label ID="Label15" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">银行账号2：</td>
            <td align="left">&nbsp;<asp:Label ID="Label16" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="center" colspan="2">联系人信息 ：</td>
        </tr>
        <tr>
            <td align="right" class="style2">联系人姓名：</td>
            <td align="left" style="height: 24px">&nbsp;<asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="right">性别：</td>
            <td align="left">&nbsp;<asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="right">联系人职务：</td>
            <td align="left">&nbsp;<asp:Label ID="Label3" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">联系电话：</td>
            <td align="left">&nbsp;<asp:Label ID="Label17" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">传真：</td>
            <td align="left">&nbsp;<asp:Label ID="Label18" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">邮编：</td>
            <td align="left">&nbsp;<asp:Label ID="Label19" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">Email：</td>
            <td align="left">&nbsp;<asp:Label ID="Label20" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">证件类型：</td>
            <td align="left">&nbsp;<asp:Label ID="Label21" runat="server" Text=""></asp:Label></td>
        </tr>
            <tr>
            <td align="right">证件号：</td>
            <td align="left">&nbsp;<asp:Label ID="Label22" runat="server" Text=""></asp:Label></td> 
        </tr>
              </table>
    </div>
</asp:Content>