<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserModel.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Page.UserModel" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加模型</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Literal ID="LNav" runat="server" Text="添加申请模型" Visible="false"></asp:Literal>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <asp:Literal ID="LTitle" runat="server" Text="添加申请模型"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td  class="td_l">
                <strong>申请模型名称：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtModelName" CssClass="form-control" runat="server" Width="168px" MaxLength="200" AutoPostBack="true" onkeyup="Getpy('TxtModelName','TxtTableName')" /><span style="color:red;">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TxtModelName">申请模型名称不能为空</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <strong>创建的数据表名：</strong>
            </td>
            <td>
                <asp:Label ID="LblTablePrefix" runat="server" Text="ZL_Reg_" />
                <asp:TextBox ID="TxtTableName" CssClass="form-control" runat="server" Width="120" MaxLength="50" AutoPostBack="True" /><span style="color:red;">*</span>
                <asp:RegularExpressionValidator ID="ValeTableName" runat="server" ControlToValidate="TxtTableName" ErrorMessage="只允许输入字母、数字或下划线" ValidationExpression="^[\w_]+$" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td>
                <strong>模型描述：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtDescription" CssClass="form-control" runat="server" TextMode="MultiLine" Width="365px" Height="60px" />
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:HiddenField ID="HdnModelId" runat="server" />
                <asp:Button ID="EBtnSubmit" CssClass="btn btn-primary" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />
                <input name="Cancel" type="button" class="btn btn-primary" id="Cancel" value="取消" onclick="location.href = 'UserModelManage.aspx'" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
 <script type="text/javascript" src="/js/Common.js"></script>
<script type="text/javascript" src="/JS/chinese.js"></script>
    <script type="text/JavaScript">
        function Getpy(ontxt, id) {
            if (true) {
                var str = document.getElementById(ontxt).value.trim();
                if (str == "") {
                    document.getElementById(id).value = "";
                    return;
                }
                var arrRslt = makePy(str);//这里将汉字转为拼音
                if (arrRslt.length > 0) {
                    document.getElementById(id).value = arrRslt.toString().toLowerCase();
                    if (document.getElementById(id).value.indexOf(',') > -1) {//判断栏目名称有多音字后栏目标识名“，”并去掉逗号后面的数据
                        var str = document.getElementById(id).value;
                        document.getElementById(id).value = str.split(',', 1);
                    }
                }
            }
        }
</script>
</asp:Content>