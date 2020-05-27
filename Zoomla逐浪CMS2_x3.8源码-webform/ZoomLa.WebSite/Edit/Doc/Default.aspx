<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="test_doc" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>文档管理</title>
<meta charset="utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
<meta name="renderer" content="webkit" />
<link rel="stylesheet" type="text/css" href="Doc_files/fui.min.css" />
<link rel="stylesheet" href="Doc_files/wordonline.min.css" />
<link rel="stylesheet" href="Doc_files/public.css" />
<link rel="stylesheet" href="Doc_files/index.css" /><!--滚动条-->
<script src="/JS/jquery-1.11.1.min.js"></script>
<script src="/Plugins/Ueditor/ueditor.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.js"></script>
<script src="/Plugins/Ueditor/third-party/zeroclipboard/ZeroClipboard.js"></script>
<script src="Doc_files/jhtmls.min.js"></script>
<script src="Doc_files/fui.all.min.js"></script>
<script src="Doc_files/wordonline.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <%--<div id="btndiv" style="z-index: 10000000">
            <input type="button" value="创建书签" onclick="createBookmark();" />
            <input type="button" value="移动至书签处" onclick="moveToBookmark();" />
            <input type="button" value="选区行内样式" onclick="applyInlineStyle();" />
        </div>--%>
        <div id="content-wrapper">
            <div id="header" class="header"></div>
            <div id="toolbar" class="toolbar"></div>
            <div class="main">
                <div class="container editor">
                    <script id="wordeditor" class="wordeditor" type="text/plain" style="width: 900px; height: 1110px;"></script>
                </div>
            </div>
        </div>
<script>
    var mark = null;
    function createBookmark() {
        var rng = editor.selection.getRange();
        mark = rng.createBookmark(true);
    }
    function moveToBookmark() {
        //获取全局选区,再移到光标至书签
        editor.selection._bakRange.moveToBookmark(mark).select();
        editor.fireEvent("selectionChange");
    }
    function getNodeIndex() {
       
    }
    function applyInlineStyle() {
        var rng = editor.selection.getRange();
        rng.applyInlineStyle("strong", { "style": "color:red;" });
    }
</script>
<script type="text/javascript">
    var fio = {};
    fio.user = {};
    fio.user.logout = function () { };
    fio.user.on = function () { };
    var editor = null;
    $(function () {
        WO.render('toolbar', 'wordeditor');
        setTimeout(function () {
            $docBody = $("#ueditor_0").contents().find("body");
        }, 2000);
        editor.addListener("afterPaste contentChange", function () {
            if (updateDoc.posting == true && updateDoc.cursor == true) {
                updateDoc.posting = false;
                var html = editor.getContent();
                $.post("UpdateDoc.aspx", { "html": html }, function (data) {
                    //提交后更新版本
                    dataPack.version = data;
                    updateDoc.posting = true;
                });
            }
        });
        //光标位置变更事件
        editor.addListener("selectionChange", function () {
            //console.log(editor.selection.getStart());//选区的开始位置
            //console.log(editor.selection.getRange());
            //range.selectNode(ci).select();
            if (cursorFlag == true && $docBody != null) {
                $docBody.find(".doc_cursor[data-uid=" + mu.UserID + "]").remove();
                cursorFlag = false;
                //不能直接用命令插入html,会覆盖掉选中,且会自动将html内容放入其中,导致无法移除
                var rng = editor.selection.getRange();
                var $node = $(myCursor);
                //var $node = $(editor.document.createElement("span"));

                rng.insertNode($node[0]);
                //rng.applyInlineStyle("strong", { "style": "color:red;" });
                setTimeout(function () { cursorFlag = true; }, 100);
            }
        })
    });
</script>
<script src="/JS/Controls/B_User.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<link href="/dist/css/bootstrap.min.css" rel="stylesheet" />
<script src="/dist/js/bootstrap.min.js"></script>
<script id="polling">
    var mu = null;
    var buser = new B_User();
    buser.IsLogged(function (data) {
        mu = JSON.parse(data);
        myCursor = "<span class='doc_cursor' style='border-color: rgb(35, 132, 209);' data-uid='" + mu.UserID + "' data-name='" + mu.HoneyName + "' data-face='" + mu.UserFace + "'></span>"
    });
    //信息
    var dataPack = { sessionID: "<%=Session.SessionID%>", version: 0 };
    var myCursor = ""
    //是否更新文档(更新中锁定,更新光标锁定),有任一为false则不否许更新与提交
    var updateDoc = { posting: true, cursor: true };
    ////光标指针
    var cursorFlag = true;
    //编辑器元素
    $docBody = null;
    setInterval(function () {
        $.post("Default.aspx", dataPack, function (data) {
            if (data == "0") { }
            else
            {
                data = JSON.parse(data);
                //关闭更新监听
                updateDoc.cursor = false;
                editor.setContent(data.html);
                dataPack.version = data.version;
                if ($docBody != null) {
                    //更新绑定修改人
                    $docBody.find(".doc_cursor").each(function () {
                        var $ref = $(this);
                        if ($ref.data("uid") == mu.UserID) { return; }
                        var html = "<span class=\"Sf-zg-Ie\" style=\"opacity: 0.83; background-color: rgb(230, 104, 91);\">"
                            + "<span style=\"left: 0px; width: 12px; height: 12px; vertical-align: middle; display: inline-block;\">"
                            + "<img style=\"width: 100%; height: 100%;\" src=\"" + $ref.data("face") + "\" onerror=\"this.src='/Images/userface/noface.png';\";>"
                            + "</span>"
                            + "<span style=\"padding-left: 2px; vertical-align: middle; display: inline-block;\">∞" + $ref.data("name") + "</span>"
                            + "</span>";
                        $ref.append(html);
                    });
                }
                updateDoc.cursor = true;
            }
        });
    }, 500);
</script>
<script id="tools">
    var loginDG = new ZL_Dialog();
    function AjaxLogin() {
        loginDG.title = "用户登录";
        loginDG.url = "/login_Ajax.aspx";
        loginDG.maxbtn = false;
        loginDG.width = "width350";
        loginDG.closebtn = false;
        loginDG.backdrop = true;
        loginDG.ShowModal();
    }
    function LoginSuccess()
    {
        location = location;
    }
</script>
<script id="longconnection">
    //客户端数据
<%--    var dataPack = { sessionID: "<%=Session.SessionID%>", version: 0, time: 30 };
    var updateDoc = true;--%>
    //function CreateAJAX() {
    //    console.log("创建连接");
    //    $.ajax({
    //        type: "POST",
    //        url: "doc.aspx",
    //        //dataType: "json",
    //        data: dataPack,
    //        //timeout: 10000,
    //        success: function (data, textStatus) {
    //            //超时
    //            if (data == "0") {
    //                console.log("超时");
    //                CreateAJAX();
    //            }
    //            else {
    //                data = JSON.parse(data);
    //                //关闭监听并更新文档
    //                updateDoc = false;
    //                editor.setContent(data.html);
    //                dataPack.version = data.version;
    //                console.log("接收信息", data, dataPack);
    //                updateDoc = true;
    //                CreateAJAX();
    //            }
    //        },
    //        complete: function (XMLHttpRequest, textStatus) {
    //            if (XMLHttpRequest.readyState == "4") {

    //            }
    //        },
    //        error: function (XMLHttpRequest, textStatus, errorThrown) {
    //            console.log("出错");
    //            console.log(XMLHttpRequest, textStatus, errorThrown);
    //            //CreateAJAX();
    //        }
    //    });
    //}
    //CreateAJAX();
</script>
    </form>
</body>
</html>
