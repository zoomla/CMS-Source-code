<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="AppearHuaTee.aspx.cs" Inherits="ZoomLa.GatherStrainManage.AppearHuaTee" %>

<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<%@ Register Src="WebUserControlGztherLetf.ascx" TagName="WebUserControlGztherLetf" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>创建群族</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="GSManage.aspx">我的群族</a></li>
        <li><a href="GSBuid.aspx?GSID=<%=GSID %>&where=5"><asp:Label ID="labGSName" runat="server"></asp:Label></a></li>
        <li class="active">话题列表</li>
    </ol>
    <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" /><br />
    <uc2:WebUserControlGztherLetf ID="WebUserControlGztherLetf2" runat="server" />
    <div>
        <a href="GSBuid.aspx?GSID=<%=GSID %>&where=5"><asp:Image ID="imgGSICQ" runat="server" Width="150px" Height="100px" /></a>
    </div>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" class="text-center"><strong>发表新话题</strong></td>
        </tr>
        <tr>
            <td align="right">标题：<span style="color: #d01e3b">*</span></td>
            <td >
                <asp:TextBox ID="txtHuaTeeTitle" CssClass="form-control" style="max-width:360px;" runat="server" MaxLength="25"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHuaTeeTitle" ErrorMessage="请填写话题标题" Font-Size="10pt"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">内容：<span style="color: #d01e3b">*</span></td>
            <td >
                <textarea cols="40" rows="5" class="form-control" style="max-width:300px;" id="FreeTextBox1" runat="server"></textarea>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FreeTextBox1" ErrorMessage="请填写话题内容" Font-Size="10pt"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnOK" CssClass="btn btn-primary" runat="server" Text="保存" OnClick="btnOK_Click" />
                <asp:Button ID="btnCal" CssClass="btn btn-primary" runat="server" Text="取消" CausesValidation="False" OnClientClick="return confirm('放弃编辑？');" OnClick="btnCal_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
    </script>
</asp:Content>
