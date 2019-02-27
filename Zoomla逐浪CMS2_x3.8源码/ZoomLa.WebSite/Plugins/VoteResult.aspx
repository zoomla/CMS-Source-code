<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VoteResult.aspx.cs" Inherits="Common_VoteResult" EnableViewStateMac="false" %>

<!DOCTYPE HTML>
<html>
<head runat="server">
    <title>投票结果</title>
    <link href="../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Guest/Survey.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="head">
            <div class="clear"></div>
            <div class="banner"></div>
        </div>
        <div class="clear"></div>
        <div id="main">
            <div id="divContent" class="content" runat="server">
                <div class="title">
                    <asp:Literal ID="ltlSurveyName" runat="server" Text="Label"></asp:Literal>
                    的调查结果
                    <div class="date">
                        提交时间:
                <asp:Literal ID="ltlDate" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="clear"></div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="result">
                    <asp:Repeater ID="rptResult" runat="server" OnItemDataBound="rptResult_ItemDataBound">
                        <ItemTemplate>
                            <dl>
                                <dt>
                                    <span>第 <%#Container.ItemIndex +1 %>  题: </span>&nbsp; 
                           <span><%# Eval("QuestionTitle") %>
                               <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
                           </span>&nbsp;
                           <span>[<%# GetQuesType(Eval("TypeID")) %>]</span>
                                </dt>
                                <dd>
                                    <div id="divTable" runat="server">
                                        <ZL:ExGridView ID="gviewOption" runat="server" AutoGenerateColumns="false" ShowFooter="true">
                                            <FooterStyle Font-Bold="true" ForeColor="#003300" />
                                            <HeaderStyle BackColor="#9ac7f0" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="选项" FooterText="本题有效填写人次">
                                                    <ItemTemplate>
                                                        <%# Container.DataItem %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-Width="60">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltlTotal" runat="server"></asp:Literal>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Literal ID="ltlSum" runat="server"></asp:Literal>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="比例" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltlRate" runat="server"></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Height="35" />
                                        </ZL:ExGridView>
                                    </div>
                                </dd>
                            </dl>
                            <div class="clear"></div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <!--/content-->
            <div class="bottom">
                <input name="btnPrint" type="button" id="btnPrint" class="btn" value="打印" onclick="window.print();" />&nbsp; &nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="导出为Word文档" CssClass="btn" Width="120" OnClick="btnExport_Click" />
                &nbsp; &nbsp;
                    <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="btn" OnClick="btnReturn_Click" />
            </div>
            <div id="foot">
                当前页面执行时间：
  <script type="text/javascript"> 
<!-- 
    var startTime, endTime;
    var d = new Date();
    startTime = d.getTime();
    //--> 
  </script>
                <script type="text/javascript">d = new Date(); endTime = d.getTime
(); document.write((endTime - startTime) / 1000);</script>
            </div>
        </div>
    </form>
</body>
</html>
