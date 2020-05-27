<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoteResult.aspx.cs" Inherits="ZoomLaCMS.Plugins.VoteResult" EnableViewStateMac="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>投票结果</title>
<link href="/App_Themes/Guest/Survey.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div id="main" class="container">
    <div id="divContent" class="panel panel-default" runat="server">
        <div class="panel-heading">
            <asp:Literal ID="ltlSurveyName" runat="server" Text="Label"></asp:Literal>
            的调查结果
                <div class="date">
                    提交时间:
            <asp:Literal ID="ltlDate" runat="server"></asp:Literal>
                </div>
        </div>
        <div class="result panel-body">
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
                                <ZL:ExGridView ID="gviewOption" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="table table-bordered">
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
    <div class="bottom text-center">
        <input name="btnPrint" type="button" id="btnPrint" class="btn btn-info" value="打印结果" onclick="window.print();" />
        <asp:Button ID="btnExport" runat="server" Text="导出为Word" CssClass="btn btn-info" OnClick="btnExport_Click" />
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script"></asp:Content>
