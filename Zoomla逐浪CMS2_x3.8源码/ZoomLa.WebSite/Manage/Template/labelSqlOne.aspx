<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="labelSqlOne.aspx.cs" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" Inherits="ZoomLa.WebSite.Manage.Template.labelSqlOne" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
    <script type="text/javascript" src="/JS/Common.js"></script>
    <script type="text/javascript" src="/JS/Popmenu.js"></script>
    <script type="text/javascript" src="/js/Drag.js"></script>
    <script type="text/javascript" src="/js/Dialog.js"></script>
    <script type="text/javascript">
        function openurls(url) {
            Dialog.open({ URL: url });
        }
        function opentitle(url, title) {
            var diag = new Dialog();
            diag.Width = 800;
            diag.Height = 600;
            diag.Title = title;
            diag.URL = url;
            diag.show();
        }
        function closdlg() {
            Dialog.close();
        }
    </script>
    <script type="text/javascript">
        function addubb(ubb) {
            var textEl = document.getElementById("<%=textContent.ClientID %>");
            var text = ubb;

            if (textEl.createTextRange && textEl.caretPos) {
                var caretPos = textEl.caretPos;
                caretPos.text = caretPos.text.charAt(caretPos.text.length - 1) == ' ' ? text + ' ' : text;
            }
            else
                textEl.value = textEl.value + text;
        }
    </script>
    <script type="text/javascript">
        var start = 0, end = 0;
        var x, y;
        var dragspan;
        var inserttext;
        var nn6 = document.getElementById && !document.all;
        var isdrag = false;
        var labeltype = '0';
        function initDrag(e) {
            var oDragHandle = nn6 ? e.target : event.srcElement;
            if (oDragHandle.className == "spanfixdiv" || oDragHandle.className == "spanfixdiv1" || oDragHandle.className == "spanfixdivchechk") {
                isdrag = true;
                dragspan = document.createElement('div');
                dragspan.style.position = "absolute";
                dragspan.className = "dragspandiv";
                y = nn6 ? e.clientY + 5 : event.clientY + 5;
                x = nn6 ? e.clientX + 10 : event.clientX + 10;
                dragspan.style.width = oDragHandle.style.width;
                dragspan.style.height = oDragHandle.style.height;
                dragspan.style.top = y + "px";
                dragspan.style.left = x + "px";
                dragspan.innerHTML = oDragHandle.innerHTML;
                document.body.appendChild(dragspan);
                document.onmousemove = moveMouse;

                inserttext = oDragHandle.getAttribute("code");
                labeltype = oDragHandle.getAttribute("outtype");

                return false;
            }
        }
        function moveMouse(e) {
            if (isdrag) {
                dragspan.style.top = (nn6 ? e.clientY : event.clientY) + document.documentElement.scrollTop + 5 + "px";
                dragspan.style.left = (nn6 ? e.clientX : event.clientX) + document.documentElement.scrollLeft + 10 + "px";
                return false;
            }
        }
        function dragend(textBox) {
            if (isdrag) {
                savePos(textBox);
                cit(this);
            }
        }
        function savePos(textBox) {
            if (typeof (textBox.selectionStart) == "number") {
                start = textBox.selectionStart;
                end = textBox.selectionEnd;
            }
        }
        function dragend2(textBox) {
            if (isdrag) {
                savePos(textBox);
                var target = document.getElementById('<% =TxtTjValue.ClientID %>');
                if (nn6) {
                    var pre = target.value.substr(0, start);
                    var post = target.value.substr(end);
                    target.value = pre + inserttext + post;
                }
                else {
                    target.focus();
                    var range = document.selection.createRange();
                    range.text = inserttext;
                }
            }
        }
        function dragend3(textBox) {
            if (isdrag) {
                savePos(textBox);
                var target = document.getElementById('<% =TextBox1.ClientID %>');
                if (nn6) {
                    var pre = target.value.substr(0, start);
                    var post = target.value.substr(end);
                    target.value = pre + inserttext + post;
                }
                else {
                    target.focus();
                    var range = document.selection.createRange();
                    range.text = inserttext;
                }
            }
        }
        function dragend4(textBox) {
            if (isdrag) {
                savePos(textBox);
                var target = document.getElementById('<% =falsecontent.ClientID %>');
                if (nn6) {
                    var pre = target.value.substr(0, start);
                    var post = target.value.substr(end);
                    target.value = pre + inserttext + post;
                }
                else {
                    target.focus();
                    var range = document.selection.createRange();
                    range.text = inserttext;
                }
            }
        }
        function dragend5(textBox) {
            if (isdrag) {
                savePos(textBox);
                var target = document.getElementById('<% =TxtSqlWhere.ClientID %>');
                if (nn6) {
                    var pre = target.value.substr(0, start);
                    var post = target.value.substr(end);
                    target.value = pre + inserttext + post;
                }
                else {
                    target.focus();
                    var range = document.selection.createRange();
                    range.text = inserttext;
                }
            }
        }
        function dragend6(textBox) {
            if (isdrag) {
                savePos(textBox);
                var target = document.getElementById('<% =TxtOrder.ClientID %>');
                if (nn6) {
                    var pre = target.value.substr(0, start);
                    var post = target.value.substr(end);
                    target.value = pre + inserttext + post;
                }
                else {
                    target.focus();
                    var range = document.selection.createRange();
                    range.text = inserttext;
                }
            }
        }
        function cit(this) {
            var target = document.getElementById('<% =textContent.ClientID %>');
            if (nn6) {
                var pre = target.value.substr(0, start);
                var post = target.value.substr(end);

                if (labeltype == '1') {
                    target.value = pre + "{ZL.Label id=\"" + inserttext + "\"/}" + post;
                }
                else if (labeltype == '2') {

                    var link = "Insertlabel.aspx?n=" + escape(inserttext);
                    if (window.showModalDialog != null) {
                        var ret = showModalDialog(link, '', 'dialogWidth:500px; dialogHeight:300px; help: no; scroll: no; status: no; edge: sunken;');
                        if (ret != null) {
                            if (ret.replace(/^\s+|\s+$/g, "") == "") {
                                alert("不能输入空值");
                            }
                            else {
                                target.value = pre + ret + post;
                            }
                        }
                    }
                    else {
                        window.open(link, window, 'modal=yes,width=500,height=300,menubar=no,toolbar=no,location=no,resizable=no,status=no,scrollbars=no');
                    }
                }
                else if (labeltype == '3') {
                    target.text = "{ZL.Source id=\"" + inserttext + "\"/}";
                }
                else if (labeltype == '5') {
                    target.text = "{ZL.Page id=\"" + inserttext + "\"/}";
                }
                else if (labeltype == '6') {
                    target.text = "{ZL.Page id=\"" + inserttext + "\" num=\"500\"/}";
                }
                else {
                    target.value = pre + inserttext + post;
                }
            }
            else {
                target.focus();
                var range = document.selection.createRange();
                if (labeltype == '1') {
                    range.text = "{ZL.Label id=\"" + inserttext + "\"/}";
                }
                else if (labeltype == '2') {
                    var link = "Insertlabel.aspx?n=" + escape(inserttext);
                    if (window.showModalDialog != null) {
                        var ret = showModalDialog(link, '', 'dialogWidth:500px; dialogHeight:300px; help: no; scroll: no; status: no; edge: sunken;');
                        if (ret != null) {
                            if (ret.replace(/^\s+|\s+$/g, "") == "") {
                                alert("不能输入空值");
                            }
                            else {
                                range.text = ret;
                            }
                        }
                    }
                    else {
                        window.open(link, window, 'modal=yes,width=500,height=300,menubar=no,toolbar=no,location=no,resizable=no,status=no,scrollbars=no');
                    }
                }
                else if (labeltype == '3') {
                    range.text = "{ZL.Source id=\"" + inserttext + "\"/}";
                }
                else if (labeltype == '5') {
                    range.text = "{ZL.Page id=\"" + inserttext + "\"/}";
                }
                else if (labeltype == '6') {
                    range.text = "{ZL.Page id=\"" + inserttext + "\" num=\"500\"/}";
                }
                else {
                    range.text = inserttext;
                }
            }
        }
        function DragPos(textBox) {
            if (isdrag) {
                if (nn6) {
                    textBox.focus();
                }
                else {
                    try {
                        var rng = textBox.createTextRange();
                        rng.moveToPoint(event.x, event.y);
                        rng.select();
                    } catch (err) {
                    }
                }
            }
        }
        document.onmousedown = initDrag;

        document.onmouseup = function () {
            isdrag = false;
            if (dragspan != null) {
                document.body.removeChild(dragspan);
                dragspan = null;
            }
        }
        function changecate() {
            var obj = document.getElementById("<%=DropLabelType.ClientID %>");
            var tar = document.getElementById("<%=TxtLabelType.ClientID %>");
            var text = obj.value;
            if (text != "") {
                tar.value = text;
            }
        }
        function inputtxt(cc) {
            if (cc.value == "这里放入标签") {
                cc.value = '';
            }
        }
    </script>
    <script type="text/javascript">
        function admin_Size(num, objname) {
            var obj = document.getElementById(objname)
            if (parseInt(obj.rows) + num >= 3) {
                obj.rows = parseInt(obj.rows) + num;
            }
            if (num > 0) {
                obj.width = "90%";
            }
        }

        function selectvalue(vas, vas2) {
            if (document.getElementById("<%=DbTableDownList.ClientID %>")) {
                for (var i = 0; i < document.getElementById("<%=DbTableDownList.ClientID %>").length; i++) {
                    if (document.getElementById("<%=DbTableDownList.ClientID %>").options[i].value == vas) {
                        document.getElementById("<%=DbTableDownList.ClientID %>").selectedIndex = i;
                    }
                }
                setTimeout('__doPostBack(\'Wizard1$DbTableDownList\',\'\')', 0);

                document.getElementById("<%=DbTableDownList2.ClientID %>").length;
                for (var i = 0; i < document.getElementById("<%=DbTableDownList2.ClientID %>").length; i++) {
                    if (document.getElementById("<%=DbTableDownList2.ClientID %>").options[i].value == vas2) {
                        document.getElementById("<%=DbTableDownList2.ClientID %>").selectedIndex = i;
                    }
                }
                setTimeout('__doPostBack(\'Wizard1$DbTableDownList2\',\'\')', 0);
            }
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="BreadDiv" class="container-fluid mysite">
        <div class="row">
            <ol class="breadcrumb navbar-fixed-top">
                <li><a href="LabelManage.aspx">标签管理</a></li>
                <li><a href="<%=Request.RawUrl %>">
                    <asp:Label ID="LabelName" runat="server" /></a>
                    <a href="../Config/CreateTable.aspx" id="a1">[添加新表]</a>
                    <a href="LabelSql.aspx?LabelName=<%=Request["LabelName"] %>">[多步式标签编辑器]</a></li>
                <%=Call.GetHelp(25) %>
            </ol>
        </div>
    </div>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <table width="100%">
                    <tr>
                        <td align="center">基本信息  </td>
                        <td width="40"></td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td class="td_m" align="right"><strong>标签名称：</strong></td>
            <td>
                <asp:TextBox ID="TxtLabelName" class="form-control text_300" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="NReq" ControlToValidate="TxtLabelName" Display="Dynamic" ErrorMessage="必须输入名称" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="TxtLabelName" ErrorMessage="名称重复" OnServerValidate="CustomValidator1_ServerValidate" SetFocusOnError="True"></asp:CustomValidator></td>
        </tr>
        <tr>
            <td align="right"><strong>标签分类：</strong></td>
            <td>
                <asp:TextBox ID="TxtLabelType" class="form-control text_300" runat="server"></asp:TextBox>
                <asp:DropDownList ID="DropLabelType" CssClass="form-control text_md" runat="server" onchange="changecate();"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtLabelType" ErrorMessage="分类不能为空"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 80px; text-align: right;"><strong>标签类型：</strong></td>
            <td>
                <asp:RadioButtonList ID="RBLType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="2">动态标签</asp:ListItem>
                    <asp:ListItem Value="4">分页列表标签</asp:ListItem>
                    <asp:ListItem Value="3">数据源标签</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td align="right"><strong>标签说明：</strong></td>
            <td>
                <asp:TextBox ID="TxtLabelIntro" class="form-control text_300" runat="server" TextMode="MultiLine" Columns="50" Rows="3" Height="60px"></asp:TextBox></td>
        </tr>

    </table>

    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                数据查询
            </td>
        </tr>
        <tr>
            <td class="td_m" align="right"><strong>选择数据：</strong></td>
            <td>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>
                            <div id="selectdatediv" class="selectlist">
                                <table class="table table-striped table-bordered table-hover">
                                    <tr>
                                        <td>
                                            <table class="table table-striped table-bordered table-hover">
                                                <tr>
                                                    <td style="width: 100px; text-align: right; height: 22px;">请选择数据库： </td>
                                                    <td style="height: 22px">
                                                        <asp:DropDownList ID="DatabaseList" runat="server" AutoPostBack="True" CssClass="form-control text_300" OnSelectedIndexChanged="TableDownList_SelectedIndexChanged" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; text-align: right; height: 22px;">主表： </td>
                                                    <td style="height: 22px">
                                                        <asp:DropDownList ID="DbTableDownList" runat="server" AutoPostBack="True" CssClass="form-control text_300" OnSelectedIndexChanged="DBTableDownList_SelectedIndexChanged" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; text-align: right;">输出字段： </td>
                                                    <td>
                                                        <asp:ListBox ID="DbFieldDownList" runat="server" CssClass="form-control text_300" Height="220px" SelectionMode="Multiple" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table class="table table-striped table-bordered table-hover" id="table2" runat="server">
                                                <tr>
                                                    <td style="width: 100px; text-align: right; height: 22px;">请选择数据库： </td>
                                                    <td style="height: 22px">
                                                        <asp:DropDownList ID="DatabaseList2" CssClass="form-control text_300" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="TableDownList_SelectedIndexChanged2" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; text-align: right; height: 22px;">从表： </td>
                                                    <td style="height: 22px">
                                                        <asp:DropDownList ID="DbTableDownList2" runat="server" CssClass="form-control text_300" AutoPostBack="True" OnSelectedIndexChanged="DBTableDownList2_SelectedIndexChanged" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50px; text-align: right;">输出字段： </td>
                                                    <td>
                                                        <asp:ListBox ID="DbFieldDownList2" CssClass="form-control text_300" runat="server" Height="220px"  SelectionMode="Multiple" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <font color="red">先取数据表后再选择输出字段，支持Ctrl或Shift选取操作</font>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tbody id="tj" runat="server" visible="false">
            <tr>
                <td class="tdbgleft" style="text-align: right;">约束字段： </td>
                <td class="tdbg">
                    <div id="ycdiv" class="selectlist">
                        <asp:DropDownList ID="Dbtj" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TableJoin">
                            <asp:ListItem Value="left join">LeftJoin</asp:ListItem>
                            <asp:ListItem Value="Inner join">InnerJoin</asp:ListItem>
                            <asp:ListItem Value="outer join">OuterJoin</asp:ListItem>
                            <asp:ListItem Value="right join">RightJoin</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="DbFieldList" runat="server" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="TableJoin" />
                        =
                      <asp:DropDownList ID="DbFieldList2" runat="server" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="TableJoin" />
                        &nbsp;
                    </div>
                </td>
            </tr>
        </tbody>
        <tr>
            <td>查询表： </td>
            <td>
                <asp:TextBox ID="TxtSqlTable" class="form-control" runat="server" Columns="60" Rows="7" Width="483px" TextMode="MultiLine" AutoPostBack="true" Height="60px"></asp:TextBox>
                <br />
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="从选择数据中选择主表，若选择了从表，请在约束字段中设定表连接条件"></asp:Label>
                <asp:RequiredFieldValidator
                    ID="RequiredFieldValidator2" runat="server" ErrorMessage="，查询表不能为空!" ControlToValidate="TxtSqlTable"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 80px; text-align: right;">查询字段： </td>
            <td>
                <asp:TextBox ID="TxtSqlField" class="form-control" runat="server" Columns="60" Rows="7" Width="485px" TextMode="MultiLine" AutoPostBack="true" Height="60px"></asp:TextBox>
                <br />
                <asp:Button ID="Button3" class="btn btn-primary" Style="width: 110px;" runat="server" Text="设定查询字段" OnClick="Button3_Click" />
                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="从输出字段中选择查询的字段，未选字段点击为全选操作"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2" align="center"><%--<asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack=true OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                    <asp:ListItem Value="1" Text="基本信息"></asp:ListItem>
                    <asp:ListItem Value="2" Text="数据查询"></asp:ListItem>
                    <asp:ListItem Value="3" Text="参数设定"></asp:ListItem>
                    <asp:ListItem Value="4" Text="标签内容"></asp:ListItem>
                  </asp:DropDownList>--%>
                <%--   <asp:Button ID="PreviousButtonStep3" runat="server" CausesValidation="False" class="C_input"  CommandName="MovePrevious" Text="上一步" OnClick="PreviousButtonStep3_Click" />
                  <asp:Button ID="NextButtonStep3" runat="server" class="C_input" CommandName="MoveNext"  OnClick="StartNextButton_Click" Text="下一步" />
                  <asp:Button ID="Button10" OnClick="BtnSave_Click" class="C_input" runat="server" Style="cursor: pointer; cursor: hand;" Text="保存标签" />
                  <input id="Button9" type="button" class="C_input" value="取　消" onclick="window.location.href='LabelManage.aspx'" style="cursor: pointer; cursor: hand;" />--%></td>
        </tr>
    </table>

    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
               参数设定
            </td>
        </tr>
        <tr>
            <td class="td_m">添加参数： </td>
            <td>
                <table class="table table-striped table-bordered table-hover">
                    <tr class="text-center">
                        <td style="width: 10%">参数名称 </td>
                        <td style="width: 10%">默认值 </td>
                        <td style="width: 15%">参数类型 </td>
                        <td style="width: 25%">参数说明 </td>
                        <td>操作 </td>
                    </tr>
                    <asp:Repeater ID="repParam" runat="server" OnItemCommand="repParam_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("ParamName")%></td>
                                <td><%#Eval("ParamValue") %>
                                <td><%# GetParamType(Eval("ParamType","{0}"))%></td>
                                <td><%#Eval("ParamDesc")%></td>
                                <td>&nbsp;
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" CssClass="btn btn-primary" CommandArgument='<%# Eval("Param") %>'>修改</asp:LinkButton>
                                    |
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Del" CssClass="btn btn-primary" CommandArgument='<%# Eval("Param") %>'>删除</asp:LinkButton></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <br />
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td style="width:100px;">
                            <asp:TextBox ID="TxtParamName" CssClass="form-control" runat="server" Text="参数名称" Width="80px" /></td>
                        <td style="width:100px;">
                            <asp:TextBox ID="TxtParamValue" runat="server" Text="默认值"  CssClass="form-control text_s" /></td>
                        <td style="width:350px;">
                            <asp:DropDownList ID="DDLParamType" CssClass="form-control text_300" runat="server">
                                <asp:ListItem Selected="True" Value="1">普通参数</asp:ListItem>
                                <asp:ListItem Value="2">页面参数</asp:ListItem>
                                <asp:ListItem Value="3">单选参数</asp:ListItem>
                                <asp:ListItem Value="4">多选参数</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="TxtParamDesc" runat="server" Text="参数说明" CssClass="form-control text_300" /></td>
                        <td>
                            <asp:Button ID="BtnAddParam" runat="server" Text="添加" class="btn btn-primary" OnClick="BtnAddParam_Click" /></td>
                        <td>
                            <asp:HiddenField ID="HdnParam" runat="server" />
                            <asp:HiddenField ID="HdnTempParam" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>自定义参数：<br />
                 </td>
            <td>
                <div id="plist" class="plist">
                    <asp:Label ID="attlist" runat="server"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td>查询条件： </td>
            <td>
                <div id="gridviewclause" class="fielddiv">
                    <asp:DropDownList CssClass="form-control text_md" ID="DDLJointj" runat="server">
                        <asp:ListItem Selected="True">And</asp:ListItem>
                        <asp:ListItem>OR</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DDLFTable" CssClass="form-control text_md" runat="server" AutoPostBack="True" OnSelectedIndexChanged="BindTableField">
                        <asp:ListItem Selected="True" Value="1">主表</asp:ListItem>
                        <asp:ListItem Value="2">从表</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList CssClass="form-control text_md" ID="DDLTjField" runat="server" Width="120px" />
                    <asp:DropDownList ID="DDLtj"  CssClass="form-control text_md" runat="server">
                        <asp:ListItem>等于</asp:ListItem>
                        <asp:ListItem>大于</asp:ListItem>
                        <asp:ListItem>小于</asp:ListItem>
                        <asp:ListItem>大于等于</asp:ListItem>
                        <asp:ListItem>小于等于</asp:ListItem>
                        <asp:ListItem>不等于</asp:ListItem>
                        <asp:ListItem>在</asp:ListItem>
                        <asp:ListItem>象</asp:ListItem>
                        <asp:ListItem>不在</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="TxtTjValue" class="form-control text_md" runat="server"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="添加查询条件" OnClick="Button1_Click" />
                    <br />
                    <asp:TextBox ID="TxtSqlWhere" runat="server" class="form-control" onmouseup="dragend5(this)" Columns="60" Rows="3" Height="74px" Width="522px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td>字段排序： </td>
            <td>
                <div id="Div2" class="fielddiv">
                    <asp:DropDownList CssClass="form-control text_300" ID="DDLOrderTable" runat="server" AutoPostBack="True" OnSelectedIndexChanged="BindOrderField">
                        <asp:ListItem Selected="True">主表</asp:ListItem>
                        <asp:ListItem>从表</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList CssClass="form-control text_300" ID="DDLOrderField" runat="server" Width="120px" />
                    <asp:DropDownList CssClass="form-control text_300" ID="DDLOrder" runat="server">
                        <asp:ListItem Value="DESC">降序</asp:ListItem>
                        <asp:ListItem Value="ASC">升序</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="添加排序字段" OnClick="Button2_Click"
                        Style="cursor: pointer; cursor: hand;" ValidationGroup="false" />
                    <br />
                    <asp:TextBox ID="TxtOrder" CssClass="form-control text_300" runat="server" Columns="60" Rows="7" TextMode="MultiLine"
                        Width="522px" Height="69px" onmouseup="dragend6(this)"></asp:TextBox>
                    <br />
                    <span style="color: Red;">(大部分时候需要填写，建议：字段排序不能为空)</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtOrder"
                        Display="None" ErrorMessage="当标签为分页标签时,字段排序不能为空" Style="display: none" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 80px; text-align: right;">数据数目： </td>
            <td>
                <asp:TextBox ID="TextBox1" CssClass="form-control text_300" runat="server" Text="10" Width="50px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" align="center"><%--<asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack=true OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                    <asp:ListItem Value="1" Text="基本信息"></asp:ListItem>
                    <asp:ListItem Value="2" Text="数据查询"></asp:ListItem>
                    <asp:ListItem Value="3" Text="参数设定"></asp:ListItem>
                    <asp:ListItem Value="4" Text="标签内容"></asp:ListItem>
                  </asp:DropDownList>--%>
                <%--  <asp:Button ID="Button4" runat="server" CausesValidation="False" class="C_input" CommandName="MovePrevious" Text="上一步" OnClick="PreviousButtonStep3_Click" />
                  <asp:Button ID="Button5" runat="server" class="C_input" CommandName="MoveNext" OnClick="StartNextButton_Click" Text="下一步" />
                  <asp:Button ID="Button12" OnClick="BtnSave_Click" class="C_input" runat="server" Style="cursor: pointer; cursor: hand;" Text="保存标签" />
                  <input id="Button11" type="button" class="C_input" value="取　消" onclick="window.location.href='LabelManage.aspx'" style="cursor: pointer; cursor: hand;" />--%></td>
        </tr>
    </table>
    <table class="table table-striped table-bordered table-hover">

        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <table width="100%">
                    <tr>
                        <td align="center">标签内容  </td>
                        <td width="40"></td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td class="td_m">自定义参数：<br />
                 </td>
            <td>
                <div id="Div3" class="plist">
                    <asp:Label ID="attlist1" runat="server"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td>判断模式： </td>
            <td bgcolor="#e8f5ff">
                <asp:CheckBox ID="boolmodel" runat="server" Text="开启判断模式" AutoPostBack="True" />
                <font color="red">[开启后将根据判断条件输出内容]</font>
                <iframe id="I1" frameborder="0" name="I1" scrolling="no" src="/manage/Template/label.htm" style="height: 30px; width: 530px; margin-left: 142px;"></iframe>
            </td>
        </tr>
        <tr id="isbool" runat="server" visible="false" bgcolor="#cbe6fc" style="background: #cbe6fc">
            <td class="tdbgleft" style="width: 80px; text-align: right; height: 24px">模式设置： </td>
            <td><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Modelvalue" ErrorMessage="条件数值不能为空!" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                <asp:DropDownList ID="Modeltypeinfo" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="计数判断">计数判断</asp:ListItem>
                    <asp:ListItem Value="用户登陆判断">用户登陆判断</asp:ListItem>
                    <asp:ListItem Value="参数判断">参数判断</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="Valueroot" runat="server" Width="179px" onmousedown="inputtxt(this)"
                    onfocus="inputtxt(this)" value="这里放入标签"></asp:TextBox>
                <asp:DropDownList ID="addroot" runat="server" Visible="false" AutoPostBack="True">
                    <asp:ListItem Value="循环计算">循环计算</asp:ListItem>
                    <asp:ListItem Value="一直累加">一直累加</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="setroot" runat="server">
                    <asp:ListItem Value="大于">大于</asp:ListItem>
                    <asp:ListItem Value="等于" Selected>等于</asp:ListItem>
                    <asp:ListItem Value="小于">小于</asp:ListItem>
                    <asp:ListItem Value="不等于">不等于</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="Modelvalue" class="l_input" runat="server" Width="140px" ToolTip="注意：当判断是否为空时，请不要填写任何内容"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" ForeColor="#339933" Visible="False" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr>
            <td class="spacingtitle" style="width: 100%; text-align: left;" colspan="2"><strong>标签内容:
                  <asp:Label ID="Label8" runat="server" Text="(<font color=red>满足</font>判断)"
                      Visible="false"></asp:Label>
            </strong></td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td style="width:500px;">
                            <table class="table table-striped table-bordered table-hover">
                                <tbody id="ss">
                                    <tr align="center">
                                        <td>
                                            <div id="Tab1_header">
                                                <table class="table table-striped table-bordered table-hover">
                                                    <tr align="center">
                                                        <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)" style="width: 81px">自定标签 </td>
                                                        <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)" style="width: 81px">字段标签 </td>
                                                        <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)" style="width: 81px">系统标签 </td>
                                                        <td id="TabTitle3" class="tabtitle" onclick="ShowTabs(3)" style="width: 81px">扩展函数 </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                                <table class="table table-striped table-bordered table-hover">
                                                    <tbody id="Tabs0">
                                                        <tr align="center">
                                                            <td>
                                                                <asp:DropDownList ID="DDLCate" CssClass="form-control text_300" runat="server" OnSelectedIndexChanged="ChangeCate" AutoPostBack="true"></asp:DropDownList></td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <div style="overflow: auto; height: 410px">
                                                                    <asp:Label ID="LblLabel" runat="server"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <tbody id="Tabs1" style="display: none">
                                                        <tr align="center">
                                                            <td>
                                                                <div style="overflow: auto; height: 410px">
                                                                    <asp:Label ID="LblColumn" runat="server" Text="标签字段"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <tbody id="Tabs2" style="display: none">
                                                        <tr align="center">
                                                            <td>
                                                                <div style="overflow: auto; height: 410px">
                                                                    <asp:Label ID="LblSysLabel" runat="server"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <tbody id="Tabs3" style="display: none">
                                                        <tr align="center">
                                                            <td>
                                                                <div style="overflow: auto; height: 410px">
                                                                    <asp:Label ID="LblFunLabel" runat="server"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td align="left">
                            <div id="Textarea">
                                <asp:TextBox TextMode="MultiLine" CssClass="form-control" runat="server" ID="textContent" Width="100%" Style="border: solid 1px #8EC1EE" Height="410px"></asp:TextBox>
                                <%=Call.GetUEditor("textContent",4) %>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="s1" runat="server" visible="false">
            <td class="spacingtitle" style="width: 100%; text-align: left;" colspan="2"><strong>标签内容:(<font color="red">不满足</font>判断)</strong></td>
        </tr>
        <tr id="s2" runat="server" visible="false">
            <td class="tdbgleft" style="width: 100%; text-align: right;" colspan="2">
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td align="left"><font color="red"><b>当选择计数判断时，下面只能填写{Repeate}标签里面的内容</b></font>
                            <asp:TextBox TextMode="MultiLine" runat="server" ID="falsecontent" CssClass="form-control" Width="100%" Height="231px" onmouseup="dragend4(this)"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:HiddenField ID="HdnlabelID" runat="server" />
                <div style="float: left; padding-left: 250px; display: none">
                    <input id="BtnCancel" type="button" class="btn btn-primary" value="取　消" onclick="window.location.href = 'LabelManage.aspx'" style="cursor: pointer;" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnSave" OnClick="BtnSave_Click" class="btn btn-primary" runat="server" Style="cursor: pointer;" Text="保存标签" />
                    &nbsp;&nbsp;&nbsp;
                </div>
                <div id="Div1" style="float: left"></div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center"><%--<asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack=true OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                    <asp:ListItem Value="1" Text="基本信息"></asp:ListItem>
                    <asp:ListItem Value="2" Text="数据查询"></asp:ListItem>
                    <asp:ListItem Value="3" Text="参数设定"></asp:ListItem>
                    <asp:ListItem Value="4" Text="标签内容"></asp:ListItem>
                  </asp:DropDownList>--%>
                <%--  <asp:Button ID="PreviousButtonFinish" runat="server" CausesValidation="False" class="C_input" CommandName="MovePrevious" Text="上一步" OnClick="PreviousButtonStep3_Click" />--%>
                <asp:Button ID="Button6" OnClick="BtnSave_Click" class="btn btn-primary" runat="server" Style="cursor: pointer; cursor: hand;" Text="保存标签" />
                <input id="Button7" type="button" class="btn btn-primary" style="width: 110px;" value="取　消" onclick="window.location.href = 'LabelManage.aspx'" style="cursor: pointer; " /></td>
        </tr>
    </table>

      <%--</ContentTemplate>
</asp:UpdatePanel>--%>

    <div class="infom">
        <div id="info_tit"><a onclick="info_show('1')" class="spacingtitle" title="显示辅助说明">辅助说明</a>  </div>
        <div id="info_div1" class="infoclass" style="display: none">
            <%-- <span><a onclick="info_hide('1')"  >×</a></span>--%>
            <strong>基本信息 :</strong>
            <br />
            本操作用于定义标签基本信息,标签建好后可以在前台任意位置调用输出动态数据：
                <ul class="disc">
                    <li>动态标签可以在任何页面引用,分页列表支持分页显示,数据源标签可节省查询效率,需要在模板中引用数据输出。</li>
                    <li>动态标签的调用格式为{ZL.Label id="标签名" NodeID="0" /}，其中 NodeID="0" 为变量参数，可于第二步编辑中定义或增减变量。</li>
                    <li>标签支持在任意动态页面输出，如&#60%Call.Label("{ZL.Label id=\"标签名称\" NodeID=\"91\" /}"); %&#62方法则能在非模板引擎页输出动态标签。</li>
                    <li>系统采用智能多库算法，数据库名请不要采用数字开头并夹带小数点等特殊符号，如[2009data.1]格式则是非法数据库名格式，不符合数据库联查标准。</li>
                </ul>

            <%--  <div id="info_div2" class="infoclass"  >--%>
            <strong>数据查询 :</strong>
            <br />
            数据库一般查询主数据库[仅高级版本支持从数据库]：
                 <ul class="disc">
                     <li>多表查询应采用ID关联,比如文章模型则采用ZL_CommonModel left join ZL_C_Article on ZL_CommonModel.ItemID=ZL_C_Article.ID方法关联查询。</li>
                     <li>为保持查询效率，每次查询请撷取所需字段，从而提升效率。</li>
                     <li>建立标签时应注意多表间的逻辑，点此<a href="javascript:void(0);" onclick="javascript:window.open('http://www.z01.com/learn')">下载数据字典</a>以提升开发效率。</li>
                 </ul>
            <%-- id="info_div3"--%>
            <strong>参数设定:</strong>
            <br />
            设定您要定义的参数：
                 <ul class="disc">
                     <li>支持中文名定义参数,参数可以在前台模板中传递数值。</li>
                     <li>支持采用页面采数传递信息。</li>
                     <li>参数可自由拖放到查询条件、排序规则、数据条目等位置。</li>
                     <li>参数支持普通参数、页面参数、单选参数、多选参数四种模式，其中单选参数与多选参数的值以$符号切割，如：华东$华南$华西$华北。</li>
                     <li>查询条件支持嵌入T-sql语法规则，例如一条查询指定节点下的二级数据规则，可设定条件：NodeID in (select NodeID from ZL_Node where ParentID=@NodeID) or NodeID=@NodeID,当然也可以采用更高效的父节点属性查询：NodeID=@NodeID or FirstNodeID=@NodeID（注@NodeID为节点变量）。</li>
                 </ul>
            <%-- id="info_div4"--%>
            <strong>标签内容 :</strong>
            <br />
            标签内容决定输出的信息表现形式：
                 <ul class="disc">
                     <li>标签内容支持CSS等HTML定义，可以从左侧拖拽标签参数使用。</li>
                     <li>重复输出数据请需要在{Repeate}{/Repeate}内进行排序输出。</li>
                     <li>扩展函数支持字段格式化、截取、重新筛选、取值获值等操作，使数据输出更加科学合理。</li>
                     <li>由于多浏览器兼容与W3C规则严谨度要求，建议标签预览仅作为预览使用，不作为保存操作，以免产生乱码。</li>
                 </ul>
        </div>
    </div>
    <script>
        function info_show(id) {
            if (document.getElementById("info_div" + id).style.display == "none") {
                jQuery("#info_div" + id).css("display", "block");
                document.getElementById("info_tit").innerHTML = "<a onclick='info_show(1)' class='spacingtitle'  title='隐藏辅助说明'>辅助说明</a> ";
            }
            else {
                //             }
                //             function info_hide(id) {
                jQuery("#info_div" + id).css("display", "none");
                document.getElementById("info_tit").innerHTML = "<a onclick='info_show(1)' class='spacingtitle'  title='显示辅助说明'>辅助说明</a> ";
            }
        }
    </script>
</asp:Content>
