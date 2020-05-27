<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Manage.Site.Default"   MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<title>站点列表</title>   
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        
    <div runat="server" id="serverDiv" style="position:absolute;margin-top:2px;" visible="false"> 
        管理其它服务器：<asp:DropDownList runat="server" ID="serverList"  onchange="getToServer()" CssClass="form-control text_md"></asp:DropDownList>
    </div>
    <div class="top_opbar">
            <span runat="server" id="titleSpan" style="color: green; margin-left: 15px;">汇总信息：当前共有{0}个站点:{1}个运行中,{2}个已停止(其中有{3}个逐浪站点,{4}个运行中,{5}个已停止)
            </span><span>
                <asp:CheckBox runat="server" ID="noZoomla" Checked="true" Text="普通网站" AutoPostBack="true" />
                <asp:CheckBox runat="server" ID="zoomlaSite" Checked="true" Text="逐浪网站" AutoPostBack="true" />
                <asp:CheckBox runat="server" ID="started" Checked="true" Text="运行中" AutoPostBack="true" />
                <asp:CheckBox runat="server" ID="stopped" Checked="true" Text="已停止" AutoPostBack="true" />
                <asp:CheckBox runat="server" ID="expire" Checked="false" Text="已到期(仅显示)" AutoPostBack="true" />
            </span>
        </div>
    <ul class="nav nav-tabs">
        <li id="tabs0" onclick="ShowTab(false)"><a href="#tab5" data-toggle="tab">全部站点</a></li>
        <li id="tabs1" onclick="ShowTab(true)"><a href="#tab0" data-toggle="tab">逐浪站点</a></li>
    </ul>
    <script>
        $(function () {
            if (document.getElementById("noZoomla").checked) {
                $("#tabs0").addClass("active");
            }
            else { $("#tabs1").addClass("active"); }

        })
        function ShowTab(flag) {//Div切换
            //$(obj).addClass("choose").siblings().removeClass("choose").addClass("nochoose");
            $("#noZoomla")[0].checked = flag;
            $("#noZoomla").trigger("click");
        }
    </script>
    <div id="tab3">
        <ZL:ExGridView ID="EGV" runat="server" DataSourceID="WSData" AllowPaging="True" RowStyle-CssClass="tdbg" AutoGenerateColumns="False" 
                    MouseOverCssClass="tdbgmouseover" CssClass="table table-striped table-bordered table-hover text-center" CellPadding="2" CellSpacing="1"  Width="100%"  OnRowDataBound="EGV_RowDataBound"
                    GridLines="None" EnableTheming="False" EmptyDataText="没有任何数据！" OnRowCommand="EGV_RowCommand" AllowSorting="True" CheckBoxFieldHeaderWidth="3%" EnableModelValidation="True" IsHoldState="false" SerialText="">
         <Columns>
             <asp:TemplateField HeaderText="ID">
                 <ItemTemplate>
                     <input type="checkbox" name="chk" value="<%#Eval("SiteName") %>"  />
                     <%#Eval("SiteID") %>
                 </ItemTemplate>
                 <ItemStyle HorizontalAlign="Left" />
             </asp:TemplateField> 
             <asp:TemplateField HeaderText="网站名称"  ItemStyle-CssClass="site_ico"  SortExpression="SiteState">
                 <ItemTemplate>
                   <a href="javascript:;" title="运行状态" style="position:relative; right:5px;">
                       <%#(Eval("SiteState") as string) == "Started" ? "<i class='fa fa-play'></i>" 
                       : "<i class='fa fa-pause'></i>" %>                     
                   </a>
                   <a href="<%#GetFileUrl(Eval("SiteName")as string) %>" title="浏览文件" style="position:relative;">
                   <i class="fa fa-folder-open"></i> </a>
                   <a href="<%# GetSiteDetailUrl(Eval("SiteName")as string) %>" title="查看网站详情"><%# Eval("SiteName") %></a>
                     <a href="javascript:;" title="<%# "版本："+Eval("ZoomlaVersion") %>" style="position:relative; bottom:-2px; right:5px;margin-left:10px;">
                       <%#Eval("ZoomlaVersion").ToString().Length==0 ? "" 
                       : "<img src='/favicon.ico' style='cursor:default;width:14px;height:14px;'/>" %>                 
                   </a>
                 </ItemTemplate>
                 <ItemStyle HorizontalAlign="Left" />
                 <EditItemTemplate>
                     <asp:TextBox ID="EditSiteName" runat="server" Text='<%#Eval("SiteName") %>' Width="90%" style="text-align:center;"/>
                 </EditItemTemplate>
            <ItemStyle CssClass="site_ico"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="状态" SortExpression="SiteState">
                 <ItemTemplate>
                     <%#(Eval("SiteState") as string) == "Started" ? "<span style='color:green;'>运行中</span>" : "<span style='color:red;'>已停止</span>" %>
                 </ItemTemplate>
                  <ItemStyle HorizontalAlign="Left" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="到期时间">
                <ItemTemplate>
                 <a href="<%#GetFranUrl(Eval("SiteName")as string) %>" title="点击进入管理">
                 <%#GetDate(DataBinder.Eval(Container.DataItem, "EndDate", "{0:yyyy年M月d日}"))%>
                 </a>
                </ItemTemplate>
                  <ItemStyle HorizontalAlign="Left" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="端口" SortExpression="SitePort">
                 <ItemTemplate>
                       <a href="Default.aspx?siteName=<%# Server.UrlEncode(Eval("SiteName")as string) %>" title="点击浏览绑定信息"><%#Eval("SitePort") %></a>
                 </ItemTemplate>
                  <ItemStyle HorizontalAlign="Left" />
                 <EditItemTemplate>
                     <asp:TextBox runat="server" ID="EditPort" Text='<%#Eval("SitePort") %>' Width="30%" style="text-align:center;"/>
                 </EditItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="site_a">
                 <ItemStyle HorizontalAlign="Left" />
                 <ItemTemplate>
                     <a href="javascript:;" title="浏览网站" onclick="testF(this,'getDomain', '<%#Eval("SiteName") %>','<%#Eval("SitePort") %>');" class="option_style" title="浏览"><i class="fa fa-eye"></i></a>
                     <asp:LinkButton  runat="server" CommandName="Edit2" CommandArgument='<%# Container.DisplayIndex %>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                     <a href="javascript:if(confirm('你确定要删除吗!')){ postToCS('del','<%#Eval("SiteName") %>')}" class="option_style" title="删除"><i class="fa fa-trash"></i></a>
                     <%#ShowOPBtn() %>
                     <a href="javascript:postToCS('restart','<%#Eval("SiteName") %>')" title="重启" class="option_style"><i class="fa fa-refresh"></i>重启</a>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:LinkButton ID="Save" runat="server" CommandName="Save" CommandArgument='<%# Container.DisplayIndex+":"+Eval("SiteID") %>' CssClass="option_style"><i class="fa fa-refresh" title="更新"></i></asp:LinkButton>
                     <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel" CommandArgument='<%# Container.DisplayIndex %>' CssClass="option_style"><i class="fa fa-remove" title="取消"></i></asp:LinkButton>
                 </EditItemTemplate>
             </asp:TemplateField>       
         </Columns>
            <PagerStyle   HorizontalAlign="Center" />
            <RowStyle Height="24px" HorizontalAlign="Center" />
        </ZL:ExGridView><br />
        <asp:ObjectDataSource runat="server" ID="WSData" OldValuesParameterFormatString="original_{0}" SelectMethod="GetWSData" TypeName="GetDSData" >
            <SelectParameters>
                <asp:ControlParameter ControlID="noZoomla" Name="f1" PropertyName="Checked" Type="String" />
                <asp:ControlParameter ControlID="zoomlaSite" Name="f2" PropertyName="Checked" Type="String" />
                <asp:ControlParameter ControlID="started" Name="f3" PropertyName="Checked" Type="String" />
                <asp:ControlParameter ControlID="stopped" Name="f4" PropertyName="Checked" Type="String" />
                <asp:ControlParameter ControlID="expire" Name="f5" PropertyName="Checked" Type="String" />
            </SelectParameters>
            </asp:ObjectDataSource> 
        <input type="button" id="btn1" value="创建新站点" class="btn btn-primary"/>
        <asp:Button runat="server" ID="batStart" Text="批量启动" class="btn btn-primary" OnClick="batStart_Click"/>
       <%-- <asp:Button runat="server" ID="batStop"  Text="批量停止" class="site_button" OnClick="batStop_Click"/>--%>
        <div style="display: none;">
            <input type="button" id="btn2" value="批量创建网站" />
            <input type="button" id="btn3" value="批量删除" />
            <input type="button" id="btn4" value="批量重启" />
        </div>
    </div>
<%--    <div id="remoteDiv" style="display: none;">
        <iframe id="remoteFrame" style="width: 100%; height: 960px;" />
    </div>--%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function testFunc2() {
            $("#localDiv").hide();
            $("#remoteDiv").show();
        }
        $().ready(function () {
            obj = $("#<%=expire.ClientID%>");
                    $("#<%=serverDiv.ClientID%>").css("left", $(obj).offset().left + 120);
                });
                function getToServer() {
                    value = $("#<%=serverList.ClientID%> option:selected").val();
                    if (value == 0) {
                        $("#localDiv").show();
                        $("#remoteDiv").hide();
                    } else if (value == 'add') {
                        location.href = "ServerClusterConfig.aspx";
                    }
                    else {
                        SyncpostToCS("serverInfo", value);
                    }
                    function SyncpostToCS(a, v) {
                        $.ajax({
                            type: "Post",
                            url: "Default.aspx",
                            //dataType: "json",
                            data: { action: a, value: v },
                            async: true,
                            success: function (data) {
                                //result = data; 
                                $("#remoteFrame").attr("src", data);
                                $("#localDiv").hide();
                                $("#remoteDiv").show();
                            },
                            error: function (data) { alert("失败"); }
                        });
                    }
                }
    </script>
    <script>
                function testF(obj, a, name, b) {
                    postToCS2(a, name, b, obj);
                }
                function postToCS2(a, name, b, obj) {
                    $.ajax({
                        type: "Post",
                        url: "Default.aspx",
                        data: { action: a, siteName: name },
                        success: function (data) {
                            var result = "";
                            var arr = data.split(',');
                            if (data == "") {
                                alert("尚未绑定域名");
                                return false;
                                //result += "<li class='tdbg'>尚未绑定域名</li>";
                            }
                            //else if (arr.length == 1) { window.open("http://" + arr[0]); return; }
                            for (i = 0; i < arr.length && data != ""; i++) {
                                //  result+="<li class='tdbg'><a href='http://"+arr[i]+"'>"+arr[i]+"</a></li>";

                                result = arr[i]
                            }

                            window.open("http://" + result + ":" + b, "_blank");
                            // $("#hrefBox").html(result);

                            easyDialog.open({
                                container: 'hrefBox',
                                follow: obj,
                                followX: -60,
                                followY: 15
                            });
                        },
                        error: function (data) { alert(data); }
                    });
                }
        </script>
    <script>
        $(function () {
            $("#btn1,#rBtn1").click(function () {
                url = "CreateSite.aspx";
                if (getParam("remote") == "true") {
                    url += "?remote=true"
                }
                location = url;
            });//btn1 end;

            $("#btn2,#rBtn2").click(function () {
                $("#BCWSDiv").toggle();
                $("#CSWSDiv").hide();
            });//btn2 end;

            $("#<%=EGV.ClientID%>  tr>th:eq(0)").html("<input type=checkbox id='chkAll' style='margin-right:10px;'/>ID");//EGV顶部
                $("#<%=EGV.ClientID%>  tr>th").css("height", "30px").css("line-height", "30px");

                $("#chkAll").click(function () {//EGV 全选
                    selectAll(this, "chk");
                });
                //$("#chkAll2").click(function () {//EGV2 全选
                //    selectAll(this, "chk2");
                //});
                //$("#chkAll3").click(function () {
                //    selectAll(this, "chk3");
                //});
                //$("#chkAll4").click(function () {
                //    selectAll(this, "chk4");
                //});

                //bindInfo("例:www.baidu.com", "BindDomain");
                //bindInfo('例:/test/test', 'VPath');
                //bindInfo('例:C:\\test\\', 'PPath')
            });//ready End;

            function bindInfo(s, id) {
                $("#" + id).val(s).css('color', '#666')
                     .focus(function () { if (this.value == s) { this.value = ''; } })
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
        </script>
</asp:Content>