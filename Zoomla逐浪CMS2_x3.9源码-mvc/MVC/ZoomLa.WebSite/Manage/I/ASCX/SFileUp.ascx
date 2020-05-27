<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SFileUp.ascx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.SFileUp" ClientIDMode="Static" %>
<!--支持手写输入,支持指定类型过滤,支持图片预览,支持多个控件共同使用,-->
<div class="sfile_body" id="<%=ID+"_body" %>">
    <div class="sfile_view" style="display:none;">
    <img src="#" class="sfile_img" style="width: 100px; height: 100px;" />
</div>
<div class="sfile_updiv" style="width:350px;margin-top:5px;">
    <div class="input-group">
        <asp:TextBox runat="server" ID="FVPath_T" CssClass="form-control sfile_t" Style="width: 300px; display: inline-block;" />
        <span class="input-group-btn">
            <a href="javascript:;" class="btn btn-default sfile_clsbtn" style="display:none;"><i class="fa fa-trash-o"></i> 清除</a>
            <a href="javascript:;" class="btn btn-info sfile_selbtn"><i class="fa fa-folder-open-o"></i> 选择</a>
        </span>
    </div>
    <ZL:FileUpload ID="FileUp_File" runat="server" Style="display: none;" class="sfile_up" />
</div>
</div>
<script>
    <%if(LoadRes){%>
    var SFile = function (conf) {
        var ref = this;
        ref.conf = conf;
        ref.conf.$body = $(this.conf.id + "_body");
        ref.conf.$text = this.conf.$body.find(".sfile_t");//上传文件名称(仅显示文件名)
        ref.conf.$up = this.conf.$body.find(".sfile_up");//用于上传的input
        ref.conf.$view = this.conf.$body.find(".sfile_view");//预览div
        ref.conf.$img = this.conf.$body.find(".sfile_img");
        ref.conf.$clsbtn = this.conf.$body.find(".sfile_clsbtn");
        ref.conf.$selbtn = this.conf.$body.find(".sfile_selbtn");
        ref.conf.$selbtn.click(function () { ref.conf.$up.click(); });
        ref.conf.$clsbtn.click(function () { ref.clear(); });
        ref.conf.$up.change(function (e) {
            //取消选择
            if (e.target.files.length < 1) { return; }
            var filename = $(this).val();
            var reader = new FileReader();
            ref.conf.$text.val(ref.getfname(filename, 100));
            reader.onload = function (e) { ref.showimg(e.target.result); }
            reader.readAsDataURL(e.target.files[0]);
            if (filename && filename != "") { ref.conf.$clsbtn.show(); }
            else { ref.conf.$clsbtn.hide(); }
        });
        //如果text中不为空, 且为图片,则显示预览
        ref.conf.fname = ref.conf.$text.val();
        ref.showimg(ref.conf.fname);
        if (ref.conf.fname && ref.conf.fname != "") { ref.conf.$clsbtn.show(); }
    };
    SFile.prototype.iswebimg = function (fname) {
        if (!fname || fname == "") { return false; }
        fname = fname.toLowerCase();
        if (fname.indexOf("data:image/") > -1)//base64
        {
            return true;
        }
        else if (fname.indexOf(".") > 0) {
            var start = fname.lastIndexOf(".");
            var ext = fname.substring((start + 1), fname.length);//jpg|png|gif
            return (ext == "jpg" || ext == "png" || ext == "gif")
        }
        else { return false; }
    }
    //显示图片预览
    SFile.prototype.showimg = function (fname) {
        var ref = this;
        if (ref.iswebimg(fname)) {
            ref.conf.$view.show();
            ref.conf.$img.attr("src", fname);
        }
    }
    SFile.prototype.getfname = function (fname, num) {
        fname = fname.replace(/\\/g, "/");
        if (fname.indexOf("/") > -1 || fname.indexOf("\\") > -1) {
            var s = fname.lastIndexOf("/") + 1;
            fname = fname.substring(s, fname.length);
        }
        if (num && num > 1 && fname.length > num) { fname = fname.substring(0, (num - 1)) + ".."; }
        return fname;
    }
    //清空已指定的元素
    SFile.prototype.clear = function () {
        var ref = this;
        ref.conf.$view.hide();
        ref.conf.$clsbtn.hide();
        ref.conf.$up.val("");
        ref.conf.$text.val("");
    }
    <%}%>
    new SFile({ ftype: "<%=FType.ToString()%>", id: "#<%=ID%>" });
</script>
