<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddComp.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.AddComp" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link rel="stylesheet" href="/Design/res/css/edit/common.css" />
<link rel="stylesheet" href="/Design/res/css/comp.css" />
<script src="/Plugins/Ueditor/design.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<style type="text/css">
.bdshare-button-style2-32 a {
            height:32px;line-height:32px;display:block;width:32px;float:left;
            background-image:url(http://bdimg.share.baidu.com/static/api/img/share/icons_2_32.png?v=1bc5c881.png);
            background-repeat:no-repeat;cursor:pointer;margin:6px 6px 6px 0;text-indent:-100em;overflow:hidden;
        }
.category-view-wrapper {padding-bottom:50px;margin-left:75px;}
.itemImg {height:150px;width:100%;}
</style>
<title>添加组件</title>
    
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="addPanel" class="left-panel-frame add-panel" style="height:500px;overflow-y:auto;">
    <ul class="category-list"  style="min-height:515px;position:fixed;left:0px;">
        <asp:Repeater runat="server" ID="CateRPT" EnableViewState="false">
            <ItemTemplate>
                <li class="category <%#Eval("Name") %>" data-type="<%#Eval("Name") %>">
                    <span class="category-name-wrapper">
                        <span class="category-name"><%#Eval("Alias") %></span>
                    </span>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
        <div id="compview" class="category-view-wrapper"></div>
        <div id="uedior_div" class="category-view-wrapper" style="width:960px;position:absolute;">
            <textarea id="content_t" style="height:380px;"></textarea>
            <div class="margint5 text-center">
                <input type="button" value="保存" class="btn btn-primary" onclick="UEditor.add();" />
                <input type="button" value="取消" class="btn btn-default" onclick="UEditor.clear();" />
            </div>
        </div>
    </div>
       </asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
    var stype = "<%:SType%>";
    $(function () {
        $(".category").click(function () {
            $(".category-name-wrapper").removeClass("is-selected");
            $(this).find(".category-name-wrapper").addClass("is-selected");
            //加载不同的html
            var type = $(this).data("type");
            switch (type) {
                case "ueditor":
                    $("#compview").hide();
                    $("#uedior_div").show();
                    UE.getEditor("content_t", {});
                    break;
                default:
                    $("#uedior_div").hide();
                    $("#compview").show();
                    $("#compview").load("/Design/Diag/" + type + "/Add.html?m=" + Math.random(), function () {
                        //绑定添加元素 
                        $(".domItem").click(function () {
                            var model = GetModel(this);//页面实现
                            AddToPage(model);
                        });
                    });
                    break;
            }
        });
        $(".category:first").click();
    })
    //将组件信息,传递给页面生成
    function AddToPage(model) {
        switch (stype) {
            case "mobile":
                break;
            case "scence"://场景对部分组件单独处理
                {
                    switch (model.config.type)
                    {
                        case "image":
                            model.config.imgstyle = "width:300px;height:300px;";
                            break;
                        case "text":
                            model.config.style = "font-size:60px;position:absolute;";
                            break;
                    }
                }
                break;
            default:
                //部分组件以fixed定位或支持自定义位置
                var style = model.config.style;
                if (style.indexOf("position:") < 0) {
                    model.config.style += "position:absolute;";
                }
                if (style.indexOf("top:") < 0 && style.indexOf("bottom:") < 0) {
                    var top = ((window.screen.height / 2) - 200) + $(parent.IfrHelper.getIfrObj()).scrollTop();
                    model.config.style += "top:" + top + "px;"
                }
                break;
        }
        parent.AddComponent(model);
    }
    //-------------------------------
    var UEditor = {
        clear: function () { if (confirm("确定要关闭吗")) { parent.closeDiag(); } },
        add: function () {
            var model = {
                dataMod: { html: "" },
                config: { type: "ueditor", css: "candrag", style: "" }
            };
            model.dataMod.html = UE.getEditor("content_t").getContent();
            AddToPage(model);
        }
    };
    //用于规范和统一模型(dataMod-->list|model)
    function GetItemModel(num, type) {
        var list = [];
        num = Convert.ToInt(num, 1);
        var getModelByType = function (type) {
            //url:图片路径,css应用时的状态,name==text:用于承载名称或字符,tip:""提示
            return { name: "", tip: "", orderid: 0, url: "", href: "#", target: "", css: "", fa: "", show: true, addon: "" };
        }
        for (var i = 0; i < num; i++) {
            var item = getModelByType(type);
            item.orderid = i;
            list.push(item);
        }
        return list;
    }
</script>
</asp:Content>