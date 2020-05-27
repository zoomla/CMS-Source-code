<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPaperQuestion.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.AddPaperQuestion"  EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加大题</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Literal ID="liPaper" runat="server" Visible="false"></asp:Literal>
    <table class="table table-striped table-bordered table-hover">
        <tr style="text-align:center;">
            <td colspan="2">
                <asp:Label ID="Label1" runat="server" Text="添加大题"></asp:Label></td>
        </tr>
        <tbody id="Tabs0">
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="ssjd_txt" runat="server" Text="标题："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_name" runat="server" class="form-control text_md"></asp:TextBox>
                    &nbsp;<font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ErrorMessage="标题不能为空!" ControlToValidate="txt_name"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label4" runat="server" Text="题型："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:DropDownList ID="ddType" CssClass="form-control text_xs" runat="server" Font-Size="12px"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label9" runat="server" Text="副标题："></asp:Label>
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txtSubTitle" runat='server' class="form-control text_md"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="txt" runat="server" Text="序号："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_OrderBy" runat="server" class="form-control text_s" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label3" runat="server" Text="每小题分数："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txtCourse" runat="server" class="form-control text_s"></asp:TextBox><font color="red">（说明：该分值是每种题型包含小题的分数）</font>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label5" runat="server" Text="包含题数："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txtNum" runat="server" class="form-control text_s"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label2" runat="server" Text="答题说明："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txtRemark" runat="server" class="form-control tarea_l" TextMode="MultiLine" Height="123px" Width="306px"></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tr>
            <td colspan="2">
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfpaperId" runat="server" />
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存" runat="server" OnClick="EBtnSubmit_Click" />
                <input type="button" class="btn btn-primary"  value="返回" onclick="<%="location.href='Paper_QuestionManage.aspx?pid="+Request.QueryString["pid"]+"'"%>" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Dialog.js"></script>
    <script type="text/javascript" src="/JS/Common.js"></script>
    <script type="text/javascript" src="/JS/chinese.js"></script>
</asp:Content>