<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BCMannger.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Sched.BCMannger" MasterPageFile="~/User/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>班次管理</title>
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script type="text/javascript">
    $().ready(function () {
        //$(":button").addClass("btn btn-primary");
        //$(":submit").addClass("btn btn-primary");
        //$("#EGV tr:last >td>:text").css("line-height", "normal");
        //$("#EGV tr:first >th").css("text-align", "center");
        $("#<%=EGV.ClientID%>  tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
    $("#chkAll").click(function () { selectAllByName(this, "idChk"); });
});
$().ready(function () {
    $("tr:gt(1):not(:last)").addClass("tdbg");
    $("tr:gt(1):not(:last)").mouseover(function () { $(this).removeClass("tdbg").addClass("tdbgmouseover") }).mouseout(function () { $(this).removeClass("tdbgmouseover").addClass("tdbg") });
    //$("tr:gt(1):not(:last)").dblclick(function () { v = $(this).find("[name='idChk']").val(); location = "MessageRead.aspx?read=1&id=" + v; });//绑定双击事件
});
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="draftnav">
        <a href="PBManage.aspx">排班管理</a>/<a href="BCManage.aspx">班次管理</a><span><a href="BCAdd.aspx" style="color:#f00">[添加班次]</a></span>
    </div>
    <div id="main" style="margin:10px;">
        <asp:TextBox runat="server" ID="searchText" placeholder="请输入会员名或真实姓名" CssClass="form-control" />
        <asp:Button runat="server" CssClass="btn btn-primary" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click"/>
        <div style="margin-top:5px;">
            <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="EGV_RowCommand"  EnableTheming="False"  GridLines="None" CellPadding="2" CellSpacing="1"  Width="98%" CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" OnPageIndexChanging="EGV_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <input type="checkbox" name="idChk" value="<%#Eval("ID") %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="ID" DataField="ID" />
                    <asp:BoundField HeaderText="班次名称" DataField="BCName" />
                    <asp:BoundField HeaderText="开始时间" DataField="StartTime" />
                    <asp:BoundField HeaderText="结束时间" DataField="EndTime" />
                    <asp:BoundField HeaderText="添加时间" DataField="AddTime" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="BCAdd.aspx?ID=<%#Eval("ID") %>">修改</a>
                            <asp:LinkButton ID="LinkButton1" CommandName="Del2" CommandArgument='<%#Eval("ID") %>' runat="server">删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center"/>
                <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
        </div>
    </div>
</asp:Content>
