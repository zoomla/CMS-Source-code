<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormDesign.aspx.cs" Inherits="Manage_Pub_FormDesign" ValidateRequest="false"%><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <script src="/JS/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="/JS/ZLForm.js"></script>
    <script type="text/javascript" src="/JS/jquery-ui.min.js"></script>
    <script type="text/javascript" src="/dist/js/bootstrap.min.js"></script>
    <script src="/JS/chinese.js"></script>
    <link href="/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .container{font-family:'Microsoft YaHei';}
        ul{padding-left:0px;}
        ul li{list-style:none;}
        /*表单呈现区*/
        #formDesign_Div{background:url(/App_Themes/AdminDefaultTheme/images/PubForm/bg.jpg) no-repeat center top;padding:10px;padding-top:5px;padding-bottom:20px;}
        #myform{color:#6f5f5c;background:#FFF; box-shadow:0px 1px 6px rgba(0, 0, 0, 0.44);}
        .formtitle{text-align:center;padding:5px;font-size:24px;font-weight:bold; color:white;line-height:30px;background-color:#724b68;}
        .formintro{font-size:12px;color:white;text-align:center;background-color:#724b68;}        
        .formli{padding:5px;border:1px solid transparent;}
        .formli:hover{border:1px dashed gray;}
        .removeli{color:red;cursor:pointer;float:right;width:20px;height:20px;}
        .removelibg{display:none;}
        /*元素属性区*/
        #Tabs1 ul li{float:left;cursor:pointer;text-align:center;border:1px dashed #ddd;}
        #Tabs1 ul li:hover{border:1px solid #57A5CA;background-color:#D0E2F3;}
        .txtli{width:80px;height:50px;line-height:50px;margin:0 5px 5px 0;}
        .EditTitle{font-size:14px;font-weight:bold;}
        .EditContent{padding-left:20px;}
        .conli{width:120px;height:60px;line-height:60px;margin:0 5px 5px 0;padding:5px;}
        .option{background:url(/App_Themes/AdminDefaultTheme/images/PubForm/option.png) no-repeat;display:inline-block;width:34px;height:30px;float:right;margin-top:17px;}
        .option_input_txt{background-position:center -4px;}
        .option_text{background-position:center -73px;}
        .option_radio{background-position:center -107px;}
        .option_checkbox{background-position:center -38px;}
        .option_select{background-position:center -142px;}
        .option_img{background-position:center -352px;}
        /*表单元素CSS*/
        .com_required{color:red;padding-left:5px;}
        .input{border:1px solid #ddd;padding-left:5px;}
        .textarea{border:1px solid #ddd;padding-left:5px;resize:none;height:120px;}
        .intro{font-size:12px;border-top:1px dashed #ddd;padding:5px;}
        .opbtn{cursor:pointer;background:#ddd;color:white;padding:3px;margin-right:3px;border-radius:50%;}
        .opbtn:hover{background-color:orange;}
        .large {width:90%;}
        .middle{width:60%;}
        .small {width:30%;}
        input[name=sel_rad], input[name=sel_chk]{margin-right:3px;}
        .img_normal{width:100%;height:200px;}
        /*杂项*/
        .highlight{border:2px dashed black;background-color:#ddd; height:60px; line-height: 60px;}
    </style>
    <title>表单设计</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="width:95%;">
            <div class="row">
                <div id="formDesign_Div" class="col-lg-6 col-md-6 col-xs-6">
                    <div id="myform" style="height:850px;overflow-y:scroll;overflow-x:hidden;">
                        <div class="formtitle">逐浪年会满意度调研</div>
                        <div class="formintro"></div>
                         <ul id="formul">
                        </ul>
                    </div>
                    <div style="text-align:center;margin-top:5px;">
                        <asp:Button runat="server" Text="保存表单" ID="SaveBtn" CssClass="btn btn-primary" OnClick="SaveBtn_Click" OnClientClick="return SaveForm();"/>
                    </div>
                    <asp:HiddenField runat="server" ID="Json_Hid" />
                </div>
                <div id="tableAttr_Div" class="col-lg-4  col-md-4 col-xs-4">
                  <ul class="nav nav-tabs">
                     <li><a href="#Tabs0" data-toggle="tab">表单设置</a></li>
                     <li class="active"><a href="#Tabs1" data-toggle="tab">添加组件</a></li>
                     <li><a href="#Tabs2" id="tabs2_href" data-toggle="tab">编辑组件</a></li>
                  </ul>
                    <div class="tab-content">
                        <div id="Tabs0" class="tab-pane">
                            <div style="text-align:center;font-size:16px;">表单设置</div>
                            <p>标题</p>
                            <div><asp:TextBox runat="server" ID="Title_T" class="form-control" Text="逐浪年会满意度调研" onkeyup="$('.formtitle').text($(this).val());Getpy('Title_T','PubName_T'); " /></div>
                            <p>表名</p>
                            <div>ZL_Pub_<asp:TextBox runat="server" ID="PubName_T" Text="zlmed" CssClass="form-control" style="width:200px;display:inline;" /></div>
                            <p>描述文字</p>
                            <div><asp:TextBox runat="server" ID="Intro_T" class="form-control" onkeyup="$('.formintro').text($(this).val());"/></div>
                        </div>
                        <div id="Tabs1" class="tab-pane active">
                            <div>
                                 <p><label>表单组件</label></p>
                                <ul>
                                   <li class="conli" data-type="input_txt"><span>单行文本</span><span class="option option_input_txt"></span></li>
                                   <li class="conli" data-type="input_text"><span>多行文本</span><span class="option option_text"></span></li>
                                   <li class="conli" data-type="radio"><span>单选项</span><span class="option option_radio"></span></li>
                                   <li class="conli" data-type="checkbox"><span>多选项</span><span class="option option_checkbox"></span></li>
                                   <li class="conli" data-type="select"><span>下拉选项</span><span class="option option_select"></span></li>
                                   <li class="conli" data-type="img"><span>图片</span><span class="option option_img"></span></li>
                                   <li class="conli" data-type="str"><span>文字</span><span class="option option_input_txt"></span></li>
                                </ul>
                            </div>
                            <div style="clear:both;"></div>
                            <div>
                                <p><label>联系人组件</label></p>
                                <ul>
                                 <%--   <li class="txtli dis"><span>姓名</span></li>--%>
                                </ul>
                            </div>
                        </div>
                        <div id="Tabs2" class="tab-pane">
                            <div id="EditDiv"><!--组件属性设置区-->
                                <div id="title_div" class="edit_child_div">
                                    <p class="EditTitle">标题</p>
                                    <input type="text" id="title_edit_txt" class="input large" data-sync="title"/>
                                </div>
                                <div id="intro_div" class="edit_child_div">
                                    <p class="EditTitle">描述文字</p>
                                    <textarea id="intro_edit_txt" class="input large" data-sync="intro"></textarea>
                                </div>
                                <div id="required_div" class="edit_child_div">
                                    <p class="EditTitle">必填设置</p>
                                    <div class="EditContent">
                                        <label><input type="checkbox" id="required_chk"/>这个是必填项</label>
                                    </div>
                                </div>
                                <div id="txt_size_div" class="edit_child_div">
                                    <p class="EditTitle">组件大小</p>
                                    <div class="EditContent">
                                        <label><input name="txt_size" type="radio" value="small"  data-sync="txt_size"/>小尺寸</label>
                                        <label><input name="txt_size" type="radio" value="middle" data-sync="txt_size"/>标准尺寸</label>
                                        <label><input name="txt_size" type="radio" value="large"  data-sync="txt_size"/>大尺寸</label>
                                    </div>
                                </div>
                                <div id="txt_type_div" class="edit_child_div">
                                    <p class="EditTitle">组件类型</p><div>
                                        <div class="EditContent">
                                            <label><input name="txt_type" type="radio" value="input"  data-sync="txt_type"/>单行文本</label>
                                            <label><input name="txt_type" type="radio" value="textarea" data-sync="txt_type"/>多行文本</label>
                                            <label><input name="txt_type" type="radio" value="html" data-sync="txt_type"/>HTML文本</label></div>
                                        </div>
                                </div>
                                <!--select-->
                                <div id="sel_option_div" class="edit_child_div">
                                     <p class="EditTitle">单选选项配置</p>
                                     <div class="EditContent">
                                          <ul id="sel_option_ul">
                                              <li data-sort="1"><input type="radio"/><input type="text"/>
                                                  <span class="fa fa-minus opbtn"></span></li>
                                              <li data-sort="2"><input type="radio"/><input type="text" />
                                                  <span class="fa fa-minus opbtn"></span>
                                                  <span class="fa fa-plus opbtn"></span>
                                              </li>
                                          </ul>
                                      </div>
                                </div>
                                <div id="sel_type_div" class="edit_child_div">
                                    <p class="EditTitle">组件类型</p>
                                      <div class="EditContent">
                                        <label><input type="radio" name="sel_type_Rad" value="radio"/>单选</label>
                                        <label><input type="radio" name="sel_type_Rad" value="checkbox"/>多选</label>
                                        <label><input type="radio" name="sel_type_Rad" value="select"/>下拉</label>
                                      </div>
                                </div>
                                <!--Img-->
                                <div id="imgsrc_div" class="edit_child_div">
                                    <p class="EditTitle">图片链接</p>
                                    <div class="EditContent">
                                       <input type="text" class="input large" id="imgsrc_txt" />
                                    </div>
                                </div>
                                <div id="txtalign_div" class="edit_child_div">
                                    <p class="EditTitle">文字对齐方式</p>
                                    <div class="EditContent">
                                        <label><input type="radio" name="txtalign_radio" value="left"/>左对齐</label>
                                        <label><input type="radio" name="txtalign_radio" value="center"/>居中</label>
                                        <label><input type="radio" name="txtalign_radio" value="right"/>右对齐</label>
                                    </div>
                                </div>
                            </div><!--EditDiv End-->
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="tlpdiv" style="display:none;">
            <div id="input_txtTlp">
                    <li id='@id' class='formli'>
                    <div class="removeli" title="关闭"><span class="fa fa-remove removelibg"></span></div>
                            <span class='title_field'><label class='title'>@title</label></span>
                            <p class='intro'>@intro</p>
                    <div class='content'>@tlp</div>
                </li>
            </div>
            <div id="selectTlp">
                <li id='@id' class='formli'>
                    <div class="removeli" title="关闭"><span class="fa fa-remove removelibg"></span></div>
                    <span class='title_field'>
                        <label class='title'>@title</label></span>
                    <p class='intro'>@intro</p>
                    <div class='content'>@tlp</div>
                </li>
            </div>
            <div id="imgTlp">
                    <li id="@id" class='formli'>
                        <div class="removeli" title="关闭"><span class="fa fa-remove removelibg"></span></div>
                        <div class="content"><img class="img_normal" src="@src" /></div>
                        <p class="intro">@intro</p>
                    </li>
            </div>
            <div id="strTlp">
                    <li id="@id" class='formli'>
                        <div class="removeli" title="关闭"><span class="fa fa-remove removelibg"></span></div>
                        <span class="title_field"><label class="title">@title</label></span>
                        <p class="intro">@intro</p>
                        <div class="content"></div>
                    </li>
                </div>
        </div>
        <script type="text/javascript">
            var comArr = new Array();
            var curid = "";
            $().ready(function () {
                $("#formul").sortable({
                    placeholder: "highlight",
                    cursor: 'crosshair'
                });
                $("#formul").disableSelection();
                BindFormli();
                $(".conli").click(function () {
                    var type = $(this).attr("data-type");
                    AddCom(type);
                });
                ZLForm.EventBind();
            });
            //-------------------------------
            function AddCom(contype) {
                var com = "";
                var newid = GetNewID();
                switch (contype) {
                    case "input_txt"://input,textarea,html
                        com = new ZLForm.Inputer({ id: newid, required: false, title: "单行文本", intro: "", value: "", txt_size: "large", txt_type: "input" });
                        break;
                    case "input_text":
                        com = new ZLForm.Inputer({ id: newid, required: false, title: "多行文本", intro: "", value: "", txt_size: "large", txt_type: "textarea" });
                        break;
                    case "radio":
                        com = new ZLForm.Select({ id: newid, required: false, title: "单选项", intro: "", sel_option: [{ id: 1, value: "选项1" }, { id: 2, value: "选项2" }, { id: 3, value: "选项3" }], sel_type: "radio" });
                        break;
                    case "checkbox":
                        com = new ZLForm.Select({ id: newid, required: false, title: "多选项", intro: "", sel_option: [{ id: 1, value: "选项1" }, { id: 2, value: "选项2" }, { id: 3, value: "选项3" }], sel_type: "checkbox" });
                        break;
                    case "select":
                        com = new ZLForm.Select({ id: newid, required: false, title: "下拉选单", intro: "", sel_option: [{ id: 1, value: "选项1" }, { id: 2, value: "选项2" }, { id: 3, value: "选项3" }], sel_type: "select" });
                        break;
                    case "img":
                        com = new ZLForm.Img({ id: newid, title: "", intro: "这里是图片,写下你的描述", src: "/App_Themes/AdminDefaultTheme/images/PubForm/defaultimg.png", align: "left" });
                        break;
                    case "str":
                        com = new ZLForm.Str({ id: newid, title: "标题", intro: "请输入你的简述", align: "left" });
                        break;
                    default:
                        break;
                }
                if (com && com != "") {
                    ZLForm.AddFormli(com);
                    comArr.push(com);
                }
                BindFormli();
            }
            function BindFormli() {
                $(".formli").click(function () {
                    curid = this.id;
                    DisSetting();
                });
                $(".formli").mouseout(function () { $(this).find(".removelibg").hide(); }).mouseover(function () { $(this).find(".removelibg").show(); });
                $(".removeli").click(function () {
                    $(this).parent().click();
                    $cur = GetCurobj();
                    $("#" + $cur.instance.id).remove();
                    ArrCommon.DelByID(comArr, $cur.instance.id);
                    $($(".formli")[0]).click();
                });
                $("#formul input,#formul textarea,#formul select").each(function () { this.disabled = true; });
            }
            function GetNewID() {
                var maxid = 1;
                $(".formli").each(function () {
                    var id = $(this).attr("id").split('-')[1];
                    maxid = parseInt(id) > maxid ? id : maxid;
                });
                return "com-" + (parseInt(maxid) + 1);
            }
            function GetCurobj() {
                $obj = $("#" + curid);
                $obj.instance = ArrCommon.GetModelByID(comArr, curid);
                return $obj;
            }
            //显示li组件的配置
            function DisSetting() {
                $("#tabs2_href").trigger("click");
                $cur = GetCurobj();
                $(".edit_child_div").hide();
                for (var i = 0; i < $cur.instance.AttrSett.length; i++) {//显示
                    var id = $cur.instance.AttrSett[i];
                    $("#" + id + "_div").show();
                }
                $cur.instance.BeginEdit($cur);//赋值
            }
            function SortLi() {
                var index = 1;
                $(".formli").each(function () {
                    var model = ArrCommon.GetModelByID(comArr, this.id);
                    if (model && model.sortnum)
                        model.sortnum = index; index++;
                });
            }
            function InitForm() {
                $("#formul").html("");
                comArr = JSON.parse($("#Json_Hid").val());
                comArr.sort(function (a, b) { return a.sortnum > b.sortnum ? 1 : -1; });
                for (var i = 0; i < comArr.length; i++) {
                    comArr[i] = myextend(comArr[i]);
                    ZLForm.AddFormli(comArr[i]);//现在只是json数据,无prototype拷贝,如何将prototype中的数据拷入?
                }//for end;
            }
            //prototype中的不会存入,重新继承
            function myextend(target) {
                var source = "";
                switch (target.ctype) {
                    case "input_txt":
                        target = new ZLForm.Inputer(target);
                        break
                    case "select":
                        target = new ZLForm.Select(target);
                        break;
                    case "img":
                        target = new ZLForm.Img(target);
                        break
                    case "str":
                        target = new ZLForm.Str(target);
                        break;
                }
                return target;
            }
            function DisDefault() {
                var com1 = new ZLForm.Img({ id: "com-1", title: "", intro: "这里是图片,写下你的描述", src: "/App_Themes/AdminDefaultTheme/images/PubForm/head.jpg", align: "left" });
                var com2 = new ZLForm.Inputer({ id: "com-2", required: true, title: "年会评价：", intro: "请用一个词描述今年晚会的感受", value: "", txt_size: "large", txt_type: "input" });
                var com3 = new ZLForm.Select({ id: "com-3", required: false, title: "年会大家最感兴趣的环节", intro: "真心评的", value: "", sel_option: [{ id: 1, value: "领导致词" }, { id: 2, value: "节目表演" }, { id: 3, value: "饕餮美食" }, { id: 4, value: "现场抽奖" }], sel_type: "radio" });
                var com4 = new ZLForm.Str({ id: "com-4", title: "谢谢你没有拒绝我们做调查，送你一个小礼物!", intro: "晚安", align: "left" });
                comArr.push(com1); comArr.push(com2); comArr.push(com3); comArr.push(com4);
                for (var i = 0; i < comArr.length; i++) {
                    ZLForm.AddFormli(comArr[i]);
                }
            }
            function SaveForm() {
                if ($("#Title_T").val() == "" || $("#PubName_T").val() == "") {
                    alert("标题与表名不能为空!");
                    return false;
                }
                else {
                    //表名重名检测,或后台判断有重复表时自动加上数字
                    SortLi();
                    $("#Json_Hid").val(JSON.stringify(comArr));
                    //PostToCS2("FormDesign.aspx", "PubName", JSON.stringify(comArr), function (data) {
                    //});
                    return true;
                }
            }
        </script>
        <script type="text/javascript">
            //arr.sort(function (a, b) { return a.sortnum > b.sortnum ? 1 : -1; });排序
            var ArrCommon =
                 {
                     DelByID: function (arr, id) {
                         for (var i = 0; i < arr.length; i++) {
                             if (arr[i].id == id) {
                                 for (var j = i; j < (arr.length - 1) ; j++) {
                                     arr[j] = arr[j + 1];
                                 }
                                 arr.pop();
                             }
                         }
                     },//DelByID End;
                     GetModelByID: function (arr, id) {
                         for (i = 0; i < arr.length; i++) {
                             if (id == arr[i].id) return arr[i];
                         }
                     }
                 }
        </script>
    </form>
</body>
</html>
