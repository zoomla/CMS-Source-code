<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.Video.Edit" MasterPageFile="~/Design/Master/Edit.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>视频设置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <div class="panel panel-primary">
       <div class="panel-heading"><i class="fa fa-video-camera"></i><span class="marginl5">视频设置</span></div>
       <div class="panel-body" style="padding-left:0px;padding-right:0px;padding-top:0px;">
           <hr class="divider-long"/>
           <div class="control-section-divider labeled">样式设置</div>
           <hr class="divider-long"/>
           <div class="setting-row">
               <div>
                   <label class="row-title">宽：</label><input type="text" id="width_t" class="form-control text_80" maxlength="4" />
                   <label class="row-title">高：</label><input type="text" id="height_t" class="form-control text_80" maxlength="4" />
               </div>
           </div>
           <hr class="divider-long"/>
           <div class="setting-row" id="file_div" style="display:none;">
                <table class="table table-bordered table-striped">
                    <tr><td>视频地址：</td><td><input type="text" class="form-control" id="src_t" /></td></tr>
                    <tr><td>视频海报：</td><td><input type="text" class="form-control" id="poster_t" /></td></tr>
                    <tr><td>自动播放：</td><td><input type="checkbox" class="switchChk" id="loop_chk"/></td></tr> 
                    <tr><td>循环播放：</td><td><input type="checkbox" class="switchChk" id="autoplay_chk"/></td></tr>
                    <tr><td></td><td><input type="button" value="保存修改" class="btn btn-primary" onclick="SaveEdit();" /></td></tr>
                </table>
           </div>
           <div class="setting-row" id="online_div" style="display:none;">
              <textarea id="htmlTlp_t" class="form-control" style="height:150px;"></textarea>
              <input type="button" value="保存修改" class="btn btn-primary" style="margin-top:5px;" onclick="SaveOnline();" />
           </div>
       </div>
   </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
   <script>
       $(function () {
           var compType = editor.model.config.compType;
           if (compType == "file") {
               editor.video = editor.dom.find("video");
               $("#src_t").val(editor.model.dataMod.src);
               $("#poster_t").val(editor.model.dataMod.poster);
               $("#loop_chk")[0].checked = editor.model.config.loop;
               $("#autoplay_chk")[0].checked = editor.model.config.autoplay;
               $("#file_div").show();
           }
           else if (compType == "online") {
               editor.video = editor.dom.find("embed");
               editor.model.dataMod.videoStyle.width = editor.video.width();
               editor.model.dataMod.videoStyle.height = editor.video.height();
               $("#htmlTlp_t").val(decodeURI(editor.model.config.htmlTlp));
               $("#online_div").show();
           }
           $("#width_t").val(editor.model.dataMod.videoStyle.width);
           $("#height_t").val(editor.model.dataMod.videoStyle.height);
       })
       function SaveEdit() {
           editor.model.dataMod.videoStyle.width = $("#width_t").val();
           editor.model.dataMod.videoStyle.height = $("#height_t").val();
           editor.model.dataMod.src = $("#src_t").val();
           editor.model.dataMod.poster = $("#poster_t").val();
           editor.model.config.loop = $("#loop_chk")[0].checked;
           editor.model.config.autoplay = $("#autoplay_chk")[0].checked;
           editor.scope.$digest();
           parent.CloseDiag();
           editor.video.attr("src", $("#src_t").val());
           editor.video.attr("poster", $("#poster_t").val());
       }
       function SaveOnline() {
           editor.model.dataMod.videoStyle.width = $("#width_t").val();
           editor.model.dataMod.videoStyle.height = $("#height_t").val();
           editor.video.attr("width", $("#width_t").val());
           editor.video.attr("height", $("#height_t").val());
           editor.model.config.htmlTlp=encodeURI(editor.video[0].outerHTML);
           parent.CloseDiag();
       }
   </script>
</asp:Content>