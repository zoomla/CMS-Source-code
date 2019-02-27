<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteDataCenter.aspx.cs" Inherits="ZoomLaCMS.Manage.Site.SiteDataCenter" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>智能采集</title>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="n_site_bread">
<ul class="breadcrumb">
<li><a href="<%= CustomerPageAction.customPath2 %>/Main.aspx">工作台</a></li>
<li><a href="Default.aspx">站群中心</a></li>
<li class="active">智能采集</li>
</ul> 
</div> 
<asp:ScriptManager runat="server"></asp:ScriptManager>
<%--<asp:UpdatePanel runat="server"><ContentTemplate>--%>
<div id="mainDiv">
<!--左边栏-->
<div id="leftDiv"  class="col-lg-3 col-md-3 col-sm-3 col-xs-12 padding5 leftDiv">
<asp:Panel ID="Panel1" runat="server" Height="650px"  Width="100%">
<table class="table table-striped table-bordered table-hover">
    <tr>
        <td style="width:40%;"><input type="checkbox" id="siteChkAll" title="选择全部" style="float:left;" onclick="selectAll(this, 'siteChk')"/>站点名</td><td style="width:50%;">网址</td>
    </tr>
  <asp:Repeater runat="server" ID="siteRepeater">
      <ItemTemplate>
      <tr>
         <td><input type="checkbox" name="siteChk" value='<%# GetDomain(Eval("Domain").ToString(),Eval("SitePort").ToString()) %>' style="float:left;"/>
            <a href="javascript:;" onclick="window.open('<%# GetDomain(Eval("Domain").ToString(),Eval("SitePort").ToString()) %>');" title="点击浏览"> <%#Eval("SiteName") %></a></td><td> <%#Eval("Domain") %></td>
      </tr>
      </ItemTemplate>
  </asp:Repeater>
</table>
<div class="input-group">
    <asp:TextBox runat="server" ID="urlText" CssClass="form-control" />
    <span class="input-group-btn">
    <asp:Button runat="server" ID="Button1" Text="开始采集" OnClick="beginGet_Click" OnClientClick="disSiteInfo();" CssClass="btn btn-primary"  />
    </span>
</div> 
<asp:Literal runat="server" ID="remind" />
</asp:Panel>
</div>
<div id="rightDiv" class="col-lg-9 col-md-9 col-sm-9 col-xs-12 padding5 rightDiv">
<!--主体头部-->
<div class="rightDiv-Head" style="height:40px;text-align:center;" >
       <%-- <asp:TextBox runat="server" ID="siteUrl" Width="500px" TextMode="MultiLine"></asp:TextBox>
        <br />--%>
       
        <div>
        <span style="float:left;" id="siteInfoSpan">采集站点：</span>
        批量修改 <asp:DropDownList runat="server" ID="mainDp1" CssClass="form-control text_md"></asp:DropDownList>
        为<asp:DropDownList runat="server" ID="mainDp2"  CssClass="form-control text_md"></asp:DropDownList>
        <input type="button" value="确定" onclick="changeNode()" class="btn btn-sm btn-primary"/>
        <asp:Button runat="server" ID="btn1" Text="采集入库"  OnClick="btn1_Click" CssClass="btn btn-sm btn-primary" OnClientClick="return checkData()"/>
        <asp:Button runat="server" ID="logBtn" Text="查看日志" OnClick="logBtn_Click" CssClass="btn btn-sm btn-primary" />
        <asp:Label runat="server" ID="count"></asp:Label>
        <script type="text/javascript">
            function changeNode() {
                o = $("#<%=mainDp1.ClientID%>").attr("value");
                n = $("#<%=mainDp2.ClientID%>").attr("value");
                $("select option:selected[value='" + o + "']").parent().attr("value", n);
            }
            function checkData()
            {
                $op=$("#<%=EGV.ClientID%> tr select option:selected[value='0']");
                if ($op.length>0)
                {
                    alert("有"+$op.length+"篇文章未分配节点,请先分配节点.");
                    return false;
                }
                return true;
            }
            $().ready(function () {
                var a = function (id) {
                    var s = "手输地址,格式:www.z01.com:端口";
                    $("#" + id).val(s).css('color', '#666')
                    .focus(function () { if (this.value == s) { this.value = ''; this.style.color = 'black'; } })
                    .blur(function () { if (this.value == '') { this.value = s; this.style.color = '#666'; } });
                }
                a("<%=urlText.ClientID%>");
            });
        </script>   
        </div> 
</div>
<!--主体内容-->
<div class="site_main" style="padding-top:2px;">
 <asp:Panel runat="server" Height="800px" ScrollBars="Vertical" Width="100%">
 <ZL:ExGridView ID="EGV" runat="server" AllowPaging="False" RowStyle-CssClass="tdbg" AutoGenerateColumns="False" 
        MouseOverCssClass="tdbgmouseover" CssClass="table table-striped table-bordered table-hover" CellPadding="2" CellSpacing="1" ForeColor="Black" 
        GridLines="None" EnableTheming="False" EmptyDataText="没有任何数据！" OnRowDataBound="EGV_RowDataBound" AllowSorting="True" CheckBoxFieldHeaderWidth="3%" EnableModelValidation="True" IsHoldState="True" SerialText="">
         <Columns>
             <asp:TemplateField HeaderText="标题">
                 <ItemTemplate>
                     <input type="checkbox" name="chk" checked="checked" value="<%#Eval("GeneralID") %>" style="float:left;"/>
                     <%#Eval("Title") %>
                 </ItemTemplate>
                 <ItemStyle HorizontalAlign="Left" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="来自" ItemStyle-HorizontalAlign="Left">
                 <ItemTemplate>
                    <%#Eval("SiteSource") %>的节点：<%#Eval("NodeName1") %>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="需要分配入的节点(自动匹配同名节点)">
                 <ItemTemplate>
                     <asp:DropDownList runat="server" ID="nodeList" CssClass="form-control text_md"></asp:DropDownList>
                 </ItemTemplate>
             </asp:TemplateField>
             <%--<asp:TemplateField HeaderText="操作">
                 <ItemTemplate>
                     <a href="javascript:;" title="浏览网站">浏览|</a>
                     <asp:LinkButton ID="LinkButton1"  runat="server" CommandName="Edit2" CommandArgument='<%# Container.DisplayIndex %>' >修改|</asp:LinkButton>
                     <a href="javascript:;">删除|</a>
                 </ItemTemplate>
                 <EditItemTemplate>
                     <asp:LinkButton ID="Save" runat="server" CommandName="Save" CommandArgument='<%# Container.DisplayIndex %>'>更新|</asp:LinkButton>
                     <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel" CommandArgument='<%# Container.DisplayIndex %>'>取消</asp:LinkButton>
                 </EditItemTemplate>
             </asp:TemplateField>       --%>
         </Columns>
            <PagerStyle CssClass="tdbg" HorizontalAlign="Center" />
            <RowStyle Height="24px" HorizontalAlign="Center" />
        </ZL:ExGridView>
<%-- <asp:ObjectDataSource runat="server" ID="contentList" OldValuesParameterFormatString="original_{0}" SelectMethod="GetContentList" TypeName="GetDSData">
     <SelectParameters>
         <asp:FormParameter DefaultValue="" FormField="urlArr" Name="url" Type="String" />
     </SelectParameters>
     </asp:ObjectDataSource>--%>
 <br /> 
     <asp:UpdatePanel runat="server">
     <ContentTemplate>
 <ZL:ExGridView runat="server" ID="EGV2" DataSourceID="historyLog" AllowPaging="True" RowStyle-CssClass="tdbg" AutoGenerateColumns="false"
        MouseOverCssClass="tdbgmouseover" CellPadding="2" CellSpacing="1" ForeColor="Black" CssClass="border" Width="100%"
        GridLines="None" EnableTheming="False" EmptyDataText="没有任何数据！" OnRowDataBound="EGV_RowDataBound" AllowSorting="true" Visible="false"> 
     <Columns>
          <asp:BoundField HeaderText="标题" DataField="title"/>
          <asp:BoundField HeaderText="来源" DataField="SiteSource"/>
          <asp:BoundField HeaderText="添加入的节点" DataField="NodeName1"/>
          <asp:BoundField HeaderText="日期" DataField="Date"/>
     </Columns>
      <PagerStyle CssClass="tdbg" HorizontalAlign="Center" />
      <RowStyle Height="24px" HorizontalAlign="Center" />
 </ZL:ExGridView>
</ContentTemplate>
</asp:UpdatePanel>
 </asp:Panel>
 <asp:ObjectDataSource runat="server" ID="historyLog" OldValuesParameterFormatString="original_{0}" SelectMethod="GetHistoryLog" TypeName="GetDSData"></asp:ObjectDataSource>
</div>
</div>
</div>
<%--</ContentTemplate></asp:UpdatePanel>--%>
<div style="clear:both;"></div>
<asp:HiddenField runat="server" ID="siteDB" />
<script type="text/javascript">
        function selectAll(obj, name) {
            var allInput = document.getElementsByName(name);
            var loopTime = allInput.length;
            for (i = 0; i < loopTime; i++) {
                if (allInput[i].type == "checkbox") {
                    allInput[i].checked = obj.checked;
                }
            }
        }
         //显示正在采集的站点
        function disSiteInfo()
        {
            $("#<%=siteDB.ClientID%>").val("");
            getSiteName();
            var siteArr = $("input[name=siteChk]:checked");
            for (var i = 0; i < siteArr.length; i++)
            {
                s = "<a href='" + $(siteArr[i]).val() + "' style='color:green;' title='" + $(siteArr[i]).val() + "'>[" + $(siteArr[i]).parent().text().trim() + "]</a>";
                $("#siteInfoSpan").append(s);
                $("#<%=siteDB.ClientID%>").val($("#<%=siteDB.ClientID%>").val()+s);
            }
            
        }
        function getSiteName()
        {
            //以网址,的方式发送后台处理成对应的JSON后返回
            var str = $("#" + "<%=urlText.ClientID%>").val();
           //return postToCS("getSiteName", str);
        }

    function postToCS(a,v)
    {
        //异步,所以需要在success中处理
        $.ajax({
            type: "Post",
            url: "SiteDataCenter.aspx",
            data: { action: a, value: v },
            dataType: "json",
            async: false,
            success: function (data)
            {
                if (data) {
                    for (var i = 0; i < data.length; i++)
                    {
                        s = "<a href='" + data[i].url + "' style='color:green;' title='" + data[i].url + "'>[" + data[i].siteName + "]</a>";
                        $("#siteInfoSpan").append(s);
                    }
                }
            },
            error: function () {alert("Failed")},
        });
        return true;
    }

    $().ready(function () { $("#siteInfoSpan").append($("#<%=siteDB.ClientID%>").val()); });
    </script>
    
</asp:Content>