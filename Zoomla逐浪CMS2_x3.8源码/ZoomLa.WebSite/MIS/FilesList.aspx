<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FilesList.aspx.cs" Inherits="MIS_FilesList" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>文档列表</title>
<link href="../App_Themes/User.css" type="text/css" rel="stylesheet" />
<script>
    function loadPage(id, url) {
        $("#" + id).addClass("loader");
        $("#" + id).append("Loading......");
        $.ajax({
            type: "get",
            url: url,
            cache: false,
            error: function () { alert('加载页面' + url + '时出错！'); },
            success: function (msg) {
                $("#" + id).empty().append(msg);
                $("#" + id).removeClass("loader");
            }
        });
    }
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div  class="Mis_pad">
<div class="Mis_Title"><strong><a href="javascript:void(0)" onclick="loadPage('see', '/Mis/AddFiles.aspx?ProID=<%=Request["ProID"] %>&MID=<%=Request["MID"]%>&Type=<%=Request["Type"] %>')">新建文档</a></strong></div>
<div class="td_center">
<table class="border"  width="100%"  cellspacing="1" cellpadding="0" rules="all" border="0">
<tr class="title" style=" background:#e7f3ff" height="25"> <th>编号</th><th> 标题 </th><th>名称 </th><th>来源 </th><th>文件大小 </th><th>来源 </th><th> 操作人 </th><th> 操作时间  </th><th>操作 </th></tr>
<ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='9' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
<ItemTemplate> 
<tr><td  width="25"><%#Eval("ID") %></td>
<td style="text-align:left" >  <a href="javascript:void(0)" onclick="loadPage('see', '/Mis/AddFiles.aspx?ID=<%#Eval("ID") %>&ProID=<%=Request["ProID"] %>&MID=<%=Request["MID"]%>&Type=<%=Request["Type"] %>')" ><%#Eval("Title") %></a>     </td>
<td  width="100"><%#Eval("Content") %></td>
<td width="100"> 文档</td>
<td width="100"><%#FileSize(Eval("Content").ToString()) %> </td>
<td width="100"><%#GetStatus(Convert.ToInt32(Eval("Status"))) %> </td> 
<td width="80"><a href="javascript:void(0)" onclick="loadPage('see', '/Mis/AddFiles.aspx?ID=<%#Eval("ID") %>&ProID=<%=Request["ProID"] %>&MID=<%=Request["MID"]%>&Type=<%=Request["Type"] %>')" >编辑</a>  </td> 
 </tr>
</ItemTemplate>
<FooterTemplate></FooterTemplate>
</ZL:ExRepeater>
</table> 
</div>
</div>
</asp:Content>
