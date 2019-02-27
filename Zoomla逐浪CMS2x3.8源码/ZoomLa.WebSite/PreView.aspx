<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreView.aspx.cs" Inherits="Plat_PreView" EnableViewState="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>文件预览</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
    <%--    <div runat="server" id="pdfDiv" visible="false">
            <embed id="pdfEmbed" class="precenter" src="<%=pdfUrl %>" <%=string.Format("style='width:{0}px;height:{1}px;'",viewWidth,viewHeight)%> type="application/pdf" />
        </div>--%>
        <div runat="server" id="pdfDiv" visible="false">
            <iframe src="/Plugins/PDFView/web/viewer.html?pdf=<%=pdfUrl %>" style="width:100%;border:none;height:600px;"></iframe>
        </div>
        <div id="imgDiv" runat="server" visible="false">
            <img src="<%=imgUrl %>" style="border: none;" />
        </div>
        <div id="generalDiv" runat="server" visible="false">
            <asp:Literal runat="server" ID="html_L"></asp:Literal>
        </div>
        <asp:TextBox runat="server" ID="ViewTxt" TextMode="MultiLine" CssClass="precenter" Visible="false"></asp:TextBox>
        <div runat="server" id="videoDiv">
            <div id="mediaplayer"></div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>
    .precenter{margin:0 auto; display:block;border:1px solid #ccc;}
</style>
<script src="/JS/jquery.media.js"></script>
<script src="/User/Cloud/Jwplayer/jwplayer.js"></script>
<script>
    $('.media').media({
                width: <%=viewWidth%>,
                height: <%=viewHeight%>,
                autoplay: true,
                //src: 'myBetterMovie.mov',
                //attrs: { attr1: 'attrValue1', attr2: 'attrValue2' }, // object/embed attrs 
                //params: { param1: 'paramValue1', param2: 'paramValue2' }, // object params/embed attrs 
                //caption: false // supress caption text 
            });
            function PlayVideo() {
                jwplayer("mediaplayer").setup({
                    flashplayer: "/User/Cloud/Jwplayer/Player.swf",
                    file: "<%:Request.QueryString["vpath"]%>",
                    autostart: true
                });
            }
</script>
</asp:Content>
