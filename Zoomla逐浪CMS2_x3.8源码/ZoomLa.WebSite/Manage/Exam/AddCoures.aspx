<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCoures.aspx.cs" Inherits="manage_Question_AddCoures" ValidateRequest="false" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
    <title>添加课程</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Literal ID="liCoures" runat="server" Visible="false"></asp:Literal>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2">
                <asp:Label ID="Label1" runat="server" Text="添加课程"></asp:Label></td>
        </tr>
        <tbody id="Tabs0">
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="ssjd_txt" runat="server" Text="课程名称："></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txt_Courename" runat="server" class="form-control" Width="200px" onkeyup="Getpy('txt_Courename','PYtitle')"></asp:TextBox>
                    <span id="span_txtTitle" name="span_txtTitle"></span>
                    <font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ErrorMessage="课程名称不能为空!" ControlToValidate="txt_Courename"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="Label3" runat="server" Text="课程缩写："></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="PYtitle" runat="server" class="form-control" Width="100px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ErrorMessage="课程缩写不能为空!" ControlToValidate="PYtitle"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="Label4" runat="server" Text="课程代码："></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txt_Code" runat="server" class="form-control" Width="100px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ErrorMessage="课程代码不能为空!" ControlToValidate="txt_Code"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="Label9" runat="server" Text="课程分类："></asp:Label>
                </td>
                <td>
                    <asp:HiddenField ID="hfid" runat="server" />
                    <div class="input-group" style="width:300px;">
                        <asp:TextBox ID="txtClassname" runat='server' CssClass="form-control"></asp:TextBox>
                        <span class="input-group-btn">
                            <input id="Button1" type="button" value="选择分类" onclick="Openwin(); void (0)" class="btn btn-primary" />
                        </span>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="课程分类不能为空!" ControlToValidate="txtClassname"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="txt" runat="server" Text="课程学分："></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txt_Creidt" runat="server" class="form-control" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="Label5" runat="server" Text="是否热门："></asp:Label>

                </td>
                <td>
                    <input type="checkbox" runat="server" id="rblHot" class="switchChk"/>
                </td>
            </tr>
            <tr>
                <td style="width: 20%" align="right">
                    <asp:Label ID="Label2" runat="server" Text="课程简介："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRemark" runat="server" class="form-control" TextMode="MultiLine" Height="123px" Width="306px"></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tr>
            <td></td>
            <td>
                <asp:HiddenField ID="coureId" runat="server" />
                <div class="btn-group">
                    <asp:Button ID="EBtnSubmit" CssClass="btn btn-primary" Text="保存" runat="server" OnClick="EBtnSubmit_Click" />
                    <asp:Button ID="Button2" CssClass="btn btn-primary" Text="试听文件" runat="server" CausesValidation="false" />
                    <asp:Button ID="Button3" CssClass="btn btn-primary" Text="下一步" runat="server" CausesValidation="false" />
                    <asp:Button ID="BtnBack" CssClass="btn btn-primary" runat="server" Text="返回列表" OnClientClick="location.href='CoureseManage.aspx';return false;" UseSubmitBehavior="False" CausesValidation="False" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <script type="text/javascript" src="/JS/Dialog.js"></script>
    <script type="text/javascript" src="/JS/Common.js"></script>
    <script type="text/javascript">
        function Openwin() {
            var diag = new Dialog();
            diag.Modal = false;
            diag.Width = 400;
            diag.Height = 450;
            diag.Title = "选择分类";
            diag.URL = "../../Exam/SelecQuestionClass.aspx";
            diag.show();
        }
        function Getpy(ontxt, id) {
            var str = document.getElementById(ontxt).value.trim();
            if (str == "") return;
            var arrRslt = makePy(str);
            if (arrRslt.length > 0) {
                document.getElementById(id).value = arrRslt.toString().toLowerCase();
            }
        }
    </script>
</asp:Content>
