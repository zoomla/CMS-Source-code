<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddLecturer.aspx.cs" Inherits="manage_Question_AddLecturer" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>讲师管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Label runat="server" ID="liCoures" Visible="false"></asp:Label>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2"></td>
        </tr>
        <tbody id="Tabs0">
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">名称： &nbsp;</td>
                <td class="bqright">
                    <asp:TextBox ID="txt_name" runat="server" class="l_input" Width="200px"></asp:TextBox>
                    &nbsp;<font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ErrorMessage="教师名称不能为空!" ControlToValidate="txt_name"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">部门：</td>
                <td class="bqright">
                    <asp:HiddenField ID="hfid" runat="server" />
                    <asp:TextBox ID="txtClassname" runat='server' class="l_input"></asp:TextBox>
                    <input id="Button1" type="button" value="选择部门" onclick="Openwin(); void (0)" class="btn btn-primary" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ErrorMessage="部门不能为空!" ControlToValidate="txtClassname"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label5" runat="server" Text="类别："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <asp:DropDownList ID="ddType" runat="server">
                        <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                        <asp:ListItem Value="内训">内训</asp:ListItem>
                        <asp:ListItem Value="外请">外请</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label6" runat="server" Text="性别："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <asp:DropDownList ID="ddSex" runat="server">
                        <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                        <asp:ListItem Value="0">男</asp:ListItem>
                        <asp:ListItem Value="1">女</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label7" runat="server" Text="级别："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <asp:DropDownList ID="ddLevel" runat="server">
                        <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                        <asp:ListItem Value="一级">一级</asp:ListItem>
                        <asp:ListItem Value="二级">二级</asp:ListItem>
                        <asp:ListItem Value="三级">三级</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label8" runat="server" Text="职称："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <asp:DropDownList ID="ddTitle" runat="server">
                        <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                        <asp:ListItem Value="初级">初级</asp:ListItem>
                        <asp:ListItem Value="中级">中级</asp:ListItem>
                        <asp:ListItem Value="副高">副高</asp:ListItem>
                        <asp:ListItem Value="正高">正高</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label9" runat="server" Text="专业类别："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <asp:DropDownList ID="ddProfessional" runat="server">
                        <asp:ListItem Selected="True" Value="0">请选择</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label3" runat="server" Text="联系电话："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Phone" runat="server" class="l_input" Width="150px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ErrorMessage="联系电话不能为空!" ControlToValidate="txt_Phone"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label4" runat="server" Text="推荐讲师："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:DropDownList ID="ddTechRecom" runat="server">
                        <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                        <asp:ListItem Value="0">否</asp:ListItem>
                        <asp:ListItem Value="1">是</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label10" runat="server" Text="人气："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Popularity" runat="server" class="l_input" Width="100px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13"
                        runat="server" ControlToValidate="txt_Popularity" ErrorMessage="人气格式不对!" ValidationExpression="\d+[.]?\d*"
                        Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label11" runat="server" Text="特长："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <textarea runat="server" id="txt_TechSpecialty" style="width: 80%; height: 80px;"></textarea>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label12" runat="server" Text="爱好："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <textarea runat="server" id="txt_TechHobby" style="width: 80%; height: 80px;"></textarea>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label13" runat="server" Text="获奖情况："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <textarea runat="server" id="txt_Awardsinfo" style="width: 80%; height: 80px;"></textarea>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label2" runat="server" Text="详细情况："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <textarea runat="server" id="txt_TechIntrodu" style="width: 80%; height: 80px;"></textarea>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="txt" runat="server" Text="请选择要上传的图片："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <ZL:SFileUp ID="SFile_Up" runat="server" FType="Img" />
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
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Dialog.js"></script>
    <script type="text/javascript">
        function Openwin() {
            var diag = new Dialog();
            diag.Modal = false;
            diag.Width = 400;
            diag.Height = 450;
            diag.Title = "选择分类";
            diag.URL = "SelecQuestionClass.aspx";
            diag.show();
        }
    </script>
</asp:Content>