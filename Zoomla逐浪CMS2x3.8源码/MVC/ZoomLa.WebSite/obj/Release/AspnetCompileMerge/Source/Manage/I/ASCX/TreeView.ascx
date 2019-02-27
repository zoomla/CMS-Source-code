<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TreeView.ascx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.TreeView1" %>
<style>
.treediv{border:1px solid #ddd; padding:10px 0px;}
.treediv ul,.treediv li{list-style:none;min-height:30px;}
.treediv ul{margin-left:20px;}
.treediv ul .lastchild {padding-left:16px;background-position: 0 0; min-height:20px;}
.treediv ul ul{margin-top:5px;}
.treediv li{background: url(/Images/TreeLineImages/treedash.gif) 0 0 no-repeat; background-position: 0 -176px;}
.treediv li:last-child{background-position:-9999px -9999px;}
.treediv .treeicon{font-size:16px; cursor:pointer; color:#999;}
.tree_shrink ul ul{display:none;}
.tree_active{color:#999; }
</style>
<div class="treediv">
    <ul>
        <li><span class='fa fa-list-alt' style="color:#999;"></span><%=liAllTlp %></li>
        <asp:Literal ID="QuestType_Lit" runat="server" EnableViewState="false"></asp:Literal>
    </ul>   
</div>
<script>
    $().ready(function () {
        var isshrink = "<%=IsShrink %>";//是否收缩
        if (isshrink == "True") {
            $('.treediv').addClass('tree_shrink');
            $('.treediv li').removeClass('showchild');
            $('.treeicon').removeClass("fa fa-minus-circle");
            $('.treeicon').addClass("fa fa-plus-circle");
        }
        var nodeid = "<%=SelectedNode %>";//初始节点id(选中节点)
        SelectNode($('.treediv [data-id=' + nodeid + ']'));
        InitTreeEvent();
    });
    var activetlp = "<span class='fa fa-arrow-left tree_active'></span>";//选中模板
    //初始化树形事件
    function InitTreeEvent() {
        $(".treeicon").click(function () {
            var $ul = $(this).closest('li');
            if ($ul.hasClass("showchild")) {
                ShrinkNode($ul)
            } else {
                ExtendNode($ul);
            }
        });
        $(".treediv .tree_name").click(function () {
            $(".tree_active").remove();
            $(this).append(activetlp);
        });
    }
    //选中节点
    function SelectNode($obj) {
        var $gen = $obj.parents('li');
        while ($gen[0])//展开上一级节点
        {
            ExtendNode($gen);
            $gen = $gen.parents('li');
        }
        $obj.find('.tree_name').first().append(activetlp);
    }
    //展开节点
    function ExtendNode($gen) {
        $gen.addClass("showchild");
        $gen.find("ul:first").show();
        $gen.find(".treeicon").first().removeClass("fa fa-plus-circle");
        $gen.find(".treeicon").first().addClass("fa fa-minus-circle");
    }
    //收缩节点，参数为根节点(包含ul标签的li)
    function ShrinkNode($gen) {
        $gen.removeClass("showchild");
        $gen.find("ul:first").hide();
        $gen.find(".treeicon").first().removeClass("fa fa-minus-circle");
        $gen.find(".treeicon").first().addClass("fa fa-plus-circle");
    }
</script>