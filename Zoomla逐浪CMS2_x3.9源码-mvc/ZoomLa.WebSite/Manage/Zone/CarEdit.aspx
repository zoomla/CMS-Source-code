<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarEdit.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.CarEdit" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>修改车辆信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2" class="text-center">修改车辆信息</td>
            </tr>
            <tr>
                <td style="width: 24%;">车辆名称：</td>
                <td>
                    <asp:TextBox ID="txtCarName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCarName" ErrorMessage="请输入车辆名称"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>车辆售价：</td>
                <td>
                    <asp:TextBox ID="txtCarMoney" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCarMoney" ErrorMessage="请输入车辆价格"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtCarMoney" ErrorMessage="请输入正确的价格" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>车辆LOG图片：</td>
                <td>
                    <asp:TextBox ID="txtCarLog" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCarLog" ErrorMessage="请上传车辆的LOG图片"></asp:RequiredFieldValidator>
                    <iframe id="smallimgs" style="top: 2px" src="../../shop/fileupload.aspx?menu=txtCarLog" width="100%" height="25px" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"></iframe>
                </td>
            </tr>
            <tr>
                <td>车辆图片：</td>
                <td>
                    <asp:TextBox ID="txtCarImg" CssClass="form-control" runat="server" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCarImg" ErrorMessage="请上传车辆的图片"></asp:RequiredFieldValidator>
                    <iframe id="Iframe1" style="top: 2px" src="../../shop/fileupload.aspx?menu=txtCarImg" width="100%" height="25px" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"></iframe>
                </td>
            </tr>
            <tr>
                <td>车辆简介：</td>
                <td>
                    <asp:TextBox ID="txtCarContext" CssClass="form-control" runat="server" Height="100px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="保存" OnClick="btnSubmit_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
