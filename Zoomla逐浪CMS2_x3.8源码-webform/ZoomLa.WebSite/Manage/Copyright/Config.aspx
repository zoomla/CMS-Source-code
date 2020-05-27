<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Config.aspx.cs" Inherits="Manage_Copyright_Config" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>版权配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div runat="server" id="Prompt_Div" visible="false">
        <div class="alert alert-info in margin_b2px">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <h4><%=Resources.L.提示 %>!</h4>
            <p style="padding-left: 50px;">
                <strong><i class="fa fa-bullhorn"></i>
                 如登录显示服务器忙,可点击前往<a href="http://sale.banquanyin.com/account/licensor" class="rd_red" title="点击访问" target="_blank">版权印</a>,完成登录后再<a href="Config.aspx" class="rd_red">刷新</a>
                </strong>
            </p>
        </div>
    </div>
    <iframe runat="server" id="config_ifr" style="width:100%;" frameborder="0" marginheight="0" marginwidth="0"></iframe>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    $("#config_ifr").load(function () {
        var $obj = $(this);
        $obj.height($(document).height() - 120);
    });
</script>
</asp:Content>
