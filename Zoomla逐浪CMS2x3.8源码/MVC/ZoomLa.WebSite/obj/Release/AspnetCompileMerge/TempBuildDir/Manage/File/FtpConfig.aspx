<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FtpConfig.aspx.cs" Inherits="ZoomLaCMS.Manage.FtpFile.FtpConfig" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>远程文件配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td class="spacingtitle" colspan="2" style="text-align: center;">
                    <strong>
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </strong>
                </td>
            </tr>
            <tr>
                <td align="left">远程服务器名：
                </td>
                <td>
                    <input id="Alias" runat="server" class="form-control text_300" onblur="CheckName()" />&nbsp;&nbsp;*不可重名
                    <span id="checkName" style="color: red; display: none;">重名了</span>
                </td>
            </tr>
            <tr>
                <td align="left">IP地址：
                </td>
                <td>
                    <asp:TextBox ID="txt_fs" runat="server" CssClass="form-control text_300" Text="u.z01.com"></asp:TextBox>&nbsp;&nbsp;可以是别名，如u.z01.com
                </td>
            </tr>
            <tr>
                <td align="left">访问路径：
                </td>
                <td>
                    <asp:TextBox ID="txt_url" runat="server" CssClass="form-control text_300" Text="u.z01.com"></asp:TextBox>
                    &nbsp;&nbsp;<span class="red"><a href="http://www.z01.com/u.html" target="_blank">免费申请云存储</a></span>
                </td>
            </tr>
            <tr>
                <td align="left">端口：
                </td>
                <td>
                    <asp:TextBox ID="txt_pt" runat="server" CssClass="form-control text_300" Text="21"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_pt" ErrorMessage="请输入端口号默认21" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">用 户 名：
                </td>
                <td>
                    <asp:TextBox ID="txt_user" runat="server" CssClass="form-control text_300"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_user" ErrorMessage="请输入用户名" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 码：
                </td>
                <td>
                    <asp:TextBox ID="txt_pass" runat="server" CssClass="form-control text_300"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_pass" ErrorMessage="请输入密码" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">存储路径：&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txt_file" runat="server" CssClass="form-control text_300" Text="/"></asp:TextBox>
                    &nbsp;&nbsp;必须为/或/path形式
                    <ZL:FileUpload ID="file_path" runat="server" CssClass="form-control text_300" Visible="false" />
                    <%--<asp:FileUpload ID="file_path" runat="server" CssClass="form-control text_300" Visible="false" />--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_file" ErrorMessage="请输入远程文件路径" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="text-center">
                    <asp:Button ID="Save_Btn" runat="server" Text="添加配置" CssClass="btn btn-primary" OnClick="Save_Btn_Click" />
                    <input id="Button1" type="button" value="返回列表" class="btn btn-primary" onclick="javascript: history.back();" />
                </td>
            </tr>
        </table>

    </div>
    <div style="display: none">
        <input type="text" id="FTPID" value="0" runat="server" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function CheckName() {
            $.ajax({
                url: "FtpConfig.aspx",
                data: "action=CheckName&Alias=" + $("#Alias").val() + "&ID=" + $("#FTPID").val(),
                success: function (msg) {
                    switch (msg) {
                        case "1": $("#checkName").css("display", "");
                            $("#Button2").attr("disabled", "disabled");
                            break;
                        case "0": $("#checkName").css("display", "none");
                            $("#Button2").attr("disabled", "");
                            break;
                    }
                }
            });
        }
    </script>
</asp:Content>

