<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerList.aspx.cs" Inherits="manage_AddCRM_CustomerList" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title><%=lang.LF("客户管理")%></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="divImp" class="Imp_div" runat="server" visible="false" style="display:block;">
    选择Excel导入源： <ZL:FileUpload ID="fileImp" runat="server" Style="margin-bottom: 2px; float: left;" />
    <asp:Button runat="server" ID="ExcelData" Text="从Excel导入数据" OnClick="ExcelData_Click"
        OnClientClick="if(document.getElementById('fileImp').value.length==0){alert('请选择Excel(CSV)文件,可下载模板!');return false};
            else {setTimeout(function () { document.getElementById('ExcelData').disabled = true;},50)}" />
    <asp:Button runat="server" ID="DownTemp" OnClick="Template_Click" Text="下载模板" />
    <span style="color: red;">*</span><span style="color: #0E529D;">(下载后用EXCEL打开,点击顶部[启用编辑],从第二行开始按规范填写并保存即可)</span>
</div>
<ul class="nav nav-tabs">
            <li class="active"><a href="#tab0" data-toggle="tab" onclick="ShowTabs(this,0)">全部客户</a></li>
            <li><a href="#tab1" data-toggle="tab" onclick="ShowTabs(this,1)">到期未跟进</a></li>
            <li><a href="#tab2" data-toggle="tab" onclick="ShowTabs(this,2)">未分配跟进人</a></li>
        </ul>
<div>
        <div id="tab1">
        <table  class="table table-striped table-bordered table-hover" id="deTable">
        <tbody id="Tbody1"> 
            <tr class="tdbg">
                <td style="width: 5%; font-weight: bold">
                    ID
                </td>
                <td style="width: 15%; font-weight: bold">
                    <%=lang.LF("客户名称")%>
                </td>
                <td  style="width: 8%; font-weight: bold">
                    <%=lang.LF("客户类型")%>
                </td>
                <td style="width: 15%; font-weight: bold">
                    <%=lang.LF("客户编号")%>
                </td>
                  <td style="width: 8%; font-weight: bold">
                    跟进人
                </td>
                <td class="td_l" style="font-weight: bold">
                    时间
                </td>
                <td style=" font-weight: bold">
                    <%=lang.LF("操作")%>
                </td>
            </tr>
            <ZL:ExRepeater ID="RPT" runat="server" PageSize="10" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='8'><div class='text-center'>" PageEnd="</div></td></tr>">
                <ItemTemplate>
                    <tr ondblclick="getinfo(<%#Eval("Flow")%>);">
                        <td><%#Eval("Flow")%></td>
                        <td style="cursor:pointer"><a href="ViewCustomer.aspx?FieldName=Person_Add&modelid=<%=ModelID %>&id=<%#Eval("Flow") %>"><%#Eval("P_name")%></a></td>
                        <td><%#Eval("Client_Type","{0}")=="1"?"企业":"个人"%></td>
                        <td><%#Eval("Code")%></td>
          
                        <td><a href="<%=CustomerPageAction.customPath2 %>User/UserInfo.aspx?id=<%#Eval("FPManID")%>"><%#Eval("p_name")%></td>
                        <td><%#Eval("Add_Date","{0:yyyy年MM月dd日}")%></td>
                        <td>
                        <a href="ViewCustomer.aspx?FieldName=Person_Add&id=<%#Eval("Flow") %>&modelid=<%=ModelID %>" class="option_style"><i class="fa fa-eye"></i>预览</a> <a href="CustomerManage.aspx?ModelID=<%=ModelID %>&FieldName=Person_Add&menu=edit&id=<%#Eval("Flow") %>" class="option_style"><i class="fa fa-edit"></i>修改</a> <a href="?menu=delete&modelid=<%=ModelID %>&code=<%#Eval("Code") %>" onclick="return confirm('你确定要将所有选择删除吗？');"  class="option_style"><i class="fa fa-trash-o"></i>删除</a></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
        </tbody>
    </table>
        </div>
        <div id="tab2" style="display:none;">
            <ZL:ExGridView ID="EGV" runat="server" DataSourceID="expireData" AllowPaging="True" RowStyle-CssClass="tdbg" AutoGenerateColumns="False"
                class="table table-striped table-bordered table-hover"
                EnableTheming="False" EmptyDataText="没有任何数据！" AllowSorting="True"
                EnableModelValidation="True" SerialText="" CheckBoxFieldHeaderWidth="3%">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="ID" />
                    <asp:BoundField HeaderText="跟进内容" DataField="UserInfo" HtmlEncode="false" />
                    <asp:TemplateField HeaderText="跟进时间">
                        <ItemTemplate>
                            <%# Convert.ToDateTime(Eval("BeginTime")).ToString("yyyy年MM月dd日") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="下次跟进时间">
                        <ItemTemplate>
                            <%# Convert.ToDateTime(Eval("CompletionTime")).ToString("yyyy年MM月dd日") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="跟进人" DataField="fpman" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="CustomerManage.aspx?FieldName=Person_Add&menu=edit&id=<%#Eval("TargetID") %>">客户信息</a> 
                            <a href="javascript:;" onclick="if (confirm('确定跟进后将不会再提示')){postToCS('FP', '<%#Eval("ID") %>',this)}">确认跟进</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="tdbg" HorizontalAlign="Center" />
                <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
            <asp:ObjectDataSource runat="server" ID="expireData" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCrmExpireList" TypeName="GetDSData">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="disType" QueryStringField="disType" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <div id="tab3" style="display:none;">
            <ZL:ExGridView ID="EGV3" runat="server" DataSourceID="noFPManData" AllowPaging="True" RowStyle-CssClass="tdbg" AutoGenerateColumns="False"
                CellPadding="2" CellSpacing="1" ForeColor="Black" CssClass="table table-striped table-bordered table-hover" Width="100%"
                GridLines="None" EnableTheming="False" EmptyDataText="没有任何数据！" AllowSorting="True"
                EnableModelValidation="True" SerialText="" CheckBoxFieldHeaderWidth="3%" IsHoldState="True">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="Flow" />
                    <asp:BoundField HeaderText="客户名称" DataField="p_name" />
                    <asp:BoundField HeaderText="客户编号" DataField="Code" />
                    <asp:TemplateField HeaderText="添加时间">
                        <ItemTemplate>
                            <%# Convert.ToDateTime(Eval("Add_Date")).ToString("yyyy年MM月dd日") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="CustomerManage.aspx?FieldName=Person_Add&menu=edit&id=<%#Eval("Flow") %>">分配跟进人</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="tdbg" HorizontalAlign="Center" />
                <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
            <asp:ObjectDataSource runat="server" ID="noFPManData" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCrmNoFPManList" TypeName="GetDSData">
            </asp:ObjectDataSource>
            </div>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <link href="/Plugins/JqueryUI/EasyDialog/easydialog.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/Common.js"></script>
    <script src="/Plugins/JqueryUI/EasyDialog/easydialog.min.js"></script>
    <script type="text/javascript">
        function getinfo(id) {
            location.href = 'ViewCustomer.aspx?FieldName=Person_Add&modelid=<%=ModelID %>&id=' + id;
        }
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
        function ShowTabs(obj, id) {//Div切换
            //$(obj).addClass("titlemouseover").siblings().removeClass("titlemouseover").addClass("tabtitle");
            $("#tab" + (id+1)).show().siblings().hide();
        }
        $().ready(function () {
            //$.ajax({
            //    type: "post",
            //    url: "CustomerList.aspx",
            //    data: { action: "getExpireNum" },
            //    success: function (data) { if (data != 1) disRemind(data); }
            //});
        });

        function disRemind(s) {
            easyDialog.open({
                container: {
                    header: '提醒',
                    content: s,
                    yesFn: true,
                    noFn: true
                }
            });
        }

        function postToCS(a, id, obj) {
            $.ajax({
                type: "Post",
                url: "CustomerList.aspx",
                data: { action: a, pid: id },
                success: function (data) { if (data == 1) { $(obj).parent().parent().remove(); } else alert(data); },
                error: function (data) { alert(data); }
            });
        }
        HideColumn("2,3,4,5","", "deTable");
    </script>
</asp:Content>