//现在为直接在h标签上加id,后期可依需要,改为在其旁边加上<a href name="">的方式
var BaiKe = {
    config: { id: "mycontent", dirid: "baike_dir", navid: "baike_nav", level: ["h1", "h2", "h3", "h4"], tags: ["h1", "h2", "h3", "h4"], titleLen: 20 },
    Init: function () { this.GetDirs(); },
    GetNextTag: function (tag) {//返回下一级tag
        var dircof = this.config;
        tag = tag.toLowerCase();
        for (var i = 0; i < dircof.tags.length; i++) {
            if (tag == dircof.tags[i] && i < (dircof.tags.length - 1)) {
                return dircof.tags[(i + 1)];
            }
        }
        return "";
    },
    GetChilds: function ($node,tag) {
        var childTag = BaiKe.GetNextTag(tag);
        while (childTag != "") {
            //查找其的子元素
            if (childTag == "") { continue; }
            var child = ($node.nextUntil(tag, childTag));//查找两个同级元素之间的数据
            if (child && child.length > 0)
            {
                return child;
            }
            childTag = BaiKe.GetNextTag(childTag);
        }
        return [];
    },
    GetDirs: function () {//按层级递归获取目录
        var ref = this;
        var list = ref.GetList();
        BaiKe.CreateDirTree(list);
        BaiKe.CreateNavUI(list);
    },
    CreateDirTree: function (list) {//百科顶部目录UI
        var count = 7;//多少换一列
        var html = $("<div>"), ulitem = $('<ul class="dirul">');
        var $ul = (traveList(list, 1) || null);
        var $lis = $ul.find("li");
        //有没附加上的
        if ($lis.length <= count) {html.append($('<ul class="dirul">').append($lis)); }
        else //将其按数目分成UL
        {
            for (var i = 0; i < $lis.length; i++) {
                ulitem.append($lis[i]);
                if (i != 0 && i % count == 0) {
                    html.append(ulitem);
                    ulitem = "";
                    ulitem = $('<ul class="dirul">');
                }
            }//for end;
            if (ulitem.find("li").length > 0)
            { html.append(ulitem); }
        }
        //查找ul下li数量,对其进行平均划分
        $("#" + BaiKe.config.dirid).html(html.html());
        function traveList(section, dep) {
            var $list, $item, $itemContent, child, childList;
            if (section.children.length) {
                $list = $('<ul>');
                for (var i = 0; i < section.children.length; i++) {
                    child = section.children[i];
                    //设置目录节点内容标签
                    var title = getSubStr(child['title'], BaiKe.config.titleLen);
                    title = (dep == 1 ? child.index + " " + title : title);
                    $itemContent = $('<div class="sectionItem"></div>').html($('<a class="itemTitle level' + dep + '" href="#' + (child.obj.attr("id")) + '">' + title + '</a>'));
                    //$itemContent.attr('data-address', child['startAddress'].join(','));//其下子元素数量
                    //$itemContent.append($('<a href="#' + (child.obj.attr("id")) + '" class="selectIcon">测试</a>'));//操作
                    //dirmap[child['startAddress'].join(',')] = child;
                    //设置目录节点容器标签
                    $item = $('<li>');
                    $item.append($itemContent);//将其附加进li中
                    //继续遍历子节点
                    if ($item.children.length) {
                        childList = traveList(child, (dep + 1));
                        childList && $item.append(childList);
                    }
                    $list.append($item);
                }
            }
            return $list;
        }
    },
    CreateNavUI: function (list) {//百科边栏目录UI
        var html = traveList(list, 1).find("li");
        $("#" + BaiKe.config.navid).html(html);
        function traveList(section, dep) {
            var $list, $item, $itemContent, child, childList;
            if (section.children.length) {
                $list = $('<ul>');
                for (var i = 0; i < section.children.length; i++) {
                    child = section.children[i];
                    //设置目录节点内容标签
                    var title = getSubStr(child['title'], BaiKe.config.titleLen);
                    $itemContent = $('<a class="navTitle level' + dep + '" href="#' + (child.obj.attr("id")) + '">' + child.index + " " + title + '</a>');
                    //设置目录节点容器标签
                    $item = $('<li>');
                    $item.append($itemContent);//将其附加进li中
                    //继续遍历子节点
                    if ($item.children.length) {
                        childList = traveList(child, (dep + 1));
                        childList && $item.append(childList);
                    }
                    $list.append($item);
                }
            }
            return $list;
        }
    },
    GetList: function () {
        //获取数据,并整理为list模型
        var ref = this;
        var dircof = ref.config;
        var list = ref.getModel();
        var body = $("#" + dircof.id);
        var topNodes = [];
        for (var i = 0; i < dircof.tags.length; i++) {
            dircof.level[i] = body.find(dircof.tags[i]);
            if (topNodes.length < 1 && dircof.level[i].length > 0) {
                //赋值第一级节点
                topNodes = dircof.level[i];
            }
        }
        if (topNodes.length < 1) { return; }
        FillList(list, topNodes, 1);
        //父模型,子元素,深度
        function FillList(parent, children, dep) {
            for (var i = 0; i < children.length; i++) {
                var node = children[i], $node = $(children[i]);
                $node.attr("id", GetRanPass(6));
                //index序号
                var index = (parent.index == "" ? "" : parent.index + ".") + (i + 1);
                var model = { tag: node.tagName, "title": $node.text(), level: dep, "index": index, obj: $node, children: [] };
                parent.children.push(model);
                var child = BaiKe.GetChilds($node, model.tag);
                if (child.length > 0) {
                    FillList(model, child, (dep + 1));
                }
            }
        }
        return list;
    },
    getModel: function () { return { tag: "", title: "root", level: -1, index: "", obj: null, children: [] }; }
}
function getSubStr(s, l) {
    var i = 0, len = 0;
    for (i; i < s.length; i++) {
        if (s.charAt(i).match(/[^\x00-\xff]/g) != null) {
            len += 2;
        } else {
            len++;
        }
        if (len > l) { break; }
    } return s.substr(0, i);
};

////------------根据百科编辑器,生成层级树(disuse)
//var resetHandler = function () {
//    var dirmap = {}, dir = myue.execCommand('getsections');
//    var list = { tag: "", title: "root", level: -1, index: "", children: [] };
//    GetDirByUE(list, dir.children);
//    //console.log(JSON.stringify(dir));
//    // 更新目录树
//    $("#indexlist").html(traversal(dir, 1) || null);
//    // 选中章节按钮
//    $('.selectIcon').click(function (e) {
//        var $target = $(this),
//                address = $target.parent().attr('data-address');
//        myue.execCommand('selectsection', dirmap[address], true);
//    });
//    function traversal(section, dep) {
//        var $list, $item, $itemContent, child, childList;
//        if (section.children.length) {
//            $list = $('<ul>');
//            for (var i = 0; i < section.children.length; i++) {
//                child = section.children[i];
//                //设置目录节点内容标签
//                var title = getSubStr(child['title'], 18);
//                $itemContent = $('<div class="sectionItem"></div>').html($('<span class="itemTitle level' + dep + '">' + title + '</span>'));
//                $itemContent.attr('data-address', child['startAddress'].join(','));//其下子元素数量
//                $itemContent.append($('<input type="button" value="选择" class="selectIcon">'));//操作
//                dirmap[child['startAddress'].join(',')] = child;
//                //设置目录节点容器标签
//                $item = $('<li>');
//                $item.append($itemContent);//将其附加进li中
//                //继续遍历子节点
//                if ($item.children.length) {
//                    childList = traversal(child, (dep + 1));
//                    childList && $item.append(childList);
//                }
//                $list.append($item);
//            }
//        }
//        return $list;
//    }
//}
//function GetDir() {
//    var dirmap = {}, dir = myue.execCommand('getsections');
//    var list = { tag: "", title: "root", level: -1, children: [] };
//    GetDirByUE(list, dir.children);
//    CreateDirTree(list);
//}
////根据UE的层级生成目录Json,将其中的需要的数据拷过来即可
//function GetDirByUE(parent, children) {
//    for (var i = 0; i < children.length; i++) {
//        var dir = children[i];
//        var model = { tag: "", title: "", level: -1, index: "", children: [] };
//        model.tag = dir.tag;
//        model.title = dir.title;
//        model.level = dir.level;
//        model.index = GetRandomNum(6);
//        if (dir.children && dir.children.length > 0) {
//            GetDirByUE(model, dir.children);
//        }
//        parent.children.push(model);
//    }
//}

//------
//<nav id="baike_div">
//          <ul class="nav" id="baike_nav">
//          <%--<li><a href="#fat">@fat</a></li>--%>
//        </ul>
//</nav>
//此属性的值为任何Bootstrap中.nav组件的父元素的ID或class。
/*-----------------------------------*/
var info = { id: "#info_tb", data: [] };
info.editTlp = '<tr><td class="td_l"><input type="text"  maxlength="6" placeholder="名称" value="@name" class="form-control info_name"></td><td><input type="text" placeholder="请输入信息" maxlength="20" class="form-control info_val" value="@val" /></td>';
info.editTlp += '<td style="width:150px;"><a href="javascript:;" class="btn btn-default" onclick="info.upRow(this)"><i class="fa fa-arrow-up"></i></a> <a href="javascript:;" class="btn btn-default" onclick="info.downRow(this)"><i class="fa fa-arrow-down"></i></a> <a class="btn btn-danger" title="移除" onclick="info.delRow(this);"><i class="fa fa-remove"></i></a></td></tr>';
info.htmlTlp = '<div class="info_li_div"><span class="info_l">@name</span><span class="info_r">@val</span></div>';
//显示用于浏览的Html
info.dataToHtml = function () {
    var html = "";
    var $lis = [];
    var addTolis = function (html) { html += "<div class='clearfix'></di>"; $li = $("<li>"); $li.append(html); html = ""; $lis.push($li); }
    for (var i = 1; i <= info.data.length; i++) {
        var model = info.data[(i-1)];
        if (i % 2 != 0) {
            html = info.htmlTlp.replace(/@name/ig, model.name).replace("@val", model.val);
        }
        else {
            html += info.htmlTlp.replace(/@name/ig, model.name).replace("@val", model.val);
            addTolis(html); html = "";
        }
    }
    if (html != "") { addTolis(html);}
    $(info.id).append($lis);
}
//显示编辑Html
info.dataToEdit = function () {
    var $items = JsonHelper.FillItem(info.editTlp, info.data);
    $(info.id).append($items);
}
info.addRow = function (model) {
    if (!model) { model = info.getModel(); }
    if (!model.val) { model.val = ""; }
    $item = JsonHelper.FillItem(info.editTlp, model);
    $(info.id).append($item);
}
info.delRow = function (obj) { $(obj).closest("tr").remove(); }
info.upRow = function (obj) {
    var $tr = $(obj).closest("tr");
    if ($tr[0] == $(info.id + " tr:first")[0]) { return; }
    $tr.insertBefore($tr.prev());
}
info.downRow = function (obj) {
    var $tr = $(obj).closest("tr");
    if ($tr[0] == $(info.id+" tr:last")[0]) { return; }
    $tr.insertAfter($tr.next());
}
info.getModel = function () { return { name: "", val: "" }; }
info.preSave = function () {
    var $trs = $(info.id).find("tr");
    info.data = [];
    for (var i = 0; i < $trs.length; i++) {
        var $tr = $($trs[i]);
        var model = info.getModel();
        model.name = $tr.find(".info_name:first").val();
        model.val = $tr.find(".info_val:first").val();
        if (ZL_Regex.isEmpty(model.name) || ZL_Regex.isEmpty(model.val)) { continue; }
        else { info.data.push(model); }
    }
    $("#info_hid").val(JSON.stringify(info.data));
}
/*-----------------------------------*/
var refence = { id: "#ref_body", data: [] };
refence.editTlp = "<div class=\"ref_item\"><div>"
            + "<div>@name．@siteName</div>"
            + "<div class=\"pull-right\">"
            + "<a onclick=\"refence.showEdit('@id');\"><i class=\"fa fa-pencil\"></i>编辑</a> "
            + "<a onclick=\"refence.del('@id');\"><i class=\"fa fa-remove\"></i>删除</a>"
            + "</div></div>"
            + "<div class=\"item_url_div\"><a href=\"@url\" class=\"item_url\" target=\"_blank\">@url</a></div>"
            + "</div>";
refence.htmlTlp = "<div class=\"ref_item\">"
                + "@oid.<a href=\"@url\" class=\"item_url\" target=\"_blank\">@name <i class=\"fa fa-share\" style=\"color:#136ec2;\"></i></a>"
                + ". <span>@siteName</span> . <span>[@cdate]</span>"
                + "</div>";
refence.get = function (id) {
    return refence.data.GetByID(id);
}
refence.showAdd = function () {
    comdiag.reload = true;
    ShowComDiag("/Baike/AddRef", "添加参考");
}
refence.add = function (model) {
    refence.data.push(model);
    refence.render();
}
refence.update = function (model) {
    refence.data.UpdateByID(model);
    refence.render();
}
refence.showEdit = function (id) {
    comdiag.reload = true;
    ShowComDiag("/Baike/AddRef?id=" + id, "修改参考");
}
refence.dataToEdit = function () {
    var $items = JsonHelper.FillItem(refence.editTlp, refence.data);
    $(refence.id).append($items);
}
refence.dataToHtml = function () {
    for (var i = 0; i < refence.data.length; i++) {
        refence.data[i].oid = (i + 1);
    }
    var $items = JsonHelper.FillItem(refence.htmlTlp, refence.data);
    $(refence.id).append($items);
}
refence.del = function (id) {
    if (confirm("确定要删除吗?")) {
        refence.data.RemoveByID(id);
        refence.render();
    }
}
//----
refence.preSave = function () { $("#refence_hid").val(JSON.stringify(refence.data)); }
refence.render = function () {
    $(refence.id).html("");
    $(refence.id).append(JsonHelper.FillItem(refence.editTlp, refence.data));
}