<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZoneApply.aspx.cs" Inherits="User_UserZone_ZoneApply" MasterPageFile="~/User/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>我的空间</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <link href="../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
    <script src='<%=ResolveUrl("~/JS/DatePicker/WdatePicker.js")%>' type="text/javascript"></script>
    <div style="margin:auto; width:100%">
<div class="us_topinfo">
<div class="us_showinfo">
<a title="会员中心" href="/User/Default.aspx" target="_parent">会员中心</a>&gt;&gt; <a title="我的空间" href="/User/Userzone/Default.aspx">我的空间</a>
</div>

<div class="us_topinfo" style="margin-top: 10px;">申请开通空间</div>
<div class="us_topinfo" style="margin-top: 10px;">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td valign="top">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="width: 80px">
                            空间名称：</td>
                        <td style="width: 341px">
                            <asp:TextBox ID="Nametxt" runat="server" Text='' Width="35%" MaxLength="30"></asp:TextBox>
                            <span><font color="red">*</font></span>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="Nametxt">名称必填</asp:RequiredFieldValidator></td>
                                    <td rowspan="4" valign="top">
                                     <asp:Image ID="Image1" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="width: 80px">
                            性别：</td>
                        <td style="width: 341px">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="true" Selected="True">男</asp:ListItem>
                                <asp:ListItem Value="false">女</asp:ListItem>
                            </asp:RadioButtonList></td> 
                    </tr>
                    <tr>
                        <td style="width: 80px">
                            生日：</td>
                        <td style="width: 341px">
                            <asp:TextBox ID="txtbir" runat="server" Width="140px" OnFocus="setday(this)" MaxLength="10"></asp:TextBox>
                            如：2008-08-08<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtbir" ErrorMessage="请输入生日日期"></asp:RequiredFieldValidator><br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtbir"
                                ErrorMessage="请输入出生年月日" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 80px">
                            血型：</td>
                        <td style="width: 341px">
                            <asp:DropDownList ID="ddlBlood" runat="server">
                            </asp:DropDownList></td> 
                    </tr>
                    <tr>
                        <td style="width: 80px; height: 25px;">
                            现居住地：</td>
                        <td style="width: 341px; height: 25px;">
                            <asp:DropDownList ID="ddlProvince1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince1_SelectedIndexChanged">
                            </asp:DropDownList><asp:DropDownList ID="ddlCity1" runat="server">
                            </asp:DropDownList></td>
                            <td style="height: 25px">
                                <ZL:FileUpload runat="server" ID="UpImg" />
                                <%--<input id="UpImg" type="file" runat="server" />--%>
                                <asp:Button ID="Button2" runat="server" Text="上传" OnClick="Button2_Click" CausesValidation="False" /></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 80px">
                            空间简介：
                        </td>
                        <td style="width: 341px">
                            <textarea id="textareacontent" style="width: 294px; height: 138px" runat="server"></textarea></td>
                   <td>
                       &nbsp;<asp:HiddenField ID="HiddenField1" runat="server" />
                   </td>
                    </tr>
                    <tr>
                        <td style="width: 80px">
                        </td>
                        <td style="width: 341px">
                            <asp:Button ID="EBtnSubmit" Text="信息提交" runat="server" OnClick="EBtnSubmit_Click" />
                            &nbsp;
                            <input id="Button1" type="button" value="返  回" onclick="javascript: history.go(-1)" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div> 
</asp:Content>
