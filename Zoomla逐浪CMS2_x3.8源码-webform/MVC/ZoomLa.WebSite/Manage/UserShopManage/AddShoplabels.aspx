<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddShoplabels.aspx.cs" Inherits="ZoomLaCMS.Manage.UserShopMannger.AddShoplabels" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加商铺标签</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="text-center" colspan="2">
                <asp:Label ID="Label1" runat="server" Text="添加标签"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%; text-align: left;"><b>标签名称</b></td>
            <td style="width: 80%; text-align: left;">
                <asp:Label ID="Label4" runat="server"></asp:Label>
                <asp:TextBox ID="LableName" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:TextBox ID="Lablevalue" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:Label ID="Label5" runat="server"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LableName"  ErrorMessage="标签名称不能为空!"></asp:RequiredFieldValidator></td>
        </tr>
        <tr runat="server" id="Derivetr">
            <td style="width: 20%; text-align: left;"><b>派生标签</b></td>
            <td style="width: 80%; text-align: left;">
                <asp:TextBox ID="Derive" runat="server" CssClass="form-control"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 20%; text-align: left;"><b>标签说明</b></td>
            <td style="width: 80%; text-align: left;">
                <asp:TextBox ID="LableInfo" runat="server" CssClass="form-control"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 20%; text-align: left;"><b>标签分类</b></td>
            <td style="width: 80%; text-align: left;">
                <asp:DropDownList ID="LableClasslist" CssClass="form-control" Width="200" runat="server" onchange="showlable(this);"></asp:DropDownList>
                <asp:TextBox ID="LableClass" CssClass="form-control" runat="server" Style="display: none"></asp:TextBox></td>
        </tr>
        <tr style="display: none">
            <td style="width: 20%; text-align: left;"><b>标签类型</b></td>
            <td style="width: 80%; text-align: left;">
                <asp:RadioButtonList ID="LableType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">系统标签</asp:ListItem>
                    <asp:ListItem Selected="True" Value="0">用户标签</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="width: 20%; text-align: left;" style="height: 24px"><b>标签状态</b></td>
            <td style="width: 80%; text-align: left;" style="height: 24px">
                <asp:RadioButtonList ID="IsTrue" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="1">启用</asp:ListItem>
                    <asp:ListItem Value="0">停用</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr id="Tr2" runat="server">
            <td style="width: 20%; text-align: left;"><b>序号初始计算值</b></td>
            <td style="width: 80%; text-align: left;">
                <asp:TextBox ID="Initial" CssClass="form-control" runat="server">1</asp:TextBox></td>
        </tr>
        <tr id="Tr4" runat="server">
            <td style="width: 20%; text-align: left;"><b>参数说明</b></td>
            <td style="width: 80%; text-align: left;">
                <asp:TextBox ID="Fildsinfo" CssClass="form-control" runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr id="Tr1" runat="server">
            <td style="width: 20%; text-align: left;"><b>标签间隔</b></td>
            <td style="width: 80%; text-align: left;">
                <asp:TextBox ID="Separator" CssClass="form-control" runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr id="lbcnt" runat="server">
            <td style="width: 20%; text-align: left;"><b>标签内容</b></td>
            <td style="width: 80%; text-align: left;">
                <font color="red">返回此输入框的内容，结果为</font><font color="blue"><b>真</b></font><font color="red">时显示内容
                  {$p} 为重复记数序号</font>
                <br />
                <asp:TextBox ID="LableContent" CssClass="form-control" runat="server" Height="200px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>

        <tr id="Tr3" runat="server">
            <td style="width: 20%; text-align: left;"><b>标签内容</b></td>
            <td style="width: 80%; text-align: left;">
                <font color="red">返回此输入框的内容，结果为</font><font color="blue"><b>假</b></font><font color="red">时显示内容</font>
                <br />
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Height="200px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="100%" align="center" colspan="2">
                <asp:HiddenField ID="sid" runat="server" />
                <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" OnClick="Button2_Click" Text="添加派生" />
                <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="保存" OnClick="Button1_Click" />
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function showlable(cc) {
            var selecttext = cc.options[cc.selectedIndex].text;
            if (selecttext == '>>输入标签') {
                document.getElementById("LableClass").style.display = "";
            } else {
                document.getElementById("LableClass").style.display = "none";
            }
            document.getElementById("LableClass").value = cc.options[cc.selectedIndex].value;
        }
    </script>
</asp:Content>
