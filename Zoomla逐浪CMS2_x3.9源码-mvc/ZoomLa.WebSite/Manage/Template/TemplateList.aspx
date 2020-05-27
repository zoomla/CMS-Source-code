<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateList.aspx.cs" Inherits="ZoomLaCMS.Manage.Template.TemplateList" MasterPageFile="~/Common/Common.master" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<style type="text/css">
#bread_div {padding-left:5px;  line-height:30px;margin-top:10px;}
#bread_div a{color:#06c;}
#bread_div .spanr {padding:0 4px;color:#aaa;font-size:18px;position:relative;top:2px;}
.colhead {padding:8px;background-color:#f7f7f7;color:#888;border:1px solid #d2d2d2;border-radius:2px;}
.coltr {padding:8px;border-bottom:1px solid #ddd;}
.col20 {display:inline-block;width:20%;}
.col60 {display:inline-block;width:50%;}
</style>
<title>选择模板</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="padding10" style="font-size:12px;padding-top:0px;position:relative;top:-10px;">
<div id="bread_div"><asp:Label runat="server" ID="Bread_L" EnableViewState="false"></asp:Label></div>
<div class="colhead"><span class="col60">文件名</span><span class="col20">大小</span><span class="col20">修改日期</span></div>
<div class="mainlist" style="height:350px;overflow-y:auto;">
    <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
        <ItemTemplate>
            <div class="coltr">
                <span class="col60"><%#GetLink()%></span>
                <span class="col20"><%#Eval("ExSize") %></span>
                <span class="col20"><%#Eval("LastWriteTime","{0:yyyy年MM月dd日 HH:mm}") %></span>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div class="input-group margin_t5" style="width: 470px;">
    <span class="input-group-addon">文件名称</span>
    <input type="text" id="fname_t" class="form-control text_300" />
    <span class="input-group-btn">
        <input type="button" id="BtnSubmit" class="btn btn-info" value="确定" onclick="SureSel()"/>
        <input type="button" id="BtnCancel" class="btn btn-info" value="关闭" onclick="closeDiag()"/>
    </span>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
    function SelHtmlFile(path) {
        $("#fname_t").val(path);
    }
    function SureSel() {
        var name = "<%:OpenerText %>";
        var val = document.getElementById('fname_t').value;
        parent.Tlp_SetValByName(name, val);
        closeDiag();
    }
    function closeDiag() {
        if (parent.CloseDiag) { parent.CloseDiag(); }
        if (parent.CloseComDiag) { parent.CloseComDiag(); }
    }
</script>
</asp:Content>