<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadMvs.aspx.cs" Inherits="ZoomLaCMS.Common.UploadMvs" MasterPageFile="~/Common/Common.master"  %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>微视频上传</title>
<style>
#Success_L{color:green;}
#Error_L{color:red;margin-left:5px;}
.upload_div{background-color:red;position:absolute;top:0px;left:0px;width:100%;height:100%;text-align:center;z-index:5;}
.upload_div .fa-spinner{font-size:40px;margin-top:100px;}
table .btn{margin-right:10px;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content"> 
<table class="table table-bordered">
    <tr>
        <td class="td_m">视频标题</td>
        <td><ZL:TextBox runat="server" ID="Title_T" CssClass="form-control text_300" AllowEmpty="false" /></td>
    </tr>
    <tr>
        <td>选择文件</td>
        <td>
            <ZL:SFileUp runat="server" ID="Video_UP" FType="All"/>
        </td>
    </tr>
    <tr>
        <td>视频封面</td>
        <td><asp:TextBox runat="server" ID="Cover_T" CssClass="form-control text_300" /><span style="color:green">*输入网络图片地址</span></td>
    </tr>
    <tr>
        <td>视频描述</td>
        <td><asp:TextBox runat="server" ID="Remind_T" TextMode="MultiLine" CssClass="form-control" Width="300" Height="70" /></td>
    </tr>
    <tr><td></td>
        <td>
            <asp:Button runat="server" ID="Up_B" Text="提交" CssClass="btn btn-info" OnClick="Up_B_Click" /><input type="button" onclick="CloseDiag();" class="btn btn-info" value="取消" />
            <asp:Label runat="server" ID="Error_L"></asp:Label>
        </td>
    </tr>
</table>
<div class="upload_div" style="display:none;">
    <i class="fa fa-spinner fa-spin"></i>
    <p>正在上传,请勿关闭窗口</p>
</div>
</asp:Content> 
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
    var CloseDiag = function () {
        parent.CloseComDiag();
    }
    var UploadMvs = function (name, remotePath, url) {
        parent.api_qq_mvs.Success(name, remotePath, url);
    }
    var beginup = function () {
        $(".upload_div").show();
    }
    var endup = function () {
        $(".upload_div").hide();
    }
</script>    
</asp:Content>

