<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelFiles.aspx.cs" Inherits="ZoomLaCMS.Common.SelFiles" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>选择文件</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container-fluid">
        <div class="navbar-form navbar-left" role="search">
        <div class="form-group">
            <asp:TextBox ID="ImgName_T" runat="server" CssClass="form-control" placeholder="文件名"></asp:TextBox>
            <asp:TextBox ID="Sdate_T" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' });" class="form-control" placeholder="开始日期"></asp:TextBox> 至
            <asp:TextBox ID="Edate_T" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' });" class="form-control" placeholder="结束日期"></asp:TextBox>
        </div>
            <asp:Button ID="Search_B" CssClass="btn btn-default" runat="server" OnClick="Search_B_Click" Text="搜索"></asp:Button>
            <button class="btn btn-primary" type="button" onclick="$('#FileUp_F').click()">上传文件</button>
            <ZL:FileUpload ID="FileUp_F" runat="server" style="display:none;" />
            <asp:Button ID="FIleUp_B" OnClick="FIleUp_B_Click" runat="server" style="display:none;" />
        </div>
        <div class="pull-right"><button type="button" onclick="window.location = location;" class="btn btn-default">刷新</button></div>
    </div>
    <div class="container-fluid">
        <div class="col-lg-3 col-md-3 col-sm-3 hidden-xs">
            <div class="panel panel-primary">
            <div class="panel-heading">文件名称</div>
            <div class="panel-body">
                已上传文件
            </div>
            </div>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-12">
            <div class="panel panel-default">
            <div class="panel-heading">文件列表</div>
                <div class="panel-body">
                <ul class="list-unstyled margin_t5" style="height:420px; overflow-y:auto;">
                <ZL:ExRepeater ID="File_RPT" runat="server" PageSize="15" PagePre="<div class='clearfix'></div><div class='text-center'>" PageEnd="</div>">
                <ItemTemplate>
                <li class="col-lg-3 col-md-4 col-sm-4 col-xs-4 padding5">
                    <div><%#GetFileInfo() %></div>
                    <div class="text-center"><label><input type="checkbox" name="img_list" value="<%#GetVpath() %>"  /><%#Eval("name") %></label></div>
                </li>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
                </ZL:ExRepeater>
                </ul>
                <div class="text-center"><button type="button" onclick="SelFileData()" class="btn btn-primary">确定</button> <button type="button" onclick="closeDiag()" class="btn btn-primary">取消</button></div>
            </div>
         </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="PVal_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <link rel="stylesheet" href="/App_Themes/V3.css" />
    <style>
        .fileimg{display:block;width:100%;height:50px;text-align:center;font-size:3em;}
        .fileimg img{height:50px; vertical-align:top;}
        .fileflod{line-height:50px;}
        .headtip{padding:5px;}
        .list-unstyled img{width:150px;height:100px;}
        .list-unstyled li{text-align:center;}
    </style>
    <script type="text/javascript" src="/JS/ICMS/ZL_Common.js"></script>
    <script type="text/javascript" src="/JS/ICMS/alt.js"></script>
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script>
        function ShowUpFile(url) {
            parent.PageCallBack('selfiles', url, pval);
        }
        var pval = JSON.parse($("#PVal_Hid").val());
        function closeDiag() {
            parent.CloseDiag();
        }
        function SelFileData() {
            var $chk = $("[name=img_list]:checked")//选中数组对象
            if ($chk.length > 0) {
                var imgurl = "";
                for (var i = 0; i < $chk.length; i++) {
                    imgurl += $chk[i].value + "|";
                }
                parent.PageCallBack('selfiles', imgurl, pval);//父页面方法 name 控件id, 数组对象,上传根目录
            }
        }
        $().ready(function () {
            $("#FileUp_F").change(function () {
                $("#FIleUp_B").click();
            });
        });

    </script>
</asp:Content>
