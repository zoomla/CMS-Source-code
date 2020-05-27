<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyResult.aspx.cs" Inherits="ZoomLaCMS.Manage.Plus.SurveyResult" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>问卷投票结果</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <ul class="nav nav-tabs">
                    <li class="active"><a href="#Tabs0" data-toggle="tab">总分评测</a></li>
                    <li><a href="#Tabs1" data-toggle="tab">用户清单</a></li>
                    <li><a href="#Tabs2" data-toggle="tab">详细信息</a></li>
                </ul>
 <div class="tab-content">
    <div class="tab-pane active" id="Tabs0">
        <table class="table table-striped table-bordered">
            <tr>
                <th>问题选项</th><th>问题分数</th><th>选择人数</th>
            </tr>
            <%= GetAll() %>
            <tr>
                <td colspan="4">
                    <h4>
                        公式：(A*N+B*N+C*N+D*N)/总问卷数/9*100<br />
                        注：N=单项投票总数
                    </h4>
                </td>
            </tr>
        </table>
    </div>
    <div class="tab-pane" id="Tabs1">
        <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" Width="100%" Height="20px" AllowPaging="True" AllowSorting="True" CellPadding="2" CellSpacing="1" BackColor="White" ForeColor="Black" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" RowStyle-CssClass="tdbg" GridLines="None" EnableModelValidation="True">
            <Columns>
                <asp:BoundField HeaderText="用户ID" DataField="Userid" />
                <asp:TemplateField HeaderText="用户名"><ItemTemplate>
                    <a href="../User/UserInfo.aspx?id=<%#Eval("UserID") %>" target="_blank"><%#Eval("UserName") %></a></ItemTemplate></asp:TemplateField>
                <asp:BoundField HeaderText="用户得分" DataField="AnswerScore" />
            </Columns>
        </ZL:ExGridView>
    </div>
    <div class="tab-pane" id="Tabs2">
        <asp:Repeater ID="rptReuslt" runat="server" OnItemDataBound="rptReuslt_ItemDataBound">
            <ItemTemplate>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td colspan="3" style="">第 <%#Container.ItemIndex +1 %>  题 : <%# Eval("QuestionTitle") %> [<%# GetQuesType(Eval("TypeID")) %>] </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTip" Visible="true" runat="server" Text="文本内容不可查看。。。"></asp:Label>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptOption" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="width: 40%;">( <%# Convert.ToChar(Container.ItemIndex + 65) %> ) 、 <%# Container.DataItem %></td>
                                <td style="width: 50%;">
                                    <asp:Image ID="imgBar" runat="server" ImageUrl="~/images/redline.jpg" Height="20px" />
                                </td>
                                <td>
                                    <asp:Label ID="lblPercent" runat="server" Style="padding-left: 15px; color: red; font-size: 12px;"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <%# GetScore(Eval("QuestionID","{0}"),Eval("TypeID","{0}")) %>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
    <div style="text-align: right; padding-right: 50px;">统计总分：<span style="color: #f00"><%=GetCountScore() %></span></div>
     <div class="text-center">
                <input name="print" type="button" id="Button1" class="btn btn-primary" value="打印" onclick="window.print();" />
                <input name="Cancel" type="button" id="Cancel" class="btn btn-primary" value="返回" onclick="window.location.href = 'SurveyManage.aspx';" />
            </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style>
        #Tabs0 table td{ text-align:center;}
    </style>
    <script src="Js/highcharts.js" type="text/javascript"></script>
    <script src="Js/exporting.js" type="text/javascript" charset="gb2312"></script>
    <script type="text/javascript">
        //保留小数点后一位
        function Decimal(x) {
            var f_x = parseFloat(x);
            if (isNaN(f_x)) {
                alert('参数为非数字，无法转换！');
                return false;
            }
            var f_x = Math.round(x * 10) / 10;

            return f_x;
        }
    </script>
</asp:Content>
