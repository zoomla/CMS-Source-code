<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDomain.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.Domain.AddDomain" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>申请域名</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="smart_f">
        <asp:TextBox runat="server" ID="domNameT" type="text" class="smart_input pull-left" placeholder="输入你心仪的网址前缀,如Hello" ToolTip="输入你心仪的网址前缀,如Hello" MaxLength="16"/>
        <span class="smart_span pull-left text-center"><%:GetDomName() %></span>
        <asp:Button runat="server" UseSubmitBehavior="false" ID="checkBtn" OnClick="checkBtn_Click" class="btn btn-primary smart_button" Text="查询" />
        <div class="clearfix"></div>
        <div style="padding-top:20px;">
            <asp:RequiredFieldValidator ID="RV1" runat="server" ControlToValidate="domNameT" ForeColor="Red" ErrorMessage="请输入域名" Display="Dynamic" SetFocusOnError="true" />
            <asp:RegularExpressionValidator ID="RV2" runat="server" ControlToValidate="domNameT" Display="Dynamic" ForeColor="Red" ErrorMessage="英文与数字组合最少三位,最多十六位" ValidationExpression="^([a-zA-Z]([a-zA-Z0-9]){2,15}$)" />
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css">
        .smart_f{ border:3px solid #333333; border-radius:5px; height:60px; width:720px;margin:auto;margin-top:30px;}
        .smart_f .smart_input{ margin-top:3px; height:48px;width:294px; border:none; padding-left:10px;font-size:16px; font-family:'Microsoft YaHei';color:#737384;}
        .smart_f .smart_span{margin-top:3px; display:block;width:310px;height:48px; background:#DADADA;border:1px solid #DADADA;border-radius:3px;color:#666666; font-size:40px;font-family:'Microsoft YaHei';line-height:46px;}
        .smart_f .smart_button{ width:95px; height:48px; margin-left:8px;margin-top:3px; font-family:'Microsoft YaHei';font-size:18px;}
        .smart_f .smart_nav{ margin-top:10px;margin-left:2%;margin-right:2%;}
        .smart_f .smart_nav li a{ color:#428bca;}
    </style>
</asp:Content>