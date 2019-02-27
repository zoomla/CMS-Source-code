<%@ Page Language="C#"  MasterPageFile="~/User/Default.master"  AutoEventWireup="true" CodeFile="PageInfo.aspx.cs" Inherits="User_Pages_PageInfo" ClientIDMode="Static" ValidateRequest="false" %>
<%@ Register Src="~/Manage/I/ASCX/TemplateView.ascx" TagPrefix="ZL" TagName="TlpViewList" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>注册企业黄页</title> 
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
        </asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="page" data-ban="page"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">注册企业黄页</li> 
    </ol>
    </div>
    <div class="container">
    <div class="us_seta" style="margin-top: 10px;">
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
    </div>
    <div class="container">
    <div class="us_seta" id="regpage" runat="server">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2" class="text-center">
                    <asp:Label ID="Label2" runat="server" Text="添加企业黄页"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="text-center text_150">会员帐号:</td>
                <td>
                    <asp:Label ID="Username" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="text-center">申请类型:</td>
                <td>
                    <asp:RadioButtonList ID="Styleids" runat="server" OnSelectedIndexChanged="Styleids_SelectedIndexChanged" AutoPostBack="True" RepeatDirection="Horizontal" Height="24px" RepeatColumns="4" Style="margin-top: 0px; padding-top: 0px; line-height: 30px"></asp:RadioButtonList>
                </td>
            </tr> 
            <tr>
                <td class="text-center">企业名称:</td>
                <td>
                    <asp:TextBox ID="Proname" Name="Proname" CssClass="form-control text_md" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-center">企业LOGO:</td>
                <td>
                    <table border='0' cellpadding='0' cellspacing='1' width='100%'>
                        <tr class='tdbg'>
                            <td>
                                <asp:TextBox ID="txt_logos" runat="server" CssClass="form-control text_md"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class='tdbg'>
                            <td>
                                <iframe id="Iframe1" src="/Common/UserUpload.aspx?FieldName=txt_logos" marginheight="0" marginwidth="0" frameborder="0" width="100%" height="30" scrolling="no"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="text-center">企业说明:</td>
                <td>
                    <asp:TextBox ID="PageInfo" runat="server" Height="400px" TextMode="MultiLine" Width="800"></asp:TextBox>
                </td>
            </tr>
            <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
			 <tr>
				<td class="text-center">黄页样式:</td>
				<td>
                    <ZL:TlpViewList ID="TlpView_Tlp" runat="server" TemplateID="PageNodeid" TemplateUrl="TemplateIndex" TemplateTitle="PageNodeName" TemplatePic="TemplateIndexPic" />
				</td>
			</tr>
        </table> 
        <div class="clearfix"></div>
        <div class="clearfix"></div>
        <table class="btn_green" style="margin: auto;">
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:HiddenField ID="HdnNode" runat="server" />
                    <asp:HiddenField ID="HdnID" runat="server" />
                    <asp:HiddenField ID="HdnModel" runat="server" />
                    <asp:Button ID="BtnSubmit" CssClass="btn btn-primary" runat="server" Text="提交" OnClick="BtnSubmit_Click" />
                    &nbsp;&nbsp;
                    <input type="reset" class="btn btn-primary" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    <div class="clear"></div>
    <div class="us_seta" style="margin-top: 10px; line-height: 28px; padding-left: 20px;" id="Auditing" runat="server" visible="false">
        正在审核中......! 请等待管理员开通您的黄页功能! <a href=""></a>
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">修改我提交的黄页信息!</asp:LinkButton><br />
        <br />
        <br />
    </div>
    <div class="clear"></div> 
    </div>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/ZL_Content.js"></script>
    <script>
        $().ready(function () {
            $("input[type=text]").addClass("form-control text_md");
            $("select").addClass("form-control text_md");
        });

        $("#mimenu_btn").click(function (e) {
            if ($(".user_mimenu_left").width() > 0) {
                $(".user_mimenu_left ul").fadeOut(100);
                $(".user_mimenu_left").animate({ width: 0 }, 200);
            }
            else {
                $(".user_mimenu_left").animate({ width: 150 }, 300);
                $(".user_mimenu_left ul").fadeIn();
            }
        });
        //会员菜单更多显示/隐藏
        $("#more_btn").click(function (e) {
            if ($(".user_menu_sub").css("display") == "none") {
                $(".user_menu_sub").fadeIn();
                $(this).find("i").removeClass("fa-angle-double-down");
                $(this).find("i").addClass("fa-angle-double-up");
            }
            else {
                $(".user_menu_sub").fadeOut();
                $(this).find("i").removeClass("fa-angle-double-up");
                $(this).find("i").addClass("fa-angle-double-down");
            }
        });
        //会员搜索
        $("#sub_btn").click(function (e) {
            if ($("#key").val() == "")
                alert("搜索关键字为空!");
            else
                window.location = "/User/SearchResult.aspx?key=" + escape($("#key").val());
        });
        //搜索回车事件
        function IsEnter(obj) {
            if (event.keyCode == 13) {
                $("#sub_btn").click(); return false;
            }
        }
    </script>
    <%=Call.GetUEditor("PageInfo",2) %>
</asp:Content>
