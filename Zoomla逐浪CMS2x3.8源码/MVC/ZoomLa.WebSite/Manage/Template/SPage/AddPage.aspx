<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPage.aspx.cs" Inherits="ZoomLaCMS.Manage.Template.SPage.AddPage" MasterPageFile="~/Manage/I/Default.Master" ValidateRequest="false"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>页面管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="min-height:550px;">
        <table class="table table-bordered table-striped">
        <tr><td class="td_m">页面名称</td><td><ZL:TextBox runat="server" ID="PageName_T" CssClass="form-control text_300" AllowEmpty="false" ValidType="String"  /></td></tr>
        <tr><td>预览链接</td><td><asp:TextBox runat="server" ID="ViewUrl_T" CssClass="form-control text_300" placeholder="为空则使用默认预览链接"/></td></tr>
        <tr><td class="td_m">模板类型</td>
           <td>
               <div class="btn-group">
                   <button type="button" class="btn btn-default" onclick="sethead('node');">节点模板</button>
                   <button type="button" class="btn btn-default" onclick="sethead('content');">内容模板</button>
                   <button type="button" class="btn btn-default" onclick="sethead('custom');">自由模板</button>
               </div>
           </td>
        </tr>
        <tr><td>数据源标签</td><td><asp:TextBox runat="server" ID="PageDSLabel_T" class="form-control" placeholder="请放置数据源标签"></asp:TextBox></td></tr>
        <tr><td>页面头部</td><td>
            <asp:TextBox runat="server" ID="PageRes_T" CssClass="form-control" TextMode="MultiLine" style="height:240px;" placeholder="请输入头部Meta,标题和需要引入的JS或CSS资源,支持标签">
            </asp:TextBox>
        </td></tr>
        <tr><td>页面底部</td><td>
             <asp:TextBox runat="server" ID="PageBottom_T" CssClass="form-control" TextMode="MultiLine" style="height:120px;" placeholder="推荐将JS资源置于底部,以加快页面解析,支持标签"></asp:TextBox>
        </td></tr>
        <tr><td>备注</td><td><asp:TextBox runat="server" ID="PageDesc_T" CssClass="form-control" /></td></tr>
        <tr><td></td><td>
            <asp:Button runat="server" ID="Save_Btn" class="btn btn-info" Text="保存信息" OnClick="Save_Btn_Click" />
        </td></tr>
    </table>
    </div>
<div style="display:none;">
<textarea id="node_label">{ZL.Source id="节点栏目信息数据源"/}</textarea>
<textarea id="node_res">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="Keywords" content="{SField sid="节点栏目信息数据源" FD="Meta_Keywords" page="0"/}" />
<meta name="description" content="{SField sid="节点栏目信息数据源" FD="Meta_Description" page="0"/}" />
<link href="{$CssDir/}global.css?Version={ZL.Label id="前端样式版本号"/}" rel="stylesheet"/>
<title>{SField sid="节点栏目信息数据源" FD="NodeName" page="0"/}_{$SiteName/}</title>
{ZL:Boot()/}
</textarea>
<textarea id="content_label">{ZL.Source id="文章模型数据源标签"/}</textarea>
<textarea id="content_res">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="Keywords" content="{SField sid="文章模型数据源标签" FD="Tagkey" page="0"/}" />
<meta name="Description" content="{SField sid="文章模型数据源标签" FD="Title" page="0"/}">
<link href="{$CssDir/}global.css?Version={ZL.Label id="前端样式版本号"/}" rel="stylesheet"/>
<title>{SField sid="文章模型数据源标签" FD="Title" page="0"/}_{$SiteName/}</title>
{ZL:Boot()/}
</textarea>
<textarea id="custom_res">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="{$CssDir/}global.css?Version={ZL.Label id="前端样式版本号"/}" rel="stylesheet"/>
<title>{$SiteName/}</title>
{ZL:Boot()/}
</textarea>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
 <script>
$(function () {
    if ("<%=Mid%>" == "0") { sethead("node"); }
})
function sethead(id) {
    $("#PageDSLabel_T").val($("#" + id + "_label").text());
    $("#PageRes_T").val($("#" + id + "_res").text());
}
 </script>
</asp:Content>