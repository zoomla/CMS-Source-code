<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPageStyle.aspx.cs" Inherits="manage_Page_AddPageStyle" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/TlpDP.ascx" TagPrefix="ZL" TagName="TlpDown" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加黄页样式</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField runat="server" ID="Label1_Hid" />
    <table class="table table-striped table-bordered table-hover">
        <tbody id="Tbody1">
            <tr>
                <td class="td_m"><strong>样式名称：</strong></td>
                <td>
                    <asp:TextBox ID="styleName" class="form-control text_300" runat="server"></asp:TextBox>
                    <font color="red">*
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="styleName" ErrorMessage="样式名称不能为空!"></asp:RequiredFieldValidator>
                    </font>
                </td>
            </tr>
            <tr>
                <td><strong>样式目录：</strong></td>
                <td>
                    <asp:TextBox ID="stylePath" CssClass="form-control text_300" runat="server"></asp:TextBox>
                    <span style="color: red;">*(示例:/Template/V3/企业黄页/)
              <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="stylePath" ErrorMessage="样式路径不能为空!"></asp:RequiredFieldValidator></span></td>
            </tr>
            <tr>
                <td><strong>首页模板：</strong></td>
                <td>
                    <%=PageCommon.GetTlpDP("TemplateIndex") %>
                    <span style="color:red">*</span>
                    <asp:HiddenField ID="TemplateIndex_hid" runat="server" />
                </td>
            </tr>
            <tr>
                <td><strong>模板预览：</strong></td>
                <td>
                    <asp:TextBox ID="TempImg_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
                    <span>*请填写模板效果图以展示效果。</span> 
                </td>
            </tr>
            <tr id="Tr3">
                <td>
                    <strong>排列顺序：</strong></td>
                <td>
                    <asp:TextBox ID="orderid" class="form-control text_300" runat="server" >0</asp:TextBox> <span>*数字越大排列越前</span>
                </td>
            </tr>
            <tr id="Tr18">
                <td colspan="2" align="center">
                    <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="保存" OnClientClick="checkdata()" OnClick="Button1_Click" />
                    <a href="PageStyle.aspx" class="btn btn-primary">返回</a>
                </td>
            </tr>
        </tbody>
    </table>
    <ZL:TlpDown runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Common.js" type="text/javascript"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script>
        $().ready(function () {
            Tlp_initTemp();
        });
        function checkdata() {//检测提交数据
            if ($("#TemplateIndex_hid").val() == "") {
                alert("请选择首页模板!");
                $('form').submit(function () { return false; });
            } else {
                $('form').unbind('submit');
            }

        }
    </script>
</asp:Content>
