<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollectionStep2.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.Collect.CollectionStep2" MasterPageFile="~/Manage/I/Default.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>添加采集项目</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <span class="margin_l5">添加采集链接</span>
            <input type="button" class="btn btn-info" value="获取源代码" onclick="$('#txtHtml').val($('#SourceHtml_Hid').val())" />
            <asp:Label ID="lblLink" runat="server"></asp:Label>
        </div>
        <div class="panel-body">
            <div style="width:45%;float:left;">
                <asp:HiddenField runat="server" ID="SourceHtml_Hid" />
                <asp:TextBox ID="txtHtml" class="form-control" runat="server" TextMode="MultiLine" Height="520px"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="tr1" ControlToValidate="txtHtml" Display="Dynamic" ErrorMessage="采集链接列表不能为空" ForeColor="Red" ValidationGroup="NextG" />
            </div>
            <div style="width:53%;float:right;">
        <%--        <ul class="nav nav-tabs">
                    <li class="active"><a onclick="ShowTabs(0)" href="#tab0" data-toggle="tab">列表设置</a></li>
                </ul>--%>
                <table class="table table-striped table-bordered">
                    <tbody id="Tabs0">
                        <tr>
                            <td class="td_l">列表开始代码：</td>
                            <td>
                                <ZL:TextBox ID="ListStart_T" class="form-control" Height="60" runat="server" TextMode="MultiLine" AllowEmpty="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>列表结束代码：</td>
                            <td>
                                <ZL:TextBox ID="ListEnd_T" class="form-control" Height="60" runat="server" TextMode="MultiLine" AllowEmpty="false"/>
                            </td>
                        </tr>
                <%--        <tr><td>必须以指定字符开始：</td>
                            <td><asp:TextBox ID="CharStart_T" class="form-control" Height="60" runat="server" TextMode="MultiLine" placeholder="多条件用|切割" /></td>
                        </tr>
                        <tr><td>必须以指定字符结尾：</td><td>
                            <asp:TextBox ID="CharEnd_T" class="form-control" Height="60" runat="server" TextMode="MultiLine" placeholder="多条件用|切割" />
                        </td></tr>--%>
                        <tr><td>必须包含指定字符：</td><td>
                            <asp:TextBox ID="CharContain_T" class="form-control" Height="60" runat="server" TextMode="MultiLine" placeholder="多条件用|切割" />
                        </td></tr>
                        <tr><td>字符正则：</td><td><asp:TextBox runat="server" ID="CharRegex_T" class="form-control" Height="60" TextMode="MultiLine" placeholder="请输入正则表达式" /></td></tr>
                        <tr>
                            <td>链接前填充：</td>
                            <td>
                                <asp:TextBox ID="FillStart_T" class="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>链接后填充：</td>
                            <td>
                                <asp:TextBox ID="FillEnd_T" class="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="FilterA_Btn" runat="server" class="btn btn-primary" Text="筛选链接" OnClick="FilterA_Btn_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">（链接为需要采集的文章网址,格式示例:http://www.z01.com/pub/2231.shtml）</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="panel-footer text-center">
            <span class="pull-left rd_red">该列表框为最终需采集的地址列表,用回车分隔,示例格式:www.z01.com/Article/news/3060.shtml</span>
            <a href="CollectionStep1.aspx?ItemId=<%:ItemID %>" class="btn btn-primary">上一步</a>
            <asp:Button ID="Next_Btn" class="btn btn-primary" runat="server" Text="下一步" OnClick="Next_Btn_Click" ValidationGroup="NextG" />
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
