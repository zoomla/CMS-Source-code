<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocManage.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Office.DocManage"  MasterPageFile="~/Common/Master/UserEmpty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>文档管理</title>
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script type="text/javascript">
    function GetTo() {
        location = '../AddContent.aspx?NodeID=<%=Server.HtmlEncode(Request.QueryString["NodeID"])%>';
    }
    $().ready(function () {
        $("#<%=EGV.ClientID%>  tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>ID");//EGV顶部
    $("#chkAll").click(function () { selectAllByName(this, "idChk"); });
    $("tr:gt(0):not(:last)").mouseover(function () { $(this).removeClass("tdbg").addClass("tdbgmouseover") }).mouseout(function () { $(this).removeClass("tdbgmouseover").addClass("tdbg") });
});
function sureFunc() {
    return confirm('你确定要其放入回收站吗!!!');
}
</script>
<style type="text/css">
    #EGV tr th {text-align: center;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
      <div class="draftnav"><a href="/Office/Main.aspx">行政公文</a>/<a href="Default.aspx">文档管理</a>
          <asp:LinkButton runat="server" ID="addBtn" OnClientClick="GetTo();return false;">[添加新公文]</asp:LinkButton></div>
            <div style="margin-bottom:10px;">
            <ul class="nav nav-tabs">
                <li><a href="Default.aspx">公文</a></li>
                <li class="active"><a href="DocManage.aspx">文档</a></li>
                <li><a href="DraftBox.aspx">草稿箱</a></li>
                <li><a href="Garbagebox.aspx">回收站</a></li>
            </ul>
        </div>
<div id="site_main" style="margin:5px;">
    <div class="input-group" style=" width:500px;float:left;margin-bottom:10px;">
        <asp:TextBox runat="server" ID="searchText" placeholder="请输入需要查询的信息" CssClass="form-control"/>
        <span class="input-group-btn">
            <asp:Button runat="server" CssClass="btn btn-primary" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click"/>
            <asp:Button runat="server" ID="batDelBtn" CssClass="btn btn-primary" Text="批量移入回收站" OnClick="batDelBtn_Click" OnClientClick="return confirm('确定要移入回收站吗?');"/>
        </span>
    </div>
    <div class="tab3">
        <ZL:ExGridView runat="server" ID="EGV" DataKeyNames="GeneralID" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"  EnableTheming="False"  GridLines="None" CellPadding="2" CellSpacing="1"  Width="100%" CssClass="table table-striped table-bordered table-hover" EmptyDataText="没有任何数据!!" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound" >
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <input type="checkbox" name="idChk" value="<%#Eval("GeneralID") %>"/>
                        <%#Eval("GeneralID") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="所属节点">
                    <ItemTemplate>
                        <%#GetNodeName(Eval("NodeID")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标题">
                    <ItemTemplate>
                        <a href="../AddContent.aspx?Gid=<%#Eval("GeneralID") %>" title="<%#Eval("Title") %>"><%#Eval("Title") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="录入者" DataField="Inputer" />
                <asp:TemplateField HeaderText="状态" >
                    <ItemTemplate>
                        <%# GetStatus( Eval("Status","{0}")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建时间">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem,"CreateTime","{0:yyyy年MM月dd日 hh:mm}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="更新时间">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem,"UpDateTime","{0:yyyy年MM月dd日 hh:mm}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbt1" CommandName="read" CommandArgument='<%#Eval("GeneralID") %>' runat="server">修改</asp:LinkButton>
                        <asp:LinkButton ID="lbt2" CommandName="del2" CommandArgument='<%#Eval("GeneralID") %>' runat="server" OnClientClick="return sureFunc();">删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center"/>
            <RowStyle Height="24px" HorizontalAlign="Center" />
        </ZL:ExGridView>
        
    </div>
</div>
</asp:Content>
