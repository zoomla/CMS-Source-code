<%@ Page Language="C#" EnableViewStateMac="false" AutoEventWireup="true" CodeFile="ChartManage.aspx.cs" Inherits="manage_Plus_ChartManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>图表管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs">
        <li class="active" id="tab0"><a href="#tab0" data-toggle="tab" onclick="location='ChartManage.aspx';">所有图表</a></li>
        <li id="tab1"><a href="#tab1" data-toggle="tab" onclick="location='ChartManage.aspx?type=1';">饼状图</a></li>
        <li id="tab2"><a href="#tab2" data-toggle="tab" onclick="location='ChartManage.aspx?type=2';">线状图</a></li>
        <li id="tab3"><a href="#tab3" data-toggle="tab" onclick="location='ChartManage.aspx?type=3';">柱状图</a></li>
    </ul>
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ChartID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Egv_RowCommand" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无相关数据！">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ChartID">
                <ItemStyle Width="5%" CssClass="Charid" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="图表标题(点击预览)">
                <HeaderStyle Width="20%" />
                <ItemTemplate>
                    <a class="preview" onclick="Move(<%#Eval("ChartID") %>)" style="cursor: pointer"><%# Eval("ChartTitle")%></a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="图表类型">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <%# Eval("ChartType")%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="图表单位">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# Eval("ChartUnit")%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="图表宽(px)">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <%# Eval("ChartWidth")%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="图表高(px)">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <%# Eval("ChartHeight")%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <HeaderStyle />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ChartID") %>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Del" OnClientClick="return confirm('确定删除此图表？');" CommandArgument='<%# Eval("ChartID") %>' CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton8" runat="server" CommandName="ifframe" CommandArgument='<%# Eval("ChartID") %>' CssClass="option_style"><i class="fa fa-font" title="获取代码"></i>获取图表代码</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript" src="/JS/Common.js"></script>
    <script type="text/javascript">
        $().ready(function () {
            var type = getParam("type");
            if (type) {
                $("#tab0").removeClass("active");
                $("#tab" + type).addClass("active");
            }
        });
        function Move(cla) {

            document.getElementById("ShowBiao").style.display = "";
            document.getElementById("ShowBiao").style.top = event.clientY;
            document.getElementById("ShowBiao").style.left = event.clientX;
            var srcva = "";
            srcva = "/Plugins/Chart/Show.aspx?Did=" + cla + "&height=300&width=400";
            document.getElementById("ShowBiao").src = srcva;

            document.getElementById("ShowBiao").style.border = "5px solid #cbe6fc";
        }
        function Mout() {
            document.getElementById("ShowBiao").style.display = "none";
        }
        function GsLlistShow() {
            document.getElementById("GsLi").style.display = "";
        }
        function GsLlistHide() {
            document.getElementById("GsLi").style.display = "none";
        }
    </script>
</asp:Content>
