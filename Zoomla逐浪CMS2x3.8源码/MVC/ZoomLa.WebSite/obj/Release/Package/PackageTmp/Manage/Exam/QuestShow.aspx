<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestShow.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.QuestShow"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>试题预览</title><style>#options p{display:inline-block;}ul{padding-left:20px;} </style>
 <script src="/JS/Plugs/angular.min.js"></script>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover" ng-app="app">
        <tr>
            <td class="td_l">试题标题:</td>
            <td><asp:Label ID="Title_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>所属年级:</td>
            <td><asp:Label ID="Grade_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>难度:</td>
            <td><asp:Label ID="Diff_L" runat="server"></asp:Label> </td>
        </tr>
        <tr>
            <td>题型:</td>
            <td><asp:Label ID="QType_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>知识点:</td>
            <td><asp:Label ID="KeyWord_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>试题内容:</td>
            <td><asp:Literal ID="Content_Li" EnableViewState="false" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td>分数:</td>
            <td><asp:Label ID="Socre_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>试题选项数:</td>
            <td><asp:Label ID="QuestNum_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>选项预览:</td>
            <td id="options" ng-controller="appController">
                <ul >
                    <asp:Literal ID="Option_Li" runat="server" EnableViewState="false"></asp:Literal>
                    <asp:Repeater ID="Quest_RPT" Visible="false" runat="server">
                        <ItemTemplate>
                            <div style="margin-top:10px;">
                                <span><%#Container.ItemIndex+1+"．"+Eval("P_Title") %></span><%#GetContent() %>
                            </div>
                             <div class="submit">
                                <ul class="submitul">
                                    <%#GetSubmit() %>
                                </ul>
                                <div class="clearfix"></div>
                            </div>
                            <div>正确答案: <%#Eval("p_Answer") %></div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </td>
        </tr>
        <tr>
            <td>正确答案(仅用于自动改卷):</td> 
            <td><asp:Label ID="Answer_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>正确答案(教师与学生可见):</td>
            <td><asp:Literal ID="AnswerHtml_Li" EnableViewState="false" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td>试题解析:</td>
            <td><asp:Literal ID="Jiexi_Li" EnableViewState="false" runat="server"></asp:Literal></td>
        </tr>
    </table>
    <div class="text-center">
        <a href="AddEngLishQuestion.aspx?id=<%=QID %>" class="btn btn-primary">重新修改</a>
        <a href="QuestionManage.aspx" class="btn btn-primary">返回列表</a>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
     <script>
    var page={scope:null,addModel:function(name,model){ 
        page.scope.list[name]=model;
    }};
    var app=angular.module("app",[]).controller("appController",function($scope,$compile){
        page.scope=$scope;
        $scope.list={};
        var idsArr=[];//仅用于显示答案
        <%=AngularJS%>
        
    });
    app.filter(
         'to_trusted', ['$sce', function ($sce) {
             return function (text) {
                 return $sce.trustAsHtml(text);
             }
         }]
     );
</script>
</asp:Content>



