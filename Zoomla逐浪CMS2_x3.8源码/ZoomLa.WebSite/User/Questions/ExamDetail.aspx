<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExamDetail.aspx.cs" Inherits="User_Questions_ExamDetail" EnableViewStateMac="false" MasterPageFile="~/Common/Common.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script src="/Plugins/Ueditor/ueditor.config.js" charset="utf-8"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js" charset="utf-8"></script>
<title>参加考试</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="panel panel-primary"  ng-app="app">
        <div class="panel-heading navbar-fixed-top" id="quelist"><i class="fa fa-file-text-o"></i>
            <span class="margin_l5"><asp:Label runat="server" ID="PName_L"></asp:Label></span>
            <span><i class="a fa-clock-o"></i><span id="time_sp" class="margin_l5" runat="server" data-time="0"></span></span>
            <span runat="server" id="totalscore_sp" style="font-size:1.8em;min-width:80px;"></span>
            <div class="uinfo_div">
                <div class="input-group">
                    <span class="input-group-addon">学校</span>
                    <asp:TextBox runat="server" ID="MySchool_T" MaxLength="50" CssClass="form-control text_150" />
                </div>
                <div class="input-group">
                    <span class="input-group-addon">班级</span>
                    <asp:TextBox runat="server" ID="MyClass_T" MaxLength="50" CssClass="form-control text_150" />
                </div>
                <div class="input-group">
                    <span class="input-group-addon">姓名</span>
                    <asp:TextBox runat="server" ID="UName_T" MaxLength="50" CssClass="form-control text_150" />
                </div>
                <div class="clearfix"></div>
            </div>
            <ul class="list-unstyled pull-right">
                <li><a href="/" title="首页"><i class="fa fa-home"></i></a></li>
                <li>
                    <asp:LinkButton runat="server" ID="Submit_Btn" OnClick="Submit_Btn_Click" OnClientClick="return PreSubmit();" ToolTip="交卷"><i class="fa fa-check"></i>交卷</asp:LinkButton></li>
                <li class="hidden">
                    <asp:LinkButton runat="server" ID="Coll_Btn" Visible="false"><i class="fa fa-heart" title="收藏"></i></asp:LinkButton></li>
                <li><a href="MyMarks.aspx" runat="server" id="return_a" title="退出"><i class="fa fa-close"></i></a></li>
                <li><%-- <a href="javascript:;" class="btn btn-primary" onclick="if(confirm('正在考试,确定要返回吗?')){location='ExamDetail.aspx?ID=<%=Mid %>';};">返回</a>--%></li>
            </ul>
        </div>
        <div style="height:50px;"></div>
        <div class="panel-body" ng-controller="appController">
            <asp:Repeater runat="server" ID="MainRPT" EnableViewState="false" OnItemDataBound="MainRPT_ItemDataBound">
                <ItemTemplate>
                    <div  style="margin-top:5px;">
                        <%#ZoomLa.BLL.Helper.StrHelper.ConvertIntegral(Container.ItemIndex+1) +"．"+Eval("QName")+"（有"+Eval("QNum")+"小题,共"+Eval("TotalScore")+"分）" %>
                    </div>
                    <div><%#Eval("LargeContent") %></div>
                    <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
                        <ItemTemplate>
                            <div class="item" data-id="<%#Eval("p_id") %>" id="item_<%#Eval("p_id") %>">
                               <div class="content">
                                  <span><%#Container.ItemIndex+1+"．"+Eval("P_Title") %></span><%#GetContent() %>
                               </div>
                                <div class="submit">
                                    <ul class="submitul">
                                        <%#GetSubmit() %>
                                    </ul>
                                    <div class="clearfix"></div>
                               </div>
                                <%if(Action.Equals("view")){ %>
                                <div class="remark_div margin_t5">
                                   <div class="panel panel-info">
                                       <div class="panel-heading"><i class="fa fa-file-text-o"></i><span class="margin_l5">教师批阅</span></div>
                                       <div class="panel-body">
                                           <div><%#GetIsRight() %></div>
                                           <div>
                                               <div class='answerdiv remark'>
                                                   <%#Eval("Remark") %>
                                               </div>
                                           </div>
                                       </div>
                                   </div>
                               </div><!--remark_div end-->
                                <div class="jiexi" runat="server" visible="true">
                                   <div class="panel panel-info">
                                       <div class="panel-heading"><i class="fa fa-file-text-o"></i><span class="margin_l5">答案&试题解析</span></div>
                                       <div class="panel-body">
                                           <p style="white-space: normal;"><span style="font-family: 宋体, sans-serif; font-size: 13px; font-weight: bold; letter-spacing: 1px; line-height: 25px; background-color: #FFFFFF;">【正确答案】</span></p>
                                            <%#Eval("p_shuming") %>
                                            <%#Eval("Jiexi") %>
                                       </div>
                                   </div>
                               </div><!--jiexi end--><%} %>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="panel-footer text-center"></div>
    </div>
<div>
</div>
<div>
    <asp:HiddenField runat="server" ID="QuestDT_Hid"  EnableViewState="false" />
    <asp:HiddenField runat="server" ID="Answer_Hid"  EnableViewState="false" />
</div>
<div id="answer_ue_div" data-scroll="0">
    <textarea id="editor" style="height: 120px;"></textarea>
    <div style="text-align: center; padding: 5px;">
        <input type="button" value="确定" class="btn btn-primary" onclick="LoadContent();" />
        <input type="button" value="关闭" class="btn btn-default" onclick="$('#answer_ue_div').hide();" />
    </div>
</div>
<asp:HiddenField ID="QuestId_Hid" runat="server" />
<asp:HiddenField ID="QuestTime_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/Plugins/Ueditor/kityformula-plugin/addKityFormulaDialog.js"></script>
<script src="/Plugins/Ueditor/kityformula-plugin/getKfContent.js"></script>
<script src="/Plugins/Ueditor/kityformula-plugin/defaultFilterFix.js"></script>
<script src="/JS/jquery-ui.min.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<style type="text/css">
label {font-weight:normal;}
#quelist li{ float:left;}
#quelist li a{ padding-left:10px; padding-right:10px; color:#fff; font-size:1.5em;}
.opitem p {display:inline-block;}
.item {border:1px solid #ddd; padding-left:20px;padding-top:5px;text-align:left;margin-bottom:5px;}
.opitem:hover {color:#ff6a00;}
.submitul li {float:left;margin-left:20px;}
.answerdiv {border:1px dashed #ddd;height:50px;color:green;width:100%;padding:5px;height:80px;overflow:auto;}
.answersp {border-bottom:1px solid #286090;padding:5px 15px 3px 15px; color:green;}
.answersp p {display:inline;}
.answersp p img {padding-bottom:8px;}
#answer_ue_div {width:500px;display:none;position:absolute;top:300px;right:30%; border:1px solid #ddd;box-shadow:0 4px 20px 1px rgba(0,0,0,0.2);background:#ffffff;}
</style>
<script>
    var page={scope:null};
    var app=angular.module("app",[]).controller("appController",function($scope,$compile){
        page.scope=$scope;
        $scope.list={};
        var idsArr=[];//仅用于显示答案
        <%=AngularJS%>
        if("<%:Action%>"=="view")
        {
            var answerArr = JSON.parse($("#Answer_Hid").val());
            //model即一个完形填空的问题集合,问题集合-->问题(答案)-->选项
            for (var i = 0; i < idsArr.length; i++) {
                var model=$scope.list["filltextblank_"+idsArr[i]];
                var answer= answerArr.GetByID(idsArr[i],"QID");
                if(!answer||answer==null||answer==""){return;}
                answer=JSON.parse(answer.Answer);
                for (var j = 0; j < model.length; j++) {
                    model[j].answer=answer[j].answer;
                }
            }
        }
    });
    app.filter(
         'to_trusted', ['$sce', function ($sce) {
             return function (text) {
                 return $sce.trustAsHtml(text);
             }
         }]
     );
</script>
<script>
    var ue, $curAnswer, boundary = "|||", action = "<%=Action%>",exTime= parseInt("<%=ExTime%>"); force = <%=ExTime>0?"true":"false"%>;
    $(function () {
        ue = UE.getEditor('editor', {
            toolbars: [[
                'bold', 'italic', 'underline', '|', 'fontsize', '|', 'kityformula'
            ]],
            elementPathEnabled: false,wordCount: false
        });
        if (action != "view") {
            //缓存考试时间
            if(localStorage.questid==""||localStorage.questid!=$("#QuestId_Hid").val())
            {
                localStorage.questid=$("#QuestId_Hid").val();
                localStorage.second=0;
            }
            var timer = $("#time_sp");
            timer.data("time", localStorage.second);
            setInterval(BeginTimer, 1000);
            $("#answer_ue_div").draggable({ handle: '#ue_foot,#edui1_toolbarbox' });
            $(".answersp,.answerdiv").click(function () {
                //OpenFormula();
                $curAnswer = $(this);
                SetContent($curAnswer);
                $("#answer_ue_div").show();
            });
        }
    });
    $(window).scroll(function(){
        var scrollTop = $(this).scrollTop();//已滚动多少
        //var scrollHeight = $(document).height();//窗体高度
        //var windowHeight = $(this).height();//总高度,滚动条+窗体高
        var nowTop=parseInt($("#answer_ue_div").css("top"));//支持拖动
        var scroll=parseInt($("#answer_ue_div").data("scroll"));
        $("#answer_ue_div").css("top",scrollTop+(nowTop-scroll));
        $("#answer_ue_div").data("scroll",scrollTop);
    });
</script>
<script src="/JS/ICMS/ZL_Exam_Paper.js"></script>
</asp:Content>