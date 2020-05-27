<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestRPT.aspx.cs" Inherits="ZoomLaCMS.BU.QuestRPT" EnableViewState="false" ClientIDMode="Static" %>
<%--<asp:Content runat="server" ContentPlaceHolderID="head"><title></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">--%>
   <form id="form1" runat="server">
<start>
    <div style="border:1px solid #ddd;padding:5px;margin-bottom:3px;">总计：<span class="r_red" runat="server" id="count_sp">0</span>道相关试题</div>
    <div runat="server" id="login_div" visible="false" class="alert alert-danger">你当前是[<span style="color:red;">游客</span>]身份,登录后可以启用更多功能!<a href="/User/Login?ReturnUrl=/User/Exam/QuestionManage">点击登录</a></div>
    <asp:Repeater runat="server" EnableViewState="false" ID="RPT">
        <ItemTemplate>
            <div class="panel panel-default" style="margin-bottom: 10px;">
                <div class="panel-body">
                    <%#GetContent()%>
                </div>
                <div class="panel-footer">
                    <a href="QuestView?ID=<%#Eval("p_id") %>" target="_blank"><i class="fa fa-check-circle"></i>查看解析</a>
                 <%--   <a href="#" class="disabled"><i class="fa fa-cloud-download"></i>下载</a>--%>
                    <a href="javascript:;" <%#GetIsContain()?"style='display:none;'":"" %> class="cart_op addQid" data-type="<%#Eval("p_type") %>" data-qid="<%#Eval("p_id") %>"><i class="fa fa-plus-circle"></i>试题篮</a>
                    <a href="javascript:;" <%#GetIsContain()?"":"style='display:none;'" %> class="cart_op removeQid" data-type="<%#Eval("p_type") %>" data-qid="<%#Eval("p_id") %>"><i class="fa fa-minus-circle">试题篮(已加入试题篮)</i></a>
                    <div style="float: right;">
                        <span>科目:<span class="r_green"><%#Eval("C_ClassName")%></span></span>
                        <span>知识点:<span class="r_green"><%#GetTagKey()%></span></span>
                    </div>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </asp:Repeater>
    <asp:Literal runat="server" ID="Page_Lit"></asp:Literal>
    <div runat="server" id="empty_div" visible="false" style="width:100%;text-align:center;">
      对不起,当前条件下没有试题,<%:Call.SiteName %>正在加速上载试题,敬请期待！
    </div>
 </start>
    </form>
<%--</asp:Content>--%>