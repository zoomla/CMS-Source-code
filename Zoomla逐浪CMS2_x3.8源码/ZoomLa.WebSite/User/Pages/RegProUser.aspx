<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="RegProUser.aspx.cs" Inherits="User_Pages_RegProUser" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>注册企业黄页</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="Default.aspx">企业黄页</a></li>
        <li class="active">注册企业黄页</li>
    </ol>
    <div class="us_seta" style="margin-top: 10px;">
        <asp:Label ID="Label1" runat="server" Text="● 由于您未申请和提交黄页信息,下面我们来花三分钟来注册企业黄页"></asp:Label>
    </div>
    <div class="us_seta" style="margin-top: 10px; float: left;" id="regpage" runat="server">
        <h1 style="text-align: center"></h1>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2" class="text-center">
                    <asp:Label ID="Label2" runat="server" Text="注册企业黄页"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%; text-align: right;">会员帐号:</td>
                <td style="width: 70%; padding-left: 5px; text-align: left;">
                    <asp:Label ID="Username" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%; text-align: right;">黄页类型:</td>
                <td style="width: 70%; padding-left: 5px; text-align: left;">
                    <asp:RadioButtonList ID="Styleids" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                        Height="24px" RepeatColumns="4" Style="margin-top: 0px; padding-top: 0px; line-height: 30px">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="width: 30%; text-align: right;">企业名称:</td>
                <td style="width: 70%; padding-left: 5px; text-align: left;">
                    <asp:TextBox ID="Proname" runat="server" CssClass=" form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 35%; text-align: right;">二级域名:
                </td>
                <td style="line-height: 30px; height: 30px; padding-left: 5px; text-align: left;">
                    <asp:TextBox ID="SeDomain" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 35%; text-align: right;">企业LOGO:
                </td>
                <td style="line-height: 30px; height: 30px; padding-left: 5px; text-align: left;">
                    <table border='0' cellpadding='0' cellspacing='1' width='100%'>
                        <tr class='tdbg'>
                            <td>
                                <asp:TextBox ID="txt_logos" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class='tdbg'>
                            <td>
                                <iframe id="Iframe1" src="/Common/UserUpload.aspx?FieldName=txt_logos" marginheight="0"
                                    marginwidth="0" frameborder="0" width="100%" height="30" scrolling="no"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 35%; text-align: right;">企业说明:
                </td>
                <td style="line-height: 30px; height: 30px; padding-left: 5px; text-align: left;">
                    <asp:TextBox ID="PageInfo" runat="server" Height="176px" TextMode="MultiLine" CssClass="form-control" Style="max-width: 400px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 35%; text-align: right;">快速栏目样式分配：
                </td>
                <td>
                    <asp:DropDownList ID="Styleid" CssClass="form-control" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
            <tr>
                <td colspan="2" class="text-center">
                    <asp:HiddenField ID="HdnNode" runat="server" />
                    <asp:HiddenField ID="HdnID" runat="server" />
                    <asp:HiddenField ID="HdnModel" runat="server" />
                    <asp:Button ID="BtnSubmit" runat="server" Text="提交我的黄页信息" CssClass="btn btn-primary" OnClick="BtnSubmit_Click" />
                    <input id="Button1" type="button" value="取消" onclick="location.href = '../MyInfo.aspx';" class="btn btn-primary" />
                </td>
            </tr>
        </table>
    </div>
    <div class="us_seta" style="margin-top: 10px; line-height: 28px; padding-left: 20px;"
        id="Auditing" runat="server">
        正在审核中......! 请等待管理员开通您的黄页功能! <a href=""></a>
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">修改我提交的企业信息!</asp:LinkButton>
        <br />
        <br />
        <br />
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Common.js"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        function SelectValues(ModelID, id) {
            window.open('/Common/OptionManage.aspx?id=' + id + '&ModelID=' + ModelID, '', 'width=600,height=450,resizable=0,scrollbars=yes');
        }
    </script>
</asp:Content>
