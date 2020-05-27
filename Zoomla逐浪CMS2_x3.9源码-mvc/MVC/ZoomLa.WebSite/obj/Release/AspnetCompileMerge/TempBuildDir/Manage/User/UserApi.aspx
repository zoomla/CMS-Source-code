<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserApi.aspx.cs" Inherits="ZoomLaCMS.Manage.User.UserApi" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="/dist/css/bootstrap-switch.min.css" />
    <title>整合配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered" style="display: none;">
        <tr class="tdbg">
            <td class="tdbgleft" colspan="2" align="center">
                <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" OnClick="btnSubmit_Click" Text="保存" />
                <asp:Button ID="btnCancel" class="btn btn-primary" runat="server" Text="取 消" OnClick="btnCancel_Click" /></td>
        </tr>
    </table>
    <div id="remind" runat="server"></div>
    <table class="table table-bordered">
        <tr>
            <td class="td_l tdleft"><strong>主站数据库服务器IP：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="ServerIP_T" Text="127.0.0.1" CssClass="form-control required ip text_md" data-enter="1" /></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>数据库名称：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="DBName_T" CssClass="form-control required text_md" data-enter="2" /></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>数据库用户：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="UName_T" CssClass="form-control required text_md" data-enter="3" /></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>数据库密码：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="Pwd_T" data-enter="4" CssClass="form-control required text_md" /></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>服务器Token：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="Token_T" CssClass="form-control text_md" data-enter="5" /></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>是否子站：</strong></td>
            <td>
                <input type="checkbox" runat="server" id="RemoteEnable" class="switchChk" /></td>
        </tr>
        <%--  <tr><td>启用Discuz!NT论坛：</td><td><input type="checkbox" runat="server" id="RBLDZ" class="switchChk" /></td></tr>--%>
        <tr>
            <td class="tdleft"><strong>操作：</strong></td>
            <td>
                <asp:Button runat="server" ID="Begin_Btn" CssClass="btn btn-primary" Text="生成整合SQL" OnClientClick="CheckData();" OnClick="Begin_Btn_Click" data-enter="6" />
                <asp:Button runat="server" ID="Cancel_Btn" CssClass="btn btn-primary" Text="生成取消SQL" OnClick="Cancel_Btn_Click" />
                <a href="http://code.z01.com/Files/跨站整合示例.docx" target="_blank" class="btn btn-primary">下载示例文档</a>
                <asp:Button runat="server" ID="Save_Btn" Text="保存配置" OnClick="Save_Btn_Click" CssClass="btn btn-primary" />
            </td>
        </tr>
    </table>
    <input type="button" id="copyhref" class="btn btn-primary opition" value="复制保存下面语句">
    <abbr style="margin-left: 10px;">在数据库管理器或后台SQL查询界面中按说明执行下列脚本：</abbr>
    <div style="height: 38px;"></div>
    <div runat="server" id="Sql_Div" class="alert alert-info">尚未生成SQL语句</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .modal_ifr {
            min-height: 650px;
        }
    </style>
    <script src="/dist/js/bootstrap-switch.js"></script>
    <script src="/JS/Controls/Control.js"></script>
    <script src="/JS/jquery.validate.min.js"></script>
    <script src="/JS/jquery.zclip.min.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script>
        var diag = new ZL_Dialog();
        $(function () {
            Control.EnableEnter();
            $('#copyhref').zclip({
                path: '/JS/ZeroClipboard.swf',
                copy: function () {
                    var str = $('#Sql_Div').html();
                    console.log(str);
                    str = str.replace(/<br>/g, "\n");
                    return str;
                },
                afterCopy: function () { alert("复制完成"); }
            });
        })
        function CheckData() {
            var vaild = $("form").validate({ meta: "validate" });
            return vaild.form();
        }
        function ShowConfig() {
            diag.title = "站群用户整合";
            diag.url = "UApiConfig.aspx";
            diag.backdrop = true;
            diag.maxbtn = false;
            diag.ShowModal();
        }
        function CloseDiag() {
            diag.CloseModal();
            location = location;
        }
    </script>
</asp:Content>
