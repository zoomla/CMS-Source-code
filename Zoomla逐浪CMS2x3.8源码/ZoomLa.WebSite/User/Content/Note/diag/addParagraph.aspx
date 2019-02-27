<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addParagraph.aspx.cs" MasterPageFile="~/Common/Common.master" Inherits="User_Content_Note_diag_addParagraph" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>添加段落</title>
    <link rel="stylesheet" type="text/css" href="/user/content/note/note.css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="container-fluid paragraph_body">
        <div class="form-group parag_item">
            <div class="col-md-2 col-sm-2 col-xs-2 control-label text-right">段落名称 </div>
            <div class="col-md-10 col-sm-10 col-xs-10">
                <input type="text" id="title_t" class="form-control" /> 
                <div class="parag_tips">添加段落名称让你的游记更加清晰明了，</div>
                <div class="parag_tips">例如“关于行程”“关于花费”“行程计划”“惊险的一天”等。</div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-2 col-sm-2 col-xs-2 text-right control-label">选择段落图片</div>
            <div class="col-md-10 col-sm-10 col-xs-10" >
                <ul class="list-inline parag_img_list">
                    <li>
                        <a href="javascript:;">
                            <img src="/user/content/note/res/ps1.gif" />
                            <span class="parag_sel_span parag_active" data-val="t1"><i class="fa fa-check"></i></span>
                        </a>
                    </li>
                    <li>
                        <a href="javascript:;">
                            <img src="/user/content/note/res/ps2.gif" />
                            <span class="parag_sel_span" data-val="t2"><i class="fa fa-check"></i></span>
                        </a>
                    </li>
                    <li>
                        <a href="javascript:;">
                            <img src="/user/content/note/res/ps3.gif" />
                            <span class="parag_sel_span" data-val="t3"><i class="fa fa-check"></i></span>
                        </a>
                    </li>
                    <li>
                        <a href="javascript:;">
                            <img src="/user/content/note/res/ps4.gif" />
                            <span class="parag_sel_span" data-val="t4"><i class="fa fa-check"></i></span>
                        </a>
                    </li>
                    <li>
                        <a href="javascript:;">
                            <img src="/user/content/note/res/ps5.gif" />
                            <span class="parag_sel_span" data-val="t5"><i class="fa fa-check"></i></span>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="form-group">
            <div class="col-xs-offset-2 col-xs-10 text-right">
                <button type="button" onclick="AddParagraph()" style="width:100px;" class="btn btn-info">确定</button>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script>
        var id = "<%:Request.QueryString["id"]%>";
        var model=null;
        $(function () {
            $(".parag_img_list a").click(function () {
                $(".parag_sel_span").removeClass("parag_active");
                $(this).find("span").addClass("parag_active");
            });
            if (id && id != "") {
                model = parent.scope.comMod.comlist.GetByID(id);
                $("#title_t").val(model.title);
            }
        });
        function AddParagraph() {
            if ($("#title_t").val() == "") { alert("段落名称不能为空!"); return; }
            if (model == null) {
                parent.note.Para.add($("#title_t").val(), $(".parag_active").data("val"));
            }
            else {
                model.title = $("#title_t").val(); model.content = $(".parag_active").data("val");
            }
            parent.scope.$digest();
            parent.closeDiag();
        }
    </script>
</asp:Content>

