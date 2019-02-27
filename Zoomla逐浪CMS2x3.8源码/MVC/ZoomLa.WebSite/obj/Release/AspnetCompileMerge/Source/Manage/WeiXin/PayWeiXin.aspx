<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayWeiXin.aspx.cs" Inherits="ZoomLaCMS.Manage.WeiXin.PayWeiXin"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>微信支付</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
     <table class="table table-striped table-bordered table-hover">
         <tr>
             <td class="td_m">APPID:</td>
             <td>
                 <asp:TextBox ID="AppID_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="AppID_T" ErrorMessage="AppID不能为空" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
             </td>
         </tr>
         <tr>
             <td>Secret:</td>
             <td>
                 <asp:TextBox ID="Secret_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="Secret_T" ErrorMessage="Secret不能为空" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
             </td>
         </tr>
         <tr>
             <td>商户号:</td>
             <td>
                 <asp:TextBox ID="MchID_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
                 <asp:RegularExpressionValidator ValidationExpression="\d+" ID="RequiredFieldValidator5" runat="server" ControlToValidate="MchID_T" SetFocusOnError="true" ErrorMessage="商户号必须为数字"
                        ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td>商户密钥:</td>
             <td>
                 <asp:TextBox ID="Key_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="Key_T" ErrorMessage="Key不能为空" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
             </td>
         </tr>
         <%--<tr>
             <td>证书路径:</td>
             <td>
                 <asp:TextBox ID="Cert_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
                 <span style="color:green;">*高级功能需要填写证书路径</span>
             </td>
         </tr>
         <tr>
             <td>证书密码:</td>
             <td>
                 <asp:TextBox ID="CertPWD_T" runat="server" TextMode="Password" CssClass="form-control text_300"></asp:TextBox>
                 <span style="color:green;">*高级功能需要填写证书密码</span>
             </td>
         </tr>--%>
         <tr><td></td><td>
                <asp:Button ID="Save_B" CssClass="btn btn-primary" OnClick="Save_B_Click" runat="server" Text="保存"></asp:Button>
                <button type="button" onclick="ReClear()" class="btn btn-primary">重置</button>
             </td>
         </tr>
     </table>
    <script>
        function ReClear() {
            $('.text_300').val('');
        }
    </script>
</asp:Content>

