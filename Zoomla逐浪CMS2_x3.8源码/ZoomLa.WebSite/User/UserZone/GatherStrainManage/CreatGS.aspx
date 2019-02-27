<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="CreatGS.aspx.cs" Inherits="ZoomLa.GatherStrainManage.CreatGS" %>

<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<%@ Register Src="WebUserControlGztherLetf.ascx" TagName="WebUserControlGztherLetf" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>创建群族</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="GSManage.aspx">我的群族</a></li>
        <li class="active">创建新的群族</li>
		<div class="clearfix"></div>
    </ol>
    <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" />
    <div class="clearfix"></div>
    <uc2:WebUserControlGztherLetf ID="WebUserControlGztherLetf2" runat="server" />
    
    <div class="us_topinfo" style="margin-top: 10px;">
        <img src="../Images/ico_qun.gif" />创建新的群族
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2" class="text-center"><strong>填写群名称等信息：</strong></td>
            </tr>
            <tr>
                <td style="width: 153px" align="right">群族名称：<span style="color: #d01e3b">*</span></td>
                <td>
                    <asp:TextBox ID="txtGSName" CssClass="form-control" runat="server" Width="218px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGSName" Display="Dynamic" ErrorMessage="请填写群族名称" Font-Size="10pt"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 153px" align="right" valign="top">群族介绍：<span style="color: #d01e3b">*</span></td>
                <td>
                    <asp:TextBox ID="txtGSInfo" CssClass="form-control" runat="server" Height="107px" TextMode="MultiLine" Width="215px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGSInfo" Display="Dynamic" ErrorMessage="请填写群族介绍" Font-Size="10pt"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 153px" align="right">群族分类：</td>
                <td>
                    <asp:DropDownList ID="dropGSType" CssClass="form-control" Width="150" runat="server" DataTextField="GSTypeName" DataValueField="ID"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 153px" align="right">群族图标：</td>
                <td>
                    <asp:TextBox ID="txtpic" CssClass="form-control" runat="server" Width="300px" /><br />
                    <iframe id="Clearimgs" style="top: 2px" src="../../FileUpload.aspx?menu=txtpic" width="400px" height="25px" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"></iframe>
                </td>
            </tr>
            <tr>
                <td style="width: 153px"></td>
                <td>
                    <asp:CheckBox ID="checkboxCryptonym" runat="server" Font-Size="10pt" Text="支持匿名发表和回复话题" /></td>
            </tr>
            <tr>
                <td style="width: 153px"></td>
                <td><asp:Button ID="btnOK" CssClass="btn btn-primary" runat="server" OnClick="btnOK_Click" Text="确定" /></td>
            </tr>
        </table>
    </div>
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
