<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SitePool.aspx.cs" Inherits="SitePool" MasterPageFile="~/Manage/I/Default.master"  Title="应用程序池列表"%>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
<title>应用程序池</title>
<style>#site_nav .site02 a {background: url(../../App_Themes/AdminDefaultTheme/images/site/menu_cur.png) left no-repeat;}</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid mysite">
        <div class="row">
            <ul class="breadcrumb">
                <li><a href="<%= CustomerPageAction.customPath2 %>/Main.aspx">工作台</a></li>
                <li><a href="Default.aspx">站群中心</a></li>
                <li><a href="SitePool.aspx">应用程序池</a></li>
                <li><span runat="server" id="titleSpan" style="color: green; margin-left: 15px;">汇总信息：当前共有{0}个程序池:{1}个经典模式,{2}个集成模式
                </span></li>
            </ul>
        </div>
    </div>
    <div id="site_main">
    <div id="tab3">
         <%--   <asp:UpdatePanel runat="server" ID="AppPoolPanel">
                <ContentTemplate>--%>
                    <ZL:ExGridView ID="EGV4" runat="server" DataSourceID="AppPoolData" AllowPaging="True" RowStyle-CssClass="tdbg" AutoGenerateColumns="false"
                        MouseOverCssClass="tdbgmouseover" CssClass="table table-striped table-bordered table-hover" CellPadding="2" CellSpacing="1" ForeColor="Black" Width="100%"
                        GridLines="None" EnableTheming="False" EmptyDataText="没有任何数据！" OnRowCommand="EGV4_RowCommand" CheckBoxFieldHeaderWidth="3%"
                        IsHoldState="true" OnRowDataBound="EGV4_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <input type="checkbox" name="chk4" value="<%#Eval("Index") %>" style="margin-right: 10px;" />
                                    <%#Eval("Index") %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="程序池名称" ItemStyle-CssClass="site_ico">
                                <ItemTemplate>
                                    <a href="javascript:;" title="运行状态" style="position:relative; bottom:-2px; right:5px;">
                       <%#(Eval("State") as string) == "Started" ? "<i class='fa fa-play'></i>" 
                       : "<i class='fa fa-pause'></i>" %>                       
                                    </a>
                                    <%# Eval("AppPoolName") %>
                                </ItemTemplate>
       <%--                         <EditItemTemplate>
                                    <asp:TextBox ID="EditAppPoolName" runat="server" Text='<%#Eval("AppPoolName") %>' Width="90%" Style="text-align: center;" />
                                </EditItemTemplate>--%>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="状态">
                                <ItemTemplate>
                                    <%#(Eval("State") as string) == "Started" ? "<span style='color:green;'>运行中</span>" : "<span style='color:red;'>已停止</span>" %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net版本">
                                <ItemTemplate>
                                    <%#Eval("NetVersion") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <input type="hidden" runat="server" id="NetVersion" value='<%#Eval("NetVersion") %>' />
                                    <asp:DropDownList runat="server" ID="EditNetVersion">
                                        <asp:ListItem Value="v2.0">v2.0</asp:ListItem>
                                        <asp:ListItem Value="v4.0">v4.0</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="模式">
                                <ItemTemplate>
                                    <%#(Eval("Mode") as string) == "Classic" ? "<span style='color:green;'>经典模式</span>" : "<span style='color:blue;'>集成模式</span>" %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <input type="hidden" runat="server" id="Mode" value='<%#Eval("Mode") %>' />
                                    <asp:DropDownList runat="server" ID="EditMode">
                                        <asp:ListItem Value="Integrated">集成模式</asp:ListItem>
                                        <asp:ListItem Value="Classic">经典模式</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="网站数量">
                                <ItemTemplate>
                                    <%#Eval("AppNum") %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="site_a">
                                <ItemTemplate>
                                  <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit2" CommandArgument='<%# Container.DisplayIndex %>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                                   <%--  <a href="javascript:if(confirm('你确定要删除吗!')){ postToCS('del','<%#Eval("AppPoolName") %>')}">删除</a>
                                   <a href="javascript:if(confirm('你确定要删除吗!')){ postToCS('del','<%#Eval("AppPoolName") %>')}">停止</a>
                                    <a href="javascript:if(confirm('你确定要删除吗!')){ postToCS('del','<%#Eval("AppPoolName") %>')}">启动</a>--%>
                                    <asp:LinkButton runat="server" CommandName="stop" CommandArgument='<%#Eval("AppPoolName") %>' Visible='<%#(Eval("State","").Equals("Started"))%>' CssClass="option_style"><i class="fa fa-pause" title="停止"></i>停止</asp:LinkButton>
                                    <asp:LinkButton  runat="server" CommandName="start" CommandArgument='<%#Eval("AppPoolName") %>' Visible='<%#!(Eval("State","").Equals("Started"))%>' CssClass="option_style"><i class="fa fa-play" title="启动"></i>启动</asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="Save" runat="server" CommandName="Save" CommandArgument='<%# Container.DisplayIndex+":"+Eval("AppPoolName") %>'>更新</asp:LinkButton>
                                    <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel" CommandArgument='<%# Container.DisplayIndex %>'>取消</asp:LinkButton>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="tdbg" HorizontalAlign="Center" />
                        <RowStyle Height="24px" HorizontalAlign="Center" />
                    </ZL:ExGridView>
        </div>
        <asp:ObjectDataSource runat="server" ID="AppPoolData" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAppPool" TypeName="GetDSData"></asp:ObjectDataSource>
<%--           </ContentTemplate>
            </asp:UpdatePanel>--%>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#btn1,#rBtn1").click(function () {
                //$("#CSWSDiv").toggle();
                //$("#BCWSDiv").hide();
                open("CreateSite.aspx");
            });//btn1 end;

            $("#btn2,#rBtn2").click(function () {
                $("#BCWSDiv").toggle();
                $("#CSWSDiv").hide();
            });//btn2 end;

            //$("#EGV  tr>th:eq(0)").html("<input type=checkbox id='chkAll' style='float:left;'/>ID");
            //$("#EGV2 tr>th:eq(0)").html("<input type=checkbox id='chkAll2' style='float:left;'/>ID");
            //$("#EGV3 tr>th:eq(0)").html("<input type=checkbox id='chkAll3' style='float:left;'/>ID");
            $("#<%=EGV4.ClientID%> tr>th:eq(0)").html("<input type=checkbox id='chkAll4' style='margin-right:10px;'/>ID");

                //$("#chkAll").click(function () {//EGV 全选
                //    selectAll(this,"chk");
                //});
                //$("#chkAll2").click(function () {//EGV2 全选
                //    selectAll(this, "chk2");
                //});
                //$("#chkAll3").click(function () {
                //    selectAll(this, "chk3");
                //});
                $("#chkAll4").click(function () {
                    selectAll(this, "chk4");
                });

                //bindInfo("例:www.baidu.com", "BindDomain");
                //bindInfo('例:/test/test', 'VPath');
                //bindInfo('例:C:\\test\\', 'PPath')

                $("table tr").mousemove(function () { this.className = 'tdbgmouseover'; }).mouseout(function () { this.className = 'tdbg'; });
            });//ready End;

            function bindInfo(s, id) {
                $("#" + id).val(s).css('color', '#666')
                     .focus(function () { if (this.value == s) { this.value = ''; this.style.color = 'black'; } })
                     .blur(function () { if (this.value == '') { this.value = s; this.style.color = '#666'; } });
            }
            function postToCS(a, name) {
                $.ajax({
                    type: "Post",
                    url: "Default.aspx",
                    data: { action: a, siteName: name },
                    success: function (data) { if (a != "del3") { location = location; } else { alert(data); }; },
                    error: function (data) { alert(data); }
                });
            }

            function selectAll(obj, name) {
                var allInput = document.getElementsByName(name);
                var loopTime = allInput.length;
                for (i = 0; i < loopTime; i++) {
                    if (allInput[i].type == "checkbox") {
                        allInput[i].checked = obj.checked;
                    }
                }
            }
            function openSite(domain, port) {
                if (domain == "") domain = "localhost";
                open("http://" + domain + ":" + port);
            }
            function ShowTabs(obj, id) {//Div切换
                $(obj).addClass("titlemouseover").siblings().removeClass("titlemouseover").addClass("tabtitle");
                $("#tab" + id).show("fast").siblings().hide();
            }
    </script>

</asp:Content>

