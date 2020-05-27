<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepairDeailt.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.RepairDeailt" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>售后详情</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="text-right td_m">服务类型：</td>
            <td><asp:Label ID="ServiceType_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">提交数量：</td>
            <td><asp:Label ID="ProNum_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">问题描述：</td>
            <td>
                <textarea class="form-control" readonly="readonly" id="Deailt_T" runat="server" style="width:500px; height:140px;"></textarea>
            </td>
        </tr>
        <tr>
            <td class="text-right">图片信息：</td>
            <td>
                <div style="margin-top: 10px;" id="uploader" class="uploader">
                    <ul class="filelist"></ul>
                </div>
                <asp:HiddenField runat="server" id="Attach_Hid"></asp:HiddenField>
            </td>
        </tr>
        <tr>
            <td class="text-right">申请凭据：</td>
            <td><asp:Label ID="CretType_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">返回方式：</td>
            <td><asp:Label ID="ReType_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">取货地点：</td>
            <td>
                <asp:Label ID="TakeCounty_L" runat="server"></asp:Label> (<asp:Label ID="TakeAddress_L" runat="server"></asp:Label>)
            </td>
        </tr>
        <tr>
            <td class="text-right">取件时间：</td>
            <td><asp:Label ID="TakeTime_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">收货地点：</td>
            <td>
                <asp:Label ID="ReCounty_L" runat="server"></asp:Label> (<asp:Label ID="ReAddress_L" runat="server"></asp:Label>)
            </td>
        </tr>
        <tr>
            <td class="text-right">客户姓名：</td>
            <td><asp:Label ID="UserName_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">手机号码：</td>
            <td><asp:Label ID="Phone_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="Audit_Btn" CssClass="btn btn-primary" runat="server" Text="审核通过" OnClick="Audit_Btn_Click" />
                <asp:Button ID="UnAudit_Btn" CssClass="btn btn-primary" runat="server" Text="取消审核" OnClick="UnAudit_Btn_Click" />
                <asp:Button ID="Del_Btn" CssClass="btn btn-primary" runat="server" Text="删除" OnClick="Del_Btn_Click" />
                <a href="OrderRepairAudit.aspx" class="btn btn-primary">返回</a>
            </td>
        </tr>
    </table>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
    <script src="/JS/Controls/ZL_Webup.js"></script>
    <script>
        $().ready(function () {
            var imgs = $("#Attach_Hid").val();
            if (imgs != "") { ZL_Webup.AddReadOnlyLi(imgs); }
        });
    </script>
</asp:Content>

