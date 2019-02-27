<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Design_Diag_Image_edit" MasterPageFile="~/Design/Master/Edit.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>图片设置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <div class="panel panel-primary">
       <div class="panel-heading"><i class="fa fa-image"></i><span class="marginl5">图片设置</span></div>
       <div class="panel-body" style="padding-left:0px;padding-right:0px;padding-top:0px;">
           <hr class="divider-long"/>
           <div class="control-section-divider labeled">图片样式</div>
           <hr class="divider-long"/>
           <div class="setting-row">
               <div><label class="row-title">上传图片</label></div>
               <ZL:FileUpload runat="server" ID="SFile_UP" style="display:inline-block;" OnlyImg="true" onchange="checkup();"/>
               <asp:Button runat="server" ID="SFile_Btn" CssClass="btn btn-primary" Text="上传" OnClick="SFile_Btn_Click" OnClientClick="return checkup();" />
               <div style="color:red;" id="upremind_div"></div>
           </div>
           <hr class="divider-long"/>
           <div class="setting-row">
               <div><label class="row-title">手动输入</label></div>
               <asp:TextBox runat="server" ID="imgurl_t" class="form-control"></asp:TextBox>
           </div>
           <div class="setting-row">
               <div><label class="row-title">图片宽高</label></div>
               <div class="input-group" style="width:400px;">
                   <span class="input-group-addon">宽</span>
                   <asp:TextBox runat="server" type="text" id="width_t" class="form-control text_80"/>
                   <span class="input-group-addon">高</span>
                   <asp:TextBox runat="server" type="text" id="height_t" class="form-control text_80" />
                   <span class="input-group-addon">px</span>
               </div>
           </div>
       </div>
         <div class="panel-footer">
             <input type="button" value="保存" class="btn btn-info" onclick="save();" />
             <input type="button" value="取消" class="btn btn-default" onclick="CloseSelf();" />
         </div>
   </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
    $(function () {
        $("#imgurl_t").val(editor.model.dataMod.src);
        var width = Convert.ToInt(editor.model.instance.css("width"));
        var height = Convert.ToInt(editor.model.instance.css("height"));
        $("#width_t").val(width); $("#height_t").val(height);
    })
    function checkup() {
        $("#upremind_div").text("");
        if (ZL_Regex.isEmpty($("#SFile_UP").val())) {
            $("#upremind_div").text("未指定需要上传图的图片"); return false;
        }
        else { return true; }
    }
    function save() {
        editor.model.dataMod.src = $("#imgurl_t").val();
        //修改宽高,不只修改外部,更需要修改img
        var width = Convert.ToInt($("#width_t").val()); var height = Convert.ToInt($("#height_t").val());
        if (width > 0 && height > 0) {
            editor.model.instance.css("width", width);
            editor.model.instance.css("height", height);
            var $wrap = editor.model.instance.find(".ui-wrapper");
            var $img = editor.model.instance.find(".imgcomp ");
            $img.width(width); $img.height(height);
            $wrap.width(width); $wrap.height(height);
        }
        NotifyUpdate();
        CloseSelf();
    }
    function updateiurl(url) {
        $("#imgurl_t").val(url)
        editor.model.dataMod.src = url;
        NotifyUpdate();
        CloseSelf();
    }
</script>
</asp:Content>