<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DaiKeManage.aspx.cs" Inherits="MIS_Ke_DaiKeManage" MasterPageFile="~/User/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>代课管理</title>
    
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a href="/User/">用户中心</a></li>
        <li><a href="ConfigList.aspx">排课配置</a></li>
        <li class="active">代课管理</li> 
    </ol>
</div>
<div class="container btn_green">
   <table class="table table-bordered table-striped">
       <tr><td></td><td>原任教师</td><td>代课教师</td><td>班级</td><td>星期</td><td>节数</td><td>代课日期</td></tr>
        <ZL:ExRepeater ID="RPT" runat="server" PageSize="10" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='8'><div class='text-center'>" PageEnd="</div></td></tr>">
            <ItemTemplate>
                <tr>
                    <td><input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /></td>
                    <td>
                        <%#Eval("TName") %>
                    </td>
                    <td>
                        <%#Eval("DTName") %>
                    </td>
                    <td>
                        <%#Eval("Regulationame") %>
                    </td>
                    <td>
                        <%#Eval("NumInfo").ToString().Split(',')[1] %>
                    </td>
                    <td>
                        <%#GetNumInfo() %>
                    </td>
                    <td>
                        <%#Eval("CDate") %>
                    </td>
                </tr>
            </ItemTemplate>        
        </ZL:ExRepeater>
   </table>
</div>
</asp:Content>
