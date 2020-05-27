<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateEdit.aspx.cs" Inherits="ZoomLaCMS.Manage.Template.TemplateEdit"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="/Plugins/CodeMirror/lib/codemirror.css">
    <link rel="stylesheet" href="/Plugins/CodeMirror/theme/eclipse.css">
    <script src="/Plugins/CodeMirror/lib/codemirror.js"></script>
    <script src="/Plugins/CodeMirror/mode/xml.js"></script>
    <script src="/Plugins/CodeMirror/mode/javascript/javascript.js"></script>
    <script src="/Plugins/CodeMirror/addon/selection/active-line.js"></script>
    <script src="/Plugins/CodeMirror/addon/edit/matchbrackets.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <%--<script src="/Plugins/CodeMirror/mode/htmlmixed.js"></script>--%>
    <title>编辑模板</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered" align="center">
        <tr class="title" style="font-weight: normal;">
            <td align="left" colspan="2">
                <div id="add_div" visible="false" runat="server">
                    <span style="float:left; margin-left:10px; margin-right:10px; font-weight:bold; margin-top:0.3em;">请输入模板名称:</span>
               <div class="input-group" style="width: 300px; float:left">
                   <input runat="server" id="TxtFilename" style="text-align: right" class="form-control" />
                   <div class="input-group-btn">                   
                   <asp:Label runat="server" ID="name_L" class="btn btn-default">.html</asp:Label>
                   <a href="javascript:;" id="ViewEdit_B" class="btn btn-default" target="_blank" title="可视化编辑"><i class="fa fa-edit"></i></a>
                   </div>
               </div>
                </div>
                <div id="edit_div" visible="false" class="input-group" runat="server" style="width:300px;">                 	                               	
                     <a runat="server" id="ViewEdit_A" class="btn btn-default input-group-addon" visible="false" target="_blank" title="可视化编辑"><i class="fa fa-edit"></i></a>
                     <asp:TextBox ID="TempUrl_L" runat="server" CssClass="form-control" EnableViewState="false"></asp:TextBox>
                </div>
                <span class="rd_red" runat="server" id="whitespace_sp" visible="false">提示：模板名不能包含空格,为了安全和规范,保存时会自动过滤空格</span>
                <asp:HiddenField ID="HdnShowPath" runat="server" />
                <asp:HiddenField ID="Hdnmethod" runat="server" />
        </tr>
        <tr>
            <td colspan="2">
                <div id="labeldiv">
        <ul class="nav nav-tabs top_opbar" style="margin-bottom:0px;border-bottom:none;">
            <li class="active"><a href="#Tabs0" data-toggle="tab">自定标签</a></li>
            <li><a href="#Tabs1" data-toggle="tab">字段标签</a></li>
            <li><a href="#Tabs2" data-toggle="tab">系统标签</a></li>
            <li><a href="#Tabs3" data-toggle="tab">扩展函数</a></li>
        </ul>
        <div class="col-lg-4 col-md-4" style="height:536px;max-width:450px; overflow-y:auto;border:1px solid #ddd;border-right:none;padding:0px;">
            <div class="tab-content">
                <div class="tab-pane active" id="Tabs0" runat="server">
                    <div class="panel panel-default">
                       <div class="panel-heading">
                            <asp:DropDownList ID="CustomLabel_DP" runat="server" CssClass="form-control text_md" onchange="GetCustom(this);" EnableViewState="false"></asp:DropDownList>
                       </div>
                        <div class="panel-body" id="CustomLabel_div"></div>
                    </div>
                </div>
                <div class="tab-pane" id="Tabs1">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:DropDownList ID="Field_DP" runat="server" CssClass="form-control text_md" onchange="GetField(this);"  EnableViewState="false"></asp:DropDownList>
                        </div>
                        <div class="panel-body" id="Field_div"></div>
                    </div>
                </div>
                <div class="tab-pane" id="Tabs2">
                    <div class="list-group">
                        <asp:Label ID="lblSys" runat="server"  EnableViewState="false"></asp:Label>
                    </div>
                </div>
                <div class="tab-pane" id="Tabs3">
                    <div class="list-group">
                        <asp:Label ID="lblFun" runat="server"  EnableViewState="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-8 col-md-8" style="border:1px solid #ddd;padding:0px;">
            <iframe style="height: 30px; width: 530px; z-index: -1;border:none;" name="I1" id="I1" scrolling="no" src="/manage/Template/label.htm"></iframe>
            <div id="Textarea">
                <asp:TextBox TextMode="MultiLine" runat="server" ID="textContent" Style="max-width: 100%; width: 100%; height: 530px;" />
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-left:450px;">
              <asp:LinkButton ID="SaveTem_LB" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary">
                     <i class="fa fa-file-text"></i> 保存模板
                 </asp:LinkButton>
                    <a href="TemplateManage.aspx" class="btn btn-primary"><i class="fa fa-backward"></i> 返回列表</a>
            </td>
        </tr>
    </table>
    <div class="modal" id="userinfo_div">
        <div class="modal-dialog" style="width: 800px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <span class="modal-title"><strong id="title">用户信息</strong></span>
                </div>
                <div class="modal-body">
                      <iframe id="user_ifr" style="width:100%;height:600px;border:none;" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/Controls/ZL_Array.js"></script>
    <script src="/Plugins/CodeMirror/LabelCall.js"></script>
    <script src="/Design/JS/sea.js"></script>
    <script type="text/javascript">
        var base64 = null;
        seajs.use(["base64"], function (instance) {
            base64 = instance;
        })
        var diag = new ZL_Dialog();
        function opentitle(url, title)
        {
            diag.title = "修改标签";
            diag.url = url;
            diag.ShowModal();
            return false;
        }
        function closeCuModal() {
            diagLabel.CloseModal();
        }
    </script>
</asp:Content>