<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCustomer.aspx.cs" Inherits="manage_AddCRM_ViewCustomer" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title id="User_Title" runat="server">客户列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Label ID="Label1" runat="server" Text="查看个人客户" Visible="false"></asp:Label>
    <ul class="nav nav-tabs">
        <li class="active"><a href="#tab0" data-toggle="tab" onclick="ShowTabs(0)">基本信息</a></li>
        <li><a href="#tab1" data-toggle="tab" onclick="ShowTabs(1)">联络信息</a></li>
        <li><a href="#tab2" data-toggle="tab" onclick="ShowTabs(2)">企业信息</a></li>
        <li><a href="#tab3" data-toggle="tab" onclick="ShowTabs(7)">企业选项</a></li>
        <li><a href="#tab4" data-toggle="tab" onclick="ShowTabs(5)">备注信息</a></li>
        <li><a href="#tab5" data-toggle="tab" onclick="ShowTabs(4)">个人信息</a></li>
        <li><a href="#tab6" data-toggle="tab" onclick="ShowTabs(3)">业务信息</a></li>
        <li><a href="#tab7" data-toggle="tab" onclick="ShowTabs(6)">会员选项</a></li>
    </ul>
    <table id="Tabs0" class="table table-striped table-bordered table-hover">
                        <tr>
                            <td align="right" class="tdbgleft">
                                客户类别：
                            </td>
                            <td>
                                <asp:Label ID="ClientType" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" class="tdbgleft">
                            </td>
                            <td>
                                <table id="Table1" border="0">
                                    <tr>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" align="right" class="tdbgleft">
                                客户名称：
                            </td>
                            <td style="width: 38%">
                                <asp:Label ID="TxtClientName" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 15%" align="right" class="tdbgleft">
                                客户编号：
                            </td>
                            <td style="width: 38%">
                                <asp:Label ID="TxtClientNum" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                助记名称：
                            </td>
                            <td>
                                <asp:Label ID="TxtShortedForm" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" class="tdbgleft">
                                上级客户：
                            </td>
                            <td>
                                <asp:Label ID="ClientSelect" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                       <%-- <tr>
                            <td align="right" class="tdbgleft">
                                区域：
                            </td>
                            <td>
                                <asp:Label ID="DropArea" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" class="tdbgleft">
                                行业：
                            </td>
                            <td>
                                <asp:Label ID="DropClientField" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                价值评估：
                            </td>
                            <td>
                                <asp:Label ID="DropValueLevel" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" class="tdbgleft">
                                信用等级：
                            </td>
                            <td>
                                <asp:Label ID="DropCreditLevel" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                重要程度：
                            </td>
                            <td>
                                <asp:Label ID="DropImportance" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" class="tdbgleft">
                                关系等级：
                            </td>
                            <td>
                                <asp:Label ID="DropConnectionLevel" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                客户来源：
                            </td>
                            <td>
                                <asp:Label ID="DropSourceType" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" class="tdbgleft">
                                阶段：
                            </td>
                            <td>
                                <asp:Label ID="DropPhaseType" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                客户组别：
                            </td>
                            <td>
                                <asp:Label ID="DropGroupID" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" class="tdbgleft">
                            </td>
                            <td>
                                <table id="Table2" border="0">
                                    <tr>
                                    </tr>
                                </table>
                            </td>
                            </tr>--%>
                        <%--<asp:Literal runat="server" ID="htmlStr"></asp:Literal>--%>
                        <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
                    </table>
    <table id="Tabs1" class="table table-striped table-bordered table-hover" style="display: none">
        <tbody id="infoE">
            <tr>
                <td style="width: 15%" rowspan="2" align="right" class="tdbgleft">
                    通讯地址：
                </td>
                <td colspan="3" style="height: 23px">
                    <div id="ctl00_CphContent_Region_UpnlRegion">
                        <table  class="table table-striped table-bordered table-hover">
                            <tr>
                                <td style="width: 100px" align="right" class="tdbgleft">
                                    国家/地区：&nbsp;&nbsp;
                                </td>
                                <td colspan="2" align="left">
                                    <asp:Label ID="country" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px" align="right" class="tdbgleft">
                                    省/市/自治区：
                                </td>
                                <td align="left">
                                    <asp:Label ID="province" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="tdbgleft">
                                    市/县/区/旗：&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label ID="city" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
                            
            <tr>
                <td colspan="3">
                    <table border="0"  class="table table-striped table-bordered table-hover">
                        <tr class="tdbg">
                            <td style="width: 15%"  class="tdbgleft">
                                联系地址：
                            </td>
                            <td>
                                <asp:Label ID="TxtAddress" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  class="tdbgleft">
                                邮政编码：
                            </td>
                            <td>
                                <asp:Label ID="TxtZipCodeW" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="t1">
                <td style="width: 15%" align="right" class="tdbgleft">
                    联系电话：
                </td>
                <td>
                    <asp:Label ID="TxtPhone" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 15%" align="right" class="tdbgleft">
                    传真号码：
                </td>
                <td>
                    <asp:Label ID="TxtFax1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="s1" style=" display:none;">
                <td style="width: 15%" align="right" class="tdbgleft">
                    办公电话：
                </td>
                <td>
                    <asp:Label ID="TxtOfficePhone" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 15%" class="tdbgleft">
                    住宅电话：
                </td>
                <td>
                    <asp:Label ID="TxtHomePhone" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="s2"  style=" display:none;">
                <td style="width: 15%" align="right" class="tdbgleft">
                    移动电话：
                </td>
                <td>
                    <asp:Label ID="TxtMobile" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 15%" class="tdbgleft">
                    传真号码：
                </td>
                <td>
                    <asp:Label ID="TxtFax" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="s3" style=" display:none;">
                <td style="width: 15%" align="right" class="tdbgleft">
                    小灵通：
                </td>
                <td>
                    <asp:Label ID="TxtPHS" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr id="s4" style=" display:none;">
                <td style="width: 15%" align="right" class="tdbgleft">
                    个人主页：
                </td>
                <td>
                    <asp:Label ID="TxtHomepage" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbg" style=" width:30%" >
                    Email地址：
                </td>
                <td>
                    <asp:Label ID="TxtEmail" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="s5" style=" display:none;">
                <td style="width: 15%" align="right" class="tdbgleft">
                    QQ号码：
                </td>
                <td>
                    <asp:Label ID="TxtQQ" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft"  style=" width:30%">
                    MSN帐号：
                </td>
                <td>
                    <asp:Label ID="TxtMSN" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="s6" style=" display:none;">
                <td style="width: 15%" align="right" class="tdbgleft">
                    ICQ号码：
                </td>
                <td>
                    <asp:Label ID="TxtICQ" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 30%" class="tdbgleft">
                    雅虎通帐号：
                </td>
                <td>
                    <asp:Label ID="TxtYahoo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="s7" style=" display:none;">
                <td style="width: 15%" align="right" class="tdbgleft">
                    UC帐号：
                </td>
                <td>
                    <asp:Label ID="TxtUC" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 30%" class="tdbgleft">
                    Aim帐号：
                </td>
                <td>
                    <asp:Label ID="TxtAim" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
    <table id="Tabs2" class="table table-striped table-bordered table-hover" style="display: none">
        <tr>
            <td style="width: 15%" align="right" class="tdbgleft">
                开户银行：
            </td>
            <td style="width: 38%">
                <asp:Label ID="TxtBankOfDeposit" runat="server" Text=""></asp:Label>
            </td>
            <td style="width: 15%" align="right" class="tdbgleft">
                银行帐号：
            </td>
            <td style="width: 38%">
                <asp:Label ID="TxtBankAccount" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" align="right">
                税号：
            </td>
            <td>
                <asp:Label ID="TxtTaxNum" runat="server" Text=""></asp:Label>
            </td>
            <td class="tdbgleft" align="right">
                网址：
            </td>
            <td>
                <asp:Label ID="TxtHomepage1" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" align="right">
                行业地位：
            </td>
            <td>
                <asp:Label ID="DropStatusInField" runat="server" Text=""></asp:Label>
            </td>
            <td class="tdbgleft" align="right">
                公司规模：
            </td>
            <td>
                <asp:Label ID="DropCompanySize" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" align="right">
                业务范围：
            </td>
            <td>
                <asp:Label ID="TxtBusinessScope" runat="server" Text=""></asp:Label>
            </td>
            <td class="tdbgleft" align="right">
                年销售额：
            </td>
            <td>
                <asp:Label ID="TxtAnnualSales" runat="server" Text=""></asp:Label>万元
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" align="right">
                经营状态：
            </td>
            <td>
                <asp:Label ID="DropManagementForms" runat="server" Text=""></asp:Label>
            </td>
            <td class="tdbgleft" align="right">
                注册资本：
            </td>
            <td>
                <asp:Label ID="TxtRegisteredCapital" runat="server" Text=""></asp:Label>万元
            </td>
        </tr>
    </table>
    <table id="Tabs3" class="table table-striped table-bordered table-hover" style="display: none">
        <tr>
            <td style="width: 15%" align="right" class="tdbgleft">
                单位名称：
            </td>
            <td style="width: 38%">
                <asp:Label ID="TxtCompany" runat="server" Text=""></asp:Label>
            </td>
            <td style="width: 15%" align="right" class="tdbgleft">
                所属部门：
            </td>
            <td style="width: 38%">
                <asp:Label ID="TxtDepartment" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" align="right">
                职位：
            </td>
            <td>
                <asp:Label ID="TxtPosition" runat="server" Text=""></asp:Label>
            </td>
            <td class="tdbgleft" align="right">
                负责业务：
            </td>
            <td>
                <asp:Label ID="TxtOperation" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                称谓：
            </td>
            <td>
                <asp:Label ID="TxtTitle" runat="server" Text=""></asp:Label>
            </td>
            <td class="tdbgleft" align="right">
                单位地址：
            </td>
            <td>
                <asp:Label ID="TxtCompanyAddress" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <table id="Tabs4" class="table table-striped table-bordered table-hover" style="display: none">
        <tr>
            <td style="width: 15%" align="right" class="tdbgleft">
                出生日期：
            </td>
            <td style="width: 38%">
                <asp:Label ID="DpkBirthday" runat="server" Text=""></asp:Label>
            </td>
            <td style="width: 15%" align="right" class="tdbgleft">
                证件号码：
            </td>
            <td style="width: 38%">
                <asp:Label ID="TxtIDCard" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" align="right">
                籍贯：
            </td>
            <td>
                <asp:Label ID="TxtNativePlace" runat="server" Text=""></asp:Label>
            </td>
            <td class="tdbgleft" align="right">
                民族：
            </td>
            <td>
                <asp:Label ID="TxtNation" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                性别：
            </td>
            <td>
                <asp:Label ID="RadlSex" runat="server" Text=""></asp:Label>
            </td>
            <td class="tdbgleft" align="right">
                婚姻状况：
            </td>
            <td>
                <asp:Label ID="RadlMarriage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" align="right">
                学历：
            </td>
            <td>
                <asp:Label ID="DropEducation" runat="server" Text=""></asp:Label>
            </td>
            <td class="tdbgleft" align="right">
                毕业学校：
            </td>
            <td>
                <asp:Label ID="TxtGraduateFrom" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" align="right">
                生活爱好：
            </td>
            <td>
                <asp:Label ID="TxtInterestsOfLife" runat="server" Text=""></asp:Label>
            </td>
            <td class="tdbgleft" align="right">
                文化爱好：
            </td>
            <td>
                <asp:Label ID="TxtInterestsOfCulture" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" align="right">
                娱乐休闲爱好：
            </td>
            <td>
                <asp:Label ID="TxtInterestsOfAmusement" runat="server" Text=""></asp:Label>
            </td>
            <td class="tdbgleft" align="right">
                体育爱好：
            </td>
            <td>
                <asp:Label ID="TxtInterestsOfSport" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" align="right">
                其他爱好：
            </td>
            <td>
                <asp:Label ID="TxtInterestsOfOther" runat="server" Text=""></asp:Label>
            </td>
            <td class="tdbgleft" align="right">
                月收入：
            </td>
            <td>
                <asp:Label ID="DropIncome" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" align="right">
                家庭情况：
            </td>
            <td colspan="3">
                <asp:Label ID="TxtFamily" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <table id="Tabs5" class="table table-striped table-bordered table-hover" style="display: none">
        <tr class="tdbg">
            <td style="width: 15%" align="right" class="tdbgleft">
                备注：
            </td>
            <td colspan="3">
                <asp:Label ID="TxtRemark" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <table id="Tabs6"  class="table table-striped table-bordered table-hover" style="display: none">
        <tr class="tdbg">
            <td colspan="4">
                <div id="ctl00_CphContent_UpnlChildProducts">
                    <table cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;">
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table id="Tabs7"  class="table table-striped table-bordered table-hover" style="display: none">
        <tr class="tdbg">
            <td colspan="4">
                <div id="ctl00_CphContent_UpnlCompany">
                    <table cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;">
                    </table>
                </div>
            </td>
        </tr>
    </table>
        <asp:Button ID="Button1" runat="server" Text="修改" class="btn btn-primary" onclick="Button1_Click" />
        <asp:Button ID="ctl00_CphContent_BtnSave0" runat="server" Text="取消" class="btn btn-primary" onclick="ctl00_CphContent_BtnSave0_Click" CausesValidation="false" />
  <div id="FPGrid" style="font-size:12px;">
            <ZL:ExGridView runat="server" ID="EGV" IsHoldState="false" DataSourceID="CRMData" AutoGenerateColumns="false" 
                AllowPaging="True"  class="table table-striped table-bordered table-hover" EnableTheming="False" EmptyDataText="没有任何数据！">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="userinfo" HeaderText="内容" HtmlEncode="false"/>
                    <asp:BoundField DataField="BeginTime" HeaderText="跟进时间" />
                    <asp:BoundField DataField="fpman" HeaderText="跟进人" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit2" CommandArgument="<%# Container.DisplayIndex %>" Enabled="false" CssClass="option_style"><i class="fa fa-pencil" title="修改"></i>修改</asp:LinkButton> 
                            <a href="javascript:if(confirm('你确定要删除吗!')){ postToCS('del','<%#Eval("ID") %>')}" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="Save" runat="server" CommandName="Save" CommandArgument='<%# Container.DisplayIndex+":"+Eval("ID") %>'>更新</asp:LinkButton> 
                            <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel" CommandArgument='<%# Container.DisplayIndex %>'>取消</asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            <PagerStyle CssClass="tdbg" HorizontalAlign="Center" />
            <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
            <asp:ObjectDataSource runat="server" ID="CRMData" OldValuesParameterFormatString="original_{0}" SelectMethod="GetFPList" TypeName="GetDSData">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="id" QueryStringField="id" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function isPerson(ele) {
            document.getElementById("routine").style.display = "block";
            if (ele == "Person_Add") {
                document.getElementById("TabTitle3").style.display = "block";
                document.getElementById("TabTitle4").style.display = "block";
                document.getElementById("TabTitle7").style.display = "none";
                document.getElementById("TabTitle2").style.display = "none";
                document.getElementById("t1").style.display = "none";
                document.getElementById("s1").style.display = "block";
                document.getElementById("s2").style.display = "block";
                document.getElementById("s3").style.display = "block";
                document.getElementById("s4").style.display = "block";
                document.getElementById("s5").style.display = "block";
                document.getElementById("s6").style.display = "block";
                document.getElementById("s7").style.display = "block";
            }
            else if (ele == "Enterprise_Add") {
                document.getElementById("TabTitle3").style.display = "none";
                document.getElementById("TabTitle4").style.display = "none";
                document.getElementById("TabTitle7").style.display = "block";
                document.getElementById("TabTitle2").style.display = "block";
                document.getElementById("t1").style.display = "block";
                document.getElementById("s1").style.display = "none";
                document.getElementById("s2").style.display = "none";
                document.getElementById("s3").style.display = "none";
                document.getElementById("s4").style.display = "none";
                document.getElementById("s5").style.display = "none";
                document.getElementById("s6").style.display = "none";
                document.getElementById("s7").style.display = "none";
            }
        }

        function displl(di) {
            if (di != "info") {
                document.getElementById("info").style.display = "none";
                document.getElementById(di).style.display = "block";
                var field = '<%=Request.QueryString["FieldName"].ToString() %>';
        if ((field == "Person_Add") || (field == "Enterprise_Add")) {
            isPerson(field);
        }
        else {
            document.getElementById("routine").style.display = "none";
        }
    } else {
        document.getElementById("routine").style.display = "none";
        document.getElementById(di).style.display = "block";
    }
}

function ShowTabs(ID) {
    for (i = 0; i < 8; i++) {
        if (i == ID) {
            document.getElementById("Tabs" + i).style.display = "";
        }
        else {
            document.getElementById("Tabs" + i).style.display = "none";
        }
    }
}

function Fsex(a) {
    document.getElementById("sex").value = a;
}

function FMarriage(b) {
    document.getElementById("Marriage").value = b;
}

function getinfo(id) {
    location.href = "../AddOn/ProjectsDetail.aspx?ProjectID=" + id + "";
}
</script>
</asp:Content>