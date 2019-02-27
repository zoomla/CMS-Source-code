<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZoneStyleManage.aspx.cs" Inherits="manage_ZoneStyleManage_StoreStyleManage" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>空间模板</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td></td>
            <td class="text-center td_s">ID</td>
            <td class="text-center td_l"><span>缩略图</span></td>
            <td class="text-center"><span>名称</span></td>
            <td class="text-center">路径</td>
            <td class="text-center"><span>状态</span></td>
            <td class="text-center"><span>操作</span></td>
        </tr>
        <ZL:ExRepeater ID="RPT" runat="server"  PageSize="10" OnItemCommand="RPT_ItemCommand"  
            PagePre="<tr id='page_tr'><td class='text-center'><input type='checkbox' id='chkAll'/></td><td colspan='10' id='page_td'>" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr ondblclick="javascript:window.location.href='ZoneStyleAdd.aspx?id=<%#Eval("ID")%>';">
                    <td class="text-center td_xs">
                        <input name="idchk" type="checkbox" value='<%# Eval("ID")%>' /></td>
                    <td class="text-center"><%#Eval("ID") %></td>
                    <td class="text-center">
                        <img src="<%#Eval("StylePic") %>" onerror="shownopic(this);" class="img_50" /></td>
                    <td class="text-center"><%#Eval("StyleName")%></td>
                    <td class="text-center"><%#Eval("UserIndexStyle") %></td>
                    <td class="text-center"><%#GetState(DataBinder.Eval(Container.DataItem, "StyleState").ToString())%></td>
                    <td class="text-center">
                        <a href="ZoneStyleAdd.aspx?id=<%#Eval("ID")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                        <asp:LinkButton runat="server" CommandName='del2' CommandArgument='<%#Eval("ID") %>'
                             OnClientClick="return confirm('不可恢复性删除数据,确定将该数据删除?');" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>       
    </table>
     <div>
         <asp:Button runat="server" ID="BatDel_Btn" Text="批量删除" CssClass="btn btn-primary" OnClick="BatDel_Btn_Click" OnClientClick="return confirm('确定要删除吗?');"/>
     </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/SelectCheckBox.js"></script>
<script>
    $(function () {
        $("#chkAll").click(function () { selectAllByName(document.getElementById("chkAll"), "idchk"); });
    })
</script>
</asp:Content>
