<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserInfoBar.ascx.cs" Inherits="Manage_I_ASCX_UserInfoBar" %>
<div style="width:<%=Width %>px;" id="<%=ClientID %>">
<span>信息完成度:</span>
<div class="progress">
    <div class="progress-bar" role="progressbar"  style="width: 0%;">
    <span class="protext_span"></span>
    </div>
</div>
</div>
<script>
    function InitUserBar(bar) {
        if (bar == -1) { $("#<%=ClientID %>").hide(); }
        $("#<%=ClientID %> .progress-bar").css("width", bar + "%");
        $("#<%=ClientID %> .protext_span").text(bar+"%");
    }
</script>
