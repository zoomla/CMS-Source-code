<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PkList.aspx.cs" Inherits="ZoomLaCMS.MIS.Ke.PkList"  MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a href="/User/">用户中心</a></li>
        <li class="active">排课列表</li> 
    </ol>
</div>
<div class="container btn_green">
    <table class="table table-bordered table-striped">
        <tr><td></td><td>ID</td><td>用户名</td><td>班级</td><td>开始时间</td><td>结束时间</td><td>创建时间</td><td>操作</td></tr>
        <ZL:ExRepeater ID="RPT" runat="server" PageSize="10" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='8'><div class='text-center'>" OnItemCommand="RPT_ItemCommand" PageEnd="</div></td></tr>">
            <ItemTemplate>
                <tr>
                    <td><input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /></td>
                    <td><%#Eval("ID") %></td>
                    <td><%#GetUserName() %></td>
                    <td><%#Eval("Regulationame") %></td>
                    <td><%#Eval("SDate") %></td>
                    <td><%#Eval("EDate") %></td>
                    <td><%#Eval("CDate") %></td>
                    <td><a href="AutoPK.aspx?ClassID=<%#Eval("Ownclass") %>">查看详情</a></td>
                </tr>
            </ItemTemplate>
        </ZL:ExRepeater>        
    </table>
</div>
</asp:Content>
