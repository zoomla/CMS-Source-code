<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateExamPoint.aspx.cs" Inherits="manage_Exam_UpdateExamPoint" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>添加组别</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table  class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td class="spacingtitle">
                <asp:Label ID="Label1" runat="server" Text="修改考点"></asp:Label>
            </td>
        </tr>
    </table>
    <table  class="table table-striped table-bordered table-hover">
        <tbody id="Tabs0">
<%--            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label14" runat="server" Text="上级考点："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                <asp:DropDownList ID="ddpoint" runat="server" ></asp:DropDownList>
                </td>
            </tr>--%>
                  <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label5" runat="server" Text="添加时间："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txtCoureTime" runat="server" class="form-control text_md"   onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label15" runat="server" Text="排序："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_End" runat="server" class="form-control text_md" ></asp:TextBox>
                </td>
            </tr>
          
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="修改考点" runat="server" onclick="EBtnSubmit_Click" />
                &nbsp;
                <asp:Button ID="BtnBack" class="btn btn-primary" runat="server" Text="返回列表" 
                    UseSubmitBehavior="False"  CausesValidation="False" onclick="BtnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript"src="/JS/Common.js"></script>
    <script type="text/javascript" src="/JS/Dialog.js"></script>  
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>