<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigList.aspx.cs" Inherits="ZoomLaCMS.MIS.Ke.ConfigList"  MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>课程列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a href="/User/">用户中心</a></li>
        <li class="active">课程列表[<a href="SchConfig.aspx">添加课表</a>]</li>
    </ol>
    </div>
    <div class="container u_cnt">
    <table class="table table-bordered table-striped">
        <tr><td></td><td>课表名称</td><td>早读</td><td>上午节数</td><td>下午节数</td><td>晚上节数</td><td>每周天数</td><td>创建人</td><td>操作</td></tr>
        <ZL:ExRepeater ID="RPT" runat="server" PageSize="10" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='8'><div class='text-center'>" OnItemCommand="RPT_ItemCommand" PageEnd="</div></td></tr>">
            <ItemTemplate>
                <tr>
                    <td><input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /></td>
                    <td><%#Eval("TermName") %></td>
                    <td><%#Eval("premoning") %></td>
                    <td><%#Eval("moring") %></td>
                    <td><%#Eval("afternoon") %></td>
                    <td><%#Eval("evening") %></td>
                    <td><%#Eval("weekday") %></td>
                    <td><%#ComRE.GetNoEmptyStr(Eval("Alias",""),Eval("UserName","")) %></td>
                    <td>
                        <a href="SchConfig.aspx?id=<%#Eval("ID") %>" class="option_style" title="查看"><i class="fa fa-edit"></i></a>
                        <asp:LinkButton runat="server" CommandName="Del"  OnClientClick="return confirm('是否删除!')" CommandArgument='<%#Eval("ID") %>' CssClass="option_style"><i class="fa fa-trash"></i>删除</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
        </div>
    <script>
        //$().ready(function () {
        //    openmenu('menu4');
        //});

    </script>
</asp:Content>

