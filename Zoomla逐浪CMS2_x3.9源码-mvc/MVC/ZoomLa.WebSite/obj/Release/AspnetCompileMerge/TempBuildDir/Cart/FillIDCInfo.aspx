<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FillIDCInfo.aspx.cs" Inherits="ZoomLaCMS.Cart.FillIDCInfo"  MasterPageFile="~/Cart/order.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>订单提交</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="head_div hidden-xs">
    <a href="/"><img src="<%=Call.LogoUrl %>" /></a>
    <div class="input-group pull-right skey_div text_300">
        <input type="text" id="skey_t" placeholder="全站检索" class="form-control skey_t" data-enter="0"/>
        <span class="input-group-btn">
            <input type="button" value="搜索" class="btn btn-default" onclick="skey();" data-enter="1"/>
        </span>
    </div>
    <div class="clearfix"></div>
</div>
 <table class="table table-bordered table-striped">
        <tr><td class="td_md">产品名称</td><td><asp:Label runat="server" ID="Proname_L"></asp:Label></td></tr>
      <%--  <tr><td>产品类别</td><td><asp:Label runat="server" ID="NodeName_L"></asp:Label></td></tr>--%>
        <tr><td>绑定域名</td><td><ZL:TextBox runat="server" ID="BindDomain_T" CssClass="form-control m715-50" AllowEmpty="false" ValidType="Url" /><span class="r_red">*</span></td></tr>
        <tr><td>备案号</td><td>
            <ZL:TextBox runat="server" ID="Record_T" CssClass="form-control m715-50"/>
            <div class="r_green">如何该域名已取得备案号,请在这里填写</div>
                        </td></tr>
        <tr><td>订购时限</td><td><asp:DropDownList runat="server" ID="IDCTime_DP" DataTextField="name" DataValueField="time" CssClass="form-control text_md"></asp:DropDownList></td></tr>
        <tr><td>操作系统</td><td>
            <asp:DropDownList runat="server" ID="OPSys_DP" CssClass="form-control text_md">
                <asp:ListItem Value="windows" Selected="True">Windows</asp:ListItem>
                <asp:ListItem Value="linux">Linux</asp:ListItem>
            </asp:DropDownList></td></tr>
        <tr><td>机房位置</td><td>
            <asp:DropDownList runat="server" ID="ServerPos_DP" CssClass="form-control text_md">
                <asp:ListItem Value="1" Selected="True">华夏互联IDC机房</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr><td>管理账号</td><td><span>webmaster@您的域名</span></td></tr>
        <tr><td>密码</td><td><ZL:TextBox runat="server" ID="Pwd_T" TextMode="Password" CssClass="form-control m715-50" AllowEmpty="false" /><span class="r_red">*</span></td></tr>
        <tr><td>密码重复</td><td><ZL:TextBox runat="server" ID="CPwd_T" TextMode="Password"  CssClass="form-control m715-50" AllowEmpty="false" /><span class="r_red">*</span></td></tr>
        <tr><td>密码自动生成</td><td><asp:CheckBox runat="server" ID="AutoPwd_Chk" Checked="true" Text="随机生成一个密码" /></td></tr>
        <tr><td>产品特性</td><td><asp:Label runat="server" ID="ProContent_L"></asp:Label></td></tr>
        <tr><td>购买协议</td><td><label><input type="checkbox" checked="checked" />我已阅读,理解并接受此协议</label></td></tr>
        <tr><td></td><td><asp:Button runat="server" ID="Submit_Btn" Text="提交订单" OnClick="Submit_Btn_Click" CssClass="btn btn-info" /></td></tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>