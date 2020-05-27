<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollectionStep2.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.Collect.CollectionStep2" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加采集项目</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <div class="panel panel-primary">
        <div class="panel-heading"><span class="margin_l5">添加采集链接</span></div>
        <div class="panel-body">
            <table class="table table-striped table-bordered">
                    <tr>
                        <td style="width:500px;">
                              <asp:TextBox ID="txtHtml" class="form-control" runat="server" TextMode="MultiLine" style="max-width:500px;min-width:500px;" Height="500px"></asp:TextBox>
                              <span style="color:red;">*该列表框为最终需采集的地址列表,用回车分隔</span>
                              <asp:RequiredFieldValidator runat="server" ID="tr1" ControlToValidate="txtHtml" Display="Dynamic" ErrorMessage="采集链接列表不能为空" ForeColor="Red" ValidationGroup="NextG"/>
                            <br />
                            <asp:Button ID="Button4" class="btn btn-primary" runat="server" Text="获取源代码" OnClick="Button4_Click" />
                            <asp:Label ID="lblLink" runat="server"></asp:Label>
                        </td>
                        <td style="vertical-align:top;">
                            <ul class="nav nav-tabs">
                                <li class="active"><a onclick="ShowTabs(0)" href="#Tabs0" data-toggle="tab">列表设置</a></li>
                       <%--         <li><a onclick="ShowTabs(1)" href="#Tabs1" data-toggle="tab">分页设置</a></li>--%>
                            </ul>
                            <table class="table table-striped table-bordered">
                                <tbody id="Tabs0">
                                    <tr>
                                        <td class="td_l text-right">列表开始代码：</td>
                                        <td>
                                            <asp:TextBox ID="txtListStart" class="form-control" runat="server" TextMode="MultiLine" Width="230px" Rows="5" Height="38px"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="r1" ControlToValidate="txtListStart" Display="Dynamic" ErrorMessage="规则不能为空" ForeColor="Red" ValidationGroup="NextG"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="text-right">列表结束代码：</td>
                                        <td>
                                            <asp:TextBox ID="txtListEnd" class="form-control" runat="server" TextMode="MultiLine" Width="230px" Rows="5" Height="38px"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="r2" ControlToValidate="txtListEnd" Display="Dynamic" ErrorMessage="规则不能为空" ForeColor="Red" ValidationGroup="NextG"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="text-right">链接前填充：</td>
                                        <td>
                                            <asp:TextBox ID="Pre_T" class="form-control" Style="width: 230px;" runat="server" TextMode="MultiLine" Width="151px" Rows="5" Height="38px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="text-right">链接后填充：</td>
                                        <td>
                                            <asp:TextBox ID="End_T" class="form-control" Style="width: 230px;" runat="server" TextMode="MultiLine" Width="151px" Rows="5" Height="38px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="FilterA_Btn" runat="server" class="btn btn-primary" Text="筛选链接" OnClick="FilterA_Btn_Click" OnClientClick="return checkList('txtListStart','txtListEnd')" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">（链接为需要采集的文章网址,格式示例:http://www.z01.com/pub/2231.shtml）</td>
                                    </tr>
                                </tbody>
                                <tbody id="Tabs1" style="display: none">
                                    <tr>
                                        <td>选择分页类型：
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input id="Radio1" runat="server" type="radio" name="rdList" value="1" checked="true" onclick="ShowText(0)" />不分页
                                            <br />
                                            <input id="Radio2" runat="server" type="radio" name="rdList" value="2" onclick="ShowText(1)" />从源代码中获取下一页的URL
										    <br />
                                            <input id="Radio3" runat="server" type="radio" name="rdList" value="3" onclick="ShowText(2)" />批量指定分页URL代码
										    <br />
                                            <input id="Radio4" runat="server" type="radio" name="rdList" value="4" onclick="ShowText(3)" />手动添加分页URL代码
										    <br />
                                            <input id="Radio5" runat="server" type="radio" name="rdList" value="5" onclick="ShowText(4)" />从源代码中获取分页URL
                                        </td>
                                    </tr>
                                    <tr id="Txt0" style="display: none">
                                        <td></td>
                                    </tr>
                                    <tr id="Txt1" style="display: none" runat="server">
                                        <td>
                                            <table class="table table-striped table-bordered table-hover">
                                                <tr>
                                                    <td class="text-right">
                                                        “下一页”URL开始代码
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNextPageBegin" class="form-control" runat="server" TextMode="MultiLine" Rows="8" Width="230px" Height="60px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="text-right">“下一页”URL结束代码</td>
                                                    <td>
                                                        <asp:TextBox ID="txtNextPageEnd" class="form-control" runat="server" TextMode="MultiLine" Rows="8" Width="230px" Height="60px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="Button7" class="btn btn-primary" runat="server" Text="测试下一页" OnClick="Button7_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="Txt2" style="display: none" runat="server">
                                        <td>
                                            Url地址：
                                            <br />
                                            Url地址：<br />
                                            <asp:TextBox ID="txturl" class="form-control" runat="server" Width="220px"></asp:TextBox>
                                            例：http://www.xxxxx.com/news/index_{$ID}.html {$ID}代表分页数
                                            <br />
                                            ID范围：<asp:TextBox ID="txtBeginNum" class="form-control" runat="server" Width="50px"></asp:TextBox>
                                             TO
                                            <asp:TextBox ID="txtEndNum" runat="server" CssClass="form-control" Width="50px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="Txt3" style="display: none" runat="server">
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td style="background-color: White; height: 1px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtUrlList" class="form-control" runat="server" TextMode="MultiLine" Width="230px" Rows="20" Height="60px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="Txt4" style="display: none" runat="server">
                                        <td>
                                            <br />
                                            分页代码开始： <asp:TextBox ID="txtPageDivBegin" class="form-control" runat="server" TextMode="MultiLine" Width="230px" Rows="5" Height="60px"></asp:TextBox><br />
                                             分页代码结束： <asp:TextBox ID="txtPageDivEnd" class="form-control" runat="server" TextMode="MultiLine" Width="220px" Rows="5"></asp:TextBox><br />
                                            分页URL开始代码： <asp:TextBox ID="txtBegin" class="form-control" runat="server" Width="220px"></asp:TextBox><br />
                                            分页URL结束代码： <asp:TextBox ID="txtEnd" class="form-control" runat="server" Width="220px"></asp:TextBox><br />
                                            <asp:Button ID="Button8" class="btn btn-primary" runat="server" Text="测试从源代码中获取分页URL" OnClick="Button8_Click" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
        </div>
        <div class="panel-footer">
            <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="上一步" OnClick="Button2_Click" />
            <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="下一步" OnClick="Button1_Click" ValidationGroup="NextG" />
            <input id="Button3" class="btn btn-primary" type="button" value="返回" onclick="window.location.href = 'CollectionManage.aspx'" />
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"></asp:UpdatePanel>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Common.js" type="text/javascript"></script>
    <script>
        var tID = 0;
        var arrTabs = new Array("Tabs0", "Tabs1");
        function ShowTabs(ID) {
            if (ID != tID)
            {
                document.getElementById(arrTabs[tID].toString()).style.display = "none";
                document.getElementById(arrTabs[ID].toString()).style.display = "";
                tID = ID;
            }
        }
        var arrText = new Array("Txt0", "Txt1", "Txt2", "Txt3", "Txt4");
        var sID = 0;
        function ShowText(ID) {

            try {
                document.getElementById(arrText[sID].toString()).style.display = "none";
                document.getElementById(arrText[ID].toString()).style.display = "";
            } catch (Error)
            { }

            sID = ID;
            document.getElementById("<%=HiddenField1.ClientID %>").value = ID;
        }
        function checkList(start, end) {
            if (document.getElementById(start).value != "" && document.getElementById(end).value != "") {
                return true;
            }
            else {
                alert("请输入开始代码和结束代码");
                return false;
            }
        }
        var s = document.getElementById("<%=HiddenField1.ClientID %>").value;
        if (s == '') { s = 0; }
        ShowText(s);
    </script>
</asp:Content>
