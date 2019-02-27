<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="Trademark.aspx.cs" Inherits="Zoomla.Website.manage.Shop.Trademark" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加品牌</title>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td colspan="2" class="spacingtitle">品牌参数设置</td>
        </tr>
        <tr>
            <td style="width: 20%"><strong>品牌名称：</strong></td>
            <td width="66%" valign="middle">
                <asp:TextBox ID="Trname" runat="server" class="form-control text_300"></asp:TextBox>
                <asp:HiddenField ID="ID_H" runat="server" />
                <asp:HiddenField ID="uptype" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 20%;"><strong>所属厂商：</strong></td>
            <td valign="middle" style="height: 22px">
                <asp:DropDownList ID="Producer" runat="server" CssClass="form-control text_300">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 20%;"><strong>是否推荐：</strong> </td>
            <td valign="middle" style="height: 22px">
                <asp:CheckBox ID="Isbest" runat="server" Text="推荐" /></td>
        </tr>
        <tr>
            <td style="width: 20%"><strong>品牌分类：</strong></td>
            <td valign="middle">
                <asp:RadioButtonList ID="TrClass" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="大陆品牌">大陆品牌</asp:ListItem>
                    <asp:ListItem Value="港台品牌">港台品牌</asp:ListItem>
                    <asp:ListItem Value="日韩品牌">日韩品牌</asp:ListItem>
                    <asp:ListItem Value="欧美品牌">欧美品牌</asp:ListItem>
                    <asp:ListItem Value="其他品牌">其他品牌</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="width: 20%"><strong>品牌照片：</strong></td>
            <td valign="middle">
                <asp:TextBox ID="TrPhoto" runat="server" CssClass="form-control text_300"></asp:TextBox><iframe id="proimgs" style="top: 2px" src="../../Shop/fileupload.aspx?menu=TrPhoto" width="100%" height="25px" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"></iframe>
            </td>
        </tr>
        <tr class="WebPart">
            <td style="width: 20%"><strong>品牌简介：</strong></td>
            <td valign="middle">
                <textarea cols="80" id="TrContent" name="TrContent" style="width: 99%; height: 200px;" rows="20" runat="server"></textarea>
                <%=Call.GetUEditor("TrContent",4) %>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="height: 49px">
                <asp:Button ID="Button1" runat="server" Text="保存设置" class="btn btn-primary" OnClick="Button1_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function GV(id, value) {
            $("#" + id).val(value);
        }
    </script>
</asp:Content>
