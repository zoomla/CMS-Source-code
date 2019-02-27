<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEngLishQuestion.aspx.cs"   ValidateRequest="false" Inherits="manage_Question_AddEngLishQuestion" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<%@ Register Src="~/Manage/I/ASCX/TreeTlpDP.ascx" TagPrefix="ZL" TagName="TreeTlp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script src="/Plugins/Ueditor/ueditor.config.js" charset="utf-8"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js" charset="utf-8"></script>
<script src="/Plugins/Ueditor/kityformula-plugin/addKityFormulaDialog.js"></script>
<script src="/Plugins/Ueditor/kityformula-plugin/getKfContent.js"></script>
<script src="/Plugins/Ueditor/kityformula-plugin/defaultFilterFix.js"></script>
<%--    <script src="/Plugins/Ueditor/Formdesign/leipi.Formdesign.v4.js" charset="utf-8" ></script>--%>
<link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="/App_Themes/V3.css" />
<script type="text/javascript" src="/JS/OAKeyWord.js"></script>
<title>试题管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="BreadDiv" class="container-fluid mysite">
        <div class="row">
            <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
                <li><a href="Papers_System_Manage.aspx">教育模块</a></li>
                <li><a href="QuestList.aspx?NodeID=<%:NodeID+"&Grade="+Grade %>">试题管理</a></li>
                <li class="active">添加试题<asp:Label runat="server" ID="CurNode_L"></asp:Label></li>
            </ol>
        </div>
    </div>
    <ul class="nav nav-tabs" style="border-bottom:none;">
        <li class="active"><a href="#basic" data-toggle="tab">基本信息</a></li>
        <li><a href="#question" id="questtype_a" data-toggle="tab">试题信息</a></li>
        <li><a href="#other" data-toggle="tab">解析说明</a></li>
    </ul>
    <table class="table table-striped table-bordered table-hover tab-content" style="border-top:none;">
        <tbody id="basic" class="tab-pane active">
            <tr>
                <td class="td_m">试题标题(编号):</td>
                <td>
                    <asp:TextBox ID="txtP_title" runat="server" CssClass="form-control text_300 required" MaxLength="50" />
                    <span class="r_red">*</span>
                    <span id="titleNum_sp"></span>
                </td>
            </tr>
            <tr><td>教材版本:</td><td>
                <asp:RadioButtonList ID="Version_Rad" runat="server" RepeatDirection="Horizontal" DataValueField="ID" DataTextField="VersionName"></asp:RadioButtonList>
             </td></tr>
            <tr>
                <td>所属年级:</td>
                <td>
                    <asp:RadioButtonList ID="Grade_Radio" runat="server" RepeatDirection="Horizontal" DataValueField="GradeID" DataTextField="GradeName"></asp:RadioButtonList>
                </td>
            </tr>
            <tr runat="server" style="display:none;" id="nodetr">
                <td class="td_l">试题类别:</td>
                <td>
                      <ZL:TreeTlp ID="Quest_Tree" runat="server" NodeID="C_id" Pid="C_Classid" Name="C_ClassName" Selected="NodeID_Hid" />
                    <asp:HiddenField ID="NodeID_Hid" runat="server" />
               </td></tr>
            <tr>
                <td>难度:</td>
                <td>
                    <div style="width: auto; float: left">
   <%--                     <asp:RadioButtonList ID="rblDiff" runat="server" DataTextField="GradeName" DataValueField="GradeID" RepeatDirection="Horizontal"></asp:RadioButtonList>--%>
                        <asp:TextBox runat="server" ID="Diffcult_T" CssClass="form-control text_xs float" Text="1" /><span class="rd_green">0-1之间的值,1为最易</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>题型:</td>
                <td>
                    <label><input type="radio" name="qtype_rad" value="0" checked="checked" />单选题 </label>
                    <label><input type="radio" name="qtype_rad" value="1" />多选题 </label>
                    <label><input type="radio" name="qtype_rad" value="2" />填空题 </label>
                    <label><input type="radio" name="qtype_rad" value="3" />解析题 </label>
                    <label><input type="radio" name="qtype_rad" value="4" />完形填空 </label>
                    <label id="bigrad"><input type="radio" name="qtype_rad" value="10" />大题</label>
                </td>
            </tr>
            <tr>
                <td>知识点:</td>
                <td>
                    <div id="Examkeyword"></div>
                    <button type="button" onclick="SelKnow()" class="btn btn-primary btn-sm">选择知识点</button>
                    <asp:HiddenField ID="TagKey_T" runat="server" />
                </td>
            </tr>
            <tr>
                <td>分数:</td>
                <td>
                    <asp:TextBox ID="txtDefaSocre" runat="server" Text="0" CssClass="form-control text_xs"></asp:TextBox></td>
            </tr>
            <tr><td>是否共享:</td><td>
                <input type="checkbox" runat="server" id="IsShare_Chk" checked="checked" class="switchChk"/>
            </td></tr>
            <tr id="normaltr">
                <td>题干:<span class="rd_red">*</span></td>
                <td>
                    <asp:TextBox ID="txtP_Content" runat="server" Style="height: 200px; width: 100%;" TextMode="MultiLine"></asp:TextBox>
                    <%=Call.GetUEditor("txtP_Content",2) %>
                </td>
            </tr>
            <tr class="bigtr" style="display:none;">
                <td>试题内容</td>
                <td ng-app="app">
                    <div ng-controller="appController">
                        <table class="table table-bordered">
                            <tr><td>序号</td><td>试题名</td><td>类型</td><td>排序</td><td>操作</td></tr>
                            <tbody ng-repeat="item in list|orderBy:'-orderid'">
                                <tr>
                                    <td ng-bind="$index+1"></td>
                                    <td ng-bind="item.p_title"></td>
                                    <td>{{getTypeStr(item.p_Type)}}</td>
                                    <td>
                                        <input type="text" class="form-control int" style="width: 60px;" ng-model="item.orderid" /></td>
                                    <td>
                                        <a href="javascript:;" ng-click="remove(item.p_id);" title="移除"><span class="fa fa-remove"></span></a>
                                        <a href="javascript:;" ng-click="edit(item.p_id);" title="修改"><span class="fa fa-pencil"></span></a>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <button class="btn btn-primary btn-sm" type="button" onclick="ShowAdd()">添加小题</button>
                        <button class="btn btn-primary btn-sm" type="button" onclick="ShowSel()">题库引用</button>
                    </div>
                </td>
            </tr>
        </tbody>
        <tbody id="question" class="tab-pane">
            <tr>
                <td class="td_l">选项信息:</td>
                <td>
                    <span></span>
                    <asp:DropDownList ID="ddlNumber1" CssClass="form-control text_x" onchange="AddOption($(this).val());" runat="server">
                        <asp:ListItem Value="0">0</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                    </asp:DropDownList>
                    <div>
                        <ul id="option_ul"></ul>
                    </div>
                    <asp:Literal ID="Tips" runat="server" Visible="false"></asp:Literal>
                    <div id="optionDiv" runat="server" visible="false"></div>
                </td>
            </tr>
            <tr>
                <td>正确答案：<span class="rd_green">(仅用于自动改卷)</span></td>
                <td>
                    <asp:TextBox runat="server" ID="Answer_T" CssClass="form-control text_300"></asp:TextBox>
                    <span class="r_green_x">如有多个答案用|号分隔,用于支持自动批阅试卷,单选:A,多选:A|B,填空:值1|值2|值3</span>
                </td>
            </tr>
            <tr>
                <td>正确答案：<span class="rd_green">(教师与学生可见)</span></td>
                <td>
                    <asp:TextBox runat="server" ID="AnswerHtml_T" TextMode="MultiLine" Style="width: 100%; height: 200px;"></asp:TextBox>
                    <%=Call.GetUEditor("AnswerHtml_T",2) %>
                </td>
            </tr>
        </tbody>
        <tbody id="other" class="tab-pane">
            <tr>
                <td class="td_m">试题解析:</td>
                <td>
                    <textarea runat="server" id="txtJiexi" style="height: 200px; width: 100%;">
                        <p style="white-space: normal;"><span style="font-family: 宋体, sans-serif; font-size: 13px; font-weight: bold; letter-spacing: 1px; line-height: 25px; background-color: #FFFFFF;">【考点】</span></p>
                        <p style="white-space: normal;"><span style="font-family: 宋体, sans-serif; font-size: 13px; letter-spacing: 1px; line-height: 25px; background-color: #FFFFFF;"><span style="font-weight: bold;">【专题】</span></span></p>
                        <p style="white-space: normal;"><span style="font-family: 宋体, sans-serif; font-size: 13px; letter-spacing: 1px; line-height: 25px; background-color: #FFFFFF;"><span style="font-weight: bold;">【分析】</span></span></p>
                        <p style="white-space: normal;"><span style="font-family: 宋体, sans-serif; font-size: 13px; letter-spacing: 1px; line-height: 25px; background-color: #FFFFFF;"><span style="font-weight: bold;">【解答】</span></span></p>
                        <p style="white-space: normal;"><span style="font-weight: bold; font-family: 宋体, sans-serif; font-size: 13px; letter-spacing: 1px; line-height: 25px; background-color: #FFFFFF;">【点评】</span></p>
                    </textarea>
                    <%=Call.GetUEditor("txtJiexi",2) %>
                </td>
            </tr>
        </tbody>
    </table>
    <div class="Conent_fix">
        <asp:HiddenField ID="Optioninfo_Hid" runat="server" />
        <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存试题" OnClientClick="return CheckData();" OnClick="EBtnSubmit_Click" runat="server" />
        <asp:Button ID="SaveNew_Btn" runat="server" Visible="false" Text="添加为新试题" CssClass="btn btn-primary" OnClick="Save_New_Btn_Click" />
        <a href="QuestList.aspx?NodeID=<%:NodeID_Hid.Value %>" class="btn btn-primary">返回列表</a>
    </div>
    <input type="hidden" id="Id_hid" value="<%=Mid %>" />
    <asp:HiddenField ID="Qids_Hid" runat="server" />
<asp:HiddenField ID="Qinfo_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        #mymodal2 .modal-dialog iframe{height:600px!important;}
        #option_ul li{margin-top:3px;}
        #errormes{z-index:9999;}
    </style>
    <script src="/dist/js/bootstrap-switch.js"></script>
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/SelectCheckBox.js"></script>
    <script src="/JS/jquery.validate.min.js"></script>
    <script src="/JS/Controls/ZL_Array.js"></script>
    <script src="/JS/Plugs/angular.min.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script src="/JS/ICMS/ZL_Exam_Controls.js"></script>
    <script src="/JS/ICMS/ZL_Exam_Paper.js"></script>
    <script src="/JS/ICMS/ZL_Exam_Question.js"></script>
     <script>
         $(function () {
             ZL_Regex.B_Num(".int");
             ZL_Regex.B_Num(".int");
             ZL_Regex.B_Float(".float");
             ZL_Regex.B_Value("#Diffcult_T", { min: 0, max: 1, });
         })
     </script>
</asp:Content>