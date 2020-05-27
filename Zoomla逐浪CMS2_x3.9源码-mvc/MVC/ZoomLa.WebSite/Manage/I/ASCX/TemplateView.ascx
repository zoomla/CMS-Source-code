<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemplateView.ascx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.TemplateView" %>
<div class="template">
    <%--<div class="template_title"><h4>黄页样式:</h4></div>--%>
    <ul class="list-unstyled">
    <asp:Repeater runat="server" ID="Temp_RPT" >
    <ItemTemplate>
    <li class="padding5">
    <div data-tlpname="<%#Eval("TemplateUrl") %>" data-tlpid="<%#Eval("TemplateID") %>" class="Template_box">
    <div class="tempthumil"><a href="javascript:;" onclick="CheckTlp(this,'<%#Eval("TemplateID") %>','<%#Eval("TemplateUrl") %>')" title="点击进入模板管理"><img onmouseover="this.style.border='1px solid #9ac7f0';" onerror="shownopic(this);" onmouseout="this.style.border='1px solid #eeeeee';" alt="点击进入模板管理" style="width: 100%;" src="<%#Eval("TemplatePic") %>"></a></div>             
    <span class="pull-left"><a href="javascript:;"><%#Eval("TemplateTitle") %></a></span>
    </span>
    <div class="clearfix"></div>
    </div>        
    </li>
    </ItemTemplate>    
    </asp:Repeater>
    </ul>
</div>
<asp:HiddenField ID="TempleID_Hid" runat="server" />
<asp:HiddenField ID="TempleUrl_Hid" runat="server" />
<script>
    //选中模板样式
    function CheckTlp(obj, name, tlp) {
        $('.Template_box').removeClass('tlp_active');
        var $box = $(obj).closest(".Template_box");
        $box.addClass("tlp_active");
        $("#TempleID_Hid").val(name);
        $("#TempleUrl_Hid").val(tlp);
        if (OnTemplateViewCheck)//模板选择事件
        { OnTemplateViewCheck(tlp) }
    }
    //智能模板选择事件
    function OnTemplateViewCheck(value) {
        $("#TxtTemplate_hid").val(value);
    }
    //初始选择样式
    function initTlp() {
        if ($("#TempleID_Hid").val() != "")
            $("[data-tlpid='" + $("#TempleID_Hid").val() + "']").addClass("tlp_active");
        else {
            if ($(".tempthumil>a").length > 0&&"<%=IsFirstSelect.ToString() %>"=="True") {
                $(".tempthumil>a:first").click();
            }
        }
    }
    $().ready(function () {
        initTlp();
    });
</script>