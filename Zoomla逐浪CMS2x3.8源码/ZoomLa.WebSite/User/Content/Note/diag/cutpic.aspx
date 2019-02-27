<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cutpic.aspx.cs" Inherits="test_diag_cutpic" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>图片裁剪</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="min-height:500px;overflow:hidden;">
        <img runat="server" src="<%=ImgPath %>" id="photo_img" class="img-responsive"/>
    </div>
<%--    <input type="button" value="确定裁剪" onclick="cutpic();" />--%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<link href="/Plugins/PicEdit/css/imgareaselect-default.css" rel="stylesheet" />
<script src="/Plugins/PicEdit/JS/jquery.imgareaselect.js"></script>
<%--<script src="/Plugins/PicEdit/JS/jquery.imgareaselect.pack.js"></script>--%>
<script>
    var img = { width: <%=(int)(1910*ImgPercent)%>, height: <%=(int)(650*ImgPercent)%> };
    //if(img.width>760){img.width=760;}if(img.height>650){img.height=650;}
    var cursele = { x1: 0, y1: 0, width: img.width, height: img.height };
    $(function () {
        var selectObj = $('#photo_img').imgAreaSelect({
            aspectRatio: "2.95:1",//裁剪框的宽高比
            fadeSpeed: 500,
            autoHide: false,
            handles: true,
            instance: true,
            autoHide: false,
            resizable:true,
            persistent: true,
            x1: 0, y1: 0, x2: img.width, y2: img.height,
            onInit: function () {

            },
            onSelectEnd: function (img, selection) {
                cursele = selection;
            }
        });//photo end;
    });
    function cutpic() {
        $.post("<%=Request.RawUrl%>", { action: "crop", "x1": cursele.x1, "y1": cursele.y1, width: cursele.width, height: cursele.height,percent:<%:ImgPercent%> }, function (url) {
            //selectObj.cancelSelection();
            parent.cutok(url);
            console.log(url);
        })
    }
    function minImgAlert()
    {
        alert("图像过小,请上传1920*680以上的高清图");parent.closeDiag();
    }
    console.log("<%:ImgPercent%>",img.width,img.height);
</script>
</asp:Content>