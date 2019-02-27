<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Design.Default" EnableViewState="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link href="/Design/res/css/main.css" rel="stylesheet" />
<title>工作台-开始您的精美站点设计</title>
<style type="text/css">.popover {max-width:none;}</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<nav class="navbar navbar-default design_top">
    <div class="container-fluid">
        <div class="navbar-header">
            <a class="navbar-brand" href="#">设计工作台</a>
        </div>
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav">
                <li class="cmd_labellist"><a href="javascript:;"><i class="fa fa-flag"></i> 组件</a></li>
                <li><a href="/Design/User/" target="_blank"><i class="fa fa-user"></i></a></li>
                <li class="active cmd_page"><a href="javascript:;"><i class="fa fa-paste"></i> 页面设置<span class="sr-only">(current)</span></a></li>
                <li class="cmd_addpage"><a href="javascript:;"><i class="fa fa-plus"></i> 新页面</a></li>
                <li><a href="/Design/h5/default.aspx" id="mobile_btn" target="_blank" title="进入手机制作"><i class="fa fa-mobile"></i></a></li>
                <li><a href="javascript:;" id="cfg_btn"><i class="fa fa-cog"></i></a></li>
            </ul>
            <ul class="nav navbar-nav navbar-right">
			    <li><a href="javascript:;">当前方案:<asp:Label runat="server" ID="TlpName_L" ForeColor="Red"></asp:Label></a></li>
                <li><a href="#" title="访问站点" target="_blank" runat="server" id="domain_a"><i class="fa fa-link"></i></a></li>
                <li><a href="javascript:;" title="保存" onclick="if (confirm('要保存当前修改吗')) { PreSave(); }"><i class="fa fa-save"></i></a></li>
                <li><a href="javascript:;" onclick="PreView();"><i class="fa fa-laptop"></i>预览</a></li>
                <li><a href="http://doc.z01.com/help/" target="_blank" title="帮助"><i class="fa fa-question-circle"></i></a></li>
            </ul>
        </div>
    </div>
</nav>
<div class="left-bar menu-collapsed-wrapper">
    <ul class="left-bar-items menu-collapsed">
        <li class="left-bar-item cmd_addbk" title="设置背景" data-cmd="addbk">
             <i class="fa fa-square-o" style="color:#F8A239;padding-left:2px;"></i><span class="fontsp">设置背景</span>
        </li>
        <li class="left-bar-item cmd_addcomp" style="color:#3797EA;" title="添加组件" data-cmd="addcomp">
             <i class="fa fa-plus-circle" style="padding-left: 1px;"></i><span class="fontsp">添加组件</span>
        </li>
        <li class="left-bar-item cmd_addlabel"><i class="fa fa-flag"></i><span class="fontsp">引入功能</span></li>
        <li class="left-bar-item cmd_addhtml" data-cmd="addhtml" title="插入Html">
           <i class="fa fa-code" style="color: #60BC57; padding-left: 2px;"></i><span class="fontsp">Html</span>
        </li>
        <li class="left-bar-item cmd_tlpshop" data-cmd="tlpshop" title="模板市场">
            <i class="fa fa-shopping-cart" style="color:#D0427C;padding-right:1px;"></i><span class="fontsp">模板市场</span>
        </li>
        <li class="left-bar-item" title="模板市场">
            <a href="h5/"><i class="fa fa-mobile-phone" style="color:#D0427C;padding-right:1px;"></i><span class="fontsp">微站场景</span></a>
        </li>
         <%-- <li class="left-bar-item disabled">
            <i class="fa fa-cloud-upload"></i><span class="fontsp">上传资源</span>
        </li>--%>
    </ul>
    <div></div>
</div>
<div style="right: 84px; top: 72px;" class="candrag rootcomp-toolbar slide-right custom-position">
    <div class="header">
        <div class="drag-zone">
            <i class="fa fa-lightbulb-o" title="提示"></i>
            <i class="fa fa-navicon" title="拖动"></i>
            <i class="fa fa-remove" title="关闭" onclick="$('.rootcomp-toolbar').hide();"></i>
        </div>
    </div>
    <div class="sperator"></div>
    <div class="inner button-grid">
        <i class="button disabled fa fa-copy"></i>
        <i class="button disabled fa fa-paste"></i>
        <i class="button disabled fa fa-file-text"></i>
        <i class="button disabled fa fa-trash"></i>
    </div>
    <div class="sperator"></div>
    <div class="inner button-grid">
        <i class="button fa fa-dedent" title="对齐"></i>
        <i class="button fa fa-align-justify" title="文字"></i>
    </div>
    <div class="sperator"></div>
    <div class="inner form">
        <i class="fontsp fa fa-send" title="角度"></i>
        <input type="text" value="0" class="form-control small" maxlength="4" />
    </div>
    <div class="sperator"></div>
    <div class="inner form">
        <div class="text-center">Size</div>
        <div>
            <span class="fontsp" title="宽">W</span>
            <input id="root_width_t" type="text" value="0" class="form-control small" maxlength="4" />
        </div>
        <div>
            <span class="fontsp" style="padding-left: 8px;" title="高">H</span>
            <input id="root_height_t" type="text" value="0" class="form-control small" maxlength="4" />
        </div>
    </div>
    <div class="sperator"></div>
    <div class="inner form">
        <div class="text-center">Position</div>
        <div>
            <span class="fontsp">X</span>
            <input id="root_x_t" type="text" value="0" class="form-control small" maxlength="4" />
        </div>
        <div>
            <span class="fontsp" style="padding-left: 8px;">Y</span>
            <input id="root_y_t" type="text" value="0" class="form-control small" maxlength="4" />
        </div>
    </div>
    <div class="sperator"></div>
</div>
<div class="editor-animation-manager-wrapper">
    <div class="editor-wrapper" style="position: static;">
        <iframe id="design_ifr" class="design_pc" onload="$(this).height($(window).height()-48)"></iframe>
    </div>
    <div id="newdiv" style="margin: 0 auto;"></div>
</div>
<div style="display:none;" id="iediv">
    <div>动力逐浪模块需要基于WebKit内核浏览器才能正常设计,推荐使用Chrome、FirFox及360、搜狗等兼容浏览器。<input type="button" value="我知道了" onclick="CloseDiag();" class="btn btn-warning" /></div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/jquery-ui.min.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script src="/JS/Modal/EventBase.js"></script>
<script src="/Design/JS/sea.js"></script>
<script src="/Design/JS/comp/page.js"></script>
<script id="testregion">
function ShowCompList() {
    for (var i = 0; i < page.compList.length; i++) {
        var model=page.compList[i];
        console.log(model.dataMod, model.config);
    }
}
var warndiag=new ZL_Dialog();
function ShowIE()
{
    var div=document.getElementById("iediv"); div.style.display="";
    warndiag.title='<i class="fa fa-adjust"></i> 提示';
    warndiag.body=div.outerHTML;
    warndiag.maxbtn=false;
    warndiag.backdrop=true;
    warndiag.ShowModal();
    $("#iediv").remove();
}
</script>
<script>
var lgDiag=new ZL_Dialog({backdrop:true,width:"wid1200"});lgDiag.maxbtn=false;
var ZLDE=null;
var sitecfg=<%=sitecfg%>;
var page = {};
$(function () {
    if (window.comdiag) {comdiag.maxbtn = false; }
    $(".cmd_addcomp").click(function () {CloseDiag();comdiag.reload=true;  ShowComDiag("Diag/AddComp.aspx", "添加组件"); });
    $(".cmd_addbk").click(function () {CloseDiag(); ShowComDiag("Diag/Background/AddBK.aspx", "设置背景"); });
    $(".cmd_addlabel").click(function(){CloseDiag(); lgDiag.reload=true;lgDiag.ShowModal("Diag/Label/LabelManage.aspx","引入标签");});
    $(".cmd_addhtml").click(function(){CloseDiag();lgDiag.reload=true;lgDiag.ShowModal("Diag/Label/InsertHtml.aspx","插入Html"); });
    $(".cmd_tlpshop").click(function(){CloseDiag();ShowDiag_L("User/TlpShop.aspx?mode=tlpshop","模板市场");});
   
    $(".cmd_domain").click(function(){CloseDiag();lgDiag.reload=true;lgDiag.ShowModal("Diag/Domain/MyDomain.aspx","我的域名");});
    $(".cmd_addpage").click(function(){CloseDiag();ShowComDiag("AddPage.aspx", "新建页面");});
    $(".cmd_page").click(function(){CloseDiag(); ShowDiag_L("Diag/Page.aspx?id="+page.guid, "页面配置");});
    $(".cmd_labellist").click(function(){CloseDiag();ShowComDiag("Diag/LabelList.aspx","标签管理");});
    //----------------------------------
    $(".candrag").draggable({});
    seajs.use(["page"], function (instance) {
        page = instance;
        ZLDE=seajs.require("base");
        page.guid = "<%:pageMod.guid%>";
        page.token = "<%:Session.SessionID%>";
        page.pageData = <%=pageMod.page%>;
        page.compData = <%=pageMod.comp%>;
        page.comp_global =<%=comp_global%>
        page.extendData = <%=extendData%>;

        //------初始化End
        $("#design_ifr").load(function(){
            page.instance = $(this).contents();
            page.init();
            for (var i = 0; i < page.compList.length; i++) {
                page.compList[i].mode="design";
                IfrHelper.addDomToIfr(page.compList[i]); 
            }
        });
        $("#design_ifr").attr("src","Editor/Editor.aspx?id="+ page.guid);
    });//seajs end;
    //-----事件监测
    eventBase.add("editor_update",function(param){
        IfrHelper.getIfrObj().editor.scope.$digest();
    });
    //-----绑定弹出
    $("#cfg_btn").popover({
        animation: true,html:true,trigger:"click",placement: 'bottom',
        content:function(){return $pop_cfg;}
    });
})
//----------------------------
var $pop_cfg=$('<div style="width:150px;"><div><label><input type="checkbox" onclick="hide_global(this);" />隐藏全局组件</label></div><div><label><input type="checkbox" onclick="hide_toolbar(this);"/>隐藏工具栏</label></div></div>');
function hide_global(obj)
{
    var hide=function (type, ishide) {
        //global
        for (var i = 0; i < page.compList.length; i++) {
            var comp = page.compList[i];
            if (!comp.config._conf) continue;
            if (comp.config._conf.type == type) {
                if (ishide) { IfrHelper.find("#"+comp.id).hide(); }
                else { IfrHelper.find("#"+comp.id).show(); }
            }
        }
    }
    hide("global",obj.checked);
}
function hide_toolbar(obj)
{
    if(obj.checked){$(".rootcomp-toolbar").hide();}
    else{$(".rootcomp-toolbar").show();}
}
//----------------------------
var waitDiag = new ZL_Dialog();
var loginDG=new ZL_Dialog();
waitDiag.closebtn = false; waitDiag.backdrop = true;
//附带具体的信息
function AddComponent(model) {
    CloseDiag();
    //1,根据类型获取到对应的组件对象,并将对象初始化
    var compObj = page.GetCompObj(model);
    //2,将其附加入目标页面,由页面html引擎解析,并将其初始化展示
    IfrHelper.addDomToIfr(compObj);
    page.compList.push(compObj);
}
function CopyComp(comp)
{
    var model=ZLDE.utils.clone(comp,model);
    var compObj = page.GetCompObj({ "dataMod": model.dataMod, "config": model.config });
    IfrHelper.addDomToIfr(compObj);
    page.compList.push(compObj);
}
function PreSave(callback) {
    waitDiag.ShowMask("正在保存中");
    page.PreSave(function (data) {
        waitDiag.CloseModal();
        page.guid = data;
        if(callback){callback(data);}
    });
}
//先保存,后支持预览
function PreView() {
    //1,显示iframe,并引导跳转
    //2,先弹出窗口,完后刷新页面(修改URL)
    var win= window.open("/design/wait.html?ver="+Math.random() , "_preview");
    waitDiag.ShowMask("正在准备预览");
    page.PreSave(function (data) {
        page.guid = data;
        CloseDiag(); 
        setTimeout(function(){
            win.location="/Design/PreView.aspx?id=" + page.guid;
        },50);
    });
}
function CloseDiag()
{
    comdiag.CloseModal();
    lgDiag.CloseModal();
    waitDiag.CloseModal();
    warndiag.CloseModal();
    //$(".left-bar-item").popover("hide");
    try{ IfrHelper.getIfrObj().CloseDiag();}catch(ex){console.log(ex.message);}
}
function AjaxLogin() {
    loginDG.title="用户登录";
    loginDG.url = "/login_Ajax.aspx";
    loginDG.maxbtn = false;
    loginDG.width = "width350";
    loginDG.closebtn = false;
    loginDG.backdrop = true;
    loginDG.ShowModal();
}
function LoginSuccess(model) {
    $("#uname_sp").text(model.name);
    loginDG.CloseModal();
}
function ShowDiag_L(url,title)
{
    CloseDiag();
    lgDiag.reload=true;
    lgDiag.ShowModal(url,title);
}
//----------------------------
function EventBind()
{
    IfrHelper.getIfrObj().EventBind();
}
//----------------------------Tools
var IfrHelper = {
    config: { id: "design_ifr" },
    addDomToIfr: function (compObj) {
        var _ref = this;
        var obj = _ref.getIfrObj();
        obj.editor.scope.addDom(compObj);
        setTimeout(function () {
            compObj.UpdateRootPanel();
            obj.EventBind();
        }, 500);
    },
    getIfrObj: function () {
        //返回的这个对象,用于直接调用ifr中的方法
        var _ref = this;
        return document.getElementById(_ref.config.id).contentWindow;
    },
    find: function (filter) {
        //根据filter,jquery筛选返回,必须加载完后再执行
        var _ref = this;
        return $("#" + _ref.config.id).contents().find(filter);
    }
};
</script>
</asp:Content>