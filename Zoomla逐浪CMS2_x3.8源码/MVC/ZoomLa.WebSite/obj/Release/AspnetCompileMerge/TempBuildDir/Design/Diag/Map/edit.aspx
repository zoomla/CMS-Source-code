<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.Map.edit" MasterPageFile="~/Design/Master/Edit.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>修改地图</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="panel panel-primary">
       <div class="panel-heading"><i class="fa fa-map-marker"></i><span class="marginl5">修改地图</span></div>
       <div class="panel-body" style="padding-left:0px;padding-right:0px;padding-top:0px;">
           <hr class="divider-long"/>
           <div class="control-section-divider labeled">属性设置</div>
           <hr class="divider-long"/>
           <div class="setting-row">
               <div>
                   <label class="row-title">宽：</label><input type="text" id="width_t" class="form-control text_80" maxlength="4" />
                   <label class="row-title">高：</label><input type="text" id="height_t" class="form-control text_80" maxlength="4" />
               </div>
           </div>
          <hr class="divider-long"/>
   <%--        <div class="setting-row">
               <label class="row-title">允许拖动：</label><input type="checkbox" class="switchChk" id="drag_chk" />
           </div>
           <div class="setting-row">
               <label class="row-title">允许缩放：</label><input type="checkbox" class="switchChk" id="zoom_chk" />
           </div>--%>
           <hr class="divider-long"/>
    <%--       <div>
               <iframe id="mapifr" style="border:none;width:100%;height:300px;"></iframe>
           </div>--%>
       </div>
   </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script>
        $(function () {
            $("#width_t").val(editor.model.dataMod.width);
            $("#height_t").val(editor.model.dataMod.height);
            //document.getElementById("drag_chk").checked = editor.model.dataMod.drag;
            //document.getElementById("zoom_chk").checked = editor.model.dataMod.zoom;
            //$("#mapifr").attr("src", "/Design/Diag/Map/map.aspx?dataMod=" + JSON.stringify(editor.model.dataMod));
            $("#width_t").change(function () {
                var val = StyleHelper.ConverToInt(this.value);
                if (val > 10) { editor.model.dataMod.width = val; editor.model.Render(); }
               
            });
            $("#height_t").change(function () {
                var val = StyleHelper.ConverToInt(this.value);
                if (val > 10) { editor.model.dataMod.height = val; editor.model.Render(); }
            });
        })
    </script>
</asp:Content>
