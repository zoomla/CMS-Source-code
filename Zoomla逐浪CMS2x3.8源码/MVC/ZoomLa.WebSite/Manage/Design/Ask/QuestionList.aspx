<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionList.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.Ask.QuestionList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style type="text/css">
/*选择题型*/
#qtype_ul li { float:left;margin-right:15px; margin-bottom:10px;text-align:center; cursor:pointer;}
#qtype_ul .wrap {width:40px;margin:0 auto;text-align:center;}
#qtype_ul .wrap div {color:#fff;width:100%;height:40px;padding-top:10px;border-radius:6px;box-shadow:5px #ddd;}
#qtype_ul .wrap .fa {font-size:25px;}
#qtype_ul .wrap span {display:block;margin-top:5px;}
</style>
<title>问题列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
    <li><a href='Default.aspx'>动力模块</a></li>
    <li><a href='AskList.aspx'>问卷调查</a></li>
    <li><a href="<%=Request.RawUrl %>">问题列表</a>(<asp:Label runat="server" ID="AskTitle_T"></asp:Label>) [<a href="javascript:;" onclick="selbox.toggle();">添加问题</a>]</li>
    <div id="sel_box" runat="server" class="padding5" style="height:80px;padding-top:10px;">
        <div id="qtype_wrap" style="width:600px;margin:0 auto;">
                <ul id="qtype_ul">
                    <li onclick="tonew('radio','')">
                        <div class="wrap">
                            <div style="background-color: #6083C7;"><i class="fa fa-check-circle-o"></i></div>
                            <span>单选</span>
                        </div>
                    </li>
                    <li onclick="tonew('checkbox','')">
                        <div class="wrap">
                            <div style="background-color: #53CEE0;"><i class="fa fa-check-square-o"></i></div>
                            <span>多选</span>
                        </div>
                    </li>
                    <li onclick="tonew('blank','')">
                        <div class="wrap">
                            <div style="background-color: #80B660;"><i class="fa fa-indent"></i></div>
                            <span>填空</span>
                        </div>
                    </li>
                    <li onclick="tonew('score','')">
                        <div class="wrap">
                            <div style="background-color: #EFB93D;"><i class="fa fa-star"></i></div>
                            <span>打分</span>
                        </div>
                    </li>
                    <li onclick="tonew('checkbox','')">
                        <div class="wrap">
                            <div style="background-color: #EEB54E;"><i class="fa fa-list-ul"></i></div>
                            <span>排序</span>
                        </div>
                    </li>
                    <li onclick="tonew('radio','sex')">
                        <div class="wrap">
                            <div style="background-color: #EF4E3A;"><i class="fa fa-venus-mars"></i></div>
                            <span>性别</span>
                        </div>
                    </li>
                    <li onclick="tonew('blank','mobile')">
                        <div class="wrap">
                            <div style="background-color: #65CD30;"><i class="fa fa-mobile"></i></div>
                            <span>手机</span>
                        </div>
                    </li>
                    <li onclick="tonew('blank','email')">
                        <div class="wrap">
                            <div style="background-color: #886EE9;"><i class="fa fa-envelope"></i></div>
                            <span>邮箱</span>
                        </div>
                    </li>
                    <li onclick="tonew('blank','area')">
                        <div class="wrap">
                            <div style="background-color: #BBDCA7;"><i class="fa fa-home"></i></div>
                            <span>地区</span>
                        </div>
                    </li>
                    <li onclick="tonew('blank','date')">
                        <div class="wrap">
                            <div style="background-color: #8674CA;"><i class="fa fa-calendar"></i></div>
                            <span>日期</span>
                        </div>
                    </li>
                </ul>
                <div class="clearfix"></div>
                <!--<div style="background-color:#fff;width:100%;height:60px;padding-top:5px; margin-top:60px;text-align:center;">
        <a href="javascript:;" style="border-radius:50%;width:50px;height:50px;border:1px solid #ddd;display:inline-block;padding-top:10px;color:#ddd;"><i class="fa fa-remove fa-2x"></i></a>
    </div>-->
            </div>
    </div>
</ol>
<div style="margin-top:40px;">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" OnRowDataBound="EGV_RowDataBound" PageSize="20" AllowPaging="true"
         CssClass="table table-striped table-bordered table-hover" OnRowCommand="EGV_RowCommand" EmptyDataText="没有数据">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_xs">
                <ItemTemplate><input type="checkbox" value="<%#Eval("ID") %>" name="idchk"/></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>
                    <a href="javascript:;" onclick=""><%#Eval("QTitle") %></a>
                </ItemTemplate>
            </asp:TemplateField>
<%--            <asp:TemplateField HeaderText="创建用户" >
                <ItemTemplate>
                    <a href="javascript:;" onclick="showuser(<%#Eval("CUser") %>)"><%#GetUserName() %></a>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="问题选项">
                <ItemTemplate>
                    <%#GetOption() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="问题类型">
                <ItemTemplate><%#GetQType() %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="必填">
                <ItemTemplate>
                    <%#GetRequired() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="创建时间" DataField="CDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="javascript:;" ><i class="fa fa-pencil"></i>编辑</a> 
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del" OnClientClick="return confirm('确认删除？')" CssClass="option_style"><i class="fa fa-trash-o" title="<%=Resources.L.删除 %>"></i><%=Resources.L.删除 %></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    function showuser(id) { ShowComDiag("../../User/Userinfo.aspx?id=" + id, "查看用户") }
    //问题类型|类型下子分类
    function tonew(qtype, qflag) {
        location = "QuestionAdd.aspx?qtype=" + qtype + "&qflag=" + qflag + "&AskID=<%:AskID%>";
    }
</script>
</asp:Content>
