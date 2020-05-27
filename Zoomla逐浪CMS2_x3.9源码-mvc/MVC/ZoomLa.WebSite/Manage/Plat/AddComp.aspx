<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddComp.aspx.cs" Inherits="ZoomLaCMS.Manage.Plat.AddComp" MasterPageFile="~/Manage/I/Default.master" %>

<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagName="SFile" TagPrefix="ZL" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>公司管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered  ">
        <tr>
            <td>公司简称:</td>
            <td>
                <asp:TextBox runat="server" ID="CompShort_T" CssClass="form-control text_300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_m">公司名称:</td>
            <td>
                <asp:TextBox ID="CompName_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Logo:</td>
            <td>
                <ZL:SFile FType="Img" ID="Logo_SFile" runat="server" />
            </td>
        </tr>
        <tr>
            <td>创建人:</td>
            <td>
                <div class="input-group" style="width:401px;"><asp:TextBox CssClass="form-control text_300" runat="server" ID="CUser_T"></asp:TextBox>
                <span class="input-group-btn"><input type="button" class="btn btn-info" style="width:101px;" value="选择用户" onclick="SelUser();" /></span>
                    </div>
            </td>
        </tr>
        <tr>
            <td>企业邮箱:</td>
            <td>
                <div class="input-group">
                    <span class="input-group-addon" id="basic-addon1">@</span>
                    <ZL:TextBox runat="server" ID="EMail_T" ValidExpressionCustom="[a-z0-9]+\.[a-z0-9]+(?:\.[a-z0-9]+)?$" ValidError="邮箱后缀有误" style="width:260px;" CssClass="form-control" />
                    <asp:RegularExpressionValidator ID="Validator" runat="Server" ControlToValidate="EMail_T" ValidationExpression="[a-z0-9]+\.[a-z0-9]+(?:\.[a-z0-9]+)?$" ForeColor="red" ValidationGroup="compVaid" ErrorMessage="邮箱后缀格式不正确" Display="Dynamic" />
                </div>
            </td>
        </tr>
        <tr>
            <td>联系电话:</td>
            <td>
                <ZL:TextBox runat="server" ID="Telephone_T" CssClass="form-control text_300" ValidationGroup="compVaid" ValidType="PhoneNumber"></ZL:TextBox></td>
        </tr>
        <tr>
            <td>联系手机:</td>
            <td>
                <ZL:TextBox runat="server" ID="Mobile_T" CssClass="form-control text_300" ValidationGroup="compVaid" ValidType="MobileNumber"></ZL:TextBox></td>
        </tr>
        <tr>
            <td>公司网址:</td>
            <td>
                <asp:TextBox runat="server" ID="CompHref_T" CssClass="form-control text_300"></asp:TextBox></td>
        </tr>
        <tr>
            <td>公司详情:</td>
            <td>
                <asp:TextBox runat="server" ID="CompDesc_T" CssClass="form-control text_300" TextMode="MultiLine" Height="120"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>创建时间:</td>
            <td>
                <asp:TextBox runat="server" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" ID="CDate_T" CssClass="form-control text_300"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td></td>
            <td>
                <asp:Button ID="Save_Btn" runat="server" ValidationGroup="compVaid" CssClass="btn btn-primary" OnClick="Save_Btn_Click" Text="保存" />
                <a href="CompList.aspx" class="btn btn-primary">返回</a></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        var TemDiag = new ZL_Dialog();
        function SelUser() {
            TemDiag.title = "选择用户";
            TemDiag.maxbtn = false;
            TemDiag.url = "/Common/Dialog/SelGroup.aspx";
            TemDiag.ShowModal();
        }
        function UserFunc(list, select) {
            //var users = "";
            //$.each(list, function (i, val) {
            //    users += val.UserName + ",";
            //});
            $("#CUser_T").val(list[0].UserName);
            TemDiag.CloseModal()
        }
    </script>
</asp:Content>
