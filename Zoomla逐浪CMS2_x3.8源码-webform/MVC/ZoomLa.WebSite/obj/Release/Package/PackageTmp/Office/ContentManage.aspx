<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentManage.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.ContentManage"  MasterPageFile="~/Common/Master/UserEmpty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>文档管理</title>
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script type="text/javascript">
    function GetTo() {
        location = 'AddContent.aspx?NodeID=<%=Server.HtmlEncode(Request.QueryString["NodeID"])%>';
        }
        $().ready(function () {
            $("#<%=EGV.ClientID%>  tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
            $("#chkAll").click(function () { selectAllByName(this, "idChk"); });
            $("tr:gt(0):not(:last)").mouseover(function () { $(this).removeClass("tdbg").addClass("tdbgmouseover") }).mouseout(function () { $(this).removeClass("tdbgmouseover").addClass("tdbg") });
        });
    </script>
<style type="text/css">
#EGV tr th {text-align: center;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="draftnav">
     <span><a href="/Office/Main.aspx">工作台</a>>><asp:Label ID="nodeNameL" runat="server" />
         <asp:LinkButton runat="server" ID="addBtn" OnClientClick="GetTo();return false;" >[添加新公文]</asp:LinkButton>
     </span>
</div>
<div id="site_main" style="margin:5px;">
    <div class="input-group" style=" width:400px;float:left;margin-bottom:10px;">
        <asp:TextBox runat="server" ID="searchText" placeholder="请输入需要查询的信息" CssClass="form-control"/>
        <span class="input-group-btn">
            <asp:Button runat="server" CssClass="btn btn-primary" ID="searchBtn" style="margin-left:10px;height:34px;" Text="搜索" OnClick="searchBtn_Click"/>
        </span>
    </div>
    <div class="tab3">
        <ZL:ExGridView runat="server" ID="EGV" DataKeyNames="GeneralID" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" enablec
             EnableTheming="False"  GridLines="None" CellPadding="2" CellSpacing="1"  Width="100%" CssClass="table table-striped table-bordered table-hover" EmptyDataText="没有任何数据!!" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound" >
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <input type="checkbox" name="idChk" value="<%#Eval("GeneralID") %>"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="ID" DataField="GeneralID" />
                <asp:TemplateField HeaderText="标题" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <a href="ViewContent.aspx?Gid=<%#Eval("GeneralID") %>" title="<%#Eval("Title") %>"><%#Eval("Title") %></a>
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
                        <%#DataBinder.Eval(Container.DataItem,"CreateTime","{0:yyyy年MM月dd日 HH:mm}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="更新时间">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem,"UpDateTime","{0:yyyy年MM月dd日 HH:mm}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="l1" runat="server" CommandName="SetTop"  CommandArgument='<%# Eval("GeneralID") %>' Visible="false"><%# GetStr(Eval("EliteLevel","{0}")) %></asp:LinkButton> 
                        <asp:LinkButton ID="l2" runat="server" CommandName="read" CommandArgument='<%#Eval("GeneralID") %>' >|阅读</asp:LinkButton> 
                        <asp:LinkButton ID="l3" runat="server" CommandName="Edit1" CommandArgument='<%#Eval("GeneralID") %>' >|修改</asp:LinkButton>
                        <asp:LinkButton ID="l4" runat="server" CommandName="Del2" CommandArgument='<%#Eval("GeneralID") %>' OnClientClick="return confirm('确定要将其移入回收站吗!!');">|删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center"/>
            <RowStyle Height="24px" HorizontalAlign="Center" />
        </ZL:ExGridView>
        <div style="height:10px;"></div>
        <asp:Button runat="server" ID="batDelBtn" CssClass="btn-primary" Text="批量移入回收站(Disuse)" OnClick="batDelBtn_Click" OnClientClick="return confirm('确定要删除吗?');" Visible="false"/>
    </div>
</div>
</asp:Content>

