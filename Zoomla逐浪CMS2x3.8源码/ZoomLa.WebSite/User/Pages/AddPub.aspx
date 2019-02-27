<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="AddPub.aspx.cs" Inherits="User_Pages_AddPub" ClientIDMode="Static" EnableViewStateMac="false" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>互动信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="Default.aspx">企业黄页</a></li>
        <li class="active">互动信息</li>
    </ol>
    <div class="us_seta" style="margin-top: 10px;" id="manageinfo" runat="server">
        <table class="table table-bordered table-hover table-striped">
            <tr>
                <td colspan="2" class="text-center">
                    <asp:Label ID="LblModelName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">标题</td>
                <td>
                    <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">内容</td>
                <td>
                    <asp:TextBox ID="tx_PubContent" CssClass="form-control" runat="server" Height="107px" TextMode="MultiLine" style="max-width:400px;"></asp:TextBox></td>
            </tr>
            <asp:Literal ID="ModelHtml" runat="server"></asp:Literal><tr class="tdbgbottom border">
                <td colspan="2" style="height: 84px">
                    <asp:HiddenField ID="HdnModel" runat="server" />
                    <asp:HiddenField ID="HiddenParentid" runat="server" />
                    <asp:HiddenField ID="HdnPubid" runat="server" />
                    <asp:HiddenField ID="HdnID" runat="server" />
                    <asp:HiddenField ID="HdnType" runat="server" />
                    <asp:TextBox ID="FilePicPath" CssClass="form-control" runat="server" Text="fbangd" Style="display: none"></asp:TextBox>
                    <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" CssClass="btn btn-primary" />
                    <asp:Button ID="Button1" Text="返回" runat="server" OnClick="Button1_Click" CssClass="btn btn-primary" />
                    <br />
                    <ZL:ExGridView ID="GridView1" runat="server"></ZL:ExGridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Common.js"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>

