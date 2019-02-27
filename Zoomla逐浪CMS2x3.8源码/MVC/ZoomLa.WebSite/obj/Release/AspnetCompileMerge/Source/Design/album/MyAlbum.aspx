<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAlbum.aspx.cs" Inherits="ZoomLaCMS.Design.album.MyAlbum" MasterPageFile="~/Design/Master/User.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>我的相册</title>
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0;" name="viewport" />
<link type="text/css" rel="stylesheet" href="/dist/css/weui.min.css" />  
<link href="/Template/PowerZ/style/global.css?Version=20150910" rel="stylesheet"/> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div> 
<div class="container">
    <div class="mbh5_list_t">
        <h4>欢迎，这是您在逐浪H5平台的相册：</h4>
    </div>
    <asp:Repeater runat="server" ID="RPT" OnItemCommand="RPT_ItemCommand" EnableViewState="false">
        <ItemTemplate>
            <div class="mb_rlist">
                <div class="mb_rlist_t" style="max-height:200px;overflow-y:hidden;"><a href="/design/album/default.aspx?id=<%#Eval("ID") %>">
                    <img src="<%#Eval("photos","").Split('|')[0]%>" onerror="javascript:this.src='/UploadFiles/nopic.gif';"></a></div>
                <div class="mb_rlist_p">
                    <h3><a href="/design/album/default.aspx?id=<%#Eval("ID") %>"><%#Eval("AlbumName") %></a></h3>
                    <p class="text-center">
                        <span><i class="fa fa-clock-o"></i><%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %></span>
                    </p>
                    <p class="text-center">
                        <a class="btn btn-info" href="/design/album/default.aspx?id=<%#Eval("ID") %>"><i class="fa fa-paint-brush"></i> 设计</a>
                        <a class="btn btn-info" href="/design/album/mbview.aspx?id=<%#Eval("ID") %>"><i class="fa fa-globe"></i> 浏览</a>
                        <asp:LinkButton runat="server" CssClass="btn btn-warning" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i> 删除</asp:LinkButton>
                    </p>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div style="padding: 10px 0">
        <a href="/design/album/" class="btn btn-danger btn-block">创建新相册</a>
    </div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>
#shortcut_ul li { padding:10px 0; width:33.3333%; height:auto;}
#shortcut_ul li .fa { padding-top:0;}
.siteinfo { padding-left:15px; padding-right:15px;}
.info_head { padding-left:0; font-family:"STHeiti","Microsoft YaHei","黑体","arial";}
.info_head_img { float:none;}
.info_head_img img { max-width:100%; max-height:none;}
.info_head_x h1 { font-size:1.5em;}
.option_style { font-size:1em;}
</style>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>
<script>
function downSite() {
	if (!confirm('确定要下载全站,以进行私有化布署吗?')) { return false; }
	comdiag.ShowMask("正在打包数据,请等待");
	return true;
}
</script>
</asp:Content>