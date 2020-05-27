<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetPageHtml.aspx.cs" Inherits="ZoomLaCMS.Manage.Template.GetPageHtml" MasterPageFile="~/Manage/I/Default.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>源码查看器</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <span class="pull-left" style="line-height: 32px;">目标网址：</span>
    <asp:TextBox ID="UrlText" CssClass="form-control text_md pull-left" runat="server">http://www.z01.com</asp:TextBox>
    <asp:Button ID="WebClientButton" runat="server" Text="用WebClient得到" OnClick="WebClientButton_Click" style="margin-left:5px;" class="btn btn-primary"></asp:Button>
    <asp:Button ID="WebRequestButton" runat="server" Text="用WebRequest得到" OnClick="WebRequestButton_Click" class="btn btn-primary"></asp:Button>
    <br>
    <asp:TextBox ID="ContentHtml" runat="server" TextMode="MultiLine" CssClass="form-control pull-left" Style="max-width: 100%; height: 360px;margin-top:10px;"> </asp:TextBox>
    <script>
        var xmlHttp = new XMLHttpRequest();
        function updatePage() {
            //readState == 4，表示请求成功完成
            if (xmlHttp.readyState == 4) {
                if (xmlHttp.status == 200) {
                    var response = xmlHttp.responseText; //HTTP请求返回的文本内容
                    //document.getElementById("txtValue").value = response;
                } else if (request.status == 404) {
                    //HTTP状态码为404，无法找到资源
                    alert("404 Not Found");
                } else if (request.status == 403) {
                    //HTTP状态码为403，资源不可用
                    alert("403 Forbidden");
                } else if (request.status == 401) {
                    //HTTP状态码为401，未经授权
                    alert("401 Unauthorized");
                }
            }
        }
        function callServer() {
            //表单中获取必要的数据
            //var city = document.getElementById("city").value;
            //var state = document.getElementById("state").value;
            //只有在数据不为空时才发出请求
            //if ((city == null)(city == "")) return;
            //if ((state == null)(state == "")) return;
            //请求的URL
            var url = "ShopSource.aspx";
            //联系服务器，打开连接
            xmlHttp.open("GET", url, true); //"true"代表该请求是异步的
            //设置请求完成时的响应函数,注意这里是请求完成时，并不是响应完成时
            xmlHttp.onreadystatechange = updatePage;
            //发送请求,因为已经在请求URL中添加了要发送给服务器的数据(city和state)，所以请求中 无需再发送其他数据.
            xmlHttp.send(null);
        }
    </script>
</asp:Content>
