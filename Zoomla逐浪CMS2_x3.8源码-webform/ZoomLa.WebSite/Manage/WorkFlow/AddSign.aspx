<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSign.aspx.cs" Inherits="Manage_WorkFlow_AddSign" MasterPageFile="~/Manage/I/Default.master" %>

<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加电子签名</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td>签章名：</td>
                <td>
                    <asp:TextBox runat="server" CssClass="form-control text_300" ID="signNameT" MaxLength="30" />
                    <span style="color: #f00">*</span><asp:RequiredFieldValidator ControlToValidate="signNameT" ID="r1" runat="server" ErrorMessage="签章名不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>签章图：</td>
                <td>
                    <ZL:SFileUp ID="SFile_Up" runat="server" />

                </td>
            </tr>
            <tr>
                <td>签章密码:</td>
                <td>
                    <asp:TextBox runat="server" ID="SignPwd_T" CssClass="form-control text_300" TextMode="Password"></asp:TextBox>
                    <span class="rd_green">默认为111111</span></td>
            </tr>
            <tr>
                <td>密钥：</td>
                <td>
                    <asp:TextBox runat="server" CssClass="form-control text_300" ID="signKeyT" Enabled="false" /><span class="rd_green">提示:勿需填,添加时自动生成</span>
                </td>
            </tr>
            <tr>
                <td>绑定用户：</td>
                <td>
                    <div class="input-group" style="width: 300px;">
                        <asp:TextBox runat="server" ID="BindUser_T" CssClass="form-control text_300" />
                        <span class="input-group-btn">
                            <input type="button" onclick="user.sel('BindUser', 'user', '')" value="选择用户" class="btn btn-info" />
                        </span>
                    </div>
                    <asp:HiddenField ID="BindUser_Hid" runat="server" />
                </td>
            </tr>
            <tr>
                <td>状态：</td>
                <td>
                    <asp:CheckBox runat="server" ID="statusChk" Text="启用" Checked="true" /></td>
            </tr>
            <tr>
                <td>备注：</td>
                <td>
                    <asp:TextBox runat="server" ID="remindT" TextMode="MultiLine" Height="100" CssClass="form-control text_300"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 30px;">
                <td>操作：</td>
                <td style="text-align: center;">
                    <asp:Button runat="server" ID="saveBtn" CssClass="btn btn-primary" Text="保存" OnClick="saveBtn_Click" />
                    <asp:Button runat="server" ID="editBtn" CssClass="btn btn-primary" Text="修改" OnClick="editBtn_Click" Visible="false" />
                    <input type="button" value="返回" class="btn btn-primary" name="back" onclick="history.back();" />
                </td>
            </tr>
        </table>
    </div>
    <div id="div_share" class="panel panel-primary" style="display: none; position: absolute; z-index: 3;">
        <div class="panel-heading">
            <span style="float: right; cursor: pointer;" onclick="hidediv()" title="关闭">关闭</span><h3 class="panel-title">选择会员</h3>
        </div>
        <div class="panel-body">
            <iframe id="userIframe" style="z-index: 2; visibility: inherit; overflow: auto; overflow-x: hidden; width: 100%; height: 300px;" name="main_right" src="/Mis/OA/Mail/SelUser.aspx?Type=Radio" frameborder="0"></iframe>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="dataField" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Controls/ZL_Array.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/Plugs/angular.min.js"></script>
    <script>
        user.hook["BindUser"] = userdeal;
        function userdeal(list, select) {
            $("#" + select + "_T").val(list[0].UserName);
            $("#" + select + "_Hid").val(list[0].UserID);
            if (comdiag != null) { CloseComDiag(); }
        }
    </script>
</asp:Content>
