<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditUser.aspx.cs" Inherits="ZoomLaCMS.Manage.Page.AuditUser" MasterPageFile="~/Manage/I/Default.master"%>
<%@ Register Src="~/Manage/I/ASCX/TlpDP.ascx" TagPrefix="ZL" TagName="TlpDown" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>查看企业黄页</title>
    <style>
    .template{float:left; margin-bottom:40px;}
    .Template_box{height:auto;}
    .template .pull-left{text-align:center; min-height:50px;}
    .template .tlp_active{border:2px solid #ff0000;}
    .template .template_title{padding-left:5px;}
    </style>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tbody id="Tbody1">
            <tr class="tdbg">
                <td align="center" colspan="2">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
                    <tr class="tdbg">
                        <td class="td_l">用 户 名：</td>
                        <td><asp:TextBox runat="server" ID="UName_T" CssClass="form-control text_300"></asp:TextBox></td>
                    </tr>
                    <tr>
                         <td>首页模板：</td>
                         <td> <%=PageCommon.GetTlpDP("TxtTemplate") %>
                            <asp:HiddenField ID="TxtTemplate_hid" runat="server" /></td>
                    </tr>
                    <tr id="Tr2" class="tdbg" runat="server">
                        <td>企业名称：</td>
                        <td><asp:TextBox runat="server" ID="CompName_T" class="form-control text_300" /></td>
                    </tr>
                    <tr id="Tr4" class="tdbg" runat="server">
                        <td>企业LOGO：</td>
                        <td>
                             <asp:TextBox runat="server" ID="Logo_T" class="form-control text_300" />
                        </td>
                    </tr>
                    <tr id="Tr5" class="tdbg" runat="server">
                        <td>企业说明：</td>
                        <td>
                             <asp:TextBox runat="server" ID="PageInfo_T"  TextMode="MultiLine" Width="600" Height="300" />
                            <%=Call.GetUEditor("PageInfo_T",2) %>
                        </td>
                    </tr>
            <asp:Literal ID="ModelHtml" runat="server"></asp:Literal> 
            <asp:Label ID="RegFileds" runat="server" Text=""></asp:Label><tr class="tdbg">
                <td></td><td>
                    <asp:HiddenField ID="HdnID" runat="server" />
                    <asp:HiddenField ID="HdnModel" runat="server" />
                    <asp:Button ID="Button5" CssClass="btn btn-primary" runat="server" Text="修改" OnClick="Button5_Click" />
                    <a href="PageManage.aspx" class="btn btn-primary">返回</a>
                </td>
            </tr>
        </tbody>
    </table>
    <ZL:TlpDown runat="server" />
        <div class="template">
            <div class="template_title"><h4>黄页样式:</h4></div>
                <ul class="list-unstyled">
                <asp:Repeater runat="server" ID="Temp_RPT" >
                <ItemTemplate>
                <li class="padding5">
                <div data-tlpname="<%#Eval("TemplateIndex") %>" data-tlpid="<%#Eval("PageNodeid") %>" class="Template_box">
                <div class="tempthumil"><a href="javascript:;" onclick="CheckTlp(this,'<%#Eval("PageNodeid") %>','<%#Eval("TemplateIndex") %>')" title="点击进入模板管理"><img onmouseover="this.style.border='1px solid #9ac7f0';" onerror="shownopic(this);" onmouseout="this.style.border='1px solid #eeeeee';" alt="点击进入模板管理" style="width: 100%;" src="<%#Eval("TemplateIndexPic") %>"></a></div>             
                <span class="pull-left"><a href="javascript:;"><%#Eval("PageNodeName") %></a></span>
                </span>
                <div class="clearfix"></div>
                </div>        
                </li>
                </ItemTemplate>    
                </asp:Repeater>
                </ul>
                </div>
        <asp:HiddenField ID="NodeStyle_Hid" runat="server" />
        <asp:HiddenField ID="Template_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .m715-50 {max-width:300px;}
    </style>
    <script type="text/javascript" src="/JS/Common.js"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/ZL_Content.js"></script>
    <script>
        var diag = new ZL_Dialog();
        function ShowTList(url) {
            diag.title = "选择模板";
            diag.url = url;
            diag.maxbtn = false;
            diag.ShowModal();
        }
        function CloseDialog() {
            diag.CloseModal();
        }
        function Tlp_SetValByName(name, val) {
            var obj = $("#" + name).val(val);
            CloseDialog();
        }
        function CheckTlp(obj, name, tlp) {
            $('.Template_box').removeClass('tlp_active');
            var $box = $(obj).closest(".Template_box");
            $box.addClass("tlp_active");
            $("#NodeStyle_Hid").val(name);
            $("#Template_Hid").val(tlp);
        }
        //初始选择样式
        function initTlp() {
            if ($("#NodeStyle_Hid").val() != "")
                $("[data-tlpid='" + $("#NodeStyle_Hid").val() + "']").addClass("tlp_active");
            else {
                $("[data-tlpid]:first").addClass("tlp_active");
                $("#NodeStyle_Hid").val($("[data-tlpid]:first").data('tlpid'));
                $("#Template_Hid").val($("[data-tlpid]:first").data('tlpname'));
            }
        }
        $().ready(function () {
            Tlp_initTemp();
            initTlp();
        });
    </script>
</asp:Content>