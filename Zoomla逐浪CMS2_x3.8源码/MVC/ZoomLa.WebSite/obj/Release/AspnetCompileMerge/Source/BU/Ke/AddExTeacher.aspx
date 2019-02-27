<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddExTeacher.aspx.cs" Inherits="ZoomLaCMS.MIS.Ke.AddExTeacher"  MasterPageFile="~/Common/Master/User.Master" ValidateRequest="false"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
    <title>编辑教师</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
    <asp:Literal ID="liCoures" runat="server" Visible="false"></asp:Literal>
    <div class="container margin_t5">
          <ol class="breadcrumb">
        <li><a href="/User/">用户中心</a></li>
        <li><a href="ExTeacherManage.aspx">教师管理</a></li>
        <li class="active">编辑教师</li>
        </ol>
        </div>
<div class="container btn_green">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2">
                <asp:Label ID="Label1" runat="server" Text="编辑教师" /></td>
        </tr>
        <tbody id="Tabs0">
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">教师名称： &nbsp;</td>
                <td class="bqright">
                    <asp:TextBox ID="txt_name" runat="server" class="form-control text_md" ></asp:TextBox>
                    &nbsp;<font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ErrorMessage="教师名称不能为空!" ControlToValidate="txt_name"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">教师分类：</td>
                <td class="bqright">
                    <asp:DropDownList ID="PaperType_Drop" CssClass="form-control text_md" runat="server" DataTextField="TypeName" DataValueField="ID"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label3" runat="server" Text="教师职位："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Post" runat="server" class="form-control text_md" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ErrorMessage="教师职位不能为空!" ControlToValidate="txt_Post"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label4" runat="server" Text="教师授课："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Teach" runat="server" class="form-control text_md" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ErrorMessage="教师授课不能为空!" ControlToValidate="txt_Teach"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="text-center"><asp:Label ID="Label2" runat="server" Text="教师信息："></asp:Label></td>
                <td>
                    <asp:TextBox runat="server" ID="textarea1" TextMode="MultiLine" style="width:700px;height:200px;"></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:HiddenField ID="hftid" runat="server" />
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存" runat="server" OnClick="EBtnSubmit_Click" />
                &nbsp;
                <asp:Button ID="BtnBack" class="btn btn-primary" runat="server" Text="返回列表" OnClientClick="location.href='ExTeacherManage.aspx';return false;" UseSubmitBehavior="False" CausesValidation="False" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Dialog.js"></script>
    <%=Call.GetUEditor("textarea1",2) %>
    <script type="text/javascript">
        
    </script>
</asp:Content>