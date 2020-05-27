<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Modifyinfo.aspx.cs" Inherits="User_Pages_Modifyinfo" ClientIDMode="Static" ValidateRequest="false" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>注册企业黄页</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="page" data-ban="page"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="Default.aspx">企业黄页</a></li>
        <li class="active">配置黄页</li> 
    </ol>
</div>
    <div class="container">
        <div class="us_seta">
            <table class="table table-bordered table-hover table-striped">
                <tr>
                    <td colspan="2" class="text-center">配置信息设置</td>
                </tr>
                <tr>
                    <td>黄页标题： </td>
                    <td>
                        <asp:TextBox ID="Title" runat="server" class="form-control text_md" />
                    </td>
                </tr>
                <tr>
                    <td>菜单高度： </td>
                    <td>
                        <asp:TextBox ID="HeadHeight" runat="server" class="form-control text_md" />px
                    </td>
                </tr>
                <tr>
                    <td>栏目背景图片： </td>
                    <td>
                        <table border='0' cellpadding='0' cellspacing='1' width='100%'>
                            <tr>
                                <td>
                                    <asp:TextBox ID="HeadBackGround" CssClass="form-control text_md" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <iframe id="Iframe1" src="/Common/UserUpload.aspx?FieldName=HeadBackGround" marginheight="0" marginwidth="0" frameborder="0" width="100%" height="30" scrolling="no"></iframe>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>栏目背景颜色： </td>
                    <td>
                        <asp:TextBox ID="HeadColor" runat="server" class="form-control text_md"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>KeyWords关键： </td>
                    <td>
                        <asp:TextBox ID="KeyWords" runat="server" class="form-control text_md" TextMode="MultiLine" Height="40px" /></td>
                </tr>
                <tr>
                    <td>Description内容： </td>
                    <td>
                        <asp:TextBox ID="Description" runat="server" class="form-control text_md" TextMode="MultiLine" Height="40px" /></td>
                </tr>
                <tr>
                    <td>顶部HTML内容： </td>
                    <td>
                        <asp:TextBox ID="TopWords" runat="server" class="form-control text_md" Style="max-width: 300px;" TextMode="MultiLine" Height="80px" /></td>
                </tr>
                <tr>
                    <td>尾部HTML内容： </td>
                    <td>
                        <asp:TextBox ID="BottonWords" runat="server" class="form-control text_md" Style="max-width: 300px;" TextMode="MultiLine" Height="80px" /></td>
                </tr>
                <tr>
                    <td class="btn_green text-center" colspan="2">
                        <asp:Button ID="BtnSubmit" runat="server" Text="确认" class="btn btn-primary" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="BtnCancle" runat="server" Text="取消" class="btn btn-primary" CausesValidation="false" OnClick="BtnCancle_Click" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
