<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPageModel.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Page.AddPageModel" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加模型</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField runat="server" ID="LNav_Hid" Value="添加内容模型" />
     <table  class="table table-striped table-bordered table-hover">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <asp:Literal ID="LTitle" runat="server" Text="添加黄页模型"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 35%">
                <strong>黄页模型名称：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtModelName" class="form-control" runat="server" Width="156" MaxLength="200" 
                    AutoPostBack="true" onkeyup="Getpy('TxtModelName','TxtTableName')" /><font
                    color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"
                    ControlToValidate="TxtModelName">黄页模型名称不能为空</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft">
                <strong>创建的数据表名：</strong>
            </td>
            <td>
                <asp:Label ID="LblTablePrefix" runat="server" Text="ZL_Page_" />
                <asp:TextBox ID="TxtTableName" class="form-control" runat="server" Width="103px" 
                    MaxLength="50" /><font color="red">*</font>
                <asp:RegularExpressionValidator ID="ValeTableName" runat="server" ControlToValidate="TxtTableName"
                    ErrorMessage="只允许输入字母、数字或下划线" ValidationExpression="^[\w_]+$" SetFocusOnError="true"
                    Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td class="tdbgleft">
                <strong>项目名称：</strong>
                <br />
                例如：文章、软件、图片、商品
            </td>
            <td>
                <asp:TextBox ID="TxtItemName" class="form-control" runat="server" Width="156" MaxLength="20" /><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtItemName"
                    ErrorMessage="RequiredFieldValidator">项目名称不能为空</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft">
                <strong>项目单位：</strong>
                <br />
                例如：篇、个、张、件
            </td>
            <td>
                <asp:TextBox ID="TxtItemUnit" class="form-control" runat="server" Width="156" MaxLength="20" /><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtItemUnit"
                    ErrorMessage="RequiredFieldValidator">项目单位不能为空</asp:RequiredFieldValidator>
            </td>
        </tr>
         <tr class="tdbg">
             <td class="tdbgleft">
                 <strong>项目图标：</strong>
             </td>
             <td>
                 <asp:TextBox ID="TxtItemIcon" CssClass="form-control pull-left" Text="Default.gif" runat="server" Width="156" />
                 <span class="spanl_30" style="margin-right: 5px;"><span id="imgIcon"></span><=</span>
                 <button type="button" onclick="ShowIcon()" class="btn btn-primary">填写或选择奥森图标</button>
             </td>
         </tr>
        <tr>
            <td class="tdbgleft">
                <strong>模型描述：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtDescription" class="form-control" runat="server" 
                    TextMode="MultiLine" Width="365px"
                    Height="60px" />
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2" style="text-align:center">
                <asp:HiddenField ID="HdnModelId" runat="server" />
                <asp:Button ID="EBtnSubmit" Text="保存" class="btn btn-primary"  OnClick="EBtnSubmit_Click" runat="server" />
                &nbsp;&nbsp;
                <input name="Cancel" type="button" class="btn btn-primary"  id="Cancel" value="取消" onclick="window.location.href = '../Content/ModelManage.aspx?ModelType=4';" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/chinese.js" type="text/javascript"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    $("body").ready(function () {
        $("#imgIcon").attr("class", $("#TxtItemIcon").val());

    });
    function LoadIcon() {
        $("[name=glyphicon]").click(function () {
            $("#TxtItemIcon").val($(this).prev().prev().attr("class"));
            $("#imgIcon").attr("class", $(this).prev().prev().attr("class"));
            diag.CloseModal();
        });
    }
    var diag = new ZL_Dialog();
    diag.title = "填写或选择奥森图标";
    diag.ajaxurl = "/Common/icon.html";
    function ShowIcon() {
        diag.ShowModal();
    }
    function ChangeTxtItemIcon(icon) {
        document.getElementById("<%= TxtItemIcon.ClientID %>").value = icon;
}
function Getpy(ontxt, id) {
    if ('<%=Request["ModelID"] %>' == "") {
        var str = document.getElementById(ontxt).value.trim();
        if (str == "") {
            document.getElementById(id).value = "";
            return;
        }
        var arrRslt = makePy(str);
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