<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppTlpOnline.aspx.cs" Inherits="ZoomLaCMS.Manage.APP.AppTlpOnline" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <iframe src="http://app.z01.com/App/AppTlp/MyTlpList.aspx" style="width:100%;height:100%; border:none;"></iframe>
        <script>
            $(function () {
                $('iframe').height($(parent.document).find('#main_right').height() - 20);
            })
        </script>
</asp:Content>
